using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escapade.src.mineral.gemstone
{
  class Emerald : Gemstone
  {
    public Emerald() : base("Emerald", new Random().Next(70, 81) / 100)
    {
    }
  }
}
