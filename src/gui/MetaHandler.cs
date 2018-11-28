using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Escapade.item;
using SwinGameSDK;

/// <summary>
/// This class under the GUI folder takes cares of handling the bottom section of the screen, which contains the countdown and level information.
/// </summary>
namespace Escapade.src.gui
{
    /// <summary>
    /// This class takes care of everything that is displayed on the screen, on panels, around the game. 
	/// This includes the bottom pannel which shows the timer, ammunition messages, energy progress bar and tips, and game the game level. 
	/// It also includes the sidebar panels showing information on enemies left to destroy in a level, mineral points, and the Food Exchange Center components.
	/// [Added by Isaac]
    /// </summary>
    public static class MetaHandler
    {
		// Preparing for the input field and output messages in the Food Exchange Center
        private static Rectangle _foodField = SwinGame.CreateRectangle(810, 560, 150, 30);
        private static String _foodMessage;

		//Prepare ammunition messages
        private static String _ammunitionMessage1;
        private static String _ammunitionMessage2;

		// Load fonts
        private static Font openSansExtraBoldLarge = SwinGame.LoadFont("OpenSans-ExtraBold", 14);
        private static Font openSansExtraBoldNormal = SwinGame.LoadFont("OpenSans-ExtraBold", 12);
        private static Font openSansExtraBoldSmall = SwinGame.LoadFont("OpenSans-ExtraBold", 10);
        private static Font openSansExtraBoldItalicNormal = SwinGame.LoadFont("OpenSans-ExtraBoldItalic", 12);
        private static Font openSansBoldLarge = SwinGame.LoadFont("OpenSans-Bold", 20);
        private static Font openSansBoldItalicNormal = SwinGame.LoadFont("OpenSans-BoldItalic", 12);

		// Load panels
        public static Panel bottomPanel = SwinGame.LoadPanelNamed("Bottom Panel", "Meta.txt"); // IA - this will hold the panel resource (meta.txt)
        public static Panel enemiesPanel = SwinGame.LoadPanelNamed("Enemies Panel", "Extra1.txt");
        public static Panel inventoryPanel = SwinGame.LoadPanelNamed("Inventory Panel", "Extra2.txt");
        public static Panel foodPanel = SwinGame.LoadPanelNamed("Food Panel", "Extra3.txt");

		// Define common positions of text in the panels
        private static int contentFirstLine = GlobalConstants.WORLD_HEIGHT + 18;
        private static int contentSecondLine = GlobalConstants.WORLD_HEIGHT + 48;
        private static int contentRightAlign = GlobalConstants.WORLD_WIDTH + 5;

		// Create the timer
        public static Countdown timer = new Countdown();

		// Prepare for the display of the energy progress bar and its additional messages
        public static String hungerIndication;
        public static double hungerIndicatorWidth = 150;
        public static Color hungerIndicatorColor;

		/// <summary>
		/// Gets the first ammunition message to display.
		/// </summary>
		/// <returns>The first ammunition message as a string.</returns>
        public static string GetAmmunitionMessage1()
        {
            return _ammunitionMessage1;
        }

		/// <summary>
		/// Resets the first ammunition message to what should be its default string value.
		/// </summary>
        public static void ResetAmmunitionMessage1()
        {
            _ammunitionMessage1 = "Press B or Shift + B to buy weapons.";
        }

		/// <summary>
		/// Gets the second ammunition message to display.
		/// </summary>
		/// <returns>The second ammunition message as a string.</returns>
        public static string GetAmmunitionMessage2()
        {
            return _ammunitionMessage2;
        }

        public static void ResetAmmunitionMessage2()
        {
            _ammunitionMessage2 = "";
        }

		/// <summary>
		/// This method serves to convert the width of the energy progress bar into energy levels measured in percentages.
		/// </summary>
		/// <returns>An integer corresponding to the percentage of energy value corresponding to the passed width value.</returns>
		/// <param name="indicator">The width of the progress bar, also called the hunger indicator.</param>
        public static int Map(double indicator)
        {
            return (int)Math.Ceiling(indicator / 1.5);
        }

