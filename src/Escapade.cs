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
                _world = new World(GlobalConstants.WORLD_WIDTH, GlobalConstants.WORLD_HEIGHT, 15);
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
        public void Update()
        {
			_enemy.enemyMovement(); //the enemy movement -Jeremy

            foreach (Entity obj in Objects)
            {
                obj.Update();
            }

            if (SwinGame.MouseClicked(MouseButton.LeftButton))
            {
                GetEnvironment().HandleGuiEvent(GuiEvent.MouseLeft, new Location((int)SwinGame.MouseX(), (int)SwinGame.MouseY()));
            }
            if (SwinGame.MouseClicked(MouseButton.RightButton))
            {
                GetEnvironment().HandleGuiEvent(GuiEvent.MouseRight, new Location((int)SwinGame.MouseX(), (int)SwinGame.MouseY()));
            }

            if (SwinGame.KeyTyped(KeyCode.IKey))
            {
                GuiEnvironment.GetRenderer().ToggleFrame("inventory");
            }

            if (SwinGame.KeyTyped(KeyCode.HKey))
            {
                GuiEnvironment.GetRenderer().ToggleFrame("help");
            }

            if (SwinGame.KeyTyped(KeyCode.OKey))
            {
                GetWorld().PutMinerals();
            }

            if (SwinGame.KeyTyped(KeyCode.MKey))
            {
                GetWorld().GenerateMap();
            }

            if (SwinGame.KeyDown(KeyCode.EscapeKey))
            {
                Environment.Exit(0);
            }

			//changes player input using keyboard - jeremy
			if (SwinGame.KeyDown(KeyCode.WKey) && _player.Location.Y != 0) 
			{
				_player.Location.Y -= 1;
			}

			if (SwinGame.KeyDown(KeyCode.SKey) && _player.Location.Y != 39) 
			{
				_player.Location.Y += 1;
			}

			if (SwinGame.KeyDown(KeyCode.AKey) && _player.Location.X != 0) 
			{
				_player.Location.X -= 1;
			}

			if (SwinGame.KeyDown(KeyCode.DKey) && _player.Location.X != 52) 
			{
				_player.Location.X += 1;
			}
        }

        /// <summary>
        /// Get the current renderer to draw the game
        /// </summary>
        public void Draw()
        {
            GuiEnvironment.GetRenderer().RenderWindow();

            meta.ShowBottomPanel(); // IA - Make the panel visible
            meta.DisplayTimer(); // IA - Make the timer visible
            meta.DisplayGameLevel(); // IA - Display the game level
        }
    }
}
