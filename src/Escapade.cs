using System;
using System.Collections.Generic;
using System.Linq;
using Escapade.gui;
using Escapade.src.gui;
using SwinGameSDK;

namespace Escapade
{

    public class Escapade
    {
        static Escapade _instance;
        static World _world;
        static Player _player;
        static GuiEnvironment _environment;
        static Enemy _enemy;
		static Enemy _spawnenemy;
        public List<Entity> Objects;
        public List<Projectile> ProjectilesToBeRemoved;
    public List<Enemy> EnemiesToBeRemoved;
    public List<Enemy> SpawnedEnemies;
    public Stack<GameState> _gameStates;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Escapade.Escapade"/> class.
        /// </summary>
        Escapade()
        {
        }

        /// <summary>
        /// Gets the current game Instance - constructs an <see cref="T:Escapade.Escapade"/>
        /// object if a current one doesn't exist
        /// </summary>
        /// <returns>The Instance</returns>
        public static Escapade GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Escapade();
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
            if (_world == null)
            {
                 _world = new World(GlobalConstants.WORLD_WIDTH, GlobalConstants.WORLD_HEIGHT, GlobalConstants.SIZE); //JY- Changed 15 to a global constant (size)
            }
            return _world;
        }

        /// <summary>
        /// Gets the current game Player - constructs a <see cref="T:Escapade.Escapade"/>
        /// object if a current one doesn't exist
        /// </summary>
        /// <returns>The Player</returns>
        public static Player GetPlayer()
        {
            if (_player == null)
            {
                Location l = new Location(20, 20);
                _player = new Player(0, "Player", l);
            }
            return _player;
        }

        //  enemy spawn point - Jeremy Toh
        public static Enemy GetEnemy()
        {
           if (_enemy == null)
            {
                Location l = new Location(25, 20);
                _enemy = new Enemy(0, "Boss Enemy", l, 1, 0); // to differentiate the enemy
            }
            return _enemy;
        }

		public static Enemy SpawnMoreEnemy()
		{
			
			Location l = new Location(25, 20);
			_spawnenemy = new Enemy(0, "Enemy", l, 1, 0);

			return _spawnenemy;  
		}

        /// <summary>
        /// Gets the current game GuiEnvironment - constructs a
        /// <see cref="T:Escapade.gui.GuiEnvironment"/> object if a current one doesn't exist
        /// </summary>
        /// <returns>The GuiEnvironment</returns>
        public static GuiEnvironment GetEnvironment()
        {
            if (_environment == null)
            {
                _environment = GuiEnvironment.GetEnvironment();
            }
            return _environment;
        }

    public void ControlGameState ()
    {
      switch (_gameStates.Peek ()) 
      {
      case GameState.ViewingMainMenu:
        MainMenu ();
        break;
      //case GameState.ViewingInstructions:
      case GameState.SinglePlayerMode:
        SinglePlayerMode ();
        break;
      case GameState.TwoPlayerMode:
        TwoPlayerMode ();
        break;
      case GameState.ViewingEndGame:
        EndGame ();
        break;
      case GameState.QuittingGame:
        QuitGame ();
        break;

      }
    }

    /// <summary>
    /// Starts the game by initialising world, game objects then starting game loop
    /// </summary>
    public void Start ()
    {
      SwinGame.OpenGraphicsWindow ("Escapade", GetWorld ().Width * GetWorld ().Size + 200, GetWorld ().Height * GetWorld ().Size + 80); // IA - Changed the height of the window to allow for the display of the bottom panel
      GameResources.LoadResources ();
      _gameStates = new Stack<GameState> ();
      _gameStates.Push (GameState.ViewingMainMenu);
      ControlGameState ();
    }