		/// <summary>
		/// Gets the player's energy level as an integer corresponding to the percentage of energy left.
		/// </summary>
		/// <returns>The player's energy level in percentages.</returns>
        public static int GetEnergyLevel()
        {
            return Map(hungerIndicatorWidth);
        }

		/// <summary>
		/// Decreases the player's energy level, very slowly.
		/// </summary>
        public static void DecreaseEnergy()
        {
            hungerIndicatorWidth -= 0.02;
        }

		/// <summary>
		/// Resets the player's energy levels to 100%.
		/// </summary>
        public static void ResetEnergyLevels()
        {
            hungerIndicatorWidth = 150;
        }

		/// <summary>
		/// Sets the output message in the Food Exchange Center's panel.
		/// </summary>
		/// <param name="message">Message.</param>
        public static void SetFoodMessage(String message)
        {
            _foodMessage = message;
        }

		/// <summary>
		/// Increases the player's energy based on the value passed as a parameter.
		/// </summary>
		/// <param name="convertedFood">Converted food.</param>
        public static void IncreaseEnergy(double convertedFood)
        {
            double energyToAdd = Math.Floor(convertedFood * 1.5); // Transfer the energy into width for the hunger progress bar.
            if (Food.EnergyNeededInPercentage() * 1.5 >= energyToAdd)
            {
                hungerIndicatorWidth += energyToAdd;
            } else
            {
                hungerIndicatorWidth = 150;
            }
        }
        
		/// <summary>
		/// Controls the informative messages educating the player on their hunger level.
		/// </summary>
        public static void ControlLevelDisplay()
        {
            if (hungerIndicatorWidth > 0)
            {
                DecreaseEnergy();
            }

            if (Map(hungerIndicatorWidth) < 80 && Map(hungerIndicatorWidth) >= 60)
            {
                hungerIndicatorColor = Color.YellowGreen;
                hungerIndication = "Start looking for food.";
            }
            else if (Map(hungerIndicatorWidth) < 60 && Map(hungerIndicatorWidth) >= 40)
            {
                hungerIndicatorColor = Color.Yellow;
                hungerIndication = "Your hunger level is rising.";
            }
            else if (Map(hungerIndicatorWidth) < 40 && Map(hungerIndicatorWidth) >= 20)
            {
                hungerIndicatorColor = Color.Orange;
                hungerIndication = "Running low on energy.";
            }
            else if (Map(hungerIndicatorWidth) < 20 && Map(hungerIndicatorWidth) > 0)
            {
                hungerIndicatorColor = Color.Red;
                hungerIndication = "Get food now! (Almost game over)";
            }
            else if (Map(hungerIndicatorWidth) == 0)
            {
                hungerIndication = "No more energy left. Game Over.";
            } else
            {
                hungerIndication = "You're in shape!";
                hungerIndicatorColor = Color.LawnGreen;
            }
        }

        /// <summary>
        /// This method calls all the methods required to make the panels visible on the screen with the intended background color.
        /// </summary>
        public static void ShowPanels()
        {
            SwinGame.GUISetBackgroundColor(Color.Black);
            SwinGame.ShowPanel(bottomPanel);
            SwinGame.ShowPanel(enemiesPanel);
            SwinGame.ShowPanel(inventoryPanel);
            SwinGame.ShowPanel(foodPanel);
            SwinGame.DrawInterface();
        }

        /// <summary>
		/// This method pulls the running time information from the Countdown object.
        /// </summary>
        public static void DisplayTimer()
        {
            SwinGame.DrawText("Time: " + timer.ShowTime(), Color.White, openSansExtraBoldLarge, 20, contentFirstLine);
        }

