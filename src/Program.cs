using System;
using System.Collections.Generic;
using SwinGameSDK;

namespace Escapade
{
  public class Program
  {

    /// <summary>
    /// Settings for the game including map size and fps display
    /// </summary>
    public static Dictionary<string, dynamic> settings = new Dictionary<string, dynamic>
    {
      {"width", 40},
      {"height", 40},
      {"fps", false}
    };

    /// <summary>
    /// Main entry method that initialises the game
    /// </summary>
    public static void Main ()
    {
      Game game = new Game (settings);
      game.Init ();
    }
  }
}