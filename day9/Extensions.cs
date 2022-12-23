using Day9;

namespace Extensions
{
    public static class Extensions
    {
        public static Directions ConvertToDirections(this string value)
        {
            return value switch
            {
                "U" => Directions.UP,
                "D" => Directions.DOWN,
                "R" => Directions.RIGHT,
                "L" => Directions.LEFT,
                _ => throw new NotSupportedException($"unexpected value ${value}"),
            };
        }
    }
}