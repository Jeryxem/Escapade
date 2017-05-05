using SwinGameSDK;
using System;

namespace Escapade.src.world
{
  public class World
  {

    private Tile[,] _map;

    private Random random;
    public int Width { get; set; }
    public int Height { get; set; }

    public Tile[,] Map { get { return _map; } set { _map = value; } }

    public World(int width, int height)
    {
      Width = width;
      Height = height;
      Map = new Tile[Width, Height];
      random = new Random();
      GenerateMap();
    }

    public void GenerateMap()
    {
      FillMap();
      RandomFill();
      EvolveMap();
      CleanMap();
    }

    private void CleanMap()
    {
      for (int x = 0; x < Width; x++)
      {
        for (int y = 0; y < Height; y++)
        {
          if (GetNeighbours(x, y) <= 2) Map[x, y] = Tile.AIR;
        }
      }
    }

    private int GetNeighbours(int xP, int yP)
    {
      int res = 8;
      for (int x = -1; x <= 1; x++)
      {
        for (int y = -1; y <= 1; y++)
        {
          if (xP + x < 0 || xP + x >= Width || yP + y < 0 || yP + y >= Height || (x == 0 && y == 0))
            continue;
          if (Map[xP + x, yP + y] == Tile.AIR)
            res--;
        }
      }
      return res;
    }

    private int GetNeighbours(Location l) { return GetNeighbours(l.X, l.Y); }

    public void FillMap()
    {
      for (int x = 0; x < Width; x++)
      {
        for (int y = 0; y < Height; y++)
        {
          Map[x, y] = Tile.WALL;
        }
      }
    }

    public void RandomFill()
    {
      for (int x = 2; x < Width - 2; x++)
      {
        for (int y = 2; y < Height - 2; y++)
        {
          Map[x, y] = random.NextDouble() < 0.6 ? Tile.AIR : Tile.WALL;
        }
      }
    }

    public void EvolveMap()
    {
      Tile[,] newMap;

      for (int i = 0; i < 8; i++)
      {
        newMap = new Tile[Width, Height];

        for (int x = 0; x < Width; x++)
        {
          for (int y = 0; y < Height; y++)
          {
            int neighbours = GetNeighbours(x, y);
            switch (Map[x, y])
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
        Map = newMap;
      }
    }

    public Location RandomEmpty()
    {
      while (true)
      {
        int x = (int)Math.Floor(random.NextDouble() * Width);
        int y = (int)Math.Floor(random.NextDouble() * Height);
        if (Map[x, y] == Tile.AIR)
          return new Location(x, y);
      }
    }

    public void Draw()
    {
      for (int x = 0; x < Width; x++)
      {
        for (int y = 0; y < Height; y++)
        {
          switch (Map[x, y])
          {
            case Tile.AIR:
              SwinGame.FillRectangle(Color.LightGoldenrodYellow,
                Escapade.SIZE * x, Escapade.SIZE * y, Escapade.SIZE, Escapade.SIZE);
              SwinGame.DrawRectangle(Color.White, Escapade.SIZE * x,
                Escapade.SIZE * y, Escapade.SIZE, Escapade.SIZE);
              break;
            case Tile.WALL:
              SwinGame.FillRectangle(Color.DarkSlateGray, Escapade.SIZE * x,
                Escapade.SIZE * y, Escapade.SIZE, Escapade.SIZE);
              SwinGame.DrawRectangle(Color.White, Escapade.SIZE * x,
                Escapade.SIZE * y, Escapade.SIZE, Escapade.SIZE);
              break;
          }
        }
      }
    }

  }
}