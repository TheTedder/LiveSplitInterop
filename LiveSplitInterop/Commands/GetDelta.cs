using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop.Commands
{
    /// <summary>
    /// A command that returns the delta for the last split against the current comparison.
    /// </summary>
    public sealed class GetDelta : Command<TimeSpan?>
    {
        public override string Message => "getdelta";

        public override TimeSpan? ParseResponse(string response)
        {
            return Util.ParseLiveSplitTimeSpanNullable(response);
        }
    }

    public static class GetDeltaExtensions
    {
        public static TimeSpan? GetDelta(this ILiveSplitCommandClient client)
        {
            return client.SendCommand(new GetDelta());
        }

        public static async Task<TimeSpan?> GetDeltaAsync(this IAsyncLiveSplitCommandClient client)
        {
            return await client.SendCommandAsync(new GetDelta());
        }
    }
}
