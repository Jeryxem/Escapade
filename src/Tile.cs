using System;

namespace Escapade
{
  public class Tile
  {
    TileType _type;

    #region Properties
    public TileType Type {
      get {
        return _type;
      }
      set {
        _type = value;
      }
    }
    #endregion Properties

    public Tile (TileType type)
    {
      Type = type;
    }
  }
}