using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace advent2020
{
	class Day7
	{
		private static readonly Regex RuleRegex = new Regex(@"(\w+ \w+) bags contain ((?:\d+ \w+ \w+ bags?,?\s?)+|no other bags)");
		private static readonly Regex ContainedBagRegex = new Regex(@"(\d+) (.*) bags?");
		public static void Solve()
		{
			System.Console.WriteLine("Day 7");
			var lines = System.IO.File.ReadAllLines("day7input.txt");
			var rules = lines.Select(x => RuleRegex.Match(x)).SelectMany(x => x.Groups[2].Value.Split(", ").Select(y => {
				var groups = ContainedBagRegex.Match(y).Groups; 
				return new KeyValuePair<string, Bag>(x.Groups[1].Value, groups.Count > 1 ? new Bag(int.Parse(groups[1].Value), groups[2].Value) : null);
			}));
			var outerDic = rules.GroupBy(x => x.Value?.Color ?? "").ToDictionary(x => x.Key, x => x.Select(y => y.Key));
			var part1 = Outer("shiny gold", outerDic).Count();
			System.Console.WriteLine($"Part1: {part1}");
			var innerDic = rules.GroupBy(x => x.Key).ToDictionary(x => x.Key, x => x.Select(y => y.Value));
			var part2 = Inner("shiny gold", innerDic) - 1;
			System.Console.WriteLine($"Part2: {part2}");
		}

		private static IEnumerable<string> Outer(string key, Dictionary<string, IEnumerable<string>> dic)
		{
			if (!dic.ContainsKey(key))
			{
				return new List<string> { key };
			} 
			var parents = dic[key];
			return parents.Union(parents.SelectMany(x => Outer(x, dic)));
		}

		private static long Inner(string key, Dictionary<string, IEnumerable<Bag>> dic)
		{
			if (!dic.ContainsKey(key))
			{
				return 0;
			}
			var node = dic[key];
			return 1 + node.Sum(x => x == null ? 0 : x.Quantity * Inner(x.Color, dic));
		}
	}

	class Bag
	{
		public Bag(int quantity, string color)
		{
			Quantity = quantity;
			Color = color;
		}
		public int Quantity { get; set; }
		public string Color { get; set; }
	}
}
