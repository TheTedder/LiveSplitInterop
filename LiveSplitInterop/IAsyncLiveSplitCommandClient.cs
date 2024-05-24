using System.Threading.Tasks;

namespace LiveSplitInterop
{
    /// <summary>
    /// A client that can send commands asynchronously to a running LiveSplit instance.
    /// </summary>
    public interface IAsyncLiveSplitCommandClient
    {
        /// <summary>
        /// Send a command to LiveSplit asynchronously.
        /// </summary>
        Task SendCommandAsync(Command command);

        /// <inheritdoc cref="SendCommandAsync(Command)"/>
        /// <typeparam name="T">
        /// The type of the data returned.
        /// </typeparam>
        /// <returns>
        /// A <see cref="Task"/> representing an asynchronous operation that returns
        /// the data object returned by LiveSplit.
        /// </returns>
        Task<T> SendCommandAsync<T>(Command<T> command);
    }
}
