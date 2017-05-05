using System.Collections.Generic;
using SwinGameSDK;
using System;
using System.IO;

namespace Escapade
{
	class Game
	{
		/// <summary>
		/// The currently existing game objects
		/// </summary>
		private List<GameObject> objects = new List<GameObject>();

		public List<GameObject> Objects
		{
			get
			{
				return objects;
			}
			set
			{
				objects = value;
			}
		}

		/// <summary>
		/// The current game map
		/// </summary>
		private Map map;

		public Map Map
		{
			get
			{
				return map;
			}
			set
			{
				map = value;
			}
		}



		private Player player;

		public Player Player
		{
			get
			{
				return player;
			}
			set
			{
				player = value;
			}
		}

		/// <summary>
		/// Configuration settings used to initialise the game
		/// </summary>
		private static Dictionary<string, dynamic> _config = new Dictionary<string, dynamic>();

		public static Dictionary<string, dynamic> Config
		{
			get
			{
				return _config;
			}
			set
			{
				_config = value;
			}
		}


		public Game(Dictionary<string, dynamic> c)
		{
			Config = c;
		}

		/// <summary>
		/// Run initialisation for all game objects
		/// </summary>
		public void Init()
		{
			SwinGame.OpenGraphicsWindow("Escapade", Config["width"] * 10, Config["height"] * 10);

			Map = new Map(Config["width"], Config["height"]);
			Player = new Player(this, Objects.Count);

			RunLoop();
		}

		/// <summary>
		/// Run the loop
		/// </summary>
		public void RunLoop()
		{
		
			while (!SwinGame.WindowCloseRequested())
			{
				// Process the UI interaction
				SwinGame.ProcessEvents();

				// Clear the screen and start a new frame
				SwinGame.ClearScreen(Color.Black);

				// Update and draw the game objects
				Update();
				Draw();

				// Draw the frame
				if (Config["fps"]) SwinGame.DrawFramerate(0, 0);
				SwinGame.RefreshScreen(30);
			}
		}

		/// <summary>
		/// Run updates of all game objects
		/// </summary>
		public void Update()
		{
			if (SwinGame.KeyTyped(KeyCode.DKey)) player.TileModify(new Coordinate(Map.ToCell(SwinGame.MouseX()), Map.ToCell(SwinGame.MouseY())), 0);
			if (SwinGame.KeyTyped(KeyCode.PKey)) player.TileModify(new Coordinate(Map.ToCell(SwinGame.MouseX()), Map.ToCell(SwinGame.MouseY())), 1);

			//if the right mouse button is clicked, let'sset the player's location to that square
			if(SwinGame.MouseClicked(MouseButton.RightButton)) Player.target = new Coordinate(Map.ToCell(SwinGame.MouseX()), Map.ToCell(SwinGame.MouseY()));
			Player.Move();
		}

		bool saved = false;

		/// <summary>
		/// Run drawing of all game objects
		/// </summary>
		public void Draw()
		{
			Map.Draw();
			Player.Draw();
			Directory.CreateDirectory("map-out");

			if (!saved)
			{
				saved = true;
				SwinGame.SaveScreenshot(SwinGame.WindowNamed("Escapade"), "map-out/" + System.DateTime.Now.ToFileTimeUtc() + ".png");
			}
		}

		/// <summary>
		/// Clean all game objects (eg. for shutdown)
		/// </summary>
		public void Clean()
		{

		}
	}
}