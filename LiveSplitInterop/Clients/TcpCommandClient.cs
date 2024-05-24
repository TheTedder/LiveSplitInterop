using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop.Clients
{
    /// <summary>
    /// A client that connects to LiveSplit over TCP/IP.
    /// </summary>
    /// <remarks>
    /// The user must start the TCP server via LiveSplit's <b>Control</b> menu before this client can connect.
    /// </remarks>
    public class TcpCommandClient : BaseClient<NetworkStream>
    {
        /// <summary>
        /// The internal <see cref="TcpClient"/> that connects to the LiveSplit server.
        /// </summary>
        protected readonly TcpClient Client;

        /// <summary>
        /// The hostname of the machine running the LiveSplit server.
        /// </summary>
        public readonly string Host;

        /// <summary>
        /// The port to connect to on the machine running the LiveSplit server.
        /// </summary>
        /// <remarks>
        /// 16834 is the default.
        /// </remarks>
        public readonly int Port;

        /// <summary>
        /// Create a new <see cref="TcpCommandClient"/> for communicating with the instance of LiveSplit
        /// located at <paramref name="host"/> via <paramref name="port"/>.
        /// </summary>
        /// <remarks>
        /// Use the <see cref="Connect"/> or the <see cref="ConnectAsync"/> method to initiate a connection
        /// before sending any commands.
        /// </remarks>
        public TcpCommandClient(string host, int port)
        {
            Client = new TcpClient();
            Host = host;
            Port = port;
        }

        /// <inheritdoc/>
        public override bool IsConnected => Client.Connected;

        /// <summary>
        /// Connect to LiveSplit.
        /// </summary>
        public void Connect()
        {
            Client.Connect(Host, Port);
            Setup(Client.GetStream());
        }

        /// <summary>
        /// Connect to LiveSplit asynchronously.
        /// </summary>
        public async Task ConnectAsync()
        {
            await Client.ConnectAsync(Host, Port);
            Setup(Client.GetStream());
        }

        /// <summary>
        /// Close the internal client.
        /// </summary>
        public void Close() => Client.Close();

        /// <inheritdoc/>
        public override void Dispose()
        {
            base.Dispose();
            Client.Dispose();
        }
    }
}
