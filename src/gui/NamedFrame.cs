namespace Escapade.gui
{
  public class NamedFrame : Frame
  {
    string _name;
    int _containX;
    int _containY;

    #region Properties
    public string Name {
      get {
        return _name;
      }
      set {
        _name = value;
      }
    }
    public int CX {
      get {
        return _containX;
      }
      set {
        _containX = value;
      }
    }
    public int CY {
      get {
        return _containY;
      }
      set {
        _containY = value;
      }
    }
    #endregion Properties

    public NamedFrame (string name, int x, int y, int w, int h) : base (x, y, w, h)
    {
      Name = name;
    }
  }
}
