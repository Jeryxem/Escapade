using System;
using SwinGameSDK;

namespace Escapade
{
  public class Projectile : Entity
  {
    private int _projectileSpeed;
    private string _owner;
    private AttackDirection _direction;
    private WeaponType _weaponType;
    private int projectileLocationX, projectileLocationY;
    private bool horizontal;

    private const int NORMAL_PROJECTILE_WIDTH = 10;
    private const int SUPER_PROJECTILE_WIDTH = 20;

    private const int PROJECTILE_LENGTH = 10;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Escapade.Projectile"/> class.
    /// </summary>
    /// <param name="weaponType">Weapon type.</param>
    /// <param name="projectileSpeed">Projectile speed.</param>
    /// <param name="direction">Direction.</param>
    /// <param name="location">Location.</param>
    /// <param name="owner">Owner.</param>
    public Projectile (WeaponType weaponType, int projectileSpeed, AttackDirection direction,Location location, string owner) : base(2, "Projectile", location)
    {
      _projectileSpeed = projectileSpeed;
      _direction = direction;
      _weaponType = weaponType;
      _owner = owner;
      projectileLocationX = location.X;
      projectileLocationY = location.Y;

      if (direction == AttackDirection.Left || direction == AttackDirection.Right)
        horizontal = true;
      else
        horizontal = false;
          
    }

    /// <summary>
    /// Gets a value indicating whether this <see cref="T:Escapade.Projectile"/> is horizontal.
    /// </summary>
    /// <value><c>true</c> if horizontal; otherwise, <c>false</c>.</value>
    public bool Horizontal 
    {
      get { return horizontal; }
    }

    /// <summary>
    /// Gets the type of weapon the projectile belongs to
    /// </summary>
    /// <value>The type.</value>
    public WeaponType Type 
    {
      get { return _weaponType; }
    }

    /// <summary>
    /// Gets or sets the owner of the weapon that fired this projectile
    /// </summary>
    /// <value>The owner.</value>
    public string Owner 
    {
      get { return _owner; }
      set { _owner = value; }
    }

    /// <summary>
    /// Gets or sets the projectile's x coordinate
    /// </summary>
    /// <value>The projectile location x.</value>
    public int ProjectileLocationX 
    {
      get { return projectileLocationX; }
      set { projectileLocationX = value; }
    }

    /// <summary>
    /// Gets or sets the projectile's y coordinate
    /// </summary>
    /// <value>The projectile location y.</value>
    public int ProjectileLocationY 
    {
    	get { return projectileLocationY; }
    	set { projectileLocationY = value; }
        }

    /// <summary>
    /// Updates the position of the projectile
    /// </summary>
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

    /// <summary>
    /// Draw the projectile
    /// </summary>
    public override void Draw ()
    {
    	if (_weaponType == WeaponType.Normal) {

    		if (horizontal)
    			SwinGame.FillRectangle (Color.Brown, projectileLocationX* GlobalConstants.SIZE, projectileLocationY* GlobalConstants.SIZE, GlobalConstants.PROJECTILE_LENGTH, GlobalConstants.NORMAL_PROJECTILE_WIDTH);
    		else
    			SwinGame.FillRectangle (Color.Brown, projectileLocationX* GlobalConstants.SIZE, projectileLocationY* GlobalConstants.SIZE, GlobalConstants.NORMAL_PROJECTILE_WIDTH, GlobalConstants.PROJECTILE_LENGTH);
    	} else {
    		if (horizontal)
    			SwinGame.FillRectangle (Color.Gold, projectileLocationX* GlobalConstants.SIZE, projectileLocationY* GlobalConstants.SIZE, GlobalConstants.PROJECTILE_LENGTH, GlobalConstants.SUPER_PROJECTILE_WIDTH);
    		else
    			SwinGame.FillRectangle (Color.Gold, projectileLocationX* GlobalConstants.SIZE, projectileLocationY* GlobalConstants.SIZE, GlobalConstants.SUPER_PROJECTILE_WIDTH, GlobalConstants.PROJECTILE_LENGTH);
    	}
    }

    /// <summary>
    /// Checks the object hit.
    /// </summary>
    /// <returns><c>true</c>, if object hit was checked, <c>false</c> otherwise.</returns>
    /// <param name="world">World.</param>
    /// <param name="enemy">Enemy.</param>
    public bool CheckObjectHit (World world, Enemy enemy) // JY- collision works
    {
    	int enemyX = enemy.Location.X * GlobalConstants.SIZE, enemyY = enemy.Location.Y * GlobalConstants.SIZE;

    	if (_weaponType == WeaponType.Normal) {
    		if (horizontal) 
        {
    			if (world.Map [projectileLocationX, projectileLocationY].Type == TileType.Rock || SwinGame.PointInRect (SwinGame.PointAt (enemyX, enemyY), projectileLocationX * GlobalConstants.SIZE, projectileLocationY * GlobalConstants.SIZE, GlobalConstants.PROJECTILE_LENGTH, GlobalConstants.NORMAL_PROJECTILE_WIDTH))
    				return true;
    			else
    				return false;
    		} 
        else 
        {
    			if (world.Map [projectileLocationX, projectileLocationY].Type == TileType.Rock || SwinGame.PointInRect (SwinGame.PointAt (enemyX, enemyY), projectileLocationX * GlobalConstants.SIZE, projectileLocationY * GlobalConstants.SIZE, GlobalConstants.NORMAL_PROJECTILE_WIDTH, GlobalConstants.PROJECTILE_LENGTH))
    				return true;
    			else
    				return false;
    		}

    	} 
      else 
      {
    		if (horizontal) 
        {
    			if (world.Map [projectileLocationX, projectileLocationY].Type == TileType.Rock || SwinGame.PointInRect (SwinGame.PointAt (enemyX, enemyY), projectileLocationX * GlobalConstants.SIZE, projectileLocationY * GlobalConstants.SIZE, GlobalConstants.PROJECTILE_LENGTH, GlobalConstants.SUPER_PROJECTILE_WIDTH))
    				return true;
    			else
    				return false;
    		} 
        else 
        {
    			if (world.Map [projectileLocationX, projectileLocationY].Type == TileType.Rock || SwinGame.PointInRect (SwinGame.PointAt (enemyX, enemyY), projectileLocationX * GlobalConstants.SIZE, projectileLocationY * GlobalConstants.SIZE, GlobalConstants.SUPER_PROJECTILE_WIDTH, GlobalConstants.PROJECTILE_LENGTH))
    				return true;
    			else
    				return false;
    		}

    	}
    }

  }
}
