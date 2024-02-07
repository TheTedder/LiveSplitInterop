using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop.Commands
{
    /// <summary>
    /// A command that unpauses the timer if it's paused; otherwise, do nothing.
    /// Games should use <see cref="UnpauseGameTime"/> instead.
    /// </summary>
    public sealed class Resume : Command
    {
        public override string Message => "resume";
    }

    public static class ResumeExtensions
    {
        /// <summary>
        /// Unpause the timer if it's paused; otherwise, do nothing. Games should use
        /// <see cref="UnpauseGameTimeExtensions.UnpauseGameTime(ILiveSplitCommandClient)"/>
        /// instead.
        /// </summary>
        public static void Resume(this ILiveSplitCommandClient client)
        {
            client.SendCommand(new Resume());
        }

        /// <summary>
        /// Unpause the timer asynchronously if it's paused; otherwise, do nothing. Games
        /// should use
        /// <see cref="UnpauseGameTimeExtensions.UnpauseGameTimeAsync(IAsyncLiveSplitCommandClient)"/>
        /// instead.
        /// </summary>
        public static async Task ResumeAsync(this IAsyncLiveSplitCommandClient client)
        {
            await client.SendCommandAsync(new Resume());
        }
    }
}