		/// <summary>
		/// Displays the information about the player's energy levels, which are connected to their hunger, and also the amount of food to purchase.
		/// </summary>
        public static void DisplayHungerInformation()
        {
            ControlLevelDisplay();

            SwinGame.DrawText("Your energy levels: ", Color.White, openSansExtraBoldNormal, 290, contentFirstLine - 3);

            SwinGame.FillRectangle(hungerIndicatorColor, 420, GlobalConstants.WORLD_HEIGHT + 12, (int)hungerIndicatorWidth, 20);

            SwinGame.DrawText(Map(hungerIndicatorWidth).ToString() + "% ", hungerIndicatorColor, openSansExtraBoldNormal, 420 + (int)hungerIndicatorWidth + 5, contentFirstLine);

            SwinGame.DrawText(hungerIndication, hungerIndicatorColor, openSansExtraBoldNormal, 420, GlobalConstants.WORLD_HEIGHT + 37);

            SwinGame.DrawText("Up to " + Food.FoodNeededInKG().ToString() + "kg of food needed for top energy.", Color.White, openSansBoldItalicNormal, 420, GlobalConstants.WORLD_HEIGHT + 55);
        }

        /// <summary>
        /// Displays the game's actual level.
        /// </summary>
        public static void DisplayGameLevel()
        {
            SwinGame.DrawText("Level: " + GameLevel.PrintLevel(), Color.White, openSansBoldLarge, GlobalConstants.WORLD_WIDTH - 120, contentFirstLine);
        }

		/// <summary>
		/// Displays the relevant ammunition messages, based on the player's Weapon state.
		/// </summary>
		/// <param name="weapon">Weapon.</param>
        public static void DisplayAmmunitionLevel(Weapon weapon)
        {
            if (weapon != null)
            {
                if (weapon.Ammunition > 0)
                {
                    {
                        _ammunitionMessage1 = "AMMUNITION AVAILABLE: " + weapon.Ammunition.ToString();
                        _ammunitionMessage2 = "AMMUNITION TYPE: " + weapon.Type.ToString();
                    }
                } else
                {
                    _ammunitionMessage1 = "You've ran out of ammo!";
                    _ammunitionMessage2 = "Gather minerals to buy more weapons!";
                }
            }
            else
            {
                ResetAmmunitionMessage1();
                ResetAmmunitionMessage2();
            }

            SwinGame.DrawText(_ammunitionMessage1, Color.Yellow, openSansExtraBoldNormal, 20, contentSecondLine);
            SwinGame.DrawText(_ammunitionMessage2, Color.Yellow, openSansExtraBoldNormal, 20, contentSecondLine + 15);
        }

		/// <summary>
		/// Displaies weapon/ammunition messages for a single player. This method should be used in Two Player Mode.
		/// </summary>
		/// <param name="player">The player for which the information should be displayed. It must be a Player object.</param>
		/// <param name="playerName">The string referring to the player's name.</param>
		/// <param name="Y">The additional space the messages should have above them.</param>
        public static void DisplaySinglePlayerWeaponInfo(Player player, String playerName, int Y)
        {
            if (player.Weapon != null)
            {
                SwinGame.DrawText(playerName, Color.White, openSansExtraBoldLarge, contentRightAlign, 450 + Y);

                SwinGame.DrawText("Projectiles left: " + player.Weapon.Ammunition.ToString(), Color.WhiteSmoke, openSansExtraBoldNormal, contentRightAlign, 470 + Y);
            }
        }

		/// <summary>
		/// Displays the number of red enemies roaming in real time, the number of successful hits, and the number left to destroy to move to the next level.
		/// </summary>
		/// <param name="existingEnemies">A list of Enemy objects, holding enemies available in the game.</param>
		/// <param name="enemiesHit">A list of Enemy objects, holding enemies that have been destroyed.</param>
        public static void DisplayEnemiesInfo(List<Enemy> existingEnemies, List<Enemy> enemiesHit)
        {
            SwinGame.DrawText("ENEMIES IN THIS LEVEL", Color.Yellow, openSansExtraBoldLarge, contentRightAlign, 15);

            SwinGame.DrawText("Red Enemies: " + existingEnemies.Count.ToString(), Color.Yellow, openSansExtraBoldNormal, contentRightAlign, 45);

            SwinGame.DrawText("Successful hits: " + enemiesHit.Count.ToString(), Color.Yellow, openSansExtraBoldNormal, contentRightAlign, 70);

            SwinGame.DrawText("Destroy " + (GameLevel.GetFormula() - enemiesHit.Count).ToString() + " more enemies.", Color.Yellow, openSansExtraBoldNormal, contentRightAlign, 100);
        }

