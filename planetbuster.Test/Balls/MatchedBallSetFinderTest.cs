using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Assets.Scripts.Balls;
using Assets.Scripts.Models;
using NUnit.Framework;
using planetbuster.Test.TestUtil;

namespace planetbuster.Test.Balls
{
    [TestFixture]
    public class MatchedBallSetFinderTest : TestBase
    {

        [Test]
        public void TestEmptyGrid_OnlyMatchesTarget()
        {
            var matchedBallSetFinder = new MatchedBallSetFinder();
            var ballControllers = new IBallController[5, 5];

            var targetPosition = new GridPosition(0, 0);
            var ball1 = CreateSubstitueBall(BallType.Red, 0, 0);
            ballControllers[0, 0] = ball1;

            var matches = matchedBallSetFinder.FindPath(targetPosition, ballControllers);
            Assert.AreEqual(1, matches.Count);
            Assert.AreSame(ball1, matches[0]);
        }

        [Test]
        public void TestMatches_ThreeInARow()
        {
            var matchedBallSetFinder = new MatchedBallSetFinder();
            var ballControllers = CreateGridFilledWithType(BallType.Blue, 5);

            var targetPosition = new GridPosition(1, 0);
            var ball1 = CreateSubstitueBall(BallType.Green, 1, 0);
            var ball2 = CreateSubstitueBall(BallType.Green, 2, 0);
            var ball3 = CreateSubstitueBall(BallType.Green, 3, 0);
            ballControllers[1, 0] = ball1;
            ballControllers[2, 0] = ball2;
            ballControllers[3, 0] = ball3;

            var matches = matchedBallSetFinder.FindPath(targetPosition, ballControllers);
            Assert.AreEqual(3, matches.Count);
            Assert.Contains(ball1, matches);
            Assert.Contains(ball2, matches);
            Assert.Contains(ball3, matches);
        }

        [Test]
        public void TestMatches_ThreeInAColumn()
        {
            var matchedBallSetFinder = new MatchedBallSetFinder();
            var ballControllers = CreateGridFilledWithType(BallType.Blue, 5);

            var targetPosition = new GridPosition(0, 1);
            var ball1 = CreateSubstitueBall(BallType.Green, 0, 1);
            var ball2 = CreateSubstitueBall(BallType.Green, 0, 2);
            var ball3 = CreateSubstitueBall(BallType.Green, 0, 3);
            ballControllers[0, 1] = ball1;
            ballControllers[0, 2] = ball2;
            ballControllers[0, 3] = ball3;

            var matches = matchedBallSetFinder.FindPath(targetPosition, ballControllers);
            Assert.AreEqual(3, matches.Count);
            Assert.Contains(ball1, matches);
            Assert.Contains(ball2, matches);
            Assert.Contains(ball3, matches);
        }

        [Test]
        public void TestDoesNotMatch_ThreeDiagonally()
        {
            var matchedBallSetFinder = new MatchedBallSetFinder();
            var ballControllers = CreateGridFilledWithType(BallType.Blue, 5);

            var targetPosition = new GridPosition(1, 1);
            var ball1 = CreateSubstitueBall(BallType.Green, 1, 1);
            var ball2 = CreateSubstitueBall(BallType.Green, 2, 2);
            var ball3 = CreateSubstitueBall(BallType.Green, 3, 3);
            ballControllers[1, 1] = ball1;
            ballControllers[2, 2] = ball2;
            ballControllers[3, 3] = ball3;

            var matches = matchedBallSetFinder.FindPath(targetPosition, ballControllers);
            Assert.AreEqual(1, matches.Count);
            Assert.Contains(ball1, matches);
        }

        [Test]
        public void TestMatches_FiveInAn_L()
        {
            var matchedBallSetFinder = new MatchedBallSetFinder();
            var ballControllers = CreateGridFilledWithType(BallType.Blue, 5);

            var targetPosition = new GridPosition(1, 1);
            var ball1 = CreateSubstitueBall(BallType.Green, 1, 1);
            var ball2 = CreateSubstitueBall(BallType.Green, 1, 2);
            var ball3 = CreateSubstitueBall(BallType.Green, 1, 3);
            var ball4 = CreateSubstitueBall(BallType.Green, 2, 3);
            var ball5 = CreateSubstitueBall(BallType.Green, 3, 3);
            ballControllers[1, 1] = ball1;
            ballControllers[1, 2] = ball2;
            ballControllers[1, 3] = ball3;
            ballControllers[2, 3] = ball4;
            ballControllers[3, 3] = ball5;

            var matches = matchedBallSetFinder.FindPath(targetPosition, ballControllers);
            Assert.AreEqual(5, matches.Count);
            Assert.Contains(ball1, matches);
            Assert.Contains(ball2, matches);
            Assert.Contains(ball3, matches);
            Assert.Contains(ball4, matches);
            Assert.Contains(ball5, matches);
        }

        [Test]
        public void TestAlgorithmTime_100SquareGrid()
        {
            var gridFilledWithType = CreateGridFilledWithType(BallType.Blue, 30);
            var matchedBallSetFinder = new MatchedBallSetFinder();
            var times = new List<long>();

            for (int i = 0; i < 10; i++)
            {
                times.Add(TimeMatching(matchedBallSetFinder, gridFilledWithType));
            }

            var average = times.Average();
            Console.WriteLine("AVG : " + average);
            Assert.Less(average, 250, "Parsing an entire grid of ~1000 balls should take less than .25 seconds. " +
                                      "Which is the worst that could happen, since most matches should be perhaps dozens?");
        }

        private static long TimeMatching(MatchedBallSetFinder matchedBallSetFinder,
            IBallController[,] gridFilledWithType)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            matchedBallSetFinder.FindPath(new GridPosition(1, 1), gridFilledWithType);
            stopwatch.Stop();
            var elapsed = stopwatch.ElapsedMilliseconds;
            return elapsed;
        }


        private static IBallController[,] CreateGridFilledWithType(BallType type, int gridSize)
        {
            var gridFilledWithType = new IBallController[gridSize, gridSize];
            for (int x = 0; x < gridFilledWithType.GetLength(0); x++)
            {
                for (int y = 0; y < gridFilledWithType.GetLength(1); y++)
                {
                    gridFilledWithType[x, y] = CreateSubstitueBall(type, x, y);
                }
            }
            return gridFilledWithType;
        }
    }
}