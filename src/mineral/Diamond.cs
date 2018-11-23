using SwinGameSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escapade.src.mineral
{
  public class Diamond : Mineral
  {
    public Diamond() : base("Diamond", 1, Color.Aquamarine)
    {
            double random = new Random().NextDouble();
            Value = (int)(4 + Math.Round(Meta * random));
        }
  }
}
