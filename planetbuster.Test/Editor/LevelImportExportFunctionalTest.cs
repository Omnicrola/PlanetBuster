using System;
using System.IO;
using Assets.Editor;
using Assets.Scripts;
using Assets.Scripts.LevelEditor;
using Assets.Scripts.Models;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using planetbuster.Test.TestUtil;

namespace planetbuster.Test.Editor
{
    [TestFixture]
    public class LevelImportExportFunctionalTest : TestBase
    {
        private Random _random;

        [SetUp]
        public void Setup()
        {
            _random = new Random();
            Directory.CreateDirectory(GameConstants.Levels.RelativeResourcePath);
        }

        [TearDown]
        public void Teardown()
        {
            Directory.Delete(GameConstants.Levels.RelativeResourcePath, true);
        }

        [Test]
        public void TestReadWriteData()
        {
            var gridEditorSettingsForExport = Substitute.For<IGridEditorSettings>();
            var gridEditorSettingsForImport = Substitute.For<IGridEditorSettings>();

            LevelSummary expectedLevelSummary = new LevelSummary(59, "the level name");
            for (int i = 0; i < 10; i++)
            {
                expectedLevelSummary.BallData.Add(CreateRandomBallData());
            }

            gridEditorSettingsForExport.GetExportData().Returns(expectedLevelSummary);
            LevelSummary actualLevelSummary = null;
            gridEditorSettingsForImport
                .When(e => e.SetLevelData(Arg.Any<LevelSummary>()))
                .Do(x => actualLevelSummary = x.Arg<LevelSummary>());

            var levelExporter = new LevelExporter(gridEditorSettingsForExport);
            var levelImporter = new LevelImporter(gridEditorSettingsForImport);

            levelExporter.Export();
            var expectedFile = GameConstants.Levels.RelativeResourcePath + "level-" + expectedLevelSummary.OrdinalNumber +
                               ".bin";
            FileAssert.Exists(expectedFile);

            var successfullyImported = levelImporter.Import(expectedFile);
            Assert.True(successfullyImported);

            Assert.NotNull(actualLevelSummary);
            Assert.AreEqual(expectedLevelSummary.LevelNumber, actualLevelSummary.LevelNumber);
            Assert.AreEqual(expectedLevelSummary.OrdinalNumber, actualLevelSummary.OrdinalNumber);
            CollectionAssert.AreEquivalent(expectedLevelSummary.BallData, actualLevelSummary.BallData);
        }

        private BallLevelData CreateRandomBallData()
        {
            return new BallLevelData
            {
                BallType = (BallType)_random.Next(0, 4),
                HasPowerGem = _random.Next(0, 100) < 50,
                Magnitude = BallMagnitude.Large,
                XPos = _random.Next(0, 1000),
                YPos = _random.Next(0, 1000)
            };
        }
    }
}