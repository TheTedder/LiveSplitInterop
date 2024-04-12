using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop.Commands
{
    /// <summary>
    /// A command that returns the current split name. Returns <c>-</c> if the timer state is ended or not running.
    /// </summary>
    public sealed class GetCurrentSplitName : Command<string>
    {
        public override string Message => "getcurrentsplitname";

        public override string ParseResponse(string response) => response;
    }

    public static class GetCurrentSplitNameExtensions
    {
        public static string GetCurrentSplitName(this ILiveSplitCommandClient client)
        {
            return client.SendCommand(new GetCurrentSplitName());
        }

        public static async Task<string> GetCurrentSplitNameAsync(this IAsyncLiveSplitCommandClient client)
        {
            return await client.SendCommandAsync(new GetCurrentSplitName());
        }
    }
}
