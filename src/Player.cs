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

		private Game game;

		private List<Coordinate> movePath;

		private Pathfinder aspf;

    public Coordinate target = null;

		public Player(Game g, int id) : base(id, "Player", g.Map.RandomEmpty())
		{
			Array facings = Enum.GetValues(typeof(FACING));
			F = (FACING)facings.GetValue(new Random().Next(facings.Length));
			game = g;
			aspf = new Pathfinder(game);
			movePath = new List<Coordinate>();
		}

		public void Draw()
		{
			SwinGame.FillRectangle(Color.MediumVioletRed, X * 10, Y * 10, 10, 10);
			SwinGame.DrawRectangle(Color.White, X * 10, Y * 10, 10, 10);
			switch (F)
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
			if (movePath.Count > 0)
				DrawPath();
		}

		public void TileModify(Coordinate c, int type)
		{
			double dist = Math.Abs(X - c.X) + Math.Abs(Y - c.Y);
			Console.WriteLine("Modify: " + type + " Location:" + c.X + "," + c.Y + " Distance: " + dist);
			if (dist <= 2)
			{
				switch (type)
				{
					case 0:
						game.Map.Grid[c.X, c.Y] = Tile.AIR;
						break;
					case 1:
						game.Map.Grid[c.X, c.Y] = Tile.WALL;
						break;
					default:
						break;
				}
			}
		}

		public void Move()
		{
			if (movePath.Count > 0)
			{
				Coordinate next = movePath[movePath.IndexOf(movePath.Last())];
				movePath.Remove(movePath.Last());
				X = next.X;
				Y = next.Y;
        if (movePath.Count == 0)
          target = null;
			} else
      {
        if (target != null)
          movePath = aspf.GetPath(new Coordinate(X, Y), target);
      }
		}

		public void DrawPath()
		{
			foreach (Coordinate c in movePath)
			{
				SwinGame.FillCircle(Color.Aquamarine, (float)(c.X + 2.5), (float)(c.Y + 2.5), 5);
				SwinGame.DrawCircle(Color.DarkBlue, (float)(c.X + 2.5), (float)(c.Y + 2.5), 5);
			}
		}
	}
}
