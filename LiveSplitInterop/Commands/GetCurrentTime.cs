using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop.Commands
{
    /// <summary>
    /// A command that gets the current time for the current timing method.
    /// </summary>
    public sealed class GetCurrentTime : Command<TimeSpan>
    {
        public override string Message => "getcurrenttime";

        public override TimeSpan ParseResponse(string response) => Util.ParseLsTimeSpan(response);
    }

    public static class GetCurrentTimeExtensions
    {
        /// <summary>
        /// Get the current time for the current timing method.
        /// </summary>
        public static TimeSpan GetCurrentTime(this ILiveSplitCommandClient client)
        {
            return client.SendCommand(new GetCurrentTime());
        }

        /// <summary>
        /// Get the current time for the current timing method asynchronously.
        /// </summary>
        public static async Task<TimeSpan> GetCurrentTimeAsync(this IAsyncLiveSplitCommandClient client)
        {
            return await client.SendCommandAsync(new GetCurrentTime());
        }
    }
}
