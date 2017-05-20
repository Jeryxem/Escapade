using System.Collections.Generic;

namespace Escapade.gui
{
	public class Container
	{
		List<GuiComponent> _components;
		Renderer _renderer;

		public Container ()
		{
			_components = new List<GuiComponent> ();
      _renderer = new Renderer ();
		}

		public void AddComponent (GuiComponent component)
		{
			_components.Add (component);
		}

		public void RemoveComponent (GuiComponent component)
		{
			if (_components.Contains (component))
				_components.Remove (component);
		}

    public void Draw ()
    {
      foreach(GuiComponent c in _components)
        _renderer.RenderView (c);
    }
	}
}
