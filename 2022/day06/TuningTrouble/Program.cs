// Problem statement: https://adventofcode.com/2022/day/6


var text = File.ReadAllText("input.txt");
var markerLen = 14;
Console.WriteLine(FindMarker(text, markerLen));


int FindMarker(string str, int markerLen)
{
    var charMap = new Dictionary<char, int>(markerLen);
    for (var i = 0; i < str.Length; i++)
    {
        if (charMap.Count == markerLen)
        {
            var uniqueCount = charMap.Values.Count(c => c == 1);
            if (uniqueCount == markerLen)
                return i;
        }

        var ch = str[i];
        if (charMap.ContainsKey(ch))
            charMap[ch]++;
        else
            charMap[ch] = 1;
        
        if(i < markerLen)
            continue;

        var toDecrement = str[i - markerLen];
        var count = --charMap[toDecrement];
        if (count == 0)
            charMap.Remove(toDecrement);
    }

    throw new Exception("Whoops!");
}