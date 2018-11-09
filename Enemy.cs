using System;
using SwinGameSDK;

namespace Escapade
{
	public class Enemy : Entity
	{
		public Enemy(int id, string name, Location location) : base(id, name, location)
		{
		}

		public override void Draw()
		{
			int size = Escapade.GetWorld().Size;
			SwinGame.FillRectangle(Color.MediumVioletRed, Location.X * size, Location.Y * size, size, size);
			SwinGame.DrawRectangle(Color.White, Location.X * size, Location.Y * size, size, size);
		}

		/// <summary>
		/// Update the player on game tick by moving it towards the target
		/// </summary>
		public override void Update()
		{
		}
	}
}
