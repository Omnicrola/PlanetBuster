using System.IO;
using Assets.Editor;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using planetbuster.Test.TestUtil;

namespace planetbuster.Test.Editor
{
    [TestFixture]
    public class LevelMetadataGeneratorTest
    {
        [Test]
        public void TestRegenerate()
        {
            string path = "Test_Metadata/";
            Directory.CreateDirectory(path);

            var levelMetaDataGenerator = new LevelMetaDataGenerator(path);
            levelMetaDataGenerator.Regenerate();

            var expectedMetadataFile = path + "meta.bin";
            FileAssert.Exists(expectedMetadataFile);
        }
    }
}