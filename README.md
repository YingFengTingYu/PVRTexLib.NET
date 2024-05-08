# PVRTexLib.NET
## Introduction
PVRTexLib.NET is a wrapper for PVRTexLib supporting .NET Framework 3.5, .NET Standard 1.1 and upper. It contains all the API from PVRTexLib.  
The documents can be found through installing PVRTexTool.
## License
PVRTexLib.NET is licensed under the MIT license.  
This product includes components of the PowerVR Tools Software from Imagination Technologies Limited.
## Supported Platforms
PVRTexLib.NET try to support all the platforms which PVRTexLib is supporting.  
For best compatibility, it is recommended that PVRTexTool is installed on machines running at least Windows 7, macOS 10.11 (El Capitan), or Ubuntu 16.04.
### Windows
x86 and x64
### macOS
x64 and arm64
### Linux
x86, x64, arm, arm64 and riscv64
## Quick Start
Create PVRTexture instance to handle your textures.  

```csharp
using PVRTexLib;

class TextureTranscoder
{
    static void Main(string[] args)
    {
        EncodeTexture("D:\\in.png", "D:\\out.bin", (ulong)PVRTexLibPixelFormat.PVRTLPF_PVRTCII_HDR_8bpp);
        DecodeTexture("D:\\out.bin", "D:\\in2.png");
    }
    
    public static unsafe void EncodeTexture(string inFile, string outFile, ulong outFormat)
    {
        using (Bitmap bitmap = new Bitmap(Image.FromFile(inFile)))
        {
            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            using (PVRTextureHeader header = new PVRTextureHeader(PVRDefine.PVRTGENPIXELID4('b', 'g', 'r', 'a', 8, 8, 8, 8), (uint)bitmap.Width, (uint)bitmap.Height, 1, 1, 1, 1, PVRTexLibColourSpace.PVRTLCS_sRGB, PVRTexLibVariableType.PVRTLVT_UnsignedByteNorm, false))
            {
                using (PVRTexture tex = new PVRTexture(header, (void*)data.Scan0))
                {
                    if (tex.GetTextureDataSize() != 0)
                    {
                        if (tex.Transcode(outFormat, PVRTexLibVariableType.PVRTLVT_UnsignedByteNorm, PVRTexLibColourSpace.PVRTLCS_sRGB, 0, false))
                        {
                            using (Stream outStream = File.Create(outFile))
                            {
                                using (BinaryWriter bw = new BinaryWriter(outStream))
                                {
                                    bw.Write(bitmap.Width);
                                    bw.Write(bitmap.Height);
                                    bw.Write(outFormat);
                                    bw.Write(new ReadOnlySpan<byte>(tex.GetTextureDataPointer(0), (int)tex.GetTextureDataSize(0)));
                                }
                            }
                        }
                    }
                }
            }
            bitmap.UnlockBits(data);
        }
    }

    public static unsafe void DecodeTexture(string inFile, string outFile)
    {
        ulong size = 0ul;
        byte[] rawTexDataArray;
        int width, height;
        ulong inFormat;
        using (Stream inStream = File.OpenRead(inFile))
        {
            using (BinaryReader br = new BinaryReader(inStream))
            {
                width = br.ReadInt32();
                height = br.ReadInt32();
                inFormat = br.ReadUInt64();
                size = (ulong)(inStream.Length - 16);
                rawTexDataArray = new byte[size];
                br.Read(rawTexDataArray);
            }
        }
        using (Bitmap bitmap = new Bitmap(width, height))
        {
            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            using (PVRTextureHeader header = new PVRTextureHeader(inFormat, (uint)width, (uint)height, 1, 1, 1, 1, PVRTexLibColourSpace.PVRTLCS_sRGB, PVRTexLibVariableType.PVRTLVT_UnsignedByteNorm, false))
            {
                fixed (byte* rawTexDataPtr = &rawTexDataArray[0])
                {
                    using (PVRTexture tex = new PVRTexture(header, rawTexDataPtr))
                    {
                        if (tex.GetTextureDataSize() != 0)
                        {
                            if (tex.Transcode(PVRDefine.PVRTGENPIXELID4('b', 'g', 'r', 'a', 8, 8, 8, 8), PVRTexLibVariableType.PVRTLVT_UnsignedByteNorm, PVRTexLibColourSpace.PVRTLCS_sRGB, 0, false))
                            {
                                NativeMemory.Copy(tex.GetTextureDataPointer(0), (void*)data.Scan0, (nuint)tex.GetTextureDataSize(0));
                            }
                        }
                    }
                }
            }
            bitmap.UnlockBits(data);
            bitmap.Save(outFile);
        }
    }
}
```
## Examples of Using the PVRTexTool Library
This section demonstrates a few of examples of using the PVRTexTool library with the required code shown.
### Reading and Transcoding an Image
In this example, an existing file is read from disk and then transcoded to RGBA8888. The resulting texture object can then be used for later processing.
```csharp
public static PVRTexture? ReadAndTranscodeImage(string filePath)
{
    // Open and read a texture from the file location specified by filePath.
    // Accepted file formats are: PVR, KTX, KTX2, ASTC, DDS,
    // PNG, JPEG, BMP, TGA, GIF, HDR, PSD, PPM, PGM and PIC
    using PVRTexture texture = new PVRTexture(filePath);

    // Check that PVRTexLib loaded the file successfully
    if (texture.GetTextureDataSize() == 0)
    {
        return null;
    }

    // Decompress texture to the standard RGBA8888 format.
    ulong RGBA8888 = PVRDefine.PVRTGENPIXELID4('r', 'g', 'b', 'a', 8, 8, 8, 8);

    if (!texture.Transcode(RGBA8888, PVRTexLibVariableType.UnsignedByteNorm, PVRTexLibColourSpace.Linear))
    {
        return null;
    }

    // texture is now in the format RGBA8888,
    // with each channel being of type unsigned integer,
    // and in linear colour space (i.e. without gamma correction).
    return new PVRTexture(in texture);
}
```
### Pre-processing a Texture
In this example, a file is loaded, the pixel format is checked to determine if it is compressed or packed and transcoded to a usable format if required. The texture is then converted into a normal map of the same dimensions and a full MIP map chain is generated. Finally the texture is encoded into PVRTC1 four bits per pixel and saved to an output file.  
The memory backing the data pointer returned from GetTextureDataPointer(...) is tied to the lifetime of the PVRTexture object.
```csharp
public static bool ApplySomeProcessing(string inFilePath, string outFilePath)
{
    // Open and read a texture from the file location specified by inFilePath.
    // Accepted file formats are: PVR, KTX, KTX2, ASTC, DDS,
    // PNG, JPEG, BMP, TGA, GIF, HDR, PSD, PPM, PGM and PIC
    using PVRTexture texture = new PVRTexture(inFilePath);

    // Check that PVRTexLib loaded the file successfully
    if (texture.GetTextureDataSize() == 0)
    {
        return false;
    }

    // Pre-processing functions will not with some formats so
    // check the input texture format to determine if the data is compressed
    // or the format is packed.
    if ((texture.GetTexturePixelFormat() & PVRDefine.PVRTEX_PFHIGHMASK) == 0 || texture.TextureHasPackedChannelData())
    {
        // Decompress texture to the standard RGBA8888 format.
        // Note: Any decompressed, non-packed pixel format would work here,
        // for example: R32G32B32A32 Signed float
        ulong RGBA8888 = PVRDefine.PVRTGENPIXELID4('r', 'g', 'b', 'a', 8, 8, 8, 8);

        if (!texture.Transcode(RGBA8888, PVRTexLibVariableType.UnsignedByteNorm, PVRTexLibColourSpace.Linear))
        {
            return false;
        }
    }

    // Convert the image to a Normal Map with a scale of 5.0, and y/z/x channel order
    if (!texture.GenerateNormalMap(5.0f, "yzx"))
    {
        return false;
    }

    // Generate MIP-map chain
    if (!texture.GenerateMIPMaps(PVRTexLibResizeMode.Linear))
    {
        return false;
    }

    // Compress to PVRTC 4bpp.
    // Note: A better compressor quality will improve image quality,
    // at the expensive of compression speed.
    if (!texture.Transcode((ulong)PVRTexLibPixelFormat.PVRTCI_4bpp_RGB, PVRTexLibVariableType.UnsignedByteNorm, PVRTexLibColourSpace.Linear, PVRTexLibCompressorQuality.PVRTCNormal))
    {
        return false;
    }

    // Save the texture to file location specified by outFilePath.
    // The file type will be determined by the extension present in the string.
    // Valid extensions are: PVR, KTX, KTX2, ASTC, DDS and h
    // If no extension is present the PVR format will be selected.
    return texture.SaveToFile(outFilePath);
}
```
### Reading an Image and Resizing the Canvas
In this example, an existing file is opened, the pixel format is checked to determine if it is compressed or packed and transcoded to a usable format if required. The canvas is resized to 512x256, leaving the (original) canvas in the top left of the texture. A full MIP map chain is then generated and the texture is saved to a file.
```csharp
public static bool ResizeCanvasAndGenMipChain(string inFilePath, string outFilePath)
{
    // Open and read a texture from the file location specified by inFilePath.
    // Accepted file formats are: PVR, KTX, KTX2, ASTC, DDS,
    // PNG, JPEG, BMP, TGA, GIF, HDR, PSD, PPM, PGM and PIC
    using PVRTexture texture = new PVRTexture(inFilePath);

    // Check that PVRTexLib loaded the file successfully
    if (texture.GetTextureDataSize() == 0)
    {
        return false;
    }

    // Pre-processing functions will not with some formats so
    // check the input texture format to determine if the data is compressed
    // or the format is packed.
    if ((texture.GetTexturePixelFormat() & PVRDefine.PVRTEX_PFHIGHMASK) == 0 || texture.TextureHasPackedChannelData())
    {
        // Decompress texture to the standard RGBA8888 format.
        // Note: Any decompressed, non-packed pixel format would work here,
        // for example: R32G32B32A32 Signed float
        ulong RGBA8888 = PVRDefine.PVRTGENPIXELID4('r', 'g', 'b', 'a', 8, 8, 8, 8);

        if (!texture.Transcode(RGBA8888, PVRTexLibVariableType.UnsignedByteNorm, PVRTexLibColourSpace.Linear))
        {
            return false;
        }
    }

    // Generate MIP-map chain
    if (!texture.GenerateMIPMaps(PVRTexLibResizeMode.Linear))
    {
        return false;
    }

    // Resize canvas
    if (!texture.ResizeCanvas(512U, 256U, 1U, 0, 0, 0))
    {
        return false;
    }

    // Save the texture to file location specified by outFilePath.
    // The file type will be determined by the extension present in the string.
    // Valid extensions are: PVR, KTX, KTX2, ASTC, DDS and h
    // If no extension is present the PVR format will be selected.
    return texture.SaveToFile(outFilePath);
}
```
### Creating an Image from Pixel Data
In this example, we show how to create a texture from raw pixel data and a PVRTextureHeader object, the resulting texture is then saved to a file.
```csharp
public static bool CreateTextureFromPixelData(string outFilePath)
{
    // Create a texture header for a RGBA8888 image of 512x256x1 pixels,
    // with 1 mip level, 1 array and 1 face.
    ulong RGBA8888 = PVRDefine.PVRTGENPIXELID4('r', 'g', 'b', 'a', 8, 8, 8, 8);
    uint width = 512U;
    uint height = 256U;
    uint depth = 1U;
    uint numMipMaps = 1U;
    uint numArrayMembers = 1U;
    uint numFaces = 1U;
    using PVRTextureHeader textureHeader = new PVRTextureHeader(RGBA8888, width, height, depth, numMipMaps, numArrayMembers, numFaces);
    ulong textureSize = textureHeader.GetTextureDataSize();

    if (textureSize == 0)
    {
        return false;
    }

    // Create a buffer to temporarily hold the pixel data
    byte[] textureData = new byte[textureSize];

    /*
        Fill in texture data...
    */

    // Create a new texture from the header and pixel data.
    unsafe
    {
        fixed (byte* ptr = &textureData[0])
        {
            using PVRTexture texture = new PVRTexture(textureHeader, ptr);
            return texture.SaveToFile(outFilePath);
        }
    }
}
```
### Reading Metadata
In this example we show how to use the API to read meta data from a PVRTexture object.
```csharp
public static bool ReadTextureMetaData(string inFilePath)
{
    // Open and read a texture from the file location specified by inFilePath.
    // Accepted file formats are: PVR, KTX, KTX2, ASTC, DDS,
    // PNG, JPEG, BMP, TGA, GIF, HDR, PSD, PPM, PGM and PIC
    using PVRTexture texture = new PVRTexture(inFilePath);

    // Check that PVRTexLib loaded the file successfully
    if (texture.GetTextureDataSize() == 0)
    {
        return false;
    }

    // Attempt to retrieve the meta data for the textures orientation
    // You may choose your own values for devFOURCC and key.
    // In this case we are using the PVR FOURCC and
    // PVRTexLibMetaData::PVRTLMD_TextureOrientation.
    MetaDataBlock metaData;
    const uint key = (uint)PVRTexLibMetaData.TextureOrientation;
    const uint devFOURCC = PVRDefine.PVRTEX_CURR_IDENT;

    // Check the call was successful
    // If the meta data doesn't exist, a block with a data size
    // of 0 will be returned.
    if ((metaData = texture.GetMetaDataBlock(key, devFOURCC)) != null && metaData.u32DataSize != 0)
    {
        // Do something...
    }

    // PVRTexLib also has some baked in meta data accessors...
    // For examaple direct data access to texture atlas meta data.
    unsafe
    {
        uint dataCount;
        float* atlasData = texture.GetTextureAtlasData(&dataCount);

        // Note: The memory backing 'atlasData' is tied to the
        // lifetime of the 'texture' object.

        if (dataCount != 0)
        {
            for (uint n = 0U; n < dataCount; n++)
            {
                Console.WriteLine("Atlas Data ({0}): {1}", n, atlasData[n]);
            }
        }
    }

    // ...also the texture cubemap order is easily queryable
    string cubeOrder = texture.GetTextureCubeMapOrder();
    Console.WriteLine("Cubemap order: {0}", cubeOrder);
    return true;
}
```
### Writing Meta Data
In this example, an existing file is read from disk, we create a MetaDataBlock and then insert the meta data into the texture, finally we save the modified texture back to a file.
```csharp
public static bool WriteMetaData(string inFilePath, string outFilePath)
{
    // Open and read a texture from the file location specified by inFilePath.
    // Accepted file formats are: PVR, KTX, KTX2, ASTC, DDS,
    // PNG, JPEG, BMP, TGA, GIF, HDR, PSD, PPM, PGM and PIC
    using PVRTexture texture = new PVRTexture(inFilePath);

    // Check that PVRTexLib loaded the file successfully
    if (texture.GetTextureDataSize() == 0)
    {
        return false;
    }

    // Meta data size in bytes
    const uint META_DATA_SIZE = 3U;

    // Create meta data block
    // FourCC 'PVR', developers may use their own FourCC
    // Meta data key, in this case texture orientation
    MetaDataBlock metaData = new MetaDataBlock();
    metaData.DevFOURCC = PVRDefine.PVRTEX_CURR_IDENT;
    metaData.u32Key = (uint)PVRTexLibMetaData.TextureOrientation;
    metaData.u32DataSize = META_DATA_SIZE;
    metaData.Data = new byte[META_DATA_SIZE];

    metaData.Data[(int)PVRTexLibAxis.X] = (byte)PVRTexLibOrientation.Left;
    metaData.Data[(int)PVRTexLibAxis.Y] = (byte)PVRTexLibOrientation.Up;
    metaData.Data[(int)PVRTexLibAxis.Z] = (byte)PVRTexLibOrientation.Out;

    // Insert the meta data into the texture
    texture.AddMetaData(metaData);

    // Save the texture to file location specified by outFilePath.
    // The file type will be determined by the extension present in the string.
    // Valid extensions are: PVR, KTX, KTX2, ASTC, DDS and h
    // If no extension is present the PVR format will be selected.
    return texture.SaveToFile(outFilePath);
}
```
### Accessing Pixel Data Directly
This example demonstrates how to use the GetTextureDataPointer(...) API which allows for direct data access. First a file is loaded, the pixel format is checked to determine if it is compressed or packed and transcoded to a usable format if required. Then each surface (Mip level, array member, face and Z slice) of the texture is iterated over, for each surface every pixel is iterated over, this allows for access to every pixel in the entire texture, each pixel can then be modified by the user, if desired.  
The appropriate data type for accessing the pixel channel data can be determined via the GetTextureChannelType() API. The stride between each pixel can be calculated by querying the bits per pixel (via GetTextureBitsPerPixel() API) and diving by 8.  
The memory backing the data pointer returned from GetTextureDataPointer(...) is tied to the lifetime of the PVRTexture object.
```csharp
public static bool AccessingPixelDataDirectly(string inFilePath, string outFilePath)
{
    // Open and read a texture from the file location specified by inFilePath.
    // Accepted file formats are: PVR, KTX, KTX2, ASTC, DDS,
    // PNG, JPEG, BMP, TGA, GIF, HDR, PSD, PPM, PGM and PIC
    using PVRTexture texture = new PVRTexture(inFilePath);

    // Check that PVRTexLib loaded the file successfully
    if (texture.GetTextureDataSize() == 0)
    {
        return false;
    }

    // Check the input texture format to determine if the data is compressed
    // or the format is packed.
    if ((texture.GetTexturePixelFormat() & PVRDefine.PVRTEX_PFHIGHMASK) == 0 || texture.TextureHasPackedChannelData())
    {
        // Dont want to deal with accessing compressed or packed data so first
        // transcode the texture to RGBA8888 format.
        // Note: Any decompressed, non-packed pixel format would work here,
        // for example: R32G32B32A32 Signed float
        ulong RGBA8888 = PVRDefine.PVRTGENPIXELID4('r', 'g', 'b', 'a', 8, 8, 8, 8);

        if (!texture.Transcode(RGBA8888, PVRTexLibVariableType.UnsignedByteNorm, PVRTexLibColourSpace.Linear))
        {
            return false;
        }
    }

    uint mipLevelCount = texture.GetTextureNumMipMapLevels();
    uint arrayCount = texture.GetTextureNumArrayMembers();
    uint faceCount = texture.GetTextureNumFaces();
    PVRTexLibVariableType channelType = texture.GetTextureChannelType();
    uint bytesPerPixel = texture.GetTextureBitsPerPixel() / 8U;
    uint numChannels = texture.GetTextureChannelCount();

    // Loop over every surface in the texture.
    // All Mip levels
    unsafe
    {
        for (uint level = 0U; level < mipLevelCount; level++)
        {
            // Width and height for this Mip level
            uint levelWidth = texture.GetTextureWidth(level);
            uint levelHeight = texture.GetTextureHeight(level);

            // Number of pixel for this (2D) surface
            uint numPixels = levelWidth * levelHeight;

            // Dimension in the Z axis for this Mip level
            uint levelDepth = texture.GetTextureDepth(level);


            // All array members
            for (uint array = 0U; array < arrayCount; ++array)
            {
                // All faces
                for (uint face = 0U; face < faceCount; ++face)
                {
                    // All Z slices (3D textures)
                    for (uint slice = 0U; slice < levelDepth; ++slice)
                    {
                        byte* data = (byte*)texture.GetTextureDataPointer(level, array, array, slice);

                        // All pixels in this surface
                        for (uint pixel = 0U; pixel < numPixels; ++pixel)
                        {
                            switch (channelType)
                            {
                                case PVRTexLibVariableType.UnsignedByteNorm:
                                case PVRTexLibVariableType.UnsignedByte:
                                    {
                                        DoSomethingWithPixel(numChannels, (byte*)data);
                                        break;
                                    }
                                case PVRTexLibVariableType.SignedByteNorm:
                                case PVRTexLibVariableType.SignedByte:
                                    {
                                        DoSomethingWithPixel(numChannels, (sbyte*)data);
                                        break;
                                    }
                                case PVRTexLibVariableType.UnsignedShortNorm:
                                case PVRTexLibVariableType.UnsignedShort:
                                    {
                                        DoSomethingWithPixel(numChannels, (ushort*)data);
                                        break;
                                    }
                                case PVRTexLibVariableType.SignedShortNorm:
                                case PVRTexLibVariableType.SignedShort:
                                    {
                                        DoSomethingWithPixel(numChannels, (short*)data);
                                        break;
                                    }
                                case PVRTexLibVariableType.UnsignedIntegerNorm:
                                case PVRTexLibVariableType.UnsignedInteger:
                                    {
                                        DoSomethingWithPixel(numChannels, (uint*)data);
                                        break;
                                    }
                                case PVRTexLibVariableType.SignedIntegerNorm:
                                case PVRTexLibVariableType.SignedInteger:
                                    {
                                        DoSomethingWithPixel(numChannels, (int*)data);
                                        break;
                                    }
                                case PVRTexLibVariableType.SignedFloat:
                                case PVRTexLibVariableType.UnsignedFloat:
                                    {
                                        DoSomethingWithPixel(numChannels, (float*)data);
                                        break;
                                    }
                                default: throw new Exception("Unknown channel type");
                            }

                            data += bytesPerPixel;
                        }
                    }
                }
            }
        }
    }

    // Save the modified texture to the file location specified by outFilePath.
    // The file type will be determined by the extension present in the string.
    // Valid extensions are: PVR, KTX, KTX2, ASTC, DDS and h
    // If no extension is present the PVR format will be selected.
    return texture.SaveToFile(outFilePath);
}
```