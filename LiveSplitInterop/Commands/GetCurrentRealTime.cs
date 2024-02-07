using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop.Commands
{
    public sealed class GetCurrentRealTime : Command<TimeSpan>
    {
        public override string Message => "getcurrentrealtime";

        public override TimeSpan ParseResponse(string response) => Util.ParseLiveSplitTimeSpan(response);
    }

    public static class GetCurrentRealTimeExtensions
    {
        public static TimeSpan GetCurrentRealTime(this ILiveSplitCommandClient client)
        {
            return client.SendCommand(new GetCurrentRealTime());
        }

        public static async Task<TimeSpan> GetCurrentRealTimeAsync(this IAsyncLiveSplitCommandClient client)
        {
            return await client.SendCommandAsync(new GetCurrentRealTime());
        }
    }
}
