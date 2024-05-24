using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop
{
    /// <summary>
    /// Abstract base class for clients implementing <see cref="ILiveSplitCommandClient"/> and <see cref="IAsyncLiveSplitCommandClient"/>.
    /// </summary>
    public abstract class BaseClient : IDisposable, ILiveSplitCommandClient, IAsyncLiveSplitCommandClient
    {
        /// <summary>
        /// The <see cref="StreamReader"/> used to read data from LiveSplit.
        /// </summary>
        /// <remarks>
        /// Ensure that <see cref="Setup(Stream)"/> has been called once before use.
        /// </remarks>
        protected StreamReader Reader { get; set; }

        /// <summary>
        /// The <see cref="StreamWriter"/> used to send data to LiveSplit.
        /// </summary>
        /// <remarks>
        /// Ensure that <see cref="Setup(Stream)"/> has been called once before use.
        /// </remarks>
        protected StreamWriter Writer { get; set; }

        /// <summary>
        /// The underlying <see cref="Stream"/> representing the flow of data to and from LiveSplit.
        /// </summary>
        /// <remarks>
        /// Ensure that <see cref="Setup(Stream)"/> has been called once before use.
        /// </remarks>
        protected Stream Stream;

        /// <summary>
        /// A <see cref="bool"/> representing whether the client is connected to LiveSplit.
        /// </summary>
        public abstract bool IsConnected { get; }

        /// <summary>
        /// Sets up or reinitializes the <see cref="Reader"/> and <see cref="Writer"/>
        /// for use with <paramref name="stream"/>.
        /// </summary>
        /// <remarks>
        /// This method should be called before attempting to send any commands.
        /// Do not call this method with the same stream twice on the same client.
        /// </remarks>
        protected virtual void Setup(Stream stream)
        {
            Reader = new StreamReader(stream, Encoding.UTF8, false);
            Writer = new StreamWriter(stream, new UTF8Encoding(false))
            {
                NewLine = "\n"
            };

            Stream = stream;
        }

        /// <inheritdoc/>
        public virtual void Dispose()
        {
            Stream?.Dispose();
        }

        /// <inheritdoc/>
        public void SendCommand(Command command)
        {
            Writer.WriteLine(command.Message);
            Writer.Flush();
        }

        /// <inheritdoc/>
        public T SendCommand<T>(Command<T> command)
        {
            SendCommand((Command)command);
            return command.ParseResponse(Reader.ReadLine());
        }

        /// <inheritdoc/>
        public async Task SendCommandAsync(Command command)
        {
            await Writer.WriteLineAsync(command.Message);
            await Writer.FlushAsync();
        }

        /// <inheritdoc/>
        public async Task<T> SendCommandAsync<T>(Command<T> command)
        {
            await SendCommandAsync((Command)command);
            string response = await Reader.ReadLineAsync();
            return command.ParseResponse(response);
        }
    }
}