    public void MainMenu ()
    {
      bool commandChosen = false;

      SwinGame.PlayMusic (GameResources.GameMusic ("main_menu_music"));
      if (!SwinGame.MusicPlaying ()) {

        while (!commandChosen) {
          SwinGame.ClearScreen (Color.White);
          SwinGame.DrawBitmap (GameResources.GameImage ("main_menu"), 0, 0);
          SwinGame.ProcessEvents ();

          if (SwinGame.MouseClicked (MouseButton.LeftButton)) {
            if (SwinGame.PointInRect (SwinGame.PointAt (SwinGame.MouseX (), SwinGame.MouseY ()), (float)GlobalConstants.SINGLE_PLAYER_BUTTON_X, (float)GlobalConstants.SINGLE_PLAYER_BUTTON_Y, (float)GlobalConstants.SINGLE_PLAYER_BUTTON_WIDTH, (float)GlobalConstants.BUTTON_HEIGHT))
              _gameStates.Push (GameState.SinglePlayerMode);
            else if (SwinGame.PointInRect (SwinGame.PointAt (SwinGame.MouseX (), SwinGame.MouseY ()), (float)GlobalConstants.TWO_PLAYER_BUTTON_X, (float)GlobalConstants.TWO_PLAYER_BUTTON_Y, (float)GlobalConstants.TWO_PLAYER_BUTTON_WIDTH, (float)GlobalConstants.BUTTON_HEIGHT))
              _gameStates.Push (GameState.TwoPlayerMode);
            else if (SwinGame.PointInRect (SwinGame.PointAt (SwinGame.MouseX (), SwinGame.MouseY ()), (float)GlobalConstants.INSTRUCTIONS_BUTTON_X, (float)GlobalConstants.INSTRUCTIONS_BUTTON_Y, (float)GlobalConstants.INSTRUCTIONS_BUTTON_WIDTH, (float)GlobalConstants.BUTTON_HEIGHT))
              _gameStates.Push (GameState.ViewingInstructions);
            else if (SwinGame.PointInRect (SwinGame.PointAt (SwinGame.MouseX (), SwinGame.MouseY ()), (float)GlobalConstants.QUIT_BUTTON_X, (float)GlobalConstants.QUIT_BUTTON_Y, (float)GlobalConstants.QUIT_BUTTON_WIDTH, (float)GlobalConstants.BUTTON_HEIGHT))
              _gameStates.Push (GameState.QuittingGame);

            commandChosen = true;
          }
          SwinGame.RefreshScreen (60);

        }
      }
      SwinGame.ClearScreen (Color.White);
      SwinGame.StopMusic ();
      ControlGameState ();
    }

    public void SinglePlayerMode ()
    {
      PreInit ();
	    Init ();
	    PostInit ();
	    PrepareExtraComponents ();
      Run ();
    }

    public void TwoPlayerMode ()
    {
    	PreInit ();
    	Init ();
    	PostInit ();
    	PrepareExtraComponents ();
	    Run ();
    }

    public void EndGame ()
    {
      _gameStates.Push (GameState.ViewingMainMenu);
      ControlGameState ();
    }

    public void QuitGame ()
    {
      GameResources.FreeResources ();
      SwinGame.ReleaseAllBitmaps();
    }


        /// <summary>
        /// This method creates all the extra (custom) components or objects needed in the game.
        /// </summary>
        private void PrepareExtraComponents()
        {
            MetaHandler.timer.StartTimer();
            // meta = new MetaHandler(); // IA - use this to control what is shown on the bottom of the screen in the meta section.
        }

        /// <summary>
        /// Initialisation stage 1 - initialise base objects
        /// </summary>
        public void PreInit()
        {
            Objects = new List<Entity>();
            ProjectilesToBeRemoved = new List<Projectile> ();
            EnemiesToBeRemoved = new List<Enemy> ();
            SpawnedEnemies = new List<Enemy> ();
        }

		/// <summary>
		/// Initialisation stage 2 - initialise objects dependent on stage 1
		/// </summary>

		bool initdone = false;
		bool initdone2 = false;
        public void Init()
        {
			if (initdone == false)
			{
				Objects.Add(GetPlayer());
        Objects.Add(GetEnemy());
				initdone = true;
			}
      
			if (initdone2 == true) //make sure only strong enemy spawn at the start of game
			{
				Enemy spawnedEnemy = SpawnMoreEnemy();
				Objects.Add(spawnedEnemy);
				SpawnedEnemies.Add(spawnedEnemy);
			}

			if (initdone2 == false)
			{
				Frame inventory = new Frame("inventory", "Inventory", new Location(10, 10), 150, 150);
				inventory.AddButton(Color.DarkRed, Color.Red, inventory.Close);
				GuiEnvironment.GetRenderer().RegisterFrame(inventory);

				Frame help = new Frame("help", "Help and Controls", new Location(250, 10), 150, 250);
				help.AddButton(Color.DarkRed, Color.Red, help.Close);
				help.AddButton(Color.DarkBlue, Color.RoyalBlue, inventory.Toggle);
				GuiEnvironment.GetRenderer().RegisterFrame(help);
				initdone2 = true;
			}

        }

