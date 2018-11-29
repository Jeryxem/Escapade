using System;
using NUnit.Framework;

namespace Escapade
{
  [TestFixture()]
  public class WeaponTest
  {
    /// <summary>
    /// Test the weapon buying process
    /// </summary>
    [Test()]
    public void BuyTest ()
    {
      Location loc = new Location (25, 25);
      Player player1 = new Player (1, "Player 1", loc);
      Player player2 = new Player (2, "Player 2", loc);
      player1.BuyWeapon (loc, WeaponType.Normal);
      player2.BuyWeapon (loc, WeaponType.Super);

      StringAssert.AreEqualIgnoringCase ("Player 1", player1.Weapon.Owner);
      Assert.IsTrue (player1.Weapon.Type == WeaponType.Normal);

      StringAssert.AreEqualIgnoringCase ("Player 2", player2.Weapon.Owner);
      Assert.IsTrue (player2.Weapon.Type == WeaponType.Super);
    }

    /// <summary>
    /// Test firing the weapon
    /// </summary>
    [Test ()]
    public void AttackTest ()
    {
      Location loc = new Location (25, 25);

      Weapon normalWeapon = new Weapon (loc, WeaponType.Normal,"player 1");
      Weapon superWeapon = new Weapon (loc, WeaponType.Super, "player 2");

      //Attacked 3 times
      normalWeapon.Attack (loc, AttackDirection.Up);
      normalWeapon.Attack (loc, AttackDirection.Up);
      normalWeapon.Attack (loc, AttackDirection.Up);

      //Ammunition decreases by 3
      Assert.AreEqual (-3, normalWeapon.Ammunition);

      //Attack 4 times
      superWeapon.Attack (loc, AttackDirection.Down);
      superWeapon.Attack (loc, AttackDirection.Down);
      superWeapon.Attack (loc, AttackDirection.Down);
      superWeapon.Attack (loc, AttackDirection.Down);

      //Ammunition decreases by 4
      Assert.AreEqual (-4, superWeapon.Ammunition);
    }
  }
}
