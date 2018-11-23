using SwinGameSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escapade.src.mineral
{
  public class Ruby : Mineral
  {
    public Ruby() : base("Ruby", 3, Color.DarkRed)
    {
            double random = new Random().NextDouble();
            Value = (int)(8 + Math.Round(Meta * random));
        }
  }
}
