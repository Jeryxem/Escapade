using SwinGameSDK;

namespace Escapade.gui
{
  public class NamedFrame : GuiComponent
  {
    string _name;

    #region Properties
    public string Name {
      get {
        return _name;
      }
      set {
        _name = value;
      }
    }
    #endregion Properties

    public NamedFrame (string name, Color c, int x, int y, int w, int h) : base (x, y, w, h, c)
    {
      Name = name;
    }
  }
}
