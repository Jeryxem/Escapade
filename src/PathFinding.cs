using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace Escapade
{
	public class AStarPathfinder
	{
		private Map Map { get; set; }

		public AStarPathfinder(Map m)
		{
			Map = m;
		}

		// Pick a start node
		// While !found
			// Get neighbours of current
				// Add to Open and get score if they can be considered
			// Current = lowest F Score
		// Stuff to retrace steps




		public List<Coordinate> GetPath(Coordinate s, Coordinate t)
		{
			//open nodes for consideration
			List<PathLocation> open = new List<PathLocation>();
			//closed nodes that have already beeen considered
			List<PathLocation> closed = new List<PathLocation>();

			//the path from the target location to the start
			List<Coordinate> foundPath = new List<Coordinate>();

			//the start PathLocation
			PathLocation start = new PathLocation(s, s, t);
			//the end PathLocation
			PathLocation target = new PathLocation(t, s, t);

			//the current PathLocation being considered
			PathLocation current = new PathLocation(s, s, t);

			//add the starting point to the open list
			open.Add(start);

			bool found = false;
			//while a path isn't found
			while(!found)
			{
				//get location with lowest F cost in the open list
				int minF = 0;
				foreach (PathLocation p in open)
					if (open[minF].F > p.F)
						minF = open.IndexOf(p);
				//add set the current location as this PathLocation
				current = open[minF];

				//add the neighbours of this current location to the open list if they are valid
				#region AddNeighbours
				//loop the locations adjacent to this PathLocation
				for (int x = -1; x <= 1; x++)
				{
					for (int y = -1; y <= 1; y++)
					{
						//new coordinates for the location to check
						int newX = current.X + x;
						int newY = current.Y + y;

						Console.WriteLine("New Coord: " + current.X + "," + current.Y);
						SwinGame.FillCircle(Color.Black, current.X, current.Y, 2);

						//new PathLocation for the location we are checking
						PathLocation loc = new PathLocation(newX, newY, s, t);

						//if we are checking the current location, if the tile isn't walkable
						//or the location is in the closed list, ignore this iteration
						if ((x == 0 && y == 0) || (Map.Grid[newX, newY] != Tile.AIR) || (closed.Contains(loc)))
							continue;

						//if it isn't in the open list already
						if(!open.Contains(loc))
						{
							//add to open list
							open.Add(loc);
							//set the parent of the new location to the previous location
							loc.Parent = current;
						}
					}
				}
				#endregion AddNeighbours

				//we've added the target PathLocation to the open list, we've found our target
				if (open.Contains(target))
					found = true;
			}

			//add correct path from start to target, looping back through each parent
			#region CreatePath

			PathLocation pl = target;

			while (pl.X != start.X && pl.Y != start.Y)
			{
				foundPath.Add(new Coordinate(pl.X, pl.Y));
				pl = pl.Parent;
			}

			#endregion CreatePath

			return foundPath;
		}
	}

	public class PathLocation : Coordinate
	{
		public Coordinate S { get; set; }
		public Coordinate T { get; set; }
		public PathLocation Parent { get; set; }
		public double F { get; set; }
		public double G { get; set; }
		public double H { get; set; }

		public PathLocation(Coordinate c, Coordinate s, Coordinate t) : base(c.X, c.Y)
		{
			S = s;
			T = t;
			G = GetDistance(S, c);
			H = GetDistance(T, c);
			F = G + H;
		}

		public PathLocation(int x, int y, int x2, int y2, int x3, int y3) : this(new Coordinate(x, y), new Coordinate(x2, y2), new Coordinate(x3, y3)) { }

		public PathLocation(int x, int y, Coordinate s, Coordinate t) : this(new Coordinate(x, y), s, t) { }

		public double GetDistance(Coordinate c1, Coordinate c2)
		{
			return Math.Sqrt(Math.Pow(Math.Abs(c1.X - c2.X), 2) + Math.Pow(Math.Abs(c1.Y - c2.Y), 2));
		}
	}
}
