using System;
using SwinGameSDK;
namespace Escapade.gui
{
  public class Button
  {
    float _xPos;
    float _yPos;
    Color _fgcolor;
    Color _bgcolor;
    Action _action;
    Frame _parent;

    /// <summary>
    /// Creates a new <see cref="T:Escapade.Button"/>
    /// </summary>
    /// <param name="fg">Foreground colour</param>
    /// <param name="bg">Background colour</param>
    /// <param name="p">The frame the button belongs to</param>
    /// <param name="a">The action done when the button is clicked</param>
    public Button (Color fg, Color bg, Frame p, Action a)
    {
      _fgcolor = fg;
      _bgcolor = bg;
      _parent = p;
      _action = a;
    }

    public Color Foreground {
    	get {
    		return _fgcolor;
    	}
    }
    public Color Background {
      get {
        return _bgcolor;
      }
    }
    public float X {
      get {
        return _xPos;
      }
      set {
        _xPos = value;
      }
    }
    public float Y {
    	get {
    		return _yPos;
    	}
      set {
        _yPos = value;
      }
    }
    public Rectangle Area {
      get {
        return new Rectangle {
          X = _xPos,
          Y = _yPos,
          Width = Width,
          Height = Height
        };
      }
    }
    public int Width {
      get {
        return 10;
      }
    }
    public int Height {
      get {
        return 10;
      }
    }

    /// <summary>
    /// Draw the button and its symbol at its location
    /// </summary>
    public void Draw ()
    {
      SwinGame.FillRectangle (_bgcolor, _xPos, _yPos, Width, Height);
      SwinGame.DrawRectangle (_fgcolor, _xPos, _yPos, Width, Height);
    }

    /// <summary>
    /// Perform the button's action - this is a basic astraction function
    /// </summary>
    public void Do ()
    {
      _action.Invoke ();
    }
  }
}
