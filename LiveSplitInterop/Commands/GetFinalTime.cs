using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop.Commands
{
    /// <summary>
    /// A command that returns the final time if the run is over or the time of the most recently completed split,
    /// optionally of a specific comparison.
    /// </summary>
    public sealed class GetFinalTime : Command<TimeSpan?>
    {
        private readonly string comparison;
        public override string Message => $"getfinaltime{comparison?.Insert(0, " ")}";

        /// <summary>
        /// Constructs a new <see cref="GetFinalTime"/>. Defaults to null for the current comparison.
        /// </summary>
        /// <param name="comp">
        /// The name of the comparison to use.
        /// Specifying null uses the current comparison.
        /// </param>
        public GetFinalTime(string comp = null)
        {
            comparison = comp;
        }

        public override TimeSpan? ParseResponse(string response) => Util.ParseLsTimeSpanNullable(response);
    }

    public static class GetFinalTimeExtensions
    {
        /// <summary>
        /// Get the final time or the time of the most recently completed split, optionally of a specific comparison.
        /// Omit <paramref name="comparison"/> to use the current comparison.
        /// </summary>
        public static TimeSpan? GetFinalTime(this ILiveSplitCommandClient client, string comparison = null)
        {
            return client.SendCommand(new GetFinalTime(comparison));
        }

        /// <summary>
        /// Get the final time or the time of the most recently completed split asynchronously,
        /// optionally of a specific comparison.
        /// </summary>
        public static async Task<TimeSpan?> GetFinalTimeAsync(
            this IAsyncLiveSplitCommandClient client,
            string comparison = null)
        {
            return await client.SendCommandAsync(new GetFinalTime(comparison));
        }
    }
}
