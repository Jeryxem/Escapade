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
      WATER = 3,
      ORE = 4,
      CRATE = 5
    }

    private Random r = new Random();

    private Tile[,] _grid;

    public Tile[,] Grid { get => _grid; set => _grid = value; }

    public Map (int w, int h)
    {
      _grid = new Tile[w, h];

      Generate();
    }

    /// <summary>
    /// Converts a coordinate in the region [0,10h] to a cell in the region [0,h]
    /// </summary>
    /// <param name="c">The coordinate to convert</param>
    /// <returns></returns>
    public static int ToCell(int c)
    {
      return c / 10;
    }

    /// <summary>
    /// Converts a cell in the region [0,h] to a coordinate in the region [0,10h]
    /// </summary>
    /// <param name="c">The coordinate to convert</param>
    /// <returns></returns>
    public static int ToCoord(int c)
    {
      return 10 * c;
    }

    /// <summary>
    /// Clears all tiles and replaces them with an empty map
    /// </summary>
    public void Clear()
    {
      for (int x = 0; x < Game.Config["width"]; x++)
        for (int y = 0; y < Game.Config["height"]; y++)
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
      GenOres();
    }

    /// <summary>
    /// Random fill the map based on a threshold, and place a border around the map
    /// </summary>
    public void RandomFill()
    {
      for (int x = 0; x < Game.Config["width"]; x++)
      {
        for (int y = 0; y < Game.Config["height"]; y++)
        {
          if(x == 0 || x == Game.Config["width"] - 1 || y == 0 || y >= Game.Config["height"] - 1)
          {
            Grid[x, y] = Tile.WALL;
            continue;
          }
          double rand = r.NextDouble();
          Grid[x, y] = rand < 0.6 ? Tile.AIR : Tile.WALL; // Chance for tile to be air
        }
      }
    }

    /// <summary>
    /// Evolve the map {evolution} number of times based on cellular automata rules
    /// </summary>
    public void Evolve()
    {
      Tile[,] newMap;


      for (int i = 0; i < 5; i++) // Number of times to evolve
      {
        newMap = new Tile[Game.Config["width"], Game.Config["height"]];

        for (int x = 0; x < Game.Config["width"]; x++) // Loop through columns
        {
          for (int y = 0; y < Game.Config["height"]; y++) // Loop through rows
          {
            int neighbours = GetNeighbours(x, y);
            switch (Grid[x, y])
            {
              case Tile.WALL:
                newMap[x, y] = neighbours >= 4 ? Tile.WALL : (neighbours < 2 ? Tile.WALL : Tile.AIR);
                break;
              case Tile.AIR:
                newMap[x, y] = neighbours >= 5 ? Tile.WALL : Tile.AIR;
                break;
            }
          }
        }
        Grid = newMap;
      }
    }

    /// <summary>
    /// Removes most stray single tiles from the map, and converts the remaining ones to ore
    /// </summary>
    public void GenOres()
    {
      // Populate map with ores
      for (int x = 0; x < Game.Config["width"]; x++) // Loop through columns
      {
        for (int y = 0; y < Game.Config["height"]; y++) // Loop through rows
        {
          if (Grid[x, y] == Tile.WALL && GetNeighbours(x, y) >= 0 && GetNeighbours(x, y) <= 3)
            Grid[x, y] = r.NextDouble() < 0.8 ? Tile.AIR : Tile.ORE;
        }
      }

      // Cover ores in layer of rock
      for (int x = 0; x < Game.Config["width"]; x++) // Loop through columns
      {
        for (int y = 0; y < Game.Config["height"]; y++) // Loop through rows
        {
          if (Grid[x, y] == Tile.ORE)
          {
            for (int x2 = -1; x <= 1; x++) // Loop through columns
            {
              for (int y2 = -1; y <= 1; y++) // Loop through rows
              {
                Grid[x + x2, y + y2] = (Grid[x + x2, y + y2] == Tile.AIR) ? Tile.ROCK : Tile.WALL;
              }
            }
          }
        }
      }
    }

    /// <summary>
    /// Gets the number of alive tiles within a 1 tile radius of the specified cell
    /// This also counts non-existent tiles (tiles outside the map) as alive
    /// </summary>
    /// <param name="xP">The x coordinate of the cell</param>
    /// <param name="yP">The y coordinate of the cell</param>
    /// <returns>The number of alive neighbours (including non-existent tiles outside the map)</returns>
    public int GetNeighbours(int xP, int yP)
    {
      int res = 8;
      for (int x = -1; x <= 1 ; x++) // Loop through columns
      {
        for (int y = -1; y <= 1 ; y++) // Loop through rows
        {
          if (xP + x < 0 || xP + x >= Game.Config["width"] || yP + y < 0 || yP + y >= Game.Config["height"] || (x == 0 && y == 0)) // If the tile is out of bounds, don't decrement
            continue;
          if (Grid[xP + x, yP + y] == Tile.AIR) // If the neighbour is air, decrement the number of neighbours by 1
            res--;
        }
      }
      return res;
    }

    public void Draw()
    {
      for (int x = 0; x < Game.Config["width"]; x++)
      {
        for (int y = 0; y < Game.Config["height"]; y++)
        {
          switch (Grid[x,y])
          {
            case Tile.AIR:
              SwinGame.FillRectangle(Color.LightGoldenrodYellow, 10 * x, 10 * y, 10, 10);
              SwinGame.DrawRectangle(Color.White, 10 * x, 10 * y, 10, 10);
              break;
            case Tile.WALL:
              SwinGame.FillRectangle(Color.DarkSlateGray, 10 * x, 10 * y, 10, 10);
              SwinGame.DrawRectangle(Color.White, 10 * x, 10 * y, 10, 10);
              break;
            case Tile.ORE:
              SwinGame.FillRectangle(Color.RoyalBlue, 10 * x, 10 * y, 10, 10);
              SwinGame.DrawRectangle(Color.White, 10 * x, 10 * y, 10, 10);
              break;
            case Tile.ROCK:
              SwinGame.FillRectangle(Color.SandyBrown, 10 * x, 10 * y, 10, 10);
              SwinGame.DrawRectangle(Color.White, 10 * x, 10 * y, 10, 10);
              break;
            default:
              break;
          }
        }
      }
    }
  }
}
