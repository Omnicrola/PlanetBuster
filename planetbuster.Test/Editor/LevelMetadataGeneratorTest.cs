using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Assets.Editor;
using Assets.Scripts;
using Assets.Scripts.Models;
using NUnit.Framework;

namespace planetbuster.Test.Editor
{
    [TestFixture]
    public class LevelMetadataGeneratorTest
    {
        private string _testingFilepath;

        [SetUp]
        public void Setup()
        {
            _testingFilepath = "Test_Metadata/";
            Directory.CreateDirectory(_testingFilepath);
        }

        [TearDown]
        public void TearDown()
        {
            Directory.Delete(_testingFilepath, true);
        }

        [Test]
        public void TestRegenerate()
        {

            var level1 = CreateEmptyLevel(1, "test one");
            var level2 = CreateEmptyLevel(6, "test six");
            var level3 = CreateEmptyLevel(99, "test ninety nine");

            var levelMetaDataGenerator = new LevelMetaDataGenerator(_testingFilepath);
            levelMetaDataGenerator.Regenerate();

            var expectedMetadataFile = _testingFilepath + GameConstants.Levels.MetadataFile;
            FileAssert.Exists(expectedMetadataFile);

            var metadata = ReadMetadataFromFile(expectedMetadataFile);
            Assert.NotNull(metadata);
            Assert.AreEqual(3, metadata.Levels.Count);

            CheckMetadata(level1, metadata.Levels[0]);
            CheckMetadata(level2, metadata.Levels[1]);
            CheckMetadata(level3, metadata.Levels[2]);
        }

        private void CheckMetadata(LevelSummary expectedLevel, LevelMetaResource actualMetadata)
        {
            Assert.NotNull(actualMetadata);
            Assert.AreEqual(expectedLevel.OrdinalNumber, actualMetadata.OrdinalNumber);
            Assert.AreEqual(expectedLevel.LevelName, actualMetadata.LevelName);

            var resourceName = ExportUtil.ConstructFilename(expectedLevel.OrdinalNumber);
            Assert.AreEqual(resourceName, actualMetadata.ResourceName);
        }

        private LevelMetadata ReadMetadataFromFile(string expectedMetadataFile)
        {
            using (var fileStream = File.Open(expectedMetadataFile, FileMode.Open))
            {
                var binaryFormatter = new BinaryFormatter();
                return (LevelMetadata)binaryFormatter.Deserialize(fileStream);
            }
        }

        private LevelSummary CreateEmptyLevel(int number, string name)
        {
            var levelSummary = new LevelSummary(number, name);
            string filename = _testingFilepath + ExportUtil.ConstructFilename(number);

            using (var fileStream = File.Open(filename, FileMode.Create))
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, levelSummary);
            }
            return levelSummary;
        }
    }
}