using System.Reflection;
using Assets.Scripts.Balls;
using Assets.Scripts.Core;
using Assets.Scripts.Models;
using Assets.Scripts.Util;
using NSubstitute;
using NUnit.Framework;

namespace planetbuster.Test.TestUtil
{
    public abstract class TestBase
    {
        private ILogging _substituteLogging;
        private IGameManager _substitueGameManager;

        [TearDown]
        public void BaseTeardown()
        {
            RestoreLogging();
            RestoreGameManager();
        }

        private void RestoreGameManager()
        {
            if (_substitueGameManager != null)
            {
                _substitueGameManager = null;
                SetStaticField<GameManager>("_instance", _substitueGameManager);
            }
        }

        private void RestoreLogging()
        {
            if (_substituteLogging != null)
            {
                _substituteLogging = null;
                SetStaticField<Logging>("_instance", null);
            }
        }

        protected ILogging UseSubstituteLogging()
        {
            _substituteLogging = Substitute.For<ILogging>();
            SetStaticField<Logging>("_instance", _substituteLogging);
            return _substituteLogging;
        }

        protected IGameManager UseSubstitueGameManager()
        {
            _substitueGameManager = Substitute.For<IGameManager>();
            var eventBus = Substitute.For<IGameEventBus>();
            _substitueGameManager.EventBus.Returns(eventBus);

            SetStaticField<GameManager>("_instance", _substitueGameManager);
            return _substitueGameManager;
        }

        private static void SetStaticField<T>(string fieldName, object valueToSet)
        {
            var type = typeof(T);
            var fieldInfo = type.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Static);
            if (fieldInfo == null)
            {
                throw new AssertionException("Type " + type.Name + " does not have a field named " + fieldName);
            }
            fieldInfo.SetValue(null, valueToSet);
        }

        protected static IBallController CreateSubstitueBall(int type, int x, int y)
        {
            var ball1 = Substitute.For<IBallController>();
            ball1.GridPosition.Returns(new GridPosition(x, y));
            var ballModel = new BallModel()
            {
                Type = type
            };
            ball1.Model.Returns(ballModel);

            return ball1;
        }
    }
}