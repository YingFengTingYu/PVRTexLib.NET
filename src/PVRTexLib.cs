using System;
using System.Runtime.InteropServices;
using System.Security;

namespace PVRTexLib
{
    /// <summary>
    /// Structure containing various texture header parameters for
    /// PVRTexLib_CreateTextureHeader().
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PVRHeader_CreateParams
    {
        /// <summary>
        /// pixel format
        /// </summary>
        public ulong pixelFormat;
        /// <summary>
        /// texture width
        /// </summary>
        public uint width;
        /// <summary>
        /// texture height
        /// </summary>
        public uint height;
        /// <summary>
        /// texture depth
        /// </summary>
        public uint depth;
        /// <summary>
        /// number of MIP maps
        /// </summary>
        public uint numMipMaps;
        /// <summary>
        /// number of array members
        /// </summary>
        public uint numArrayMembers;
        /// <summary>
        /// number of faces
        /// </summary>
        public uint numFaces;
        /// <summary>
        /// colour space
        /// </summary>
        public PVRTexLibColourSpace colourSpace;
        /// <summary>
        /// channel type
        /// </summary>
        public PVRTexLibVariableType channelType;
        /// <summary>
        /// has the RGB been pre-multiplied by the alpha?
        /// </summary>
        public bool preMultiplied;
    }

    /// <summary>
    /// Structure containing a textures orientation in each axis.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PVRTexLib_Orientation
    {
        /// <summary>
        /// X axis orientation
        /// </summary>
        public PVRTexLibOrientation x;
        /// <summary>
        /// Y axis orientation
        /// </summary>
        public PVRTexLibOrientation y;
        /// <summary>
        /// Z axis orientation
        /// </summary>
        public PVRTexLibOrientation z;
    }

    /// <summary>
    /// Structure containing a OpenGL[ES] format.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PVRTexLib_OpenGLFormat
    {
        /// <summary>
        /// GL internal format
        /// </summary>
        public uint internalFormat;
        /// <summary>
        /// GL format
        /// </summary>
        public uint format;
        /// <summary>
        /// GL type
        /// </summary>
        public uint type;
    }

    /// <summary>
    /// Structure containing a block of meta data for a texture.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PVRTexLib_MetaDataBlock
    {
        /// <summary>
        /// A 4cc descriptor of the data type's creator. Values starting with 'PVR' are reserved for PVRTexLib.
        /// </summary>
        public uint DevFOURCC;
        /// <summary>
        /// A unique value identifying the data type, and thus how to read it. For example PVRTexLibMetaData.
        /// </summary>
        public uint u32Key;
        /// <summary>
        /// Size of 'Data' in bytes.
        /// </summary>
        public uint u32DataSize;
        /// <summary>
        /// Meta data bytes
        /// </summary>
        public byte* Data;

        /// <summary>
        /// Reset PVRTexLib_MetaDataBlock
        /// </summary>
        public unsafe void Reset()
        {
            DevFOURCC = PVRDefine.PVRTEX_CURR_IDENT;
            u32Key = 0u;
            u32DataSize = 0u;
            Data = null;
        }
    }

#if NET8_0_OR_GREATER
    /// <summary>
    /// An array with 4 elements
    /// </summary>
    /// <typeparam name="T">element type</typeparam>
    [System.Runtime.CompilerServices.InlineArray(4)]
    public struct Buffer4<T>
    {
        private T _element0;
    }
#endif

    /// <summary>
    /// Structure containing the transcoder options for
    /// PVRTexLib_TranscodeTexture().
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct PVRTexLib_TranscoderOptions
    {
        /// <summary>
        /// For versioning - sizeof(PVRTexLib_TranscoderOptions)
        /// </summary>
        public uint sizeofStruct;
        /// <summary>
        /// Pixel format type
        /// </summary>
        public ulong pixelFormat;
#if NET8_0_OR_GREATER
        /// <summary>
        /// Per-channel variable type.
        /// </summary>
        public Buffer4<PVRTexLibVariableType> channelType;
#else
        /// <summary>
        /// Per-channel variable type.
        /// </summary>
        public PVRTexLibVariableType channelType0;
        /// <summary>
        /// Per-channel variable type.
        /// </summary>
        public PVRTexLibVariableType channelType1;
        /// <summary>
        /// Per-channel variable type.
        /// </summary>
        public PVRTexLibVariableType channelType2;
        /// <summary>
        /// Per-channel variable type.
        /// </summary>
        public PVRTexLibVariableType channelType3;
#endif
        /// <summary>
        /// Colour space
        /// </summary>
        public PVRTexLibColourSpace colourspace;
        /// <summary>
        /// Compression quality for PVRTC, ASTC, ETC, BASISU and IMGIC, higher quality usually requires more processing time.
        /// </summary>
        public PVRTexLibCompressorQuality quality;
        /// <summary>
        /// Apply dithering to lower precision formats.
        /// </summary>
        public bool doDither;
        /// <summary>
        /// Max range value for RGB[M|D] encoding
        /// </summary>
        public float maxRange;
        /// <summary>
        /// Max number of threads to use for transcoding, if set to 0 PVRTexLib will use all available cores.
        /// </summary>
        public uint maxThreads;
    }

