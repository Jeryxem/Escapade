using System;
using SwinGameSDK;

namespace Escapade.gui
{
  public class Frame
  {
    Location _position;
    string _identifier;
    string _title;
    Rectangle _titlebar;
    Rectangle _hideicon;
    Rectangle _contentarea;
    bool _visible;

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

    public Frame (string id, string title, Location xy, int width, int height)
    {
      _identifier = id;
      _title = title;
      _position = xy;
      _titlebar = new Rectangle {
        X = _position.X,
        Y = _position.Y,
        Width = width,
        Height = height / 10
      };
      _hideicon = new Rectangle {
        Width = 10,
        Height = 10,
        X = _position.X + width - (10 - (_titlebar.Width / 20)),
        Y = _position.Y + _titlebar.Height - ((_titlebar.Height - 10) / 2),

      };
      _contentarea = new Rectangle {
        X = _position.X,
        Y = _position.Y + _titlebar.Height,
        Width = width,
        Height = height - _titlebar.Height
      };
      _visible = false;
    }

    /// <summary>
    /// Check whether the mouse was clicked in the frame's area
    /// </summary>
    public bool IsActive (Location l)
    {
      return SwinGame.PointInRect (new Point2D { X = l.X, Y = l.Y }, Area);
    }

    /// <summary>
    /// GuiAction enumeration. Used to determine what action to take on a gui
    /// </summary>
    public enum GuiAction
    {
      Content,
      Title,
      Icon
    }

    /// <summary>
    /// Gets the relative action based on where in the frame the location of the event occurs
    /// </summary>
    /// <returns>The appropriate action to take</returns>
    /// <param name="l">The location of the event (e.g. mouse click)</param>
    public GuiAction? GetAction (Location l)
    {
      if (SwinGame.PointInRect (l.X, l.Y, _hideicon)) return GuiAction.Icon;
      if (SwinGame.PointInRect (l.X, l.Y, _titlebar)) return GuiAction.Title;
      if (SwinGame.PointInRect (l.X, l.Y, _contentarea)) return GuiAction.Content;
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
    /// Draw the frame if it is visible
    /// </summary>
    public void Draw ()
    {
      if (_visible) {
        
        //draw title bar
        SwinGame.FillRectangle (Color.Azure, _titlebar);
        SwinGame.DrawRectangle (Color.Black, _titlebar);

        //draw hide icon
        SwinGame.FillRectangle (Color.Azure, _hideicon);
        SwinGame.DrawRectangle (Color.Black, _hideicon);
        SwinGame.DrawLine (Color.DarkRed, _hideicon.X + (_hideicon.Width / 4), _hideicon.Y + (_hideicon.Height / 4),
                           (_hideicon.X + _hideicon.Width) - (_hideicon.Width / 4), (_hideicon.Y + _hideicon.Height) - (_hideicon.Height / 4));
        SwinGame.DrawLine (Color.DarkRed, _hideicon.X + _hideicon.Width - (_hideicon.Width / 4), _hideicon.Y + (_hideicon.Height / 4),
                           _hideicon.X - (_hideicon.Width / 4), (_hideicon.Y + _hideicon.Height) - (_hideicon.Height / 4));

        //draw content area
        SwinGame.FillRectangle (Color.Azure, _contentarea);
        SwinGame.DrawRectangle (Color.Black, _contentarea);
      }
    }
 }
}