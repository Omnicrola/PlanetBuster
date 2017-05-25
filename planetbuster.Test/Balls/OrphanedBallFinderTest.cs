﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Assets.Scripts.Balls;
using Assets.Scripts.Models;
using Castle.DynamicProxy.Internal;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
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
                MakeBall(-3, 10),
                MakeBall(-2, 10),
                MakeBall(-1, 10),
                MakeBall(0, 10),
                MakeBall(1, 10),
                MakeBall(2, 10),
                MakeBall(3, 10),
            };

            var orphanedBalls = _orphanedBallFinder.Find(10, ballControllers);
            Assert.AreEqual(0, orphanedBalls.Count);
        }

        [Test]
        public void TestBallsInAColumn_AreNotOrphaned()
        {
            var topBall = MakeBall(2, 5);
            var ball1 = MakeBall(2, 4);
            var ball2 = MakeBall(2, 3);
            var ball3 = MakeBall(2, 2);

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

            var orphanedBalls = _orphanedBallFinder.Find(5, ballControllers);
            orphanedBalls.ForEach(b => Console.WriteLine(b.Model.GridX + " " + b.Model.GridY));
            Assert.AreEqual(0, orphanedBalls.Count);
        }

        private static IBallController MakeBall(int gridX, int gridY)
        {
            var ballController = Substitute.For<IBallController>();
            ballController.Model.Returns(new BallModel(gridX, gridY));
            return ballController;
        }
    }
}