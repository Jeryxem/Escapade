using System.Collections.Generic;
using System.Linq;

namespace Escapade.gui
{
  public class Renderer
  {
    List<Frame> _frames;

    /// <summary>
    /// Initializes a new <see cref="T:Escapade.gui.Renderer"/>
    /// </summary>
    public Renderer ()
    {
      _frames = new List<Frame> ();
    }

    /// <summary>
    /// Register a frame so we can render it on the screen
    /// </summary>
    /// <param name="frame">Frame to register</param>
    public void RegisterFrame (Frame frame)
    {
      if (_frames.FirstOrDefault (f => f.Id == frame.Id) == null) {
        _frames.Add (frame);
      }
    }

    /// <summary>
    /// Deregister a frame from the renderer if we don't need it anymore
    /// </summary>
    /// <param name="frame">Frame to be deregistered</param>
    public void DeregisterFrame (Frame frame)
    {
      if (_frames.FirstOrDefault (f => f.Id == frame.Id) != null) {
        _frames.Remove (frame);
      }
    }

    /// <summary>
    /// Gets which frame is being activated by an event
    /// </summary>
    /// <returns>The active frame.</returns>
    /// <param name="l">Location of event (e.g. mouse click)</param>
    public Frame GetActiveFrame (Location l)
    {
      foreach (Frame f in _frames)
        if (f.IsActive (l)) return f;
      return null;
    }

    /// <summary>
    /// Toggle frame visibility
    /// </summary>
    /// <param name="id">Frame id</param>
    public void ToggleFrame (string id)
    {
      foreach (Frame f in _frames)
        if (f.Id == id) f.Toggle ();
    }

    public Frame GetFrame (string id)
    {
      return _frames.FirstOrDefault (f => f.Id == id);
    }

    /// <summary>
    /// Render the game window - renders visible frames after (on top of) the world
    /// </summary>
    public void RenderWindow ()
    {
      RenderWorld ();
      RenderObjects ();
      RenderFrames ();
    }

    /// <summary>
    /// Render the world and its tiles
    /// </summary>
    public void RenderWorld ()
    {
      Escapade.GetWorld ().Draw ();
    }

    /// <summary>
    /// Render any visible game objects in this instance
    /// </summary>
    public void RenderObjects ()
    {
      foreach (Entity e in Escapade.GetInstance ().Objects)
        e.Draw ();
    }

    /// <summary>
    /// Render currently visible frames
    /// </summary>
    public void RenderFrames ()
    {
      foreach (Frame f in _frames)
        f.Draw ();
    }
  }
}