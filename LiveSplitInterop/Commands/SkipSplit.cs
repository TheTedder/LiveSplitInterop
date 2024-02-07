using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop.Commands
{
    /// <summary>
    /// A command that skips the current split.
    /// </summary>
    public sealed class SkipSplit : Command
    {
        public override string Message => "skipsplit";
    }

    public static class SkipSplitExtensions
    {
        /// <summary>
        /// Skip the current split.
        /// </summary>
        public static void SkipSplit(this ILiveSplitCommandClient client)
        {
            client.SendCommand(new SkipSplit());
        }

        /// <summary>
        /// Skip the current split asynchronously.
        /// </summary>
        public static async Task SkipSplitAsync(this IAsyncLiveSplitCommandClient client)
        {
            await client.SendCommandAsync(new SkipSplit());
        }
    }
}
