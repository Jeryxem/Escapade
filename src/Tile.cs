using SwinGameSDK;
using System.Collections.Generic;

namespace Escapade
{
  public class Tile
  {
    
    TileType _type = TileType.Empty;
    Mineral _mineral;
    BitmapMask _mask;
		int _locationX; 
		int _locationY; //testing -jeremy
		bool _collison;//added this - jeremy

    #region Properties
    public TileType Type {
      get {
        return _type;
      }
      set {
        _type = value;
      }
    }
    public Mineral Mineral {
      get {
        return _mineral;
      }
      set {
        _mineral = value;
      }
    }
    public BitmapMask Mask {
      get {
        return _mask;
      }
      set {
        _mask = value;
      }
    }
    #endregion Properties

    public Tile(TileType type, int locationX, int locationY)
    {
      Type = type;
      Mask = BitmapMask.None;
			_locationX = locationX;
			_locationY = locationY;
			_collison = false; //added this - jeremy
    }

		public bool Collision
		{
			get { return _collison; }
			set { _collison = value; } 
		}

		public int LocationX 
		{
			get { return _locationX;}
		}

		public int LocationY
		{
			get { return _locationY; } 
		}

    //public Tile() : this(TileType.Empty) { }

  }
}