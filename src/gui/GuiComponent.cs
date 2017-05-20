using SwinGameSDK;

namespace Escapade.gui
{
  public class GuiComponent
  {
    int _x;
    int _y;
    int _w;
    int _h;
    Color _color;
    GuiComponent _content;

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
    public Color Color {
      get {
        return _color;
      }
      set {
        _color = value;
      }
    }
    public GuiComponent Content {
      get {
        return _content;
      }
     private set {
        _content = value;
      }
    }
    #endregion Properties

    public GuiComponent (int x, int y, int w, int h, Color c)
    {
      X = x;
      Y = y;
      Width = w;
      Height = h;
      Color = c;
    }

    public void SetContent (GuiComponent component)
    {
      Content = component;
    }
  }
}
