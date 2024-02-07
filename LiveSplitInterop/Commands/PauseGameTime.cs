using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop.Commands
{
    public class PauseGameTime : Command
    {
        public override string Message => "pausegametime";
    }

    public static class PauseGameTimeExtensions
    {
        public static void PauseGameTime(this ILiveSplitCommandClient client)
        {
            client.SendCommand(new PauseGameTime());
        }

        public static async Task PauseGameTimeAsync(this IAsyncLiveSplitCommandClient client)
        {
            await client.SendCommandAsync(new PauseGameTime());
        }
    }
}
