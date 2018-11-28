using SwinGameSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escapade.src.mineral
{
	/// <summary>
	/// This Ruby class holds the value of Ruby minerals in the game.
	/// </summary>
	public class Ruby : Mineral
	{
		public Ruby() : base("Ruby", 3, Color.DarkRed)
		{
			Value = 20;
		}
	}
}
