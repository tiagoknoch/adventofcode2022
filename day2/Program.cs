// See https://aka.ms/new-console-template for more information

var entries = await File.ReadAllLinesAsync("input.txt");
var scores = new List<int>();
var scores2 = new List<int>();

foreach (var item in entries)
{
    var choices = item.Split(" ");
    var score = calculateScore(convert(choices[0]), convert(choices[1]));
    scores.Add(score);

    var score2 = calculateScore2(convert(choices[0]), convertScore(choices[1]));
    scores2.Add(score2);
}

Console.WriteLine($"total score: {scores.Sum()}");
Console.WriteLine($"total score2: {scores2.Sum()}");


int calculateScore(RockPaperScissors opponentPlay, RockPaperScissors myPlay)
{
    // draw = 3
    if (opponentPlay == myPlay)
    {
        return 3 + (int)myPlay;
    }

    if (myPlay == RockPaperScissors.Scissors && opponentPlay == RockPaperScissors.Paper)
    {
        return 6 + (int)myPlay;
    }

    if (myPlay == RockPaperScissors.Scissors && opponentPlay == RockPaperScissors.Rock)
    {
        return 0 + (int)myPlay;
    }

    if (myPlay == RockPaperScissors.Paper && opponentPlay == RockPaperScissors.Rock)
    {
        return 6 + (int)myPlay;
    }

    if (myPlay == RockPaperScissors.Paper && opponentPlay == RockPaperScissors.Scissors)
    {
        return 0 + (int)myPlay;
    }

    if (myPlay == RockPaperScissors.Rock && opponentPlay == RockPaperScissors.Scissors)
    {
        return 6 + (int)myPlay;
    }

    if (myPlay == RockPaperScissors.Rock && opponentPlay == RockPaperScissors.Paper)
    {
        return 0 + (int)myPlay;
    }

    throw new InvalidOperationException($"Missing play {opponentPlay} vs {myPlay}");
}


int calculateScore2(RockPaperScissors opponentPlay, Result result)
{


    // draw = 3
    if (result == Result.Draw)
    {
        return (int)result + (int)opponentPlay;
    }

    return (int)result + (int)calculatePlay(opponentPlay, result);
}

RockPaperScissors calculatePlay(RockPaperScissors opponentPlay, Result result)
{
    if (opponentPlay == RockPaperScissors.Scissors && result == Result.Win)
    {
        return RockPaperScissors.Rock;
    }

    if (opponentPlay == RockPaperScissors.Paper && result == Result.Win)
    {
        return RockPaperScissors.Scissors;
    }

    if (opponentPlay == RockPaperScissors.Rock && result == Result.Win)
    {
        return RockPaperScissors.Paper;
    }

    if (opponentPlay == RockPaperScissors.Scissors && result == Result.Lost)
    {
        return RockPaperScissors.Paper;
    }

    if (opponentPlay == RockPaperScissors.Paper && result == Result.Lost)
    {
        return RockPaperScissors.Rock;
    }

    if (opponentPlay == RockPaperScissors.Rock && result == Result.Lost)
    {
        return RockPaperScissors.Scissors;
    }

    throw new InvalidOperationException($"Missing play {opponentPlay} vs {result}");
}


RockPaperScissors convert(string play)
{
    switch (play)
    {
        case "A":
        case "X":
            return RockPaperScissors.Rock;
        case "B":
        case "Y":
            return RockPaperScissors.Paper;
        case "C":
        case "Z":
            return RockPaperScissors.Scissors;
    }

    throw new InvalidOperationException("convert was not expecting value " + play);
}


Result convertScore(string play)
{
    switch (play)
    {
        case "X":
            return Result.Lost;
        case "Y":
            return Result.Draw;
        case "Z":
            return Result.Win;
    }

    throw new InvalidOperationException("convertScore was not expecting value " + play);
}

enum RockPaperScissors
{
    Rock = 1,
    Paper = 2,
    Scissors = 3
}

enum Result
{
    Lost = 0,
    Draw = 3,
    Win = 6
}