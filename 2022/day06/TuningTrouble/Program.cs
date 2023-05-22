// Problem statement: https://adventofcode.com/2022/day/6


var text = File.ReadAllText("input.txt");
var markerLen = 14;
Console.WriteLine(FindMarker(text, markerLen));
Console.WriteLine(FindMarker2(text, markerLen));


int FindMarker2(string str, int markerLen)
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


int FindMarker(string str, int markerLen)
{
    var charMap = new Dictionary<char, int>();
    for (var i = 0; i < markerLen; i++)
    {
        var ch = str[i];
        if (charMap.ContainsKey(ch))
        {
            charMap[ch]++;
        }
        else
        {
            charMap[ch] = 1;
        }
    }
    
    for(var i = markerLen; i < str.Length; i++)
    {

        var diffChars = 0;
        if (charMap.Count == markerLen)
        {
            foreach (var keyValuePair in charMap)
            {
                var res = keyValuePair.Value == 1 ? 1 : 0;
                diffChars += res;
            }
        }
        
        
        if (diffChars == markerLen)
        {
            return i;
        }

        var ch = str[i];
        if(charMap.ContainsKey(ch))
        {
            charMap[ch]++;
        }
        else
        {
            charMap[ch] = 1;
        }

        var chToRemove = str[i - markerLen];
        if (charMap[chToRemove] > 1)
        {
            charMap[chToRemove]--;
        }
        else
        {
            charMap.Remove(chToRemove);
        }

    }
    throw new Exception();
}