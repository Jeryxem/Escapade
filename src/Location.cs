namespace Escapade
{
  public class Location
  {
    int _x;
    int _y;

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
    #endregion Properties

    public Location (int x, int y)
    {
      X = x;
      Y = y;
    }

  }
}