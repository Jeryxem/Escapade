using System;
using SwinGameSDK;
using Escapade.src.mineral;
using Escapade.item;
using System.Collections.Generic;

namespace Escapade
{
  public class World
  {

    Tile [,] _map;
    Random random;


    #region Properties
    public int Width { get; set; }
    public int Height { get; set; }
    public int Size { get; set; }

    public Tile [,] Map {
      get {
        return _map;
      }
      set {
        _map = value;
      }
    }
    #endregion Properties

    public World (int width, int height, int size)
    {
      Width = width / size;
      Height = height / size;
      Size = size;
      random = new Random ();
      Map = new Tile [Width, Height];
      GenerateMap ();
    }

    /// <summary>
    /// Generate a new map with generation methods
    /// </summary>
    public void GenerateMap ()
    {
      FillMap ();
      RandomFill ();
      EvolveMap ();
      CleanMap ();
      PutMinerals ();
      CalcTileMasks ();
    }

    /// <summary>
    /// Fill the map with rock tiles
    /// </summary>
    void FillMap ()
    {
      for (int x = 0; x < Width; x++) {
        for (int y = 0; y < Height; y++) {
          Map [x, y] = new Tile (TileType.Rock);
        }
      }
    }

    /// <summary>
    /// Randomly fill the map with air and rock tiles
    /// leaving a 1 tile border around the map
    /// </summary>
    void RandomFill ()
    {
      for (int x = 1; x < Width - 1; x++) {
        for (int y = 1; y < Height - 1; y++) {
          Map [x, y].Type = random.NextDouble () < 0.6 ? TileType.Air : TileType.Rock;
        }
      }
    }

    /// <summary>
    /// Evolve the map with cellular automata rules
    /// </summary>
    void EvolveMap ()
    {
      Tile [,] newMap;

      for (int i = 0; i < 8; i++) {
        newMap = new Tile [Width, Height];

        for (int x = 0; x < Width; x++) {
          for (int y = 0; y < Height; y++) {
            int neighbours = GetNeighbours (x, y);
			newMap [x, y] = new Tile (TileType.Empty);
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

    /// <summary>
    /// Cleans the map and removes isolated groups of 1-2
    /// tiles to make the map look much cleaner
    /// </summary>
    void CleanMap ()
    {
      for (int x = 0; x < Width; x++) {
        for (int y = 0; y < Height; y++) {
          if (GetNeighbours (x, y) <= 2) Map [x, y].Type = TileType.Air;
        }
      }
      Location l = Escapade.GetPlayer ().Location;
      if (l != null) {
        Map [l.X, l.Y].Type = TileType.Air;
      }
    }

    /// <summary>
    /// Gets the number of rock tiles around the current location
    /// </summary>
    /// <returns>The number of rock tile neighbours</returns>
    /// <param name="p1">X coordinate</param>
    /// <param name="p2">Y coordinate</param>
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

    /// <summary>
    /// Randomly place some minerals in our world
    /// </summary>
    public void PutMinerals ()
    {
      for (int x = 0; x < Width; x++) {
        for (int y = 0; y < Height; y++) {
          if (Map [x, y].Type == TileType.Rock && Map [x, y].Mineral == null) {
            double rand = random.NextDouble ();
            Mineral mineral = null;
            if (rand < 0.05) {
              rand = random.NextDouble ();
              if (rand < 0.15)
                mineral = new Diamond ();
              else
                if (rand < 0.35)
                mineral = new Emerald ();
              else
                  if (rand < 0.65)
                mineral = new Ruby ();
              else
                mineral = new Sapphire ();
            }
            Map [x, y].Mineral = mineral;
          }
        }
      }
    }

    /// <summary>
    /// Finds a random air tile we can use to place an entity
    /// </summary>
    /// <returns>The empty.</returns>
    public Location RandomEmpty ()
    {
      while (true) {
        int x = (int)Math.Floor (random.NextDouble () * Width);
        int y = (int)Math.Floor (random.NextDouble () * Height);
        if (Map [x, y].Type == TileType.Air)
          return new Location (x, y);
      }
    }

    /// <summary>
    /// Modifies a tile at a specific location
    /// </summary>
    /// <param name="loc">The location of the tile being modified</param>
    public void ModifyTile (Location loc)
    {
      if (SwinGame.PointPointDistance (new Point2D { X = loc.X, Y = loc.Y }, new Point2D { X = Escapade.GetPlayer ().Location.X, Y = Escapade.GetPlayer ().Location.Y }) > 2) return;
      if (loc.X < 1 || loc.X >= Width - 1 || loc.Y < 1 || loc.Y >= Height - 1) return;
      Tile tile = Map [loc.X, loc.Y];
      if (tile.Type == TileType.Rock) {
				Map[loc.X, loc.Y] = new Tile(TileType.Air);
        if (tile.Mineral != null)
          Escapade.GetPlayer ().Inventory.AddItem (tile.Mineral);
      }
      if(tile.Type == TileType.Air && SwinGame.KeyDown(KeyCode.ShiftKey)) {
        Map [loc.X, loc.Y] = new Tile (TileType.Rock);
      }
      for (int x = -1; x <= 1; x++) {
        for (int y = -1; y <= 1; y++) {
          if (loc.X + x < 0 || loc.Y + y < 0 || loc.X + x > Width - 1 || loc.Y + y > Height - 1) continue;
          Map [loc.X + x, loc.Y + y].Mask = GetTileMask (loc.X + x, loc.Y + y);
        }
      }
    }

    /// <summary>
    /// Draws the map - called by the renderer - in a later version all
    /// drawing code will be handled by the renderer
    /// </summary>
    public void Draw ()
    {
      for (int x = 0; x < Width; x++) {
        for (int y = 0; y < Height; y++) {
          if (Map [x, y].Type == TileType.Air) {
            SwinGame.FillRectangle (Color.LightGoldenrodYellow, Size * x, Size * y, Size, Size);
            SwinGame.DrawRectangle (Color.White, Size * x, Size * y, Size, Size);
          } else {
            Color rockColour = null;
            if (x < 1 || x >= Width - 1 || y < 1 || y >= Height - 1) rockColour = Color.DarkRed;
            SwinGame.FillRectangle (rockColour ?? Color.Grey, Size * x, Size * y, Size, Size);
            SwinGame.DrawRectangle (Color.White, Size * x, Size * y, Size, Size);
          }
          if (Map [x, y].Mineral != null)
            SwinGame.FillCircle (Map [x, y].Mineral.Colour, (Size * x) + (float)(Size / 2), (Size * y) + (float)(Size / 2), Size / 7);
        }
      }
    }

    public void CalcTileMasks ()
    {
      for (int x = 0; x < Width; x++) {
        for (int y = 0; y < Height; y++) {
          Map [x, y].Mask = GetTileMask (x, y);
        }
      }
    }

    public BitmapMask GetTileMask (int x, int y)
    {
      BitmapMask mask =  BitmapMask.None;
      if (x < 0 || x >= Width || y < 0 || y >= Height) return BitmapMask.None;
      if (y > 0) {
        if (Map [x, y - 1].Type == TileType.Air) mask |= BitmapMask.North;
      }
      if (x<Width - 1) {
        if (Map [x + 1, y].Type == TileType.Air) mask |= BitmapMask.East;
      }
      if (y<Height - 1) {
        if (Map [x, y + 1].Type == TileType.Air) mask |= BitmapMask.South;
      }
      if (x > 0) {
        if (Map [x - 1, y].Type == TileType.Air) mask |= BitmapMask.West;
      }
      return mask;
    }
  }
}