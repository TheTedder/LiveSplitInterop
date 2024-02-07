using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop.Commands
{
    /// <summary>
    /// A command that unpauses game time.
    /// </summary>
    public class UnpauseGameTime : Command
    {
        public override string Message => "unpausegametime";
    }

    public static class UnpauseGameTimeExtensions
    {
        /// <summary>
        /// Unpause game time.
        /// </summary>
        public static void UnpauseGameTime(this ILiveSplitCommandClient client)
        {
            client.SendCommand(new UnpauseGameTime());
        }

        /// <summary>
        /// Unpause game time asynchronously.
        /// </summary>
        public static async Task UnpauseGameTimeAsync(this IAsyncLiveSplitCommandClient client)
        {
            await client.SendCommandAsync(new UnpauseGameTime());
        }
    }
}
