using System;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop.Commands
{
    /// <summary>
    /// A command that causes the timer to split.
    /// </summary>
    public sealed class Split : Command
    {
        public override string Message => "split";
    }

    public static class SplitExtensions
    {
        /// <summary>
        /// Make the timer split.
        /// </summary>
        public static void Split(this ILiveSplitCommandClient client)
        {
            client.SendCommand(new Split());
        }

        /// <summary>
        /// Make the timer split asynchronously.
        /// </summary>
        public static async Task SplitAsync(this IAsyncLiveSplitCommandClient client)
        {
            await client.SendCommandAsync(new Split());
        }
    }
}
