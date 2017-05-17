using System;
using System.Collections.Generic;

namespace Escapade
{
  public class Tile
  {
    TileType _type = TileType.Air;
    List<Mineral> _minerals = new List<Mineral>();

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

    public Tile(TileType type)
    {
      Type = type;
    }

    public Tile() : this(TileType.Air) { }
  }
}