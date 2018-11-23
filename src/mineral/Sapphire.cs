using SwinGameSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escapade.src.mineral
{
  public class Sapphire : Mineral
  {
    public Sapphire() : base("Sapphire", 4, Color.DarkBlue)
    {
            double random = new Random().NextDouble();
            Value = (int)(10 + Math.Round(Meta * random));
        }
  }
}
