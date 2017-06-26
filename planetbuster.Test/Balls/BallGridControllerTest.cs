using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Balls;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Models;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using planetbuster.Test.TestUtil;

namespace planetbuster.Test.Balls
{
    [TestFixture]
    public class BallGridControllerTest : TestBase
    {
        private BallGridController _ballGridController;
        private IBallFactory _mockBallFactory;
        private IBallGrid _mockBallGrid;
        private IBallGridPositionCalculator _gridPositionCalculator;

        [SetUp]
        public void Setup()
        {
            UseSubstitueGameManager();
            _mockBallFactory = Substitute.For<IBallFactory>();
            _mockBallGrid = Substitute.For<IBallGrid>();
            _gridPositionCalculator = Substitute.For<IBallGridPositionCalculator>();
            _ballGridController = new BallGridController(_mockBallFactory, _mockBallGrid, _gridPositionCalculator);
        }

        [Test]
        public void TestClearResetsGrid()
        {
            _ballGridController.Clear();
            _mockBallGrid.Received().Clear();
        }

        [Test]
        [TestCase(45, 6, 5)]
        [TestCase(134.999f, 6, 5)]
        [TestCase(135, 5, 6)]
        [TestCase(224.999f, 5, 6)]
        [TestCase(225, 4, 5)]
        [TestCase(314.999f, 4, 5)]
        public void TestBallCollision(float angleOfImpact, int expectedX, int expectedY)
        {
            var incomingBall = CreateSubstitueBall(1, int.MaxValue, int.MaxValue);
            var ballInGrid = CreateSubstitueBall(1, 5, 5);
            incomingBall.IsProjectile.Returns(true);

            var ballCollisionEventArgs = new BallCollisionEventArgs(incomingBall, ballInGrid, angleOfImpact);
            _ballGridController.CallPrivateMethod("OnBallCollision", ballCollisionEventArgs);

            _mockBallGrid.Received().Append(incomingBall, new GridPosition(expectedX, expectedY));
        }
    }
}