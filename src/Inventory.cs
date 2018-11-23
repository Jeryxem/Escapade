using Escapade.src.mineral;
using System.Collections.Generic;

namespace Escapade.item
{
    public class Inventory
    {
        List<Item> _itemlist;
        List<Item> _itemToBeRemoved;

        public List<Item> ItemList
        {
            get
            {
                return _itemlist;
            }
        }

        public Inventory()
        {
            _itemlist = new List<Item>();
            _itemToBeRemoved = new List<Item>();
        }

        public void AddItem(Item i)
        {
            ItemList.Add(i);
            //JY- checks everytime it enters
            // CalculateMineralPoints ();
        }

        public void RemoveItem(Item i)
        {
            ItemList.Remove(i);
        }

        /// <summary>
        /// Return the total value of minerals.
        /// </summary>
        public double GetMineralPoints()
        {
            double mineralpoints = 0;
            foreach (Mineral mineral in _itemlist)
            {
                mineralpoints += mineral.Value;
            }
            return mineralpoints;
        }

        /// <summary>
        /// This method returns the total worth of minerals that the player has gathered - Added by Isaac
        /// </summary>
        /// <returns></returns>
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

        // Added by JY- to calculate the mineral points 
        /*
        public void CalculateMineralPoints ()
        {
          foreach (Mineral item in ItemList) 
          {
            if (item is Diamond) 
            {
              _mineralPoints += 10;
              _itemToBeRemoved.Add (item);
            } 
            else if (item is Emerald) 
            {
              _mineralPoints += 5;
              _itemToBeRemoved.Add(item);
            } 
            else if (item is Ruby) 
            {
              _mineralPoints += 3;
               _itemToBeRemoved.Add(item);
            } 
            else if (item is Sapphire) 
            {
              _mineralPoints += 1;
               _itemToBeRemoved.Add(item);
            }

          }
          foreach (Mineral mineral in _itemToBeRemoved) 
          {
            _itemlist.Remove (mineral);
          }
        }
            */

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
                weaponCost = 20;
            }
            
            for (int i = 0; i < _itemlist.Count; i++)
            {
                payableAmount += ((Mineral)_itemlist[i]).Value; // IA - cast to a Mineral if necessary
                _itemlist.RemoveAt(i); // IA - Remove the mineral in question
                if (payableAmount >= weaponCost)
                    break;
            }
        }
    }

}
