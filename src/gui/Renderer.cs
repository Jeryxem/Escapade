using SwinGameSDK;

namespace Escapade.gui
{
  class Renderer
  {
    public void RenderView (GuiComponent c)
    {
      SwinGame.FillRectangle (Color.Azure, c.X, c.Y, c.Width, c.Height);
      SwinGame.DrawRectangle (c.Color, c.X, c.Y, c.Width, c.Height);
      if (c.GetType () == typeof (NamedFrame)) {
        NamedFrame cFrame = (NamedFrame)c;
        Rectangle inner = new Rectangle {
          X = cFrame.X + (cFrame.Width / 20),
          Y = cFrame.Y + (cFrame.Height / 12.5F),
          Width = cFrame.Width - (cFrame.Width / 10),
          Height = cFrame.Height - (cFrame.Height / 7.5F)
        };
        SwinGame.FillRectangle (Color.LightGrey, inner);
        SwinGame.DrawRectangle (cFrame.Color, inner);
        Bitmap fontBitmap = SwinGame.DrawTextToBitmap (Text.LoadFont("Arial", 9), cFrame.Name, Color.Black, Color.White);
        float fontWidth = cFrame.Width - (cFrame.Width / 10);
        float fontHeight = (cFrame.Height / 12.5F);
        SwinGame.DrawBitmap (fontBitmap, cFrame.X + ((cFrame.Width - fontBitmap.Width) / 2), cFrame.Y + ((fontHeight - fontBitmap.Height) / 2));
      }
    }
  }
}