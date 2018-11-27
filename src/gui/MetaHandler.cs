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
    /// This class takes care of the bottom panel at the bottom of the screen, which shows the timer and the game's level [by Isaac]
    /// </summary>
    public static class MetaHandler
    {
        private static Rectangle _foodField = SwinGame.CreateRectangle(810, 560, 150, 30);
        private static String _foodMessage;
        private static String _ammunitionMessage1;
        private static String _ammunitionMessage2;
        private static Font openSansExtraBoldLarge = SwinGame.LoadFont("OpenSans-ExtraBold", 14);
        private static Font openSansExtraBoldNormal = SwinGame.LoadFont("OpenSans-ExtraBold", 12);
        private static Font openSansExtraBoldSmall = SwinGame.LoadFont("OpenSans-ExtraBold", 10);
        private static Font openSansExtraBoldItalicNormal = SwinGame.LoadFont("OpenSans-ExtraBoldItalic", 12);
        private static Font openSansBoldLarge = SwinGame.LoadFont("OpenSans-Bold", 20);
        private static Font openSansBoldItalicNormal = SwinGame.LoadFont("OpenSans-BoldItalic", 12);

        public static Panel bottomPanel = SwinGame.LoadPanelNamed("Bottom Panel", "Meta.txt"); // IA - this will hold the panel resource (meta.txt)
        public static Panel enemiesPanel = SwinGame.LoadPanelNamed("Enemies Panel", "Extra1.txt");
        public static Panel inventoryPanel = SwinGame.LoadPanelNamed("Inventory Panel", "Extra2.txt");
        public static Panel foodPanel = SwinGame.LoadPanelNamed("Food Panel", "Extra3.txt");
        private static int contentFirstLine = GlobalConstants.WORLD_HEIGHT + 18;
        private static int contentSecondLine = GlobalConstants.WORLD_HEIGHT + 48;
        private static int contentRightAlign = GlobalConstants.WORLD_WIDTH + 5;
        public static Countdown timer = new Countdown();

        public static String hungerIndication;
        public static double hungerIndicatorWidth = 150;
        public static Color hungerIndicatorColor;

        public static string GetAmmunitionMessage1()
        {
            return _ammunitionMessage1;
        }

        public static void ResetAmmunitionMessage1()
        {
            _ammunitionMessage1 = "Press B or Shift + B to buy weapons.";
        }

        public static string GetAmmunitionMessage2()
        {
            return _ammunitionMessage2;
        }

        public static void ResetAmmunitionMessage2()
        {
            _ammunitionMessage2 = "";
        }

        public static int Map(double indicator)
        {
            return (int)Math.Ceiling(indicator / 1.5);
        }

        public static int GetEnergyLevel()
        {
            return Map(hungerIndicatorWidth);
        }

        public static void DecreaseEnergy()
        {
            hungerIndicatorWidth -= 0.02;
        }

        public static void ResetEnergyLevels()
        {
            hungerIndicatorWidth = 150;
        }

        public static void SetFoodMessage(String message)
        {
            _foodMessage = message;
        }

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
        /// This method calls all the required methods to make the panel visible on the screen with the intended background color.
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
        /// In the next update, this method will pull the timer info from a Countdown object.
        /// </summary>
        public static void DisplayTimer()
        {
            SwinGame.DrawText("Time: " + timer.ShowTime(), Color.White, openSansExtraBoldLarge, 20, contentFirstLine);
        }

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
        /// In the next update, this method will pull the level info from a GameLevel object.
        /// </summary>
        public static void DisplayGameLevel()
        {
            SwinGame.DrawText("Level: " + GameLevel.PrintLevel(), Color.White, openSansBoldLarge, GlobalConstants.WORLD_WIDTH - 120, contentFirstLine);
        }

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

        public static void DisplaySinglePlayerWeaponInfo(Player player, String playerName, int Y)
        {
            if (player.Weapon != null)
            {
                SwinGame.DrawText(playerName, Color.White, openSansExtraBoldLarge, contentRightAlign, 450 + Y);

                SwinGame.DrawText("Projectiles left: " + player.Weapon.Ammunition.ToString(), Color.WhiteSmoke, openSansExtraBoldNormal, contentRightAlign, 470 + Y);
            }
        }

        public static void DisplayEnemiesInfo(List<Enemy> existingEnemies, List<Enemy> enemiesHit)
        {
            SwinGame.DrawText("ENEMIES IN THIS LEVEL", Color.Yellow, openSansExtraBoldLarge, contentRightAlign, 15);

            SwinGame.DrawText("Red Enemies: " + existingEnemies.Count.ToString(), Color.Yellow, openSansExtraBoldNormal, contentRightAlign, 45);

            SwinGame.DrawText("Successful hits: " + enemiesHit.Count.ToString(), Color.Yellow, openSansExtraBoldNormal, contentRightAlign, 70);

            SwinGame.DrawText("Destroy " + (GameLevel.GetFormula() - enemiesHit.Count).ToString() + " more enemies.", Color.Yellow, openSansExtraBoldNormal, contentRightAlign, 100);
        }

        public static void DisplayRate(double[] mineralWorth, Inventory inventory)
        {
            // Font arial = SwinGame.LoadFont("arial", 14);

            // IA - Display the total value of minerals while the game runs
            SwinGame.DrawText("YOU HAVE " + inventory.ItemList.Count.ToString() + " MINERALS", Color.White, openSansExtraBoldLarge, contentRightAlign, 210);

            // IA - Display the total value of minerals while the game runs
            SwinGame.DrawText("Diamond points:  " + mineralWorth[0].ToString(), Color.White, openSansExtraBoldNormal, contentRightAlign, 240);

            // IA - Display the total value of minerals while the game runs
            SwinGame.DrawText("Emerald points:  " + mineralWorth[1].ToString(), Color.White, openSansExtraBoldNormal, contentRightAlign, 265);

            // IA - Display the total value of minerals while the game runs
            SwinGame.DrawText("Ruby points:  " + mineralWorth[2].ToString(), Color.White, openSansExtraBoldNormal, contentRightAlign, 290);

            // IA - Display the total value of minerals while the game runs
            SwinGame.DrawText("Sapphire points:  " + mineralWorth[3].ToString(), Color.White, openSansExtraBoldNormal, contentRightAlign, 315);

            SwinGame.DrawText("Total points:  " + inventory.GetMineralPoints(), Color.White, openSansExtraBoldNormal, contentRightAlign, 360);

            SwinGame.DrawText("(Balance not included)", Color.White, openSansBoldItalicNormal, contentRightAlign, 385);

        }

        public static void DisplayRate(double[] mineralWorth, Inventory inventory, String playerName, int Y, Color color)
        {
            // Font arial = SwinGame.LoadFont("arial", 14);

            // IA - Display the total value of minerals while the game runs
            SwinGame.DrawText(playerName + " HAS " + inventory.ItemList.Count.ToString() + " MINERALS", color, openSansExtraBoldNormal, contentRightAlign, 210 + Y);

            SwinGame.DrawText("Diamond points:  " + mineralWorth[0].ToString(), color, openSansExtraBoldNormal, contentRightAlign, 240 + Y);

            SwinGame.DrawText("Emerald points:  " + mineralWorth[1].ToString(), color, openSansExtraBoldNormal, contentRightAlign, 265 + Y);

            SwinGame.DrawText("Ruby points:  " + mineralWorth[2].ToString(), color, openSansExtraBoldNormal, contentRightAlign, 290 + Y);

            SwinGame.DrawText("Sapphire points:  " + mineralWorth[3].ToString(), color, openSansExtraBoldNormal, contentRightAlign, 315 + Y);

            SwinGame.DrawText("Sapphire points:  " + mineralWorth[3].ToString(), color, openSansExtraBoldNormal, contentRightAlign, 315 + Y);

            SwinGame.DrawText("Total points:  " + inventory.GetMineralPoints(), color, openSansExtraBoldNormal, contentRightAlign, 360 + Y);

        }

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

            if (!Input.ReadingText())
            {
                _foodMessage = "Hit Space to enter amount.";
            }
        }

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

        public static void DisplayTwoPlayersInstructions()
        {
            SwinGame.DrawText("Outlast your opponent and collect the most mineral points without getting killed!", Color.White, openSansExtraBoldLarge, 20, contentFirstLine);
        }

    }
}
