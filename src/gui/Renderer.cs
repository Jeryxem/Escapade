using System;
using System.Collections.Generic;
using System.Linq;

namespace Escapade.gui
{
  public class Renderer
  {
    List<Frame> _frames;

    public Renderer ()
    {
      _frames = new List<Frame> ();
    }

    public void RegisterFrame (Frame frame)
    {
      if (_frames.FirstOrDefault (f => f.Id == frame.Id) == null) {
        _frames.Add (frame);
      }
    }

    public void DeregisterFrame (Frame frame)
    {
      if (_frames.FirstOrDefault (f => f.Id == frame.Id) != null) {
        _frames.Remove (frame);
      }
    }

    public Frame GetActiveFrame (Location l)
    {
      foreach (Frame f in _frames)
        if (f.IsActive (l)) return f;
      return null;
    }

    public void RenderFrames ()
    {
      foreach (Frame f in _frames)
        f.Draw ();
    }

 }
}