        /// <summary>
        /// Initialisation stage 3 - initialise objects dependent on stage 1 + 2
        /// </summary>
        public void PostInit()
        {
            Frame helpFrame = GuiEnvironment.GetRenderer().GetFrame("help");
            List<string> helpList = new List<string>();
            helpList.Add("~ Frame help ~");
            helpList.Add("- The red button closes a frame.");
            helpList.Add("- The blue button toggles the");
            helpList.Add("inventory frame");
            helpList.Add(" ");
            helpList.Add("~ Mouse Controls ~");
            helpList.Add("Left: On air tile - move here");
            helpList.Add("Right: On rock tile - dig rock");
            helpList.Add("Right (+ Shift): On air - place rock");
            helpList.Add(" ");
            helpList.Add("~ Keyboard controls ~");
            helpList.Add("O - Generate new minerals");
            helpList.Add("M - Generate a new map");
            helpList.Add("H - Toggle this frame");
            helpList.Add("I - Toggle inventory frame");
            helpList.Add("Esc - Quit game");

            helpFrame.Content = helpList;

        }

        /// <summary>
        /// Run the game loop until the window is closed or the ESC key is pressed
        /// </summary>
        public void Run()
        {
            SwinGame.PlayMusic (GameResources.GameMusic ("game_music"));
            while (!SwinGame.WindowCloseRequested())
            {
                
                SwinGame.ClearScreen(Color.White);
                SwinGame.ProcessEvents();

                Update();
                Draw();

                SwinGame.RefreshScreen(30);
            }
      _gameStates.Push (GameState.QuittingGame);
      ControlGameState ();
        }

