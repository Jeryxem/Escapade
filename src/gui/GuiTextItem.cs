using System;
using SwinGameSDK;

namespace Escapade.gui
{
  public class GuiTextItem : GuiElement
  {
    Bitmap _image;

    public Bitmap Image {
      get {
        return _image;
      }
      set {
        _image = value;
      }
    }

    public GuiTextItem (string text, Color fgcolor, GuiContainer parent, bool visible) : this(text, fgcolor, parent.BackgroundColor, parent, visible)
    {
      
    }

    public GuiTextItem (string text, Color fgcolor, Color bgcolor, GuiContainer parent, bool visible) : base(1, 1, parent.Width - 2, parent.Height - 2, fgcolor, bgcolor, parent, visible)
    {
      int fontSize = 20;
      Font font;
      while (true) {
        font = new Font ("Arial", fontSize);
        int fontHeight = font.TextHeight (text);
        int fontWidth = font.TextWidth (text);
        if (fontWidth < Width && fontHeight < Height)
          break;
        fontSize--;
      }
      Image = SwinGame.DrawTextToBitmap (font, text, fgcolor, bgcolor);
    }

    public override void Draw ()
    {
      SwinGame.DrawBitmap (Image, Parent.XPos + (((Parent.Width - (2 * XOffset)) / 2) - (Image.Width / 2)), Parent.YPos + (((Parent.Height - (2 * YOffset)) / 2) - (Image.Height / 2)));
    }
  }
}
