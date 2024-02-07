using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop.Commands
{
    public sealed class SkipSplit : Command
    {
        public override string Message => "skipsplit";
    }

    public static class SkipSplitExtensions
    {
        public static void SkipSplit(this ILiveSplitCommandClient client)
        {
            client.SendCommand(new SkipSplit());
        }

        public static async Task SkipSplitAsync(this IAsyncLiveSplitCommandClient client)
        {
            await client.SendCommandAsync(new SkipSplit());
        }
    }
}
