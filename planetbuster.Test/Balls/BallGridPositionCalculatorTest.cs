using System;
using Assets.Scripts.Balls;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using planetbuster.Test.TestUtil;
using UnityEngine;

namespace planetbuster.Test.Balls
{
    [TestFixture]
    public class BallGridPositionCalculatorTest : TestBase
    {
        // http://www.redblobgames.com/grids/hexagons/
        // offset coordinate system : even-q vertical layout

        private BallGridPositionCalculator _ballGridPositionCalculator;

        [SetUp]
        public void Setup()
        {
            _ballGridPositionCalculator = new BallGridPositionCalculator();
        }

        public static object[] PositionCases = new object[]
        {
            new object[] {new GridPosition(0, 0), new Vector2(0.5f, -1f),},

            new object[] {new GridPosition(1, 0), new Vector2(1.25f, -0.5f),},
            new object[] {new GridPosition(2, 0), new Vector2(2f, -1f),},

            new object[] {new GridPosition(0, 1), new Vector2(0.5f, -2f),},
            new object[] {new GridPosition(0, 2), new Vector2(0.5f, -3f),},

            new object[] {new GridPosition(1, 1), new Vector2(1.25f, -1.5f),},

            new object[] {new GridPosition(25, 10), new Vector2(19.25f, -10.5f),},
            new object[] {new GridPosition(32, 91), new Vector2(24.5f, -92f),},
            new object[] {new GridPosition(31, 91), new Vector2(23.75f, -91.5f),},
        };

        [Test]
        [TestCaseSource(nameof(PositionCases))]
        public void TestGetWorldPosition(GridPosition gridPosition, Vector2 expectedPosition)
        {
            var ceilingOffset = Vector3.zero;
            var offset = Vector2.zero;
            var worldPosition = _ballGridPositionCalculator.GetWorldPosition(gridPosition, ceilingOffset, offset);

            var message = $"Expected : {gridPosition} => \n({expectedPosition.x}, {expectedPosition.y}) but was \n({worldPosition.x}, {worldPosition.y}) ";
            Assert.AreEqual(expectedPosition, worldPosition, message);
        }
    }
}