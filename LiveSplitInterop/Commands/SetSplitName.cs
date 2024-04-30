using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop.Commands
{
    /// <summary>
    /// A command that sets the name of the split at the specified index.
    /// </summary>
    public sealed class SetSplitName : Command
    {
        /// <summary>
        /// The index of the split to change the name of.
        /// </summary>
        public int Index { get; private set; }

        /// <summary>
        /// The name of the split. Cannot be empty.
        /// </summary>
        public string Name { get; private set; }
        
        /// <inheritdoc/>
        public override string Message => $"setsplitname {Index} {Name}";

        /// <summary>
        /// Construct a new <see cref="SetSplitName"/>.
        /// </summary>
        /// <remarks>
        /// <paramref name="i"/> cannot be less than zero or greater than or equal to the total number of splits.
        /// <paramref name="n"/> cannot be empty.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public SetSplitName(int i, string n)
        {
            if (i < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(i), i, "Split index cannot be less than zero");
            }

            if (string.IsNullOrWhiteSpace(n))
            {
                throw new ArgumentException("Split name cannot be empty", nameof(n));
            }

            Index = i;
            Name = n;
        }
    }

    public static class SetSplitNameExtensions
    {
        /// <summary>
        /// Set the name of the split at <paramref name="index"/> to <paramref name="name"/>.
        /// </summary>
        public static void SetSplitName(this ILiveSplitCommandClient client, int index, string name)
        {
            client.SendCommand(new SetSplitName(index, name));
        }

        /// <summary>
        /// Set the name of the split at <paramref name="index"/> to <paramref name="name"/> asynchronously.
        /// </summary>
        public static async Task SetSplitNameAsync(this IAsyncLiveSplitCommandClient client, int index, string name)
        {
            await client.SendCommandAsync(new SetSplitName(index, name));
        }
    }
}
