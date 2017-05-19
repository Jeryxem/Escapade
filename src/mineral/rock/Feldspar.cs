using SwinGameSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escapade.src.mineral.rock
{
  class Feldspar : Rock
  {
    public Feldspar() : base("Feldspar", new Random().Next(55, 66) / 100, Color.BlanchedAlmond)
    {
    }
  }
}
