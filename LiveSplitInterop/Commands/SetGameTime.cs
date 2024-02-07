using System;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop.Commands
{
    /// <summary>
    /// A command that sets the game time.
    /// </summary>
    public sealed class SetGameTime : Command
    {
        private readonly TimeSpan gameTime;

        /// <summary>
        /// Constructs a new <see cref="SetGameTime"/>.
        /// </summary>
        /// <param name="gameTime">
        /// The amount of time that has elapsed since the run started, not including loading times.
        /// </param>
        public SetGameTime(TimeSpan gameTime)
        {
            this.gameTime = gameTime;
        }

        public override string Message => gameTime.ToLiveSplitString();
    }

    public static class SetGameTimeExtensions
    {
        /// <summary>
        /// Set the game time.
        /// </summary>
        public static void SetGameTime(this ILiveSplitCommandClient client, TimeSpan gameTime)
        {
            client.SendCommand(new SetGameTime(gameTime));
        }

        /// <summary>
        /// Set the game time asynchronously.
        /// </summary>
        public static async Task SetGameTimeAsync(this IAsyncLiveSplitCommandClient client, TimeSpan gameTime)
        {
            await client.SendCommandAsync(new SetGameTime(gameTime));
        }
    }
}
