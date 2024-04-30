using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop.Commands
{
    /// <summary>
    /// A command that gets the current <see cref="TimerPhase"/>.
    /// </summary>
    public sealed class GetTimerPhase : Command<TimerPhase>
    {
        public override string Message => "gettimerphase";

        public override TimerPhase ParseResponse(string response) => (TimerPhase)Enum.Parse(typeof(TimerPhase), response);
    }

    public static class GetTimerPhaseExtensions
    {
        /// <summary>
        /// Get the current timer phase.
        /// </summary>
        public static TimerPhase GetTimerPhase(this ILiveSplitCommandClient client)
        {
            return client.SendCommand(new GetTimerPhase());
        }

        /// <summary>
        /// Get the current timer phase asynchronously.
        /// </summary>
        public static async Task<TimerPhase> GetTimerPhaseAsync(this IAsyncLiveSplitCommandClient client)
        {
            return await client.SendCommandAsync(new GetTimerPhase());
        }
    }
}
