using System;
using System.Linq;

namespace advent2020
{
	class Day3
	{
		public static void Solve()
		{
			Console.WriteLine("Day 3");
			var coords = System.IO.File.ReadAllLines(@"day3input.txt").Select(x => x.Select(y => y == '#').ToArray());
			var width = coords.First().Count();
			var part1 = coords.Select((x, i) => x[3 * i % width]).Count(x => x);
			Console.WriteLine($"Part1: {part1}");
			var part2 = coords.Select((x, i) => new[] { Convert.ToInt32(x[i % width]), Convert.ToInt32(x[3 * i % width]), Convert.ToInt32(x[5 * i % width]), Convert.ToInt32(x[7 * i % width]), i % 2 == 0 ? Convert.ToInt32(x[i /2 % width]) : 0 })
				.Aggregate(new[] { 0, 0, 0, 0, 0 }, (x, y) => new[] { x[0] + y[0], x[1] + y[1], x[2] + y[2], x[3] + y[3], x[4] + y[4] }, r => r.Aggregate(1, (x, y) => x * y));
			Console.WriteLine($"Part2: {part2}");
		}
	}
}
