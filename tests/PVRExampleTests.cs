namespace PVRTexLib.Tests
{
    [TestFixture]
    public class PVRExampleTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            Directory.Delete("images/generated", true);
            Directory.CreateDirectory("images/generated");
        }

        [TestCase("images/flower.jpg")]
        [TestCase("images/flower.pvr")]
        public void IsReadAndTranscodeImageReturnNull(string imagePath)
        {
            using PVRTexture? texture = PVRExamples.ReadAndTranscodeImage(Path.GetFullPath(imagePath));
            Assert.That(texture, Is.Not.Null, "texture should not be null");
        }

        [TestCase("images/cloud.png", "images/generated/cloud_ApplySomeProcessing.pvr")]
        public void IsApplySomeProcessingReturnFalse(string inputPath, string outputPath)
        {
            bool status = PVRExamples.ApplySomeProcessing(inputPath, outputPath);
            Assert.That(status, Is.True, $"Can not run ApplySomeProcessing for file {inputPath}");
        }

        [TestCase("images/cloud.png", "images/generated/cloud_ResizeCanvasAndGenMipChain.pvr")]
        [TestCase("images/flower.jpg", "images/generated/flower_ResizeCanvasAndGenMipChain.pvr")]
        public void IsResizeCanvasAndGenMipChainReturnFalse(string inputPath, string outputPath)
        {
            bool status = PVRExamples.ResizeCanvasAndGenMipChain(inputPath, outputPath);
            Assert.That(status, Is.True, $"Can not run ResizeCanvasAndGenMipChain for file {inputPath}");
        }

        [TestCase("images/generated/CreateTextureFromPixelData.pvr")]
        public void IsCreateTextureFromPixelDataReturnFalse(string outputPath)
        {
            bool status = PVRExamples.CreateTextureFromPixelData(outputPath);
            Assert.That(status, Is.True, $"Can not run CreateTextureFromPixelData to generate file {outputPath}");
        }

        [TestCase("images/cloud.png")]
        public void IsReadTextureMetaDataReturnFalse(string inputPath)
        {
            bool status = PVRExamples.ReadTextureMetaData(inputPath);
            Assert.That(status, Is.True, $"Can not run ReadTextureMetaData for file {inputPath}");
        }

        [TestCase("images/flower.jpg", "images/generated/flower_WriteMetaData.pvr")]
        public void IsWriteMetaDataReturnFalse(string inputPath, string outputPath)
        {
            bool status = PVRExamples.WriteMetaData(inputPath, outputPath);
            Assert.That(status, Is.True, $"Can not run WriteMetaData for file {inputPath}");
        }

        [TestCase("images/flower.jpg", "images/generated/flower_AccessingPixelDataDirectly.pvr")]
        public void IsAccessingPixelDataDirectlyReturnFalse(string inputPath, string outputPath)
        {
            bool status = PVRExamples.AccessingPixelDataDirectly(inputPath, outputPath);
            Assert.That(status, Is.True, $"Can not run AccessingPixelDataDirectly for file {inputPath}");
        }
    }
}
