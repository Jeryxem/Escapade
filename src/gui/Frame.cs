namespace Escapade.gui
{
  public class Frame
  {
    int _x;
    int _y;
    int _w;
    int _h;

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

    public Frame (int x, int y, int w, int h)
    {
      X = x;
      Y = y;
      Width = w;
      Height = h;
    }
  }
}
