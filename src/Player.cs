using Escapade.item;
using Escapade.src.gui;
using SwinGameSDK;

namespace Escapade
{
  public class Player : Entity
  {
    Inventory _inventory;
    Weapon _weapon;

    #region Properties
    public Inventory Inventory {
      get {
        return _inventory;
      }
      set {
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
      Inventory = new Inventory ();
			_location.X = 20;
			_location.Y = 20;
    }

    public Weapon Weapon 
    {
      get { return _weapon; }
    }

    public void BuyWeapon (Location location, WeaponType weaponType) //JY- player buys weapon
    {
      _weapon = new Weapon (_location,weaponType);
    }

    public void DeployWeapon (AttackDirection attackDirection) //JY- player uses weapon
    {
      if (_weapon != null)
        _weapon.Attack (_location, attackDirection);
            MetaHandler.DisplayAmmunitionLevel(_weapon);
        }

    /// <summary>
    /// Draws this player at its current location, and draws a
    /// path to its target location if it has one
    /// </summary>
    public override void Draw()
    {
      int size = Escapade.GetWorld ().Size;
      SwinGame.FillRectangle(Color.MediumSlateBlue, Location.X * size, Location.Y * size, size, size);
      SwinGame.DrawRectangle(Color.White, Location.X * size, Location.Y * size, size, size);
    /*  if (Path.TargetPath.Count > 0) {
        foreach (Location loc in Path.TargetPath) {
          SwinGame.FillCircle(Color.LightPink, (loc.X * size) + (size / 2), (loc.Y * size) + (size / 2), size / 5);
          SwinGame.DrawRectangle(Color.White, loc.X* size, loc.Y* size, size, size);
        }
      }*/
    }

    /// <summary>
    /// Update the player on game tick by moving it towards the target
    /// </summary>
    public override void Update()
    {
     // Move();
    }
  }
}
