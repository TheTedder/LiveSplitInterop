using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop.Commands
{
    public sealed class Pause : Command
    {
        public override string Message => "pause";
    }

    public static class PauseExtensions
    {
        public static void Pause(this ILiveSplitCommandClient client)
        {
            client.SendCommand(new Pause());
        }

        public static async Task PauseAsync(this IAsyncLiveSplitCommandClient client)
        {
            await client.SendCommandAsync(new Pause());
        }
    }
}