    /// <summary>
    /// Structure containing the resulting error metrics computed by:
    /// PVRTexLib_MaxDifference(),
    /// PVRTexLib_MeanError(),
    /// PVRTexLib_MeanSquaredError(),
    /// PVRTexLib_RootMeanSquaredError(),
    /// PVRTexLib_StandardDeviation(),
    /// PVRTexLib_PeakSignalToNoiseRatio().
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PVRTexLib_ErrorMetrics
    {
        /// <summary>
        /// Per-channel metrics
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct PVRTexLib_ErrorMetrics_Channel
        {
            /// <summary>
            /// Channel name. PVRTLCN_NoChannel indicates invalid entry.
            /// </summary>
            public PVRTexLibChannelName name;
            /// <summary>
            /// Value for this channel.
            /// </summary>
            public double value;
        }

#if NET8_0_OR_GREATER
        /// <summary>
        /// Per-channel metrics, not all entries have to be valid.
        /// </summary>
        public Buffer4<PVRTexLib_ErrorMetrics_Channel> channel;
#else
        /// <summary>
        /// Per-channel metrics, not all entries have to be valid.
        /// </summary>
        public PVRTexLib_ErrorMetrics_Channel channel0;
        /// <summary>
        /// Per-channel metrics, not all entries have to be valid.
        /// </summary>
        public PVRTexLib_ErrorMetrics_Channel channel1;
        /// <summary>
        /// Per-channel metrics, not all entries have to be valid.
        /// </summary>
        public PVRTexLib_ErrorMetrics_Channel channel2;
        /// <summary>
        /// Per-channel metrics, not all entries have to be valid.
        /// </summary>
        public PVRTexLib_ErrorMetrics_Channel channel3;
#endif
        /// <summary>
        /// Value for all channels.
        /// </summary>
        public double allChannels;
        /// <summary>
        /// Value for RGB channels.
        /// </summary>
        public double rgbChannels;
    }

    /// <summary>
    /// Native functions
    /// </summary>
    public static unsafe partial class PVRTexLibNative
    {
        private const string PVRTexLibName = "PVRTexLib";

        /// <summary>
        /// Callback for GetMetaDataBlock
        /// </summary>
        /// <param name="allocSize">alloc memory size</param>
        /// <returns>memory pointer</returns>
        public delegate IntPtr GetMetaDataBlockAllocCallback(uint allocSize);

        /// <summary>
        /// Callback for TextureCreateRaw
        /// </summary>
        /// <param name="allocSize">alloc memory size</param>
        /// <returns>memory pointer</returns>
        public delegate IntPtr TextureCreateRawAllocCallback(ulong allocSize);

        /// <summary>
        /// Callback for SaveTextureToMemory
        /// </summary>
        /// <param name="privateData">raw pointer</param>
        /// <param name="allocSize">alloc memory size</param>
        /// <returns>memory pointer</returns>
        public delegate IntPtr SaveTextureToMemoryRealloc(IntPtr privateData, ulong allocSize);

        /// <summary>
        /// Gets the cube map face order.
        /// cubeOrder string will be in the form "ZzXxYy" with capitals
        /// representing positive and lower case letters representing
        /// negative. I.e. Z=Z-Positive, z=Z-Negative.
        /// </summary>
        /// <param name="header">A handle to a previously allocated PVRTexLib_PVRTextureHeader.</param>
        /// <param name="cubeOrder">terminated cube map order string.</param>
        public static void PVRTexLib_GetTextureCubeMapOrder(void* header, out string cubeOrder)
        {
            sbyte* cubeOrderBuffer = stackalloc sbyte[7];
            PVRTexLib_GetTextureCubeMapOrder_Internal(header, cubeOrderBuffer);
            cubeOrder = Marshal.PtrToStringAnsi((IntPtr)cubeOrderBuffer);
        }

