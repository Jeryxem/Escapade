using SwinGameSDK;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Escapade
{
  public class Path
  {
    List<Location> _targetpath = new List<Location> ();

    #region Properties
    public List<Location> TargetPath {
      get {
        return _targetpath;
      }
      set {
        _targetpath = value;
      }
    }
    #endregion Properties

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
        open = open.OrderBy (x => x.H).ToList();

        //Console.WriteLine ("HEY MAN IM SORTIN HERE");
        //foreach (PathNode n in open)
        //  Console.WriteLine (n.F.ToString ());
        //Console.WriteLine ("~~~~Thanks Matt :)~~~~");

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
            node.CalculateScores ();

            if (Escapade.GetWorld().Map[newX, newY].Type != TileType.Air) continue;

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
      }
      #endregion FindingPath

      PathNode pn = current;

      while (pn.Parent != null) {
        found.Add (pn);
        pn = pn.Parent;
      }
      found.Reverse();
      TargetPath = found;
    }
  }
}