		/// <summary>
		/// Displays the amount of mineral points the player has in total, and also by type.
		/// </summary>
		/// <param name="mineralWorth">The player's mineral worth by type, as a double array.</param>
		/// <param name="inventory">The player's inventory holding all collected minerals.</param>
        public static void DisplayRate(double[] mineralWorth, Inventory inventory)
        {
            // IA - Display the total value of minerals while the game runs
            SwinGame.DrawText("YOU HAVE " + inventory.ItemList.Count.ToString() + " MINERALS", Color.White, openSansExtraBoldLarge, contentRightAlign, 210);

            SwinGame.DrawText("Diamond points:  " + mineralWorth[0].ToString(), Color.White, openSansExtraBoldNormal, contentRightAlign, 240);

            SwinGame.DrawText("Emerald points:  " + mineralWorth[1].ToString(), Color.White, openSansExtraBoldNormal, contentRightAlign, 265);

            SwinGame.DrawText("Ruby points:  " + mineralWorth[2].ToString(), Color.White, openSansExtraBoldNormal, contentRightAlign, 290);

            SwinGame.DrawText("Sapphire points:  " + mineralWorth[3].ToString(), Color.White, openSansExtraBoldNormal, contentRightAlign, 315);

            SwinGame.DrawText("Total points:  " + inventory.GetMineralPoints(), Color.White, openSansExtraBoldNormal, contentRightAlign, 360);

            SwinGame.DrawText("(Balance not included)", Color.White, openSansBoldItalicNormal, contentRightAlign, 385);

        }

		/// <summary>
		/// Displays the amount of mineral points a player has in total, with a player's name.
		/// </summary>
		/// <param name="mineralWorth">The player's mineral points by type, as a double array.</param>
		/// <param name="inventory">The player's inventory holding all collected minerals.</param>
		/// <param name="playerName">The player's name as a string.</param>
		/// <param name="Y">The additional amount of space to have above the messages.</param>
		/// <param name="color">The color of the messages.</param>
        public static void DisplayRate(double[] mineralWorth, Inventory inventory, String playerName, int Y, Color color)
        {

            // IA - Display the total value of minerals while the game runs
            SwinGame.DrawText(playerName + " HAS " + inventory.ItemList.Count.ToString() + " MINERALS", color, openSansExtraBoldNormal, contentRightAlign, 210 + Y);

            SwinGame.DrawText("Diamond points:  " + mineralWorth[0].ToString(), color, openSansExtraBoldNormal, contentRightAlign, 240 + Y);

            SwinGame.DrawText("Emerald points:  " + mineralWorth[1].ToString(), color, openSansExtraBoldNormal, contentRightAlign, 265 + Y);

            SwinGame.DrawText("Ruby points:  " + mineralWorth[2].ToString(), color, openSansExtraBoldNormal, contentRightAlign, 290 + Y);

            SwinGame.DrawText("Sapphire points:  " + mineralWorth[3].ToString(), color, openSansExtraBoldNormal, contentRightAlign, 315 + Y);

            SwinGame.DrawText("Total points:  " + inventory.GetMineralPoints(), color, openSansExtraBoldNormal, contentRightAlign, 360 + Y);

        }

