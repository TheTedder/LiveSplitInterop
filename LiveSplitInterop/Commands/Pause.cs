using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop.Commands
{
    /// <summary>
    /// A command that pauses the timer. Games should use <see cref="PauseGameTime"/> instead.
    /// </summary>
    public sealed class Pause : Command
    {
        public override string Message => "pause";
    }

    public static class PauseExtensions
    {
        /// <summary>
        /// Pause the timer. Games should use
        /// <see cref="PauseGameTimeExtensions.PauseGameTime(ILiveSplitCommandClient)"/>
        /// instead.
        /// </summary>
        public static void Pause(this ILiveSplitCommandClient client)
        {
            client.SendCommand(new Pause());
        }

        /// <summary>
        /// Pause the timer asynchronously. Games should use
        /// <see cref="PauseGameTimeExtensions.PauseGameTimeAsync(IAsyncLiveSplitCommandClient)"/>
        /// instead.
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public static async Task PauseAsync(this IAsyncLiveSplitCommandClient client)
        {
            await client.SendCommandAsync(new Pause());
        }
    }
}
