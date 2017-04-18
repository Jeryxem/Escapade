using System;
using System.Collections.Generic;
using SwinGameSDK;

namespace Escapade
{
  public class Program
  {

    public static Dictionary<string, dynamic> settings = new Dictionary<string, dynamic>
    {
      {"width", 40},
      {"height", 40},
      {"fps", false}
    };

    public static void Main ()
    {
      Game game = new Game (settings);
      game.Init ();
    }
  }
}