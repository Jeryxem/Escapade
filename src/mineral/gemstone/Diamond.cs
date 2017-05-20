using SwinGameSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escapade.src.mineral.gemstone
{
  class Diamond : Gemstone
  {
    public Diamond() : base("Diamond", 1, new Random().Next(85, 96) / 100, Color.Aquamarine)
    {
    }
  }
}
