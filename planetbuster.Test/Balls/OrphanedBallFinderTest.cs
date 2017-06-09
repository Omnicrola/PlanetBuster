using System;
using System.Collections.Generic;
using Assets.Scripts.Balls;
using Assets.Scripts.Models;
using NSubstitute;
using NUnit.Framework;
using planetbuster.Test.TestUtil;

namespace planetbuster.Test.Balls
{
    [TestFixture]
    public class OrphanedBallFinderTest : TestBase
    {
        private OrphanedBallFinder _orphanedBallFinder;

        [SetUp]
        public void Setup()
        {
            _orphanedBallFinder = new OrphanedBallFinder();
        }

        [Test]
        public void TestBallsOnCeiling_AreNotOrphaned()
        {
            var ballControllers = new List<IBallController>
            {
                MakeBall(-3, 0),
                MakeBall(-2, 0),
                MakeBall(-1, 0),
                MakeBall(0, 0),
                MakeBall(1, 0),
                MakeBall(2, 0),
                MakeBall(3, 0),
            };

            var orphanedBalls = _orphanedBallFinder.Find(ballControllers);
            Assert.AreEqual(0, orphanedBalls.Count);
        }

        [Test]
        public void TestBallsInAColumn_AreNotOrphaned()
        {
            var topBall = MakeBall(2, 0);
            var ball1 = MakeBall(2, 1);
            var ball2 = MakeBall(2, 2);
            var ball3 = MakeBall(2, 3);

            topBall.Model.South = ball1;
            ball1.Model.South = ball2;
            ball2.Model.South = ball3;
            ball3.Model.South = null;

            var ballControllers = new List<IBallController>
            {
                topBall,
                ball1,
                ball2,
                ball3,
            };

            var orphanedBalls = _orphanedBallFinder.Find(ballControllers);
            orphanedBalls.ForEach(b => Console.WriteLine(b.Model.GridX + " " + b.Model.GridY));
            Assert.AreEqual(0, orphanedBalls.Count);
        }

        [Test]
        public void TestBallsInA_L_AreNotOrphaned()
        {
            var topBall = MakeBall(2, 0);
            var ball1 = MakeBall(2, 1);
            var ball2 = MakeBall(1, 1);
            var ball3 = MakeBall(1, 2);

            topBall.Model.South = ball1;
            ball1.Model.North = topBall;
            ball1.Model.West = ball2;
            ball2.Model.East = ball1;
            ball3.Model.North = ball2;
            ball2.Model.South = ball3;

            var ballControllers = new List<IBallController>
            {
                topBall,
                ball1,
                ball2,
                ball3,
            };

            var orphanedBalls = _orphanedBallFinder.Find(ballControllers);
            orphanedBalls.ForEach(b => Console.WriteLine(b.Model.GridX + " " + b.Model.GridY));
            Assert.AreEqual(0, orphanedBalls.Count);
        }

        [Test]
        public void TestBallsDiagonal_AreOrphaned()
        {
            var topBall = MakeBall(3, 0);
            var ball1 = MakeBall(2, 1);
            var ball2 = MakeBall(3, 2);
            var ball3 = MakeBall(4, 1);

            topBall.Model.South = null;
            ball1.Model.South = null;
            ball2.Model.South = null;
            ball3.Model.South = null;

            var ballControllers = new List<IBallController>
            {
                topBall,
                ball1,
                ball2,
                ball3,
            };

            var orphanedBalls = _orphanedBallFinder.Find(ballControllers);

            Assert.AreEqual(3, orphanedBalls.Count);
            Assert.Contains(ball1, orphanedBalls);
            Assert.Contains(ball2, orphanedBalls);
            Assert.Contains(ball3, orphanedBalls);
        }

        private static IBallController MakeBall(int gridX, int gridY)
        {
            var ballController = Substitute.For<IBallController>();
            ballController.Model.Returns(new BallModel(gridX, gridY));
            return ballController;
        }
    }
}