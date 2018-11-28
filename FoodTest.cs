using System;
using NUnit.Framework;

namespace Escapade
{
	// Tests by Isaac
	[TestFixture()]
	public class FoodTest
	{
		[Test()]
		public void IncreaseMineralValueTest()
		{
			// Reminder => The mineral value should be increased using the following formula: CurrentMineralValue + GameLevel / 1.5
			// ...with "GameLevel" referring to the current game level the player is in.

			Food.ResetMineralValue(); // Set the mineral value to the default value of 1.
			GameLevel.SetLevel(15); // Set the game to Level 15;
			Food.IncreaseMineralValue(); // Use the formula above

			// The new mineral value should be: 1 + 15 / 1.5 = 11.00
			// The GetMineralValue() method returns a double rounded off to 2 decimals.
			Assert.AreEqual(11.00, Food.GetMineralValue());

			// Reuse the obtained mineral value to increase again after changing the game level as well.
			GameLevel.SetLevel(9); // Level 9, now.
			Food.IncreaseMineralValue();

			// The new mineral value should be: 11 + 9 / 1.5 = 17.00
			Assert.AreEqual(17.00, Food.GetMineralValue());

			// The mineral value can no longer be increased once it is greater than 15.
			GameLevel.SetLevel(30); // Level 30, now.
			Food.IncreaseMineralValue();

			// The new mineral value should be: 17 + 30 / 1.5 = 37
			Assert.IsTrue(Food.GetMineralValue() == 17.00); // The value should be frozen to its previous state.
		}

		[Test()]
		public void IncreaseEnergyValueTest()
		{
			// Reminder => The formula to calculate the energy value is: CurrentEnergyValue + CurrentMineralValue - CurrentGameLevel;

			Food.ResetEnergyValue(); // Set the energy value to the default value of 2.
			Food.ResetMineralValue(); // Default is 1.
			GameLevel.SetLevel(1); // Level 1.

			// The energy value should be: 2 + 1 - 1 = 2
			// The GetEnergyValue() method returns a double rounded off to 2 decimals.
			Assert.AreEqual(2.00, Food.GetEnergyValue());

			// Increase the game level and test again.
			GameLevel.IncreaseLevel(); // Level 2, now.

			// A level increase changes the mineral value as well (see the IncreaseMineralValueTest() above)
			// The new energy value should be: 2 + 2 - 2 = 1;
			Assert.AreEqual(2.00, Food.GetEnergyValue());

		}

		[Test()]
		public void GetAndSetBalanceTest()
		{
			Food.SetBalance(10);
			Assert.AreEqual(10, Food.GetBalance());

			Food.SetBalance(4);
			Assert.AreEqual(4, Food.GetBalance());
		}

	}
}
