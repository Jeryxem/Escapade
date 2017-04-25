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

    public Pathfinder p;

    public Player(Game g, int id) : base(id, "Player", g.Map.RandomEmpty())
    {
      p = new Pathfinder(g.Map);
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
    }

    public void Move(Coordinate target)
    {
      List<Coordinate> path = p.GetPath(new Coordinate(X, Y), target);
      foreach(Coordinate step in path)
      {
        X = step.X;
        Y = step.Y;
        F = GetFacing(new Coordinate(X, Y), target);
      }
    }

    /// <summary>
    /// Get the facing for a player based on a start and end coordinate
    /// </summary>
    /// <param name="c1">Start coordinate</param>
    /// <param name="c2">Target coordinate</param>
    /// <returns>The appropriate FACING value for the new location</returns>
    public static FACING GetFacing(Coordinate c1, Coordinate c2)
    {
      string vert, horiz;

      //get horizontal facing
      switch(Math.Abs(c1.X - c2.X))
      {
        case -1:
          horiz = "WEST";
          break;
        case 1:
          horiz = "EAST";
          break;
        default:
          horiz = "";
          break;
      }

      //get vertical facing
      switch(Math.Abs(c1.Y - c2.Y))
      {
        case -1:
          vert = "SOUTH";
          break;
        case 1:
          vert = "NORTH";
          break;
        default:
          vert = "";
          break;
      }

      //add horizontal and vertical together
      string newfacing = horiz + vert;

      //Parse the string to a facing enum
      Enum.TryParse(newfacing, out FACING facing);

      //return the new facing for the player
      return facing;
    }
  }
}
