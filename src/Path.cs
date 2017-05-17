using SwinGameSDK;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Escapade
{
  public class Path
  {
    List<Location> _targetpath = new List<Location> ();
    Instance _instance;

    #region Properties
    public List<Location> TargetPath {
      get {
        return _targetpath;
      }
      set {
        _targetpath = value;
      }
    }
    public Instance Instance {
      get {
        return _instance;
      }
      set {
        _instance = value;
      }
    }
    #endregion Properties

    public Path (Instance instance)
    {
      Instance = instance;
    }

    public void GetPath (Location start, Location target)
    {
      TargetPath = new List<Location>();
      Pathfind (start, target);
    }

    void Pathfind (Location start, Location target)
    {
      #region NodeLists
      List<PathNode> open = new List<PathNode>();
      List<PathNode> closed = new List<PathNode>();
      #endregion NodeLists

      List<Location> found = new List<Location>();

      #region FindingPath

      PathNode current = new PathNode(start.X, start.Y, start, target);
      current.CalculateScores();

      open.Add(current);

      // While the current location isn't our target
      while(!(current.X == target.X && current.Y == target.Y))
      {
        open.OrderBy(Node => Node.F);


        current = open[0];

        open.Remove(current);
        closed.Add(current);

        #region AddNeighbours

        for(int x = -1; x <= 1; x++)
        {
          for (int y = -1; y <= 1; y++)
          {
            if (x == 0 && y == 0) continue;

            int newX = current.X + x;
            int newY = current.Y + y;

            PathNode node = new PathNode(newX, newY, start, target);

            if (Instance.World.Map[newX - 1, newY - 1].Type != TileType.Air) continue;

            //If it's in the closed list, skip it
            if (closed.Any(Node => Node.X == newX && Node.Y == newY)) continue;

            if(!open.Any(Node => Node.X == newX && Node.Y == newY))
            {
              node.Parent = current;
              open.Add(node);
            }
          }
        }
        #endregion AddNeighbours

        SwinGame.ClearScreen(Color.White);

        foreach (PathNode c in open)
        {
          SwinGame.FillCircle(Color.Red, c.X * 15 + 7.5F, c.Y * 15 + 7.5F, 5F);
          SwinGame.DrawText(((int)c.F).ToString(), Color.Black, c.X * 15 + 7.5F, c.Y * 15 + 7.5F);
        }

        foreach (PathNode c in closed)
        {
          SwinGame.FillCircle(Color.Green, c.X * 15 + 7.5F, c.Y * 15 + 7.5F, 7.5F);
        }

        SwinGame.FillCircle(Color.Blue, current.X * 15 + 7.5F, current.Y * 15 + 7.5F, 7.5F);

        SwinGame.DrawText(open.Count().ToString(), Color.Black, 15, 15);

        SwinGame.DrawText(closed.Count().ToString(), Color.Black, 45, 45);

        SwinGame.RefreshScreen(30);
        SwinGame.Delay(500);
      }
      #endregion FindingPath

      PathNode pn = current;

      while (pn.Parent != null)
      {
        found.Add(pn.Parent);
        pn = pn.Parent;
      }

      TargetPath = found;

    }
  }
}