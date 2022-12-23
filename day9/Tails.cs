namespace Day9
{
    class Tails
    {
        public Tails(int nr)
        {
            Nr = nr;
            VisitedCoordinates = new HashSet<(int, int)>
        {
            (0, 0)
        };
        }

        readonly int Nr;

        int X;
        int Y;
        public HashSet<(int, int)> VisitedCoordinates { get; }

        public event EventHandler<HeadMoveEventArgs> MoveEvent;

        private bool isHeadInRange(int headX, int headY)
        {
            return Math.Abs(headX - X) < 2 && Math.Abs(headY - Y) < 2;
        }

        public void OnHeadMove(object sender, HeadMoveEventArgs args)
        {
            if (isHeadInRange(args.X, args.Y))
            {
                return;
            }

            switch (args.Direction)
            {
                case Directions.UP:
                    Y++;

                    if (args.X != X)
                    {
                        if (args.X > X)
                        {
                            X++;
                        }
                        else
                        {
                            X--;
                        }
                    }
                    break;
                case Directions.DOWN:
                    Y--;

                    if (args.X != X)
                    {
                        if (args.X > X)
                        {
                            X++;
                        }
                        else
                        {
                            X--;
                        }
                    }
                    break;
                case Directions.LEFT:
                    X--;

                    if (args.Y != Y)
                    {
                        if (args.Y > Y)
                        {
                            Y++;
                        }
                        else
                        {
                            Y--;
                        }
                    }
                    break;
                case Directions.RIGHT:
                    X++;

                    if (args.Y != Y)
                    {
                        if (args.Y > Y)
                        {
                            Y++;
                        }
                        else
                        {
                            Y--;
                        }
                    }
                    break;
            }

            Console.WriteLine($"OnMove Tails{Nr} [{X},{Y}]");
            AddCoordinates();
            OnMove(args.Direction, X, Y);
        }

        private void AddCoordinates()
        {
            if (!VisitedCoordinates.Contains((X, Y)))
            {
                VisitedCoordinates.Add((X, Y));
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