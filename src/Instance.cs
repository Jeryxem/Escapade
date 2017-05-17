using System.Collections.Generic;
using SwinGameSDK;

namespace Escapade
{
  public class Instance
  {

    int _width;
    int _height;
    int _size;
    World _world;

    public List<Object> Objects;

    #region Properties
    public int Width {
      get {
        return _width;
      }
      set {
        _width = value;
      }
    }
    public int Height {
      get {
        return _height;
      }
      set {
        _height = value;
      }
    }
    public int Size {
      get {
        return _size;
      }
      set {
        _size = value;
      }
    }
    public World World {
      get {
        return _world;
      }
      set {
        _world = value;
      }
    }
    #endregion Properties

    public Instance ()
    {
      Width = 450;
      Height = 450;
      Size = 15;
      Start ();
    }

    public void Start ()
    {
      SwinGame.OpenGraphicsWindow ("Escapade", Width, Height);

      PreInit ();
      Init ();
      PostInit ();
      Run ();
    }

    public void PreInit ()
    {
      Objects = new List<Object> ();
      World = new World (Width, Height, Size, this);
    }

    public void Init ()
    {

    }

    public void PostInit ()
    {

    }

    public void Run ()
    {
      while (!SwinGame.WindowCloseRequested ()) {
        SwinGame.ClearScreen (Color.White);
        SwinGame.ProcessEvents ();

        Update ();
        Draw ();

        SwinGame.RefreshScreen (30);
      }
    }

    public void Update ()
    {
      World.Update ();
      foreach (Object obj in Objects)
        obj.Update (this);
    }

    public void Draw ()
    {
      World.Draw ();
      foreach (Object obj in Objects)
        obj.Draw (this);
    }

  }
}