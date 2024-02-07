using System;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop.Commands
{
    /// <summary>
    /// A command that starts the timer if it's not running or causes it to split if it is.
    /// </summary>
    public sealed class StartOrSplit : Command
    {
        public override string Message => "startorsplit";
    }

    public static class StartOrSplitExtensions
    {
        /// <summary>
        /// Start the timer if it's not running or make it split if it is.
        /// </summary>
        public static void StartOrSplit(this ILiveSplitCommandClient client)
        {
            client.SendCommand(new StartOrSplit());
        }

        /// <summary>
        /// Asynchronously start the timer if it's not running or make it split if it is.
        /// </summary>
        public static async Task StartOrSplitAsync(this IAsyncLiveSplitCommandClient client)
        {
            await client.SendCommandAsync(new StartOrSplit());
        }
    }
}

