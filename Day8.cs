using System.Linq;

namespace advent2020
{
	class Day8
	{
		public static void Solve()
		{
			System.Console.WriteLine("Day 8");
			var lines = System.IO.File.ReadAllLines("day8input.txt");
			var instructions = lines.Select(x => new Node(x)).ToArray();
			var part1 = Part1(instructions);
			System.Console.WriteLine($"Part1: {part1}");
			foreach (var node in instructions)
			{
				node.Visited = false;
			}
			var part2 = Part2(instructions);
			System.Console.WriteLine($"Part2: {part2}");
		}

		private static int Part1(Node[] nodes)
		{
			var index = 0;
			var accumulator = 0;
			var run = true;
			while (run)
			{
				var node = nodes[index];
				if (node.Visited)
				{
					run = false;
				}
				else
				{
					node.Visited = true;
					switch (node.Instruction)
					{
						case "acc":
							accumulator += node.Value;
							index++;
							break;
						case "jmp":
							index += node.Value;
							break;
						case "nop":
							index++;
							break;
					}
				}
			}
			return accumulator;
		}

		private static int Part2(Node[] nodes)
		{
			var index = 0;
			var accumulator = 0;
			var terminateIndex = nodes.Count();
			var run = true;
			while (run)
			{
				var node = nodes[index];
				node.Visited = node.Visited2 = true;
				int accOut;
				switch (node.Instruction)
				{
					case "acc":
						accumulator += node.Value;
						index++;
						break;
					case "jmp":
						if (Terminates(nodes, index + 1, terminateIndex, accumulator, out accOut))
						{
							return accOut;
						}
						Reset(nodes);
						index += node.Value;
						break;
					case "nop":
						if (Terminates(nodes, index + node.Value, terminateIndex, accumulator, out accOut))
						{
							return accOut;
						}
						Reset(nodes);
						index++;
						break;
				}
			}
			return accumulator;
		}

		private static void Reset(Node[] nodes)
		{
			foreach (var node in nodes)
			{
				node.Visited2 = node.Visited;
			}
		}

		private static bool Terminates(Node[] nodes, int index, int terminateIndex, int currentAccumulator, out int accumulator)
		{
			accumulator = currentAccumulator;
			var run = true;
			while (run)
			{
				if (index == terminateIndex)
				{
					return true;
				}
				var node = nodes[index];
				if (node.Visited2)
				{
					return false;
				}
				node.Visited2 = true;
				switch (node.Instruction)
				{
					case "acc":
						accumulator += node.Value;
						index++;
						break;
					case "jmp":
						index += node.Value;
						break;
					case "nop":
						index++;
						break;
				}
			}
			return false;
		}
	}

	class Node
	{
		public string Instruction { get; }
		public int Value { get; }
		public bool Visited { get; set; }
		public bool Visited2 { get; set; }
		public Node(string line)
		{
			var parts = line.Split();
			Instruction = parts[0];
			Value = int.Parse(parts[1]);
			Visited = false;
			Visited2 = false;
		}
	}
}
