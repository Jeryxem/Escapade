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
    public class MetaHandler
    {
        static Panel bottomPanel = SwinGame.LoadPanel("meta.txt");

        static void ShowBottomPanel()
        {
            SwinGame.ShowPanel(bottomPanel);
            SwinGame.GUISetBackgroundColor(Color.Black);
            SwinGame.DrawInterface();
        }
    }
}
