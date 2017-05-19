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

    public Gemstone(string name, float clarity) : base(name)
    {
      Clarity = clarity;
      Value = (int) (((new Random().NextDouble() - 0.5) * 100) + (int) (1000 * Clarity));
    }
  }
}
