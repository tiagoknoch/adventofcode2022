
var input = await File.ReadAllLinesAsync("input.txt");

Part1(input);
Part2(input);

static void Part1(string[] input)
{
    var cpu = new CPU();
    cpu.InputCommands(input);
    cpu.Execute();
    Console.WriteLine($"Total Signal Strength={cpu.SignalStrength}");
}

static void Part2(string[] input)
{
    var crt = new CRT();
    var cpu = new CPU();
    cpu.CycleStartEvent += (sender, f2) =>
    {
        var cpu = sender as CPU;
        crt.Render(cpu.Cycle, cpu.RegisterX);
    };

    cpu.InputCommands(input);
    cpu.Execute();
    crt.Draw();
}

public class CRT
{
    private readonly bool[,] crt = new bool[6, 40];

    public void Render(int cycle, int registerX)
    {
        var didRender = false;
        var row = (cycle - 1) / 40;
        var column = (cycle - 1) % 40;

        if (column >= registerX - 1 && column <= registerX + 1)
        {
            crt[row, column] = true;
            didRender = true;
        }

        //Console.WriteLine($"CRT draw cycle {cycle}, [{row},{column}]: {didRender}");
    }

    public void Draw()
    {
        for (int row = 0; row < 6; row++)
        {
            Console.WriteLine(string.Concat(Enumerable.Range(0, crt.GetLength(1))
              .Select(x => crt[row, x])
              .Select(i => i ? "#" : ".")));
        }
    }
}

public interface IInstruction
{
    void ExecuteCycle();

    bool IsFinished();
}

public class CPU
{
    public CPU()
    {
        Cycle = 1;
        RegisterX = 1;
    }

    public int Cycle { get; private set; }

    public int SignalStrength { get; private set; }

    public int RegisterX { get; set; }

    public event EventHandler<int> CycleStartEvent;

    private readonly Queue<IInstruction> Commands = new();

    private IInstruction currentCommand;

    public void InputCommands(string[] commands)
    {
        foreach (var command in commands)
        {
            var commandParts = command.Split(" ");
            switch (commandParts[0])
            {
                case "noop":
                    Commands.Enqueue(new Noop());
                    break;
                case "addx":
                    Commands.Enqueue(new AddX(this, commandParts[1]));
                    break;
                default:
                    throw new NotSupportedException("Invalid command " + command);
            }
        }
    }

    public void Execute()
    {
        while (true)
        {
            if (Commands.Count == 0 && currentCommand == null)
            {
                return;
            }

            currentCommand ??= Commands.Dequeue();

            //Console.WriteLine($"Cycle: {Cycle}, RegisterX: {RegisterX}");
            OnCycleStartEvent();

            if (Cycle == 20 || Cycle == 60 || Cycle == 100 || Cycle == 140 || Cycle == 180 || Cycle == 220)
            {
                //Console.WriteLine($"Cycle Strength: {Cycle * RegisterX}");
                SignalStrength += Cycle * RegisterX;
            }

            currentCommand.ExecuteCycle();

            if (currentCommand.IsFinished())
            {
                currentCommand = null;
            }

            Cycle++;
        }
    }

    protected virtual void OnCycleStartEvent()
    {
        CycleStartEvent?.Invoke(this, RegisterX);
    }
}

public class Noop : IInstruction
{
    public void ExecuteCycle()
    {
    }

    public bool IsFinished()
    {
        return true;
    }
}

public class AddX : IInstruction
{
    int cycles = 2;
    readonly int value;
    readonly CPU cpu;

    public AddX(CPU cpu, string param)
    {
        this.cpu = cpu;
        value = int.Parse(param);
    }

    public void ExecuteCycle()
    {
        --cycles;
        if (cycles == 0)
        {
            cpu.RegisterX += value;
        }
    }

    public bool IsFinished()
    {
        return cycles == 0;
    }
}