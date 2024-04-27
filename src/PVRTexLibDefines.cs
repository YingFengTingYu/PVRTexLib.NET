/*!****************************************************************************

 @file         PVRTexLibDefines.h
 @copyright    Copyright (c) Imagination Technologies Limited.
 @brief        Public PVRTexLib defines header.

******************************************************************************/
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace PVRTexLib
{
    public static partial class PVRDefine
    {
        /*****************************************************************************
        * Texture related constants and enumerations.
        *****************************************************************************/
        /// <summary>
        /// V3 Header Identifiers: 'P''V''R'3
        /// </summary>
        public const uint PVRTEX3_IDENT = 0x03525650U;

        /// <summary>
        /// V3 Header Identifiers: 'P''V''R'3 (If endianness is backwards then PVR3 will read as 3RVP, hence why it is written as an int)
        /// </summary>
        public const uint PVRTEX3_IDENT_REV = 0x50565203U;

        /// <summary>
        /// Current version texture identifiers
        /// </summary>
        public const uint PVRTEX_CURR_IDENT = PVRTEX3_IDENT;

        /// <summary>
        /// Current version texture identifiers
        /// </summary>
        public const uint PVRTEX_CURR_IDENT_REV = PVRTEX3_IDENT_REV;

        // PVR Header file flags. Condition if true. If false, opposite is true unless specified.
        /// <summary>
        /// Texture has been file compressed using PVRTexLib (currently unused)
        /// </summary>
        public const uint PVRTEX3_FILE_COMPRESSED = 1U << 0;

        /// <summary>
        /// Texture has been premultiplied by alpha value.
        /// </summary>
        public const uint PVRTEX3_PREMULTIPLIED = 1U << 1;

        /// <summary>
        /// Mip Map level specifier constants. Other levels are specified by 1,2...n
        /// </summary>
        public const int PVRTEX_TOPMIPLEVEL = 0;

        /// <summary>
        /// Mip Map level specifier constants. Other levels are specified by 1,2...n. This is a special number used simply to return a total of all MIP levels when dealing with data sizes.
        /// </summary>
        public const int PVRTEX_ALLMIPLEVELS = -1;

        /// <summary>
        /// A 64 bit pixel format ID & this will give you the high bits of a pixel format to check for a compressed format.
        /// </summary>
        public const ulong PVRTEX_PFHIGHMASK = 0xffffffff00000000ul;

        /*
            Preprocessor definitions to generate a pixelID for use when consts are needed. For example - switch statements.
            These should be evaluated by the compiler rather than at run time - assuming that arguments are all constant.
        */

        /// <summary>
        /// Generate a 4 channel PixelID.
        /// </summary>
        /// <param name="C1Name"></param>
        /// <param name="C2Name"></param>
        /// <param name="C3Name"></param>
        /// <param name="C4Name"></param>
        /// <param name="C1Bits"></param>
        /// <param name="C2Bits"></param>
        /// <param name="C3Bits"></param>
        /// <param name="C4Bits"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong PVRTGENPIXELID4(ulong C1Name, ulong C2Name, ulong C3Name, ulong C4Name, ulong C1Bits, ulong C2Bits, ulong C3Bits, ulong C4Bits)
        {
            return C1Name + (C2Name << 8) + (C3Name << 16) + (C4Name << 24) + (C1Bits << 32) + (C2Bits << 40) + (C3Bits << 48) + (C4Bits << 56);
        }

        /// <summary>
        /// Generate a 1 channel PixelID.
        /// </summary>
        /// <param name="C1Name"></param>
        /// <param name="C2Name"></param>
        /// <param name="C3Name"></param>
        /// <param name="C1Bits"></param>
        /// <param name="C2Bits"></param>
        /// <param name="C3Bits"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong PVRTGENPIXELID3(ulong C1Name, ulong C2Name, ulong C3Name, ulong C1Bits, ulong C2Bits, ulong C3Bits)
        {
            return PVRTGENPIXELID4(C1Name, C2Name, C3Name, 0, C1Bits, C2Bits, C3Bits, 0);
        }

        /// <summary>
        /// Generate a 2 channel PixelID.
        /// </summary>
        /// <param name="C1Name"></param>
        /// <param name="C2Name"></param>
        /// <param name="C1Bits"></param>
        /// <param name="C2Bits"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong PVRTGENPIXELID2(ulong C1Name, ulong C2Name, ulong C1Bits, ulong C2Bits)
        {
            return PVRTGENPIXELID4(C1Name, C2Name, 0, 0, C1Bits, C2Bits, 0, 0);
        }

        /// <summary>
        /// Generate a 3 channel PixelID.
        /// </summary>
        /// <param name="C1Name"></param>
        /// <param name="C1Bits"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong PVRTGENPIXELID1(ulong C1Name, ulong C1Bits)
        {
            return PVRTGENPIXELID4(C1Name, 0, 0, 0, C1Bits, 0, 0, 0);
        }

        /// <summary>
        /// 2D texture offset
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static int TEXOFFSET2D(int x, int y, int width)
        {
            return x + y * width;
        }

        /// <summary>
        /// 3D texture offset
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static int TEXOFFSET3D(int x, int y, int z, int width, int height)
        {
            return x + y * width + z * width * height;
        }

        public const uint PVRTEX3_HEADERSIZE = 52U;
    }

    /// <summary>
    /// Values for each meta data type that PVRTexLib knows about. 
    /// Texture arrays hinge on each surface being identical in all
    /// but content, including meta data.If the meta data varies even
    /// slightly then a new texture should be used.
    /// It is possible to write your own extension to get around this however.
    /// </summary>
    public enum PVRTexLibMetaData
    {
        PVRTLMD_TextureAtlasCoords = 0,
        PVRTLMD_BumpData,
        PVRTLMD_CubeMapOrder,
        PVRTLMD_TextureOrientation,
        PVRTLMD_BorderData,
        PVRTLMD_Padding,
        PVRTLMD_PerChannelType,
        PVRTLMD_SupercompressionGlobalData,
        PVRTLMD_MaxRange,
        PVRTLMD_NumMetaDataTypes
    }

    /// <summary>
    /// Axis
    /// </summary>
    public enum PVRTexLibAxis
    {
        PVRTLA_X = 0,
        PVRTLA_Y = 1,
        PVRTLA_Z = 2
    }

    /// <summary>
    /// Image orientations per axis
    /// </summary>
    public enum PVRTexLibOrientation
    {
        PVRTLO_Left = 1 << PVRTexLibAxis.PVRTLA_X,
        PVRTLO_Right = 0,
        PVRTLO_Up = 1 << PVRTexLibAxis.PVRTLA_Y,
        PVRTLO_Down = 0,
        PVRTLO_Out = 1 << PVRTexLibAxis.PVRTLA_Z,
        PVRTLO_In = 0
    }

    /// <summary>
    /// Describes the colour space of the texture
    /// </summary>
    public enum PVRTexLibColourSpace
    {
        PVRTLCS_Linear,
        PVRTLCS_sRGB,
        PVRTLCS_BT601,
        PVRTLCS_BT709,
        PVRTLCS_BT2020,
        PVRTLCS_NumSpaces
    }

    /// <summary>
    /// Channel names for non-compressed formats
    /// </summary>
    public enum PVRTexLibChannelName
    {
        PVRTLCN_NoChannel,
        PVRTLCN_Red,
        PVRTLCN_Green,
        PVRTLCN_Blue,
        PVRTLCN_Alpha,
        PVRTLCN_Luminance,
        PVRTLCN_Intensity,
        PVRTLCN_Depth,
        PVRTLCN_Stencil,
        PVRTLCN_Unspecified,
        PVRTLCN_NumChannels
    }

    /// <summary>
    /// Compressed pixel formats that PVRTexLib understands
    /// </summary>
    public enum PVRTexLibPixelFormat
    {
        PVRTLPF_PVRTCI_2bpp_RGB,
        PVRTLPF_PVRTCI_2bpp_RGBA,
        PVRTLPF_PVRTCI_4bpp_RGB,
        PVRTLPF_PVRTCI_4bpp_RGBA,
        PVRTLPF_PVRTCII_2bpp,
        PVRTLPF_PVRTCII_4bpp,
        PVRTLPF_ETC1,
        PVRTLPF_DXT1,
        PVRTLPF_DXT2,
        PVRTLPF_DXT3,
        PVRTLPF_DXT4,
        PVRTLPF_DXT5,

        //These formats are identical to some DXT formats.
        PVRTLPF_BC1 = PVRTLPF_DXT1,
        PVRTLPF_BC2 = PVRTLPF_DXT3,
        PVRTLPF_BC3 = PVRTLPF_DXT5,
        PVRTLPF_BC4,
        PVRTLPF_BC5,

        /* Currently unsupported: */
        PVRTLPF_BC6,
        PVRTLPF_BC7,
        /* ~~~~~~~~~~~~~~~~~~ */

        // Packed YUV formats
        PVRTLPF_UYVY_422, // https://www.fourcc.org/pixel-format/yuv-uyvy/
        PVRTLPF_YUY2_422, // https://www.fourcc.org/pixel-format/yuv-yuy2/

        PVRTLPF_BW1bpp,
        PVRTLPF_SharedExponentR9G9B9E5,
        PVRTLPF_RGBG8888,
        PVRTLPF_GRGB8888,
        PVRTLPF_ETC2_RGB,
        PVRTLPF_ETC2_RGBA,
        PVRTLPF_ETC2_RGB_A1,
        PVRTLPF_EAC_R11,
        PVRTLPF_EAC_RG11,

        PVRTLPF_ASTC_4x4,
        PVRTLPF_ASTC_5x4,
        PVRTLPF_ASTC_5x5,
        PVRTLPF_ASTC_6x5,
        PVRTLPF_ASTC_6x6,
        PVRTLPF_ASTC_8x5,
        PVRTLPF_ASTC_8x6,
        PVRTLPF_ASTC_8x8,
        PVRTLPF_ASTC_10x5,
        PVRTLPF_ASTC_10x6,
        PVRTLPF_ASTC_10x8,
        PVRTLPF_ASTC_10x10,
        PVRTLPF_ASTC_12x10,
        PVRTLPF_ASTC_12x12,

        PVRTLPF_ASTC_3x3x3,
        PVRTLPF_ASTC_4x3x3,
        PVRTLPF_ASTC_4x4x3,
        PVRTLPF_ASTC_4x4x4,
        PVRTLPF_ASTC_5x4x4,
        PVRTLPF_ASTC_5x5x4,
        PVRTLPF_ASTC_5x5x5,
        PVRTLPF_ASTC_6x5x5,
        PVRTLPF_ASTC_6x6x5,
        PVRTLPF_ASTC_6x6x6,

        PVRTLPF_BASISU_ETC1S,
        PVRTLPF_BASISU_UASTC,

        PVRTLPF_RGBM,
        PVRTLPF_RGBD,

        PVRTLPF_PVRTCI_HDR_6bpp,
        PVRTLPF_PVRTCI_HDR_8bpp,
        PVRTLPF_PVRTCII_HDR_6bpp,
        PVRTLPF_PVRTCII_HDR_8bpp,

        // The memory layout for 10 and 12 bit YUV formats that are packed into a WORD (16 bits) is denoted by MSB or LSB:
        // MSB denotes that the sample is stored in the most significant <N> bits
        // LSB denotes that the sample is stored in the least significant <N> bits
        // All YUV formats are little endian

        // Packed YUV formats
        PVRTLPF_VYUA10MSB_444,
        PVRTLPF_VYUA10LSB_444,
        PVRTLPF_VYUA12MSB_444,
        PVRTLPF_VYUA12LSB_444,
        PVRTLPF_UYV10A2_444,    // Y410
        PVRTLPF_UYVA16_444,     // Y416
        PVRTLPF_YUYV16_422,     // Y216
        PVRTLPF_UYVY16_422,
        PVRTLPF_YUYV10MSB_422,  // Y210
        PVRTLPF_YUYV10LSB_422,
        PVRTLPF_UYVY10MSB_422,
        PVRTLPF_UYVY10LSB_422,
        PVRTLPF_YUYV12MSB_422,
        PVRTLPF_YUYV12LSB_422,
        PVRTLPF_UYVY12MSB_422,
        PVRTLPF_UYVY12LSB_422,

        /*
            Reserved for future expansion
        */

        // 3 Plane (Planar) YUV formats
        PVRTLPF_YUV_3P_444 = 270,
        PVRTLPF_YUV10MSB_3P_444,
        PVRTLPF_YUV10LSB_3P_444,
        PVRTLPF_YUV12MSB_3P_444,
        PVRTLPF_YUV12LSB_3P_444,
        PVRTLPF_YUV16_3P_444,
        PVRTLPF_YUV_3P_422,
        PVRTLPF_YUV10MSB_3P_422,
        PVRTLPF_YUV10LSB_3P_422,
        PVRTLPF_YUV12MSB_3P_422,
        PVRTLPF_YUV12LSB_3P_422,
        PVRTLPF_YUV16_3P_422,
        PVRTLPF_YUV_3P_420,
        PVRTLPF_YUV10MSB_3P_420,
        PVRTLPF_YUV10LSB_3P_420,
        PVRTLPF_YUV12MSB_3P_420,
        PVRTLPF_YUV12LSB_3P_420,
        PVRTLPF_YUV16_3P_420,
        PVRTLPF_YVU_3P_420,

        /*
            Reserved for future expansion
        */

        // 2 Plane (Biplanar/semi-planar) YUV formats
        PVRTLPF_YUV_2P_422 = 480,   // P208
        PVRTLPF_YUV10MSB_2P_422,    // P210
        PVRTLPF_YUV10LSB_2P_422,
        PVRTLPF_YUV12MSB_2P_422,
        PVRTLPF_YUV12LSB_2P_422,
        PVRTLPF_YUV16_2P_422,       // P216
        PVRTLPF_YUV_2P_420,         // NV12
        PVRTLPF_YUV10MSB_2P_420,    // P010
        PVRTLPF_YUV10LSB_2P_420,
        PVRTLPF_YUV12MSB_2P_420,
        PVRTLPF_YUV12LSB_2P_420,
        PVRTLPF_YUV16_2P_420,       // P016
        PVRTLPF_YUV_2P_444,
        PVRTLPF_YVU_2P_444,
        PVRTLPF_YUV10MSB_2P_444,
        PVRTLPF_YUV10LSB_2P_444,
        PVRTLPF_YVU10MSB_2P_444,
        PVRTLPF_YVU10LSB_2P_444,
        PVRTLPF_YVU_2P_422,
        PVRTLPF_YVU10MSB_2P_422,
        PVRTLPF_YVU10LSB_2P_422,
        PVRTLPF_YVU_2P_420,         // NV21
        PVRTLPF_YVU10MSB_2P_420,
        PVRTLPF_YVU10LSB_2P_420,

        //Invalid value
        PVRTLPF_NumCompressedPFs
    }

    /// <summary>
    /// Data types. Describes how the data is interpreted by PVRTexLib and
    /// how the pointer returned by PVRTexLib_GetTextureDataPtr() should
    /// be interpreted.
    /// </summary>
    public enum PVRTexLibVariableType
    {
        PVRTLVT_UnsignedByteNorm,
        PVRTLVT_SignedByteNorm,
        PVRTLVT_UnsignedByte,
        PVRTLVT_SignedByte,
        PVRTLVT_UnsignedShortNorm,
        PVRTLVT_SignedShortNorm,
        PVRTLVT_UnsignedShort,
        PVRTLVT_SignedShort,
        PVRTLVT_UnsignedIntegerNorm,
        PVRTLVT_SignedIntegerNorm,
        PVRTLVT_UnsignedInteger,
        PVRTLVT_SignedInteger,
        PVRTLVT_SignedFloat,
        PVRTLVT_Float = PVRTLVT_SignedFloat, //the name Float is now deprecated.
        PVRTLVT_UnsignedFloat,
        PVRTLVT_NumVarTypes,

        PVRTLVT_Invalid = 255
    }

    /// <summary>
    /// Quality level to compress the texture with. Applies to PVRTC,
    /// ETC, ASTC, BASIS and IMGIC formats.
    /// </summary>
    public enum PVRTexLibCompressorQuality
    {
        PVRTLCQ_PVRTCFastest = 0,   //!< PVRTC fastest
        PVRTLCQ_PVRTCFast,          //!< PVRTC fast
        PVRTLCQ_PVRTCLow,           //!< PVRTC low
        PVRTLCQ_PVRTCNormal,        //!< PVRTC normal
        PVRTLCQ_PVRTCHigh,          //!< PVRTC high
        PVRTLCQ_PVRTCVeryHigh,      //!< PVRTC very high
        PVRTLCQ_PVRTCThorough,      //!< PVRTC thorough
        PVRTLCQ_PVRTCBest,          //!< PVRTC best
        PVRTLCQ_NumPVRTCModes,      //!< Number of PVRTC modes

        PVRTLCQ_ETCFast = 0,        //!< ETC fast
        PVRTLCQ_ETCNormal,          //!< ETC normal
        PVRTLCQ_ETCSlow,            //!< ETC slow
        PVRTLCQ_NumETCModes,        //!< Number of ETC modes

        PVRTLCQ_ASTCVeryFast = 0,   //!< ASTC very fast
        PVRTLCQ_ASTCFast,           //!< ASTC fast
        PVRTLCQ_ASTCMedium,         //!< ASTC medium
        PVRTLCQ_ASTCThorough,       //!< ASTC thorough
        PVRTLCQ_ASTCExhaustive,     //!< ASTC exhaustive
        PVRTLCQ_NumASTCModes,       //!< Number of ASTC modes

        PVRTLCQ_BASISULowest = 0,   //!< BASISU lowest quality
        PVRTLCQ_BASISULow,          //!< BASISU low quality
        PVRTLCQ_BASISUNormal,       //!< BASISU normal quality
        PVRTLCQ_BASISUHigh,         //!< BASISU high quality
        PVRTLCQ_BASISUBest,         //!< BASISU best quality
        PVRTLCQ_NumBASISUModes,     //!< Number of BASISU modes
    }

    /// <summary>
    /// Filter to apply when resizing an image
    /// </summary>
    public enum PVRTexLibResizeMode
    {
        /// <summary>
        /// Nearest filtering
        /// </summary>
        PVRTLRM_Nearest,
        /// <summary>
        /// Linear filtering
        /// </summary>
        PVRTLRM_Linear,
        /// <summary>
        /// Cubic filtering, uses Catmull-Rom splines.
        /// </summary>
        PVRTLRM_Cubic,
        /// <summary>
        /// Number of resize modes
        /// </summary>
        PVRTLRM_Modes
    }

    public enum PVRTexLibFileContainerType
    {
        /// <summary>
        /// PVR: https://docs.imgtec.com/Specifications/PVR_File_Format_Specification/topics/pvr_intro.html
        /// </summary>
        PVRTLFCT_PVR,
        /// <summary>
        /// KTX version 1: https://www.khronos.org/registry/KTX/specs/1.0/ktxspec_v1.html
        /// </summary>
        PVRTLFCT_KTX,
        /// <summary>
        /// KTX version 2: https://github.khronos.org/KTX-Specification/
        /// </summary>
        PVRTLFCT_KTX2,
        /// <summary>
        /// ASTC compressed textures only: https://github.com/ARM-software/astc-encoder
        /// </summary>
        PVRTLFCT_ASTC,
        /// <summary>
        /// Basis Universal compressed textures only: https://github.com/BinomialLLC/basis_universal
        /// </summary>
        PVRTLFCT_BASIS,
        /// <summary>
        /// DirectDraw Surface: https://docs.microsoft.com/en-us/windows/win32/direct3ddds/dx-graphics-dds-reference
        /// </summary>
        PVRTLFCT_DDS,
        /// <summary>
        /// C style header
        /// </summary>
        PVRTLFCT_CHeader
    }

    /// <summary>
    /// The clamping mode to use when performing a colour diff
    /// </summary>
    public enum PVRTexLibColourDiffMode
    {
        /// <summary>
        /// Absolute
        /// </summary>
        PVRTLCDM_Abs,
        /// <summary>
        /// Signed
        /// </summary>
        PVRTLCDM_Signed
    }

    /// <summary>
    /// Legacy API enum.
    /// </summary>
    public enum PVRTexLibLegacyApi
    {
        /// <summary>
        /// OpenGL ES 1.x
        /// </summary>
        PVRTLLAPI_OGLES = 1,
        /// <summary>
        /// OpenGL ES 2.0
        /// </summary>
        PVRTLLAPI_OGLES2,
        /// <summary>
        /// Direct 3D M
        /// </summary>
        PVRTLLAPI_D3DM,
        /// <summary>
        /// Open GL
        /// </summary>
        PVRTLLAPI_OGL,
        /// <summary>
        /// DirextX 9
        /// </summary>
        PVRTLLAPI_DX9,
        /// <summary>
        /// DirectX 10
        /// </summary>
        PVRTLLAPI_DX10,
        /// <summary>
        /// Open VG
        /// </summary>
        PVRTLLAPI_OVG,
        /// <summary>
        /// MGL
        /// </summary>
        PVRTLLAPI_MGL,
    }

    /// <summary>
    /// <para>**************************************************************************</para>
    /// <para>* Integer types</para>
    /// <para>**************************************************************************</para>
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct PVRTextureHeaderV3
    {
        public uint u32Version;
        public uint u32Flags;
        public ulong u64PixelFormat;
        public uint u32ColourSpace;
        public uint u32ChannelType;
        public uint u32Height;
        public uint u32Width;
        public uint u32Depth;
        public uint u32NumSurfaces;
        public uint u32NumFaces;
        public uint u32MIPMapCount;
        public uint u32MetaDataSize;
    }
}
/*****************************************************************************
End of file (PVRTexLibDefines.h)
*****************************************************************************/
