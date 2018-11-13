using System;
using System.Collections.Generic;
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
        static MetaHandler meta;
        public List<Entity> Objects;
        public List<Projectile> ProjectilesToBeRemoved;

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

        /// <summary>
        /// Create enemy object.
        /// </summary>
        /// <returns>The enemy.</returns> // by - Jeremy Toh
        public static Enemy GetEnemy()
        {
            if (_enemy == null)
            {
                Location l = new Location(25, 20);
                _enemy = new Enemy(0, "Enemy", l);
            }
            return _enemy;
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

        /// <summary>
        /// Starts the game by initialising world, game objects then starting game loop
        /// </summary>
        public void Start()
        {
            SwinGame.OpenGraphicsWindow("Escapade", GetWorld().Width * GetWorld().Size, GetWorld().Height * GetWorld().Size + 45);
            PreInit();
            Init();
            PostInit();
            PrepareExtraComponents();
            Run();
        }

        /// <summary>
        /// This method creates all the extra (custom) components or objects needed in the game.
        /// </summary>
        private void PrepareExtraComponents()
        {
            meta = new MetaHandler(); // IA - use this to control what is shown on the bottom of the screen in the meta section.
        }

        /// <summary>
        /// Initialisation stage 1 - initialise base objects
        /// </summary>
        public void PreInit()
        {
            Objects = new List<Entity>();
            ProjectilesToBeRemoved = new List<Projectile> ();
        }

        /// <summary>
        /// Initialisation stage 2 - initialise objects dependent on stage 1
        /// </summary>
        public void Init()
        {
            Objects.Add(GetPlayer());
            Objects.Add(GetEnemy());

            Frame inventory = new Frame("inventory", "Inventory", new Location(10, 10), 150, 150);
            inventory.AddButton(Color.DarkRed, Color.Red, inventory.Close);
            GuiEnvironment.GetRenderer().RegisterFrame(inventory);

            Frame help = new Frame("help", "Help and Controls", new Location(250, 10), 150, 250);
            help.AddButton(Color.DarkRed, Color.Red, help.Close);
            help.AddButton(Color.DarkBlue, Color.RoyalBlue, inventory.Toggle);
            GuiEnvironment.GetRenderer().RegisterFrame(help);


			/*//testing
			for (int x = 0; x<GlobalConstants.WORLD_WIDTH/GlobalConstants.SIZE; x++) 
			{
				for (int y = 0; y<GlobalConstants.WORLD_HEIGHT/GlobalConstants.SIZE; y++) 
				{

					if (_world.Map[_enemy.Location.X, _enemy.Location.Y].Type == TileType.Rock)
					{
						_enemy.DirectionX = 1;
					}
					else 
					{
						_enemy.DirectionX = 2;
					}

		        }
		     }
			//_enemy.SetCollision();*/
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
            while (!SwinGame.WindowCloseRequested())
            {

                SwinGame.ClearScreen(Color.White);
                SwinGame.ProcessEvents();

                Update();
                Draw();

                SwinGame.RefreshScreen(30);
            }
            SwinGame.ReleaseAllBitmaps();
        }

    /// <summary>
    /// Update the game based on events, objects and other things
    /// </summary>
    public void Update ()
    {
      		_enemy.enemyMovement (); //the enemy movement -Jeremy


					// click button M to change map to check collision
					// collision on left right up down, some part of edge no collision if look carefully - jeremy
					if (_world.Map[_enemy.Location.X+1, _enemy.Location.Y].Type == TileType.Rock)
					{
						_enemy.DirectionX = 2;
					}
					if (_world.Map[_enemy.Location.X-1, _enemy.Location.Y].Type == TileType.Rock)
					{
						_enemy.DirectionX = 1;
					}

					if (_world.Map[_enemy.Location.X, _enemy.Location.Y+1].Type == TileType.Rock)
					{
						_enemy.DirectionY = 2;
					}

					if (_world.Map[_enemy.Location.X, _enemy.Location.Y-1].Type == TileType.Rock)
					{
						_enemy.DirectionY = 1;
					}

          foreach (Entity e in Objects) {
            if (e is Projectile) {
              if (((Projectile)e).CheckObjectHit (_world, _enemy))
                ProjectilesToBeRemoved.Add ((Projectile)e);
            }
          }

          foreach (Projectile p in ProjectilesToBeRemoved)
            Objects.Remove (p);


					/*//collision on sharp edge - my problem with my logic
					if (_world.Map[_enemy.Location.X+1, _enemy.Location.Y+1].Type == TileType.Rock)
					{
						_enemy.DirectionX = 2;
						_enemy.DirectionY = 2;
					}

					if (_world.Map[_enemy.Location.X+1, _enemy.Location.Y-1].Type == TileType.Rock)
					{
						_enemy.DirectionX = 2;
						_enemy.DirectionY = 1;
					}

					if (_world.Map[_enemy.Location.X-1, _enemy.Location.Y+1].Type == TileType.Rock)
					{
						_enemy.DirectionX = 1;
						_enemy.DirectionY = 2;
					}

					if (_world.Map[_enemy.Location.X-1, _enemy.Location.Y-1].Type == TileType.Rock)
					{
						_enemy.DirectionX = 1;
						_enemy.DirectionY = 1;
					}*/


      foreach (Entity obj in Objects) {
        obj.Update ();
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
      if (SwinGame.KeyDown (KeyCode.VKey) && SwinGame.KeyTyped (KeyCode.WKey))
      {
        if (_player.Weapon != null) {
          _player.DeployWeapon (AttackDirection.Up);
          Objects.Add (_player.Weapon.Projectile);
        }
      }
      else if (SwinGame.KeyDown (KeyCode.WKey) && _player.Location.Y != 0) {
        _player.Location.Y -= 1;
      }

      if (SwinGame.KeyDown (KeyCode.VKey) && SwinGame.KeyTyped (KeyCode.SKey))
      {
        if (_player.Weapon != null) {
          _player.DeployWeapon (AttackDirection.Down);
          Objects.Add (_player.Weapon.Projectile);
        }
      }
			else if (SwinGame.KeyDown (KeyCode.SKey) && _player.Location.Y != GlobalConstants.WORLD_HEIGHT/GlobalConstants.SIZE - 1) {
        _player.Location.Y += 1;
      } 

      if (SwinGame.KeyDown (KeyCode.VKey) && SwinGame.KeyTyped (KeyCode.AKey))
      {
       if (_player.Weapon != null) {
          _player.DeployWeapon (AttackDirection.Left);
          Objects.Add (_player.Weapon.Projectile);
        }
      }
      else if (SwinGame.KeyDown (KeyCode.AKey) && _player.Location.X != 0) {
        _player.Location.X -= 1;
      }

      if (SwinGame.KeyDown (KeyCode.VKey) && SwinGame.KeyTyped (KeyCode.DKey))
      {
        if (_player.Weapon != null) {
          _player.DeployWeapon (AttackDirection.Right);
          Objects.Add (_player.Weapon.Projectile);
        }
      }
			else if (SwinGame.KeyDown (KeyCode.DKey) && _player.Location.X != GlobalConstants.WORLD_WIDTH/GlobalConstants.SIZE - 1) {
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

            meta.ShowBottomPanel(); // IA - Make the panel visible
            meta.DisplayHungerInformation(); // IA - Show the hunger level progress bar and messages
            meta.DisplayTimer(); // IA - Make the timer visible
            meta.DisplayGameLevel(); // IA - Display the game level
        }
    }
}
