using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop.Commands
{
    /// <summary>
    /// A command that returns the current realtime.
    /// </summary>
    public sealed class GetCurrentRealTime : Command<TimeSpan>
    {
        public override string Message => "getcurrentrealtime";

        public override TimeSpan ParseResponse(string response) => Util.ParseLiveSplitTimeSpan(response);
    }

    public static class GetCurrentRealTimeExtensions
    {
        /// <summary>
        /// Get the current realtime as a <see cref="TimeSpan"/>.
        /// </summary>
        public static TimeSpan GetCurrentRealTime(this ILiveSplitCommandClient client)
        {
            return client.SendCommand(new GetCurrentRealTime());
        }

        /// <summary>
        /// Get the current realtime as a <see cref="TimeSpan"/> asynchronously.
        /// </summary>
        public static async Task<TimeSpan> GetCurrentRealTimeAsync(this IAsyncLiveSplitCommandClient client)
        {
            return await client.SendCommandAsync(new GetCurrentRealTime());
        }
    }
}
