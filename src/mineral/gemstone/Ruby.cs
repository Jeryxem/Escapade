using SwinGameSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escapade.src.mineral.gemstone
{
  class Ruby : Gemstone
  {
    public Ruby() : base("Ruby", new Random().Next(55, 66) / 100, Color.DarkRed)
    {
    }
  }
}
