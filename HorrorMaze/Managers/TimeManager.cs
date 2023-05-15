using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrorMaze
{
    public class TimeManager
    {
        private TimeSpan _timer;

        // PROPERTIES
        public bool TimerIsReady { get; private set; }
        public int TargetMilliseconds { get; set; }
        private bool _timerRunning;


        public TimeManager(int targetMilliseconds, bool startTimerAsReady = false)
        {
            TimerIsReady = startTimerAsReady;
            TargetMilliseconds = targetMilliseconds;
            _timer = TimeSpan.Zero;
        }

        public int GetTimer()
        {
            return (int)_timer.TotalMilliseconds;
        }

        public void UpdateTimer()
        {
            if (_timerRunning)
                _timer += Globals.GameTime.ElapsedGameTime;
        }

        public void UpdateTimer(float speed)
        {
            _timer += TimeSpan.FromTicks((long)(Globals.GameTime.ElapsedGameTime.Ticks * speed));
        }

        public void AddToTimer(int milliseconds)
        {
            _timer += TimeSpan.FromMilliseconds(milliseconds);
        }

        public bool Test()
        {
            return _timer.TotalMilliseconds >= TargetMilliseconds || TimerIsReady;
        }

        public void Reset()
        {
            _timer -= TimeSpan.FromMilliseconds(TargetMilliseconds);
            _timer = _timer.TotalMilliseconds < 0 ? TimeSpan.Zero : _timer;
            TimerIsReady = false;
        }

        public void Reset(int newTargetMilliseconds)
        {
            _timer = TimeSpan.Zero;
            TargetMilliseconds = newTargetMilliseconds;
            TimerIsReady = false;
        }

        public void ResetToZero()
        {
            _timer = TimeSpan.Zero;
            TimerIsReady = false;
        }     

        public void SetTimer(TimeSpan time)
        {
            _timer = time;
        }

        public void SetTimer(int milliseconds)
        {
            _timer = TimeSpan.FromMilliseconds(milliseconds);
        }

        public void StopTimer()
        {
            _timerRunning = false;
        }

        public void StartTimer()
        {
            _timerRunning = true;
        }
    }
}
