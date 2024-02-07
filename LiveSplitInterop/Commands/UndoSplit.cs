using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop.Commands
{
    /// <summary>
    /// A command that undoes the last split.
    /// </summary>
    public sealed class UndoSplit : Command
    {
        public override string Message => "undosplit";
    }

    public static class UndoSplitExtensions
    {
        /// <summary>
        /// Undo the last split.
        /// </summary>
        public static void UndoSplit(this ILiveSplitCommandClient client)
        {
            client.SendCommand(new UndoSplit());
        }

        /// <summary>
        /// Undo the last split asynchronously.
        /// </summary>
        public static async Task UndoSplitAsync(this IAsyncLiveSplitCommandClient client)
        {
            await client.SendCommandAsync(new UndoSplit());
        }
    }
}
