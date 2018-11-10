using System;
using SwinGameSDK;

namespace Escapade
{
	public class Enemy : Entity
	{
		private int _directionX;
		private int _directionY;
		//private bool _collison;
		public World _tile;

		public Enemy(int id, string name, Location location) : base(id, name, location)
		{
			_location.X = 25;
			_location.Y = 20;
			_directionX = 1;
			_directionY = 1;
		}

		public void SetCollision()
		{
			//testing
			for (int x = 0; x < GlobalConstants.WORLD_WIDTH / GlobalConstants.SIZE; x++)
			{
				for (int y = 0; y < GlobalConstants.WORLD_HEIGHT / GlobalConstants.SIZE; y++)
				{
					if ( _tile.Map[x,y].Type == TileType.Rock) 
					{
						_tile.Map[x+1, y].Collision = true;
						_tile.Map[x-1, y].Collision = true;
						//_directionX = 2;
						//if (_tile.Map[x,y].Type == TileType.Rock)
						{
							//_directionX = 2;
						}
					}

				}
			}
		}

		public void CheckCollision() 
		{
			for (int x = 0; x < GlobalConstants.WORLD_WIDTH / GlobalConstants.SIZE; x++)
			{
				for (int y = 0; y < GlobalConstants.WORLD_HEIGHT / GlobalConstants.SIZE; y++) 
				{
					if (_location.X == _tile.Map[x,y].LocationX) 
					{
						//if(_tile.Map[x, y].Collision == true )
						//_directionY = 1;
					}

					/*if (_location.X == _tile.Map[x+1, y].LocationX) 
					{
						_directionY = 2;
					}*/
				}
			}
		}



		public void enemyMovement()
		{
			//direction X, 1 go right
			if (_directionX == 1)
			{
				_location.X += 1;
			}

			//direction X, 2 go left
			if (_directionX == 2)
			{
				_location.X -= 1;
			}

			if (_location.X == 0)
			{
				_directionX = 1;
			}

			if (_location.X == GlobalConstants.WORLD_WIDTH/GlobalConstants.SIZE - 1)
			{
				_directionX = 2;
			}

			//direction Y, 1 go down
			if (_directionY == 1)
			{
				_location.Y += 1;
			}

			//direction Y, 2 go up
			if (_directionY == 2)
			{
				_location.Y -= 1;
			}

			if (_location.Y == 0)
			{
				_directionY = 1;
			}

			if (_location.Y == GlobalConstants.WORLD_HEIGHT/GlobalConstants.SIZE - 1)
			{
				_directionY = 2;

			}
		}

		public int DirectionX 
		{
			get { return _directionX;} set { _directionX = value;}
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
