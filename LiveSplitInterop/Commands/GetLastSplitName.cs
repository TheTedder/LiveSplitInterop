using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop.Commands
{
    /// <summary>
    /// A command that gets the name of the previous split.
    /// </summary>
    /// <remarks>
    /// Returns <c>-</c> if there is no previous split.
    /// </remarks>
    public sealed class GetLastSplitName : Command<string>
    {
        public override string Message => $"getlastsplitname";
        public override string ParseResponse(string response) => response;
    }

    public static class GetLastSplitNameExtensions
    {
        /// <summary>
        /// Get the previous split name.
        /// </summary>
        public static string GetLastSplitName(this ILiveSplitCommandClient client)
        {
            return client.SendCommand(new GetLastSplitName());
        }

        /// <summary>
        /// Get the previous split name asynchronously.
        /// </summary>
        public static async Task<string> GetLastSplitNameAsync(this IAsyncLiveSplitCommandClient client)
        {
            return await client.SendCommandAsync(new GetLastSplitName());
        }
    }
}
