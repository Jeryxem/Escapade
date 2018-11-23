using System;
using NUnit.Framework;

namespace Escapade
{
  [TestFixture()]
  public class WeaponTest
  {
    [Test()]
    public void BuyTest ()
    {
      Location loc = new Location (25, 25);
      Player player1 = new Player (1, "Player 1", loc);
      Player player2 = new Player (2, "Player 2", loc);
      player1.BuyWeapon (loc, WeaponType.Normal);
      player2.BuyWeapon (loc, WeaponType.Super);

      Assert.AreEqual (40, player1.Weapon.Ammunition);
      Assert.IsTrue (player1.Weapon.Type == WeaponType.Normal);

      Assert.AreEqual (20, player2.Weapon.Ammunition);
      Assert.IsTrue (player2.Weapon.Type == WeaponType.Super);
    }

    [Test ()]
    public void AttackTest ()
    {
      Location loc = new Location (25, 25);

      Weapon normalWeapon = new Weapon (loc, WeaponType.Normal,"player 1");
      Weapon superWeapon = new Weapon (loc, WeaponType.Super, "player 2");

      normalWeapon.Attack (loc, AttackDirection.Up);
      normalWeapon.Attack (loc, AttackDirection.Up);
      normalWeapon.Attack (loc, AttackDirection.Up);

      Assert.AreEqual (37, normalWeapon.Ammunition);

      superWeapon.Attack (loc, AttackDirection.Down);
      superWeapon.Attack (loc, AttackDirection.Down);
      superWeapon.Attack (loc, AttackDirection.Down);
      superWeapon.Attack (loc, AttackDirection.Down);

      Assert.AreEqual (36, superWeapon.Ammunition);
    }
  }
}
