using System;
using SwinGameSDK;

namespace Escapade
{
  public class World
  {

    Tile [,] _map;
    Instance _instance;
    Random random;

    #region Properties
    public int Width { get; set; }
    public int Height { get; set; }

    public Instance Instance {
      get {
        return _instance;
      }
      set {
        _instance = value;
      }
    }
    public Tile [,] Map {
      get {
        return _map;
      }
      set {
        _map = value;
      }
    }
    #endregion Properties

    public World (int width, int height, int size, Instance instance)
    {
      Width = width / size;
      Height = height / size;
      Instance = instance;
      random = new Random ();
      Map = new Tile [Width, Height];
      GenerateMap ();
    }

    void GenerateMap ()
    {
      FillEmpty ();
      RandomFill ();
      EvolveMap ();
      CleanMap ();
    }

    void FillEmpty ()
    {
      for (int x = 0; x < Width; x++) {
        for (int y = 0; y < Height; y++) {
          Map [x, y] = new Tile (TileType.Air);
        }
      }
    }

    void RandomFill ()
    {
      for (int x = 2; x < Width - 2; x++) {
        for (int y = 2; y < Height - 2; y++) {
          Map [x, y].Type = random.NextDouble () < 0.6 ? TileType.Air : TileType.Rock;
        }
      }
    }

    void EvolveMap ()
    {
      Tile [,] newMap;

      for (int i = 0; i < 8; i++) {
        newMap = new Tile [Width, Height];

        for (int x = 0; x < Width; x++) {
          for (int y = 0; y < Height; y++) {
            int neighbours = GetNeighbours (x, y);
            switch (Map [x, y].Type) {
            case TileType.Rock:
              newMap [x, y].Type = neighbours >= 4 ? TileType.Rock : (neighbours < 2 ? TileType.Rock : TileType.Air);
              break;
            case TileType.Air:
              newMap [x, y].Type = neighbours >= 5 ? TileType.Rock : TileType.Air;
              break;
            }
          }
        }
        Map = newMap;
      }
    }

    void CleanMap ()
    {
      for (int x = 0; x<Width; x++)
      {
        for (int y = 0; y<Height; y++)
        {
          if (GetNeighbours (x, y) <= 2) Map [x, y].Type = TileType.Air;
        }
      }
    }

    int GetNeighbours (int p1, int p2)
    {
      int res = 8;
      for (int x = -1; x <= 1; x++) {
        for (int y = -1; y <= 1; y++) {
          if (p1 + x < 0 || p1 + x >= Width || p2 + y < 0 || p2 + y >= Height || (x == 0 && y == 0))
            continue;
          if (Map [p1 + x, p2 + y].Type == TileType.Air)
            res--;
        }
      }
      return res;
    }

    public Location RandomEmpty ()
    {
      while (true) {
        int x = (int) Math.Floor (random.NextDouble () * Width);
        int y = (int) Math.Floor (random.NextDouble () * Height);
        if (Map [x, y].Type == TileType.Air)
          return new Location (x, y);
      }
    }

    public void Update ()
    {
      //TODO World Update Method
    }

    public void Draw ()
    {
      for (int x = 0; x < Width; x++) {
        for (int y = 0; y < Height; y++) {
          int size = Instance.Size;
          Color color = Map [x, y].Type == TileType.Air ? Color.LightGoldenrodYellow : Color.SlateGray;
          SwinGame.FillRectangle (color, size * x, size * y, size, size);
          SwinGame.DrawRectangle (Color.White, size * x, size * y, size, size);
        }
      }
    }

  }
}