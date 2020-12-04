using System.Linq;

namespace advent2020
{
	class Day1
	{
		public static void Solve()
		{
			System.Console.WriteLine("Day 1");
			var intList = System.IO.File.ReadAllLines("day1input.txt").Select(int.Parse);
			var part1 = intList.SelectMany(x => intList, (y, z) => new { y, z, s = y * z }).Where(x => x.y + x.z == 2020).First().s;
			System.Console.WriteLine($"Part1: {part1}");
			var part2 = intList.SelectMany(x => intList, (y, z) => new { y, z }).SelectMany(x => intList, (y, z) => new { a = y.y, b = y.z, c = z, s = y.y * y.z * z }).Where(x => x.a + x.b + x.c == 2020).First().s;
			System.Console.WriteLine($"Part2: {part2}");
		}
	}
}
