//  Problem statement: https://adventofcode.com/2022/day/1

var inputString = File.ReadAllText("input.txt");
Console.WriteLine(TotalCalories(inputString));
Console.WriteLine(Top3CaloriesSum(inputString));

int TotalCalories(string str)	// Part 1
{
    return str.Split(Environment.NewLine)
        .Aggregate(new List<int> { 0 }, (list, s) =>
        {
            if (s == string.Empty)
            {
                list.Add(0);
            }
            else
            {
                list[^1] += int.Parse(s);
            }

            return list;
        })
        .Max();
}

int Top3CaloriesSum(string str)	// Part 2  
{
    return str.Split(Environment.NewLine)
        .Aggregate(new List<int> { 0 }, (list, s) =>
        {
            if (s == string.Empty)
            {
                list.Add(0);
            }
            else
            {
                list[^1] += int.Parse(s);
            }

            return list;
        })
        .OrderBy(i => i)
        .TakeLast(3)
        .Sum();
}