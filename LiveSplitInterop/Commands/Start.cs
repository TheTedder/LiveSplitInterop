using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop.Commands
{
    public sealed class Start : Command
    {
        public override string Message => "start";
    }

    public static class StartExtensions
    {
        public static void Start(this ILiveSplitCommandClient client)
        {
            client.SendCommand(new Start());
        }

        public static async Task StartAsync(this IAsyncLiveSplitCommandClient client)
        {
            await client.SendCommandAsync(new Start());
        }
    }
}
