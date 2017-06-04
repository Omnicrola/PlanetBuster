using System;
using System.Collections.Generic;
using System.Diagnostics;
using Assets.Scripts.Core;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using planetbuster.Test.TestUtil;

namespace planetbuster.Test.Core
{
    [TestFixture]
    public class UniversalEventBusTest : TestBase
    {
        private TestEventArgsOne _argsOneRecieved;
        private TestEventArgsTwo _argsTwoRecieved;

        [SetUp]
        public void Setup()
        {
            _argsOneRecieved = null;
            _argsTwoRecieved = null;
        }

        private void EventHandlerOne(TestEventArgsOne args)
        {
            _argsOneRecieved = args;
        }

        private void EventHandlerTwo(TestEventArgsTwo args)
        {
            _argsTwoRecieved = args;
        }

        [Test]
        public void TestBroadcastsToSubscribers()
        {
            var testEventArgsOne = new TestEventArgsOne();

            var universalEventBus = new UniversalEventBus();
            universalEventBus.Subscribe<TestEventArgsOne>(EventHandlerOne);

            universalEventBus.Broadcast(this, testEventArgsOne);

            Assert.AreEqual(testEventArgsOne, _argsOneRecieved);
        }


        [Test]
        public void TestUnsubscribes()
        {
            var testEventArgsOne = new TestEventArgsOne();

            var universalEventBus = new UniversalEventBus();
            universalEventBus.Subscribe<TestEventArgsOne>(EventHandlerOne);
            universalEventBus.Unsubscribe<TestEventArgsOne>(EventHandlerOne);

            universalEventBus.Broadcast(this, testEventArgsOne);

            Assert.IsNull(_argsOneRecieved);
        }

        [Test]
        public void TestSelectivelySendsMessages()
        {
            var testEventArgsOne = new TestEventArgsOne();
            var testEventArgsTwo = new TestEventArgsTwo();

            var universalEventBus = new UniversalEventBus();
            universalEventBus.Subscribe<TestEventArgsOne>(EventHandlerOne);
            universalEventBus.Subscribe<TestEventArgsTwo>(EventHandlerTwo);

            universalEventBus.Broadcast(this, testEventArgsOne);

            Assert.AreSame(testEventArgsOne, _argsOneRecieved);
            Assert.IsNull(_argsTwoRecieved);

            universalEventBus.Broadcast(this, testEventArgsTwo);
            Assert.AreSame(testEventArgsTwo, _argsTwoRecieved);
        }

        [Test]
        public void TestSpeed()
        {
            var eventCount = 1000;
            var subscriberCount = 1000;
            var universalEventBus = new UniversalEventBus();
            for (int i = 0; i < subscriberCount; i++)
            {
                var subscriber = new MockEventSubscriberOne();
                universalEventBus.Subscribe<TestEventArgsOne>(subscriber.Handle);
            }

            var events = new List<TestEventArgsOne>();

            for (int i = 0; i < eventCount; i++)
            {
                events.Add(new TestEventArgsOne());
            }

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            foreach (var testEventArgsOne in events)
            {
                universalEventBus.Broadcast(this, testEventArgsOne);
            }

            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            Assert.Less(stopwatch.ElapsedMilliseconds, 2500);
        }
    }

    internal class MockEventSubscriberOne
    {
        public TestEventArgsOne _argsRecieved;

        public void Handle(TestEventArgsOne args)
        {
            _argsRecieved = args;
        }
    }


    internal class TestEventArgsOne : EventArgs
    {
    }

    internal class TestEventArgsTwo : EventArgs
    {
    }
}