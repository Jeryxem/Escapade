using Escapade.src.mineral.gemstone;
using System.Collections.Generic;

namespace Escapade.item
{
    public class Inventory
    {
        List<Item> _itemlist;
    List<Item> _itemToBeRemoved;
    private int _mineralPoints = 0;

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
      _itemToBeRemoved = new List<Item> ();
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

    public int MineralPoints 
    {
      get { return _mineralPoints; }
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

    public void DeductMineralPoints (int deduction)
    {
      _mineralPoints -= deduction;
    }

    }
}
