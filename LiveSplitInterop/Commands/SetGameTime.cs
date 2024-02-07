using System;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop.Commands
{
    public sealed class SetGameTime : Command
    {
        private readonly TimeSpan gameTime;

        public SetGameTime(TimeSpan gameTime)
        {
            this.gameTime = gameTime;
        }

        public override string Message => gameTime.ToLiveSplitString();
    }

    public static class SetGameTimeExtensions
    {
        public static void SetGameTime(this ILiveSplitCommandClient client, TimeSpan gameTime)
        {
            client.SendCommand(new SetGameTime(gameTime));
        }

        public static async Task SetGameTimeAsync(this IAsyncLiveSplitCommandClient client, TimeSpan gameTime)
        {
            await client.SendCommandAsync(new SetGameTime(gameTime));
        }
    }
}
