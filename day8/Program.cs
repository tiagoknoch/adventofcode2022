// See https://aka.ms/new-console-template for more information
using MoreLinq;

var input = await File.ReadAllLinesAsync("input.txt");

var matrix = ParseInput(input);

Part1(matrix);

Part2(matrix);

static IEnumerable<int> GetLineTop(int[,] matrix, int i, int j)
{
    int index = i;
    while (index > 0)
    {
        yield return matrix[--index, j];
    }
}

static IEnumerable<int> GetLineBottom(int[,] matrix, int i, int j)
{
    int index = i;
    var length = matrix.GetLength(0);
    while (index < length - 1)
    {
        yield return matrix[++index, j];
    }
}

static IEnumerable<int> GetLineLeft(int[,] matrix, int i, int j)
{
    int index = j;
    while (index > 0)
    {
        yield return matrix[i, --index];
    }
}

static IEnumerable<int> GetLineRight(int[,] matrix, int i, int j)
{
    int index = j;
    var length = matrix.GetLength(0);
    while (index < length - 1)
    {
        yield return matrix[i, ++index];
    }
}

static int[,] ParseInput(string[] input)
{
    var length = input[0].Length;
    int[,] result = new int[length, length];
    int i = 0, j = 0;

    foreach (var stringValue in input)
    {
        foreach (var charValue in stringValue)
        {
            result[i, j] = int.Parse(charValue.ToString());
            ++j;
        }
        ++i;
        j = 0;
    }

    return result;
}

static void Part1(int[,] matrix)
{
    var length = matrix.GetLength(0);

    int countVisible = 0;
    for (int i = 0; i < length; i++)
    {
        for (int j = 0; j < length; j++)
        {
            if (i == 0 || j == 0 || i == length - 1 || j == length - 1)
            {
                ++countVisible;
                continue;
            }

            var item = matrix[i, j];

            bool isVisibleTop = !GetLineTop(matrix, i, j).Any(i => i >= item);
            bool isVisibleBottom = !GetLineBottom(matrix, i, j).Any(i => i >= item);
            bool isVisibleLeft = !GetLineLeft(matrix, i, j).Any(i => i >= item);
            bool isVisibleRight = !GetLineRight(matrix, i, j).Any(i => i >= item);

            if (isVisibleTop ||
            isVisibleBottom ||
            isVisibleLeft ||
            isVisibleRight)
            {
                ++countVisible;
            }
        }
    }

    Console.WriteLine($"Visible trees: {countVisible}");
}

static void Part2(int[,] matrix)
{
    var length = matrix.GetLength(0);

    int maxScore = 0;
    for (int i = 0; i < length; i++)
    {
        for (int j = 0; j < length; j++)
        {
            if (i == 0 || j == 0 || i == length - 1 || j == length - 1)
            {
                continue;
            }

            var item = matrix[i, j];

            var scoreTop = GetLineTop(matrix, i, j)
            .TakeUntil(i => i >= item)
            .Count();

            var scoreBottom = GetLineBottom(matrix, i, j)
            .TakeUntil(i => i >= item)
            .Count();

            var scoreLeft = GetLineLeft(matrix, i, j)
            .TakeUntil(i => i >= item)
            .Count();

            var scoreRight = GetLineRight(matrix, i, j)
            .TakeUntil(i => i >= item)
            .Count();

            var totalScore = scoreTop * scoreBottom * scoreLeft * scoreRight;
            if (totalScore > maxScore)
            {
                maxScore = totalScore;
            }
        }
    }

    Console.WriteLine($"Max scenic score: {maxScore}");
}