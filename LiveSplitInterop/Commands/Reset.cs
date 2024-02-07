using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop.Commands
{
    public sealed class Reset : Command
    {
        public override string Message => "reset";
    }

    public static class ResetExtensions
    {
        public static void Reset(this ILiveSplitCommandClient client)
        {
            client.SendCommand(new Reset());
        }

        public static async Task ResetAsync(this IAsyncLiveSplitCommandClient client)
        {
            await client.SendCommandAsync(new Reset());
        }
    }
}
