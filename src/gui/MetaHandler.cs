using System;
using System.Collections.Generic;
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

        public static int Map(double indicator)
        {
            return (int)Math.Ceiling(indicator / 1.5);
        }

        public static void DecreaseEnergy()
        {
            hungerIndicatorWidth -= 0.02;
        }

        public static void IncreaseEnergy()
        {
            hungerIndicatorWidth += 0.02;
        }

        public static void ControlLevelDisplay()
        {
            if (hungerIndicatorWidth > 0)
            {
                DecreaseEnergy();
            }
            else if ((SwinGame.KeyDown(KeyCode.LeftShiftKey) || SwinGame.KeyDown(KeyCode.RightShiftKey)) && hungerIndicatorWidth < 149)
            {
                IncreaseEnergy();
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
            SwinGame.DrawText("Time: " + timer.ShowTime(), Color.White, 20, contentFirstLine);
        }

        public static void DisplayHungerInformation()
        {
            ControlLevelDisplay();

            SwinGame.DrawText("Your energy levels: ", Color.White, 200, contentFirstLine);

            SwinGame.FillRectangle(hungerIndicatorColor, 370, GlobalConstants.WORLD_HEIGHT + 12, (int)hungerIndicatorWidth, 20);

            SwinGame.DrawText(Map(hungerIndicatorWidth).ToString() + "% ", hungerIndicatorColor, 370 + (int)hungerIndicatorWidth + 5, contentFirstLine);

            SwinGame.DrawText(hungerIndication, hungerIndicatorColor, 370, GlobalConstants.WORLD_HEIGHT + 37);
        }

        /// <summary>
        /// In the next update, this method will pull the level info from a GameLevel object.
        /// </summary>
        public static void DisplayGameLevel()
        {
            Font arial = SwinGame.LoadFont("arial", 20);
            SwinGame.DrawText("Level: " + GameLevel.PrintLevel(), Color.White, arial, GlobalConstants.WORLD_WIDTH - 150, contentFirstLine);
        }

        public static void DisplayAmmunitionLevel(Weapon weapon)
        {
            if (weapon != null)
            {
                if (weapon.Ammunition > 0)
                {
                    {
                        SwinGame.DrawText("AMMUNITION AVAILABLE: " + weapon.Ammunition.ToString(), Color.Yellow, 20, contentSecondLine);

                        SwinGame.DrawText("AMMUNITION TYPE: " + weapon.Type.ToString(), Color.Yellow, 20, contentSecondLine + 15);
                    }
                } else
                {
                    SwinGame.DrawText("You've ran out of ammo!", Color.Yellow, 20, contentSecondLine);

                    SwinGame.DrawText("Gather minerals to buy more weapons!", Color.Yellow, 20, contentSecondLine + 15);
                }
            }
            else
            {
                SwinGame.DrawText("Press B or Shift + B to buy weapons.", Color.Yellow, 20, contentSecondLine);
            }
        }

        public static void DisplayEnemiesInfo(List<Enemy> existingEnemies, List<Enemy> enemiesHit)
        {
            SwinGame.DrawText("ENEMIES IN THIS LEVEL", Color.Yellow, contentRightAlign, 15);

            SwinGame.DrawText("Red Enemies: " + existingEnemies.Count.ToString(), Color.Yellow, contentRightAlign, 45);

            SwinGame.DrawText("Successful hits: " + enemiesHit.Count.ToString(), Color.Yellow, contentRightAlign, 70);

            SwinGame.DrawText("Destroy " + (GameLevel.GetFormula() - enemiesHit.Count).ToString() + " more enemies.", Color.Yellow, contentRightAlign, 100);
        }

        public static void DisplayRate(double[] mineralWorth)
        {
            Font arial = SwinGame.LoadFont("arial", 14);
            // IA - Make sure that the game is not already reading text.
            /* if (SwinGame.ReadingText())
            {
                SwinGame.EndReadingText();
            } */

            // IA - Display the total value of minerals while the game runs
            SwinGame.DrawText("YOUR MINERAL POINTS", Color.White, contentRightAlign, 210);

            // IA - Display the total value of minerals while the game runs
            SwinGame.DrawText("Diamond worth: " + mineralWorth[0].ToString(), Color.White, contentRightAlign, 240);

            // IA - Display the total value of minerals while the game runs
            SwinGame.DrawText("Emerald worth: " + mineralWorth[1].ToString(), Color.White, contentRightAlign, 265);

            // IA - Display the total value of minerals while the game runs
            SwinGame.DrawText("Ruby worth: " + mineralWorth[2].ToString(), Color.White, contentRightAlign, 290);

            // IA - Display the total value of minerals while the game runs
            SwinGame.DrawText("Sapphire: " + mineralWorth[3].ToString(), Color.White, contentRightAlign, 315);
            
        }

    }
}
