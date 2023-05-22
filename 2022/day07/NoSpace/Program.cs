// Problem statement: https://adventofcode.com/2022/day/7

var inputString = File.ReadAllText("input.txt");
var dirMap = CalculateAllDirectorySizes(inputString);
var maxSize = 100_000;

var criteriaSum = dirMap.Values.Select(d => d.TotalSize)
    .Where(s => s < maxSize)
    .Sum();

Console.WriteLine($"Sum of files < than {maxSize} = {criteriaSum}");

var spaceRequired = 30000000;
var spaceRemaining = 70000000 - dirMap["/"].TotalSize;

var neededToClear = spaceRequired - spaceRemaining;

var sizeToDelete = dirMap.Values
    .Select(d => d.TotalSize)
    .Where(s => s > neededToClear)
    .Min();
Console.WriteLine($"Folder of size {sizeToDelete} should be deleted");

IDictionary<string, Dir> CalculateAllDirectorySizes(string str)
{
    var dirMap = new Dictionary<string, Dir>();
    var lines = str.Split(Environment.NewLine);
    var currentLine = 0;
    var rootDirName = GetDirName(lines[currentLine++]);

    var currentDir = new Dir
    {
        Name = rootDirName
    };

    for (; currentLine < lines.Length; currentLine++)
    {
        var args = lines[currentLine].Split(" ");
        if (IsLs(args))
        {
            while (currentLine + 1 < lines.Length && !lines[currentLine + 1].StartsWith("$"))
            {
                if (currentDir.HasLs)
                    continue;

                var fileEntry = lines[++currentLine].Split(" ");
                if (IsDir(fileEntry))
                    continue;

                currentDir.TotalFileSize += GetFileSize(fileEntry);
            }

            currentDir.HasLs = true;
        }

        else if (IsCd(args))
        {
            var cdPath = GetCdPath(args);
            if (cdPath == "..")
            {
                dirMap[currentDir.FullName] = currentDir;
                currentDir = currentDir.parentDir;
            }
            else
            {
                dirMap[currentDir.FullName] = currentDir;
                var childDir = new Dir
                {
                    Name = cdPath,
                    parentDir = currentDir
                };
                currentDir.ChildDirs.Add(childDir);
                currentDir = childDir;
            }
        }

        else
            throw new Exception("Unknown command!");
    }

    dirMap[currentDir.Name] = currentDir;
    return dirMap;
}

bool IsLs(string[] args) => args[1] == "ls";

bool IsCd(string[] args) => args[1] == "cd";

bool IsDir(string[] args) => args[0] == "dir";

int GetFileSize(string[] args) => int.Parse(args[0]);

string GetFileName(string[] args) => args[1];

string GetCdPath(string[] args) => args[2];

string GetDirName(string line)
{
    return line.Split(" ")[2];
}

class Dir
{
    public string FullName
    {
        get
        {
            if (parentDir is null)
            {
                return "/";
            }

            if (parentDir.FullName == "/")
            {
                return parentDir.FullName + Name;
            }

            return parentDir.FullName + "/" + Name;
        }
    }

    public Dir parentDir;
    public string Name;
    public long TotalFileSize;
    public readonly List<Dir> ChildDirs = new();
    public bool HasLs;

    public long TotalSize
    {
        get
        {
            var totalSize = TotalFileSize;
            foreach (var dir in ChildDirs)
            {
                totalSize += dir.TotalSize;
            }

            return totalSize;
        }
    }
}