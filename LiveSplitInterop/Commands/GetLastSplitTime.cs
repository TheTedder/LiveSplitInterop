using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop.Commands
{
    /// <summary>
    /// A command that returns the previous split time.
    /// </summary>
    /// <remarks>
    /// Uses current timing method. Returns null if the split index is -1 (timer not running) or 0.
    /// </remarks>
    public sealed class GetLastSplitTime : Command<TimeSpan?>
    {
        public override string Message => "getlastsplittime";
        public override TimeSpan? ParseResponse(string response) => Util.ParseLsTimeSpanNullable(response);
    }

    public static class GetLastSplitTimeExtensions
    {
        /// <summary>
        /// Get the previous split time.
        /// </summary>
        /// <remarks>
        /// Uses current timing method. Returns null if the split index is -1 (timer not running) or 0.
        /// </remarks>
        public static TimeSpan? GetLastSplitTime(this ILiveSplitCommandClient client)
        {
            return client.SendCommand(new GetLastSplitTime());
        }

        /// <summary>
        /// Get the previous split time asynchronously.
        /// </summary>
        /// <remarks>
        /// Uses current timing method. Returns null if the split index is -1 (timer not running) or 0.
        /// </remarks>
        public static async Task<TimeSpan?> GetLastSplitTimeAsync(this IAsyncLiveSplitCommandClient client)
        {
            return await client.SendCommandAsync(new GetLastSplitTime());
        }
    }
}
