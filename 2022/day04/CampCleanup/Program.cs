// Problem statement: https://adventofcode.com/2022/day/4

var inputString = File.ReadAllText("input.txt");

Console.WriteLine(ContainedAssignmentPairCount(inputString));
Console.WriteLine(OverlapAssignmentPairCount(inputString));

int ContainedAssignmentPairCount(string str) => MatchingPairCount(str, Pair.EitherContains);
int OverlapAssignmentPairCount(string str) => MatchingPairCount(str, Pair.Overlap);

int MatchingPairCount(string str, Func<Pair, Pair, bool> fn)
{
    return str
        .Split(Environment.NewLine)
        .Select(l => l.Split(",")
            .Select(e => e.Split("-").Select(int.Parse)).Aggregate((a,b ) => a.Concat(b)).ToArray())
        .Count(arr => fn.Invoke(new Pair(arr[0], arr[1]), new Pair(arr[2], arr[3])));
}


readonly struct Pair
{
    private readonly int _start;
    private readonly int _end;

    public Pair(int start, int end)
    {
        _start = start;
        _end = end;
    }

    internal static bool Overlap(Pair thisPair, Pair p2)
    {
        if (p2._start > thisPair._end)
            return false;

        if (thisPair._start > p2._end)
            return false;

        if (thisPair._start <= p2._start && thisPair._end >= p2._start)
            return true;

        if (p2._start <= thisPair._start && p2._end >= thisPair._start)
            return true;

        throw new Exception();
    }

    internal static bool EitherContains(Pair p1, Pair p2)
    {
        return p2._start <= p1._start ? Contains(p2, p1) : Contains(p1, p2);
    }

    private static bool Contains(Pair p1, Pair p2)
    {
        if (p1._start == p2._start)
            return true;

        return p1._end >= p2._end;
    }
}