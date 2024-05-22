using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop
{
    public abstract class BaseClient : IDisposable, ILiveSplitCommandClient, IAsyncLiveSplitCommandClient
    {
        protected StreamReader Reader { get; set; }
        protected StreamWriter Writer { get; set; }
        protected Stream Stream;
        public abstract bool IsConnected { get; }

        /// <summary>
        /// Sets up or reinitializes the <see cref="Reader"/> and <see cref="Writer"/>
        /// for use with <paramref name="stream"/>. This method should be called before
        /// attempting to send any commands.
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
