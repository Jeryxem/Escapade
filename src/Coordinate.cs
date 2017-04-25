using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escapade
{
  public class Coordinate
  {
    /// <summary>
    /// Create a new coordinate with the specified x and y values
    /// </summary>
    /// <param name="x">The x coordinate</param>
    /// <param name="y">The y coordinate</param>
    public Coordinate(int x, int y)
    {
      X = x;
      Y = y;
    }

    public int X { get; set; }
    public int Y { get; set; }
  }

  public class Location : Coordinate
  {
    public Location(int x, int y) : base(x, y)
    {
    }

    public Location Parent { get; set; }
    public int F { get; set; }
    public int G { get; set; }
    public int H { get; set; }

    public int GetH(Coordinate start, Coordinate target)
    {
      return Math.Abs(target.X - start.X) + Math.Abs(target.Y - start.Y);
    }

    public static double GetDistance(Coordinate c1, Coordinate c2)
    {
      return Math.Sqrt(Math.Pow(Math.Abs(c1.X - c2.X), 2) + Math.Pow(Math.Abs(c1.Y - c2.Y), 2));
    }
  }
}
