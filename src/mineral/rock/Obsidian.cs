using SwinGameSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escapade.src.mineral.rock
{
  class Obsidian : Rock
  {
    public Obsidian() : base("Obsidian", new Random().Next(85, 96) / 100, Color.DarkViolet)
    {
    }
  }
}
