using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escapade
{
  class PathNode : Location
  {

    float _f = 0.0F;
    float _g = 0.0F;
    float _h = 0.0F;

    Location _start;
    Location _target;

    PathNode _parent = null;

    #region properties
    public float F {
      get {
        return _f;
      }
      set {
        _f = value;
      }
    }
    public float G {
      get {
        return _g;
      }
      set {
        _g = value;
      }
    }
    public float H {
      get {
        return _h;
      }
      set {
        _h = value;
      }
    }
    public Location Start {
      get {
        return _start;
      }
      set {
        _start = value;
      }
    }
    public Location Target {
      get {
        return _target;
      }
      set {
        _target = value;
      }
    }
    public PathNode Parent {
      get {
        return _parent;
      }
      set {
        _parent = value;
      }
    }
    #endregion Properties

    public PathNode(int x, int y, Location start, Location target) : base(x, y)
    {
      Start = start;
      Target = target;
    }

    public void CalculateScores()
    {
      float sum = 0.0F;
      PathNode pn = this;
      while (pn.Parent != null)
      {
        sum += pn.F;
        pn = pn.Parent;
      }
      //H = (float)Math.Sqrt(Math.Pow(Math.Abs(X - Target.X), 2) + Math.Pow(Math.Abs(Y - Target.Y), 2));
      H = Math.Abs (X - Target.X) + Math.Abs (Y - Target.Y);
      F = H;
    }

  }
}
