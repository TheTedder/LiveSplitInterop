using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop.Commands
{
    /// <summary>
    /// A command that sets the current split name.
    /// </summary>
    public sealed class SetCurrentSplitName : Command
    {
        /// <summary>
        /// The name of the split. Cannot be empty.
        /// </summary>
        public string Name { get; private set; }

        public override string Message => $"setcurrentsplitname {Name}";

        /// <summary>
        /// Construct a new <see cref="SetCurrentSplitName"/>.
        /// </summary>
        /// <remarks>
        /// <paramref name="n"/> cannot be empty.
        /// </remarks>
        /// <exception cref="ArgumentException"></exception>
        public SetCurrentSplitName(string n)
        {
            if (string.IsNullOrWhiteSpace(n))
            {
                throw new ArgumentException("Split name cannot be empty", nameof(n));
            }

            Name = n;
        }
    }

    public static class SetCurrentSplitNameExtensions
    {
        /// <summary>
        /// Set the current split name.
        /// </summary>
        public static void SetCurrentSplitName(this ILiveSplitCommandClient client, string name)
        {
            client.SendCommand(new SetCurrentSplitName(name));
        }

        /// <summary>
        /// Set the current split name asynchronously.
        /// </summary>
        public static async Task SetCurrentSplitName(this IAsyncLiveSplitCommandClient client, string name)
        {
            await client.SendCommandAsync(new SetCurrentSplitName(name));
        }
    }
}
