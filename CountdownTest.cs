using System;
using SwinGameSDK;
using NUnit.Framework;

namespace Escapade
{
	// Tests by Isaac
	[TestFixture()]
	public class CountdownTest
	{
		[Test]
		public void StartTimerTest()
		{
			// Ensure that the timer can start as expected.
			Countdown myTimer = new Countdown();

			Assert.IsTrue(myTimer.GetTimePassed() == 0); // Creating a Countdown object should not start the timer automatically.

			myTimer.StartTimer(); // Start the timer
			SwinGame.Delay(100); // Delay for 100 milliseconds.

			// The GetTimePassed() method returns the time passed in milliseconds since the timer was started, so it must now return a number greater than 0.
			Assert.IsTrue(myTimer.GetTimePassed() > 0);
			Assert.IsTrue(myTimer.GetTimePassed() >= 100); // At least 100 milliseconds should be registered.
		}

		[Test()]
		public void ResetTimerTest()
		{
			Countdown myTimer2 = new Countdown();

			myTimer2.StartTimer();
			SwinGame.Delay(2000); // Make 2 seconds pass after the timer has started.

			Assert.IsTrue(myTimer2.GetTimePassed() >= 2000); // Reflect that 2 seconds have passed.

			myTimer2.ResetTimer(); // Reset the timer

			// The timer's ticks should start over again.
			Assert.IsTrue(myTimer2.GetTimePassed() < 100);
			        
		}

		[Test()]
		public void ShowTimeTest()
		{
			Countdown myTimer3 = new Countdown();

			// Creating a timer does not automatically start the clock, so the clock should reflect that when displayed as a string.
			StringAssert.AreEqualIgnoringCase("00:00:00", myTimer3.ShowTime());

			myTimer3.StartTimer();
			SwinGame.Delay(1000); // Make a second pass after starting the clock.

			// The clock should now reflect that one second has passed.
			StringAssert.AreEqualIgnoringCase("00:00:01", myTimer3.ShowTime());
		}
	}
}