		/// <summary>
		/// Displays output messages of minerals-to-food transactions.
		/// </summary>
		/// <param name="inventory">The player's inventory holding all collected minerals that will be used for minerals-to-food transactions.</param>
        public static void DisplayFoodExchange(Inventory inventory)
        {

            if (SwinGame.KeyTyped(KeyCode.SpaceKey))
            {
                _foodMessage = "Enter amount & hit Tab.";
                if (SwinGame.ReadingText())
                {
                    SwinGame.EndReadingText();
                }
                Input.StartReadingText(Color.Black, 3, openSansBoldLarge, _foodField);
            }
            
            if (Input.ReadingText() && SwinGame.KeyTyped(KeyCode.TabKey))
            {
                try
                {
                    int requestedAmount = int.Parse(Input.TextReadAsASCII()); // IA - The amount entered.

                    int requestedFoodValue = (int) Math.Floor((requestedAmount * Food.GetMineralValue())); // IA - the cost of the food in mineral points.
                    if (requestedAmount == 0)
                    {
                        _foodMessage = "Buy 1kg or more.";
                    }
                    else if (Food.GetMaximumFoodToPurchase(inventory) >= requestedAmount && requestedFoodValue > 0 && requestedAmount <= 300)
                    {
                        if (requestedFoodValue > 1)
                        {
                            _foodMessage = requestedAmount.ToString() + "kg food  =>  " + requestedFoodValue.ToString() + " points";
                        } else
                        {
                            _foodMessage = requestedAmount.ToString() + "kg food  =>  " + requestedFoodValue.ToString() + " point"; // IA - no "s" after the word points.
                        }
                        Food.PurchaseFood(inventory, requestedFoodValue);
                    }
                    else if (inventory.GetMineralPoints() == 0 && requestedAmount >= 1 && requestedFoodValue < Food.GetBalance())
                    {
                        Food.PurchaseFoodFromBalance(requestedFoodValue);
                    } else if (requestedAmount > 300)
                    {
                        _foodMessage = "300kg max. at once";
                    }
                    else
                    {
                        _foodMessage = "Not enough points.";
                    }
                } catch (Exception e)
                {
                    _foodMessage = "Invalid input.";
                    Debug.WriteLine(e.ToString());
                }
            }

			if (SwinGame.MouseClicked(MouseButton.LeftButton))
			{
				Input.EndReadingText();
			}
            if (!Input.ReadingText())
            {
                _foodMessage = "Hit Space to enter amount.";
            }
        }

		/// <summary>
		/// Draws the input field for food transactions, and also shows information on the available balance and volume of food the player can afford.
		/// </summary>
		/// <param name="inventory">Inventory.</param>
        public static void DrawFoodField(Inventory inventory)
        {
            SwinGame.DrawText("FOOD EXCHANGE CENTER", Color.WhiteSmoke, openSansExtraBoldLarge, contentRightAlign, 435);
            
            // IA - avoid grammar errors by knowing when to put the term "point" in plural form.
            if (Food.GetMineralValue() == 1)
            {
                SwinGame.DrawText("1kg Food = " + Food.GetMineralValue().ToString() + " point", Color.Wheat, openSansExtraBoldNormal, contentRightAlign, 460);
            } else
            {
                SwinGame.DrawText("1kg Food  =  " + (Math.Round(Food.GetMineralValue(), 0)).ToString() + "+ points", Color.Wheat, openSansExtraBoldNormal, contentRightAlign, 460);
            }

            if (Food.GetBalance() <= 1)
            {
                SwinGame.DrawText("Available balance:  " + Food.GetBalance().ToString() + " point", Color.Wheat, openSansExtraBoldNormal, contentRightAlign, 485);
            } else
            {
                SwinGame.DrawText("Available balance:  " + Food.GetBalance().ToString() + " points", Color.Wheat, openSansExtraBoldNormal, contentRightAlign, 485);
            }

            SwinGame.DrawText("You can afford:  " + Food.GetMaximumFoodToPurchase(inventory).ToString() + "kg", Color.Wheat, openSansExtraBoldNormal, contentRightAlign, 510);

            SwinGame.DrawText("Enter food amount in kg:", Color.Wheat, openSansExtraBoldItalicNormal, contentRightAlign, 535);
            
            SwinGame.FillRectangle(Color.Yellow, _foodField);
            if (SwinGame.PointInRect(SwinGame.MousePosition(), _foodField) || Input.ReadingText())
            {
                SwinGame.FillRectangle(Color.White, _foodField);
            }

            SwinGame.DrawText(_foodMessage, Color.White, openSansExtraBoldNormal, contentRightAlign, 600);
        }

		/// <summary>
		/// Displays the main instructions for the game in Two Players Mode.
		/// </summary>
        public static void DisplayTwoPlayersInstructions()
        {
            SwinGame.DrawText("Outlast your opponent and collect the most mineral points without getting killed!", Color.White, openSansExtraBoldLarge, 20, contentFirstLine);
        }

    }
}
