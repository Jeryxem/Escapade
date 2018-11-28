using System;
using NUnit.Framework;
using SwinGameSDK;

namespace Escapade
{
	[TestFixture()]
	public class CollisionAndDirectionTest
	{
		[Test()]
		public void EnemyMovementTest()
		{
			Location l = new Location(25, 20);
			Enemy boss = new Enemy(0, "Boss Enemy", l, 0, 1);

			boss._world = new World(GlobalConstants.WORLD_WIDTH, GlobalConstants.WORLD_HEIGHT, GlobalConstants.SIZE);
			//set all tiles to air
			for (int x = 0; x<GlobalConstants.WORLD_WIDTH/GlobalConstants.SIZE; x++)
			{
				for (int y = 0; y<GlobalConstants.WORLD_HEIGHT/GlobalConstants.SIZE; y++)
					boss._world.Map[x, y] = new Tile(TileType.Air);
			}

			//test movement
			boss.enemyMovement();
			Assert.AreEqual(21, boss.Location.Y);
			boss.enemyMovement();
			Assert.AreEqual(25, boss.Location.X);
			Assert.AreEqual(22, boss.Location.Y);

			//change Y direction
			boss.DirectionY = 2;
			boss.enemyMovement();
			Assert.AreEqual(25, boss.Location.X);
			Assert.AreEqual(21, boss.Location.Y);

			//change X direction
			boss.DirectionY = 0; //test reset direction Y to 0 so only X has movement
			boss.DirectionX = 1;
			boss.enemyMovement();
			Assert.AreEqual(26, boss.Location.X);
			Assert.AreEqual(21, boss.Location.Y);

			boss.DirectionX = 2;
			boss.enemyMovement();
			Assert.AreEqual(25, boss.Location.X);
			Assert.AreEqual(21, boss.Location.Y);
		}

		[Test()]
		public void CollisionTest() 
		{
			Location l = new Location(25, 20);
			Enemy boss1 = new Enemy(0, "Boss", l, 0, 1);
			boss1._world = new World(GlobalConstants.WORLD_WIDTH, GlobalConstants.WORLD_HEIGHT, GlobalConstants.SIZE);

			for (int x = 0; x < GlobalConstants.WORLD_WIDTH/GlobalConstants.SIZE; x++)
			{
				for (int y = 0; y < GlobalConstants.WORLD_HEIGHT/GlobalConstants.SIZE; y++)
					boss1._world.Map[x, y] = new Tile(TileType.Air);
			}
			boss1._world.Map[25, 22] = new Tile(TileType.Rock);

			//check initial, bottom is air so no collision, [25,21] is air
			Assert.AreEqual(TileType.Rock, boss1._world.Map[25, 22].Type);
			Assert.AreEqual(TileType.Air, boss1._world.Map[boss1.Location.X, boss1.Location.Y + 1].Type);
			Assert.AreEqual(20, boss1.Location.Y);
			Assert.AreEqual(1, boss1.DirectionY);

			//bottom is rock, collision occur, hence direction change, [25,22] is rock
			boss1.Update();
			Assert.AreEqual(21, boss1.Location.Y);
			Assert.AreEqual(2, boss1.DirectionY);
			Assert.AreEqual(TileType.Rock, boss1._world.Map[boss1.Location.X,boss1.Location.Y + 1].Type);

			//since direction change, next update goes opposite direction, [25,21] to [25,20]
			boss1.Update();
			Assert.AreEqual(20, boss1.Location.Y);
			Assert.AreEqual(2, boss1.DirectionY);
		}

		[Test()]
		public void EnemySpawnTest() 
		{
			//enemy initial location X= 25 and Y = 20
			//Method spawn new enemy, checking their location spawned
			Escapade.SpawnMoreEnemy();
			Assert.AreEqual(25, Escapade.SpawnMoreEnemy().Location.X);
			Assert.AreEqual(20, Escapade.SpawnMoreEnemy().Location.Y);
		}

		[Test()]
		public void MineAndBuildRockTest() 
		{
			Location loc = new Location(25, 21);
			Player player1 = new Player(1, "Player 1", loc);

			Location l = new Location(25, 20);
			Enemy boss1 = new Enemy(0, "Boss", l, 0, 1);
			boss1._world = new World(GlobalConstants.WORLD_WIDTH, GlobalConstants.WORLD_HEIGHT, GlobalConstants.SIZE);
			//all tiles are air
			for (int x = 0; x<GlobalConstants.WORLD_WIDTH/GlobalConstants.SIZE; x++)
			{
				for (int y = 0; y<GlobalConstants.WORLD_HEIGHT/GlobalConstants.SIZE; y++)
					boss1._world.Map[x, y] = new Tile(TileType.Air);
			}

			Assert.AreEqual(TileType.Rock, boss1._world.Map[player1.Location.X, player1.Location.Y + 1].Type);

			//build rock bottom, logic used in Main 1 line code below is the method's logic
			boss1._world.Map[player1.Location.X, player1.Location.Y+1] = new Tile(TileType.Rock);
			Assert.AreEqual(TileType.Rock, boss1._world.Map[player1.Location.X, player1.Location.Y + 1].Type);

			//mine rock bottom, hence tile is back to air, logic used in Main 1 line code below is the method's logic
			boss1._world.Map[player1.Location.X, player1.Location.Y+1] = new Tile(TileType.Air);
			Assert.AreEqual(TileType.Air, boss1._world.Map[player1.Location.X, player1.Location.Y + 1].Type);
		}
	}
}
