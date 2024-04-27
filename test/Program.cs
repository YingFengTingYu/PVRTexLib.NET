using PVRTexLib;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            PVRTexture? texture = ReadAndTranscodeImage("D:\\123.jpg");
            if (texture != null)
            {
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
    }
}
