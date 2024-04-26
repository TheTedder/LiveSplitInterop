using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop.Commands
{
    /// <summary>
    /// A command that sets the loading times.
    /// </summary>
    /// <remarks>
    /// This command is the inverse of <see cref="SetGameTime"/>; using it sets
    /// game time equal to real time minus loading times.
    /// </remarks>
    public sealed class SetLoadingTimes : Command
    {
        private readonly TimeSpan? loadingTimes;

        public SetLoadingTimes(TimeSpan? lt)
        {
            loadingTimes = lt;
        }

        public override string Message => $"setloadingtimes {loadingTimes?.ToLsString() ?? "-"}";
    }

    public static class SetLoadingTimesExtensions
    {
        /// <summary>
        /// Set loading times.
        /// </summary>
        public static void SetLoadingTimes(this ILiveSplitCommandClient client, TimeSpan? loadingTimes)
        {
            client.SendCommand(new SetLoadingTimes(loadingTimes));
        }

        /// <summary>
        /// Set loading times asynchronously.
        /// </summary>
        public static async Task SetLoadingTimesAsync(this IAsyncLiveSplitCommandClient client, TimeSpan? loadingTimes)
        {
            await client.SendCommandAsync(new SetLoadingTimes(loadingTimes));
        }
    }
}
