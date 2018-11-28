using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace Escapade
{
	/// <summary>
	/// This class holds all the methods and fields for the game's timer.
	/// </summary>
    public class Countdown
    {
		/// <summary>
		/// The timer that will be used to display the clock.
		/// </summary>
        private Timer timer;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Escapade.Countdown"/> class.
		/// </summary>
        public Countdown()
        {
            timer = SwinGame.CreateTimer();
        }

		/// <summary>
		/// Gets the timer.
		/// </summary>
		/// <value>The timer.</value>
        public Timer Timer
        {
            get { return timer; }
        }

		/// <summary>
		/// Starts the timer.
		/// </summary>
        public void StartTimer()
        {
            timer.Start();
        }

		/// <summary>
		/// Stops the timer.
		/// </summary>
        public void StopTimer()
        {
            timer.Stop();
        }

		/// <summary>
		/// Resets the timer.
		/// </summary>
        public void ResetTimer()
        {
            timer.Reset();
        }

		/// <summary>
		/// Pauses the timer.
		/// </summary>
        public void PauseTimer()
        {
            timer.Pause();
        }

		/// <summary>
		/// Resumes the timer.
		/// </summary>
        public void ResumeTimer()
        {
            timer.Resume();
        }

		/// <summary>
		/// Gets the time passed since the timer was started.
		/// </summary>
		/// <returns>The time passed in milliseconds.</returns>
        public long GetTimePassed()
        {
            return timer.Ticks;
        }

		/// <summary>
		/// Displays the time in HH:MM:SS format.
		/// </summary>
		/// <returns>The time passed as a string.</returns>
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
