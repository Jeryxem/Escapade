using System;
using SwinGameSDK;

namespace Escapade
{
	public class Enemy : Entity
	{
		//private Timer _spawntimer;
		private int _directionX;
		private int _directionY;
    private Color _enemyColor;
		public World _world;
		//private bool _spawn = false;
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


    /*//spawn enemy every 5 sec(will change time for final product)- jeremy
		public void SpawnEnemy()
		{
			_spawntimer = SwinGame.CreateTimer();
			_spawntimer.Start();
			var milliseconds = _spawntimer.Ticks;
			var seconds = milliseconds / 1000;

			if (seconds == 2)
			{
				_spawn = true;
			}
			else if (seconds > 2)
			{
				_spawntimer.Reset();
			}else 
			{
				_spawn = false;
			}
		}*/

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

			if (_location.X == 0)
			{
				_directionX = 1;
			}


			if (_world.Map[_location.X+1, _location.Y].Type == TileType.Rock)
			{
				_directionX = 2;
			}

			if (_world.Map[_location.X-1, _location.Y].Type == TileType.Rock)
			{
				_directionX = 1;
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

			if (_world.Map[_location.X, _location.Y+1].Type == TileType.Rock)
			{
				_directionY = 2;

			}

			if (_world.Map[_location.X, _location.Y-1].Type == TileType.Rock)
			{
				_directionY = 1;
			}
		}

		//added properties for direction - jeremy
	/*	//spawn timer property
		public bool SpawnTimer
		{
			get { return _spawn; } 
		}*/

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
		}
	}
}
