using Extensions;

namespace Day9;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var input = await File.ReadAllLinesAsync("demo2.txt");

        //Part1(input);
        Part2(input);
    }

    private static void Part1(string[] input)
    {
        var head = new Head();
        var tail = new Tails(1);
        head.MoveEvent += tail.OnHeadMove;

        foreach (var item in input)
        {
            var commands = item.Split(' ');
            Console.WriteLine($"Command {item}");
            head.Move(commands[0].ConvertToDirections(), int.Parse(commands[1]));
        }

        var tailsVisitedCoordinates = tail.VisitedCoordinates.Count;
        Console.WriteLine($"Tails coordinates visited: {tailsVisitedCoordinates}");
    }

    private static void Part2(string[] input)
    {
        var head = new Head();
        var tail1 = new Tails(1);
        head.MoveEvent += tail1.OnHeadMove;

        var tail2 = new Tails(2);
        tail1.MoveEvent += tail2.OnHeadMove;

        var tail3 = new Tails(3);
        tail2.MoveEvent += tail3.OnHeadMove;

        var tail4 = new Tails(4);
        tail3.MoveEvent += tail4.OnHeadMove;

        var tail5 = new Tails(5);
        tail4.MoveEvent += tail5.OnHeadMove;

        var tail6 = new Tails(6);
        tail5.MoveEvent += tail6.OnHeadMove;

        var tail7 = new Tails(7);
        tail6.MoveEvent += tail7.OnHeadMove;

        var tail8 = new Tails(8);
        tail7.MoveEvent += tail8.OnHeadMove;

        var tail9 = new Tails(9);
        tail8.MoveEvent += tail9.OnHeadMove;

        foreach (var item in input)
        {
            var commands = item.Split(' ');
            Console.WriteLine($"Command {item}");
            head.Move(commands[0].ConvertToDirections(), int.Parse(commands[1]));
        }

        var tailsVisitedCoordinates = tail9.VisitedCoordinates.Count;
        Console.WriteLine($"Tail 9 coordinates visited: {tailsVisitedCoordinates}");
    }
}