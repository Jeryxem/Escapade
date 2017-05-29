using System;
using System.Collections.Generic;
using System.Linq;
using SwinGameSDK;

namespace Escapade.gui
{
  public class Frame
  {
    Location _position;
    string _identifier;
    string _title;
    Rectangle _titlebar;
    Rectangle _contentarea;

    //Rectangle _hideicon;
    List<Button> _buttons;

    bool _visible;
    List<string> _content;

    public string Id {
      get {
        return _identifier;
      }
      private set {
        _identifier = value;
      }
    }
    public Rectangle Area {
      get {
        return new Rectangle {
          X = _titlebar.X,
          Y = _titlebar.Y,
          Width = _titlebar.Width,
          Height = _titlebar.Height + _contentarea.Height
        };
      }
    }
    public List<string> Content {
      set {
        _content = value;
      }
    }

    public Frame (string id, string title, Location xy, int width, int height)
    {
      _identifier = id;
      _title = title;
      _position = xy;
      _titlebar = new Rectangle {
        X = _position.X,
        Y = _position.Y,
        Width = width,
        Height = 15
      };
      /*_hideicon = new Rectangle {
        Width = 10,
        Height = 10,
        X = _position.X + width - (10 + (_titlebar.Width / 25)),
        Y = _position.Y + 2.5F
      };*/
      _contentarea = new Rectangle {
        X = _position.X,
        Y = _position.Y + _titlebar.Height,
        Width = width,
        Height = height - _titlebar.Height
      };
      _visible = false;
      _content = new List<string> ();
      _buttons = new List<Button> ();
    }

    /// <summary>
    /// Check whether the mouse was clicked in the frame's area
    /// </summary>
    public bool IsActive (Location l)
    {
      return _visible && SwinGame.PointInRect (new Point2D { X = l.X, Y = l.Y }, Area);
    }

    /// <summary>
    /// Adds a button to the frame
    /// </summary>
    /// <param name="b">The button to be added</param>
    public void AddButton (Color f, Color b, Action a)
    {
      float x = _buttons.Count == 0 ? _position.X + _titlebar.Width - 12.5F : _buttons.Last().X - 12.5F;
      float y = _position.Y + 2.5F;
      Button button = new Button (f, b, this, a);
      button.X = x;
      button.Y = y;
      _buttons.Add (button);
    }

    /// <summary>
    /// Adds a pre-existing button to the frame
    /// </summary>
    /// <param name="b">The button to add</param>
    public void AddButton (Button b)
    {
      _buttons.Add (b);
    }

    /// <summary>
    /// Gets the button the event occured at, returns null if it wasn't a button
    /// </summary>
    /// <returns>The button clicked | null</returns>
    /// <param name="l">The location of the event (e.g. mouse click)</param>
    public Button GetButton (Location l)
    {
      foreach (Button b in _buttons) {
        if (SwinGame.PointInRect (l.X, l.Y, b.Area)) return b;
      }
      return null;
    }

    /// <summary>
    /// Close the frame and make it invisible
    /// </summary>
    public void Close () { _visible = false; }

    /// <summary>
    /// Opens the frame and make it visible again
    /// </summary>
    public void Show () { _visible = true; }

    /// <summary>
    /// Toggles the visibility of the frame
    /// </summary>
    public void Toggle () { _visible = !_visible; }

    /// <summary>
    /// Draw the frame if it is visible
    /// </summary>
    public void Draw ()
    {
      if (_visible) {
        
        //draw title bar
        SwinGame.FillRectangle (Color.Azure, _titlebar);
        SwinGame.DrawRectangle (Color.Black, _titlebar);

        //draw buttons
        foreach (Button b in _buttons) {
          b.Draw ();
        }

        //draw content area
        SwinGame.FillRectangle (Color.Azure, _contentarea);
        SwinGame.DrawRectangle (Color.Black, _contentarea);

        //draw title string
        int fontWidth = GuiEnvironment.ArialFont.TextWidth (_title);
        int fontHeight = GuiEnvironment.ArialFont.TextHeight (_title);
        float txtXPos = _position.X + ((_titlebar.Height - fontHeight) / 2);
        float txtYPos = _position.Y + ((_titlebar.Height - fontHeight) / 2);
        SwinGame.DrawText (_title, Color.Black, GuiEnvironment.ArialFont, txtXPos, txtYPos);

        //draw the content in the content area
        DrawContent ();
      }
    }

    /// <summary>
    /// Draw the list of strings (content) in the content area of the frame
    /// </summary>
    public void DrawContent ()
    {
      float xPos = _contentarea.Left + 2.5F;
      float yPos = _contentarea.Top + 2.5F;
      foreach (string str in _content) {
        SwinGame.DrawText (str, Color.Black, GuiEnvironment.ArialFont, xPos, yPos);
        yPos += 12;
        if (yPos > _contentarea.Bottom - 12)
          break;
      }
    }
  }
}