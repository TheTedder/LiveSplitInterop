using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop
{
    public abstract class BaseClient : IDisposable, ILiveSplitCommandClient, IAsyncLiveSplitCommandClient
    {
        protected StreamReader Reader { get; set; }
        protected StreamWriter Writer { get; set; }
        protected Stream Stream;

        /// <summary>
        /// Sets up or reinitializes the <see cref="Reader"/> and <see cref="Writer"/>
        /// for use with <paramref name="stream"/>
        /// </summary>
        /// <param name="stream"></param>
        protected virtual void Setup(Stream stream)
        {
            Reader?.Close();
            Writer?.Close();
            Stream?.Dispose();
            Reader = new StreamReader(stream, Encoding.UTF8, false, 1024, true);
            Writer = new StreamWriter(stream, new UTF8Encoding(false), 1024, true)
            {
                NewLine = "\n"
            };

            Stream = stream;
        }

        /// <inheritdoc/>
        public virtual void Dispose()
        {
            Reader?.Dispose();
            Writer?.Dispose();
            Stream?.Dispose();
        }

        /// <inheritdoc/>
        public void SendCommand(Command command)
        {
            string msg = command.Message;
            Writer.WriteLine(msg);
            Writer.Flush();
        }

        /// <inheritdoc/>
        public T SendCommand<T>(Command<T> command)
        {
            SendCommand((Command)command);
            string response = Reader.ReadLine();
            Debug.WriteLine(response);
            return command.ParseResponse(response);
        }

        /// <inheritdoc/>
        public async Task SendCommandAsync(Command command)
        {
            string msg = command.Message;
            await Writer.WriteLineAsync(msg);
            await Writer.FlushAsync();
        }

        /// <inheritdoc/>
        public async Task<T> SendCommandAsync<T>(Command<T> command)
        {
            await SendCommandAsync((Command)command);
            string response = await Reader.ReadLineAsync();
            Debug.WriteLine(response);
            return command.ParseResponse(response);
        }

#if DEBUG
        /// <summary>
        /// Send a raw string.
        /// </summary>
        public async Task SendCommandRaw(string str)
        {
            await Writer.WriteLineAsync(str);
            await Writer.FlushAsync();
        }

        public async Task<string> ConsumeLineAsync()
        {
            return await Reader.ReadLineAsync();
        }
#endif
    }
}
