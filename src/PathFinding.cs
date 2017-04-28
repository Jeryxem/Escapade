using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escapade
{
	public class AStarPathfinder
	{
		public List<Coordinate> GetPath(Coordinate s, Coordinate t)
		{
			//open nodes for consideration
			List<PathLocation> open = new List<PathLocation>();
			//closed nodes that have already beeen considered
			List<PathLocation> closed = new List<PathLocation>();

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
				current = open[minF];


			}

			return null;
		}
	}

	public class PathLocation
	{
		public Coordinate C { get; set; }
		public Coordinate S { get; set; }
		public Coordinate T { get; set; }
		public double F { get; set; }
		public double G { get; set; }
		public double H { get; set; }

		public PathLocation(Coordinate c, Coordinate s, Coordinate t)
		{
			C = c;
			S = s;
			T = t;
			G = GetDistance(S, C);
			H = GetDistance(T, C);
			F = G + H;
		}

		public PathLocation(int x, int y, int x2, int y2, int x3, int y3) : this(new Coordinate(x, y), new Coordinate(x2, y2), new Coordinate(x3, y3)) { }

		public double GetDistance(Coordinate c1, Coordinate c2)
		{
			return Math.Sqrt(Math.Pow(Math.Abs(c1.X - c2.X), 2) + Math.Pow(Math.Abs(c1.Y - c2.Y), 2));
		}
	}
}
