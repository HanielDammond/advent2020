using System.Linq;

namespace advent2020
{
	class Day3
	{
		public static void Solve()
		{
			System.Console.WriteLine("Day 3");
			var coords = System.IO.File.ReadAllLines("day3input.txt").Select(x => x.Select(y => y == '#' ? 1 : 0).ToArray());
			var width = coords.First().Count();
			var part1 = coords.Select((x, i) => x[3 * i % width]).Sum();
			System.Console.WriteLine($"Part1: {part1}");
			var part2 = coords.Select((x, i) => new Vector5(x[i % width], x[3 * i % width], x[5 * i % width], x[7 * i % width], i % 2 == 0 ? x[i / 2 % width] : 0))
				.Aggregate(new Vector5(), (x, y) => x + y, x => x.Product);
			System.Console.WriteLine($"Part2: {part2}");
		}
	}

	class Vector5
	{
		private readonly long[] data;
		public Vector5()
		{
			data = new long[] { 0, 0, 0, 0, 0 };
		}
		public Vector5(long a, long b, long c, long d, long e)
		{
			data = new[] { a, b, c, d, e };
		}
		public long this[int key]
		{
			get => data[key];
		}
		public long Product => data[0] * data[1] * data[2] * data[3] * data[4];
		public static Vector5 operator +(Vector5 l, Vector5 r) => new Vector5(l[0] + r[0], l[1] + r[1], l[2] + r[2], l[3] + r[3], l[4] + r[4]);
	}
}
