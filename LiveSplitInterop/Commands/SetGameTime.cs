﻿using System;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop.Commands
{
    /// <summary>
    /// A command that sets the game time.
    /// </summary>
    public sealed class SetGameTime : Command
    {
        public TimeSpan GameTime { get; private set; }

        public SetGameTime(TimeSpan gameTime)
        {
            GameTime = gameTime;
        }

        public override string Message => $"setgametime {GameTime.ToLsString()}";
    }

    public static class SetGameTimeExtensions
    {
        /// <summary>
        /// Set the game time.
        /// </summary>
        /// <param name="client">
        /// The client.
        /// </param>
        /// <param name="gameTime">
        /// The amount of time that has elapsed since the run started, not including loading times.
        /// </param>
        public static void SetGameTime(this ILiveSplitCommandClient client, TimeSpan gameTime)
        {
            client.SendCommand(new SetGameTime(gameTime));
        }

        /// <summary>
        /// Set the game time asynchronously.
        /// </summary>
        /// <param name="client">
        /// The client.
        /// </param>
        /// <param name="gameTime">
        /// The amount of time that has elapsed since the run started, not including loading times.
        /// </param>
        public static async Task SetGameTimeAsync(this IAsyncLiveSplitCommandClient client, TimeSpan gameTime)
        {
            await client.SendCommandAsync(new SetGameTime(gameTime));
        }
    }
}
