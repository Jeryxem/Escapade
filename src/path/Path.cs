using Escapade.src.world;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escapade.src.path
{
  public class Path
  {

    private List<Node> _nodes;

    public List<Node> Nodes { get { return _nodes; } set { _nodes = value; } }
    public Node Start { get { return Nodes.First(); } }
    public Node Target { get { return Nodes.Last(); } }

    public Path(Location start, Location target)
    {
      Nodes.Add(new Node(start));
      Nodes.Add(new Node(target));
    }

    public void AddNode(Node n)
    {
      Nodes.Insert(Nodes.Count - 2, n);
    }

    public void FindPath()
    {

    }
    
  }
}
