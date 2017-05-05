using Escapade.src.path;
using SwinGameSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escapade.src.player
{
  public class Player : Object
  {

    private Path _path;

    public Path Path { get { return _path; } set { _path = value; } }

    public Player(Escapade instance) : base(instance.Objects.Count, "Player", instance) { }

    public void Draw()
    {
      SwinGame.FillRectangle(Color.MediumVioletRed, Location.X * Escapade.SIZE, Location.Y * Escapade.SIZE, Escapade.SIZE, Escapade.SIZE);
      SwinGame.DrawRectangle(Color.White, Location.X * Escapade.SIZE, Location.Y * Escapade.SIZE, Escapade.SIZE, Escapade.SIZE);
    }

  }
}
