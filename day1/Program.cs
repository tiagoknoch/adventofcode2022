var entries = await File.ReadAllLinesAsync("input.txt");

var result = new List<int>();
var temp = new List<int>();
foreach (var item in entries)
{
    if (item.Equals(""))
    {
        result.Add(temp.Sum());
        temp.Clear();
    }
    else
    {
        temp.Add(int.Parse(item));
    }
}

Console.WriteLine("result1: " + result.Max());

var max1 = result.Max();
var indexAtMax = result.IndexOf(result.Max());
result.RemoveAt(indexAtMax);
var max2 = result.Max();
indexAtMax = result.IndexOf(result.Max());
result.RemoveAt(indexAtMax);
var max3 = result.Max();
indexAtMax = result.IndexOf(result.Max());
result.RemoveAt(indexAtMax);

Console.WriteLine("result1: " + (max1 + max2 + max3));