	int randomhit = 0;
    /// <summary>
    /// Update the game based on events, objects and other things
    /// </summary>
    public void Update ()
    {

					// click button M to change map to check collision
					// collision on left right up down, some part of edge no collision if look carefully - jeremy
					if (_world.Map[_enemy.Location.X+1, _enemy.Location.Y].Type == TileType.Rock)
					{
						randomhit++;
					}
					if (_world.Map[_enemy.Location.X-1, _enemy.Location.Y].Type == TileType.Rock)
					{
						randomhit++;
					}

					if (_world.Map[_enemy.Location.X, _enemy.Location.Y+1].Type == TileType.Rock)
					{
						randomhit++;
					}

					if (_world.Map[_enemy.Location.X, _enemy.Location.Y - 1].Type == TileType.Rock)
					{
						randomhit++;
					}


			//spawn rate - jeremy
			if (randomhit >= 20) 
			{
				Init();
				randomhit = 0;
			}

      // JY- detect projectile hits, projectile and enemies will be deleted
       foreach (Entity e in Objects) 
      {
        if (e is Projectile) 
        {
          if (SpawnedEnemies.Count > 0) 
          {
            foreach (Enemy _e in SpawnedEnemies) 
            {
              if (((Projectile)e).CheckObjectHit (_world, _e))
                ProjectilesToBeRemoved.Add ((Projectile)e);

              if (_e.CheckHit ((Projectile)e))
                EnemiesToBeRemoved.Add (_e);

            }
          } 
          else
            if (((Projectile)e).CheckObjectHit (_world, _enemy))
                ProjectilesToBeRemoved.Add ((Projectile)e);
        }
      }

      foreach (Projectile p in ProjectilesToBeRemoved)
        Objects.Remove (p);

      foreach (Enemy en in EnemiesToBeRemoved)
      {
        if (SpawnedEnemies.Contains (en))
          SpawnedEnemies.Remove (en);
        
        Objects.Remove (en);
      }

      foreach (Entity obj in Objects) {
        obj.Update ();
      }

      /*if (SwinGame.MouseClicked (MouseButton.LeftButton)) {
        GetEnvironment ().HandleGuiEvent (GuiEvent.MouseLeft, new Location ((int)SwinGame.MouseX (), (int)SwinGame.MouseY ()));
      }*/

	  //mine rocks/minerals right
			if (SwinGame.KeyDown(KeyCode.DKey) && SwinGame.KeyReleased(KeyCode.FKey)) 
			{
				//GetEnvironment ().HandleGuiEvent (GuiEvent.MouseRight, new Location (_player.Location.X+1, _player.Location.Y));
				GetEnvironment();
				int x = (_player.Location.X+1);
				int y = (_player.Location.Y);
				Frame f = GuiEnvironment.GetRenderer().GetActiveFrame(new Location (x, y));
				if (f == null)
				{
					Tile tile = _world.Map[x, y];
					if (tile.Type == TileType.Rock)
					{
						_world.Map[x, y] = new Tile(TileType.Air);
						if (tile.Mineral != null)
							GetPlayer().Inventory.AddItem(tile.Mineral);
					}

					Frame inv = GuiEnvironment.GetRenderer().GetFrame("inventory");
					if (inv != null)
					{
						List<string> minerals = GetPlayer().Inventory.ItemList.Select(i => i.Name).ToList();
						Dictionary<string, int> mineralCount = new Dictionary<string, int>();
						foreach (string s in minerals)
						{
							if (mineralCount.ContainsKey(s))
							{
								mineralCount[s]++;
							}
							else
							{
								mineralCount[s] = 1;
							}
						}
						minerals = mineralCount.Select(kvp => kvp.Key + " - " + kvp.Value).ToList();
						inv.Content = minerals;
					}
				}
	 		}

			//mine left
			if (SwinGame.KeyDown(KeyCode.AKey) && SwinGame.KeyReleased(KeyCode.FKey)) 
			{
				//GetEnvironment ().HandleGuiEvent (GuiEvent.MouseRight, new Location (_player.Location.X+1, _player.Location.Y));
				GetEnvironment();
				int x = (_player.Location.X - 1);
				int y = (_player.Location.Y);
				Frame f = GuiEnvironment.GetRenderer().GetActiveFrame(new Location(x, y));
				if (f == null)
				{
					Tile tile = _world.Map[x, y];
					if (tile.Type == TileType.Rock)
					{
						_world.Map[x, y] = new Tile(TileType.Air);
						if (tile.Mineral != null)
							GetPlayer().Inventory.AddItem(tile.Mineral);
					}

					Frame inv = GuiEnvironment.GetRenderer().GetFrame("inventory");
					if (inv != null)
					{
						List<string> minerals = GetPlayer().Inventory.ItemList.Select(i => i.Name).ToList();
						Dictionary<string, int> mineralCount = new Dictionary<string, int>();
						foreach (string s in minerals)
						{
							if (mineralCount.ContainsKey(s))
							{
								mineralCount[s]++;
							}
							else
							{
								mineralCount[s] = 1;
							}
						}
						minerals = mineralCount.Select(kvp => kvp.Key + " - " + kvp.Value).ToList();
						inv.Content = minerals;
					}
				}
			}

			//mine top
			if (SwinGame.KeyDown(KeyCode.WKey) && SwinGame.KeyReleased(KeyCode.FKey)) 
			{
				//GetEnvironment ().HandleGuiEvent (GuiEvent.MouseRight, new Location (_player.Location.X+1, _player.Location.Y));
				GetEnvironment();
				int x = (_player.Location.X);
				int y = (_player.Location.Y-1);
				Frame f = GuiEnvironment.GetRenderer().GetActiveFrame(new Location(x, y));
				if (f == null)
				{
					Tile tile = _world.Map[x, y];
					if (tile.Type == TileType.Rock)
					{
						_world.Map[x, y] = new Tile(TileType.Air);
						if (tile.Mineral != null)
							GetPlayer().Inventory.AddItem(tile.Mineral);
					}

					Frame inv = GuiEnvironment.GetRenderer().GetFrame("inventory");
					if (inv != null)
					{
						List<string> minerals = GetPlayer().Inventory.ItemList.Select(i => i.Name).ToList();
						Dictionary<string, int> mineralCount = new Dictionary<string, int>();
						foreach (string s in minerals)
						{
							if (mineralCount.ContainsKey(s))
							{
								mineralCount[s]++;
							}
							else
							{
								mineralCount[s] = 1;
							}
						}
						minerals = mineralCount.Select(kvp => kvp.Key + " - " + kvp.Value).ToList();
						inv.Content = minerals;
					}
				}
			}

			//mine bottom
			if (SwinGame.KeyDown(KeyCode.SKey) && SwinGame.KeyReleased(KeyCode.FKey)) 
			{
				//GetEnvironment ().HandleGuiEvent (GuiEvent.MouseRight, new Location (_player.Location.X+1, _player.Location.Y));
				GetEnvironment();
				int x = (_player.Location.X);
				int y = (_player.Location.Y+1);
				Frame f = GuiEnvironment.GetRenderer().GetActiveFrame(new Location(x, y));
				if (f == null)
				{
					Tile tile = _world.Map[x, y];
					if (tile.Type == TileType.Rock)
					{
						_world.Map[x, y] = new Tile(TileType.Air);
						if (tile.Mineral != null)
							GetPlayer().Inventory.AddItem(tile.Mineral);
					}

					Frame inv = GuiEnvironment.GetRenderer().GetFrame("inventory");
					if (inv != null)
					{
						List<string> minerals = GetPlayer().Inventory.ItemList.Select(i => i.Name).ToList();
						Dictionary<string, int> mineralCount = new Dictionary<string, int>();
						foreach (string s in minerals)
						{
							if (mineralCount.ContainsKey(s))
							{
								mineralCount[s]++;
							}
							else
							{
								mineralCount[s] = 1;
							}
						}
						minerals = mineralCount.Select(kvp => kvp.Key + " - " + kvp.Value).ToList();
						inv.Content = minerals;
					}
				}
			}

			//build rock right
			if (SwinGame.KeyDown(KeyCode.DKey) && SwinGame.KeyReleased(KeyCode.GKey)) 
			{
				//GetEnvironment ().HandleGuiEvent (GuiEvent.MouseRight, new Location (_player.Location.X+1, _player.Location.Y));
				GetEnvironment();
				int x = (_player.Location.X + 1);
				int y = (_player.Location.Y);
				Frame f = GuiEnvironment.GetRenderer().GetActiveFrame(new Location(x, y));
				if (f == null)
				{
					Tile tile = _world.Map[x, y];
					if (tile.Type == TileType.Air)
					{
						_world.Map[x, y] = new Tile (TileType.Rock);
					}
				}
	 	   }

			//build rock left
			if (SwinGame.KeyDown(KeyCode.AKey) && SwinGame.KeyReleased(KeyCode.GKey)) 
			{
				//GetEnvironment ().HandleGuiEvent (GuiEvent.MouseRight, new Location (_player.Location.X+1, _player.Location.Y));
				GetEnvironment();
				int x = (_player.Location.X - 1);
				int y = (_player.Location.Y);
				Frame f = GuiEnvironment.GetRenderer().GetActiveFrame(new Location(x, y));
				if (f == null)
				{
					Tile tile = _world.Map[x, y];
					if (tile.Type == TileType.Air)
					{
						_world.Map[x, y] = new Tile(TileType.Rock);
					}
				}
	 	   }

			//build rock up
			if (SwinGame.KeyDown(KeyCode.WKey) && SwinGame.KeyReleased(KeyCode.GKey)) 
			{
				//GetEnvironment ().HandleGuiEvent (GuiEvent.MouseRight, new Location (_player.Location.X+1, _player.Location.Y));
				GetEnvironment();
				int x = (_player.Location.X);
				int y = (_player.Location.Y-1);
				Frame f = GuiEnvironment.GetRenderer().GetActiveFrame(new Location(x, y));
				if (f == null)
				{
					Tile tile = _world.Map[x, y];
					if (tile.Type == TileType.Air)
					{
						_world.Map[x, y] = new Tile(TileType.Rock);
					}
				}
	 	   }

			//build rock down
			if (SwinGame.KeyDown(KeyCode.SKey) && SwinGame.KeyReleased(KeyCode.GKey)) 
			{
				//GetEnvironment ().HandleGuiEvent (GuiEvent.MouseRight, new Location (_player.Location.X+1, _player.Location.Y));
				GetEnvironment();
				int x = (_player.Location.X);
				int y = (_player.Location.Y+1);
				Frame f = GuiEnvironment.GetRenderer().GetActiveFrame(new Location(x, y));
				if (f == null)
				{
					Tile tile = _world.Map[x, y];
					if (tile.Type == TileType.Air)
					{
						_world.Map[x, y] = new Tile(TileType.Rock);
					}
				}
	 	   }


      if (SwinGame.KeyTyped (KeyCode.IKey)) {
        GuiEnvironment.GetRenderer ().ToggleFrame ("inventory");
      }

      if (SwinGame.KeyTyped (KeyCode.HKey)) {
        GuiEnvironment.GetRenderer ().ToggleFrame ("help");
      }

      if (SwinGame.KeyTyped (KeyCode.OKey)) {
        GetWorld ().PutMinerals ();
      }

      if (SwinGame.KeyTyped (KeyCode.MKey)) {
        GetWorld ().GenerateMap ();
      }

      if (SwinGame.KeyDown (KeyCode.EscapeKey)) {
        Environment.Exit (0);
      }

      //changes player input using keyboard - jeremy
      //Allowed player to attack,
      //else if if used so that the player can spam attack and move at the same time - Jonathan

      //please test it, use b to buy or shift b (for super) and try attacking
      //you have to buy a weapon first
      if (SwinGame.KeyDown (KeyCode.VKey) && SwinGame.KeyDown (KeyCode.WKey))
      {
                if (_player.Weapon != null && _player.Weapon.Ammunition > 0)
                {
                    _player.DeployWeapon(AttackDirection.Up);
                    Objects.Add(_player.Weapon.Projectile);
                }
      }
			else if (SwinGame.KeyDown (KeyCode.WKey) && _world.Map[_player.Location.X, _player.Location.Y-1].Type != TileType.Rock && _player.Location.Y-1 != 0) {
        _player.Location.Y -= 1;
      }

      if (SwinGame.KeyDown (KeyCode.VKey) && SwinGame.KeyDown (KeyCode.SKey))
      {
        if (_player.Weapon != null && _player.Weapon.Ammunition > 0) {
          _player.DeployWeapon (AttackDirection.Down);
          Objects.Add (_player.Weapon.Projectile);
        }
      }
			else if (SwinGame.KeyDown (KeyCode.SKey) && _world.Map[_player.Location.X, _player.Location.Y+1].Type != TileType.Rock && _player.Location.Y+1 != 36) {
        _player.Location.Y += 1;
      } 

      if (SwinGame.KeyDown (KeyCode.VKey) && SwinGame.KeyDown (KeyCode.AKey))
      {
       if (_player.Weapon != null && _player.Weapon.Ammunition > 0) {
          _player.DeployWeapon (AttackDirection.Left);
          Objects.Add (_player.Weapon.Projectile);
        }
      }
      else if (SwinGame.KeyDown (KeyCode.AKey) &&  _world.Map[_player.Location.X-1, _player.Location.Y].Type != TileType.Rock && _player.Location.X-1 != 0) {
        _player.Location.X -= 1;
      }

      if (SwinGame.KeyDown (KeyCode.VKey) && SwinGame.KeyDown (KeyCode.DKey))
      {
        if (_player.Weapon != null && _player.Weapon.Ammunition > 0) {
          _player.DeployWeapon (AttackDirection.Right);
          Objects.Add (_player.Weapon.Projectile);
        }
      }
			else if (SwinGame.KeyDown (KeyCode.DKey) && _world.Map[_player.Location.X+1, _player.Location.Y].Type != TileType.Rock && _player.Location.X+1 != 52) {
        _player.Location.X += 1;
      }

      if (SwinGame.KeyTyped (KeyCode.BKey)) 
      {
        _player.BuyWeapon (_player.Location,WeaponType.Normal);
        Objects.Add (_player.Weapon);
      }

      if (SwinGame.KeyTyped (KeyCode.BKey) && SwinGame.KeyDown(KeyCode.LeftShiftKey)) 
      {
        _player.BuyWeapon (_player.Location, WeaponType.Super);
        Objects.Add (_player.Weapon);
      }
      
    }
        /// <summary>
        /// Get the current renderer to draw the game
        /// </summary>
        public void Draw()
        {
            GuiEnvironment.GetRenderer().RenderWindow();

            MetaHandler.ShowPanels(); // IA - Make the panel visible
            MetaHandler.DisplayHungerInformation(); // IA - Show the hunger level progress bar and messages
            MetaHandler.DisplayTimer(); // IA - Make the timer visible
            MetaHandler.DisplayGameLevel(); // IA - Display the game level
            MetaHandler.DisplayAmmunitionLevel(_player.Weapon); // IA - Display info about amminutions (type and amount)
            MetaHandler.DisplayEnemyHitCount(EnemiesToBeRemoved); // IA - Display how many enemies have been destroyed.
            MetaHandler.DisplayExistingEnemies(SpawnedEnemies); // IA - Display how many enemies have been created.
        }
    }
}
