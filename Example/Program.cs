using LiveSplitInterop.Clients;
using LiveSplitInterop.Commands;
using System.Net;

using NamedPipeCommandClient client = new();
//using TcpCommandClient client = new("localhost", 16834);
Console.WriteLine("Connecting to LiveSplit...");
await client.ConnectAsync();
Console.WriteLine(
@"Connected! Available commands: 
[Q]      Quit
[Enter]  Start or split
[I]      Print current split index
[SG]     Set game time
[SL]     Set loading times
[SNC]    Set current split name
[SNI]    Set split name at index
[A]      Add loading times
[GR]     Get current real time
[GNL]    Get last split name
[GTL]    Get last split 
[GTC]    Get current split time
[GP]     Get timer phase
[U]      Undo split
[D]      Get delta
[N]      Get current split name
[T]      Get current time
[F]      Get final time
[B]      Get best possible time
[P]      Get predicted time
[C]      Set comparison
[SWG]    Switch to game time
[SWR]    Switch to real time
[Insert] Custom command");

bool quit = false;
while (!quit)
{
    ConsoleKeyInfo key = Console.ReadKey(true);

    switch (key.Key)
    {
        case ConsoleKey.Q:
            {
            quit = true;

            break;
            }

        case ConsoleKey.Enter:
            {

            await client.StartOrSplitAsync();
            break;
            }

        case ConsoleKey.I:
            {

            int index = await client.GetSplitIndexAsync();
            Console.WriteLine(index);
            break;
            }

        case ConsoleKey.S:
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.G:
                        {
                            Console.Write("Set game time to (Enter a time): ");
                            string? line = Console.ReadLine();

                            if (line is not null)
                            {
                                await client.SetGameTimeAsync(TimeSpan.Parse(line));
                            }

                            break;
                        }

                    case ConsoleKey.L:
                        {
                            Console.Write("Set loading times to (Enter a time or leave blank to reset):");
                            string? line = Console.ReadLine();
                            await client.SetLoadingTimesAsync(string.IsNullOrEmpty(line) ? null : TimeSpan.Parse(line));
                            break;
                        }

                    case ConsoleKey.N:
                        {
                            switch (Console.ReadKey(true).Key)
                            {
                                case ConsoleKey.C:
                                    {
                                        Console.Write("Split name: ");
                                        string? name = Console.ReadLine();

                                        if (name is not null)
                                        {
                                            await client.SetCurrentSplitNameAsync(name);
                                        }

                                        break;
                                    }

                                case ConsoleKey.I:
                                    {
                                        Console.Write("Index: ");
                                        string? index = Console.ReadLine();

                                        if (index is null)
                                        {
                                            break;
                                        }

                                        int i = int.Parse(index);
                                        Console.Write("Split name: ");
                                        string? name = Console.ReadLine();

                                        if (name is null)
                                        {
                                            break;
                                        }

                                        await client.SetSplitNameAsync(i, name);

                                        break;
                                    }
                            }
                            break;
                        }

                    case ConsoleKey.W:
                        {
                            switch (Console.ReadKey(true).Key)
                            {
                                case ConsoleKey.G:
                                    {
                                        await client.SwitchToGameTimeAsync();
                                        break;
                                    }

                                case ConsoleKey.R:
                                    {
                                        await client.SwitchToRealTimeAsync();
                                        break;
                                    }
                            }

                            break;
                        }
                }

                break;
            }

        case ConsoleKey.A:
            {
                Console.Write("Add to loading times:");
                string? line = Console.ReadLine();

                if (line is not null)
                {
                    await client.AddLoadingTimesAsync(TimeSpan.Parse(line));
                }

                break;
            }

        case ConsoleKey.G:
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.R:
                        {
                            Console.WriteLine("Current time: {0:c}", await client.GetCurrentRealTimeAsync());
                            break;
                        }

                    case ConsoleKey.N:
                        {
                            switch (Console.ReadKey(true).Key)
                            {
                                case ConsoleKey.L:
                                    {
                                        Console.WriteLine("Previous split name: {0}", await client.GetLastSplitNameAsync());
                                        break;
                                    }
                            }

                            break;
                        }

                    case ConsoleKey.T:
                        {
                            switch (Console.ReadKey(true).Key)
                            {
                                case ConsoleKey.L:
                                    {
                                        TimeSpan? time = await client.GetLastSplitTimeAsync();
                                        Console.WriteLine("Previous split time: {0}", time?.ToString("c") ?? "-");
                                        break;
                                    }

                                case ConsoleKey.C:
                                    {
                                        Console.Write("Comparison (leave blank for current):");
                                        string? comp = Console.ReadLine();

                                        if (comp == "")
                                        {
                                            comp = null;
                                        }

                                        TimeSpan? time = await client.GetCurrentSplitTimeAsync(comp);
                                        Console.WriteLine("Previous split time: {0}", time?.ToString("c") ?? "-");
                                        break;
                                    }
                            }

                            break;
                        }

                    case ConsoleKey.P:
                        {
                            Console.WriteLine("Timer phase: {0}", await client.GetTimerPhaseAsync());
                            break;
                        }
                }

                break;
            }

        case ConsoleKey.U:
            {
                await client.UndoSplitAsync();
                break;
            }

        case ConsoleKey.D:
            {
                TimeSpan? delta = await client.GetDeltaAsync();
                Console.WriteLine("Delta: {0:c}", delta?.ToString("c") ?? "-");
                break;
            }

        case ConsoleKey.N:
            {
                string splitname = await client.GetCurrentSplitNameAsync();
                Console.WriteLine("Current split: {0}", splitname);
                break;
            }

        case ConsoleKey.T:
            {
                Console.WriteLine("Curren time: {0:c}", await client.GetCurrentTimeAsync());
                break;
            }

        case ConsoleKey.F:
            {
                Console.Write("Comparison (leave blank for current):");
                string? comp = Console.ReadLine();

                if (comp == "")
                {
                    comp = null;
                }

                TimeSpan? time = await client.GetFinalTimeAsync(comp);
                Console.WriteLine("Final time: {0}", time?.ToString("c") ?? "-");
                break;
            }

        case ConsoleKey.B:
            {
                TimeSpan? time = await client.GetBestPossibleTimeAsync();
                Console.WriteLine("Best possible time: {0}", time?.ToString("c") ?? "-");
                break;
            }

        case ConsoleKey.P:
            {
                Console.Write("Comparison (leave blank for current):");
                string? comp = Console.ReadLine();

                if (comp == "")
                {
                    comp = null;
                }

                TimeSpan? time = await client.GetPredictedTimeAsync(comp);
                Console.WriteLine("Predicted time: {0}", time?.ToString("c") ?? "-");
                break;
            }

        case ConsoleKey.C:
            {
                Console.Write("Set comparison: ");
                string? comp = Console.ReadLine();

                if (comp is not null)
                {
                    await client.SetComparisonAsync(comp);
                }

                break;
            }

#if DEBUG
        case ConsoleKey.Insert:
            {
                Console.Write('>');

                string? line = Console.ReadLine();

                if (line is not null)
                {
                    await client.SendCommandRaw(line);

                    if (true || (key.Modifiers & ConsoleModifiers.Shift) != 0)
                    {
                        string? res = await client.ConsumeLineAsync();
                    
                        if (res is not null)
                        {
                            //Console.WriteLine("{0:x}", (short)res[0]);
                            Console.WriteLine(res);
                        }
                    }
                }

                break;
            }
#endif
    }
}
