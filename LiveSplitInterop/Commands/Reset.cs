using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop.Commands
{
    /// <summary>
    /// A command that resets the timer.
    /// </summary>
    public sealed class Reset : Command
    {
        public override string Message => "reset";
    }

    public static class ResetExtensions
    {
        /// <summary>
        /// Reset the timer.
        /// </summary>
        public static void Reset(this ILiveSplitCommandClient client)
        {
            client.SendCommand(new Reset());
        }

        /// <summary>
        /// Reset the timer asynchronously.
        /// </summary>
        public static async Task ResetAsync(this IAsyncLiveSplitCommandClient client)
        {
            await client.SendCommandAsync(new Reset());
        }
    }
}
