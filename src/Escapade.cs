using Escapade.src.player;
using Escapade.src.world;
using SwinGameSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escapade.src
{
  public class Escapade
  {
    public static int SIZE = 15;

    private List<Object> _objects;
    private World _world;
    private Player _player;

    public List<Object> Objects { get { return _objects; } set { _objects = value; } }
    public World World { get { return _world; } set { _world = value; } }
    public Player Player { get { return _player; } set { _player = value; } }

    public static void Main()
    {
      Escapade game = new Escapade();
      game.Init(40, 40);
    }

    public void Init(int width, int height)
    {
      SwinGame.OpenGraphicsWindow("Escapade",
        width * SIZE, height * SIZE);

      World = new World(width, height);
      Player = new Player(this);
    }

  }
}
