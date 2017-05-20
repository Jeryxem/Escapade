using System;
using System.Collections.Generic;
using Escapade.gui;
using SwinGameSDK;

namespace Escapade
{
  public class Instance
  {

    int _width;
    int _height;
    int _size;
    World _world;
    Container _container;

    public List<Entity> Objects;

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
    public Container Container {
      get {
        return _container;
      }
      set {
        _container = value;
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
      SwinGame.OpenGraphicsWindow ("Escapade", Width + 250, Height);

      PreInit ();
      Init ();
      PostInit ();
      Run ();
    }

    public void PreInit ()
    {
      Objects = new List<Entity> ();
      World = new World (Width, Height, Size, this);
    }

    public void Init ()
    {
      Player player = new Player (0, "Player", World.RandomEmpty (), this);
      Objects.Add (player);
    }

    public void PostInit ()
    {
      Container = new Container ();
      Container.AddComponent (new NamedFrame ("Inventory", Color.Black, 460, 0, 240, 450));
      Container.AddComponent (new NamedFrame ("Hello", Color.Black, 10, 10, 100, 100));
      Container.AddComponent (new NamedFrame ("I Am A Container", Color.Black, 250, 250, 150, 150));
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
      foreach (Entity obj in Objects) {
        obj.Update ();
        if (obj.GetType () == typeof (Player)) {
          if (SwinGame.MouseClicked (MouseButton.LeftButton)) {
            if (SwinGame.MouseX () > 450) continue;
            int x = (int)(SwinGame.MouseX () / Size);
            int y = (int)(SwinGame.MouseY () / Size);
            if (World.Map [x, y].Type == TileType.Air)
              ((Player)obj).NewPath (new Location (x, y));
          }
          if (SwinGame.MouseClicked (MouseButton.RightButton)) {
            if (SwinGame.MouseX () > 450) continue;
            int x = (int)(SwinGame.MouseX () / Size);
            int y = (int)(SwinGame.MouseY () / Size);
            if (x < 2 || x > Width - 2 || y < 2 || y > Height - 2) continue;
            if (World.Map [x, y].Type == TileType.Rock)
              World.ModifyTile ((Player)obj, new Location (x, y));
          }
        }
      }
      if (SwinGame.KeyDown (KeyCode.EscapeKey))
        Environment.Exit (0);
    }

    public void Draw ()
    {
      World.Draw ();
      foreach (Entity obj in Objects)
        obj.Draw ();
      Container.Draw ();
    }

  }
}