﻿using System;
using SwinGameSDK;

namespace Escapade
{
  public class Weapon : Entity
  {
    private bool _equipped;
    private int _damage, _projectileSpeed;
    private WeaponType _weaponType;
    private Projectile _projectile;

    private const int NORMAL_DAMAGE = 20;
    private const int SUPER_DAMAGE = 40;

    private const int NORMAL_PROJECTILE_SPEED = 1;
    private const int SUPER_PROJECTILE_SPEED = 2;

    private const string NORMAL_NAME = "Rusted Harpoon";
    private const string SUPER_NAME = "Golden Harpoon";

    public Weapon (Location location, WeaponType weaponType) : base (1, "Weapon", location)
    {
      _weaponType = weaponType;
      _equipped = false;

      if (_weaponType == WeaponType.Normal) {
        _name = NORMAL_NAME;
        _damage = NORMAL_DAMAGE;
        _projectileSpeed = NORMAL_PROJECTILE_SPEED;
      } else {
        _name = SUPER_NAME;
        _damage = SUPER_DAMAGE;
        _projectileSpeed = SUPER_PROJECTILE_SPEED;
      }
    }

    #region Properties
    public int Damage {
      get { return _damage; }
    }

    public Projectile Projectile 
    {
      get { return _projectile; }
    }

    public int ProjectileSpeed {
      get { return _projectileSpeed; }
    }

    public WeaponType Type {
      get { return _weaponType; }
    }

    public bool Equipped {
      get { return _equipped; }
      set { _equipped = value; }
    }
    #endregion Properties

    public void Attack (Location location, AttackDirection direction)
    {
      _projectile = new Projectile (_weaponType, _projectileSpeed, direction, location);
    }

    public override void Update ()
    {
    }

    public override void Draw ()
    {
       
    }

  }
}