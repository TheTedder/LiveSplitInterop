﻿using System;
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
    /// Use <see cref="Connect(int)"/>, <see cref="ConnectAsync(int)"/>, or
    /// <see cref="ConnectAsync(CancellationToken, int)"/> to initiate a connection before sending any
    /// commands.
    /// </para>
    /// <para>
    /// The LiveSplit named pipe is always open, so this client can be used to control LiveSplit without
    /// any prior setup by the user.
    /// </para>
    /// <para>
    /// This client can be used over LAN but it is not very efficient. Use a <see cref="TcpCommandClient"/>
    /// instead for networked communication.
    /// </para>
    /// </remarks>
    public class NamedPipeCommandClient : BaseClient<NamedPipeClientStream>
    {
        /// <summary>
        /// The hostname of the server to connect to.
        /// </summary>
        public readonly string ServerName;

        /// <summary>
        /// Create a new <see cref="NamedPipeCommandClient"/> for communicating with the instance of LiveSplit
        /// on the specified server.
        /// </summary>
        /// 
        /// <param name="serverName">
        /// The address of the server to connect to. It defaults to "." for connections on the local machine.
        /// This must be a computer name and not an IP address.
        /// </param>
        public NamedPipeCommandClient(string serverName = ".")
        {
            ServerName = serverName;
        }

        /// <summary>
        /// Create the <see cref="NamedPipeClientStream"/> for sending and receiving data from LiveSplit.
        /// </summary>
        protected NamedPipeClientStream CreateStream() => new NamedPipeClientStream(
            ServerName,
            "LiveSplit",
            PipeDirection.InOut,
            // TODO: Is PipeOptions.WriteThrough necessary? Better to have it on or off? 
            PipeOptions.Asynchronous | PipeOptions.WriteThrough);

        /// <inheritdoc/>
        protected override void Setup(NamedPipeClientStream stream)
        {
            // It is an error to set this variable before the pipe is connected.

            stream.ReadMode = PipeTransmissionMode.Byte;
            base.Setup(stream);
        }

        /// <summary>
        /// Connect to LiveSplit with a specified timeout in milliseconds.
        /// </summary>
        public void Connect(int timeout = Timeout.Infinite)
        {
            NamedPipeClientStream client = CreateStream();
            client.Connect(timeout);
            Setup(client);
        }

        /// <summary>
        /// Connect to LiveSplit asynchronously with a specified timeout in milliseconds.
        /// </summary>
        public async Task ConnectAsync(int timeout = Timeout.Infinite)
        {
            NamedPipeClientStream client = CreateStream();
            await client.ConnectAsync(timeout);
            Setup(client);
        }

        /// <inheritdoc cref="ConnectAsync(int)"/>
        public async Task ConnectAsync(
            CancellationToken cancellationToken,
            int timeout = Timeout.Infinite
            )
        {
            NamedPipeClientStream client = CreateStream();
            await client.ConnectAsync(timeout, cancellationToken);
            Setup(client);
        }

        ///<inheritdoc/>
        public override bool IsConnected => Stream?.IsConnected ?? false;
    }
}
