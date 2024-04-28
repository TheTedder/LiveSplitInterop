﻿using LiveSplitInterop.Clients;
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
[A]      Add loading times
[GR]     Get current real time
[GNL]    Get last split name
[GTL]    Get last split time
[U]      Undo split
[D]      Get delta
[N]      Get current split name
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
                            }

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
                Console.WriteLine("Current Split: {0}", splitname);
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
