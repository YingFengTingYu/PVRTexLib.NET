/*!****************************************************************************

 @file         PVRTexLibDefines.h
 @copyright    Copyright (c) Imagination Technologies Limited.
 @brief        Public PVRTexLib defines header.

******************************************************************************/
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace PVRTexLib
{
    /// <summary>
    /// Texture related constants and macros.
    /// </summary>
    public static partial class PVRDefine
    {
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
        /// <param name="C1Name">channel1 name</param>
        /// <param name="C2Name">channel2 name</param>
        /// <param name="C3Name">channel3 name</param>
        /// <param name="C4Name">channel4 name</param>
        /// <param name="C1Bits">channel1 bits</param>
        /// <param name="C2Bits">channel2 bits</param>
        /// <param name="C3Bits">channel3 bits</param>
        /// <param name="C4Bits">channel4 bits</param>
        /// <returns>format PixelID</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong PVRTGENPIXELID4(ulong C1Name, ulong C2Name, ulong C3Name, ulong C4Name, ulong C1Bits, ulong C2Bits, ulong C3Bits, ulong C4Bits)
        {
            return C1Name + (C2Name << 8) + (C3Name << 16) + (C4Name << 24) + (C1Bits << 32) + (C2Bits << 40) + (C3Bits << 48) + (C4Bits << 56);
        }

        /// <summary>
        /// Generate a 1 channel PixelID.
        /// </summary>
        /// <param name="C1Name">channel1 name</param>
        /// <param name="C2Name">channel2 name</param>
        /// <param name="C3Name">channel3 name</param>
        /// <param name="C1Bits">channel1 bits</param>
        /// <param name="C2Bits">channel2 bits</param>
        /// <param name="C3Bits">channel3 bits</param>
        /// <returns>format PixelID</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong PVRTGENPIXELID3(ulong C1Name, ulong C2Name, ulong C3Name, ulong C1Bits, ulong C2Bits, ulong C3Bits)
        {
            return PVRTGENPIXELID4(C1Name, C2Name, C3Name, 0, C1Bits, C2Bits, C3Bits, 0);
        }

        /// <summary>
        /// Generate a 2 channel PixelID.
        /// </summary>
        /// <param name="C1Name">channel1 name</param>
        /// <param name="C2Name">channel2 name</param>
        /// <param name="C1Bits">channel1 bits</param>
        /// <param name="C2Bits">channel2 bits</param>
        /// <returns>format PixelID</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong PVRTGENPIXELID2(ulong C1Name, ulong C2Name, ulong C1Bits, ulong C2Bits)
        {
            return PVRTGENPIXELID4(C1Name, C2Name, 0, 0, C1Bits, C2Bits, 0, 0);
        }

        /// <summary>
        /// Generate a 3 channel PixelID.
        /// </summary>
        /// <param name="C1Name">channel1 name</param>
        /// <param name="C1Bits">channel1 bits</param>
        /// <returns>format PixelID</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong PVRTGENPIXELID1(ulong C1Name, ulong C1Bits)
        {
            return PVRTGENPIXELID4(C1Name, 0, 0, 0, C1Bits, 0, 0, 0);
        }

        /// <summary>
        /// 2D texture offset
        /// </summary>
        /// <param name="x">x point</param>
        /// <param name="y">y point</param>
        /// <param name="width">texture width</param>
        /// <returns>offset of the point</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int TEXOFFSET2D(int x, int y, int width)
        {
            return x + y * width;
        }

        /// <summary>
        /// 3D texture offset
        /// </summary>
        /// <param name="x">x point</param>
        /// <param name="y">y point</param>
        /// <param name="z">z point</param>
        /// <param name="width">texture width</param>
        /// <param name="height">texture height</param>
        /// <returns>offset of the point</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int TEXOFFSET3D(int x, int y, int z, int width, int height)
        {
            return x + y * width + z * width * height;
        }

        /// <summary>
        /// header size of pvr3 file
        /// </summary>
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
        TextureAtlasCoords = 0,
        BumpData,
        CubeMapOrder,
        TextureOrientation,
        BorderData,
        Padding,
        PerChannelType,
        SupercompressionGlobalData,
        MaxRange,
        NumMetaDataTypes
    }

    /// <summary>
    /// Axis
    /// </summary>
    public enum PVRTexLibAxis
    {
        /// <summary>
        /// the axis is x
        /// </summary>
        X = 0,
        /// <summary>
        /// y
        /// </summary>
        Y = 1,
        /// <summary>
        /// the axis is z
        /// </summary>
        Z = 2
    }

    /// <summary>
    /// Image orientations per axis
    /// </summary>
    [Flags]
    public enum PVRTexLibOrientation
    {
        /// <summary>
        /// the orientation is left
        /// </summary>
        Left = 1 << PVRTexLibAxis.X,
        /// <summary>
        /// the orientation is right
        /// </summary>
        Right = 0,
        /// <summary>
        /// the orientation is up
        /// </summary>
        Up = 1 << PVRTexLibAxis.Y,
        /// <summary>
        /// the orientation is down
        /// </summary>
        Down = 0,
        /// <summary>
        /// the orientation is out
        /// </summary>
        Out = 1 << PVRTexLibAxis.Z,
        /// <summary>
        /// the orientation is in
        /// </summary>
        In = 0
    }

    /// <summary>
    /// Describes the colour space of the texture
    /// </summary>
    public enum PVRTexLibColourSpace
    {
        /// <summary>
        /// linear color space
        /// </summary>
        Linear,
        /// <summary>
        /// srgb color space
        /// </summary>
        sRGB,
        /// <summary>
        /// bt601 color space
        /// </summary>
        BT601,
        /// <summary>
        /// bt709 color space
        /// </summary>
        BT709,
        /// <summary>
        /// bt2020 color space
        /// </summary>
        BT2020,
        /// <summary>
        /// Invalid value
        /// </summary>
        NumSpaces
    }

    /// <summary>
    /// Channel names for non-compressed formats
    /// </summary>
    public enum PVRTexLibChannelName
    {
        NoChannel,
        Red,
        Green,
        Blue,
        Alpha,
        Luminance,
        Intensity,
        Depth,
        Stencil,
        Unspecified,
        NumChannels
    }

    /// <summary>
    /// Compressed pixel formats that PVRTexLib understands
    /// </summary>
    public enum PVRTexLibPixelFormat
    {
        PVRTCI_2bpp_RGB,
        PVRTCI_2bpp_RGBA,
        PVRTCI_4bpp_RGB,
        PVRTCI_4bpp_RGBA,
        PVRTCII_2bpp,
        PVRTCII_4bpp,
        ETC1,
        DXT1,
        DXT2,
        DXT3,
        DXT4,
        DXT5,

        //These formats are identical to some DXT formats.
        BC1 = DXT1,
        BC2 = DXT3,
        BC3 = DXT5,
        BC4,
        BC5,

        /* Currently unsupported: */
        BC6,
        BC7,
        /* ~~~~~~~~~~~~~~~~~~ */

        // Packed YUV formats
        /// <summary>
        /// https://www.fourcc.org/pixel-format/yuv-uyvy/
        /// </summary>
        UYVY_422,
        /// <summary>
        /// https://www.fourcc.org/pixel-format/yuv-yuy2/
        /// </summary>
        YUY2_422,

        BW1bpp,
        SharedExponentR9G9B9E5,
        RGBG8888,
        GRGB8888,
        ETC2_RGB,
        ETC2_RGBA,
        ETC2_RGB_A1,
        EAC_R11,
        EAC_RG11,

        ASTC_4x4,
        ASTC_5x4,
        ASTC_5x5,
        ASTC_6x5,
        ASTC_6x6,
        ASTC_8x5,
        ASTC_8x6,
        ASTC_8x8,
        ASTC_10x5,
        ASTC_10x6,
        ASTC_10x8,
        ASTC_10x10,
        ASTC_12x10,
        ASTC_12x12,

        ASTC_3x3x3,
        ASTC_4x3x3,
        ASTC_4x4x3,
        ASTC_4x4x4,
        ASTC_5x4x4,
        ASTC_5x5x4,
        ASTC_5x5x5,
        ASTC_6x5x5,
        ASTC_6x6x5,
        ASTC_6x6x6,

        BASISU_ETC1S,
        BASISU_UASTC,

        RGBM,
        RGBD,

        PVRTCI_HDR_6bpp,
        PVRTCI_HDR_8bpp,
        PVRTCII_HDR_6bpp,
        PVRTCII_HDR_8bpp,

        // The memory layout for 10 and 12 bit YUV formats that are packed into a WORD (16 bits) is denoted by MSB or LSB:
        // MSB denotes that the sample is stored in the most significant <N> bits
        // LSB denotes that the sample is stored in the least significant <N> bits
        // All YUV formats are little endian

        // Packed YUV formats
        VYUA10MSB_444,
        VYUA10LSB_444,
        VYUA12MSB_444,
        VYUA12LSB_444,
        /// <summary>
        /// Y410
        /// </summary>
        UYV10A2_444,
        /// <summary>
        /// Y416
        /// </summary>
        UYVA16_444,
        /// <summary>
        /// Y216
        /// </summary>
        YUYV16_422,
        UYVY16_422,
        /// <summary>
        /// Y210
        /// </summary>
        YUYV10MSB_422,
        YUYV10LSB_422,
        UYVY10MSB_422,
        UYVY10LSB_422,
        YUYV12MSB_422,
        YUYV12LSB_422,
        UYVY12MSB_422,
        UYVY12LSB_422,

        /*
            Reserved for future expansion
        */

        // 3 Plane (Planar) YUV formats
        YUV_3P_444 = 270,
        YUV10MSB_3P_444,
        YUV10LSB_3P_444,
        YUV12MSB_3P_444,
        YUV12LSB_3P_444,
        YUV16_3P_444,
        YUV_3P_422,
        YUV10MSB_3P_422,
        YUV10LSB_3P_422,
        YUV12MSB_3P_422,
        YUV12LSB_3P_422,
        YUV16_3P_422,
        YUV_3P_420,
        YUV10MSB_3P_420,
        YUV10LSB_3P_420,
        YUV12MSB_3P_420,
        YUV12LSB_3P_420,
        YUV16_3P_420,
        YVU_3P_420,

        /*
            Reserved for future expansion
        */

        // 2 Plane (Biplanar/semi-planar) YUV formats
        /// <summary>
        /// P208
        /// </summary>
        YUV_2P_422 = 480,
        /// <summary>
        /// P210
        /// </summary>
        YUV10MSB_2P_422,
        YUV10LSB_2P_422,
        YUV12MSB_2P_422,
        YUV12LSB_2P_422,
        /// <summary>
        /// P216
        /// </summary>
        YUV16_2P_422,
        /// <summary>
        /// NV12
        /// </summary>
        YUV_2P_420,
        /// <summary>
        /// P010
        /// </summary>
        YUV10MSB_2P_420,
        YUV10LSB_2P_420,
        YUV12MSB_2P_420,
        YUV12LSB_2P_420,
        /// <summary>
        /// P016
        /// </summary>
        YUV16_2P_420,
        YUV_2P_444,
        YVU_2P_444,
        YUV10MSB_2P_444,
        YUV10LSB_2P_444,
        YVU10MSB_2P_444,
        YVU10LSB_2P_444,
        YVU_2P_422,
        YVU10MSB_2P_422,
        YVU10LSB_2P_422,
        /// <summary>
        /// NV21
        /// </summary>
        YVU_2P_420,
        YVU10MSB_2P_420,
        YVU10LSB_2P_420,

        /// <summary>
        /// Invalid value
        /// </summary>
        NumCompressedPFs
    }

    /// <summary>
    /// Data types. Describes how the data is interpreted by PVRTexLib and
    /// how the pointer returned by PVRTexLib_GetTextureDataPtr() should
    /// be interpreted.
    /// </summary>
    public enum PVRTexLibVariableType
    {
        UnsignedByteNorm,
        SignedByteNorm,
        UnsignedByte,
        SignedByte,
        UnsignedShortNorm,
        SignedShortNorm,
        UnsignedShort,
        SignedShort,
        UnsignedIntegerNorm,
        SignedIntegerNorm,
        UnsignedInteger,
        SignedInteger,
        SignedFloat,
        [Obsolete("the name Float is now deprecated. Use SignedFloat instead.")] Float = SignedFloat,
        UnsignedFloat,
        NumVarTypes,

        Invalid = 255
    }

    /// <summary>
    /// Quality level to compress the texture with. Applies to PVRTC,
    /// ETC, ASTC, BASIS and IMGIC formats.
    /// </summary>
    public enum PVRTexLibCompressorQuality
    {
        /// <summary>
        /// PVRTC fastest
        /// </summary>
        PVRTCFastest = 0,
        /// <summary>
        /// PVRTC fast
        /// </summary>
        PVRTCFast,
        /// <summary>
        /// PVRTC low
        /// </summary>
        PVRTCLow,
        /// <summary>
        /// PVRTC normal
        /// </summary>
        PVRTCNormal,
        /// <summary>
        /// PVRTC high
        /// </summary>
        PVRTCHigh,
        /// <summary>
        /// PVRTC very high
        /// </summary>
        PVRTCVeryHigh,
        /// <summary>
        /// PVRTC thorough
        /// </summary>
        PVRTCThorough,
        /// <summary>
        /// PVRTC best
        /// </summary>
        PVRTCBest,
        /// <summary>
        /// Number of PVRTC modes
        /// </summary>
        NumPVRTCModes,

        /// <summary>
        /// ETC fast
        /// </summary>
        ETCFast = 0,
        /// <summary>
        /// ETC normal
        /// </summary>
        ETCNormal,
        /// <summary>
        /// ETC slow
        /// </summary>
        ETCSlow,
        /// <summary>
        /// Number of ETC modes
        /// </summary>
        NumETCModes,

        /// <summary>
        /// ASTC very fast
        /// </summary>
        ASTCVeryFast = 0,
        /// <summary>
        /// ASTC fast
        /// </summary>
        ASTCFast,
        /// <summary>
        /// ASTC medium
        /// </summary>
        ASTCMedium,
        /// <summary>
        /// ASTC thorough
        /// </summary>
        ASTCThorough,
        /// <summary>
        /// ASTC exhaustive
        /// </summary>
        ASTCExhaustive,
        /// <summary>
        /// Number of ASTC modes
        /// </summary>
        NumASTCModes,

        /// <summary>
        /// BASISU lowest quality
        /// </summary>
        BASISULowest = 0,
        /// <summary>
        /// BASISU low quality
        /// </summary>
        BASISULow,
        /// <summary>
        /// BASISU normal quality
        /// </summary>
        BASISUNormal,
        /// <summary>
        /// BASISU high quality
        /// </summary>
        BASISUHigh,
        /// <summary>
        /// BASISU best quality
        /// </summary>
        BASISUBest,
        /// <summary>
        /// Number of BASISU modes
        /// </summary>
        NumBASISUModes
    }

    /// <summary>
    /// Filter to apply when resizing an image
    /// </summary>
    public enum PVRTexLibResizeMode
    {
        /// <summary>
        /// Nearest filtering
        /// </summary>
        Nearest,
        /// <summary>
        /// Linear filtering
        /// </summary>
        Linear,
        /// <summary>
        /// Cubic filtering, uses Catmull-Rom splines.
        /// </summary>
        Cubic,
        /// <summary>
        /// Number of resize modes
        /// </summary>
        Modes
    }

    /// <summary>
    /// File container type
    /// </summary>
    public enum PVRTexLibFileContainerType
    {
        /// <summary>
        /// PVR: https://docs.imgtec.com/Specifications/PVR_File_Format_Specification/topics/pvr_intro.html
        /// </summary>
        PVR,
        /// <summary>
        /// KTX version 1: https://www.khronos.org/registry/KTX/specs/1.0/ktxspec_v1.html
        /// </summary>
        KTX,
        /// <summary>
        /// KTX version 2: https://github.khronos.org/KTX-Specification/
        /// </summary>
        KTX2,
        /// <summary>
        /// ASTC compressed textures only: https://github.com/ARM-software/astc-encoder
        /// </summary>
        ASTC,
        /// <summary>
        /// Basis Universal compressed textures only: https://github.com/BinomialLLC/basis_universal
        /// </summary>
        BASIS,
        /// <summary>
        /// DirectDraw Surface: https://docs.microsoft.com/en-us/windows/win32/direct3ddds/dx-graphics-dds-reference
        /// </summary>
        DDS,
        /// <summary>
        /// C style header
        /// </summary>
        CHeader
    }

    /// <summary>
    /// The clamping mode to use when performing a colour diff
    /// </summary>
    public enum PVRTexLibColourDiffMode
    {
        /// <summary>
        /// Absolute
        /// </summary>
        Abs,
        /// <summary>
        /// Signed
        /// </summary>
        Signed
    }

    /// <summary>
    /// Legacy API enum.
    /// </summary>
    public enum PVRTexLibLegacyApi
    {
        /// <summary>
        /// OpenGL ES 1.x
        /// </summary>
        OGLES = 1,
        /// <summary>
        /// OpenGL ES 2.0
        /// </summary>
        OGLES2,
        /// <summary>
        /// Direct 3D M
        /// </summary>
        D3DM,
        /// <summary>
        /// Open GL
        /// </summary>
        OGL,
        /// <summary>
        /// DirextX 9
        /// </summary>
        DX9,
        /// <summary>
        /// DirectX 10
        /// </summary>
        DX10,
        /// <summary>
        /// Open VG
        /// </summary>
        OVG,
        /// <summary>
        /// MGL
        /// </summary>
        MGL
    }

    /// <summary>
    /// A header for a PVR texture. Contains everything required to read
    /// a texture accurately, and nothing more. Extraneous data is stored
    /// in a MetaDataBlock. Correct use of the texture may rely on
    /// MetaDataBlock, but accurate data loading can be done through the
    /// standard header alone.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct PVRTextureHeaderV3
    {
        /// <summary>
        /// Version of the file header, used to identify it.
        /// </summary>
        public uint u32Version;
        /// <summary>
        /// Various format flags.
        /// </summary>
        public uint u32Flags;
        /// <summary>
        /// The pixel format, 8cc value storing the 4 channel identifiers and their respective sizes.
        /// </summary>
        public ulong u64PixelFormat;
        /// <summary>
        /// The Colour Space of the texture, currently either linear RGB or sRGB.
        /// </summary>
        public uint u32ColourSpace;
        /// <summary>
        /// Variable type that the channel is stored in. Supports signed/unsigned int/short/byte or float for now.
        /// </summary>
        public uint u32ChannelType;
        /// <summary>
        /// Height of the texture.
        /// </summary>
        public uint u32Height;
        /// <summary>
        /// Width of the texture.
        /// </summary>
        public uint u32Width;
        /// <summary>
        /// Depth of the texture. (Z-slices)
        /// </summary>
        public uint u32Depth;
        /// <summary>
        /// Number of members in a Texture Array.
        /// </summary>
        public uint u32NumSurfaces;
        /// <summary>
        /// Number of faces in a Cube Map. Maybe be a value other than 6.
        /// </summary>
        public uint u32NumFaces;
        /// <summary>
        /// Number of MIP Maps in the texture - NB: Includes top level.
        /// </summary>
        public uint u32MIPMapCount;
        /// <summary>
        /// Size of the accompanying meta data.
        /// </summary>
        public uint u32MetaDataSize;
    }
}
/*****************************************************************************
End of file (PVRTexLibDefines.h)
*****************************************************************************/
