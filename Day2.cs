using System.Linq;
using System.Text.RegularExpressions;

namespace advent2020
{
	class Day2
	{
		public static void Solve()
		{
			System.Console.WriteLine("Day 2");
			var regex = @"(\d+)-(\d+) (\w): (.*)";
			var ruleList = System.IO.File.ReadAllLines("day2input.txt").Select(x => { var m = Regex.Match(x, regex).Groups; return new { min = int.Parse(m[1].Value), max = int.Parse(m[2].Value), letter = m[3].Value[0], pass = m[4].Value }; });
			var part1 = ruleList.Count(x => { var c = x.pass.Count(y => y == x.letter); return c >= x.min && c <= x.max; });
			System.Console.WriteLine($"Part1: {part1}");
			var part2 = ruleList.Count(x => x.pass[x.min - 1] == x.letter ^ x.pass[x.max - 1] == x.letter);
			System.Console.WriteLine($"Part2: {part2}");
		}
	}
}
