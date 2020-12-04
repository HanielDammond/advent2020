using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace advent2020
{
	class Day4
	{
		private static readonly Regex EyeColorRegex = new Regex(@"amb|blu|brn|gry|grn|hzl|oth");
		private static readonly Regex HairColorRegex = new Regex(@"hcl:#[0-9a-f]{6}");
		private static readonly Regex PassportRegex = new Regex(@"pid:\d{9}$");
		private static readonly Regex HeightRegex = new Regex(@"hgt:(\d+)(.*)");
		public static void Solve()
		{
			System.Console.WriteLine("Day 4");
			var lines = System.IO.File.ReadAllLines("day4input.txt").ToList();
			var part1 = CheckNext(lines, false);
			System.Console.WriteLine($"Part1: {part1}");
			var part2 = CheckNext(lines, true);
			System.Console.WriteLine($"Part2: {part2}");
		}
		private static long CheckNext(List<string> lines, bool strict)
		{
			if (lines == null || lines.Count < 1)
			{
				return 0;
			}
			var nextEmpty = lines.IndexOf(string.Empty);
			var chunk = nextEmpty == -1 ? lines : lines.GetRange(0, nextEmpty);
			var fields = chunk.SelectMany(x => x.Split(' ')).Where(x => !x.Contains("cid"));
			return (IsValid(fields, strict) ? 1 : 0) + (nextEmpty == -1 ? CheckNext(null, strict) : CheckNext(lines.GetRange(nextEmpty + 1, lines.Count - nextEmpty - 1), strict));
		}
		private static bool IsValid(IEnumerable<string> fields, bool strict)
		{
			var numFields = fields.Count();
			if (numFields != 7)
			{
				return false;
			}
			return !strict || HasValidByr(fields) &&
				HasValidIyr(fields) &&
				HasValidEyr(fields) &&
				HasValidHgt(fields) &&
				HasValidHcl(fields) &&
				HasValidEcl(fields) &&
				HasValidPid(fields);
		}
		private static bool HasValidByr(IEnumerable<string> fields)
		{
			int.TryParse(fields.FirstOrDefault(x => x.Contains("byr"))?.Replace("byr:", ""), out var byrInt);
			return byrInt >= 1920 && byrInt <= 2002;
		}
		private static bool HasValidIyr(IEnumerable<string> fields)
		{
			int.TryParse(fields.FirstOrDefault(x => x.Contains("iyr"))?.Replace("iyr:", ""), out var iyrInt);
			return iyrInt >= 2010 && iyrInt <= 2020;
		}
		private static bool HasValidEyr(IEnumerable<string> fields)
		{
			int.TryParse(fields.FirstOrDefault(x => x.Contains("eyr"))?.Replace("eyr:", ""), out var eyrInt);
			return eyrInt >= 2020 && eyrInt <= 2030;
		}
		private static bool HasValidHgt(IEnumerable<string> fields)
		{
			var field = fields.FirstOrDefault(x => x.Contains("hgt"));
			if (field == null)
			{
				return false;
			}
			var matches = HeightRegex.Match(field);
			return (matches.Groups[2].Value == "cm" && int.Parse(matches.Groups[1].Value) >= 150 && int.Parse(matches.Groups[1].Value) <= 193) ||
				(matches.Groups[2].Value == "in" && int.Parse(matches.Groups[1].Value) >= 59 && int.Parse(matches.Groups[1].Value) <= 76);
		}
		private static bool HasValidHcl(IEnumerable<string> fields) => fields.Any(x => HairColorRegex.IsMatch(x));
		private static bool HasValidEcl(IEnumerable<string> fields) => fields.Any(x => EyeColorRegex.IsMatch(x));
		private static bool HasValidPid(IEnumerable<string> fields) => fields.Any(x => PassportRegex.IsMatch(x));
	}
}
