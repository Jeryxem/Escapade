using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

/// <summary>
/// This class under the GUI folder takes cares of handling the bottom section of the screen, which contains the countdown and level information.
/// </summary>
namespace Escapade.src.gui
{
    /// <summary>
    /// This class takes care of the bottom panel at the bottom of the screen, which shows the timer and the game's level [by Isaac]
    /// </summary>
    public class MetaHandler
    {
        
        private Panel bottomPanel; // IA - this will hold the panel resource (meta.txt)
        private int contentVerticalAlign;
        private Countdown timer;
        private String hungerIndication;
        private int hungerIndicatorWidth;
        private Color hungerIndicatorColor;
        

        public MetaHandler()
        {
            bottomPanel = SwinGame.LoadPanelNamed("Bottom Pannel", "Meta.txt");
            timer = new Countdown();
            timer.StartTimer();
            contentVerticalAlign = GlobalConstants.WORLD_HEIGHT + 18;
            hungerIndication = "You're in shape!";
            hungerIndicatorWidth = 150;
            hungerIndicatorColor = Color.Green;
        }

        private int Map(int indicator)
        {
            return (int) Math.Ceiling(indicator / 1.5);
        }

        private void ControlLevelDisplay()
        {
            if (SwinGame.KeyDown(KeyCode.SpaceKey))
            {
                if (hungerIndicatorWidth >= 0)
                    hungerIndicatorWidth--;

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
                else if (Map(hungerIndicatorWidth) < 20)
                {
                    hungerIndicatorColor = Color.Red;
                    hungerIndication = "Get food now! (Almost game over)";
                }
            }
        }

        /// <summary>
        /// This method calls all the required methods to make the panel visible on the screen with the intended background color.
        /// </summary>
        public void ShowBottomPanel()
        {
            SwinGame.GUISetBackgroundColor(Color.Black);
            SwinGame.ShowPanel(bottomPanel);
            SwinGame.DrawInterface();
        }

        /// <summary>
        /// In the next update, this method will pull the timer info from a Countdown object.
        /// </summary>
        public void DisplayTimer ()
        {            
            SwinGame.DrawText("Time: " + timer.ShowTime(), Color.White, 20, contentVerticalAlign);
        }

        public void DisplayHungerInformation()
        {
            SwinGame.DrawText("Your energy levels: ", Color.White, 200, contentVerticalAlign);

            SwinGame.FillRectangle(hungerIndicatorColor, 370, GlobalConstants.WORLD_HEIGHT + 5, hungerIndicatorWidth, 20);

            SwinGame.DrawText(Map(hungerIndicatorWidth).ToString() + "% ", hungerIndicatorColor, 370 + hungerIndicatorWidth + 5, GlobalConstants.WORLD_HEIGHT + 12);

            SwinGame.DrawText(hungerIndication, hungerIndicatorColor, 370, GlobalConstants.WORLD_HEIGHT + 30);

            ControlLevelDisplay();
        }

        /// <summary>
        /// In the next update, this method will pull the level info from a GameLevel object.
        /// </summary>
        public void DisplayGameLevel()
        {
            SwinGame.DrawText("Level: 1", Color.White, GlobalConstants.WORLD_WIDTH - 150, contentVerticalAlign);
        }
            
    }
}
