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
        private IBallFactory _ballFactory;
        private BallGrid _ballGrid;
        private IGameManager _useSubstitueGameManager;

        [SetUp]
        public void Setup()
        {
            UseSubstituteLogging();
            _useSubstitueGameManager = UseSubstitueGameManager();
            _ballFactory = Substitute.For<IBallFactory>();
            _ballGrid = new BallGrid(_ballFactory, new OrphanedBallFinder(), null);
        }




    }
}