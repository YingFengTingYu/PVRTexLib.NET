namespace PVRTexLib.Tests
{
    [TestFixture]
    public class FunctionTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            if (Directory.Exists("images/generated2"))
            {
                Directory.Delete("images/generated2", true);
            }
            Directory.CreateDirectory("images/generated2");
        }

        [TestCase("images/cloud.png", "images/generated2/cloud_SaveTextureToMemory.dds")]
        [TestCase("images/flower.pvr", "images/generated2/flower_SaveTextureToMemory.dds")]
        public void CanSaveTextureToMemory(string inputPath, string outputPath)
        {
            using PVRTexture texture = new PVRTexture(inputPath);
            Assert.That(texture.GetTextureDataSize(), Is.Not.Zero, "texture should not be null");
            Assert.That(texture.Transcode((ulong)PVRTexLibPixelFormat.DXT5, PVRTexLibVariableType.UnsignedByteNorm, PVRTexLibColourSpace.sRGB), Is.True, "status should be true");
            unsafe
            {
                void* ptr = null;
                try
                {
                    ptr = texture.SaveTextureToMemory(PVRTexLibFileContainerType.DDS, out ulong size);
                    Assert.That((nint)ptr, Is.Not.Zero, "pointer should not be zero");
                    using UnmanagedMemoryStream memStream = new UnmanagedMemoryStream((byte*)ptr, (long)size);
                    using FileStream fileStream = File.Create(outputPath);
                    memStream.CopyTo(fileStream);
                }
                finally
                {
                    if (ptr != null)
                    {
                        PVRTextureHeader.Free((nint)ptr);
                    }
                }
            }
            Assert.That(texture, Is.Not.Null, "texture should not be null");
        }
    }
}
