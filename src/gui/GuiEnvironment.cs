using SwinGameSDK;

namespace Escapade.gui
{
  public class GuiEnvironment
  {
    static GuiEnvironment _instance;
    static Renderer _renderer;

    GuiEnvironment ()
    {
    }

    public static GuiEnvironment GetInstance ()
    {
      if (_instance == null)
        _instance = new GuiEnvironment ();
      return _instance;
    }

    public static Renderer GetRenderer ()
    {
      if (_renderer == null)
        _renderer = new Renderer ();
      return _renderer;
    }

    public void HandleGuiEvent (GuiEvent e, Location l)
    {
      if (e == GuiEvent.MouseLeft) {
        Frame f = GetRenderer ().GetActiveFrame (l);
        if (f == null) {
           /*
            int x = (int)(SwinGame.MouseX () / Size);
            int y = (int)(SwinGame.MouseY () / Size);
            if (World.Map [x, y].Type == TileType.Air)
              ((Player)obj).NewPath (new Location (x, y));
              */
        }
      }
    }
  }
}
