using System.Collections.Generic;
using System.Linq;

namespace Escapade.gui
{
  public class GuiEnvironment
  {
    static GuiEnvironment _environment;
    static Renderer _renderer;

    GuiEnvironment ()
    {
    }

    /// <summary>
    /// Gets the game GuiEnvironment
    /// </summary>
    /// <returns>Game GuiEnvironment</returns>
    public static GuiEnvironment GetEnvironment ()
    {
      if (_environment == null)
        _environment = new GuiEnvironment ();
      return _environment;
    }

    /// <summary>
    /// Gets the active game renderer
    /// </summary>
    /// <returns>The renderer</returns>
    public static Renderer GetRenderer ()
    {
      if (_renderer == null)
        _renderer = new Renderer ();
      return _renderer;
    }

    /// <summary>
    /// Handle a gui event (such as clicking the mouse)
    /// and follow up with respective actions
    /// </summary>
    /// <param name="e">E.</param>
    /// <param name="l">L.</param>
    public void HandleGuiEvent (GuiEvent e, Location l)
    {
      int x = (l.X / Escapade.GetWorld ().Size);
      int y = (l.Y / Escapade.GetWorld ().Size);
      Frame f = GetRenderer ().GetActiveFrame (l);
      if (f == null) {
        if (e == GuiEvent.MouseLeft) {
          if (Escapade.GetWorld ().Map [x, y].Type == TileType.Air)
            Escapade.GetPlayer ().NewPath (new Location (x, y));
        }
        if (e == GuiEvent.MouseRight) {
          Escapade.GetWorld ().ModifyTile (new Location (x, y));
          Frame inv = GetRenderer ().GetFrame ("inventory");
          if (inv != null) {
            List<string> minerals = Escapade.GetPlayer ().Inventory.ItemList.Select (i => i.Name).ToList ();
            Dictionary<string, int> mineralCount = new Dictionary<string, int> ();
            foreach (string s in minerals) {
              if (mineralCount.ContainsKey (s)) {
                mineralCount [s]++;
              } else {
                mineralCount [s] = 1;
              }
            }
            minerals = mineralCount.Select (kvp => kvp.Key + " - " + kvp.Value).ToList ();
            inv.Content = minerals;
          }
        }
      } else {
        if (f.GetAction (l) == Frame.GuiAction.Icon) {
          f.Close ();
          return;
        }
      }
    }
  }
}
