// See https://aka.ms/new-console-template for more information
var entries = await File.ReadAllLinesAsync("input.txt");

//Part1(entries);
//Part2(entries);
void Part2(string[] entries)
{
    var totalValue = Split(entries, 3)
    .Select(group => findTheCommonItem3(group.ElementAt(0), group.ElementAt(1), group.ElementAt(2)))
    .Select(commonItem => convert(commonItem))
    .Sum();

    Console.WriteLine($"Total value is {totalValue}");
}

void Part1(string[] entries)
{
    var query = entries.Select(entry => (entry[..(entry.Length / 2)], entry.Substring(entry.Length / 2, entry.Length / 2)));

    //there should only be one
    var totalValue = query
        .Select(entry => getRealValue(entry))
        .Sum();

    Console.WriteLine($"Total value is {totalValue}");
}

char findTheCommonItem(string item1, string item2)
{
    foreach (var item in item1)
    {
        var index = item2.IndexOf(item);
        if (index > -1)
        {
            return item;
        }
    }

    throw new InvalidOperationException("could not find common item");
}

char findTheCommonItem3(string item1, string item2, string item3)
{
    foreach (var item in item1)
    {
        var index = item2.IndexOf(item);
        var index2 = item3.IndexOf(item);
        if (index > -1 && index2 > -1)
        {
            return item;
        }
    }

    throw new InvalidOperationException("could not find common item");
}

int getRealValue((string, string) entry)
{
    var commonItem = findTheCommonItem(entry.Item1, entry.Item2);
    return convert(commonItem);
}

int convert(char commonItem)
{
    return char.IsUpper(commonItem)
    ? commonItem - 38
    : commonItem - 96;
}
IEnumerable<IEnumerable<T>> Split<T>(T[] array, int size)
{
    for (var i = 0; i < (float)array.Length / size; i++)
    {
        yield return array.Skip(i * size).Take(size);
    }
}