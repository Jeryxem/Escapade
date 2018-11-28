using System;
using Escapade.item;
using Escapade.src.mineral;
using NUnit.Framework;

namespace Escapade
{
	// Tests by Isaac
	[TestFixture()]
	public class InventoryTest
	{
		[Test()]
		public void AddItemTest()
		{
			// Reminder: Diamond, emerald, ruby and sapphire objects are all Item objects as well.
			Inventory inventory = new Inventory();

			Assert.AreEqual(0, inventory.ItemList.Count);

			inventory.AddItem(new Diamond());
			inventory.AddItem(new Emerald());

			Assert.AreEqual(2, inventory.ItemList.Count);
		}

		[Test()]
		public void RemoveItemTest()
		{
			Inventory inventory2 = new Inventory();

			Sapphire sp = new Sapphire();
			inventory2.AddItem(sp);

			Assert.AreEqual(1, inventory2.ItemList.Count); // There's one item (the Sapphire object) in the Item List.

			inventory2.RemoveItem(sp);

			Assert.AreEqual(0, inventory2.ItemList.Count); // The list should be empty.
		}

		[Test()]
		public void ClearInventoryTest()
		{
			Inventory inventory3 = new Inventory();

			inventory3.AddItem(new Diamond());
			inventory3.AddItem(new Emerald());
			inventory3.AddItem(new Sapphire());
			inventory3.AddItem(new Ruby());

			Assert.AreEqual(4, inventory3.ItemList.Count); // All four minerals added above should reflect.

			inventory3.ClearInventory(); // Empty the Item List in one go.

			Assert.AreEqual(0, inventory3.ItemList.Count); // The Item List should now be empty.
		}

		[Test()]
		public void GetMineralPointsTest()
		{
			Inventory inventory4 = new Inventory();

			inventory4.AddItem(new Diamond()); // Worth 10 points
			inventory4.AddItem(new Diamond()); // 10 points
			inventory4.AddItem(new Emerald()); // 15 points
			inventory4.AddItem(new Ruby()); // 20 points
			inventory4.AddItem(new Sapphire()); // 25 points

			// Total amound of mineral poins in the Item List should be: 10 + 10 + 15 + 20 + 25 = 80;
			Assert.AreEqual(80, inventory4.GetMineralPoints());
		}

		[Test()]
		public void GetTotalValueTest()
		{
			Inventory inventory5 = new Inventory();

			inventory5.AddItem(new Diamond()); // 10 points
			inventory5.AddItem(new Diamond()); // 10 points
			inventory5.AddItem(new Emerald()); // 15 points
			inventory5.AddItem(new Ruby()); // 20 points
			inventory5.AddItem(new Sapphire()); // 25 points
			inventory5.AddItem(new Sapphire()); // 25 points

			// Total values by type: Diamonds (20 points), Emeralds (15 points), Rubies (20 points), Sapphires (50 points) 

			double[] expectedValueByType = { 20.00, 15.00, 20.00, 50.00 };

			Assert.AreEqual(expectedValueByType, inventory5.GetTotalValue());

		}

	}
}
