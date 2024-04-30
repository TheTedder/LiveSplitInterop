using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop.Commands
{
    /// <summary>
    /// A command that adds to the tracked loading times.
    /// </summary>
    /// <remarks>
    /// Using this command subtracts the specified time from the current game time.
    /// </remarks>
    public sealed class AddLoadingTimes : Command
    {
        /// <summary>
        /// The amount of time to add to loading times.
        /// </summary>
        public TimeSpan? LoadingTimes { get; private set; }

        public AddLoadingTimes(TimeSpan? lt)
        {
            LoadingTimes = lt;
        }

        public override string Message => $"addloadingtimes {LoadingTimes?.ToLsString() ?? "-"}";
    }

    public static class AddLoadingTimesExtensions
    {
        /// <summary>
        /// Add to the loading times.
        /// </summary>
        public static void AddLoadingTimes(this ILiveSplitCommandClient client, TimeSpan? loadingTimes)
        {
            client.SendCommand(new AddLoadingTimes(loadingTimes));
        }

        /// <summary>
        /// Add to the loading times asynchronously.
        /// </summary>
        public static async Task AddLoadingTimesAsync(this IAsyncLiveSplitCommandClient client, TimeSpan? loadingTimes)
        {
            await client.SendCommandAsync(new AddLoadingTimes(loadingTimes));
        }
    }
}
