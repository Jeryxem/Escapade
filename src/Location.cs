namespace Escapade
{
  public class Location
  {
    int _x;
    int _y;
	bool _collison;//added this - jeremy

    #region Properties
    public int X {
      get {
        return _x;
      }
      set {
        _x = value;
      }
    }
    public int Y {
      get {
        return _y;
      }
      set {
        _y = value;
      }
    }
    #endregion Properties

    public Location (int x, int y)
    {
      X = x;
      Y = y;
			_collison = false; //added this - jeremy
    }

		public bool Collision
		{
			get { return _collison; }
			set { _collison = value; } 
		}


  }
}