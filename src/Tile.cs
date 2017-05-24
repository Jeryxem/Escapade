using SwinGameSDK;
using System.Collections.Generic;

namespace Escapade
{
  public class Tile
  {
    
    TileType _type = TileType.Air;
    Mineral _mineral;
    BitmapMask _mask;

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

    public Tile(TileType type)
    {
      Type = type;
      Mask = BitmapMask.None;
    }

    public Tile() : this(TileType.Air) { }

  }
}