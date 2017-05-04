using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escapade
{
  class Pathfinder
  {
    #region LocalMembers

    private Game _game;
    private Player _player;
    private Coordinate Start;
    private Coordinate Target;

    #endregion LocalMembers

    #region Properties

    public Game Game
    {
      get { return _game; }
      set { _game = value; }
    }

    public Player Player
    {
      get { return _player; }
      set { _player = value; }
    }

    #endregion Properties

    public Pathfinder(Game g)
    {
      Game = g;
      Player = g.Player;
	    Start = new Coordinate(Player.X, Player.Y);
    }

    public List<Coordinate> GetPath(Coordinate loc1, Coordinate loc2)
    {
      Start = loc1;
      Target = loc2;

      //open nodes for consideration
      List<AStarLoc> open = new List<AStarLoc>();
      //closed nodes that have already beeen considered
      List<AStarLoc> closed = new List<AStarLoc>();

      //the path from the target location to the start
      List<AStarLoc> foundPath = new List<AStarLoc>();

      AStarLoc current = new AStarLoc(Start.X, Start.Y, Start, Target);

      #region FindingPath

      //add current to open list
      open.Add(current);

      //while a path isn't found
      while (true)
      {
        current = open[GetMinIndex(open)];

        #region AddNeighbours

        for (int x = -1; x <= 1; x++)
        {
          for (int y = -1; y <= 1; y++)
          {
            int newX = current.X + x;
            int newY = current.Y + y;

            AStarLoc n = new AStarLoc(newX, newY, Start, Target);

            //if we are currently checking the actual location as a neighbour
            //or if it's not a walkable tile, skip it
            if ((x == 0 && y == 0) || Game.Map.Grid[n.X, n.Y] != Tile.AIR) continue;

            //if closed list contains this location, skip it
            bool skip = false;
            foreach (AStarLoc l in closed)
              if (l.X == n.X && l.Y == n.Y)
                skip = true;
            if (skip) continue;

            //if it's not on the open list, add it
            if(!open.Contains(n))
            {
              n.Parent = current;
              open.Add(n);
            }

            //check if this is the target
            if (n.X == Target.X && n.Y == Target.Y)
            {
              foundPath.Add(n);
              break;
            }
          }
        }

        #endregion AddNeighbours
      }

      #endregion FindingPath

      //add correct path from start to target, looping back through each parent
      #region CreatePath

      AStarLoc pl = foundPath[0];

      while (pl.X != Start.X && pl.Y != Start.Y)
      {
        foundPath.Add(pl);
        pl = pl.Parent;
      }

	  #endregion CreatePath

	  List<Coordinate> foundCoords = new List<Coordinate>();

	  foreach (AStarLoc a in foundPath)
			foundCoords.Add(a);

	  return foundCoords;
    }

    public static int GetMinIndex(List<AStarLoc> list)
    {
      int min = 0;
      foreach(AStarLoc l in list)
      {
        if (l.F < list[min].F)
          min = list.IndexOf(l);
      }
      return min;
    }
  }

  class AStarLoc : Coordinate
  {
    private float g;
    private float h;

    private float f;
		public float F { get { return f; } set { f = value; } }

    private AStarLoc parent;
		public AStarLoc Parent { get { return parent; } set { parent = value; } }

		public AStarLoc(int x, int y, Coordinate s, Coordinate t) : base(x, y)
    {
      CalcScores(s, t);
    }

    public AStarLoc(Coordinate c, Coordinate s, Coordinate t) : this(c.X, c.Y, s, t)
    {
    }

    public void CalcScores(Coordinate c1, Coordinate c2)
    {
      g = (float) Math.Sqrt(Math.Pow(Math.Abs(c1.X - X), 2) + Math.Pow(Math.Abs(c1.Y - Y), 2));
      h = (float) Math.Sqrt(Math.Pow(Math.Abs(X - c2.X), 2) + Math.Pow(Math.Abs(Y - c2.Y), 2));
      f = g + h;
    }
  }
}
