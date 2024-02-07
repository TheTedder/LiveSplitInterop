using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LiveSplitInterop.Commands
{
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
        public static int GetSplitIndex(this ILiveSplitCommandClient client)
        {
            return client.SendCommand(new GetSplitIndex());
        }

        public static async Task<int> GetSplitIndexAsync(this IAsyncLiveSplitCommandClient client)
        {
            return await client.SendCommandAsync(new GetSplitIndex());
        }
    }
}
