using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop.Commands
{
    /// <summary>
    /// A command that stops game time from automatically advancing.
    /// </summary>
    public class AlwaysPauseGameTime : Command
    {
        public override string Message => "alwayspausegametime";
    }

    public static class AlwaysPauseGameTimeExtensions
    {
        /// <summary>
        /// Stop game time from automatically advancing.
        /// </summary>
        public static void AlwaysPauseGameTime(this ILiveSplitCommandClient client)
        {
            client.SendCommand(new AlwaysPauseGameTime());
        }

        /// <inheritdoc cref="AlwaysPauseGameTime(ILiveSplitCommandClient)"/>
        public static async Task AlwaysPauseGameTimeAsync(this IAsyncLiveSplitCommandClient client)
        {
            await client.SendCommandAsync(new AlwaysPauseGameTime());
        }
    }
}
