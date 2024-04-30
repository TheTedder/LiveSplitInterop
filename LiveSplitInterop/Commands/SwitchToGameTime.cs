using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop.Commands
{
    /// <summary>
    /// A command that switches the timer to game time.
    /// </summary>
    public sealed class SwitchToGameTime : Command
    {
        public override string Message => "switchto gametime";
    }

    public static class SwitchToGameTimeExtensions
    {
        /// <summary>
        /// Switch the timer to game time.
        /// </summary>
        public static void SwitchToGameTime(this ILiveSplitCommandClient client)
        {
            client.SendCommand(new SwitchToGameTime());
        }

        /// <summary>
        /// Switch the timer to game time asynchronously.
        /// </summary>
        public static async Task SwitchToGameTimeAsync(this IAsyncLiveSplitCommandClient client)
        {
            await client.SendCommandAsync(new SwitchToGameTime());
        }
    }
}
