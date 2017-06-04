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
        [Test]
        public void TestBroadcastsToSubscribers()
        {
            var mockEventSubscriberOne = new MockEventSubscriberOne();
            var testEventArgsOne = new TestEventArgsOne();

            var universalEventBus = new UniversalEventBus();
            universalEventBus.Subscribe<TestEventArgsOne>(mockEventSubscriberOne);

            universalEventBus.Broadcast(this, testEventArgsOne);

            Assert.AreEqual(testEventArgsOne, mockEventSubscriberOne.ArgsOneReceived);
            Assert.AreEqual(this, mockEventSubscriberOne._sourceOneRecieved);
        }


        [Test]
        public void TestBroadcastsToSubscribers_ExtendedClass()
        {
            var mockEventSubscriberTwo = new MockEventSubscriberTwo();
            var testEventArgsOne = new TestEventArgsOne();

            var universalEventBus = new UniversalEventBus();
            universalEventBus.Subscribe<TestEventArgsOne>(mockEventSubscriberTwo);

            universalEventBus.Broadcast(this, testEventArgsOne);

            Assert.AreEqual(testEventArgsOne, mockEventSubscriberTwo.ArgsOneReceived);
            Assert.AreEqual(this, mockEventSubscriberTwo._sourceOneRecieved);
        }

        [Test]
        public void TestUnsubscribes()
        {
            var mockEventSubscriberOne = new MockEventSubscriberOne();
            var testEventArgsOne = new TestEventArgsOne();

            var universalEventBus = new UniversalEventBus();
            universalEventBus.Subscribe<TestEventArgsOne>(mockEventSubscriberOne);
            universalEventBus.Unsubscribe<TestEventArgsOne>(mockEventSubscriberOne);

            universalEventBus.Broadcast(this, testEventArgsOne);

            Assert.IsNull(mockEventSubscriberOne.ArgsOneReceived);
            Assert.IsNull(mockEventSubscriberOne._sourceOneRecieved);
        }

        [Test]
        public void TestSelectivelySendsMessages()
        {
            var mockEventSubscriberOne = new MockEventSubscriberOne();
            var mockEventSubscriberTwo = new MockEventSubscriberTwo();
            var testEventArgsOne = new TestEventArgsOne();
            var testEventArgsTwo = new TestEventArgsTwo();

            var universalEventBus = new UniversalEventBus();
            universalEventBus.Subscribe<TestEventArgsOne>(mockEventSubscriberOne);
            universalEventBus.Subscribe<TestEventArgsTwo>(mockEventSubscriberTwo);

            universalEventBus.Broadcast(this, testEventArgsOne);
            universalEventBus.Broadcast(this, testEventArgsTwo);

            Assert.AreSame(testEventArgsOne, mockEventSubscriberOne.ArgsOneReceived);
            Assert.IsNull(mockEventSubscriberOne.ArgsTwoReceived);

            Assert.AreSame(testEventArgsTwo, mockEventSubscriberTwo.ArgsTwoReceived);
            Assert.IsNull(mockEventSubscriberTwo.ArgsOneReceived);
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
                universalEventBus.Subscribe<TestEventArgsOne>(subscriber);
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
        public TestEventArgsOne ArgsOneReceived;
        public TestEventArgsTwo ArgsTwoReceived;
        public object _sourceOneRecieved;
        public object _sourceTwoRecieved;

        private void OnEventTwo(object source, TestEventArgsTwo eventArgs)
        {
            _sourceTwoRecieved = source;
            ArgsTwoReceived = eventArgs;
        }

        private void OnEventOne(object source, TestEventArgsOne eventArgs)
        {
            _sourceOneRecieved = source;
            ArgsOneReceived = eventArgs;
        }
    }

    internal class MockEventSubscriberTwo : MockEventSubscriberOne
    {
    }

    internal class TestEventArgsOne : EventArgs
    {
        public int eventNumber;
    }

    internal class TestEventArgsTwo : EventArgs
    {
        public int eventNumber;
    }
}