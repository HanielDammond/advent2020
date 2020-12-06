using System.Collections.Generic;
using System.Linq;

namespace advent2020
{
	class Day6
	{
		public static void Solve()
		{
			System.Console.WriteLine("Day 6");
			var lines = System.IO.File.ReadAllLines("day6input.txt").ToList();
			var part1 = CheckNext(lines, false);
			System.Console.WriteLine($"Part1: {part1}");
			var part2 = CheckNext(lines, true);
			System.Console.WriteLine($"Part2: {part2}");
		}
		
		private static long CheckNext(List<string> lines, bool all)
		{
			if (lines == null || lines.Count < 1)
			{
				return 0;
			}
			var nextEmpty = lines.IndexOf(string.Empty);
			var chunk = nextEmpty == -1 ? lines : lines.GetRange(0, nextEmpty);
			long numAnswers = 0;
			if (all)
			{
				var numPeople = chunk.Count();
				numAnswers = chunk.SelectMany(x => x.ToCharArray()).GroupBy(x => x).Count(x => x.Count() == numPeople);
			}
			else
			{
				numAnswers = chunk.SelectMany(x => x.ToCharArray()).Distinct().Count();
			}
			return numAnswers + (nextEmpty == -1 ? CheckNext(null, all) : CheckNext(lines.GetRange(nextEmpty + 1, lines.Count - nextEmpty - 1), all));
		}
	}
}
