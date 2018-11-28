using SwinGameSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escapade.src.mineral
{
	/// <summary>
	/// This Sapphire class holds the value of Sapphire minerals in the game.
	/// </summary>
	public class Sapphire : Mineral
	{
		public Sapphire() : base("Sapphire", 4, Color.DarkBlue)
		{
			Value = 25;
		}
	}
}
