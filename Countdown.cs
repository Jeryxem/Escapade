using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace Escapade
{
    public class Countdown
    {
        private Timer timer;

        public Countdown()
        {
            timer = SwinGame.CreateTimer();
        }

        public Timer Timer
        {
            get { return timer; }
        }

        public void StartTimer()
        {
            timer.Start();
        }

        public void StopTimer()
        {
            timer.Stop();
        }

        public void ResetTimer()
        {
            timer.Reset();
        }

        public void PauseTimer()
        {
            timer.Pause();
        }

        public void ResumeTimer()
        {
            timer.Resume();
        }

        public long GetTimePassed()
        {
            return timer.Ticks;
        }

        public String ShowTime()
        {
            uint milliseconds = timer.Ticks;
            double seconds = milliseconds / 1000;
            double minute = seconds / 60;
            TimeSpan formatTime = TimeSpan.FromSeconds(seconds);

            // if (seconds > 59)
                // seconds = 0;

            // Display time elapsed in the format mm:ss
            String time = formatTime.ToString();

            return time;



        }
    }
}
