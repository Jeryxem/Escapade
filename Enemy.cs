using System;
using SwinGameSDK;

namespace Escapade
{
	public class Enemy : Entity
	{
		private int _directionX;
		private int _directionY;
    private Color _enemyColor;
		public World _world;
		public Escapade _escapade;
		public Enemy(int id, string name, Location location, int directionX, int directionY) : base(id, name, location)
		{
			_location.X = 25;
			_location.Y = 20;
			_directionX = directionX;
			_directionY = directionY;
			_world = Escapade.GetWorld();

      // JY- to differentiate between the boss and the spawned ones
      if (name == "Boss Enemy")
        _enemyColor = Color.GreenYellow;
      else
        _enemyColor = Color.MediumVioletRed;
		}

    // JY- added to help the enemy detect if it is hit
    public bool CheckHit (Projectile projectile)
    {
      int enemyX = Location.X * GlobalConstants.SIZE, enemyY = Location.Y * GlobalConstants.SIZE;
      int projectileLocationX = projectile.ProjectileLocationX, projectileLocationY = projectile.ProjectileLocationY;

      if (projectile.Type == WeaponType.Normal) {
        if (projectile.Horizontal) {
          if (SwinGame.PointInRect (SwinGame.PointAt (enemyX, enemyY), projectileLocationX * GlobalConstants.SIZE, projectileLocationY * GlobalConstants.SIZE, GlobalConstants.NORMAL_PROJECTILE_WIDTH, GlobalConstants.PROJECTILE_LENGTH))
            return true;
          else
            return false;
        } else {
          if (SwinGame.PointInRect (SwinGame.PointAt (enemyX, enemyY), projectileLocationX * GlobalConstants.SIZE, projectileLocationY * GlobalConstants.SIZE, GlobalConstants.NORMAL_PROJECTILE_WIDTH, GlobalConstants.PROJECTILE_LENGTH))
            return true;
          else
            return false;
        }

      } else {
        if (projectile.Horizontal) {
          if (SwinGame.PointInRect (SwinGame.PointAt (enemyX, enemyY), projectileLocationX * GlobalConstants.SIZE, projectileLocationY * GlobalConstants.SIZE, GlobalConstants.PROJECTILE_LENGTH, GlobalConstants.SUPER_PROJECTILE_WIDTH))
            return true;
          else
            return false;
        } else {
          if (SwinGame.PointInRect (SwinGame.PointAt (enemyX, enemyY), projectileLocationX * GlobalConstants.SIZE, projectileLocationY * GlobalConstants.SIZE, GlobalConstants.SUPER_PROJECTILE_WIDTH, GlobalConstants.PROJECTILE_LENGTH))
            return true;
          else
            return false;
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

			//if inside rock because of edge direction
			if (_world.Map[_location.X, _location.Y].Type == TileType.Rock && (_directionX == 1 || _directionY == 1))
			{
				_directionX = 2;
				_directionY = 2;
			}

			if (_world.Map[_location.X, _location.Y].Type == TileType.Rock && (_directionX == 1 || _directionY == 2))
			{
				_directionX = 2;
				_directionY = 1;
			}

			if (_world.Map[_location.X, _location.Y].Type == TileType.Rock && (_directionX == 2 || _directionY == 1))
			{
				_directionX = 1;
				_directionY = 2;
			}

			if (_world.Map[_location.X, _location.Y].Type == TileType.Rock && (_directionX == 2 || _directionY == 2))
			{
				_directionX = 1;
				_directionY = 1;
			}

			//map edge, due to random direction and frame, need to put 2 statements to make sure enemy stays inside map 
			//in case direction push it out of the map on certain frames
			if (_world.Map[_location.X+1, _location.Y].Type == TileType.Rock || Location.X == 51 || Location.X == 50)
			{
				_directionX = 2;
				Random y = new Random();
				int yy = y.Next(1, 19);
				if (yy < 10)
				{
					_directionY = 1;
				}
				else 
				{
					_directionY = 2;
				}
			}

			if (_world.Map[_location.X-1, _location.Y].Type == TileType.Rock || Location.X == 1 || Location.X == 2)
			{
				_directionX = 1;
				Random y = new Random();
				int yy = y.Next(1, 19);
				if (yy< 10)
				{
					_directionY = 1;
				}
				else 
				{
					_directionY = 2;
				}
			}

			//up down
			if (_world.Map[_location.X, _location.Y+1].Type == TileType.Rock || Location.Y == 35 || Location.Y == 34)
			{
				_directionY = 2;
				Random y = new Random();
				int yy = y.Next(1, 19);
				if (yy< 10)
				{
					_directionX = 1;
				}
				else 
				{
					_directionX = 2;
				}
			}

			if (_world.Map[_location.X, _location.Y-1].Type == TileType.Rock || Location.Y == 1 || Location.Y == 2)
			{
				_directionY = 1;
				Random y = new Random();
				int yy = y.Next(1, 19);
				if (yy< 10)
				{
					_directionX = 1;
				}
				else 
				{
					_directionX = 2;
				}
			}


			//right left up down sharp edge collision
			if (_world.Map[_location.X-1, _location.Y].Type == TileType.Rock && _world.Map[_location.X, _location.Y+1].Type == TileType.Rock && (_directionX == 2 || _directionY == 1))
			{
				_directionX = 1;
				_directionY = 2;
			}

			if (_world.Map[_location.X-1, _location.Y].Type == TileType.Rock && _world.Map[_location.X, _location.Y-1].Type == TileType.Rock && (_directionX == 2 || _directionY == 2))
			{
				_directionX = 1;
				_directionY = 1;
			}

			if (_world.Map[_location.X+1, _location.Y].Type == TileType.Rock && _world.Map[_location.X, _location.Y+1].Type == TileType.Rock && (_directionX == 1 || _directionY == 1))
			{
				_directionX = 2;
				_directionY = 2;
			}

			if (_world.Map[_location.X+1, _location.Y].Type == TileType.Rock && _world.Map[_location.X, _location.Y-1].Type == TileType.Rock && (_directionX == 1 || _directionY == 2))
			{
				_directionX = 2;
				_directionY = 1;
			}
		}

		int randomcounter = 0;
		public void RandomDirection()
		{
			if (randomcounter < 13) //change this to increase/decrease direction change frequency - jeremy
			{
				randomcounter++;
			}
			if (randomcounter == 13)
			{
				//random directionX
				if (_directionX == 0 && _directionY == 1)
				{
					Random r = new Random();
					int nextdirection = r.Next(0, 39);

					if (nextdirection < 10 && _world.Map[_location.X, _location.Y + 1].Type != TileType.Rock && _world.Map[_location.X, _location.Y - 1].Type != TileType.Rock && _world.Map[_location.X + 1, _location.Y].Type != TileType.Rock && _world.Map[_location.X - 1, _location.Y].Type != TileType.Rock)					{
						_directionX = 2;
						_directionY = 0;
						randomcounter = 0;
					}
					else if (nextdirection >= 10 && nextdirection <= 19 && _world.Map[_location.X, _location.Y + 1].Type != TileType.Rock && _world.Map[_location.X, _location.Y - 1].Type != TileType.Rock && _world.Map[_location.X + 1, _location.Y].Type != TileType.Rock && _world.Map[_location.X - 1, _location.Y].Type != TileType.Rock)					{
						_directionY = 2;
						_directionX = 0;
						randomcounter = 0;
					}
					else if (nextdirection >= 20 && nextdirection <= 29 && _world.Map[_location.X, _location.Y + 1].Type != TileType.Rock && _world.Map[_location.X, _location.Y - 1].Type != TileType.Rock && _world.Map[_location.X + 1, _location.Y].Type != TileType.Rock && _world.Map[_location.X - 1, _location.Y].Type != TileType.Rock)					{
						_directionX = 1;
						_directionY = 0;
						randomcounter = 0;
					}
					else if (nextdirection >= 30 && nextdirection <= 39 && _world.Map[_location.X, _location.Y + 1].Type != TileType.Rock && _world.Map[_location.X, _location.Y - 1].Type != TileType.Rock && _world.Map[_location.X + 1, _location.Y].Type != TileType.Rock && _world.Map[_location.X - 1, _location.Y].Type != TileType.Rock)					{
						_directionY = 1;
						_directionX = 0;
						randomcounter = 0;
					}
					else 
					{
						randomcounter = 0;
					}
				}

				if (_directionX == 0 && _directionY == 2)
				{
					Random r = new Random();
					int nextdirection = r.Next(0, 39);

					if (nextdirection < 10 && _world.Map[_location.X, _location.Y + 1].Type != TileType.Rock && _world.Map[_location.X, _location.Y - 1].Type != TileType.Rock && _world.Map[_location.X + 1, _location.Y].Type != TileType.Rock && _world.Map[_location.X - 1, _location.Y].Type != TileType.Rock)					{
						_directionX = 1;
						_directionY = 0;
						randomcounter = 0;
					}
					else if (nextdirection >= 10 && nextdirection <= 19 && _world.Map[_location.X, _location.Y + 1].Type != TileType.Rock && _world.Map[_location.X, _location.Y - 1].Type != TileType.Rock && _world.Map[_location.X + 1, _location.Y].Type != TileType.Rock && _world.Map[_location.X - 1, _location.Y].Type != TileType.Rock)					{
						_directionY = 1;
						_directionX = 0;
						randomcounter = 0;
					}
					else if (nextdirection >= 20 && nextdirection <= 29 && _world.Map[_location.X, _location.Y + 1].Type != TileType.Rock && _world.Map[_location.X, _location.Y - 1].Type != TileType.Rock && _world.Map[_location.X + 1, _location.Y].Type != TileType.Rock && _world.Map[_location.X - 1, _location.Y].Type != TileType.Rock)					{
						_directionX = 2;
						_directionY = 0;
						randomcounter = 0;
					}
					else if (nextdirection >= 30 && nextdirection <= 39 && _world.Map[_location.X, _location.Y + 1].Type != TileType.Rock && _world.Map[_location.X, _location.Y - 1].Type != TileType.Rock && _world.Map[_location.X + 1, _location.Y].Type != TileType.Rock && _world.Map[_location.X - 1, _location.Y].Type != TileType.Rock)					{
						_directionY = 2;
						_directionX = 0;
						randomcounter = 0;
					}
					else 
					{
						randomcounter = 0;
					}
				}

				if (_directionX == 1 && _directionY == 0)
				{
					Random r = new Random();
					int nextdirection = r.Next(0, 39);

						if (nextdirection < 10 && _world.Map[_location.X, _location.Y + 1].Type != TileType.Rock && _world.Map[_location.X, _location.Y - 1].Type != TileType.Rock && _world.Map[_location.X + 1, _location.Y].Type != TileType.Rock && _world.Map[_location.X - 1, _location.Y].Type != TileType.Rock)					{
						_directionX = 0;
						_directionY = 1;
						randomcounter = 0;
					}
						else if (nextdirection >= 10 && nextdirection <= 19 && _world.Map[_location.X, _location.Y + 1].Type != TileType.Rock && _world.Map[_location.X, _location.Y - 1].Type != TileType.Rock && _world.Map[_location.X + 1, _location.Y].Type != TileType.Rock && _world.Map[_location.X - 1, _location.Y].Type != TileType.Rock)					{
						_directionX = 0;
						_directionY = 2;
						randomcounter = 0;
					}
						else if (nextdirection >= 20 && nextdirection <= 29 && _world.Map[_location.X, _location.Y + 1].Type != TileType.Rock && _world.Map[_location.X, _location.Y - 1].Type != TileType.Rock && _world.Map[_location.X + 1, _location.Y].Type != TileType.Rock && _world.Map[_location.X - 1, _location.Y].Type != TileType.Rock)					{
						_directionX = 2;
						_directionY = 0;
						randomcounter = 0;
					}
						else if (nextdirection >= 30 && nextdirection <= 39 && _world.Map[_location.X, _location.Y + 1].Type != TileType.Rock && _world.Map[_location.X, _location.Y - 1].Type != TileType.Rock && _world.Map[_location.X + 1, _location.Y].Type != TileType.Rock && _world.Map[_location.X - 1, _location.Y].Type != TileType.Rock)					{
						_directionX = 1;
						_directionY = 0;
						randomcounter = 0;
					}
					else 
					{
						randomcounter = 0;
					}
				}

				if (_directionX == 1 && _directionY == 1)
				{
					Random r = new Random();
					int nextdirection = r.Next(0, 39);

					if (nextdirection < 10 && _world.Map[_location.X, _location.Y + 1].Type != TileType.Rock && _world.Map[_location.X, _location.Y - 1].Type != TileType.Rock && _world.Map[_location.X + 1, _location.Y].Type != TileType.Rock && _world.Map[_location.X - 1, _location.Y].Type != TileType.Rock)					{
						_directionX = 0;
						_directionY = 1;
						randomcounter = 0;
					}
					else if (nextdirection >= 10 && nextdirection <= 19 && _world.Map[_location.X, _location.Y + 1].Type != TileType.Rock && _world.Map[_location.X, _location.Y - 1].Type != TileType.Rock && _world.Map[_location.X + 1, _location.Y].Type != TileType.Rock && _world.Map[_location.X - 1, _location.Y].Type != TileType.Rock)					{
						_directionX = 0;
						_directionY = 2;
						randomcounter = 0;
					}
					else if (nextdirection >= 20 && nextdirection <= 29 && _world.Map[_location.X, _location.Y + 1].Type != TileType.Rock && _world.Map[_location.X, _location.Y - 1].Type != TileType.Rock && _world.Map[_location.X + 1, _location.Y].Type != TileType.Rock && _world.Map[_location.X - 1, _location.Y].Type != TileType.Rock)					{
						_directionX = 2;
						_directionY = 0;
						randomcounter = 0;
					}
					else if (nextdirection >= 30 && nextdirection <= 39 && _world.Map[_location.X, _location.Y + 1].Type != TileType.Rock && _world.Map[_location.X, _location.Y - 1].Type != TileType.Rock && _world.Map[_location.X + 1, _location.Y].Type != TileType.Rock && _world.Map[_location.X - 1, _location.Y].Type != TileType.Rock)					{
						_directionX = 1;
						_directionY = 0;
						randomcounter = 0;
					}
					else 
					{
						randomcounter = 0;
					}
				}

				if (_directionX == 1 && _directionY == 2)
				{
					Random r = new Random();
					int nextdirection = r.Next(0, 39);

					if (nextdirection < 10 && _world.Map[_location.X, _location.Y + 1].Type != TileType.Rock && _world.Map[_location.X, _location.Y - 1].Type != TileType.Rock && _world.Map[_location.X + 1, _location.Y].Type != TileType.Rock && _world.Map[_location.X - 1, _location.Y].Type != TileType.Rock)					{
						_directionX = 0;
						_directionY = 1;
						randomcounter = 0;
					}
					else if (nextdirection >= 10 && nextdirection <= 19 && _world.Map[_location.X, _location.Y + 1].Type != TileType.Rock && _world.Map[_location.X, _location.Y - 1].Type != TileType.Rock && _world.Map[_location.X + 1, _location.Y].Type != TileType.Rock && _world.Map[_location.X - 1, _location.Y].Type != TileType.Rock)					{
						_directionX = 0;
						_directionY = 2;
						randomcounter = 0;
					}
					else if (nextdirection >= 20 && nextdirection <= 29 && _world.Map[_location.X, _location.Y + 1].Type != TileType.Rock && _world.Map[_location.X, _location.Y - 1].Type != TileType.Rock && _world.Map[_location.X + 1, _location.Y].Type != TileType.Rock && _world.Map[_location.X - 1, _location.Y].Type != TileType.Rock)					{
						_directionX = 2;
						_directionY = 0;
						randomcounter = 0;
					}
					else if (nextdirection >= 30 && nextdirection <= 39 && _world.Map[_location.X, _location.Y + 1].Type != TileType.Rock && _world.Map[_location.X, _location.Y - 1].Type != TileType.Rock && _world.Map[_location.X + 1, _location.Y].Type != TileType.Rock && _world.Map[_location.X - 1, _location.Y].Type != TileType.Rock)					{
						_directionX = 1;
						_directionY = 0;
						randomcounter = 0;
					}
					else 
					{
						randomcounter = 0;
					}
				}

				if (_directionX == 2 && _directionY == 0)
				{
					Random r = new Random();
					int nextdirection = r.Next(0, 39);

					if (nextdirection < 10 && _world.Map[_location.X, _location.Y + 1].Type != TileType.Rock && _world.Map[_location.X, _location.Y - 1].Type != TileType.Rock && _world.Map[_location.X + 1, _location.Y].Type != TileType.Rock && _world.Map[_location.X - 1, _location.Y].Type != TileType.Rock)					{
						_directionX = 0;
						_directionY = 1;
						randomcounter = 0;
					}
					else if (nextdirection >= 10 && nextdirection <= 19 && _world.Map[_location.X, _location.Y + 1].Type != TileType.Rock && _world.Map[_location.X, _location.Y - 1].Type != TileType.Rock && _world.Map[_location.X + 1, _location.Y].Type != TileType.Rock && _world.Map[_location.X - 1, _location.Y].Type != TileType.Rock)					{
						_directionX = 0;
						_directionY = 2;
						randomcounter = 0;
					}
					else if (nextdirection >= 20 && nextdirection <= 29 && _world.Map[_location.X, _location.Y + 1].Type != TileType.Rock && _world.Map[_location.X, _location.Y - 1].Type != TileType.Rock && _world.Map[_location.X + 1, _location.Y].Type != TileType.Rock && _world.Map[_location.X - 1, _location.Y].Type != TileType.Rock)					{
						_directionX = 1;
						_directionY = 0;
						randomcounter = 0;
					}
					else if (nextdirection >= 30 && nextdirection <= 39 && _world.Map[_location.X, _location.Y + 1].Type != TileType.Rock && _world.Map[_location.X, _location.Y - 1].Type != TileType.Rock && _world.Map[_location.X + 1, _location.Y].Type != TileType.Rock && _world.Map[_location.X - 1, _location.Y].Type != TileType.Rock)					{
						_directionX = 2;
						_directionY = 0;
						randomcounter = 0;
					}
					else 
					{
						randomcounter = 0;
					}
				}

				if (_directionX == 2 && _directionY == 1)
				{
					Random r = new Random();
					int nextdirection = r.Next(0, 39);

					if (nextdirection < 10 && _world.Map[_location.X, _location.Y + 1].Type != TileType.Rock && _world.Map[_location.X, _location.Y - 1].Type != TileType.Rock && _world.Map[_location.X + 1, _location.Y].Type != TileType.Rock && _world.Map[_location.X - 1, _location.Y].Type != TileType.Rock)					{
						_directionX = 0;
						_directionY = 1;
						randomcounter = 0;
					}
					else if (nextdirection >= 10 && nextdirection <= 19 && _world.Map[_location.X, _location.Y + 1].Type != TileType.Rock && _world.Map[_location.X, _location.Y - 1].Type != TileType.Rock && _world.Map[_location.X + 1, _location.Y].Type != TileType.Rock && _world.Map[_location.X - 1, _location.Y].Type != TileType.Rock)					{
						_directionX = 0;
						_directionY = 2;
						randomcounter = 0;
					}
					else if (nextdirection >= 20 && nextdirection <= 29 && _world.Map[_location.X, _location.Y + 1].Type != TileType.Rock && _world.Map[_location.X, _location.Y - 1].Type != TileType.Rock && _world.Map[_location.X + 1, _location.Y].Type != TileType.Rock && _world.Map[_location.X - 1, _location.Y].Type != TileType.Rock)					{
						_directionX = 1;
						_directionY = 0;
						randomcounter = 0;
					}
					else if (nextdirection >= 30 && nextdirection <= 39 && _world.Map[_location.X, _location.Y + 1].Type != TileType.Rock && _world.Map[_location.X, _location.Y - 1].Type != TileType.Rock && _world.Map[_location.X + 1, _location.Y].Type != TileType.Rock && _world.Map[_location.X - 1, _location.Y].Type != TileType.Rock)					{
						_directionX = 2;
						_directionY = 0;
						randomcounter = 0;
					}
					else 
					{
						randomcounter = 0;
					}
				}

				if (_directionX == 2 && _directionY == 2)
				{
					Random r = new Random();
					int nextdirection = r.Next(0, 39);

					if (nextdirection < 10 && _world.Map[_location.X, _location.Y + 1].Type != TileType.Rock && _world.Map[_location.X, _location.Y - 1].Type != TileType.Rock && _world.Map[_location.X + 1, _location.Y].Type != TileType.Rock && _world.Map[_location.X - 1, _location.Y].Type != TileType.Rock)					{
						_directionX = 0;
						_directionY = 1;
						randomcounter = 0;
					}
					else if (nextdirection >= 10 && nextdirection <= 19 && _world.Map[_location.X, _location.Y + 1].Type != TileType.Rock && _world.Map[_location.X, _location.Y - 1].Type != TileType.Rock && _world.Map[_location.X + 1, _location.Y].Type != TileType.Rock && _world.Map[_location.X - 1, _location.Y].Type != TileType.Rock)					{
						_directionX = 0;
						_directionY = 2;
						randomcounter = 0;
					}
					else if (nextdirection >= 20 && nextdirection <= 29 && _world.Map[_location.X, _location.Y + 1].Type != TileType.Rock && _world.Map[_location.X, _location.Y - 1].Type != TileType.Rock && _world.Map[_location.X + 1, _location.Y].Type != TileType.Rock && _world.Map[_location.X - 1, _location.Y].Type != TileType.Rock)					{
						_directionX = 1;
						_directionY = 0;
						randomcounter = 0;
					}
					else if (nextdirection >= 30 && nextdirection <= 39 && _world.Map[_location.X, _location.Y + 1].Type != TileType.Rock && _world.Map[_location.X, _location.Y - 1].Type != TileType.Rock && _world.Map[_location.X + 1, _location.Y].Type != TileType.Rock && _world.Map[_location.X - 1, _location.Y].Type != TileType.Rock)					{
						_directionX = 2;
						_directionY = 0;
						randomcounter = 0;
					}
					else 
					{
						randomcounter = 0;
					}
				}
			}
		}

		public int DirectionX 
		{
			get { return _directionX;} set { _directionX = value;}
		}

		public int DirectionY
		{
			get { return _directionY; }
			set { _directionY = value; } 
		}

		public override void Draw()
		{
				int size = Escapade.GetWorld().Size;
				SwinGame.FillRectangle(_enemyColor, Location.X * size, Location.Y * size, size, size);
				SwinGame.DrawRectangle(Color.White, Location.X * size, Location.Y * size, size, size);
		}

		/// <summary>
		/// Update the player on game tick by moving it towards the target
		/// </summary>
		public override void Update()
		{
			enemyMovement();
			RandomDirection();
		}
	}
}
