using PVRTexLib;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PVRTexture? texture = ReadAndTranscodeImage("D:\\123.jpg");
            if (texture != null)
            {
                Console.WriteLine(texture.GetTextureCubeMapOrder());
                if (!texture.GenerateMIPMaps(PVRTexLibResizeMode.PVRTLRM_Linear))
                {
                    texture.Dispose();
                }
                else if (!texture.ResizeCanvas(512, 256, 1, 0, 0, 0))
                {
                    texture.Dispose();
                }
                else
                {
                    texture.SaveToFile("D:\\1234.png");
                }
            }
        }

        static unsafe PVRTexture? ReadAndTranscodeImage(string filePath)
        {
            PVRTexture texture = new PVRTexture(filePath);
            if (texture.GetTextureDataSize() == 0)
            {
                texture.Dispose();
                return null;
            }
            ulong RGBA8888 = PVRDefine.PVRTGENPIXELID4('r', 'g', 'b', 'a', 8, 8, 8, 8);
            if (!texture.Transcode(RGBA8888, PVRTexLibVariableType.PVRTLVT_UnsignedByteNorm, PVRTexLibColourSpace.PVRTLCS_Linear))
            {
                texture.Dispose();
                return null;
            }
            return texture;
        }

        static unsafe void Transcode(void* inData, void* outData, uint width, uint height, ulong inFormat, ulong outFormat, PVRTexLibCompressorQuality quality, bool dither)
        {
            using PVRTextureHeader header = new PVRTextureHeader(inFormat, width, height, 1, 1, 1, 1, PVRTexLibColourSpace.PVRTLCS_sRGB, PVRTexLibVariableType.PVRTLVT_UnsignedByteNorm, false);
            using PVRTexture tex = new PVRTexture(header, inData);
            if (tex.GetTextureDataSize() != 0)
            {
                if (tex.Transcode(outFormat, PVRTexLibVariableType.PVRTLVT_UnsignedByteNorm, PVRTexLibColourSpace.PVRTLCS_sRGB, quality, dither))
                {
                    NativeMemory.Copy(tex.GetTextureDataPointer(0), outData, (nuint)tex.GetTextureDataSize(0));
                }
            }
        }
    }
}
