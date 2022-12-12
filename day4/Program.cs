// See https://aka.ms/new-console-template for more information

var lines = await File.ReadAllLinesAsync("input.txt");

//Part1(lines);
Part2(lines);

static bool fullyContains((int, int) entry1, (int, int) entry2)
{
    return (entry1.Item1 <= entry2.Item1 && entry1.Item2 >= entry2.Item2)
    || (entry2.Item1 <= entry1.Item1 && entry2.Item2 >= entry1.Item2);
}

static bool overlap((int, int) entry1, (int, int) entry2)
{
    return (entry1.Item1 <= entry2.Item2 && entry1.Item1 >= entry2.Item1)
    || (entry2.Item1 <= entry1.Item2 && entry2.Item1 >= entry1.Item1);
}

static void Part1(string[] lines)
{
    var entries = lines
        .Select(line => line.Split(','))
        .Select(line => line.Select(entry => entry.Split('-')))
        .Select(line => line.Select(entry => (int.Parse(entry[0]), int.Parse(entry[1]))))
        .Count(line => fullyContains(line.ElementAt(0), line.ElementAt(1)));

    Console.WriteLine($"total entries: {entries}");
}

static void Part2(string[] lines)
{
    var entries = lines
        .Select(line => line.Split(','))
        .Select(line => line.Select(entry => entry.Split('-')))
        .Select(line => line.Select(entry => (int.Parse(entry[0]), int.Parse(entry[1]))))
        .Count(line => overlap(line.ElementAt(0), line.ElementAt(1)));

    Console.WriteLine($"total entries: {entries}");
}