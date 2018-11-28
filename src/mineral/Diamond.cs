using SwinGameSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escapade.src.mineral
{
	/// <summary>
	/// This diamond class holds the value of diamond minerals in the game.
	/// </summary>
	public class Diamond : Mineral
	{
		public Diamond() : base("Diamond", 1, Color.Aquamarine)
		{
			Value = 10;
		}
	}
}
