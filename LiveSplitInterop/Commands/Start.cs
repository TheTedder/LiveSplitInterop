using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop.Commands
{
    /// <summary>
    /// A command that starts the timer.
    /// </summary>
    public sealed class Start : Command
    {
        public override string Message => "start";
    }

    public static class StartExtensions
    {
        /// <summary>
        /// Start the timer.
        /// </summary>
        public static void Start(this ILiveSplitCommandClient client)
        {
            client.SendCommand(new Start());
        }

        /// <summary>
        /// Start the timer.
        /// </summary>
        public static async Task StartAsync(this IAsyncLiveSplitCommandClient client)
        {
            await client.SendCommandAsync(new Start());
        }
    }
}
