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
        /// A 64 bit pixel format ID and this will give you the high bits of a pixel format to check for a compressed format.
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
        /// <summary>
        /// Texture Atlas Coords
        /// </summary>
        TextureAtlasCoords = 0,
        /// <summary>
        /// Bump Data
        /// </summary>
        BumpData,
        /// <summary>
        /// Cube Map Order
        /// </summary>
        CubeMapOrder,
        /// <summary>
        /// Texture Orientation
        /// </summary>
        TextureOrientation,
        /// <summary>
        /// Border Data
        /// </summary>
        BorderData,
        /// <summary>
        /// Padding
        /// </summary>
        Padding,
        /// <summary>
        /// Per-Channel Type
        /// </summary>
        PerChannelType,
        /// <summary>
        /// Supercompression Global Data
        /// </summary>
        SupercompressionGlobalData,
        /// <summary>
        /// Max Range
        /// </summary>
        MaxRange,
        /// <summary>
        /// Invalid value
        /// </summary>
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
        /// linear color space https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#PRIMARIES_ADOBERGB
        /// </summary>
        Linear,
        /// <summary>
        /// srgb color space https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#TRANSFER_SRGB
        /// </summary>
        sRGB,
        /// <summary>
        /// bt601 color space https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#PRIMARIES_BT601_EBU
        /// </summary>
        BT601,
        /// <summary>
        /// bt709 color space https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#PRIMARIES_BT709
        /// </summary>
        BT709,
        /// <summary>
        /// bt2020 color space https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#PRIMARIES_BT2020
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
        /// <summary>
        /// No channel
        /// </summary>
        NoChannel,
        /// <summary>
        /// Red channel
        /// </summary>
        Red,
        /// <summary>
        /// Green channel
        /// </summary>
        Green,
        /// <summary>
        /// Blue channel
        /// </summary>
        Blue,
        /// <summary>
        /// Alpha channel
        /// </summary>
        Alpha,
        /// <summary>
        /// Luminance channel
        /// </summary>
        Luminance,
        /// <summary>
        /// Intensity channel
        /// </summary>
        Intensity,
        /// <summary>
        /// Depth channel
        /// </summary>
        Depth,
        /// <summary>
        /// Stencil channel
        /// </summary>
        Stencil,
        /// <summary>
        /// Unspecified
        /// </summary>
        Unspecified,
        /// <summary>
        /// Invalid value
        /// </summary>
        NumChannels
    }

    /// <summary>
    /// Compressed pixel formats that PVRTexLib understands
    /// </summary>
    public enum PVRTexLibPixelFormat
    {
        /// <summary>
        /// PVRTCv1 2bpp RGB https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#_format_pvrtc1_2bpp
        /// </summary>
        PVRTCI_2bpp_RGB,
        /// <summary>
        /// PVRTCv1 2bpp RGBA https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#_format_pvrtc1_2bpp
        /// </summary>
        PVRTCI_2bpp_RGBA,
        /// <summary>
        /// PVRTCv1 4bpp RGB https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#_format_pvrtc1_4bpp
        /// </summary>
        PVRTCI_4bpp_RGB,
        /// <summary>
        /// PVRTCv1 4bpp RGBA https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#_format_pvrtc1_4bpp
        /// </summary>
        PVRTCI_4bpp_RGBA,
        /// <summary>
        /// PVRTCv2 2bpp RGBA https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#_format_pvrtc2_2bpp
        /// </summary>
        PVRTCII_2bpp,
        /// <summary>
        /// PVRTCv2 4bpp RGBA https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#_format_pvrtc2_4bpp
        /// </summary>
        PVRTCII_4bpp,
        /// <summary>
        /// ETC1 RGB https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#ETC1
        /// </summary>
        ETC1,
        /// <summary>
        /// DXT1 RGB/RGBA(BC1) https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#s3tc_bc1_noalpha and https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#s3tc_bc1_alpha
        /// </summary>
        DXT1,
        /// <summary>
        /// DXT2 RGBA(similar to DXT3: https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#s3tc_bc2)
        /// </summary>
        DXT2,
        /// <summary>
        /// DXT3 RGBA(BC2) https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#s3tc_bc2
        /// </summary>
        DXT3,
        /// <summary>
        /// DXT4 RGBA(similar to DXT5: https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#s3tc_bc3)
        /// </summary>
        DXT4,
        /// <summary>
        /// DXT5 RGBA(BC3) https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#s3tc_bc3
        /// </summary>
        DXT5,

        //These formats are identical to some DXT formats.
        /// <summary>
        /// BC1 RGB/RGBA(DXT1) https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#s3tc_bc1_noalpha and https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#s3tc_bc1_alpha
        /// </summary>
        BC1 = DXT1,
        /// <summary>
        /// BC2 RGBA(DXT3) https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#s3tc_bc2
        /// </summary>
        BC2 = DXT3,
        /// <summary>
        /// BC3 RGBA(DXT5) https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#s3tc_bc3
        /// </summary>
        BC3 = DXT5,
        /// <summary>
        /// BC4(ATI1, LATC1, RGTC1, DXN) https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#rgtc_bc4 and https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#_bc4_signed
        /// </summary>
        BC4,
        /// <summary>
        /// BC5(ATI2, LATC2, RGTC2, DXN) https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#rgtc_bc5 and https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#_bc5_signed
        /// </summary>
        BC5,

        /* Currently unsupported: */
        /// <summary>
        /// BC6H(BPTC RGB HDR) https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#bptc_bc6h
        /// </summary>
        BC6,
        /// <summary>
        /// BC7(BPTC RGBA) https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#bptc_bc7
        /// </summary>
        BC7,
        /* ~~~~~~~~~~~~~~~~~~ */

        // Packed YUV formats
        /// <summary>
        /// Packed YUV formats https://www.fourcc.org/pixel-format/yuv-uyvy/
        /// </summary>
        UYVY_422,
        /// <summary>
        /// Packed YUV formats https://www.fourcc.org/pixel-format/yuv-yuy2/
        /// </summary>
        YUY2_422,
        /// <summary>
        /// 1bpp
        /// </summary>
        BW1bpp,
        /// <summary>
        /// Shared Exponent R9 G9 B9 E5 https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#_example_format_descriptors Table 98
        /// </summary>
        SharedExponentR9G9B9E5,
        /// <summary>
        /// RGBG Compression https://learn.microsoft.com/zh-cn/windows-hardware/drivers/ddi/d3dukmdt/ne-d3dukmdt-_d3dddiformat
        /// </summary>
        RGBG8888,
        /// <summary>
        /// GRGB Compression https://learn.microsoft.com/zh-cn/windows-hardware/drivers/ddi/d3dukmdt/ne-d3dukmdt-_d3dddiformat
        /// </summary>
        GRGB8888,
        /// <summary>
        /// ETC2 RGB https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#RGBETC2
        /// </summary>
        ETC2_RGB,
        /// <summary>
        /// ETC2 RGBA https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#RGBAETC2
        /// </summary>
        ETC2_RGBA,
        /// <summary>
        /// ETC2 RGB A1 https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#RGBETC2_PUNCHTHROUGH
        /// </summary>
        ETC2_RGB_A1,
        /// <summary>
        /// EAC R11 https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#_format_signed_r11_eac
        /// </summary>
        EAC_R11,
        /// <summary>
        /// EAC RG11 https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#Section-signedr11eac-signedr11eac
        /// </summary>
        EAC_RG11,

        /// <summary>
        /// ASTC https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#ASTC
        /// </summary>
        ASTC_4x4,
        /// <summary>
        /// ASTC https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#ASTC
        /// </summary>
        ASTC_5x4,
        /// <summary>
        /// ASTC https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#ASTC
        /// </summary>
        ASTC_5x5,
        /// <summary>
        /// ASTC https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#ASTC
        /// </summary>
        ASTC_6x5,
        /// <summary>
        /// ASTC https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#ASTC
        /// </summary>
        ASTC_6x6,
        /// <summary>
        /// ASTC https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#ASTC
        /// </summary>
        ASTC_8x5,
        /// <summary>
        /// ASTC https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#ASTC
        /// </summary>
        ASTC_8x6,
        /// <summary>
        /// ASTC https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#ASTC
        /// </summary>
        ASTC_8x8,
        /// <summary>
        /// ASTC https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#ASTC
        /// </summary>
        ASTC_10x5,
        /// <summary>
        /// ASTC https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#ASTC
        /// </summary>
        ASTC_10x6,
        /// <summary>
        /// ASTC https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#ASTC
        /// </summary>
        ASTC_10x8,
        /// <summary>
        /// ASTC https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#ASTC
        /// </summary>
        ASTC_10x10,
        /// <summary>
        /// ASTC https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#ASTC
        /// </summary>
        ASTC_12x10,
        /// <summary>
        /// ASTC https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#ASTC
        /// </summary>
        ASTC_12x12,

        /// <summary>
        /// ASTC https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#ASTC
        /// </summary>
        ASTC_3x3x3,
        /// <summary>
        /// ASTC https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#ASTC
        /// </summary>
        ASTC_4x3x3,
        /// <summary>
        /// ASTC https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#ASTC
        /// </summary>
        ASTC_4x4x3,
        /// <summary>
        /// ASTC https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#ASTC
        /// </summary>
        ASTC_4x4x4,
        /// <summary>
        /// ASTC https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#ASTC
        /// </summary>
        ASTC_5x4x4,
        /// <summary>
        /// ASTC https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#ASTC
        /// </summary>
        ASTC_5x5x4,
        /// <summary>
        /// ASTC https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#ASTC
        /// </summary>
        ASTC_5x5x5,
        /// <summary>
        /// ASTC https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#ASTC
        /// </summary>
        ASTC_6x5x5,
        /// <summary>
        /// ASTC https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#ASTC
        /// </summary>
        ASTC_6x6x5,
        /// <summary>
        /// ASTC https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#ASTC
        /// </summary>
        ASTC_6x6x6,

        /// <summary>
        /// ETC1S https://github.com/BinomialLLC/basis_universal/wiki/.basis-File-Format-and-ETC1S-Texture-Video-Specification
        /// </summary>
        BASISU_ETC1S,
        /// <summary>
        /// UASTC https://github.com/BinomialLLC/basis_universal/wiki/UASTC-Texture-Specification
        /// </summary>
        BASISU_UASTC,

        /// <summary>
        /// RGBM is an 8bit RGBA format, where Alpha is sacrificed to store a shared multiplier.
        /// </summary>
        RGBM,
        /// <summary>
        /// RGBD is an 8bit RGBA format, where Alpha is sacrificed to store a shared divider.
        /// </summary>
        RGBD,

        /// <summary>
        /// PVRTCv1 RGB 6bpp HDR
        /// </summary>
        PVRTCI_HDR_6bpp,
        /// <summary>
        /// PVRTCv1 RGB 8bpp HDR
        /// </summary>
        PVRTCI_HDR_8bpp,
        /// <summary>
        /// PVRTCv2 RGB 6bpp HDR
        /// </summary>
        PVRTCII_HDR_6bpp,
        /// <summary>
        /// PVRTCv2 RGB 8bpp HDR
        /// </summary>
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
        /// <summary>
        /// Normalized UByte
        /// </summary>
        UnsignedByteNorm,
        /// <summary>
        /// Normalized SByte
        /// </summary>
        SignedByteNorm,
        /// <summary>
        /// UByte
        /// </summary>
        UnsignedByte,
        /// <summary>
        /// SByte
        /// </summary>
        SignedByte,
        /// <summary>
        /// Normalized UInt16
        /// </summary>
        UnsignedShortNorm,
        /// <summary>
        /// Normalized SInt16
        /// </summary>
        SignedShortNorm,
        /// <summary>
        /// UInt16
        /// </summary>
        UnsignedShort,
        /// <summary>
        /// SInt16
        /// </summary>
        SignedShort,
        /// <summary>
        /// Normalized UInt32
        /// </summary>
        UnsignedIntegerNorm,
        /// <summary>
        /// Normalized SInt32
        /// </summary>
        SignedIntegerNorm,
        /// <summary>
        /// UInt32
        /// </summary>
        UnsignedInteger,
        /// <summary>
        /// SInt32
        /// </summary>
        SignedInteger,
        /// <summary>
        /// SFloat
        /// </summary>
        SignedFloat,
        /// <summary>
        /// Float
        /// </summary>
        [Obsolete("the name Float is now deprecated. Use SignedFloat instead.")] Float = SignedFloat,
        /// <summary>
        /// UFloat
        /// </summary>
        UnsignedFloat,
        /// <summary>
        /// Invalid value
        /// </summary>
        NumVarTypes,

        /// <summary>
        /// Invalid value
        /// </summary>
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
