﻿using System.Threading;
using NUnit.Framework;
using NUnit.Framework.Internal;
using SecondsTimer = Application.Misc.SecondsTimer;

namespace Application.Test.Unittests
{
    [TestFixture]
    public class TimerUnitTests
    {
        private SecondsTimer _uut;
        private int _tickCounter;

        [SetUp]
        public void SetUp()
        {
            _uut = new SecondsTimer();
            _tickCounter = 0;
        }

        [TearDown]
        public void TearDown()
        {
            _uut.Stop();
        }

        [TestCase(5, 5200)]
        [TestCase(10, 10200)]
        [TestCase(15, 15200)]
        public void Start_WaitLittleMoreThanSetTime_TickCounterIsSetTime(int secondsToWait, int msToWait)
        {
            _uut.TickEvent += (sender, args) => { _tickCounter++; };

            _uut.Start(secondsToWait);

            Thread.Sleep(msToWait);

            Assert.That(_tickCounter == secondsToWait, Is.EqualTo(true));
        }

        [TestCase(5, 4800)]
        [TestCase(10, 9800)]
        [TestCase(15, 14800)]
        public void Start_WaitLittleLessThanSetTime_TickCounterOneLessThanSetTime(int secondsToWait, int msToWait)
        {
            _uut.TickEvent += (sender, args) => { _tickCounter++; };

            _uut.Start(secondsToWait);

            Thread.Sleep(msToWait);

            Assert.That(_tickCounter == (secondsToWait - 1), Is.EqualTo(true));
        }

        [TestCase(5, 4800)]
        [TestCase(10, 9800)]
        [TestCase(15, 14800)]
        public void Start_WaitLittleLessThanSetTime_ExpiredEventNotCalled(int secondsToWait, int msToWait)
        {
            bool isCalled = false;
            _uut.ExpiredEvent += (sender, args) => { isCalled = true; };

            _uut.Start(secondsToWait);

            Thread.Sleep(msToWait);

            Assert.That(isCalled, Is.EqualTo(false));
        }

        [TestCase(5, 5200)]
        [TestCase(10, 10200)]
        [TestCase(15, 15200)]
        public void Start_WaitLittleMoreThanSetTime_ExpiredEventCalled(int secondsToWait, int msToWait)
        {
            bool isCalled = false;
            _uut.ExpiredEvent += (sender, args) => { isCalled = true; };

            _uut.Start(secondsToWait);

            Thread.Sleep(msToWait);

            Assert.That(isCalled, Is.EqualTo(true));
        }

    }
}