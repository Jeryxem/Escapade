using Escapade.src.mineral.gemstone;
using System.Collections.Generic;

namespace Escapade.item
{
    public class Inventory
    {
        List<Item> _itemlist;

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
        }

        public void AddItem(Item i)
        {
            ItemList.Add(i);
        }

        public void RemoveItem(Item i)
        {
            ItemList.Remove(i);
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
    }
}
