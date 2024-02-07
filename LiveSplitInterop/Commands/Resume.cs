using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop.Commands
{
    public sealed class Resume : Command
    {
        public override string Message => "resume";
    }

    public static class ResumeExtensions
    {
        public static void Resume(this ILiveSplitCommandClient client)
        {
            client.SendCommand(new Resume());
        }

        public static async Task ResumeAsync(this IAsyncLiveSplitCommandClient client)
        {
            await client.SendCommandAsync(new Resume());
        }
    }
}
