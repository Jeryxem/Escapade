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
            Double random = new Random().NextDouble();
      Value = (int) (((random - 0.5) * (random - 0.5) * 100) + (int) (3000 * Clarity)); // IA - apply Math.Sqrt to make sure substracting the random number by 0.5 never returns a negative number.
    }
  }
}
