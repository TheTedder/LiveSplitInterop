using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop.Commands
{
    /// <summary>
    /// A command that sets the current comparison.
    /// </summary>
    public sealed class SetComparison : Command
    {
        private readonly string comparison;
        public override string Message => $"setcomparison {comparison}";

        public SetComparison(string comp)
        {
            if (string.IsNullOrWhiteSpace(comp))
            {
                throw new ArgumentException("Comparison name cannot be empty", nameof(comp));
            }

            comparison = comp;
        }
    }

    public static class SetComparisonExtensions
    {
        /// <summary>
        /// Set the current comparison.
        /// </summary>
        public static void SetComparison(this ILiveSplitCommandClient client, string comparison)
        {
            client.SendCommand(new SetComparison(comparison));
        }

        /// <summary>
        /// Set the current comparison asynchronously.
        /// </summary>
        public static async Task SetComparisonAsync(
            this IAsyncLiveSplitCommandClient client,
            string comparison)
        {
            await client.SendCommandAsync(new SetComparison(comparison));
        }
    }
}
