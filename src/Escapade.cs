﻿using System;
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
        static Player _player2;
        static GuiEnvironment _environment;
        static Enemy _enemy;
        static Enemy _spawnenemy;
        public List<Entity> Objects;
        public List<Mineral> Minerals = new List<Mineral>(); // IA - The list that will contain mined minerals
        public List<Projectile> ProjectilesToBeRemoved;
        public List<Enemy> EnemiesToBeRemoved;
        public List<Enemy> SpawnedEnemies;
        public Stack<GameState> _gameStates;
        private bool firstGame = true;

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
                Location l = new Location(7, 5);
                _player = new Player(0, "Player1", l);
            }
            return _player;
        }

        //player 2 -jeremy toh
		/// <summary>
		/// Gets the player2.
		/// </summary>
		/// <returns>The player2.</returns>
        public static Player GetPlayer2()
        {
            if (_player2 == null)
            {
                Location l = new Location(45, 30);
                _player2 = new Player(0, "Player2", l);
            }
            return _player2;
        }


        //  enemy spawn point - Jeremy Toh
		/// <summary>
		/// Gets the enemy and set spawnpoint
		/// </summary>
		/// <returns>The enemy.</returns>
        public static Enemy GetEnemy()
        {
            if (_enemy == null)
            {
                Random r = new Random();
                int morerandom = r.Next(0, 19);
                if (morerandom < 10)
                {
                    Location l = new Location(25, 20);
                    _enemy = new Enemy(0, "Boss Enemy", l, 0, 1); // to differentiate the enemy
                }
                else if (morerandom >= 10)
                {
                    Location l = new Location(25, 20);
                    _enemy = new Enemy(0, "Boss Enemy", l, 0, 2); // to differentiate the enemy
                }
            }
            return _enemy;
        }

		/// <summary>
		/// Spawns more enemy and set their spawn point
		/// </summary>
		/// <returns>The more enemy.</returns>
        public static Enemy SpawnMoreEnemy()
        {
            Random r2 = new Random();
            int morerandom2 = r2.Next(0, 39);
            if (morerandom2 < 10)
            {
                Location l = new Location(25, 20);
                _spawnenemy = new Enemy(0, "Enemy", l, 0, 1);
            }
            else if (morerandom2 >= 10 && morerandom2 < 20)
            {
                Location l = new Location(25, 20);
                _spawnenemy = new Enemy(0, "Enemy", l, 0, 2);
            }
            else if (morerandom2 >= 20 && morerandom2 < 30)
            {
                Location l = new Location(25, 20);
                _spawnenemy = new Enemy(0, "Enemy", l, 1, 0);
            }
            else if (morerandom2 >= 30 && morerandom2 < 40)
            {
                Location l = new Location(25, 20);
                _spawnenemy = new Enemy(0, "Enemy", l, 2, 0);
            }
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

        /// <summary>
        /// This method sets a formula that must be evaluated before deciding whether the player can progress to the next level. Added by Isaac.
        /// </summary>
        public void ControlLevels()
        {
            if (EnemiesToBeRemoved.Count >= GameLevel.GetFormula())
            {
                GameLevel.IncreaseLevel(); // IA - move the next level if enough enemies have been destroyed
                Food.IncreaseMineralValue(); // IA - Make food more expensive
                Food.IncreaseEnergyValue(); // IA - Make the food conversion to energy less significant.
                EnemiesToBeRemoved.Clear(); // IA - Reset the count of enemies destroyed at each level.
                GetWorld().PutMinerals(); //jeremy - for every new level, new minerals added to game
            }
        }

        public void ControlGameState()
        {
            switch (_gameStates.Peek())
            {
                case GameState.ViewingMainMenu:
                    MainMenu();
                    break;
                case GameState.ViewingInstructions:
                    Instructions();
                    break;
                case GameState.SinglePlayerMode:
                    SinglePlayerMode();
                    break;
                case GameState.TwoPlayerMode:
                    TwoPlayerMode();
                    break;
                case GameState.PlayerOneWins:
                    EndGame("player_one_wins");
                    break;
                case GameState.PlayerTwoWins:
                    EndGame("player_two_wins");
                    break;
                case GameState.SinglePlayerEndGame:
                    EndGame("you_died");
                    break;
                case GameState.QuittingGame:
                    QuitGame();
                    break;

            }
        }

        /// <summary>
        /// Starts the game by initialising world, game objects then starting game loop
        /// </summary>
        public void Start()
        {
            SwinGame.OpenGraphicsWindow("Escapade", GetWorld().Width * GetWorld().Size + 200, GetWorld().Height * GetWorld().Size + 80); // IA - Changed the height of the window to allow for the display of the bottom panel
			SwinGame.ToggleFullScreen();
            GameResources.LoadResources();
            SwinGame.PlayMusic(GameResources.GameMusic("main_menu_music"));
            _gameStates = new Stack<GameState>();
            _gameStates.Push(GameState.ViewingMainMenu);
            ControlGameState();
        }

        /// <summary>
        /// The main menu screen
        /// </summary>
        public void MainMenu()
        {
            bool commandChosen = false;
            SwinGame.ResumeMusic();
            //SwinGame.LoadMusic("Pixel-Island.wav");
            //SwinGame.PlayMusic("Pixel-Island.wav");
            //SwinGame.PlayMusic (GameResources.GameMusic ("main_menu_music"));

            while (!commandChosen)
            {
                SwinGame.ClearScreen(Color.White);
                SwinGame.DrawBitmap(GameResources.GameImage("main_menu"), 0, 0);
                SwinGame.ProcessEvents();

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

          SwinGame.DrawText ("Original game by: FlareLine", Color.White, 700, 560);
          SwinGame.DrawText ("Upgraded by: Jeremy Toh (101213618)", Color.White,700, 580);
          SwinGame.DrawText ("             Jonathan Yap (100069580)", Color.White,700, 600);
          SwinGame.DrawText ("             Isaac Asante (101208203)", Color.White,700, 620);
          SwinGame.RefreshScreen(60);
            }
            SwinGame.ClearScreen(Color.White);
            SwinGame.PauseMusic();
            ControlGameState();
        }

        /// <summary>
        /// Displays the instructions of the game
        /// </summary>
        public void Instructions()
        {
            SwinGame.ResumeMusic();
            int page = 1;

            while (page <= 2)
            {
                SwinGame.ClearScreen(Color.White);
                SwinGame.DrawBitmap(GameResources.GameImage("instructions"), 0, 0);
                DisplayInstructionsContent(page);
                SwinGame.ProcessEvents();

                if (SwinGame.MouseClicked(MouseButton.LeftButton))
                    page++;
                SwinGame.RefreshScreen(60);
            }
            SwinGame.PauseMusic();
            _gameStates.Push(GameState.ViewingMainMenu);
            ControlGameState();
        }

        /// <summary>
        /// Displays the content of the instructions.
        /// </summary>
        /// <param name="page">Page.</param>
        public void DisplayInstructionsContent(int page)
        {
            int textSpacing = 10, textSpacingIncrementer = 10;

            if (page == 1)
            {
                SwinGame.DrawText("Objective:", Color.Black, GlobalConstants.INSTRUCTIONS_CONTENT_X, GlobalConstants.INSTRUCTIONS_CONTENT_Y);
                SwinGame.DrawText("(1) Kill enemies & advance in levels (Single-player)", Color.Black, GlobalConstants.INSTRUCTIONS_CONTENT_X, GlobalConstants.INSTRUCTIONS_CONTENT_Y + textSpacing);
                textSpacing += textSpacingIncrementer;
                SwinGame.DrawText("(2) Defeat the other player! (Two-player)", Color.Black, GlobalConstants.INSTRUCTIONS_CONTENT_X, GlobalConstants.INSTRUCTIONS_CONTENT_Y + textSpacing);
                textSpacing += textSpacingIncrementer * 2;
                SwinGame.DrawText("Controls: ", Color.Black, GlobalConstants.INSTRUCTIONS_CONTENT_X, GlobalConstants.INSTRUCTIONS_CONTENT_Y + textSpacing);
                textSpacing += textSpacingIncrementer;
                SwinGame.DrawText("Player 1 > ", Color.Black, GlobalConstants.INSTRUCTIONS_CONTENT_X, GlobalConstants.INSTRUCTIONS_CONTENT_Y + textSpacing);
                SwinGame.DrawText("Player 2 > ", Color.Black, GlobalConstants.INSTRUCTIONS_CONTENT_X + 550, GlobalConstants.INSTRUCTIONS_CONTENT_Y + textSpacing);
                textSpacing += textSpacingIncrementer;
                SwinGame.DrawBitmap(GameResources.GameImage("wasd"), GlobalConstants.INSTRUCTIONS_CONTENT_X, GlobalConstants.INSTRUCTIONS_CONTENT_Y + textSpacing);
                SwinGame.DrawBitmap(GameResources.GameImage("arrow_key"), GlobalConstants.INSTRUCTIONS_CONTENT_X + 550, GlobalConstants.INSTRUCTIONS_CONTENT_Y + textSpacing);
                SwinGame.DrawText("To move", Color.Black, GlobalConstants.INSTRUCTIONS_CONTENT_X + 320, GlobalConstants.INSTRUCTIONS_CONTENT_Y + textSpacing + 50);
                textSpacing += textSpacingIncrementer * 2;
                SwinGame.DrawBitmap(GameResources.GameImage("wasd_v_key"), GlobalConstants.INSTRUCTIONS_CONTENT_X - 6, GlobalConstants.INSTRUCTIONS_CONTENT_Y + textSpacing);
                SwinGame.DrawBitmap(GameResources.GameImage("arrow_key_O"), GlobalConstants.INSTRUCTIONS_CONTENT_X + 544, GlobalConstants.INSTRUCTIONS_CONTENT_Y + textSpacing);
                SwinGame.DrawText("To use weapon", Color.Black, GlobalConstants.INSTRUCTIONS_CONTENT_X + 300, GlobalConstants.INSTRUCTIONS_CONTENT_Y + textSpacing + 100);
                textSpacing += textSpacingIncrementer * 16;
                SwinGame.DrawBitmap(GameResources.GameImage("b_key"), GlobalConstants.INSTRUCTIONS_CONTENT_X + 25, GlobalConstants.INSTRUCTIONS_CONTENT_Y + textSpacing);
                SwinGame.DrawBitmap(GameResources.GameImage("p_key"), GlobalConstants.INSTRUCTIONS_CONTENT_X + 575, GlobalConstants.INSTRUCTIONS_CONTENT_Y + textSpacing);
                SwinGame.DrawText("To buy normal weapon", Color.Black, GlobalConstants.INSTRUCTIONS_CONTENT_X + 275, GlobalConstants.INSTRUCTIONS_CONTENT_Y + textSpacing + 25);
                textSpacing += textSpacingIncrementer * 2;
                SwinGame.DrawBitmap(GameResources.GameImage("shift_b_key"), GlobalConstants.INSTRUCTIONS_CONTENT_X - 25, GlobalConstants.INSTRUCTIONS_CONTENT_Y + textSpacing);
                SwinGame.DrawBitmap(GameResources.GameImage("j_p_key"), GlobalConstants.INSTRUCTIONS_CONTENT_X + 550, GlobalConstants.INSTRUCTIONS_CONTENT_Y + textSpacing);
                SwinGame.DrawText("To buy super weapon", Color.Black, GlobalConstants.INSTRUCTIONS_CONTENT_X + 275, GlobalConstants.INSTRUCTIONS_CONTENT_Y + textSpacing + 100);
            }
            else
            {
                SwinGame.DrawBitmap(GameResources.GameImage("wasd_f_key"), GlobalConstants.INSTRUCTIONS_CONTENT_X - 5, GlobalConstants.INSTRUCTIONS_CONTENT_Y + textSpacing);
                SwinGame.DrawBitmap(GameResources.GameImage("arrow_key_K"), GlobalConstants.INSTRUCTIONS_CONTENT_X + 545, GlobalConstants.INSTRUCTIONS_CONTENT_Y + textSpacing);
                textSpacing += textSpacingIncrementer * 3;
                SwinGame.DrawText("Player 1 > ", Color.Black, GlobalConstants.INSTRUCTIONS_CONTENT_X, GlobalConstants.INSTRUCTIONS_CONTENT_Y + textSpacing);
                SwinGame.DrawText("Player 2 > ", Color.Black, GlobalConstants.INSTRUCTIONS_CONTENT_X + 550, GlobalConstants.INSTRUCTIONS_CONTENT_Y + textSpacing);
                textSpacing += textSpacingIncrementer;
                SwinGame.DrawText("To mine tiles", Color.Black, GlobalConstants.INSTRUCTIONS_CONTENT_X + 290, GlobalConstants.INSTRUCTIONS_CONTENT_Y + textSpacing + 50);
                textSpacing += textSpacingIncrementer * 5;
                SwinGame.DrawBitmap(GameResources.GameImage("wasd_g_key"), GlobalConstants.INSTRUCTIONS_CONTENT_X - 6, GlobalConstants.INSTRUCTIONS_CONTENT_Y + textSpacing);
                SwinGame.DrawBitmap(GameResources.GameImage("arrow_key_L"), GlobalConstants.INSTRUCTIONS_CONTENT_X + 544, GlobalConstants.INSTRUCTIONS_CONTENT_Y + textSpacing);
                SwinGame.DrawText("To place mined tiles", Color.Black, GlobalConstants.INSTRUCTIONS_CONTENT_X + 280, GlobalConstants.INSTRUCTIONS_CONTENT_Y + textSpacing + 100);
            }
        }

        public void SinglePlayerMode()
        {
            SwinGame.PlayMusic(GameResources.GameMusic("game_music"));
            if (!firstGame)
            {
                _player.Location.X = 7;
                _player.Location.Y = 5;
                _enemy.Location.X = 25;
                _enemy.Location.Y = 20;
                GetWorld().GenerateMap();
            }
            PreInit();
            Init();
            PostInit();
            PrepareExtraComponents();
            Run();
        }

        bool _twoplayer = false;//if false, player 2 object wont be created - jeremy toh
        public void TwoPlayerMode()
        {
            SwinGame.PlayMusic(GameResources.GameMusic("game_music"));
            _twoplayer = true;

            if (!firstGame)
            {
                _player.Location.X = 7;
                _player.Location.Y = 5;
                _enemy.Location.X = 25;
                _enemy.Location.Y = 20;
                GetWorld().GenerateMap();
            }

            PreInit();
            Init();
            PostInit();
            PrepareExtraComponents();
            Run();
        }

        public void EndGame(string status)
        {
            SwinGame.PlayMusic(GameResources.GameMusic("main_menu_music"));
            bool proceed = false;

            // IA - Reset minerals, game levels, food purchased and energy levels.
            Food.SetBalance(0);
            Food.ResetFoodPurchased();
            Food.ResetMineralValue();
            Food.ResetEnergyValue();
            MetaHandler.ResetEnergyLevels();
            MetaHandler.SetFoodMessage("Hit Space to enter amount.");
            MetaHandler.ResetAmmunitionMessage1();
            MetaHandler.ResetAmmunitionMessage2();
            GameLevel.SetLevel(1);
            EnemiesToBeRemoved.Clear();
            GetPlayer().DestroyWeapon();

            // IA - Clear the input field from the Food Exchange Center
            if (Input.ReadingText())
            {
                Input.EndReadingText();
            }


            GetPlayer().Inventory.ClearInventory();
            if (_twoplayer == true)
            {
                _player2.Inventory.ClearInventory();
                _player2.DestroyWeapon();
            }
            

            while (!proceed)
            {
                SwinGame.ClearScreen(Color.White);
                SwinGame.DrawBitmap(GameResources.GameImage(status), 0, 0);

                SwinGame.ProcessEvents();

                if (SwinGame.MouseClicked(MouseButton.LeftButton))
                {
                    if (_twoplayer == true)
                    {
                        _player2.Location.X = 45;
                        _player2.Location.Y = 30;
                    }
                    proceed = true;
                    initdone = false;
                    initdone2 = false;
                    initPlayer2 = false;
                    _twoplayer = false;
                    firstGame = false;
                }

                SwinGame.RefreshScreen(60);
            }
            SwinGame.ClearScreen(Color.White);
            _gameStates.Push(GameState.ViewingMainMenu);
            ControlGameState();
        }

        public void QuitGame()
        {
            GameResources.FreeResources();
            SwinGame.ReleaseAllBitmaps();
        }


        /// <summary>
        /// This method creates all the extra (custom) components or objects needed in the game.
        /// </summary>
        private void PrepareExtraComponents()
        {
            MetaHandler.timer.StartTimer();
        }

        /// <summary>
        /// Initialisation stage 1 - initialise base objects
        /// </summary>
        public void PreInit()
        {
            Objects = new List<Entity>();
            ProjectilesToBeRemoved = new List<Projectile>();
            EnemiesToBeRemoved = new List<Enemy>();
            SpawnedEnemies = new List<Enemy>();
        }

        /// <summary>
        /// Initialisation stage 2 - initialise objects dependent on stage 1
        /// </summary>

        bool initdone = false;
        bool initdone2 = false;
        bool initPlayer2 = false;
        public void Init()
        {
            if (initPlayer2 == false && _twoplayer == true)
            {
                Objects.Add(GetPlayer2());
                initPlayer2 = true;
            }

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
            helpList.Add("B, LShift+B(P1), P, P+J(P2): Buy Weapon");
            helpList.Add("F(P1), K(P2): On rock tile - dig rock");
            helpList.Add("G(P1), L(P2): On air - place rock");
            helpList.Add("V(P1), O(P2): Shoot ");
            helpList.Add("~ Keyboard controls ~");
            helpList.Add("E - Generate new minerals");
            helpList.Add("M - Generate a new map(removed)");
            helpList.Add("H - Toggle this frame");
            helpList.Add("Q - Toggle inventory frame");
            helpList.Add("Esc - Quit game");

            helpFrame.Content = helpList;

        }

        /// <summary>
        /// Run the game loop until the window is closed or the ESC key is pressed
        /// </summary>
        public void Run()
        {
            while (!SwinGame.WindowCloseRequested() && _gameStates.Peek() != GameState.QuittingGame)
            {

                SwinGame.ClearScreen(Color.White);
                SwinGame.ProcessEvents();

                if (SwinGame.KeyTyped (KeyCode.EscapeKey))
                  break;

                Update();
                Draw();

                SwinGame.RefreshScreen(30);
            }
            _gameStates.Push(GameState.QuittingGame);
            ControlGameState();
        }

        int randomhit = 0;
        /// <summary>
        /// Update the game based on events, objects and other things
        /// </summary>
        public void Update()
        {
			randomhit++;

            //spawn rate - jeremy 
            if (randomhit >= 200)
            {
                Init();
                randomhit = 0;
            }

            // JY- detect projectile hits, projectile and enemies will be deleted
            foreach (Entity e in Objects)
            {
                if (e is Projectile)
                {

                    if (_twoplayer)
                    {
                        if (_player.PlayerHitbyProjectile((Projectile)e))
                        {
                            _gameStates.Push(GameState.PlayerTwoWins);
                            ControlGameState();
                        }
                        else if (_player2.PlayerHitbyProjectile((Projectile)e))
                        {
                            _gameStates.Push(GameState.PlayerOneWins);
                            ControlGameState();
                        }
                    }
                    else
                    {
                        if (_player.PlayerHitbyProjectile((Projectile)e))
                        {
                            _gameStates.Push(GameState.SinglePlayerEndGame);
                            ControlGameState();
                        }
                    }

                    if (SpawnedEnemies.Count > 0)
                    {
                        foreach (Enemy _e in SpawnedEnemies)
                        {
                            if (((Projectile)e).CheckObjectHit(_world, _e))
                                ProjectilesToBeRemoved.Add((Projectile)e);

                            if (_e.CheckHit((Projectile)e))
                                EnemiesToBeRemoved.Add(_e);

                        }
                    }
                    else
                      if (((Projectile)e).CheckObjectHit(_world, _enemy))
                        ProjectilesToBeRemoved.Add((Projectile)e);
                }
                //JY- detect enemies hitting players
                else if (e is Enemy)
                {
                    if (_twoplayer)
                    {
                        if (_player.PlayerHitbyEnemy((Enemy)e))
                        {
                            _gameStates.Push(GameState.PlayerTwoWins);
                            //_player.PlayerHitbyEnemy((Enemy)e) = !_player.PlayerHitbyEnemy((Enemy)e);
                            ControlGameState();
                        }
                        else if (_player2.PlayerHitbyEnemy((Enemy)e))
                        {
                            _gameStates.Push(GameState.PlayerOneWins);
                            ControlGameState();
                        }
                    }
                    else
                    {
                        if (_player.PlayerHitbyEnemy((Enemy)e))
                        {
                            _gameStates.Push(GameState.SinglePlayerEndGame);
                            ControlGameState();
                        }
                    }
                }
            }

            foreach (Projectile p in ProjectilesToBeRemoved)
                Objects.Remove(p);

            foreach (Enemy en in EnemiesToBeRemoved)
            {
                if (SpawnedEnemies.Contains(en))
                    SpawnedEnemies.Remove(en);

                Objects.Remove(en);
            }

            foreach (Entity obj in Objects)
            {
                obj.Update();
            }


            //PLAYER 1 MINE/BUILD ROCKS INPUT
            //mine rocks/minerals right
            // IA - Rewritten the mining logic for the single player
            if (SwinGame.KeyDown(KeyCode.FKey))
            {
                int x = _player.Location.X;
                int y = _player.Location.Y;
                if (SwinGame.KeyReleased(KeyCode.DKey))
                {
                    x += 1;
                }
                else if (SwinGame.KeyReleased(KeyCode.SKey))
                {
                    y += 1;
                }
                else if (SwinGame.KeyReleased(KeyCode.AKey))
                {
                    x -= 1;
                }
                else if (SwinGame.KeyReleased(KeyCode.WKey))
                {
                    y -= 1;
                }

                try
                {
					if (_world.Map[x, y].Type == TileType.Rock && x != 0 && y != 0 && x != 52 && y != 36)
                    {
                        _world.Map[x, y].Type = TileType.Air;
                        if (_world.Map[x, y].Mineral != null)
                        {
                            GetPlayer().Inventory.AddItem(_world.Map[x, y].Mineral);
                        }
                    }
                }
                catch (Exception e)
                {
                    // IA - in case of an exception, force the reassignment of x and y using the player's location.
                    x = _player.Location.X;
                    y = _player.Location.Y;
                }
            }

            //build rock right
            if (SwinGame.KeyDown(KeyCode.DKey) && SwinGame.KeyDown(KeyCode.GKey))
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
                        _world.Map[x, y] = new Tile(TileType.Rock);
                    }
                }
            }

            //build rock left
            if (SwinGame.KeyDown(KeyCode.AKey) && SwinGame.KeyDown(KeyCode.GKey))
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
            if (SwinGame.KeyDown(KeyCode.WKey) && SwinGame.KeyDown(KeyCode.GKey))
            {
                //GetEnvironment ().HandleGuiEvent (GuiEvent.MouseRight, new Location (_player.Location.X+1, _player.Location.Y));
                GetEnvironment();
                int x = (_player.Location.X);
                int y = (_player.Location.Y - 1);
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
            if (SwinGame.KeyDown(KeyCode.SKey) && SwinGame.KeyDown(KeyCode.GKey))
            {
                //GetEnvironment ().HandleGuiEvent (GuiEvent.MouseRight, new Location (_player.Location.X+1, _player.Location.Y));
                GetEnvironment();
                int x = (_player.Location.X);
                int y = (_player.Location.Y + 1);
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

            if (_twoplayer == true)
            {
                //PLAYER 2 MINE/BUILD ROCKS INPUT
                //mine rocks/minerals right
				 if (SwinGame.KeyDown(KeyCode.KKey))
           	 	{
                int i = _player2.Location.X;
				int j = _player2.Location.Y;
					if (SwinGame.KeyReleased(KeyCode.RightKey))
                {
                    i += 1;
                }
					else if (SwinGame.KeyReleased(KeyCode.DownKey))
                {
                    j += 1;
                }
					else if (SwinGame.KeyReleased(KeyCode.LeftKey))
                {
                    i -= 1;
                }
					else if (SwinGame.KeyReleased(KeyCode.UpKey))
                {
                    j -= 1;
                }

                try
                {
					if (_world.Map[i, j].Type == TileType.Rock && i != 0 && j != 0 && i != 52 && j != 36)
                    {
                        _world.Map[i, j].Type = TileType.Air;
                        if (_world.Map[i, j].Mineral != null)
                        {

							_player2.Inventory.AddItem(_world.Map[i, j].Mineral);
                        }
                    }
                }
                catch (Exception e)
                {
                    // IA - in case of an exception, force the reassignment of x and y using the player's location.
                    i = _player2.Location.X;
                    j = _player2.Location.Y;
                }
            }

                //build rock right
                if (SwinGame.KeyDown(KeyCode.RightKey) && SwinGame.KeyDown(KeyCode.LKey))
                {
                    //GetEnvironment ().HandleGuiEvent (GuiEvent.MouseRight, new Location (_player.Location.X+1, _player.Location.Y));
                    GetEnvironment();
                    int x = (_player2.Location.X + 1);
                    int y = (_player2.Location.Y);
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

                //build rock left
                if (SwinGame.KeyDown(KeyCode.LeftKey) && SwinGame.KeyDown(KeyCode.LKey))
                {
                    //GetEnvironment ().HandleGuiEvent (GuiEvent.MouseRight, new Location (_player.Location.X+1, _player.Location.Y));
                    GetEnvironment();
                    int x = (_player2.Location.X - 1);
                    int y = (_player2.Location.Y);
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
                if (SwinGame.KeyDown(KeyCode.UpKey) && SwinGame.KeyDown(KeyCode.LKey))
                {
                    //GetEnvironment ().HandleGuiEvent (GuiEvent.MouseRight, new Location (_player.Location.X+1, _player.Location.Y));
                    GetEnvironment();
                    int x = (_player2.Location.X);
                    int y = (_player2.Location.Y - 1);
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
                if (SwinGame.KeyDown(KeyCode.DownKey) && SwinGame.KeyDown(KeyCode.LKey))
                {
                    //GetEnvironment ().HandleGuiEvent (GuiEvent.MouseRight, new Location (_player.Location.X+1, _player.Location.Y));
                    GetEnvironment();
                    int x = (_player2.Location.X);
                    int y = (_player2.Location.Y + 1);
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
            }

            if (SwinGame.KeyDown(KeyCode.EscapeKey))
            {
                Environment.Exit(0);
            }

            //PLAYER 1 KEY MOVEMENT/WEAPON INPUT
            //changes player input using keyboard - jeremy
            //Allowed player to attack,
            //else if if used so that the player can spam attack and move at the same time - Jonathan

            // buy weapon
            if (SwinGame.KeyDown(KeyCode.VKey) && SwinGame.KeyDown(KeyCode.WKey))
            {
                if (_player.Weapon != null && _player.Weapon.Ammunition > 0)
                {
                    _player.DeployWeapon(AttackDirection.Up);
                    Objects.Add(_player.Weapon.Projectile);
                }
            }
            else if (SwinGame.KeyDown(KeyCode.WKey) && _world.Map[_player.Location.X, _player.Location.Y - 1].Type != TileType.Rock && _player.Location.Y - 1 != 0)
            {
                _player.Location.Y -= 1;
            }

            if (SwinGame.KeyDown(KeyCode.VKey) && SwinGame.KeyDown(KeyCode.SKey))
            {
                if (_player.Weapon != null && _player.Weapon.Ammunition > 0)
                {
                    _player.DeployWeapon(AttackDirection.Down);
                    Objects.Add(_player.Weapon.Projectile);
                }
            }
            else if (SwinGame.KeyDown(KeyCode.SKey) && _world.Map[_player.Location.X, _player.Location.Y + 1].Type != TileType.Rock && _player.Location.Y + 1 != 36)
            {
                _player.Location.Y += 1;
            }

            if (SwinGame.KeyDown(KeyCode.VKey) && SwinGame.KeyDown(KeyCode.AKey))
            {
                if (_player.Weapon != null && _player.Weapon.Ammunition > 0)
                {
                    _player.DeployWeapon(AttackDirection.Left);
                    Objects.Add(_player.Weapon.Projectile);
                }
            }
            else if (SwinGame.KeyDown(KeyCode.AKey) && _world.Map[_player.Location.X - 1, _player.Location.Y].Type != TileType.Rock && _player.Location.X - 1 != 0)
            {
                _player.Location.X -= 1;
            }

            if (SwinGame.KeyDown(KeyCode.VKey) && SwinGame.KeyDown(KeyCode.DKey))
            {
                if (_player.Weapon != null && _player.Weapon.Ammunition > 0)
                {
                    _player.DeployWeapon(AttackDirection.Right);
                    Objects.Add(_player.Weapon.Projectile);
                }
            }
            else if (SwinGame.KeyDown(KeyCode.DKey) && _world.Map[_player.Location.X + 1, _player.Location.Y].Type != TileType.Rock && _player.Location.X + 1 != 52)
            {
                _player.Location.X += 1;
            }

            if (SwinGame.KeyTyped(KeyCode.BKey) && !SwinGame.KeyDown(KeyCode.LeftShiftKey))
            {
                if (GetPlayer().Inventory.GetMineralPoints() >= 20)
                {
                    if (_player.Weapon == null)
                    {
                        _player.BuyWeapon(_player.Location, WeaponType.Normal);
                    }
                    if (_player.Weapon.Type == WeaponType.Super)
                    {
                        _player.BuyWeapon(_player.Location, WeaponType.Normal);
                    }
                    _player.Weapon.Ammunition += 30;
                    // Objects.Add(_player.Weapon);
                    _player.Inventory.DeductMineralPoints(WeaponType.Normal);
                }

            }

            if (SwinGame.KeyTyped(KeyCode.BKey) && SwinGame.KeyDown(KeyCode.LeftShiftKey))
            {
                if (GetPlayer().Inventory.GetMineralPoints() >= 30)
                {
                    if (_player.Weapon == null)
                    {
                        _player.BuyWeapon(_player.Location, WeaponType.Super);
                    }
                    if (_player.Weapon.Type == WeaponType.Normal)
                    {
                        _player.BuyWeapon(_player.Location, WeaponType.Super);
                    }
                    _player.Weapon.Ammunition += 30;
                    // Objects.Add(_player.Weapon);
                    _player.Inventory.DeductMineralPoints(WeaponType.Super);
                }
            }

            if (_twoplayer == true)
            {
                //PLAYER 2 MOVEMENT/WEAPON KEY INPUT
                if (SwinGame.KeyDown(KeyCode.OKey) && SwinGame.KeyDown(KeyCode.UpKey))
                {
                    if (_player2.Weapon != null && _player2.Weapon.Ammunition > 0)
                    {
                        _player2.DeployWeapon(AttackDirection.Up);
                        Objects.Add(_player2.Weapon.Projectile);
                    }
                }
                else if (SwinGame.KeyDown(KeyCode.UpKey) && _world.Map[_player2.Location.X, _player2.Location.Y - 1].Type != TileType.Rock && _player2.Location.Y - 1 != 0)
                {
                    _player2.Location.Y -= 1;
                }

                if (SwinGame.KeyDown(KeyCode.OKey) && SwinGame.KeyDown(KeyCode.DownKey))
                {
                    if (_player2.Weapon != null && _player2.Weapon.Ammunition > 0)
                    {
                        _player2.DeployWeapon(AttackDirection.Down);
                        Objects.Add(_player2.Weapon.Projectile);
                    }
                }
                else if (SwinGame.KeyDown(KeyCode.DownKey) && _world.Map[_player2.Location.X, _player2.Location.Y + 1].Type != TileType.Rock && _player2.Location.Y + 1 != 36)
                {
                    _player2.Location.Y += 1;
                }

                if (SwinGame.KeyDown(KeyCode.OKey) && SwinGame.KeyDown(KeyCode.LeftKey))
                {
                    if (_player2.Weapon != null && _player2.Weapon.Ammunition > 0)
                    {
                        _player2.DeployWeapon(AttackDirection.Left);
                        Objects.Add(_player2.Weapon.Projectile);
                    }
                }
                else if (SwinGame.KeyDown(KeyCode.LeftKey) && _world.Map[_player2.Location.X - 1, _player2.Location.Y].Type != TileType.Rock && _player2.Location.X - 1 != 0)
                {
                    _player2.Location.X -= 1;
                }

                if (SwinGame.KeyDown(KeyCode.OKey) && SwinGame.KeyDown(KeyCode.RightKey))
                {
                    if (_player2.Weapon != null && _player2.Weapon.Ammunition > 0)
                    {
                        _player2.DeployWeapon(AttackDirection.Right);
                        Objects.Add(_player2.Weapon.Projectile);
                    }
                }
                else if (SwinGame.KeyDown(KeyCode.RightKey) && _world.Map[_player2.Location.X + 1, _player2.Location.Y].Type != TileType.Rock && _player2.Location.X + 1 != 52)
                {
                    _player2.Location.X += 1;
                }

                // Player 2 buys a weapon
                if (SwinGame.KeyTyped(KeyCode.PKey) && !SwinGame.KeyDown(KeyCode.JKey))
                {
                    if (_player2.Inventory.GetMineralPoints() >= 20)
                    {
                        if (_player2.Weapon == null)
                        {
                            _player2.BuyWeapon(_player.Location, WeaponType.Normal);
                        }
                        if (_player2.Weapon.Type == WeaponType.Super)
                        {
                            _player2.BuyWeapon(_player.Location, WeaponType.Normal);
                        }
                        _player2.Weapon.Ammunition += 20;
                        // Objects.Add(_player.Weapon);
                        _player2.Inventory.DeductMineralPoints(WeaponType.Normal);
                    }

                }


                if (SwinGame.KeyTyped(KeyCode.PKey) && SwinGame.KeyDown(KeyCode.JKey))
                {
                    if (_player2.Inventory.GetMineralPoints() >= 30)
                    {
                        if (_player2.Weapon == null)
                        {
                            _player2.BuyWeapon(_player.Location, WeaponType.Super);
                        }
                        if (_player2.Weapon.Type == WeaponType.Normal)
                        {
                            _player2.BuyWeapon(_player.Location, WeaponType.Super);
                        }
                        _player2.Weapon.Ammunition += 40;
                        // Objects.Add(_player.Weapon);
                        _player2.Inventory.DeductMineralPoints(WeaponType.Super);
                    }
                }

            }

			// Accept the spawning of minerals on demand - Useful for demonstration purposes only (to make it easier to collect mineral points, buy food, weapons, etc.)
			// if (SwinGame.KeyTyped(KeyCode.EKey))
			// {
				// GetWorld().PutMinerals();         
			// }

            // IA - After computing everything, determine when/if the level should be increased.
            ControlLevels();
            MetaHandler.DisplayFoodExchange(GetPlayer().Inventory);

            // IA - End the game when the player's energy level reaches 0.
            if (MetaHandler.GetEnergyLevel() == 0)
            {
                EndGame("you_died");
            }

        }

        /// <summary>
        /// Get the current renderer to draw the game
        /// </summary>
        public void Draw()
        {
            GuiEnvironment.GetRenderer().RenderWindow();

            MetaHandler.ShowPanels(); // IA - Make the panel visible

            // IA - Single Player Mode only.
            if (_gameStates.Peek() == GameState.SinglePlayerMode)
            {

                MetaHandler.DisplayHungerInformation(); // IA - Show the hunger level progress bar and messages
                MetaHandler.DisplayTimer(); // IA - Make the timer visible
                MetaHandler.DisplayGameLevel(); // IA - Display the game level
                MetaHandler.DisplayAmmunitionLevel(_player.Weapon); // IA - Display info about amminutions (type and amount)
                MetaHandler.DisplayEnemiesInfo(SpawnedEnemies, EnemiesToBeRemoved); // IA - Display how many enemies have been destroyed.
                MetaHandler.DrawFoodField(GetPlayer().Inventory);
            }

            // IA - Only display the worth of minerals while the game runs.
            if (_gameStates.Peek() == GameState.SinglePlayerMode)
            {
                MetaHandler.DisplayRate(GetPlayer().Inventory.GetTotalValue(), GetPlayer().Inventory);
            }

            // IA - Two Players Mode only.
            if (_gameStates.Peek() == GameState.TwoPlayerMode)
            {
                MetaHandler.DisplayTwoPlayersInstructions();
                MetaHandler.DisplaySinglePlayerWeaponInfo(_player, "Player 1", 15);
                MetaHandler.DisplaySinglePlayerWeaponInfo(_player2, "Player 2", 85);
                MetaHandler.DisplayRate(_player.Inventory.GetTotalValue(), _player.Inventory, "PLAYER 1", -190, Color.Wheat);
                MetaHandler.DisplayRate(_player2.Inventory.GetTotalValue(), _player2.Inventory, "PLAYER 2", 10, Color.Yellow);
            }
        }
    }
}
