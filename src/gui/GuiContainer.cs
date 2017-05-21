using System.Collections.Generic;
using SwinGameSDK;

namespace Escapade.gui
{
  public abstract class GuiContainer : GuiElement
  {
    List<GuiElement> _children;

    public List<GuiElement> Children {
      get {
        return _children;
      }
      set {
        _children = value;
      }
    }

    protected GuiContainer (int xOffset, int yOffset, int width, int height, Color fgcolor, Color bgcolor, GuiContainer parent, bool visible)
      : base (xOffset, yOffset, width, height, fgcolor, bgcolor, parent, visible)
    {
      Children = new List<GuiElement> ();
    }

    public void AddChild (GuiElement e)
    {
      e.Parent = this;
      Children.Add (e);
    }

    public void RemoveChild (GuiElement e)
    {
      if (Children.Contains (e)) Children.Remove (e);
    }

    public override void Render ()
    {
      base.Render ();
      foreach (GuiElement e in Children)
        e.Render ();
    }

  }
}