using System;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop.Commands
{
    public sealed class Split : Command
    {
        public override string Message => "split";
    }

    public static class SplitExtensions
    {
        public static void Split(this ILiveSplitCommandClient client)
        {
            client.SendCommand(new Split());
        }

        public static async Task SplitAsync(this IAsyncLiveSplitCommandClient client)
        {
            await client.SendCommandAsync(new Split());
        }
    }
}
