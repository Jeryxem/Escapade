using SwinGameSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escapade.src.mineral.rock
{
  class Marble : Rock
  {
    public Marble() : base("Marble", new Random().Next(70, 81) / 100, Color.FloralWhite)
    {
    }
  }
}
