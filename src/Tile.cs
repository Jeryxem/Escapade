using SwinGameSDK;
using System.Collections.Generic;

namespace Escapade
{
  public class Tile
  {
    
    TileType _type = TileType.Air;
    Mineral _mineral = null;

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
    #endregion Properties

    public Tile(TileType type)
    {
      Type = type;
    }

    public Tile() : this(TileType.Air) { }

  }
}