using System.Collections.Generic;
using SwinGameSDK;

namespace Escapade
{
  public class Instance
  {

    World _world;

    public List<Object> Objects;
    
    public int Width { get; set; }
    public int Height { get; set; }
    public int Size { get; set; }
    public World World {
      get {
        return _world;
      }
      set {
        _world = value;
      }
    }


    public Instance ()
    {
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
      while (!SwinGame.WindowCloseRequested ())
      {
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