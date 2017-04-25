using Escapade;
using SwinGameSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escapade
{
  public class Pathfinder
  {
    private Map map;

    public Pathfinder(Map m)
    {
      map = m;
    }

    public List<Coordinate> GetPath(Coordinate s, Coordinate t)
    {
      List<Coordinate> resultpath = new List<Coordinate>();
      Location current = null;
      Location start = new Location(s.X, s.Y);
      Location target = new Location(t.X, t.Y);
      var open = new List<Location>();
      var closed = new List<Location>();
      int g = 0;

      //add start pos to open list
      open.Add(start);

      while(open.Count > 0)
      {
        //pathfinding algorithm

        //find the tile with the lowest F
        int lowest = open.Min(x => x.F);
        current = open.First(x => x.F == lowest);

        //remove the current tile from the open list and add it to the closed list
        closed.Add(current);
        open.Remove(current);

        //if the target exists in the closed list, we've arrived
        if (closed.FirstOrDefault(x => x.X == target.X && x.Y == target.Y) != null) break;

        List<Location> adjacent = GetValidAdjacents(current, map);
        g++;

        //loop through the adjacent tiles
        foreach(Location loc in adjacent)
        {
          //if current adjacent location is in close list, we can ignore it
          if (closed.FirstOrDefault(l => l.X == loc.X && l.Y == loc.Y) != null) continue;

          //if location isn't in our open locations list
          if(open.FirstOrDefault(l => l.X == loc.X && l.Y == loc.Y) == null)
          {
            loc.G = g;
            loc.H = HScore(start, target);
            loc.F = loc.G + loc.H;
            loc.Parent = current;

            //insert this tile into the start of the open list
            open.Insert(0, loc);
          } else
          {
            //check whether using the g from here makes the tile's score better
            if(g+loc.G < loc.F)
            {
              loc.G = g;
              loc.F = loc.F + loc.H;
              loc.Parent = current;
            }
          }
        }
        resultpath.Add(closed.Last());
      }
      return resultpath;
    }

    private static List<Location> GetValidAdjacents(Coordinate c, Map m)
    {
      List<Location> res = new List<Location>();
      for (int x = -1; x <= 1; x++)
        for (int y = -1; y <= 1; y++)
          if (m.Grid[c.X + x, c.Y + y] == Tile.AIR)
            if (x == 0 && y == 0)
              continue;
            else
              res.Add(new Location(c.X + x, c.Y + y));
      return res;
    }

    /// <summary>
    /// Calculates the horizontal and vertical distance between two coordinates
    /// </summary>
    /// <param name="s">The start coordinate</param>
    /// <param name="t">The target coordinate</param>
    /// <returns>The sum of the two distances</returns>
    private static int HScore(Coordinate s, Coordinate t)
    {
      return Math.Abs(t.X - s.X) + Math.Abs(t.Y - s.Y);
    }
  }
}
