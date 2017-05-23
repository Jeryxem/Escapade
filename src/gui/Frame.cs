using System;
using System.Collections.Generic;
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
    List<string> _content;

    Font _font = new Font ("Arial", 9);

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
      _hideicon = new Rectangle {
        Width = 10,
        Height = 10,
        X = _position.X + width - (10 + (_titlebar.Width / 25)),
        Y = _position.Y + 2.5F
      };
      _contentarea = new Rectangle {
        X = _position.X,
        Y = _position.Y + _titlebar.Height,
        Width = width,
        Height = height - _titlebar.Height
      };
      _visible = false;
      _content = new List<string> ();
    }

    /// <summary>
    /// Check whether the mouse was clicked in the frame's area
    /// </summary>
    public bool IsActive (Location l)
    {
      return _visible && SwinGame.PointInRect (new Point2D { X = l.X, Y = l.Y }, Area);
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

        //draw hide icon
        SwinGame.FillRectangle (Color.Azure, _hideicon);
        SwinGame.DrawRectangle (Color.Black, _hideicon);
        SwinGame.DrawLine (Color.DarkRed, _hideicon.X + (_hideicon.Width / 4), _hideicon.Y + (_hideicon.Height / 4),
                           (_hideicon.X + _hideicon.Width) - (_hideicon.Width / 4), (_hideicon.Y + _hideicon.Height) - (_hideicon.Height / 4));
        SwinGame.DrawLine (Color.DarkRed, _hideicon.X + _hideicon.Width - (_hideicon.Width / 4), _hideicon.Y + (_hideicon.Height / 4),
                           _hideicon.X + (_hideicon.Width / 4), (_hideicon.Y + _hideicon.Height) - (_hideicon.Height / 4));

        //draw content area
        SwinGame.FillRectangle (Color.Azure, _contentarea);
        SwinGame.DrawRectangle (Color.Black, _contentarea);

        //draw title string
        int fontWidth = _font.TextWidth (_title);
        int fontHeight = _font.TextHeight (_title);
        float txtXPos = _position.X + ((_titlebar.Height - fontHeight) / 2);
        float txtYPos = _position.Y + ((_titlebar.Height - fontHeight) / 2);
        SwinGame.DrawText (_title, Color.Black, _font, txtXPos, txtYPos);

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
        SwinGame.DrawText (str, Color.Black, _font, xPos, yPos);
        yPos += 12;
        if (yPos > _contentarea.Bottom - 12)
          break;
      }
    }
  }
}