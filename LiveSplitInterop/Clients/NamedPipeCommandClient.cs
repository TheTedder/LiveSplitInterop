using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LiveSplitInterop.Clients
{
    /// <summary>
    /// A client that connects to LiveSplit over a <see href="https://en.wikipedia.org/wiki/Named_pipe">
    /// named pipe</see>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The LiveSplit named pipe is always open, so this client can be used to control LiveSplit without
    /// any prior setup by the user.
    /// </para>
    /// <para>
    /// This client can be used over LAN but it is not very efficient. It is recommended to use TCP/IP instead.
    /// </para>
    /// </remarks>
    public class NamedPipeCommandClient : IDisposable, ILiveSplitCommandClient, IAsyncLiveSplitCommandClient
    {
        /// <summary>
        /// The underlying NamedPipeClientStream.
        /// </summary>
        protected readonly NamedPipeClientStream NamedPipeClient;

        /// <summary>
        /// The <see cref="StreamReader"/> used to read data from the <see cref="NamedPipeClient"/>.
        /// This value may be null if the client is not currently connected to a LiveSplit instance.
        /// </summary>
        protected StreamReader Reader;

        /// <summary>
        /// The <see cref="StreamWriter"/> used to write data to the <see cref="NamedPipeClient"/>.
        /// This value may be null if the client is not currently connected to a LiveSplit instance.
        /// </summary>
        protected StreamWriter Writer;

        /// <summary>
        /// Creates a new <see cref="NamedPipeCommandClient"/> for communicating with the instance of LiveSplit
        /// on the specified server. Use the Connect or the ConnectAsync method to initiate a connection
        /// before sending any commands.
        /// </summary>
        /// <param name="serverName">
        /// The address of the server to connect to. It defaults to "." for connections
        /// on the local machine. This must be a computer name and not an IP address.
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

        /// <summary>
        /// Setup the client for reading and writing from the <see cref="NamedPipeClient"/>.
        /// This method can also be used to re-initialize the <see cref="Reader"/> and the <see cref="Writer"/>.
        /// </summary>
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

        /// <summary>
        /// Connect to LiveSplit.
        /// </summary>
        public void Connect(int timeout = Timeout.Infinite)
        {
            NamedPipeClient.Connect(timeout);
            Setup();
        }

        /// <summary>
        /// Connect to LiveSplit Asynchronously.
        /// </summary>
        public async Task ConnectAsync(int timeout = Timeout.Infinite)
        {
            await NamedPipeClient.ConnectAsync(timeout);
            Setup();
        }

        /// <inheritdoc cref="ConnectAsync(int)"/>
        public async Task ConnectAsync(
            CancellationToken cancellationToken,
            int timeout = Timeout.Infinite
            )
        {
            await NamedPipeClient.ConnectAsync(timeout, cancellationToken);
            Setup();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Reader?.Dispose();
            Writer?.Dispose();
            NamedPipeClient?.Dispose();
        }

        /// <summary>
        /// A value indicating whether a client is connected to LiveSplit.
        /// </summary>
        public bool IsConnected => NamedPipeClient.IsConnected;

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