        /// <summary>
        /// Gets the bump map channel order relative to rgba.
        /// For	example, an RGB texture with bumps mapped to XYZ returns
        /// 'xyz'. A BGR texture with bumps in the order ZYX will also
        /// return 'xyz' as the mapping is the same: R=X, G=Y, B=Z.
        /// If the letter 'h' is present in the string, it means that
        /// the height map has been stored here.
        /// Other characters are possible if the bump map was created
        /// manually, but PVRTexLib will ignore these characters. They
        /// are returned simply for completeness.
        /// </summary>
        /// <param name="header">A handle to a previously allocated PVRTexLib_PVRTextureHeader.</param>
        /// <param name="bumpOrder">terminated bump map order string relative to rgba.</param>
        public static void PVRTexLib_GetTextureBumpMapOrder(void* header, out string bumpOrder)
        {
            sbyte* bumpOrderBuffer = stackalloc sbyte[5];
            PVRTexLib_GetTextureCubeMapOrder_Internal(header, bumpOrderBuffer);
            bumpOrder = Marshal.PtrToStringAnsi((IntPtr)bumpOrderBuffer);
        }

#if NET7_0_OR_GREATER
        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetDefaultTextureHeaderParams")]
        public static partial void PVRTexLib_SetDefaultTextureHeaderParams(PVRHeader_CreateParams* result);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_CreateTextureHeader")]
        public static partial void* PVRTexLib_CreateTextureHeader(PVRHeader_CreateParams* attribs);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_CreateTextureHeaderFromHeader")]
        public static partial void* PVRTexLib_CreateTextureHeaderFromHeader(PVRTextureHeaderV3* header, uint metaDataCount, PVRTexLib_MetaDataBlock* metaData);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_CopyTextureHeader")]
        public static partial void* PVRTexLib_CopyTextureHeader(void* header);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_DestroyTextureHeader")]
        public static partial void PVRTexLib_DestroyTextureHeader(void* header);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_TextureCreateRaw")]
        public static partial PVRTextureHeaderV3* PVRTexLib_TextureCreateRaw(uint width, uint height, uint depth, uint wMin, uint hMin, uint dMin, uint nBPP, [MarshalAs(UnmanagedType.I1)] bool bMIPMap, TextureCreateRawAllocCallback pfnAllocCallback);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_TextureLoadTiled")]
        public static partial void PVRTexLib_TextureLoadTiled(byte* pDst, uint widthDst, uint heightDst, byte* pSrc, uint widthSrc, uint heightSrc, uint elementSize, [MarshalAs(UnmanagedType.I1)] bool twiddled);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureBitsPerPixel")]
        public static partial uint PVRTexLib_GetTextureBitsPerPixel(void* header);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetFormatBitsPerPixel")]
        public static partial uint PVRTexLib_GetFormatBitsPerPixel(ulong u64PixelFormat);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureChannelCount")]
        public static partial uint PVRTexLib_GetTextureChannelCount(void* header);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureChannelType")]
        public static partial PVRTexLibVariableType PVRTexLib_GetTextureChannelType(void* header);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureColourSpace")]
        public static partial PVRTexLibColourSpace PVRTexLib_GetTextureColourSpace(void* header);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureWidth")]
        public static partial uint PVRTexLib_GetTextureWidth(void* header, uint mipLevel);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureHeight")]
        public static partial uint PVRTexLib_GetTextureHeight(void* header, uint mipLevel);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureDepth")]
        public static partial uint PVRTexLib_GetTextureDepth(void* header, uint mipLevel);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureSize")]
        public static partial uint PVRTexLib_GetTextureSize(void* header, int mipLevel, [MarshalAs(UnmanagedType.I1)] bool allSurfaces, [MarshalAs(UnmanagedType.I1)] bool allFaces);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureDataSize")]
        public static partial ulong PVRTexLib_GetTextureDataSize(void* header, int mipLevel, [MarshalAs(UnmanagedType.I1)] bool allSurfaces, [MarshalAs(UnmanagedType.I1)] bool allFaces);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureOrientation")]
        public static partial void PVRTexLib_GetTextureOrientation(void* header, PVRTexLib_Orientation* result);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureOpenGLFormat")]
        public static partial void PVRTexLib_GetTextureOpenGLFormat(void* header, PVRTexLib_OpenGLFormat* result);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureOpenGLESFormat")]
        public static partial void PVRTexLib_GetTextureOpenGLESFormat(void* header, PVRTexLib_OpenGLFormat* result);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureVulkanFormat")]
        public static partial uint PVRTexLib_GetTextureVulkanFormat(void* header);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureD3DFormat")]
        public static partial uint PVRTexLib_GetTextureD3DFormat(void* header);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureDXGIFormat")]
        public static partial uint PVRTexLib_GetTextureDXGIFormat(void* header);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureFormatMinDims")]
        public static partial void PVRTexLib_GetTextureFormatMinDims(void* header, uint* minX, uint* minY, uint* minZ);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetPixelFormatMinDims")]
        public static partial void PVRTexLib_GetPixelFormatMinDims(ulong ui64Format, uint* minX, uint* minY, uint* minZ);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureMetaDataSize")]
        public static partial uint PVRTexLib_GetTextureMetaDataSize(void* header);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureIsPreMultiplied")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static partial bool PVRTexLib_GetTextureIsPreMultiplied(void* header);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureIsFileCompressed")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static partial bool PVRTexLib_GetTextureIsFileCompressed(void* header);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureIsBumpMap")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static partial bool PVRTexLib_GetTextureIsBumpMap(void* header);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureBumpMapScale")]
        public static partial float PVRTexLib_GetTextureBumpMapScale(void* header);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetNumTextureAtlasMembers")]
        public static partial uint PVRTexLib_GetNumTextureAtlasMembers(void* header);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureAtlasData")]
        public static partial float* PVRTexLib_GetTextureAtlasData(void* header, uint* count);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureNumMipMapLevels")]
        public static partial uint PVRTexLib_GetTextureNumMipMapLevels(void* header);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureNumFaces")]
        public static partial uint PVRTexLib_GetTextureNumFaces(void* header);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureNumArrayMembers")]
        public static partial uint PVRTexLib_GetTextureNumArrayMembers(void* header);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureCubeMapOrder")]
        private static partial void PVRTexLib_GetTextureCubeMapOrder_Internal(void* header, sbyte* cubeOrder);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureBumpMapOrder")]
        private static partial void PVRTexLib_GetTextureBumpMapOrder_Internal(void* header, sbyte* bumpOrder);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTexturePixelFormat")]
        public static partial ulong PVRTexLib_GetTexturePixelFormat(void* header);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_TextureHasPackedChannelData")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static partial bool PVRTexLib_TextureHasPackedChannelData(void* header);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetTextureChannelType")]
        public static partial void PVRTexLib_SetTextureChannelType(void* header, PVRTexLibVariableType type);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetTextureColourSpace")]
        public static partial void PVRTexLib_SetTextureColourSpace(void* header, PVRTexLibColourSpace colourSpace);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetTextureD3DFormat")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static partial bool PVRTexLib_SetTextureD3DFormat(void* header, uint d3dFormat);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetTextureDXGIFormat")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static partial bool PVRTexLib_SetTextureDXGIFormat(void* header, uint dxgiFormat);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetTextureOGLFormat")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static partial bool PVRTexLib_SetTextureOGLFormat(void* header, PVRTexLib_OpenGLFormat* oglFormat);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetTextureOGLESFormat")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static partial bool PVRTexLib_SetTextureOGLESFormat(void* header, PVRTexLib_OpenGLFormat* oglesFormat);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetTextureVulkanFormat")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static partial bool PVRTexLib_SetTextureVulkanFormat(void* header, uint vulkanFormat);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetTexturePixelFormat")]
        public static partial void PVRTexLib_SetTexturePixelFormat(void* header, ulong format);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetTextureWidth")]
        public static partial void PVRTexLib_SetTextureWidth(void* header, uint width);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetTextureHeight")]
        public static partial void PVRTexLib_SetTextureHeight(void* header, uint height);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetTextureDepth")]
        public static partial void PVRTexLib_SetTextureDepth(void* header, uint depth);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetTextureNumArrayMembers")]
        public static partial void PVRTexLib_SetTextureNumArrayMembers(void* header, uint numMembers);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetTextureNumMIPLevels")]
        public static partial void PVRTexLib_SetTextureNumMIPLevels(void* header, uint numMIPLevels);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetTextureNumFaces")]
        public static partial void PVRTexLib_SetTextureNumFaces(void* header, uint numFaces);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetTextureOrientation")]
        public static partial void PVRTexLib_SetTextureOrientation(void* header, PVRTexLib_Orientation* orientation);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetTextureIsFileCompressed")]
        public static partial void PVRTexLib_SetTextureIsFileCompressed(void* header, [MarshalAs(UnmanagedType.I1)] bool isFileCompressed);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetTextureIsPreMultiplied")]
        public static partial void PVRTexLib_SetTextureIsPreMultiplied(void* header, [MarshalAs(UnmanagedType.I1)] bool isPreMultiplied);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureBorder")]
        public static partial void PVRTexLib_GetTextureBorder(void* header, uint* borderWidth, uint* borderHeight, uint* borderDepth);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetMetaDataBlock")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static partial bool PVRTexLib_GetMetaDataBlock(void* header, uint devFOURCC, uint key, PVRTexLib_MetaDataBlock* dataBlock, GetMetaDataBlockAllocCallback pfnAllocCallback);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_TextureHasMetaData")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static partial bool PVRTexLib_TextureHasMetaData(void* header, uint devFOURCC, uint key);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetTextureBumpMap")]
        public static partial void PVRTexLib_SetTextureBumpMap(void* header, float bumpScale, [MarshalAs(UnmanagedType.LPStr)] string bumpOrder);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetTextureAtlas")]
        public static partial void PVRTexLib_SetTextureAtlas(void* header, float* atlasData, uint dataSize);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetTextureCubeMapOrder")]
        public static partial void PVRTexLib_SetTextureCubeMapOrder(void* header, [MarshalAs(UnmanagedType.LPStr)] string cubeMapOrder);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetTextureBorder")]
        public static partial void PVRTexLib_SetTextureBorder(void* header, uint borderWidth, uint borderHeight, uint borderDepth);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_AddMetaData")]
        public static partial void PVRTexLib_AddMetaData(void* header, PVRTexLib_MetaDataBlock* dataBlock);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_RemoveMetaData")]
        public static partial void PVRTexLib_RemoveMetaData(void* header, uint devFOURCC, uint key);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_CreateTexture")]
        public static partial void* PVRTexLib_CreateTexture(void* header, void* data);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_CopyTexture")]
        public static partial void* PVRTexLib_CopyTexture(void* texture);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_MoveTexture")]
        public static partial void* PVRTexLib_MoveTexture(void* texture);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_DestroyTexture")]
        public static partial void PVRTexLib_DestroyTexture(void* texture);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_CreateTextureFromFile")]
        public static partial void* PVRTexLib_CreateTextureFromFile([MarshalAs(UnmanagedType.LPStr)] string filePath);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_CreateTextureFromData")]
        public static partial void* PVRTexLib_CreateTextureFromData(void* data);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureDataPtr")]
        public static partial void* PVRTexLib_GetTextureDataPtr(void* texture, uint MIPLevel, uint arrayMember, uint faceNumber, uint ZSlice);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureDataConstPtr")]
        public static partial void* PVRTexLib_GetTextureDataConstPtr(void* texture, uint MIPLevel, uint arrayMember, uint faceNumber, uint ZSlice);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureHeader")]
        public static partial void* PVRTexLib_GetTextureHeader(void* texture);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureHeaderW")]
        public static partial void* PVRTexLib_GetTextureHeaderW(void* texture);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_AddPaddingMetaData")]
        public static partial void PVRTexLib_AddPaddingMetaData(void* texture, uint padding);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_SaveTextureToFile")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static partial bool PVRTexLib_SaveTextureToFile(void* texture, [MarshalAs(UnmanagedType.LPStr)] string filePath);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_SaveTextureToMemory")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static partial bool PVRTexLib_SaveTextureToMemory(void* texture, PVRTexLibFileContainerType fileType, void* privateData, ulong* outSize, SaveTextureToMemoryRealloc pfnRealloc);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_SaveSurfaceToImageFile")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static partial bool PVRTexLib_SaveSurfaceToImageFile(void* texture, [MarshalAs(UnmanagedType.LPStr)] string filePath, uint MIPLevel, uint arrayMember, uint face, uint ZSlice);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_SaveTextureToLegacyPVRFile")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static partial bool PVRTexLib_SaveTextureToLegacyPVRFile(void* texture, [MarshalAs(UnmanagedType.LPStr)] string filePath, PVRTexLibLegacyApi api);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_IsTextureMultiPart")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static partial bool PVRTexLib_IsTextureMultiPart(void* texture);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureParts")]
        public static partial void PVRTexLib_GetTextureParts(void* inTexture, void** outTextures, uint* count);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_ResizeTexture")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static partial bool PVRTexLib_ResizeTexture(void* texture, uint newWidth, uint newHeight, uint newDepth, PVRTexLibResizeMode resizeMode);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_ResizeTextureCanvas")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static partial bool PVRTexLib_ResizeTextureCanvas(void* texture, uint newWidth, uint newHeight, uint newDepth, int xOffset, int yOffset, int zOffset);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_RotateTexture")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static partial bool PVRTexLib_RotateTexture(void* texture, PVRTexLibAxis rotationAxis, [MarshalAs(UnmanagedType.I1)] bool forward);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_FlipTexture")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static partial bool PVRTexLib_FlipTexture(void* texture, PVRTexLibAxis flipDirection);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_BorderTexture")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static partial bool PVRTexLib_BorderTexture(void* texture, uint borderX, uint borderY, uint borderZ);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_PreMultiplyAlpha")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static partial bool PVRTexLib_PreMultiplyAlpha(void* texture);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_Bleed")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static partial bool PVRTexLib_Bleed(void* texture);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetTextureChannels")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static partial bool PVRTexLib_SetTextureChannels(void* texture, uint numChannelSets, PVRTexLibChannelName* channels, uint* pValues);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetTextureChannelsFloat")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static partial bool PVRTexLib_SetTextureChannelsFloat(void* texture, uint numChannelSets, PVRTexLibChannelName* channels, float* pValues);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_CopyTextureChannels")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static partial bool PVRTexLib_CopyTextureChannels(void* textureDestination, void* textureSource, uint numChannelCopies, PVRTexLibChannelName* destinationChannels, PVRTexLibChannelName* sourceChannels);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_GenerateNormalMap")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static partial bool PVRTexLib_GenerateNormalMap(void* texture, float fScale, [MarshalAs(UnmanagedType.LPStr)] string channelOrder);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_GenerateMIPMaps")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static partial bool PVRTexLib_GenerateMIPMaps(void* texture, PVRTexLibResizeMode filterMode, int mipMapsToDo);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_ColourMIPMaps")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static partial bool PVRTexLib_ColourMIPMaps(void* texture);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_TranscodeTexture")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static partial bool PVRTexLib_TranscodeTexture(void* texture, PVRTexLib_TranscoderOptions* transcoderOptions);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_Decompress")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static partial bool PVRTexLib_Decompress(void* texture, uint maxThreads);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_EquiRectToCubeMap")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static partial bool PVRTexLib_EquiRectToCubeMap(void* texture, PVRTexLibResizeMode filter);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_GenerateDiffuseIrradianceCubeMap")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static partial bool PVRTexLib_GenerateDiffuseIrradianceCubeMap(void* texture, uint sampleCount, uint mapSize);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_GeneratePreFilteredSpecularCubeMap")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static partial bool PVRTexLib_GeneratePreFilteredSpecularCubeMap(void* texture, uint sampleCount, uint mapSize, uint numMipLevelsToDiscard, [MarshalAs(UnmanagedType.I1)] bool zeroRoughnessIspartialal);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_MaxDifference")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static partial bool PVRTexLib_MaxDifference(void* textureLHS, void* textureRHS, uint MIPLevel, uint arrayMember, uint face, uint zSlice, PVRTexLib_ErrorMetrics* metrics);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_MeanError")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static partial bool PVRTexLib_MeanError(void* textureLHS, void* textureRHS, uint MIPLevel, uint arrayMember, uint face, uint zSlice, PVRTexLib_ErrorMetrics* metrics);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_MeanSquaredError")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static partial bool PVRTexLib_MeanSquaredError(void* textureLHS, void* textureRHS, uint MIPLevel, uint arrayMember, uint face, uint zSlice, PVRTexLib_ErrorMetrics* metrics);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_RootMeanSquaredError")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static partial bool PVRTexLib_RootMeanSquaredError(void* textureLHS, void* textureRHS, uint MIPLevel, uint arrayMember, uint face, uint zSlice, PVRTexLib_ErrorMetrics* metrics);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_StandardDeviation")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static partial bool PVRTexLib_StandardDeviation(void* textureLHS, void* textureRHS, uint MIPLevel, uint arrayMember, uint face, uint zSlice, PVRTexLib_ErrorMetrics* metrics);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_PeakSignalToNoiseRatio")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static partial bool PVRTexLib_PeakSignalToNoiseRatio(void* textureLHS, void* textureRHS, uint MIPLevel, uint arrayMember, uint face, uint zSlice, PVRTexLib_ErrorMetrics* metrics);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_ColourDiff")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static partial bool PVRTexLib_ColourDiff(void* textureLHS, void* textureRHS, void** textureResult, float multiplier, PVRTexLibColourDiffMode mode);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_ToleranceDiff")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static partial bool PVRTexLib_ToleranceDiff(void* textureLHS, void* textureRHS, void** textureResult, float tolerance);

        [SuppressUnmanagedCodeSecurity, LibraryImport(PVRTexLibName, EntryPoint = "PVRTexLib_BlendDiff")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static partial bool PVRTexLib_BlendDiff(void* textureLHS, void* textureRHS, void** textureResult, float blendFactor);
#else
        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetDefaultTextureHeaderParams", CallingConvention = CallingConvention.Cdecl)]
        public static extern void PVRTexLib_SetDefaultTextureHeaderParams(PVRHeader_CreateParams* result);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_CreateTextureHeader", CallingConvention = CallingConvention.Cdecl)]
        public static extern void* PVRTexLib_CreateTextureHeader(PVRHeader_CreateParams* attribs);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_CreateTextureHeaderFromHeader", CallingConvention = CallingConvention.Cdecl)]
        public static extern void* PVRTexLib_CreateTextureHeaderFromHeader(PVRTextureHeaderV3* header, uint metaDataCount, PVRTexLib_MetaDataBlock* metaData);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_CopyTextureHeader", CallingConvention = CallingConvention.Cdecl)]
        public static extern void* PVRTexLib_CopyTextureHeader(void* header);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_DestroyTextureHeader", CallingConvention = CallingConvention.Cdecl)]
        public static extern void PVRTexLib_DestroyTextureHeader(void* header);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_TextureCreateRaw", CallingConvention = CallingConvention.Cdecl)]
        public static extern PVRTextureHeaderV3* PVRTexLib_TextureCreateRaw(uint width, uint height, uint depth, uint wMin, uint hMin, uint dMin, uint nBPP, [MarshalAs(UnmanagedType.I1)] bool bMIPMap, TextureCreateRawAllocCallback pfnAllocCallback);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_TextureLoadTiled", CallingConvention = CallingConvention.Cdecl)]
        public static extern void PVRTexLib_TextureLoadTiled(byte* pDst, uint widthDst, uint heightDst, byte* pSrc, uint widthSrc, uint heightSrc, uint elementSize, [MarshalAs(UnmanagedType.I1)] bool twiddled);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureBitsPerPixel", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint PVRTexLib_GetTextureBitsPerPixel(void* header);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetFormatBitsPerPixel", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint PVRTexLib_GetFormatBitsPerPixel(ulong u64PixelFormat);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureChannelCount", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint PVRTexLib_GetTextureChannelCount(void* header);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureChannelType", CallingConvention = CallingConvention.Cdecl)]
        public static extern PVRTexLibVariableType PVRTexLib_GetTextureChannelType(void* header);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureColourSpace", CallingConvention = CallingConvention.Cdecl)]
        public static extern PVRTexLibColourSpace PVRTexLib_GetTextureColourSpace(void* header);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureWidth", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint PVRTexLib_GetTextureWidth(void* header, uint mipLevel);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureHeight", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint PVRTexLib_GetTextureHeight(void* header, uint mipLevel);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureDepth", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint PVRTexLib_GetTextureDepth(void* header, uint mipLevel);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureSize", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint PVRTexLib_GetTextureSize(void* header, int mipLevel, [MarshalAs(UnmanagedType.I1)] bool allSurfaces, [MarshalAs(UnmanagedType.I1)] bool allFaces);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureDataSize", CallingConvention = CallingConvention.Cdecl)]
        public static extern ulong PVRTexLib_GetTextureDataSize(void* header, int mipLevel, [MarshalAs(UnmanagedType.I1)] bool allSurfaces, [MarshalAs(UnmanagedType.I1)] bool allFaces);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureOrientation", CallingConvention = CallingConvention.Cdecl)]
        public static extern void PVRTexLib_GetTextureOrientation(void* header, PVRTexLib_Orientation* result);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureOpenGLFormat", CallingConvention = CallingConvention.Cdecl)]
        public static extern void PVRTexLib_GetTextureOpenGLFormat(void* header, PVRTexLib_OpenGLFormat* result);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureOpenGLESFormat", CallingConvention = CallingConvention.Cdecl)]
        public static extern void PVRTexLib_GetTextureOpenGLESFormat(void* header, PVRTexLib_OpenGLFormat* result);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureVulkanFormat", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint PVRTexLib_GetTextureVulkanFormat(void* header);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureD3DFormat", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint PVRTexLib_GetTextureD3DFormat(void* header);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureDXGIFormat", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint PVRTexLib_GetTextureDXGIFormat(void* header);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureFormatMinDims", CallingConvention = CallingConvention.Cdecl)]
        public static extern void PVRTexLib_GetTextureFormatMinDims(void* header, uint* minX, uint* minY, uint* minZ);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetPixelFormatMinDims", CallingConvention = CallingConvention.Cdecl)]
        public static extern void PVRTexLib_GetPixelFormatMinDims(ulong ui64Format, uint* minX, uint* minY, uint* minZ);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureMetaDataSize", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint PVRTexLib_GetTextureMetaDataSize(void* header);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureIsPreMultiplied", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool PVRTexLib_GetTextureIsPreMultiplied(void* header);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureIsFileCompressed", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool PVRTexLib_GetTextureIsFileCompressed(void* header);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureIsBumpMap", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool PVRTexLib_GetTextureIsBumpMap(void* header);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureBumpMapScale", CallingConvention = CallingConvention.Cdecl)]
        public static extern float PVRTexLib_GetTextureBumpMapScale(void* header);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetNumTextureAtlasMembers", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint PVRTexLib_GetNumTextureAtlasMembers(void* header);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureAtlasData", CallingConvention = CallingConvention.Cdecl)]
        public static extern float* PVRTexLib_GetTextureAtlasData(void* header, uint* count);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureNumMipMapLevels", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint PVRTexLib_GetTextureNumMipMapLevels(void* header);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureNumFaces", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint PVRTexLib_GetTextureNumFaces(void* header);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureNumArrayMembers", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint PVRTexLib_GetTextureNumArrayMembers(void* header);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureCubeMapOrder", CallingConvention = CallingConvention.Cdecl)]
        private static extern void PVRTexLib_GetTextureCubeMapOrder_Internal(void* header, sbyte* cubeOrder);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureBumpMapOrder", CallingConvention = CallingConvention.Cdecl)]
        private static extern void PVRTexLib_GetTextureBumpMapOrder_Internal(void* header, sbyte* bumpOrder);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTexturePixelFormat", CallingConvention = CallingConvention.Cdecl)]
        public static extern ulong PVRTexLib_GetTexturePixelFormat(void* header);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_TextureHasPackedChannelData", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool PVRTexLib_TextureHasPackedChannelData(void* header);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetTextureChannelType", CallingConvention = CallingConvention.Cdecl)]
        public static extern void PVRTexLib_SetTextureChannelType(void* header, PVRTexLibVariableType type);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetTextureColourSpace", CallingConvention = CallingConvention.Cdecl)]
        public static extern void PVRTexLib_SetTextureColourSpace(void* header, PVRTexLibColourSpace colourSpace);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetTextureD3DFormat", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool PVRTexLib_SetTextureD3DFormat(void* header, uint d3dFormat);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetTextureDXGIFormat", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool PVRTexLib_SetTextureDXGIFormat(void* header, uint dxgiFormat);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetTextureOGLFormat", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool PVRTexLib_SetTextureOGLFormat(void* header, PVRTexLib_OpenGLFormat* oglFormat);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetTextureOGLESFormat", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool PVRTexLib_SetTextureOGLESFormat(void* header, PVRTexLib_OpenGLFormat* oglesFormat);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetTextureVulkanFormat", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool PVRTexLib_SetTextureVulkanFormat(void* header, uint vulkanFormat);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetTexturePixelFormat", CallingConvention = CallingConvention.Cdecl)]
        public static extern void PVRTexLib_SetTexturePixelFormat(void* header, ulong format);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetTextureWidth", CallingConvention = CallingConvention.Cdecl)]
        public static extern void PVRTexLib_SetTextureWidth(void* header, uint width);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetTextureHeight", CallingConvention = CallingConvention.Cdecl)]
        public static extern void PVRTexLib_SetTextureHeight(void* header, uint height);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetTextureDepth", CallingConvention = CallingConvention.Cdecl)]
        public static extern void PVRTexLib_SetTextureDepth(void* header, uint depth);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetTextureNumArrayMembers", CallingConvention = CallingConvention.Cdecl)]
        public static extern void PVRTexLib_SetTextureNumArrayMembers(void* header, uint numMembers);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetTextureNumMIPLevels", CallingConvention = CallingConvention.Cdecl)]
        public static extern void PVRTexLib_SetTextureNumMIPLevels(void* header, uint numMIPLevels);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetTextureNumFaces", CallingConvention = CallingConvention.Cdecl)]
        public static extern void PVRTexLib_SetTextureNumFaces(void* header, uint numFaces);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetTextureOrientation", CallingConvention = CallingConvention.Cdecl)]
        public static extern void PVRTexLib_SetTextureOrientation(void* header, PVRTexLib_Orientation* orientation);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetTextureIsFileCompressed", CallingConvention = CallingConvention.Cdecl)]
        public static extern void PVRTexLib_SetTextureIsFileCompressed(void* header, [MarshalAs(UnmanagedType.I1)] bool isFileCompressed);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetTextureIsPreMultiplied", CallingConvention = CallingConvention.Cdecl)]
        public static extern void PVRTexLib_SetTextureIsPreMultiplied(void* header, [MarshalAs(UnmanagedType.I1)] bool isPreMultiplied);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureBorder", CallingConvention = CallingConvention.Cdecl)]
        public static extern void PVRTexLib_GetTextureBorder(void* header, uint* borderWidth, uint* borderHeight, uint* borderDepth);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetMetaDataBlock", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool PVRTexLib_GetMetaDataBlock(void* header, uint devFOURCC, uint key, PVRTexLib_MetaDataBlock* dataBlock, GetMetaDataBlockAllocCallback pfnAllocCallback);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_TextureHasMetaData", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool PVRTexLib_TextureHasMetaData(void* header, uint devFOURCC, uint key);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetTextureBumpMap", CallingConvention = CallingConvention.Cdecl)]
        public static extern void PVRTexLib_SetTextureBumpMap(void* header, float bumpScale, [MarshalAs(UnmanagedType.LPStr)] string bumpOrder);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetTextureAtlas", CallingConvention = CallingConvention.Cdecl)]
        public static extern void PVRTexLib_SetTextureAtlas(void* header, float* atlasData, uint dataSize);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetTextureCubeMapOrder", CallingConvention = CallingConvention.Cdecl)]
        public static extern void PVRTexLib_SetTextureCubeMapOrder(void* header, [MarshalAs(UnmanagedType.LPStr)] string cubeMapOrder);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetTextureBorder", CallingConvention = CallingConvention.Cdecl)]
        public static extern void PVRTexLib_SetTextureBorder(void* header, uint borderWidth, uint borderHeight, uint borderDepth);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_AddMetaData", CallingConvention = CallingConvention.Cdecl)]
        public static extern void PVRTexLib_AddMetaData(void* header, PVRTexLib_MetaDataBlock* dataBlock);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_RemoveMetaData", CallingConvention = CallingConvention.Cdecl)]
        public static extern void PVRTexLib_RemoveMetaData(void* header, uint devFOURCC, uint key);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_CreateTexture", CallingConvention = CallingConvention.Cdecl)]
        public static extern void* PVRTexLib_CreateTexture(void* header, void* data);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_CopyTexture", CallingConvention = CallingConvention.Cdecl)]
        public static extern void* PVRTexLib_CopyTexture(void* texture);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_MoveTexture", CallingConvention = CallingConvention.Cdecl)]
        public static extern void* PVRTexLib_MoveTexture(void* texture);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_DestroyTexture", CallingConvention = CallingConvention.Cdecl)]
        public static extern void PVRTexLib_DestroyTexture(void* texture);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_CreateTextureFromFile", CallingConvention = CallingConvention.Cdecl)]
        public static extern void* PVRTexLib_CreateTextureFromFile([MarshalAs(UnmanagedType.LPStr)] string filePath);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_CreateTextureFromData", CallingConvention = CallingConvention.Cdecl)]
        public static extern void* PVRTexLib_CreateTextureFromData(void* data);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureDataPtr", CallingConvention = CallingConvention.Cdecl)]
        public static extern void* PVRTexLib_GetTextureDataPtr(void* texture, uint MIPLevel, uint arrayMember, uint faceNumber, uint ZSlice);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureDataConstPtr", CallingConvention = CallingConvention.Cdecl)]
        public static extern void* PVRTexLib_GetTextureDataConstPtr(void* texture, uint MIPLevel, uint arrayMember, uint faceNumber, uint ZSlice);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureHeader", CallingConvention = CallingConvention.Cdecl)]
        public static extern void* PVRTexLib_GetTextureHeader(void* texture);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureHeaderW", CallingConvention = CallingConvention.Cdecl)]
        public static extern void* PVRTexLib_GetTextureHeaderW(void* texture);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_AddPaddingMetaData", CallingConvention = CallingConvention.Cdecl)]
        public static extern void PVRTexLib_AddPaddingMetaData(void* texture, uint padding);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_SaveTextureToFile", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool PVRTexLib_SaveTextureToFile(void* texture, [MarshalAs(UnmanagedType.LPStr)] string filePath);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_SaveTextureToMemory", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool PVRTexLib_SaveTextureToMemory(void* texture, PVRTexLibFileContainerType fileType, void* privateData, ulong* outSize, SaveTextureToMemoryRealloc pfnRealloc);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_SaveSurfaceToImageFile", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool PVRTexLib_SaveSurfaceToImageFile(void* texture, [MarshalAs(UnmanagedType.LPStr)] string filePath, uint MIPLevel, uint arrayMember, uint face, uint ZSlice);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_SaveTextureToLegacyPVRFile", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool PVRTexLib_SaveTextureToLegacyPVRFile(void* texture, [MarshalAs(UnmanagedType.LPStr)] string filePath, PVRTexLibLegacyApi api);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_IsTextureMultiPart", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool PVRTexLib_IsTextureMultiPart(void* texture);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureParts", CallingConvention = CallingConvention.Cdecl)]
        public static extern void PVRTexLib_GetTextureParts(void* inTexture, void** outTextures, uint* count);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_ResizeTexture", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool PVRTexLib_ResizeTexture(void* texture, uint newWidth, uint newHeight, uint newDepth, PVRTexLibResizeMode resizeMode);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_ResizeTextureCanvas", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool PVRTexLib_ResizeTextureCanvas(void* texture, uint newWidth, uint newHeight, uint newDepth, int xOffset, int yOffset, int zOffset);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_RotateTexture", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool PVRTexLib_RotateTexture(void* texture, PVRTexLibAxis rotationAxis, [MarshalAs(UnmanagedType.I1)] bool forward);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_FlipTexture", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool PVRTexLib_FlipTexture(void* texture, PVRTexLibAxis flipDirection);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_BorderTexture", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool PVRTexLib_BorderTexture(void* texture, uint borderX, uint borderY, uint borderZ);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_PreMultiplyAlpha", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool PVRTexLib_PreMultiplyAlpha(void* texture);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_Bleed", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool PVRTexLib_Bleed(void* texture);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetTextureChannels", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool PVRTexLib_SetTextureChannels(void* texture, uint numChannelSets, PVRTexLibChannelName* channels, uint* pValues);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetTextureChannelsFloat", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool PVRTexLib_SetTextureChannelsFloat(void* texture, uint numChannelSets, PVRTexLibChannelName* channels, float* pValues);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_CopyTextureChannels", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool PVRTexLib_CopyTextureChannels(void* textureDestination, void* textureSource, uint numChannelCopies, PVRTexLibChannelName* destinationChannels, PVRTexLibChannelName* sourceChannels);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_GenerateNormalMap", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool PVRTexLib_GenerateNormalMap(void* texture, float fScale, [MarshalAs(UnmanagedType.LPStr)] string channelOrder);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_GenerateMIPMaps", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool PVRTexLib_GenerateMIPMaps(void* texture, PVRTexLibResizeMode filterMode, int mipMapsToDo);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_ColourMIPMaps", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool PVRTexLib_ColourMIPMaps(void* texture);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_TranscodeTexture", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool PVRTexLib_TranscodeTexture(void* texture, PVRTexLib_TranscoderOptions* transcoderOptions);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_Decompress", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool PVRTexLib_Decompress(void* texture, uint maxThreads);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_EquiRectToCubeMap", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool PVRTexLib_EquiRectToCubeMap(void* texture, PVRTexLibResizeMode filter);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_GenerateDiffuseIrradianceCubeMap", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool PVRTexLib_GenerateDiffuseIrradianceCubeMap(void* texture, uint sampleCount, uint mapSize);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_GeneratePreFilteredSpecularCubeMap", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool PVRTexLib_GeneratePreFilteredSpecularCubeMap(void* texture, uint sampleCount, uint mapSize, uint numMipLevelsToDiscard, [MarshalAs(UnmanagedType.I1)] bool zeroRoughnessIsExternal);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_MaxDifference", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool PVRTexLib_MaxDifference(void* textureLHS, void* textureRHS, uint MIPLevel, uint arrayMember, uint face, uint zSlice, PVRTexLib_ErrorMetrics* metrics);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_MeanError", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool PVRTexLib_MeanError(void* textureLHS, void* textureRHS, uint MIPLevel, uint arrayMember, uint face, uint zSlice, PVRTexLib_ErrorMetrics* metrics);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_MeanSquaredError", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool PVRTexLib_MeanSquaredError(void* textureLHS, void* textureRHS, uint MIPLevel, uint arrayMember, uint face, uint zSlice, PVRTexLib_ErrorMetrics* metrics);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_RootMeanSquaredError", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool PVRTexLib_RootMeanSquaredError(void* textureLHS, void* textureRHS, uint MIPLevel, uint arrayMember, uint face, uint zSlice, PVRTexLib_ErrorMetrics* metrics);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_StandardDeviation", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool PVRTexLib_StandardDeviation(void* textureLHS, void* textureRHS, uint MIPLevel, uint arrayMember, uint face, uint zSlice, PVRTexLib_ErrorMetrics* metrics);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_PeakSignalToNoiseRatio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool PVRTexLib_PeakSignalToNoiseRatio(void* textureLHS, void* textureRHS, uint MIPLevel, uint arrayMember, uint face, uint zSlice, PVRTexLib_ErrorMetrics* metrics);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_ColourDiff", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool PVRTexLib_ColourDiff(void* textureLHS, void* textureRHS, void** textureResult, float multiplier, PVRTexLibColourDiffMode mode);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_ToleranceDiff", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool PVRTexLib_ToleranceDiff(void* textureLHS, void* textureRHS, void** textureResult, float tolerance);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_BlendDiff", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool PVRTexLib_BlendDiff(void* textureLHS, void* textureRHS, void** textureResult, float blendFactor);
#endif
    }
}
