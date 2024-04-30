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
        /// <summary>
        /// The name of the comparison to switch to.
        /// </summary>
        public string Comparison { get; private set; }
        public override string Message => $"setcomparison {Comparison}";

        public SetComparison(string comp)
        {
            if (string.IsNullOrWhiteSpace(comp))
            {
                throw new ArgumentException("Comparison name cannot be empty", nameof(comp));
            }

            Comparison = comp;
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
