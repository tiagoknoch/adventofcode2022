namespace Day11
{
    internal class Program
    {
        static List<Monkey> monkeys = new();
        private static void Main(string[] args)
        {
            //GenerateDemoMonkeys();
            GenerateInputMonkeys();

            var round = 20;
            while (round > 0)
            {
                round--;
                foreach (var monkey in monkeys)
                {
                    monkey.Inspect();
                }
            }

            var maxInspections = monkeys.Select(m => m.NrInspections)
            .OrderByDescending(i => i)
            .Take(2)
            .ToList();

            Console.WriteLine($"Max Inspections: {string.Join(",", maxInspections)}, multiplication: {maxInspections[0] * maxInspections[1]}");

            void OnItemThrow(object? sender, ItemThrownEventArs e)
            {
                monkeys[e.ToMonkey].ReceiveItem(e.Item);
            }

            void GenerateDemoMonkeys()
            {
                var monkey0 = new Monkey(new long[] { 79, 98 },
                    i => i % 23 == 0 ? 2 : 3,
                    i => i * 19);
                monkey0.ItemThrownEvent += OnItemThrow;

                var monkey1 = new Monkey(new long[] { 54, 65, 75, 74 },
                i => i % 19 == 0 ? 2 : 0,
                i => i + 6);
                monkey1.ItemThrownEvent += OnItemThrow;

                var monkey2 = new Monkey(new long[] { 79, 60, 97 },
                    i => i % 13 == 0 ? 1 : 3,
                    i => i * i);
                monkey2.ItemThrownEvent += OnItemThrow;

                var monkey3 = new Monkey(new long[] { 74 },
                    i => i % 17 == 0 ? 0 : 1,
                    i => i + 3);
                monkey3.ItemThrownEvent += OnItemThrow;

                monkeys = new List<Monkey> { monkey0, monkey1, monkey2, monkey3 };
            }

            void GenerateInputMonkeys()
            {
                var monkey0 = new Monkey(new long[] { 98, 97, 98, 55, 56, 72 },
                    i => i % 11 == 0 ? 4 : 7,
                    i => i * 13);
                monkey0.ItemThrownEvent += OnItemThrow;

                var monkey1 = new Monkey(new long[] { 73, 99, 55, 54, 88, 50, 55 },
                i => i % 17 == 0 ? 2 : 6,
                i => i + 4);
                monkey1.ItemThrownEvent += OnItemThrow;

                var monkey2 = new Monkey(new long[] { 67, 98 },
                i => i % 5 == 0 ? 6 : 5,
                i => i * 11);
                monkey2.ItemThrownEvent += OnItemThrow;

                var monkey3 = new Monkey(new long[] { 82, 91, 92, 53, 99 },
                    i => i % 13 == 0 ? 1 : 2,
                    i => i + 8);
                monkey3.ItemThrownEvent += OnItemThrow;

                var monkey4 = new Monkey(new long[] { 52, 62, 94, 96, 52, 87, 53, 60 },
                    i => i % 19 == 0 ? 3 : 1,
                    i => i * i);
                monkey4.ItemThrownEvent += OnItemThrow;

                var monkey5 = new Monkey(new long[] { 94, 80, 84, 79 },
                    i => i % 2 == 0 ? 7 : 0,
                    i => i + 5);
                monkey5.ItemThrownEvent += OnItemThrow;

                var monkey6 = new Monkey(new long[] { 89 },
                    i => i % 3 == 0 ? 0 : 5,
                    i => i + 1);
                monkey6.ItemThrownEvent += OnItemThrow;

                var monkey7 = new Monkey(new long[] { 70, 59, 63 },
                    i => i % 7 == 0 ? 4 : 3,
                    i => i + 3);
                monkey7.ItemThrownEvent += OnItemThrow;

                monkeys = new List<Monkey> { monkey0, monkey1, monkey2, monkey3, monkey4, monkey5, monkey6, monkey7 };
            }
        }
    }
}