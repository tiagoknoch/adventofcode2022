// See https://aka.ms/new-console-template for more information

var lines = await File.ReadAllLinesAsync("input.txt");
var stacks = GetLists(lines.TakeWhile(line => !string.IsNullOrEmpty(line.Trim())));

var orders = lines.SkipWhile(line => !string.IsNullOrEmpty(line.Trim())).Skip(1);
processLists(orders, stacks);

//get the top crates
var resultString = string.Concat(stacks.Select(stack => stack.Last()));

Console.WriteLine($"result is {resultString}");

static void processLists(IEnumerable<string> orders, List<List<string>> stacks)
{
    foreach (var order in orders)
    {
        var orderSplit = order.Split(' ');
        var nrCrates = int.Parse(orderSplit[1]);
        var fromStack = int.Parse(orderSplit[3]);
        var toStack = int.Parse(orderSplit[5]);

        var items = stacks[fromStack - 1].TakeLast(nrCrates).ToList();
        stacks[fromStack - 1].RemoveRange(stacks[fromStack - 1].Count - nrCrates, nrCrates);
        stacks[toStack - 1].AddRange(items);
    }
}

static void Part1(string[] lines)
{
    var stacks = GetStacks(lines.TakeWhile(line => !string.IsNullOrEmpty(line.Trim())));

    var orders = lines.SkipWhile(line => !string.IsNullOrEmpty(line.Trim())).Skip(1);
    processStacks1(orders, stacks);

    //get the top crates
    var resultString = string.Concat(stacks.Select(stack => stack.Pop()));

    Console.WriteLine($"result is {resultString}");
}

static void processStacks1(IEnumerable<string> orders, List<Stack<string>> stacks)
{
    foreach (var order in orders)
    {
        var orderSplit = order.Split(' ');
        var nrCrates = int.Parse(orderSplit[1]);
        var fromStack = int.Parse(orderSplit[3]);
        var toStack = int.Parse(orderSplit[5]);

        foreach (var i in Enumerable.Range(1, nrCrates))
        {
            var item = stacks[fromStack - 1].Pop();
            stacks[toStack - 1].Push(item);
        }
    }
}

static List<Stack<string>> GetStacks(IEnumerable<string> lines)
{
    var result = new List<Stack<string>>();
    foreach (var line in lines)
    {
        var items = line.Split(' ');
        result.Add(new Stack<string>(items.Reverse()));
    }

    return result;
}

static List<List<string>> GetLists(IEnumerable<string> lines)
{
    var result = new List<List<string>>();
    foreach (var line in lines)
    {
        var items = line.Split(' ');
        result.Add(new List<string>(items.Reverse()));
    }

    return result;
}