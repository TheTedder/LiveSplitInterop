using System;
using System.Text;
using System.Threading;

namespace LiveSplitInterop
{
    /// <summary>
    /// A client that can send commands to a running LiveSplit instance.
    /// </summary>
    public interface ILiveSplitCommandClient
    {
        /// <summary>
        /// Send a command to LiveSplit.
        /// </summary>
        void SendCommand(Command command);

        /// <inheritdoc cref="SendCommand(Command)"/>
        /// <typeparam name="T">
        /// The type of the data returned.
        /// </typeparam>
        /// <returns>
        /// The data object returned by LiveSplit.
        /// </returns>
        T SendCommand<T>(Command<T> command);
    }
}
