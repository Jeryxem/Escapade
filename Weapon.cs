using System;
using SwinGameSDK;

namespace Escapade
{
  public class Weapon
  {
    private string _name;
    private bool _equipped;
    private int _damage, _projectileSpeed;
    private WeaponType _weaponType;

    private const int NORMAL_DAMAGE = 20;
    private const int SUPER_DAMAGE = 40;

    private const int NORMAL_PROJECTILE_SPEED = 10;
    private const int SUPER_PROJECTILE_SPEED = 20;

    private const string NORMAL_NAME = "Rusted Harpoon";
    private const string SUPER_NAME = "Golden Harpoon";

    private const int NORMAL_PROJECTILE_WIDTH = 20;
    private const int SUPER_PROJECTILE_WIDTH = 30;

    private const int PROJECTILE_LENGTH = 50;

    public Weapon () : this(WeaponType.Normal)
    {
    }

    public Weapon (WeaponType weaponType)
    {
      _weaponType = weaponType;
      _equipped = false;

      if (_weaponType == WeaponType.Normal) {
        _name = NORMAL_NAME;
        _damage = NORMAL_DAMAGE;
        _projectileSpeed = NORMAL_PROJECTILE_SPEED;
      } 
      else  {
        _name = SUPER_NAME;
        _damage = SUPER_DAMAGE;
        _projectileSpeed = SUPER_PROJECTILE_SPEED;
        }
    }

    #region Properties
    public string Name 
    {
      get { return _name; }
    }

    public int Damage 
    {
      get { return _damage; }
    }

    public int ProjectileSpeed 
    {
      get { return _projectileSpeed; }
    }

    public WeaponType Type 
    {
      get { return _weaponType; }
    }

    public bool Equipped 
    {
      get { return _equipped; }
      set { _equipped = value; }
    }
    #endregion Properties

    public void Attack (Location location,AttackDirection direction)
    {
      switch (direction) {
      case AttackDirection.Up:
        //attack up
        break;
      case AttackDirection.Down:
        //attack down
        break;
      case AttackDirection.Left:
        //attack left
        break;
      case AttackDirection.Right:
        //attack down
        break;
      }
    }

    public void Deploy (Location location, bool horizontal)
    {
      int worldBorder,projectileLocationX, projectileLocationY;
      int playerLocationX = location.X;
      int playerLocationY = location.Y;

      if (horizontal == true) {
        
        worldBorder = GlobalConstants.WORLD_WIDTH;
        projectileLocationX = playerLocationX;
        projectileLocationY = playerLocationY;

        while (playerLocationX != worldBorder) 
        {
          projectileLocationX += _projectileSpeed;
          DrawProjectile (projectileLocationX,projectileLocationY, true);
        }

      } 

      else {
        worldBorder = GlobalConstants.WORLD_HEIGHT;
        projectileLocationX = playerLocationX;
        projectileLocationY = playerLocationY;

        while (playerLocationY != worldBorder) 
        {
          projectileLocationY += _projectileSpeed;
          DrawProjectile (projectileLocationX,projectileLocationY, false);
        }
      }

    }

    public void DrawProjectile (int projectileLocationX, int projectileLocationY, bool horizontal)
    {
      if (_weaponType == WeaponType.Normal) {
        
        if (horizontal)
          SwinGame.DrawRectangle (Color.Brown, projectileLocationX, projectileLocationY, PROJECTILE_LENGTH, NORMAL_PROJECTILE_WIDTH);
        else
          SwinGame.DrawRectangle (Color.Brown, projectileLocationX, projectileLocationY, NORMAL_PROJECTILE_WIDTH, PROJECTILE_LENGTH);
      } 
      else {
        if (horizontal)
          SwinGame.DrawRectangle (Color.Gold, projectileLocationX, projectileLocationY, PROJECTILE_LENGTH, SUPER_PROJECTILE_WIDTH);
        else
          SwinGame.DrawRectangle (Color.Gold, projectileLocationX, projectileLocationY, SUPER_PROJECTILE_WIDTH, PROJECTILE_LENGTH);
      }
    }
  }
}
