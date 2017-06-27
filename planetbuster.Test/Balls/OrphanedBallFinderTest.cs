using System;
using System.Collections.Generic;
using System.Linq;
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
            var ballControllers = new IBallController[5, 5];
            ballControllers[0, 0] = MakeBall();
            ballControllers[1, 0] = MakeBall();
            ballControllers[2, 0] = MakeBall();
            ballControllers[3, 0] = MakeBall();
            ballControllers[4, 0] = MakeBall();

            var orphanedBalls = _orphanedBallFinder.Find(ballControllers);
            Assert.AreEqual(0, orphanedBalls.Count);
        }

        [Test]
        public void TestBallsInAColumn_AreNotOrphaned()
        {
            var ballControllers = new IBallController[5, 5];
            ballControllers[1, 0] = MakeBall();
            ballControllers[1, 1] = MakeBall();
            ballControllers[1, 2] = MakeBall();
            ballControllers[1, 3] = MakeBall();

            var orphanedBalls = _orphanedBallFinder.Find(ballControllers);
            Assert.AreEqual(0, orphanedBalls.Count);
        }

        [Test]
        public void TestBallsInA_L_AreNotOrphaned()
        {
            var ballControllers = new IBallController[5, 5];
            ballControllers[2, 0] = MakeBall();
            ballControllers[2, 1] = MakeBall();
            ballControllers[1, 1] = MakeBall();
            ballControllers[1, 2] = MakeBall();

            var orphanedBalls = _orphanedBallFinder.Find(ballControllers);
            Assert.AreEqual(0, orphanedBalls.Count);
        }

        [Test]
        public void TestBallsDiagonal_AreOrphaned()
        {

            var ballControllers = new IBallController[5, 5];
            ballControllers[3, 0] = MakeBall();
            ballControllers[2, 1] = MakeBall();
            ballControllers[3, 2] = MakeBall();
            ballControllers[4, 3] = MakeBall();

            var orphanedBalls = _orphanedBallFinder.Find(ballControllers);

            Assert.AreEqual(3, orphanedBalls.Count);
            Assert.Contains(new GridPosition(2, 1), orphanedBalls);
            Assert.Contains(new GridPosition(3, 2), orphanedBalls);
            Assert.Contains(new GridPosition(4, 3), orphanedBalls);
        }

        private static IBallController MakeBall()
        {
            var ballController = Substitute.For<IBallController>();

            return ballController;
        }
    }
}