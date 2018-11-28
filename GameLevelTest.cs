using System;
using NUnit.Framework;
namespace Escapade
{
	// Tests by Isaac
	[TestFixture()]
	public class GameLevelTest
	{
		// Reminder: The GameLevel class is a static class.
		[Test()]
		public void GetAndSetLevelTest()
		{
			GameLevel.SetLevel(2);
			Assert.AreEqual(2, GameLevel.GetLevel());

			GameLevel.SetLevel(25);
			Assert.AreEqual(25, GameLevel.GetLevel());
		}

		[Test()]
		public void GetFormulaTest()
		{
			// Reminder: The GameLevel class' formula = (LevelNo * 5) + 1 + (LevelNo * LevelNo)
			// ...with "LevelNo" referring to the player's current game level.

			GameLevel.SetLevel(3);

			// (3 * 5) + 1 + (3 * 3) = 15 + 1 + 9 = 25
			Assert.AreEqual(25, GameLevel.GetFormula());

			GameLevel.SetLevel(14);

			// (14 * 5) + 1 + (14 * 14) = 70 + 1 + 196 = 267
			Assert.AreEqual(267, GameLevel.GetFormula());
		}

		[Test()]
		public void IncreaseLevelTest()
		{
			GameLevel.SetLevel(1);
			GameLevel.IncreaseLevel(); // Increase the level by one.

			Assert.AreEqual(2, GameLevel.GetLevel());

			GameLevel.SetLevel(50);
			GameLevel.IncreaseLevel();
			GameLevel.IncreaseLevel();
			GameLevel.IncreaseLevel(); // Call it three times to get 53.

			Assert.AreEqual(53, GameLevel.GetLevel());
		}

		[Test()]
		public void ResetLevelTest()
		{
			GameLevel.SetLevel(10);
			GameLevel.ResetLevel(); // Set the level back to 1.

			Assert.AreEqual(1, GameLevel.GetLevel());
		}

		[Test()]
		public void PrintLevelTest()
		{
			GameLevel.SetLevel(10); // Level 10

			// The PrintLevel() method should return the level "10" as a string.
			StringAssert.AreEqualIgnoringCase("10", GameLevel.PrintLevel());
		}
	}
}
