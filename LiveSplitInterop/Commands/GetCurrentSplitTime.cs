using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop.Commands
{
    /// <summary>
    /// A command that gets the current split time, optionally of a specific comparison.
    /// </summary>
    /// <remarks>
    /// Returns null is there is no current split.
    /// </remarks>
    public sealed class GetCurrentSplitTime : Command<TimeSpan?>
    {
        private readonly string comparison;
        public override string Message => $"getcurrentsplittime{comparison?.Insert(0, " ")}";

        /// <summary>
        /// Construct a new <see cref="GetCurrentSplitTime"/>. Defaults to null for the current comparison.
        /// </summary>
        /// <param name="comp">
        /// The name of the comparison to get the split time of.
        /// Specifying null uses the current comparison.
        /// </param>
        public GetCurrentSplitTime(string comp = null)
        {
            comparison = comp;
        }

        public override TimeSpan? ParseResponse(string response) => Util.ParseLsTimeSpanNullable(response);
    }

    public static class GetCurrentSplitTimeExtensions
    {
        /// <summary>
        /// Get the current split time. Omit <paramref name="comparison"/> to use the current comparison.
        /// </summary>
        /// <remarks>
        /// Returns null is there is no current split.
        /// </remarks>
        public static TimeSpan? GetCurrentSplitTime(this ILiveSplitCommandClient client, string comparison = null)
        {
            return client.SendCommand(new GetCurrentSplitTime(comparison));
        }

        /// <summary>
        /// Get the current split time asynchronously. Omit <paramref name="comparison"/> to use the current comparison.
        /// </summary>
        /// <inheritdoc cref="GetCurrentSplitTime(ILiveSplitCommandClient, string)"/>
        public static async Task<TimeSpan?> GetCurrentSplitTimeAsync(
            this IAsyncLiveSplitCommandClient client,
            string comparison = null)
        {
            return await client.SendCommandAsync(new GetCurrentSplitTime(comparison));
        }
    }
}
