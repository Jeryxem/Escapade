using System;
using System.Collections.Generic;
using Escapade.gui;
using SwinGameSDK;

namespace Escapade
{
  public class Escapade
  {
    static Escapade _instance;
    static World _world;
    static Player _player;
    static GuiEnvironment _environment;

    public List<Entity> Objects;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Escapade.Escapade"/> class.
    /// </summary>
    Escapade ()
    {
    }

    /// <summary>
    /// Gets the current game Instance - constructs an <see cref="T:Escapade.Escapade"/>
    /// object if a current one doesn't exist
    /// </summary>
    /// <returns>The Instance</returns>
    public static Escapade GetInstance ()
    {
      if (_instance == null) {
        _instance = new Escapade ();
      }
      return _instance;
    }

    /// <summary>
    /// Gets the current game World - constructs a <see cref="T:Escapade.World"/>
    /// object if a current one doesn't exist
    /// </summary>
    /// <returns>The World</returns>
    public static World GetWorld()
    {
      if (_world == null) {
        _world = new World (450, 450, 15);
      }
      return _world;
    }

    /// <summary>
    /// Gets the current game Player - constructs a <see cref="T:Escapade.Escapade"/>
    /// object if a current one doesn't exist
    /// </summary>
    /// <returns>The Player</returns>
    public static Player GetPlayer ()
    {
      if (_player == null) {
        Location l = new Location (20, 20);
        _player = new Player (0, "Player", l);
      }
      return _player;
    }

    /// <summary>
    /// Gets the current game GuiEnvironment - constructs a
    /// <see cref="T:Escapade.gui.GuiEnvironment"/> object if a current one doesn't exist
    /// </summary>
    /// <returns>The GuiEnvironment</returns>
    public static GuiEnvironment GetEnvironment ()
    {
      if (_environment == null) {
        _environment = GuiEnvironment.GetEnvironment ();
      }
    	return _environment;
    }

    /// <summary>
    /// Starts the game by initialising world, game objects then starting game loop
    /// </summary>
    public void Start ()
    {
      SwinGame.OpenGraphicsWindow ("Escapade", GetWorld().Width * GetWorld().Size, GetWorld().Height * GetWorld().Size);
  	  PreInit ();
  	  Init ();
  	  PostInit ();
      Run ();
    }

    /// <summary>
    /// Initialisation stage 1 - initialise base objects
    /// </summary>
    public void PreInit ()
    {
    	Objects = new List<Entity> ();
    }

    /// <summary>
    /// Initialisation stage 2 - initialise objects dependent on stage 1
    /// </summary>
    public void Init ()
    {
      Objects.Add (GetPlayer ());
    }

    /// <summary>
    /// Initialisation stage 3 - initialise objects dependent on stage 1 + 2
    /// </summary>
    public void PostInit ()
    {
      Frame inventory = new Frame ("inventory", "Inventory", new Location (10, 10), 150, 150);
      GuiEnvironment.GetRenderer ().RegisterFrame (inventory);
    }

    /// <summary>
    /// Run the game loop until the window is closed or the ESC key is pressed
    /// </summary>
    public void Run ()
    {
      while (!SwinGame.WindowCloseRequested ()) {
        
        SwinGame.ClearScreen (Color.White);
        SwinGame.ProcessEvents ();

        Update ();
        Draw ();

        SwinGame.RefreshScreen (30);
      }
      SwinGame.ReleaseAllBitmaps ();
    }

    /// <summary>
    /// Update the game based on events, objects and other things
    /// </summary>
    public void Update ()
    {
    	foreach (Entity obj in Objects) {
    		obj.Update ();
    	}

      if (SwinGame.MouseDown (MouseButton.LeftButton)) {
        GetEnvironment ().HandleGuiEvent (GuiEvent.LeftDown, new Location ((int)SwinGame.MouseX (), (int)SwinGame.MouseY ()));
      }

      if (SwinGame.MouseClicked (MouseButton.LeftButton)) {
        GetEnvironment ().HandleGuiEvent (GuiEvent.MouseLeft, new Location ((int)SwinGame.MouseX (), (int)SwinGame.MouseY ()));
      }
      if (SwinGame.MouseClicked (MouseButton.RightButton)) {
        GetEnvironment ().HandleGuiEvent (GuiEvent.MouseRight, new Location ((int)SwinGame.MouseX (), (int)SwinGame.MouseY ()));
      }

      if (SwinGame.KeyTyped (KeyCode.IKey)) {
        GuiEnvironment.GetRenderer ().ToggleFrame ("inventory");
      }

      if (SwinGame.KeyTyped (KeyCode.OKey)) {
        GetWorld ().PutMinerals ();
      }

      if (SwinGame.KeyDown (KeyCode.EscapeKey)) {
        Environment.Exit (0);
      }
    }

    /// <summary>
    /// Get the current renderer to draw the game
    /// </summary>
    public void Draw ()
    {
      GuiEnvironment.GetRenderer ().RenderWindow ();
    }
  }
}
