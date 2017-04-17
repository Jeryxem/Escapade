using System;
using System.Collections.Generic;
using SwinGameSDK;

namespace Escapade
{
  public class Program
  {

    public static Dictionary<string, dynamic> settings = new Dictionary<string, dynamic>()
      {
        {"title", "Escapade"},
        {"width", 400},
        {"height", 400},
        {"cellsize", 10},
        {"fps", true}
      };

    public static void Main ()
    {
      Game game = new Game (settings);
      game.Init ();
    }
  }
}