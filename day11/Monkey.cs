namespace Day11
{

    public class Monkey
    {
        public Monkey(IEnumerable<long> items, Func<long, int> action, Func<long, long> operation)
        {
            this.items = new List<long>(items);
            this.action = action;
            this.operation = operation;
            this.NrInspections = 0;
        }

        public long NrInspections { get; private set; }

        private readonly List<long> items;

        private readonly Func<long, int> action;

        private readonly Func<long, long> operation;

        public event EventHandler<ItemThrownEventArs> ItemThrownEvent;

        public void ReceiveItem(long item)
        {
            items.Add(item);
        }

        public void Inspect()
        {
            while (items.Any())
            {
                NrInspections++;
                var worryLevel = items[0];
                items.RemoveAt(0);
                var newWorryLevel = operation(worryLevel);
                newWorryLevel /= 3;
                var monkeyToThrow = action(newWorryLevel);
                OnItemThrownEvent(newWorryLevel, monkeyToThrow);
            }
        }

        protected virtual void OnItemThrownEvent(long item, int toMonkey)
        {
            var eventArgs = new ItemThrownEventArs
            {
                Item = item,
                ToMonkey = toMonkey
            };

            ItemThrownEvent?.Invoke(this, eventArgs);
        }
    }

    public class ItemThrownEventArs : EventArgs
    {
        public long Item { get; set; }
        public int ToMonkey { get; set; }
    }
}