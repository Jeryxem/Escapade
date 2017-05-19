using SwinGameSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escapade.src.mineral.rock
{
  class Quartz : Rock
  {
    public Quartz() : base("Quartz", new Random().Next(40, 51) / 100, Color.AntiqueWhite)
    {
    }
  }
}
