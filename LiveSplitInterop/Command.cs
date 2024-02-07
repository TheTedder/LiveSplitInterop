using System;
using System.Text;

namespace LiveSplitInterop
{
    /// <summary>
    /// A command that returns no data.
    /// </summary>
    public abstract class Command
    {
        /// <summary>
        /// The underlying message to send to LiveSplit.
        /// </summary>
        public abstract string Message { get; }
    }

    /// <summary>
    /// A command that returns a <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T">
    /// The type of data returned by the command.
    /// </typeparam>
    public abstract class Command<T> : Command
    {
        /// <summary>
        /// Parse the reponse from LiveSplit and return it as a <typeparamref name="T"/>.
        /// </summary>
        public abstract T ParseResponse(string response);
    }
}
