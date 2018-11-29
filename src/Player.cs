using Escapade.item;
using Escapade.src.gui;
using SwinGameSDK;

namespace Escapade
{
    public class Player : Entity
    {
        Inventory _inventory;
        Weapon _weapon;
        private Color _playerColor;
        private string _name;

        #region Properties
        public Inventory Inventory
        {
            get
            {
                return _inventory;
            }
            set
            {
                _inventory = value;
            }
        }
        #endregion Properties

        /// <summary>
        /// Initializes a new <see cref="T:Escapade.Player"/>
        /// </summary>
        /// <param name="id">Unique id</param>
        /// <param name="name">Name</param>
        /// <param name="location">Starting location</param>
        public Player(int id, string name, Location location) : base(id, name, location)
        {
            Inventory = new Inventory();
            _location.X = location.X;
            _location.Y = location.Y;
            _name = name;

            // differentiate between the player 1 and player 2 - jeremy toh
            if (name == "Player2")
                _playerColor = Color.Black;
            else
                _playerColor = Color.MediumSlateBlue;
        }

        public Weapon Weapon
        {
            get { return _weapon; }
        }

        public void DestroyWeapon()
        {
            _weapon = null;
        }

        public void BuyWeapon(Location location, WeaponType weaponType) //JY- player buys weapon
        {
            _weapon = new Weapon(_location, weaponType, _name);
        }

        /// <summary>
        /// Deploys the weapon.
        /// </summary>
        /// <param name="attackDirection">Attack direction.</param>
        public void DeployWeapon(AttackDirection attackDirection) //JY- player uses weapon
        {
            if (_weapon != null)
                _weapon.Attack(_location, attackDirection);
            MetaHandler.DisplayAmmunitionLevel(_weapon);
        }

        /// <summary>
        /// Players hit by projectile
        /// </summary>
        /// <returns><c>true</c>, if player is hit by projectile, <c>false</c> otherwise.</returns>
        /// <param name="projectile">Projectile.</param>
        public bool PlayerHitbyProjectile(Projectile projectile)
        {
            int playerX = Location.X * GlobalConstants.SIZE, playerY = Location.Y * GlobalConstants.SIZE;
            int projectileLocationX = projectile.ProjectileLocationX, projectileLocationY = projectile.ProjectileLocationY;

            if (projectile.Type == WeaponType.Normal)
            {
                if (projectile.Horizontal)
                {
                    if (SwinGame.PointInRect(SwinGame.PointAt(playerX, playerY), projectileLocationX * GlobalConstants.SIZE, projectileLocationY * GlobalConstants.SIZE, GlobalConstants.NORMAL_PROJECTILE_WIDTH, GlobalConstants.PROJECTILE_LENGTH))
                        if (_name != (projectile.Owner))
                            return true;
                        else
                            return false;
                    else
                        return false;
                }
                else
                {
                    if (SwinGame.PointInRect(SwinGame.PointAt(playerX, playerY), projectileLocationX * GlobalConstants.SIZE, projectileLocationY * GlobalConstants.SIZE, GlobalConstants.NORMAL_PROJECTILE_WIDTH, GlobalConstants.PROJECTILE_LENGTH))
                        if (_name != (projectile.Owner))
                            return true;
                        else
                            return false;
                    else
                        return false;
                }

            }
            else
            {
                if (projectile.Horizontal)
                {
                    if (SwinGame.PointInRect(SwinGame.PointAt(playerX, playerY), projectileLocationX * GlobalConstants.SIZE, projectileLocationY * GlobalConstants.SIZE, GlobalConstants.PROJECTILE_LENGTH, GlobalConstants.SUPER_PROJECTILE_WIDTH))
                        if (_name != (projectile.Owner))
                            return true;
                        else
                            return false;
                    else
                        return false;
                }
                else
                {
                    if (SwinGame.PointInRect(SwinGame.PointAt(playerX, playerY), projectileLocationX * GlobalConstants.SIZE, projectileLocationY * GlobalConstants.SIZE, GlobalConstants.SUPER_PROJECTILE_WIDTH, GlobalConstants.PROJECTILE_LENGTH))
                        if (_name != (projectile.Owner))
                            return true;
                        else
                            return false;
                    else
                        return false;
                }

            }
        }

       /// <summary>
       /// Players hit by enemy
       /// </summary>
       /// <returns><c>true</c>, if enemy hits player, <c>false</c> otherwise.</returns>
       /// <param name="e">E.</param>
        public bool PlayerHitbyEnemy(Enemy e)
        {
            int playerX = Location.X * GlobalConstants.SIZE, playerY = Location.Y * GlobalConstants.SIZE;
            if (SwinGame.PointInRect(SwinGame.PointAt(playerX, playerY), e.Location.X * GlobalConstants.SIZE, e.Location.Y * GlobalConstants.SIZE, 15, 15))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Draws this player at its current location, and draws a
        /// path to its target location if it has one
        /// </summary>
        public override void Draw()
        {
            int size = Escapade.GetWorld().Size;
            SwinGame.FillRectangle(_playerColor, Location.X * size, Location.Y * size, size, size);
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
