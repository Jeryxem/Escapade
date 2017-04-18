using SwinGameSDK;
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

      Generate();
    }

    /// <summary>
    /// Converts a coordinate in the region [0,h] to a cell in the region [0,h/cellsize]
    /// </summary>
    /// <param name="c">The dimension to convert</param>
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
      for (int x = 0; x < CoordToCell(Game.Config["width"]); x++)
        for (int y = 0; y < CoordToCell(Game.Config["height"]); y++)
          Grid[x, y] = Tile.EMPTY;
    }

    /// <summary>
    /// Generates a new map
    /// </summary>
    public void Generate()
    {
      Clear();
      RandomFill();
      Evolve();
    }

    /// <summary>
    /// Random fill the map based on a threshold, and place a border around the map
    /// </summary>
    public void RandomFill()
    {
      Random r = new Random();
      for (int x = 0; x < CoordToCell(Game.Config["width"]); x++)
      {
        for (int y = 0; y < CoordToCell(Game.Config["height"]); y++)
        {
          if(x == 0 || x == CoordToCell(Game.Config["width"]) - 1 || y == 0 || y >= CoordToCell(Game.Config["height"]) - 1)
          {
            Grid[x, y] = Tile.WALL;
            continue;
          }
          double rand = r.NextDouble();
          Grid[x, y] = rand < 0.4 ? Tile.AIR : Tile.WALL; // Chance for tile to be air
        }
      }
    }

    /// <summary>
    /// Evolve the map {evolution} number of times based on cellular automata rules
    /// </summary>
    public void Evolve()
    {
      for (int i = 0; i < 4; i++) // Number of times to evolve
      {
        for (int x = 0; x < CoordToCell(Game.Config["width"]); x++) // Loop through columns
        {
          for (int y = 0; y < CoordToCell(Game.Config["height"]); y++) // Loop through rows
          {

          }
        }
      }
    }

    /// <summary>
    /// Gets the number of alive tiles within a 1 tile radius of the specified cell
    /// This also counts non-existent tiles (tiles outside the map) as alive
    /// </summary>
    /// <returns>The number of alive neighbours (including non-existent tiles outside the map)</returns>
    public int GetAliveNeighbours()
    {
      return 0;
    }

    public void Draw()
    {
      for (int x = 0; x < CoordToCell(Game.Config["width"]); x++)
      {
        for (int y = 0; y < CoordToCell(Game.Config["height"]); y++)
        {
          int cellsize = Game.Config["cellsize"];
          switch (Grid[x,y])
          {
            case Tile.AIR:
              SwinGame.FillRectangle(Color.LightGoldenrodYellow, cellsize * x, cellsize * y, cellsize, cellsize);
              SwinGame.DrawRectangle(Color.White, cellsize * x, cellsize * y, cellsize, cellsize);
              break;
            case Tile.WALL:
              SwinGame.FillRectangle(Color.DarkSlateGray, cellsize * x, cellsize * y, cellsize, cellsize);
              SwinGame.DrawRectangle(Color.White, cellsize * x, cellsize * y, cellsize, cellsize);
              break;
            default:
              break;
          }
        }
      }
    }
  }
}
