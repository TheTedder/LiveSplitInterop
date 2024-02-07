using System;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop.Commands
{
    public sealed class StartOrSplit : Command
    {
        public override string Message => "startorsplit";
    }

    public static class StartOrSplitExtensions
    {
        public static void StartOrSplit(this ILiveSplitCommandClient client)
        {
            client.SendCommand(new StartOrSplit());
        }

        public static async Task StartOrSplitAsync(this IAsyncLiveSplitCommandClient client)
        {
            await client.SendCommandAsync(new StartOrSplit());
        }
    }
}

