using System;
namespace Escapade
{
  public class Map
  {

    public enum Tile
    {
      EMPTY = -1,
      AIR = 0,
      WALL = 1,
      ROCK = 2,
      WATER = 3
    }

    private Tile[,] _grid;
    
    public Tile[,] Grid { get => _grid; set => _grid = value; }

    public Map (int w, int h)
    {
      _grid = new Tile[w, h];
    }

    /// <summary>
    /// Converts a coordinate in the region [0,h] to a cell in the region [0,h/cellsize]
    /// </summary>
    /// <param name="d">The dimension to convert</param>
    /// <returns></returns>
    public static int CoordToCell(int c)
    {
      return c / Game.Config["cellsize"];
    }

    /// <summary>
    /// Clears all tiles and replaces them with an empty map
    /// </summary>
    public void Clear()
    {
      for (int x = 0; x < Grid.GetLength(0); x++)
        for (int y = 0; x < Grid.GetLength(1); y++)
          Grid[x, y] = Tile.EMPTY;
    }
  }
}
