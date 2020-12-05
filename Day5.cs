using System;
using System.Linq;

namespace advent2020
{
	class Day5
	{
		public static void Solve()
		{
			Console.WriteLine("Day 5");
			var lines = System.IO.File.ReadAllLines("day5input.txt");
			var seatIds = lines.Select(x => 8 * Convert.ToInt32(x.Substring(0, 7).Replace("F", "0").Replace("B", "1"), 2) + Convert.ToInt32(x[7..].Replace("L", "0").Replace("R", "1"), 2));
			var part1 = seatIds.Max();
			Console.WriteLine($"Part1: {part1}");
			var min = seatIds.Min();
			var max = seatIds.Max();
			var part2 = (max - min + 1)*(max + min)/2 - seatIds.Sum();
			Console.WriteLine($"Part2: {part2}");
		}
	}
}
