using System;
using SwinGameSDK;

namespace Escapade
{
  public class Projectile : Entity
  {
    private int _projectileSpeed;
    private AttackDirection _direction;
    private WeaponType _weaponType;
    private int projectileLocationX, projectileLocationY;
    private bool horizontal;

    private const int NORMAL_PROJECTILE_WIDTH = 10;
    private const int SUPER_PROJECTILE_WIDTH = 20;

    private const int PROJECTILE_LENGTH = 10;

    public Projectile (WeaponType weaponType, int projectileSpeed, AttackDirection direction,Location location) : base(2, "Projectile", location)
    {
      _projectileSpeed = projectileSpeed;
      _direction = direction;
      _weaponType = weaponType;
      projectileLocationX = location.X;
      projectileLocationY = location.Y;

      if (direction == AttackDirection.Left || direction == AttackDirection.Right)
        horizontal = true;
      else
        horizontal = false;
          
    }

    public override void Update ()
    {
       if (_direction == AttackDirection.Left || _direction == AttackDirection.Right) 
      {
          //int worldBorder = GlobalConstants.WORLD_WIDTH;

          if (_direction == AttackDirection.Right)
            projectileLocationX += _projectileSpeed;
          else
            projectileLocationX -= _projectileSpeed;

      } 
      else 
      {
        //int worldBorder = GlobalConstants.WORLD_HEIGHT;

        if (_direction == AttackDirection.Down) 
          projectileLocationY += _projectileSpeed;
          else
            projectileLocationY -= _projectileSpeed;
       } 
    }

    public override void Draw ()
    {
    	if (_weaponType == WeaponType.Normal) {

    		if (horizontal)
    			SwinGame.FillRectangle (Color.Brown, projectileLocationX* GlobalConstants.SIZE, projectileLocationY* GlobalConstants.SIZE, PROJECTILE_LENGTH, NORMAL_PROJECTILE_WIDTH);
    		else
    			SwinGame.FillRectangle (Color.Brown, projectileLocationX* GlobalConstants.SIZE, projectileLocationY* GlobalConstants.SIZE, NORMAL_PROJECTILE_WIDTH, PROJECTILE_LENGTH);
    	} else {
    		if (horizontal)
    			SwinGame.FillRectangle (Color.Gold, projectileLocationX* GlobalConstants.SIZE, projectileLocationY* GlobalConstants.SIZE, PROJECTILE_LENGTH, SUPER_PROJECTILE_WIDTH);
    		else
    			SwinGame.FillRectangle (Color.Gold, projectileLocationX* GlobalConstants.SIZE, projectileLocationY* GlobalConstants.SIZE, SUPER_PROJECTILE_WIDTH, PROJECTILE_LENGTH);
    	}
    }

    public bool CheckObjectHit (World world, Enemy enemy) // JY- collision works
    {
    	int enemyX = enemy.Location.X * GlobalConstants.SIZE, enemyY = enemy.Location.Y * GlobalConstants.SIZE;

    	if (_weaponType == WeaponType.Normal) {
    		if (horizontal) 
        {
    			if (world.Map [projectileLocationX, projectileLocationY].Type == TileType.Rock || SwinGame.PointInRect (SwinGame.PointAt (enemyX, enemyY), projectileLocationX * GlobalConstants.SIZE, projectileLocationY * GlobalConstants.SIZE, PROJECTILE_LENGTH, NORMAL_PROJECTILE_WIDTH))
    				return true;
    			else
    				return false;
    		} 
        else 
        {
    			if (world.Map [projectileLocationX, projectileLocationY].Type == TileType.Rock || SwinGame.PointInRect (SwinGame.PointAt (enemyX, enemyY), projectileLocationX * GlobalConstants.SIZE, projectileLocationY * GlobalConstants.SIZE, NORMAL_PROJECTILE_WIDTH, PROJECTILE_LENGTH))
    				return true;
    			else
    				return false;
    		}

    	} 
      else 
      {
    		if (horizontal) 
        {
    			if (world.Map [projectileLocationX, projectileLocationY].Type == TileType.Rock || SwinGame.PointInRect (SwinGame.PointAt (enemyX, enemyY), projectileLocationX * GlobalConstants.SIZE, projectileLocationY * GlobalConstants.SIZE, PROJECTILE_LENGTH, SUPER_PROJECTILE_WIDTH))
    				return true;
    			else
    				return false;
    		} 
        else 
        {
    			if (world.Map [projectileLocationX, projectileLocationY].Type == TileType.Rock || SwinGame.PointInRect (SwinGame.PointAt (enemyX, enemyY), projectileLocationX * GlobalConstants.SIZE, projectileLocationY * GlobalConstants.SIZE, SUPER_PROJECTILE_WIDTH, PROJECTILE_LENGTH))
    				return true;
    			else
    				return false;
    		}

    	}
    }

  }
}
