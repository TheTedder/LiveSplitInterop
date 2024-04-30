using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop.Commands
{
    /// <summary>
    /// A command that gets the best possible predicted time for the run.
    /// </summary>
    public sealed class GetBestPossibleTime : Command<TimeSpan?>
    {
        public override string Message => "getbestpossibletime";

        public override TimeSpan? ParseResponse(string response) => Util.ParseLsTimeSpanNullable(response);
    }

    public static class GetBestPossibleTimeExtensions
    {
        /// <summary>
        /// Get the best possible predicted time for the run.
        /// </summary>
        public static TimeSpan? GetBestPossibleTime(this ILiveSplitCommandClient client)
        {
            return client.SendCommand(new GetBestPossibleTime());
        }

        /// <summary>
        /// Get the best possible predicted time for the run asynchronously.
        /// </summary>
        public static async Task<TimeSpan?> GetBestPossibleTimeAsync(this IAsyncLiveSplitCommandClient client)
        {
            return await client.SendCommandAsync(new GetBestPossibleTime());
        }
    }
}
