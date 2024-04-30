using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop.Commands
{
    /// <summary>
    /// A command that pings the server.
    /// </summary>
    /// <remarks>
    /// Responds with "pong".
    /// </remarks>
    public sealed class Ping : Command<string>
    {
        public override string Message => "ping";

        public override string ParseResponse(string response) => response;
    }

    public static class PingExtensions
    {
        /// <summary>
        /// Ping the server.
        /// </summary>
        /// <returns>"pong"</returns>
        public static string Ping(this ILiveSplitCommandClient client)
        {
            return client.SendCommand(new Ping());
        }

        /// <summary>
        /// Ping the server asynchronously.
        /// </summary>
        /// <returns>"pong"</returns>
        public static async Task<string> PingAsync(this IAsyncLiveSplitCommandClient client)
        {
            return await client.SendCommandAsync(new Ping());
        }
    }
}
