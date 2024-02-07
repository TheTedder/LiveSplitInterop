using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LiveSplitInterop.Clients
{
    public class NamedPipeCommandClient : IDisposable, ILiveSplitCommandClient, IAsyncLiveSplitCommandClient
    {
        /// <summary>
        /// The underlying NamedPipeClientStream
        /// </summary>
        protected readonly NamedPipeClientStream NamedPipeClient;
        protected StreamReader Reader;
        protected StreamWriter Writer;

        /// <summary>
        ///     Creates a new LiveSplitClient for communicating with the specified LiveSplit
        ///     server. Use the Connect or the ConnectAsync method to initiate a connection
        ///     before sending any commands.
        /// </summary>
        /// <param name="serverName">
        ///     The address of the server to connect to. It defaults to "." for connections
        ///     on the local machine. This must be a computer name and not an IP address.
        /// </param>
        public NamedPipeCommandClient(string serverName = ".")
        {
            NamedPipeClient = new NamedPipeClientStream(
                serverName,
                "LiveSplit",
                PipeDirection.InOut,
                // TODO: Is PipeOptions.WriteThrough necessary? Better to have it on or off? 
                PipeOptions.Asynchronous | PipeOptions.WriteThrough);
        }

        protected void Setup()
        {
            NamedPipeClient.ReadMode = PipeTransmissionMode.Byte;
            Reader?.Dispose();
            Writer?.Dispose();
            Reader = new StreamReader(NamedPipeClient, Encoding.UTF8, false, 1024, true);
            Writer = new StreamWriter(NamedPipeClient, Encoding.UTF8, 1024, true)
            {
                NewLine = "\n"
            };
        }

        public void Connect(int timeout = Timeout.Infinite)
        {
            NamedPipeClient.Connect(timeout);
            Setup();
        }
        public async Task ConnectAsync(int timeout = Timeout.Infinite)
        {
            await NamedPipeClient.ConnectAsync(timeout);
            Setup();
        }
        public async Task ConnectAsync(
            CancellationToken cancellationToken,
            int timeout = Timeout.Infinite
            )
        {
            await NamedPipeClient.ConnectAsync(timeout, cancellationToken);
            Setup();
        }

        public void Dispose()
        {
            Reader?.Dispose();
            Writer?.Dispose();
            NamedPipeClient?.Dispose();
        }

        public bool IsConnected => NamedPipeClient.IsConnected;

        public void SendCommand(Command command)
        {
            string msg = command.Message;
            Writer.WriteLine(msg);
            Writer.Flush();
        }

        public T SendCommand<T>(Command<T> command)
        {
            SendCommand((Command)command);
            string response = Reader.ReadLine();
            Debug.WriteLine(response);
            return command.ParseResponse(response);
        }

        public async Task SendCommandAsync(Command command)
        {
            string msg = command.Message;
            Debug.WriteLine(msg);
            await Writer.WriteLineAsync(msg);
            await Writer.FlushAsync();
        }

        public async Task<T> SendCommandAsync<T>(Command<T> command)
        {
            await SendCommandAsync((Command)command);
            T response = command.ParseResponse(await Reader.ReadLineAsync());
            return response;
        }
    }
}
