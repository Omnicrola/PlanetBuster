using System.Collections.Generic;
using Assets.Scripts.Balls;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Models;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using planetbuster.Test.TestUtil;
using UnityEngine;

namespace planetbuster.Test.Balls
{
    [TestFixture]
    public class BallGridTest : TestBase
    {
        private BallFactory _ballFactory;
        private BallGrid _ballGrid;
        private IGameManager _useSubstitueGameManager;

        [SetUp]
        public void Setup()
        {
            UseSubstituteLogging();
            _useSubstitueGameManager = UseSubstitueGameManager();
            _ballFactory = Substitute.For<BallFactory>(null, null, 0, null);
            _ballGrid = new BallGrid(3, _ballFactory);
        }

        [Test]
        public void TestInitialize_SetsNeighborsCorrectly()
        {
            var ball1 = CreateSubstitueBall(0, 0, 0);
            var ball2 = CreateSubstitueBall(0, 1, 0);
            var ball3 = CreateSubstitueBall(0, 2, 0);

            var ball4 = CreateSubstitueBall(0, 0, 1);
            var ball5 = CreateSubstitueBall(0, 1, 1);
            var ball6 = CreateSubstitueBall(0, 2, 1);

            var ball7 = CreateSubstitueBall(0, 0, 2);
            var ball8 = CreateSubstitueBall(0, 1, 2);
            var ball9 = CreateSubstitueBall(0, 2, 2);


            _ballGrid.Initialize(new List<IBallController> { ball1, ball2, ball3, ball4, ball5, ball6, ball7, ball8, ball9 });


            AssertNeighbors(ball1, ball4, null, ball2, null);
            AssertNeighbors(ball2, ball5, null, ball3, ball1);
            AssertNeighbors(ball3, ball6, null, null, ball2);

            AssertNeighbors(ball4, ball7, ball1, ball5, null);
            AssertNeighbors(ball5, ball8, ball2, ball6, ball4);
            AssertNeighbors(ball6, ball9, ball3, null, ball5);

            AssertNeighbors(ball7, null, ball4, ball8, null);
            AssertNeighbors(ball8, null, ball5, ball9, ball7);
            AssertNeighbors(ball9, null, ball6, null, ball8);

            AssertGridPosition(ball1, 0, 0);
            AssertGridPosition(ball2, 1, 0);
            AssertGridPosition(ball3, 2, 0);

            AssertGridPosition(ball4, 0, 1);
            AssertGridPosition(ball5, 1, 1);
            AssertGridPosition(ball6, 2, 1);

            AssertGridPosition(ball7, 0, 2);
            AssertGridPosition(ball8, 1, 2);
            AssertGridPosition(ball9, 2, 2);

        }

        private void AssertGridPosition(IBallController ball, int expectedX, int expectedY)
        {
            Assert.AreEqual(expectedX, ball.Model.GridX);
            Assert.AreEqual(expectedY, ball.Model.GridY);
        }

        [Test]
        public void TestAppend_SetsNeighborsCorrectly()
        {
            var ball1 = CreateSubstitueBall(0, 0, 0);
            var ball2 = CreateSubstitueBall(0, 1, 0);
            var ball3 = CreateSubstitueBall(0, 2, 0);

            var ball4 = CreateSubstitueBall(0, 0, 1);
            var ball5 = CreateSubstitueBall(0, 1, 1);
            var ball6 = CreateSubstitueBall(0, 2, 1);

            var ball7 = CreateSubstitueBall(0, 0, 2);
            var ball8 = CreateSubstitueBall(0, 1, 2);
            var ball9 = CreateSubstitueBall(0, 2, 2);

            var newBall = CreateSubstitueBall(5, int.MaxValue, int.MaxValue);

            _ballGrid.Initialize(new List<IBallController> { ball1, ball2, ball3, ball4, ball5, ball6, ball7, ball8, ball9 });
            _ballGrid.Append(newBall, 1, -1);

            Assert.AreEqual(10, _ballGrid.ActiveBalls);

            AssertNeighbors(newBall, ball2, null, null, null);
            AssertNeighbors(ball2, ball5, newBall, ball3, ball1);

            AssertGridPosition(newBall, 1, -1);
            AssertGridPosition(ball2, 1, 0);
        }

        private void AssertNeighbors(IBallController centerBall, IBallController north, IBallController south, IBallController east, IBallController west)
        {
            Assert.AreEqual(north, centerBall.Model.North);
            Assert.AreEqual(south, centerBall.Model.South);
            Assert.AreEqual(east, centerBall.Model.East);
            Assert.AreEqual(west, centerBall.Model.West);
        }
    }
}