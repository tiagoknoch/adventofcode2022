// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var input = await File.ReadAllTextAsync("input.txt");

//var markerIndex = FindMarker(input);
var markerIndex = FindMessage(input);
Console.WriteLine($"Marker found at index {markerIndex}");

static int FindMarker(string input)
{
    const int markerLength = 4;
    for (int i = 0; i < input.Length; i++)
    {
        var marker = input.Substring(i, markerLength);
        if (marker.Distinct().Count() == markerLength)
        {
            return i + markerLength;
        }
    }

    throw new InvalidOperationException("could not find marker");
}

static int FindMessage(string input)
{
    const int messageLength = 14;
    for (int i = 0; i < input.Length; i++)
    {
        var marker = input.Substring(i, messageLength);
        if (marker.Distinct().Count() == messageLength)
        {
            return i + messageLength;
        }
    }

    throw new InvalidOperationException("could not find message");
}