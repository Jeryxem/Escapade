using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escapade.src.world
{
  public class Location
  {

    private int _x;
    private int _y;

    public int X { get { return _x; } set { _x = value; } }
    public int Y { get { return _y; } set { _y = value; } }

    public Location(int x, int y)
    {
      X = x;
      Y = y;
    }

    public static int Cell(float c)
    {
      return (int)c / Escapade.SIZE;
    }

  }
}
