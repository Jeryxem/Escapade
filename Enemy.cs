using System;
using SwinGameSDK;

namespace Escapade
{
	public class Enemy : Entity
	{
		private int _directionX;
		private int _directionY;

		public Enemy(int id, string name, Location location) : base(id, name, location)
		{
			_location.X = 25;
			_location.Y = 20;
			_directionX = 1;
			_directionY = 1;
		}

		public void enemyMovement()
		{
			//direction X
			if (_directionX == 1)
			{
				_location.X += 1;
			}

			if (_directionX == 2)
			{
				_location.X -= 1;
			}

			if (_location.X == 0)
			{
				_directionX = 1;
			}

			if (_location.X == 52)
			{
				_directionX = 2;
			}

			//direction Y
			if (_directionY == 1)
			{
				_location.Y += 1;
			}

			if (_directionY == 2)
			{
				_location.Y -= 1;
			}

			if (_location.Y == 0)
			{
				_directionY = 1;
			}

			if (_location.Y == 39)
			{
				_directionY = 2;

			}
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
