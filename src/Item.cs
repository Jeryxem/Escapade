namespace Escapade.item
{
	public abstract class Item
	{
		string _id;
		int _meta;
		string _name;

		#region Properties
		public string Id {
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

		protected Item (string id, int meta, string name)
		{
			_id = id;
			Meta = meta;
			Name = name;
		}
	}
}
