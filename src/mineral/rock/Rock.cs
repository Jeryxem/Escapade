using SwinGameSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escapade.src.mineral.rock
{
  public abstract class Rock : Mineral
  {
    float _weight;

    public float Weight {
      get {
        return _weight;
      }
      set {
        _weight = value;
      }
    }

    public Rock(string name, float weight, Color colour) : base(name, colour)
    {
      Weight = weight;
      Value = (int)(((new Random().NextDouble() - 0.5) * 100) + (int)(500 * Weight));
    }
  }
}
