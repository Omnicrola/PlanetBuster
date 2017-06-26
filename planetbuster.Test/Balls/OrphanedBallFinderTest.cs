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

            topBall.South.Add(ball1);
            ball1.South.Add(ball2);
            ball2.South.Add(ball3);
            ball3.South.Add(null);

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

            topBall.South.Add(ball1);
            ball1.North.Add(topBall);
            ball1.West.Add(ball2);
            ball2.East.Add(ball1);
            ball3.North.Add(ball2);
            ball2.South.Add(ball3);

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

            topBall.South.Clear();
            ball1.South.Clear();
            ball2.South.Clear();
            ball3.South.Clear();

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
            var north = new List<IBallController>();
            var south = new List<IBallController>();
            var east = new List<IBallController>();
            var west = new List<IBallController>();

            ballController.North.Returns(north);
            ballController.South.Returns(south);
            ballController.East.Returns(east);
            ballController.West.Returns(west);
            ballController.AllNeighbors.Returns((info) =>
            {
                return new List<IBallController>()
                    .Concat(north)
                    .Concat(south)
                    .Concat(east)
                    .Concat(west).ToList();
            });
            return ballController;
        }
    }
}