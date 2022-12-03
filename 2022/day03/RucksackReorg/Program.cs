//  Problem statement: https://adventofcode.com/2022/day/3

var inputString = File.ReadAllText("input.txt");
Console.WriteLine(CommonItemsPrioritySum(inputString));
Console.WriteLine(BadgePrioritySum(inputString));

int CommonItemsPrioritySum(string str) // Part 1
{
    return str.Split(Environment.NewLine)
        .Select(l => new[] { l[..(l.Length / 2)], l[(l.Length / 2)..] })
        .Select(arrs => arrs[0].Intersect(arrs[1]))
        .Select(cArr => cArr.First())
        .Sum(c => char.IsUpper(c) ? c - 38 : c - 96);
}

int BadgePrioritySum(string str) // Part 2
{
    return str.Split(Environment.NewLine)
        .Select((s, i) => new { Index = i, Value = s })
        .GroupBy(e => e.Index / 3)
        .Select(g => g.Select((i, v) => i.Value).ToList())
        .Select(l => l.Select(s => s.ToCharArray())
            .Aggregate((s1, s2) => s1.Intersect(s2).ToArray()))
        .Select(arr => arr.First())
        .Sum(c => char.IsUpper(c) ? c - 38 : c - 96);
}