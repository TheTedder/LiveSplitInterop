using LiveSplitInterop.Clients;
using LiveSplitInterop.Commands;

using NamedPipeCommandClient client = new();
Console.WriteLine("Connecting to LiveSplit...");
await client.ConnectAsync();
Console.WriteLine(
@"Connected! Available commands: 
[Q]     Quit
[Enter] Start or split
[I]     Print current split index
[SG]    Set game time
[GR]    Get current real time
[U]     Undo split");

bool quit = false;
while (!quit)
{
    ConsoleKeyInfo key = Console.ReadKey(true);

    switch (key.Key)
    {
        case ConsoleKey.Q:
            quit = true;
            break;

        case ConsoleKey.Enter:
            await client.StartOrSplitAsync();
            break;

        case ConsoleKey.I:
            int index = await client.GetSplitIndexAsync();
            Console.WriteLine(index);
            break;

        case ConsoleKey.S:
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.G:
                Console.Write("Set game time to (Enter a time): ");
                string? line = Console.ReadLine();

                if (line != null)
                {
                    await client.SetGameTimeAsync(TimeSpan.Parse(line));
                }

                break;
            }

            break;
            
        case ConsoleKey.G:
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.R:
                    Console.WriteLine("Current time: {0:c}", await client.GetCurrentRealTimeAsync());
                    break;
            }

            break;

        case ConsoleKey.U:
            await client.UndoSplitAsync();
            break;
    }
}
