﻿using System;
using System.Timers;
using Application.Interfaces;


namespace Application.Misc
{
    public class SecondsTimer : ITimer
    {
        public event EventHandler TickEvent;
        public event EventHandler ExpiredEvent;

        private readonly System.Timers.Timer _timeoutTimer;
        private int _timeRemainingInSeconds;

        public SecondsTimer()
        {
            _timeoutTimer = new System.Timers.Timer();

            _timeoutTimer.Elapsed += new ElapsedEventHandler(OnTimerEvent);
            _timeoutTimer.Interval = 1000;
            _timeoutTimer.AutoReset = true;
        }
        public void Start(int seconds)
        {
            _timeRemainingInSeconds = seconds;
            _timeoutTimer.Enabled = true;
        }

        public void Stop()
        {
            _timeoutTimer.Enabled = false;
        }

        private void Expire()
        {
            _timeoutTimer.Enabled = false;
            ExpiredEvent?.Invoke(this, EventArgs.Empty);
        }

        private void OnTimerEvent(object sender, ElapsedEventArgs args)
        {
            _timeRemainingInSeconds -= 1;
            TickEvent?.Invoke(this, EventArgs.Empty);

            if(_timeRemainingInSeconds <= 0)
                Expire();               
        }
    }
}