using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop.Commands
{
    /// <summary>
    /// A command that pauses game time.
    /// </summary>
    public sealed class PauseGameTime : Command
    {
        public override string Message => "pausegametime";
    }

    public static class PauseGameTimeExtensions
    {
        /// <summary>
        /// Pause game time.
        /// </summary>
        public static void PauseGameTime(this ILiveSplitCommandClient client)
        {
            client.SendCommand(new PauseGameTime());
        }

        /// <summary>
        /// Pause game time asynchronously.
        /// </summary>
        public static async Task PauseGameTimeAsync(this IAsyncLiveSplitCommandClient client)
        {
            await client.SendCommandAsync(new PauseGameTime());
        }
    }
}
