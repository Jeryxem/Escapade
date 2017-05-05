using Escapade.src.world;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escapade.src.path
{
  public class Node : Location
  {

    private int _gscore;
    private int _hscore;
    private int _fscore;

    private Node _parent;

    public int GScore { get { return _gscore; } set { _gscore = value; } }
    public int HScore { get { return _hscore; } set { _hscore = value; } }
    public int FScore { get { return _fscore; } set { _fscore = value; } }

    public Node Parent { get { return _parent; } set { _parent = value; } }

    public Node(int x, int y) : base(x, y)
    {
      CalculateScores();
    }

    public Node(Location l) : this(l.X, l.Y) { }

    public void CalculateScores()
    {

    }

    public static bool AreEqual(Node node1, Node node2)
    {
      return (node1.X == node2.X && node1.Y == node2.Y);
    }

  }
}
