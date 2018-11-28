using System;
using SwinGameSDK;

namespace Escapade
{
    public class Weapon : Entity
    {
        private int _damage, _projectileSpeed, _ammunition;
        private WeaponType _weaponType;
        private Projectile _projectile;
        private string _owner;

        private const int NORMAL_DAMAGE = 20;
        private const int SUPER_DAMAGE = 60;

        private const int NORMAL_PROJECTILE_SPEED = 1;
        private const int SUPER_PROJECTILE_SPEED = 1;

        private const string NORMAL_NAME = "Rusted Harpoon";
        private const string SUPER_NAME = "Golden Harpoon";

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Escapade.Weapon"/> class.
        /// </summary>
        /// <param name="location">Location.</param>
        /// <param name="weaponType">Weapon type.</param>
        /// <param name="owner">Owner.</param>
        public Weapon(Location location, WeaponType weaponType, string owner) : base(1, "Weapon", location)
        {
            _weaponType = weaponType;
            _owner = owner;
            _ammunition = 0; // IA - No longer fixing a number of projectiles here. This is controlled in Escapade.cs to allow the player to purchase as much as they can afford with mineral points.

            if (_weaponType == WeaponType.Normal)
            {
                _name = NORMAL_NAME;
                _damage = NORMAL_DAMAGE;
                _projectileSpeed = NORMAL_PROJECTILE_SPEED;
            }
            else
            {
                _name = SUPER_NAME;
                _damage = SUPER_DAMAGE;
                _projectileSpeed = SUPER_PROJECTILE_SPEED;
            }
        }

        #region Properties
        /// <summary>
        /// Gets the damage.
        /// </summary>
        /// <value>The damage.</value>
        public int Damage
        {
            get { return _damage; }
        }

        /// <summary>
        /// Gets the projectile.
        /// </summary>
        /// <value>The projectile.</value>
        public Projectile Projectile
        {
            get { return _projectile; }
        }

        /// <summary>
        /// Gets the projectile speed.
        /// </summary>
        /// <value>The projectile speed.</value>
        public int ProjectileSpeed
        {
            get { return _projectileSpeed; }
        }

        /// <summary>
        /// Gets the type of weapon
        /// </summary>
        /// <value>The type.</value>
        public WeaponType Type
        {
            get { return _weaponType; }
        }

        /// <summary>
        /// Gets the owner of the weapon
        /// </summary>
        /// <value>The owner.</value>
        public string Owner
        {
            get { return _owner; }
        }

        #endregion Properties

        /// <summary>
        /// Attack the specified location and direction.
        /// </summary>
        /// <returns>The attack.</returns>
        /// <param name="location">Location.</param>
        /// <param name="direction">Direction.</param>
        public void Attack(Location location, AttackDirection direction)
        {
            _projectile = new Projectile(_weaponType, _projectileSpeed, direction, location, _owner);
            Ammunition--;
        }

        public override void Update()
        {
        }

        public override void Draw()
        {

        }

        /// <summary>
        /// Gets or sets the ammunition.
        /// </summary>
        /// <value>The ammunition.</value>
        public int Ammunition
        {
            get { return _ammunition; }
            set { _ammunition = value; }
        }

        /// <summary>
        /// Resets the ammunition.
        /// </summary>
        public void ResetAmmunition()
        {
            _ammunition = 0;
        }

    }
}
