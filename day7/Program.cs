internal static class Program
{
    private static async Task Main()
    {
        var input = await System.IO.File.ReadAllLinesAsync("input.txt");
        var rootFolder = ParseCommands(input);

        var neededSize = 70000000 - rootFolder.CalculateSize();
        var neededSizeToDelete = 30000000 - neededSize;
        //var totalSize = GetFoldersAtMostSize(rootFolder, 100000);

        var folderSizeToDelete = Traverse(rootFolder)
        .Where(f => f.CalculateSize() >= neededSizeToDelete)
        .Min(f => f.CalculateSize());
        Console.WriteLine($"Size={folderSizeToDelete}");
    }

    public static IEnumerable<Folder> Traverse(Folder root)
    {
        var stack = new Stack<Folder>();
        stack.Push(root);
        while (stack.Count > 0)
        {
            var current = stack.Pop();
            yield return current;
            foreach (var child in current.GetFolders())
                stack.Push(child);
        }
    }

    private static long GetFoldersAtMostSize(Folder rootFolder, long maxSize)
    {
        long totalSize = 0;
        if (!rootFolder.Name.Equals("/") && !rootFolder.GetFolders().Any())
        {
            var size = rootFolder.CalculateSize();
            return size < maxSize ? size : 0;
        }

        foreach (var folder in rootFolder.GetFolders())
        {
            totalSize += GetFoldersAtMostSize(folder, maxSize);
        }

        var size2 = rootFolder.CalculateSize();
        size2 = size2 < maxSize ? size2 : 0;
        return totalSize += size2;
    }

    private static Folder ParseCommands(string[] inputs)
    {
        var rootFolder = new Folder("/", null);
        var currentFolder = rootFolder;
        foreach (var input in inputs)
        {
            if (input.StartsWith("$"))
            {
                if (input.Equals("$ ls"))
                {
                    continue;
                }

                if (input.StartsWith("$ cd"))
                {
                    var cdInputs = input.Split(" ");
                    if (cdInputs[2].Equals("/"))
                    {
                        continue;
                    }
                    else if (cdInputs[2].Equals(".."))
                    {
                        currentFolder = currentFolder.Parent;
                    }
                    else
                    {
                        currentFolder = currentFolder.GetFolder(cdInputs[2]);
                    }
                }
            }
            else
            {
                var entry = input.Split(' ');
                if (entry[0].Equals("dir"))
                {
                    var newFolder = new Folder(entry[1], currentFolder);
                    currentFolder.Add(newFolder);
                }
                else
                {
                    var newFile = new File(entry[1], long.Parse(entry[0]));
                    currentFolder.Add(newFile);
                }
            }
        }

        return rootFolder;
    }
}


interface IFileSystemEntry
{
    string Name { get; }
    long Size { get; }

    long CalculateSize();
}

class File : IFileSystemEntry
{
    public File(string name, long size)
    {
        Name = name;
        Size = size;
    }

    public string Name { get; }
    public long Size { get; }

    public override string ToString()
    {
        return $"{Name}: {Size}";
    }

    public long CalculateSize()
    {
        return Size;
    }
}

class Folder : IFileSystemEntry
{
    public Folder(string name, Folder? parent)
    {
        Name = name;
        Parent = parent;
        Entries = new List<IFileSystemEntry>();
    }

    public string Name { get; }
    public long Size { get; }

    public Folder? Parent { get; }

    private List<IFileSystemEntry> Entries { get; }

    public void Add(IFileSystemEntry entry)
    {
        Entries.Add(entry);
    }

    public IEnumerable<Folder> GetFolders()
    {
        return Entries.OfType<Folder>();
    }

    public Folder GetFolder(string name)
    {
        return (Folder)Entries.First(e => e.Name.Equals(name));
    }

    public long CalculateSize()
    {
        return Entries.Sum(i => i.CalculateSize());
    }

    public override string ToString()
    {
        return $"{Name}";
    }
}