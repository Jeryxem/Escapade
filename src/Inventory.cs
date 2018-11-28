using Escapade.src.gui;
using Escapade.src.mineral;
using System.Collections.Generic;

namespace Escapade.item
{
	/// <summary>
	/// This class holds all the fields and methods for the player's inventory, which holds items, including enemies and minerals.
	/// [Reworked on by Isaac]
	/// </summary>
    public class Inventory
    {
        List<Item> _itemlist;
        List<Item> _itemToBeRemoved;

		/// <summary>
		/// Gets the item list containing minerals.
		/// </summary>
		/// <value>The item list containing minerals.</value>
        public List<Item> ItemList
        {
            get
            {
                return _itemlist;
            }
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Escapade.item.Inventory"/> class containing two intialized Lists of Items.
		/// </summary>
        public Inventory()
        {
            _itemlist = new List<Item>();
            _itemToBeRemoved = new List<Item>();
        }

		/// <summary>
		/// Adds an Item object to the list containing minerals.
		/// </summary>
		/// <param name="i">The Item object to add to the list.</param>
        public void AddItem(Item i)
        {
            ItemList.Add(i);
            //JY- checks everytime it enters
            // CalculateMineralPoints ();
        }

		/// <summary>
		/// Removes and Item object from the list containing minerals.
		/// </summary>
		/// <param name="i">The Item object to remove from the list.</param>
        public void RemoveItem(Item i)
        {
            ItemList.Remove(i);
        }

		/// <summary>
		/// Clears the inventory's list of mineral items.
		/// </summary>
        public void ClearInventory()
        {
            _itemlist.Clear();
        }

        /// <summary>
        /// Return the total value of all minerals gathered. This must not be confused with the number of minerals gathered. 
		/// Only the total value (not the count) of minerals is returned.
        /// </summary>
        public int GetMineralPoints()
        {
            int mineralpoints = 0;
            foreach (Mineral mineral in _itemlist)
            {
                mineralpoints += mineral.Value;
            }
            return mineralpoints;
        }

        /// <summary>
        /// This method returns the total worth of minerals that the player has gathered, as a double array.
        /// </summary>
        /// <returns>A double array that the player can iterate through to get the exact amound of points accumulated per mineral type.</returns>
        public double[] GetTotalValue()
        {
            double diamondTotal, emeraldTotal, rubyTotal, sapphireTotal;
            diamondTotal = emeraldTotal = rubyTotal = sapphireTotal = 0;

            foreach (Mineral item in ItemList)
            {
                if (item is Diamond)
                    diamondTotal += item.Value;
                else if (item is Emerald)
                    emeraldTotal += item.Value;
                else if (item is Ruby)
                    rubyTotal += item.Value;
                else if (item is Sapphire)
                    sapphireTotal += item.Value;
            }

            double[] mineralTotalArray = { diamondTotal, emeraldTotal, rubyTotal, sapphireTotal };
            return mineralTotalArray;
        }

		/// <summary>
		/// Deducts available mineral points previously accumulated, to purchase a type of Weapon.
		/// </summary>
		/// <param name="type">The type of Weapon to purchase (must be of type WeaponType)</param>
        public void DeductMineralPoints(WeaponType type)
        {
            int weaponCost = 0;
            int payableAmount = 0;

            if (type == WeaponType.Normal)
            {
                weaponCost = 30;
            }
            else if (type == WeaponType.Super)
            {
                weaponCost = 50;
            }

            for (int i = 0; i < _itemlist.Count; i++)
            {
                payableAmount += ((Mineral)_itemlist[i]).Value; // IA - cast to a Mineral if necessary
                _itemlist.RemoveAt(i); // IA - Remove the mineral in question
                if (payableAmount >= weaponCost)
                    break;
            }
        }

        /// <summary>
        /// This method handles the deduction of mineral points and balance transfer, anytime the player makes a food purchase.
        /// </summary>
		/// <param name="foodValue">The food volume (in kg) converted into mineral points.</param>
        public void DeductPointsForFood(int foodValue) // IA - Method overloading
        {
            int balance = 0;
            int amount = Food.GetBalance();

            Food.DeductBalance(foodValue, this);

            for (int i = _itemlist.Count - 1;  i >= 0; i--)
            {
                if (amount < foodValue)
                {
                    amount += ((Mineral)_itemlist[i]).Value;
                    _itemlist.RemoveAt(i); // Remove the mineral from the list once its value has been consumed for a purchase
                } else
                {
                    if ((amount - foodValue) >= 1)
                    {
                        balance = amount - foodValue;
                        Food.SetBalance(balance); // Save the remaining points as balance for the player's next food purchase, if applicable.
                    }
                    break; // Exit once the food amount requested as been met;
                }
            }
        }
    }
}
