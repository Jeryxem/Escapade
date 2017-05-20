using System.Collections.Generic;

namespace Escapade.item
{
	public class Inventory
	{
		List<Item> _itemlist;

		public List<Item> ItemList {
			get {
        return _itemlist;
			}
		}

		public Inventory ()
		{
      _itemlist = new List<Item> ();
		}

    public void AddItem (Item i)
    {
      ItemList.Add (i);
    }

    public void RemoveItem (Item i)
    {
	    ItemList.Remove (i);
    }
	}
}
