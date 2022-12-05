// Problem statement: https://adventofcode.com/2022/day/5

var inputString = File.ReadAllText("inout.txt");
Console.WriteLine(SortCrates(OneAtATime, inputString)); // Part 1
Console.WriteLine(SortCrates(InOrder, inputString));    // Part 2

string SortCrates(Action<List<Stack<char>>, List<MoveInstruction>> action, string str)
{
    var lineList = str.Split(Environment.NewLine);
    var separationLine = lineList.Select((s, idx) => new { s, idx })
        .First((e) => string.IsNullOrWhiteSpace(e.s)).idx;

    var stackLines = lineList.Take(separationLine).ToList();
    var moveLines = lineList.Skip(separationLine + 1).ToList();

    var stacks = CreateStacks(stackLines);
    var moves = ParseMoves(moveLines);

    action.Invoke(stacks, moves);
    return ReadTopCrates(stacks);
}

string ReadTopCrates(List<Stack<char>> stacks)
{
    var str = "";
    foreach (var stack in stacks)
    {
        str += stack.Peek();
    }

    return str;
}

void OneAtATime(List<Stack<char>> list, List<MoveInstruction> moveInstructions)
{
    foreach (var move in moveInstructions)
    {
        var fromStack = list[move.From - 1];
        var toStack = list[move.To - 1];

        for (int i = 0; i < move.Count; i++)
        {
            toStack.Push(fromStack.Pop());
        }
    }
}

void InOrder(List<Stack<char>> stacks, List<MoveInstruction> moves)
{
    foreach (var move in moves)
    {
        var fromStack = stacks[move.From - 1];
        var toStack = stacks[move.To - 1];

        var tempStack = new Stack<char>(move.Count);
        for (var i = 0; i < move.Count; i++)
        {
            tempStack.Push(fromStack.Pop());
        }

        for (var i = 0; i < move.Count; i++)
        {
            toStack.Push(tempStack.Pop());
        }
    }
}

List<MoveInstruction> ParseMoves(List<string> moveList)
{
    var moves = new List<MoveInstruction>();
    foreach (var move in moveList)
    {
        var i = 0;
        var j = 0;
        var arr = new int[3];
        while (j < 3)
        {
            if (!char.IsDigit(move[i]))
            {
                i++;
                continue;
            }

            var str = "";
            while (i < move.Length && char.IsDigit(move[i]))
            {
                str += move[i];
                i++;
            }

            arr[j++] = int.Parse(str);
        }

        moves.Add(new MoveInstruction() { Count = arr[0], From = arr[1], To = arr[2] });
    }

    return moves;
}

List<Stack<char>> CreateStacks(List<string> stackInputLines)
{
    var stackCount = stackInputLines[^2].Split(" ").Length;
    var stackList = new List<Stack<char>>(stackCount);
    for (var i = 0; i < stackCount; i++)
    {
        stackList.Add(new Stack<char>());
    }

    for (int i = stackInputLines.Count - 2; i >= 0; i--)
    {
        var inputLine = stackInputLines[i];
        var stack = 0;

        for (var j = 0; j < inputLine.Length; j += 3)
        {
            var token = inputLine.Substring(j, Math.Min(3, inputLine.Length - 3));
            j++;

            if (!string.IsNullOrWhiteSpace(token))
            {
                stackList[stack].Push(token[1]);
            }

            stack++;
        }
    }

    return stackList;
}

struct MoveInstruction
{
    internal int Count, From, To;
}