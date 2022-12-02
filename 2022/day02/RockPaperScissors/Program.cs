//  Problem statement: https://adventofcode.com/2022/day/2

var inputString = File.ReadAllText("example.txt");
Console.WriteLine(StrategyGuide1(inputString));
Console.WriteLine(StrategyGuide2(inputString));

int StrategyGuide1(string str)  // Part 1
{
    return str.Split(Environment.NewLine)
        .Select(r => r.Split(' ').Select(s => s[0]).ToArray())
        .Sum(c =>(c[0] == c[1] - 23 ? 3 : (c[1] - 23 - c[0] + 3) %3 == 1 ? 6 : 0) + (c[1] - 87));
}

int StrategyGuide2(string str)  // Part 2
{
    return str.Split(Environment.NewLine)
        .Select(r => r.Split(' ').Select(s => s[0]).ToArray())
        .Sum(c => c[1] == 'Y' ? c[0] - 64 + 3
            : c[1] == 'Z' ? 6 + ((c[0] - 64 + 1) % 3 > 0 ? (c[0] - 64 + 1) % 3 : 3)
            : (c[0] - 64 + 2) % 3 > 0 ? (c[0] - 64 + 2) % 3 : 3);
}