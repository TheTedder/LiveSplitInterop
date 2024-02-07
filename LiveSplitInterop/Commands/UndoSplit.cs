using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop.Commands
{
    public sealed class UndoSplit : Command
    {
        public override string Message => "undosplit";
    }

    public static class UndoSplitExtensions
    {
        public static void UndoSplit(this ILiveSplitCommandClient client)
        {
            client.SendCommand(new UndoSplit());
        }

        public static async Task UndoSplitAsync(this IAsyncLiveSplitCommandClient client)
        {
            await client.SendCommandAsync(new UndoSplit());
        }
    }
}
