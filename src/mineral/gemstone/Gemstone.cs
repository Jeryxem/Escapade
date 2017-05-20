using SwinGameSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escapade.src.mineral
{
  public abstract class Gemstone : Mineral
  {
    float _clarity;

    public float Clarity {
      get {
        return _clarity;
      }
      set {
        _clarity = value;
      }
    }

    public Gemstone(string name, int meta, float clarity, Color colour) : base(name, meta, colour)
    {
      Clarity = clarity;
      Value = (int) (((new Random().NextDouble() - 0.5) * 100) + (int) (3000 * Clarity));
    }
  }
}
