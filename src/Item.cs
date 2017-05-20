namespace Escapade.item
{
	public abstract class Item
	{
		int _id;
		int _meta;
		string _name;

		#region Properties
		public int Id {
			get {
				return _id;
			}
			set {
				_id = value;
			}
		}
		public int Meta {
			get {
				return _meta;
			}
			set {
				_meta = value;
			}
		}
		public string Name {
			get {
				return _name;
			}
			set {
				_name = value;
			}
		}
		#endregion Properties

		public Item (int id, int meta, string name)
		{
			Id = id;
			Meta = meta;
			Name = name;
		}
	}
}
