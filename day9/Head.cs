namespace Day9
{
    public class Head
    {
        public Head()
        {
            X = 0;
            Y = 0;
        }

        public event EventHandler<HeadMoveEventArgs> MoveEvent;

        int X;
        int Y;

        public void Move(Directions direction, int times)
        {
            foreach (var i in Enumerable.Range(1, times))
            {
                switch (direction)
                {
                    case Directions.UP:
                        Y++;
                        break;
                    case Directions.DOWN:
                        Y--;
                        break;
                    case Directions.LEFT:
                        X--;
                        break;
                    case Directions.RIGHT:
                        X++;
                        break;
                }


                Console.WriteLine($"-- OnMove Head [{X},{Y}]");
                OnMove(direction, X, Y);
            }
        }

        protected virtual void OnMove(Directions direction, int x, int y)
        {
            var args = new HeadMoveEventArgs
            {
                X = x,
                Y = y,
                Direction = direction
            };

            MoveEvent?.Invoke(this, args);
        }
    }
}