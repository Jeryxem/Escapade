using System;
using NUnit.Framework;

namespace Escapade
{
  [TestFixture()]
  public class ProjectileTest
  {

    /// <summary>
    /// Test the properties of the projectiles
    /// </summary>
    [Test()]
    public void PropertiesTest ()
    {
      Location loc = new Location (25, 25);
      Projectile projectile1 = new Projectile (WeaponType.Normal, 1, AttackDirection.Up, loc, "Player 1");
      Projectile projectile2 = new Projectile (WeaponType.Super, 1, AttackDirection.Left, loc, "Player 2");

      Assert.IsTrue (projectile1.Horizontal == false);
      Assert.IsTrue (projectile1.Type == WeaponType.Normal);

      Assert.IsTrue (projectile2.Horizontal == true);
      Assert.IsTrue (projectile2.Type == WeaponType.Super);

    }
  }
}
