using PVRTexLib;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Example
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EncodeTexture("D:\\in.png", "D:\\out.bin", (ulong)PVRTexLibPixelFormat.PVRTCII_HDR_8bpp);
            DecodeTexture("D:\\out.bin", "D:\\in2.png");
        }

        static unsafe void Transcode(void* inData, void* outData, uint width, uint height, ulong inFormat, ulong outFormat, PVRTexLibCompressorQuality quality, bool dither)
        {
            using PVRTextureHeader header = new PVRTextureHeader(inFormat, width, height, 1, 1, 1, 1, PVRTexLibColourSpace.sRGB, PVRTexLibVariableType.UnsignedByteNorm, false);
            using PVRTexture tex = new PVRTexture(header, inData);
            if (tex.GetTextureDataSize() != 0)
            {
                if (tex.Transcode(outFormat, PVRTexLibVariableType.UnsignedByteNorm, PVRTexLibColourSpace.sRGB, quality, dither))
                {
                    NativeMemory.Copy(tex.GetTextureDataPointer(0), outData, (nuint)tex.GetTextureDataSize(0));
                }
            }
        }

        public static unsafe void EncodeTexture(string inFile, string outFile, ulong outFormat)
        {
            using (Bitmap bitmap = new Bitmap(Image.FromFile(inFile)))
            {
                BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
                using (PVRTextureHeader header = new PVRTextureHeader(PVRDefine.PVRTGENPIXELID4('b', 'g', 'r', 'a', 8, 8, 8, 8), (uint)bitmap.Width, (uint)bitmap.Height, 1, 1, 1, 1, PVRTexLibColourSpace.sRGB, PVRTexLibVariableType.UnsignedByteNorm, false))
                {
                    using (PVRTexture tex = new PVRTexture(header, (void*)data.Scan0))
                    {
                        if (tex.GetTextureDataSize() != 0)
                        {
                            if (tex.Transcode(outFormat, PVRTexLibVariableType.UnsignedByteNorm, PVRTexLibColourSpace.sRGB, 0, false))
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
                using (PVRTextureHeader header = new PVRTextureHeader(inFormat, (uint)width, (uint)height, 1, 1, 1, 1, PVRTexLibColourSpace.sRGB, PVRTexLibVariableType.UnsignedByteNorm, false))
                {
                    fixed (byte* rawTexDataPtr = &rawTexDataArray[0])
                    {
                        using (PVRTexture tex = new PVRTexture(header, rawTexDataPtr))
                        {
                            if (tex.GetTextureDataSize() != 0)
                            {
                                if (tex.Transcode(PVRDefine.PVRTGENPIXELID4('b', 'g', 'r', 'a', 8, 8, 8, 8), PVRTexLibVariableType.UnsignedByteNorm, PVRTexLibColourSpace.sRGB, 0, false))
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
}
