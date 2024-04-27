using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace PVRTexLib
{
    /// <summary>***********************************************************************</summary>
    /// <remarks>
    /// <para>Structure containing various texture header parameters for</para>
    /// <para>PVRTexLib_CreateTextureHeader().</para>
    /// <para>***********************************************************************</para>
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct PVRHeader_CreateParams
    {
        public ulong pixelFormat;
        public uint width;
        public uint height;
        public uint depth;
        public uint numMipMaps;
        public uint numArrayMembers;
        public uint numFaces;
        public PVRTexLibColourSpace colourSpace;
        public PVRTexLibVariableType channelType;
        public bool preMultiplied;
    }

    /// <summary>***********************************************************************</summary>
    /// <remarks>
    /// <para>Structure containing a textures orientation in each axis.</para>
    /// <para>***********************************************************************</para>
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct PVRTexLib_Orientation
    {
        public PVRTexLibOrientation x;
        public PVRTexLibOrientation y;
        public PVRTexLibOrientation z;
    }

    /// <summary>***********************************************************************</summary>
    /// <remarks>
    /// <para>Structure containing a OpenGL[ES] format.</para>
    /// <para>***********************************************************************</para>
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct PVRTexLib_OpenGLFormat
    {
        public uint internalFormat;
        public uint format;
        public uint type;
    }

    /// <summary>***********************************************************************</summary>
    /// <remarks>
    /// <para>Structure containing a block of meta data for a texture.</para>
    /// <para>***********************************************************************</para>
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PVRTexLib_MetaDataBlock
    {
        public uint DevFOURCC;
        public uint u32Key;
        public uint u32DataSize;
        public byte* Data;

        public unsafe void Reset()
        {
            DevFOURCC = PVRDefine.PVRTEX_CURR_IDENT;
            u32Key = 0u;
            u32DataSize = 0u;
            Data = null;
        }
    }

#if NET8_0_OR_GREATER
    [System.Runtime.CompilerServices.InlineArray(4)]
    public struct Buffer4<T>
    {
        private T _element0;
    }
#endif

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct PVRTexLib_TranscoderOptions
    {
        public uint sizeofStruct;
        public ulong pixelFormat;
#if NET8_0_OR_GREATER
        public Buffer4<PVRTexLibVariableType> channelType;
#else
        public PVRTexLibVariableType channelType0;
        public PVRTexLibVariableType channelType1;
        public PVRTexLibVariableType channelType2;
        public PVRTexLibVariableType channelType3;
#endif
        public PVRTexLibColourSpace colourspace;
        public PVRTexLibCompressorQuality quality;
        public bool doDither;
        public float maxRange;
        public uint maxThreads;
    }

    /// <summary>***********************************************************************</summary>
    /// <remarks>
    /// <para>Structure containing the resulting error metrics computed by:</para>
    /// <para>PVRTexLib_MaxDifference(),</para>
    /// <para>PVRTexLib_MeanError(),</para>
    /// <para>PVRTexLib_MeanSquaredError(),</para>
    /// <para>PVRTexLib_RootMeanSquaredError(),</para>
    /// <para>PVRTexLib_StandardDeviation(),</para>
    /// <para>PVRTexLib_PeakSignalToNoiseRatio().</para>
    /// <para>***********************************************************************</para>
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct PVRTexLib_ErrorMetrics
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct PVRTexLib_ErrorMetrics_Channel
        {
            public PVRTexLibChannelName name;
            public double value;
        }

#if NET8_0_OR_GREATER
        public Buffer4<PVRTexLib_ErrorMetrics_Channel> channel;
#else
        public PVRTexLib_ErrorMetrics_Channel channel0;
        public PVRTexLib_ErrorMetrics_Channel channel1;
        public PVRTexLib_ErrorMetrics_Channel channel2;
        public PVRTexLib_ErrorMetrics_Channel channel3;
#endif
        public double allChannels;
        public double rgbChannels;
    }

    public static unsafe class NativeMethod
    {
        private const string PVRTexLibName = "PVRTexLib";

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
        public static extern PVRTextureHeaderV3* PVRTexLib_TextureCreateRaw(uint width, uint height, uint depth, uint wMin, uint hMin, uint dMin, uint nBPP, bool bMIPMap, Func<ulong, IntPtr> pfnAllocCallback);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_TextureLoadTiled", CallingConvention = CallingConvention.Cdecl)]
        public static extern void PVRTexLib_TextureLoadTiled(byte* pDst, uint widthDst, uint heightDst, byte* pSrc, uint widthSrc, uint heightSrc, uint elementSize, bool twiddled);

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
        public static extern uint PVRTexLib_GetTextureSize(void* header, int mipLevel, bool allSurfaces, bool allFaces);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureDataSize", CallingConvention = CallingConvention.Cdecl)]
        public static extern ulong PVRTexLib_GetTextureDataSize(void* header, int mipLevel, bool allSurfaces, bool allFaces);

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
        public static extern void PVRTexLib_GetTextureCubeMapOrder(void* header, [MarshalAs(UnmanagedType.LPStr)] StringBuilder cubeOrder);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureBumpMapOrder", CallingConvention = CallingConvention.Cdecl)]
        public static extern void PVRTexLib_GetTextureBumpMapOrder(void* header, [MarshalAs(UnmanagedType.LPStr)] StringBuilder bumpOrder);

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
        public static extern void PVRTexLib_SetTextureIsFileCompressed(void* header, bool isFileCompressed);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_SetTextureIsPreMultiplied", CallingConvention = CallingConvention.Cdecl)]
        public static extern void PVRTexLib_SetTextureIsPreMultiplied(void* header, bool isPreMultiplied);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetTextureBorder", CallingConvention = CallingConvention.Cdecl)]
        public static extern void PVRTexLib_GetTextureBorder(void* header, uint* borderWidth, uint* borderHeight, uint* borderDepth);

        [SuppressUnmanagedCodeSecurity, DllImport(PVRTexLibName, EntryPoint = "PVRTexLib_GetMetaDataBlock", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool PVRTexLib_GetMetaDataBlock(void* header, uint devFOURCC, uint key, PVRTexLib_MetaDataBlock* dataBlock, Func<uint, IntPtr> pfnAllocCallback);

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
        public static extern bool PVRTexLib_SaveTextureToMemory(void* texture, PVRTexLibFileContainerType fileType, void* privateData, ulong* outSize, Func<IntPtr, ulong, IntPtr> pfnRealloc);

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
        public static extern bool PVRTexLib_RotateTexture(void* texture, PVRTexLibAxis rotationAxis, bool forward);

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
        public static extern bool PVRTexLib_GeneratePreFilteredSpecularCubeMap(void* texture, uint sampleCount, uint mapSize, uint numMipLevelsToDiscard, bool zeroRoughnessIsExternal);

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
    }
}
