using SwinGameSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escapade.src.mineral.gemstone
{
  class Sapphire : Gemstone
  {
    public Sapphire() : base("Sapphire", 4, new Random().Next(40, 51) / 100, Color.DeepSkyBlue)
    {
    }
  }
}
