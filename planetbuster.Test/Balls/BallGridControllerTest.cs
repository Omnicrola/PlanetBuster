using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Balls;
using Assets.Scripts.Core.Events;
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

        [SetUp]
        public void Setup()
        {
            UseSubstitueGameManager();
            _mockBallFactory = Substitute.For<IBallFactory>();
            _mockBallGrid = Substitute.For<IBallGrid>();
            _ballGridController = new BallGridController(_mockBallFactory, _mockBallGrid);
        }

        [Test]
        public void TestClearResetsGrid()
        {
            _ballGridController.Clear();
            _mockBallGrid.Received().Clear();
        }

        [Test]
        public void TestGenerate()
        {
            var ball1 = CreateSubstitueBall(0, 0, 0);
            var ball2 = CreateSubstitueBall(0, 1, 0);
            var ball3 = CreateSubstitueBall(0, 0, 1);
            var ball4 = CreateSubstitueBall(0, 1, 1);

            _mockBallGrid.Size.Returns(2);
            _mockBallFactory.GenerateBall(0, 0).Returns(ball1);
            _mockBallFactory.GenerateBall(1, 0).Returns(ball2);
            _mockBallFactory.GenerateBall(0, 1).Returns(ball3);
            _mockBallFactory.GenerateBall(1, 1).Returns(ball4);

            _ballGridController.Generate();

            var expectedBalls = new List<IBallController> { ball1, ball3, ball2, ball4 };
            _mockBallGrid.Received().Initialize(Matchers.EqualCollections(expectedBalls));
        }
        [Test]
        [TestCase(45, 6, 5)]
        [TestCase(134.999f, 6, 5)]
        [TestCase(135, 5, 4)]
        [TestCase(224.999f, 5, 4)]
        [TestCase(225, 4, 5)]
        [TestCase(314.999f, 4, 5)]
        public void TestBallCollision(float angleOfImpact, int expectedX, int expectedY)
        {
            var incomingBall = CreateSubstitueBall(1, int.MaxValue, int.MaxValue);
            var ballInGrid = CreateSubstitueBall(1, 5, 5);
            incomingBall.IsProjectile.Returns(true);

            var ballCollisionEventArgs = new BallCollisionEventArgs(incomingBall, ballInGrid, angleOfImpact);
            _ballGridController.CallPrivateMethod("OnBallCollision", null, ballCollisionEventArgs);

            _mockBallGrid.Received().Append(incomingBall, expectedX, expectedY);
        }
    }
}