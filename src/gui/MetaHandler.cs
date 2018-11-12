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
        public Panel bottomPanel; // IA - this will hold the panel resource (meta.txt)

        public MetaHandler()
        {
            bottomPanel = SwinGame.LoadPanelNamed("Bottom Pannel", "Meta.txt");
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
            SwinGame.DrawText("Timer: [Get timer info]", Color.White, 20, GlobalConstants.WORLD_HEIGHT + 20);
        }

        /// <summary>
        /// In the next update, this method will pull the level info from a GameLevel object.
        /// </summary>
        public void DisplayGameLevel()
        {
            SwinGame.DrawText("Level: [Get the level info]", Color.White, GlobalConstants.WORLD_WIDTH / 2, GlobalConstants.WORLD_HEIGHT + 20);
        }
            
    }
}
