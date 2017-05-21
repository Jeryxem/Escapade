using SwinGameSDK;

namespace Escapade.gui
{
  public abstract class GuiElement
  {
    int _xOffset;
    int _yOffset;
    int _width;
    int _height;
    Color _fgcolor;
    Color _bgcolor;
    GuiContainer _parent;
    bool _visible;

    public int XPos {
      get {
        int xPos = XOffset + (Parent != null ? Parent.XPos : 0);
        return xPos;
      }
    }
    public int YPos {
    	get {
        int yPos = YOffset + (Parent != null ? Parent.YPos : 0);
        return yPos;
    	}
    }
    public int XOffset {
      get {
        return _xOffset;
      }
      set {
        _xOffset = value;
      }
    }
    public int YOffset {
      get {
        return _yOffset;
      }
      set {
        _yOffset = value;
      }
    }
    public int Width {
      get {
        return _width;
      }
      set {
        _width = value;
      }
    }
    public int Height {
      get {
        return _height;
      }
      set {
        _height = value;
      }
    }
    public Color ForegroundColor {
      get {
        return _fgcolor;
      }
      set {
        _fgcolor = value;
      }
    }
    public Color BackgroundColor {
      get {
        return _bgcolor;
      }
      set {
        _bgcolor = value;
      }
    }
    public GuiContainer Parent {
    	get {
    		return _parent;
    	}
    	set {
    		_parent = value;
    	}
    }
    public bool Visible {
      get {
        return _visible;
      }
      set {
        _visible = value;
      }
    }

    protected GuiElement (int xOffset, int yOffset, int width, int height, Color fgcolor, Color bgcolor, GuiContainer parent, bool visible)
    {
      XOffset = xOffset;
      YOffset = yOffset;
      Width = width;
      Height = height;
      ForegroundColor = fgcolor;
      BackgroundColor = bgcolor;
      Parent = parent;
      if(Parent != null)
        Parent.AddChild (this);
      Visible = visible;
    }

    public virtual void Render ()
    {
      if(Visible)
        Draw ();
    }

    public abstract void Draw ();
  }
}
