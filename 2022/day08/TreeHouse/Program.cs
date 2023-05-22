// Problem statement: https://adventofcode.com/2022/day/8

var str = File.ReadAllText("input.txt");
var grid = CreateGrid(str);

Console.WriteLine($"Visible tree count is: {VisibleTreeCount(grid)}");
Console.WriteLine($"Maximum scenic score is: {MaxScenicScore(grid)}");


int[][] CreateGrid(string inputString)
{
    var rows = inputString.Split(Environment.NewLine);
    var grid = new int[rows.Length][];
    var rowNo = 0;

    foreach (var row in rows)
    {
        var gridRow = new int[row.Length];
        var i = 0;
        foreach (var c in row)
        {
            gridRow[i++] = int.Parse(c + "");
        }

        grid[rowNo++] = gridRow;
    }

    return grid;
}

int MaxScenicScore(int[][] grid)
{
    var maxScore = 0;
    for (var i = 1; i < grid.Length - 1; i++)
    {
        for (var j = 1; j < grid.Length - 1; j++)
        {
            var score = ScenicScore(i, j, grid);
            if (score > maxScore)
                maxScore = score;
        }
    }

    return maxScore;
}

int ScenicScore(int x, int y, int[][] grid)
{
    var value = grid[x][y];
    var leftDist = 0;
    var rightDist = 0;
    var topDist = 0;
    var bottomDist = 0;

    // Left
    for (var i = y - 1; i >= 0; i--)
    {
        if (grid[x][i] < value)
            leftDist++;

        else if (grid[x][i] >= value)
        {
            leftDist++;
            break;
        }
    }

    // Right
    for (var i = y + 1; i < grid[x].Length; i++)
    {
        if (grid[x][i] < value)
            rightDist++;

        else if (grid[x][i] >= value)
        {
            rightDist++;
            break;
        }
    }

    // Top
    for (var i = x - 1; i >= 0; i--)
    {
        if (grid[i][y] < value)
            topDist++;

        else if (grid[i][y] >= value)
        {
            topDist++;
            break;
        }
    }

    // Bottom
    for (var i = x + 1; i < grid.Length; i++)
    {
        if (grid[i][y] < value)
            bottomDist++;

        else if (grid[i][y] >= value)
        {
            bottomDist++;
            break;
        }
    }

    return leftDist * rightDist * bottomDist * topDist;
}

int VisibleTreeCount(int[][] grid)
{
    var visibleCount = 0;
    for (var i = 1; i < grid.Length - 1; i++)
    {
        for (var j = 1; j < grid.Length - 1; j++)
        {
            var isVisible = IsVisible(i, j, grid);
            if (isVisible)
                ++visibleCount;
        }
    }

    visibleCount += 4 * (grid.Length - 1);
    return visibleCount;
}

bool IsVisible(int x, int y, int[][] grid)
{
    var value = grid[x][y];
    bool visR = true, visL = true, visT = true, visB = true;

    // Check Left
    for (var i = 0; i < y && visL; i++)
    {
        if (grid[x][i] >= value)
            visL = false;
    }

    // Check Right
    for (var i = y + 1; i < grid[x].Length && visR; i++)
    {
        if (grid[x][i] >= value)
            visR = false;
    }

    // Check Top
    for (var i = 0; i < x && visT; i++)
    {
        if (grid[i][y] >= value)
            visT = false;
    }

    // Check Bottom
    for (var i = x + 1; i < grid.Length && visB; i++)
    {
        if (grid[i][y] >= value)
            visB = false;
    }

    return visB || visR || visT || visL;
}