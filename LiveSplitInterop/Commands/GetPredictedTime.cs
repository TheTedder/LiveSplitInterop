using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop.Commands
{
    /// <summary>
    /// A command that gets the predicted final run time, optionally in relation to a specific comparison.
    /// </summary>
    public sealed class GetPredictedTime : Command<TimeSpan?>
    {
        /// <summary>
        /// The comparison to get the predicted time for.
        /// </summary>
        public string Comparison { get; private set; }
        public override string Message => $"getpredictedtime{Comparison?.Insert(0, " ")}";

        /// <summary>
        /// Construct a new <see cref="GetPredictedTime"/>. Defaults to null for the current comparison.
        /// </summary>
        /// <param name="comp">
        /// The name of the comparison to get the split time of.
        /// Specifying null uses the current comparison.
        /// </param>
        public GetPredictedTime(string comp = null)
        {
            Comparison = comp;
        }

        public override TimeSpan? ParseResponse(string response) => Util.ParseLsTimeSpanNullable(response);
    }

    public static class GetPredictedTimeExtensions
    {
        /// <summary>
        /// Get the predicted final run time. Omit <paramref name="comparison"/> to use the current comparison.
        /// </summary>
        public static TimeSpan? GetPredictedTime(this ILiveSplitCommandClient client, string comparison = null)
        {
            return client.SendCommand(new GetPredictedTime(comparison));
        }

        /// <summary>
        /// Get the predicted final run time asynchronously. Omit <paramref name="comparison"/> to use the current comparison.
        /// </summary>
        public static async Task<TimeSpan?> GetPredictedTimeAsync(
            this IAsyncLiveSplitCommandClient client,
            string comparison = null)
        {
            return await client.SendCommandAsync(new GetPredictedTime(comparison));
        }
    }
}
