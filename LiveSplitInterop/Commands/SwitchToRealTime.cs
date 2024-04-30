using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop.Commands
{
    /// <summary>
    /// A command that switches the timer to real time.
    /// </summary>
    public sealed class SwitchToRealTime : Command
    {
        public override string Message => "switchto realtime";
    }

    public static class SwitchToRealTimeExtensions
    {
        /// <summary>
        /// Switch the timer to real time.
        /// </summary>
        public static void SwitchToRealTime(this ILiveSplitCommandClient client)
        {
            client.SendCommand(new SwitchToRealTime());
        }

        /// <summary>
        /// Switch the timer to real time asynchronously.
        /// </summary>
        public static async Task SwitchToRealTimeAsync(this IAsyncLiveSplitCommandClient client)
        {
            await client.SendCommandAsync(new SwitchToRealTime());
        }
    }
}
