using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Threading.Tasks;

namespace LiveSplitInterop.Commands
{
    /// <summary>
    /// A command that returns the current split index. Returns -1 when the timer is not running.
    /// </summary>
    public sealed class GetSplitIndex : Command<int>
    {
        public override string Message => "getsplitindex";

        public override int ParseResponse(string response)
        {
            return int.Parse(
                response,
                NumberStyles.AllowLeadingSign | NumberStyles.AllowTrailingWhite);
        }
    }

    public static class GetSplitIndexExtensions
    {
        /// <summary>
        /// Get the current split index. Returns -1 when the timer is not running.
        /// </summary>
        public static int GetSplitIndex(this ILiveSplitCommandClient client)
        {
            return client.SendCommand(new GetSplitIndex());
        }

        /// <summary>
        /// Get the current split index asynchronously. Returns -1 when the timer is not running.
        /// </summary>
        public static async Task<int> GetSplitIndexAsync(this IAsyncLiveSplitCommandClient client)
        {
            return await client.SendCommandAsync(new GetSplitIndex());
        }
    }
}
