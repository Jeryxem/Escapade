namespace Escapade
{
  public class Frame
  {
    int _x;
    int _y;
    int _h;
    int _w;

    #region Properties
    public int X {
      get {
        return _x;
      }
      set {
        _x = value;
      }
    }
    public int Y {
      get {
        return _y;
      }
      set {
        _y = value;
      }
    }
    public int Height {
      get {
        return _h;
      }
      set {
        _h = value;
      }
    }
    public int Width {
      get {
        return _w;
      }
      set {
        _w = value;
      }
    }
    #endregion Properties

    public Frame (int x, int y, int h, int w)
    {
      X = x;
      Y = y;
      Height = h;
      Width = h;
    }
    public Frame (Location l, int h, int w) : this (l.X, l.Y, h, w) { }

  }
}
