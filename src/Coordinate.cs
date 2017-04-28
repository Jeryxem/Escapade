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
}
