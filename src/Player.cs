using SwinGameSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escapade
{
  class Player : GameObject
  {
    public enum FACING
    {
      NORTH = 0,
      NORTHEAST = 1,
      EAST = 2,
      SOUTHEAST = 3,
      SOUTH = 4,
      SOUTHWEST = 5,
      WEST = 6,
      NORTHWEST = 7
    }

    public FACING F { get; set; }

    public Player(Game g, int id) : base(id, "Player", g.Map.RandomEmpty())
    {
      Array facings = Enum.GetValues(typeof(FACING));
      F = (FACING)facings.GetValue(new Random().Next(facings.Length));
    }

    public void Draw()
    {
      SwinGame.FillRectangle(Color.MediumVioletRed, X*10, Y*10, 10, 10);
      SwinGame.DrawRectangle(Color.White, X * 10, Y * 10, 10, 10);
      switch(F)
      {
        case FACING.NORTH:
          SwinGame.DrawCircle(Color.DarkRed, (X + 0.5F) * 10, (Y + 0.25F) * 10, 2);
          break;
        case FACING.NORTHEAST:
          SwinGame.DrawCircle(Color.DarkRed, (X + 0.75F) * 10, (Y + 0.25F) * 10, 2);
          break;
        case FACING.EAST:
          SwinGame.DrawCircle(Color.DarkRed, (X + 0.75F) * 10, (Y + 0.5F) * 10, 2);
          break;
        case FACING.SOUTHEAST:
          SwinGame.DrawCircle(Color.DarkRed, (X + 0.75F) * 10, (Y + 0.75F) * 10, 2);
          break;
        case FACING.SOUTH:
          SwinGame.DrawCircle(Color.DarkRed, (X + 0.5F) * 10, (Y + 0.75F) * 10, 2);
          break;
        case FACING.SOUTHWEST:
          SwinGame.DrawCircle(Color.DarkRed, (X + 0.25F) * 10, (Y + 0.75F) * 10, 2);
          break;
        case FACING.WEST:
          SwinGame.DrawCircle(Color.DarkRed, (X + 0.25F) * 10, (Y + 0.5F) * 10, 2);
          break;
        case FACING.NORTHWEST:
          SwinGame.DrawCircle(Color.DarkRed, (X + 0.25F) * 10, (Y + 0.25F) * 10, 2);
          break;
      }
      Console.WriteLine(F.ToString());
    }
  }
}
