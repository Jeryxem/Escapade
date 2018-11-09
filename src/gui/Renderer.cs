using System;
using System.Collections.Generic;
using System.Linq;
using SwinGameSDK;

namespace Escapade.gui
{
  public class Renderer
  {
    List<Frame> _frames;
    Dictionary<string, Bitmap> _bitmaps;
    Dictionary<BitmapMask, Bitmap> _maskmap;

    /// <summary>
    /// Initializes a new <see cref="T:Escapade.gui.Renderer"/>
    /// </summary>
    public Renderer ()
    {
      _frames = new List<Frame> ();
      _bitmaps = new Dictionary<string, Bitmap> ();
      _maskmap = new Dictionary<BitmapMask, Bitmap> ();
      RegisterBitmaps ();
      MapBitmaps ();
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

    /// <summary>
    /// Get a frame according to its id
    /// </summary>
    /// <returns>The frame with identifier 'id'</returns>
    /// <param name="id">frame id</param>
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
      DrawWorldBitmaps ();
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

    /// <summary>
    /// Register bitmaps into the bitmap list with respective ids
    /// </summary>-*
    public void RegisterBitmaps ()
    {
      _bitmaps ["rock_inner_1"] = new Bitmap ("tiles\\rock_inner_1.png");
      _bitmaps ["rock_inner_2"] = new Bitmap ("tiles\\rock_inner_2.png");
      _bitmaps ["rock_inner_3"] = new Bitmap ("tiles\\rock_inner_3.png");

      _bitmaps ["rock_wall_vert"] = new Bitmap ("tiles\\rock_wall_vert.png");
      _bitmaps ["rock_wall_horiz"] = new Bitmap ("tiles\\rock_wall_horiz.png");

      _bitmaps ["rock_north"] = new Bitmap ("tiles\\rock_north.png");
      _bitmaps ["rock_east"] = new Bitmap ("tiles\\rock_east.png");
      _bitmaps ["rock_south"] = new Bitmap ("tiles\\rock_south.png");
      _bitmaps ["rock_west"] = new Bitmap ("tiles\\rock_west.png");

      _bitmaps ["rock_single"] = new Bitmap ("tiles\\rock_single.png");

      _bitmaps ["rock_pointnorth"] = new Bitmap ("tiles\\rock_pointnorth.png");
      _bitmaps ["rock_pointsouth"] = new Bitmap ("tiles\\rock_pointsouth.png");
      _bitmaps ["rock_pointwest"] = new Bitmap ("tiles\\rock_pointwest.png");
      _bitmaps ["rock_pointeast"] = new Bitmap ("tiles\\rock_pointeast.png");

      _bitmaps ["rock_northwest"] = new Bitmap ("tiles\\rock_northwest.png");
      _bitmaps ["rock_northeast"] = new Bitmap ("tiles\\rock_northeast.png");
      _bitmaps ["rock_southeast"] = new Bitmap ("tiles\\rock_southeast.png");
      _bitmaps ["rock_southwest"] = new Bitmap ("tiles\\rock_southwest.png");

      _bitmaps ["rock_vertex_northwest"] = new Bitmap ("tiles\\rock_vertex_northwest.png");
      _bitmaps ["rock_vertex_northeast"] = new Bitmap ("tiles\\rock_vertex_northeast.png");
      _bitmaps ["rock_vertex_southeast"] = new Bitmap ("tiles\\rock_vertex_southeast.png");
      _bitmaps ["rock_vertex_southwest"] = new Bitmap ("tiles\\rock_vertex_southwest.png");

      _bitmaps ["overlay_diamond"] = new Bitmap ("tiles\\overlay_diamond.png");
      _bitmaps ["overlay_sapphire"] = new Bitmap ("tiles\\overlay_sapphire.png");
      _bitmaps ["overlay_ruby"] = new Bitmap ("tiles\\overlay_ruby.png");
      _bitmaps ["overlay_emerald"] = new Bitmap ("tiles\\overlay_emerald.png");
    }

    /// <summary>
    /// Maps each mask to a respective bitmap
    /// </summary>
    public void MapBitmaps ()
    {
      _maskmap.Add (BitmapMask.North, _bitmaps ["rock_north"]);
      _maskmap.Add (BitmapMask.East, _bitmaps ["rock_east"]);
      _maskmap.Add (BitmapMask.South, _bitmaps ["rock_south"]);
      _maskmap.Add (BitmapMask.West, _bitmaps ["rock_west"]);

      _maskmap.Add (BitmapMask.North | BitmapMask.East, _bitmaps ["rock_vertex_northeast"]);
      _maskmap.Add (BitmapMask.North | BitmapMask.West, _bitmaps ["rock_vertex_northwest"]);
      _maskmap.Add (BitmapMask.South | BitmapMask.East, _bitmaps ["rock_vertex_southeast"]);
      _maskmap.Add (BitmapMask.South | BitmapMask.West, _bitmaps ["rock_vertex_southwest"]);

      _maskmap.Add (BitmapMask.North | BitmapMask.East | BitmapMask.South, _bitmaps ["rock_pointeast"]);
      _maskmap.Add (BitmapMask.North | BitmapMask.West | BitmapMask.South, _bitmaps ["rock_pointwest"]);
      _maskmap.Add (BitmapMask.South | BitmapMask.East | BitmapMask.West, _bitmaps ["rock_pointsouth"]);
      _maskmap.Add (BitmapMask.North | BitmapMask.West | BitmapMask.East, _bitmaps ["rock_pointnorth"]);

      _maskmap.Add (BitmapMask.North | BitmapMask.South, _bitmaps ["rock_wall_horiz"]);
      _maskmap.Add (BitmapMask.East | BitmapMask.West, _bitmaps ["rock_wall_vert"]);

      _maskmap.Add (BitmapMask.None, _bitmaps ["rock_inner_1"]);
      _maskmap.Add (BitmapMask.North | BitmapMask.West | BitmapMask.East | BitmapMask.South, _bitmaps ["rock_single"]);
    }

    /// <summary>
    /// Draws tiles based on their mapped bitmap
    /// </summary>
    public void DrawWorldBitmaps ()
    {
      int size = Escapade.GetWorld ().Size;
      for (int x = 0; x < Escapade.GetWorld ().Width; x++) {
        for (int y = 0; y < Escapade.GetWorld ().Height; y++) {
          if (Escapade.GetWorld ().Map [x, y].Type == TileType.Rock) {
            SwinGame.DrawBitmap (_maskmap [Escapade.GetWorld ().Map [x, y].Mask], x * size, y * size);
            Mineral m = Escapade.GetWorld ().Map [x, y].Mineral;
            if (m != null) {
              string mineralBitmap = "overlay_" + m.GetType ().ToString ().ToLower ().Split ('.').Last ();
              SwinGame.DrawBitmap (_bitmaps [mineralBitmap], x * size, y * size);
            }
          }
        }
      }
    }
  }
}