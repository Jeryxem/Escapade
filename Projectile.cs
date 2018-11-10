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

    private const int NORMAL_PROJECTILE_WIDTH = 5;
    private const int SUPER_PROJECTILE_WIDTH = 7;

    private const int PROJECTILE_LENGTH = 20;

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


  }
}
