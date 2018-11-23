using SwinGameSDK;
using System;
using Escapade.src;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escapade.src.mineral
{
  public class Emerald : Mineral
  {
    public Emerald() : base("Emerald", 2, Color.DarkOliveGreen)
    {
            double random = new Random().NextDouble();
            Value = (int) (6 + Math.Round(Meta * random));
    }
  }
}
