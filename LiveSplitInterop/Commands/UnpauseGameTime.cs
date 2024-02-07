using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop.Commands
{
    public class UnpauseGameTime : Command
    {
        public override string Message => "unpausegametime";
    }

    public static class UnpauseGameTimeExtensions
    {
        public static void UnpauseGameTime(this ILiveSplitCommandClient client)
        {
            client.SendCommand(new UnpauseGameTime());
        }

        public static async Task UnpauseGameTimeAsync(this IAsyncLiveSplitCommandClient client)
        {
            await client.SendCommandAsync(new UnpauseGameTime());
        }
    }
}
