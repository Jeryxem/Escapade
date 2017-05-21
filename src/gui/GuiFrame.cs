using SwinGameSDK;

namespace Escapade.gui
{
  public class GuiFrame : GuiContainer
  {
    public GuiFrame (int xOffset, int yOffset, int width, int height, Color fgcolor, Color bgcolor, GuiContainer parent, bool visible)
    : base (xOffset, yOffset, width, height, fgcolor, bgcolor, parent, visible)
    {
    }

    public override void Draw ()
    {
      SwinGame.FillRectangle (BackgroundColor, XPos, YPos, Width, Height);
      SwinGame.DrawRectangle (ForegroundColor, XPos, YPos, Width, Height);
    }
  }
}
