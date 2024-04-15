using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop.Clients
{
    public class TcpCommandClient : IDisposable, ILiveSplitCommandClient, IAsyncLiveSplitCommandClient
    {
        protected readonly TcpClient Client;
        protected readonly string Host;
        protected readonly int Port;

        /// <summary>
        /// The <see cref="StreamReader"/> used to read data from the <see cref="Client"/>.
        /// This value may be null if the client is not currently connected to a LiveSplit instance.
        /// </summary>
        protected StreamReader Reader;

        /// <summary>
        /// The <see cref="StreamWriter"/> used to write data to the <see cref="Client"/>.
        /// This value may be null if the client is not currently connected to a LiveSplit instance.
        /// </summary>
        protected StreamWriter Writer;

        public TcpCommandClient(string host, int port)
        {
            Client = new TcpClient();
            Host = host;
            Port = port;
        }

        public bool Connected => Client.Connected;

        protected void Setup()
        {
            Reader?.Dispose();
            Writer?.Dispose();
            Reader = new StreamReader(Client.GetStream(), Encoding.UTF8, false, 1024, true);
            Writer = new StreamWriter(Client.GetStream(), Encoding.UTF8, 1024, true)
            {
                NewLine = "\n"
            };
        }

        public void Connect()
        {
            Client.Connect(Host, Port);
            Setup();
        }

        public async Task ConnectAsync()
        {
            await Client.ConnectAsync(Host, Port);
            Setup();
        }

        public void Close()
        {
            Client.Close();
        }

        public void Dispose()
        {
            Client.Dispose();
        }

        public void SendCommand(Command command)
        {
            Writer.WriteLine(command.Message);
            Writer.Flush();
        }

        public T SendCommand<T>(Command<T> command)
        {
            SendCommand((Command)command);
            string msg = Reader.ReadLine();
            return command.ParseResponse(msg);
        }

        public async Task SendCommandAsync(Command command)
        {
            await Writer.WriteLineAsync(command.Message);
            await Writer.FlushAsync();
        }

        public async Task<T> SendCommandAsync<T>(Command<T> command)
        {
            await SendCommandAsync((Command)command);
            string msg = await Reader.ReadLineAsync();
            return command.ParseResponse(msg);
        }

#if DEBUG
        public async Task SendCommandRaw(string str)
        {
            await Writer.WriteLineAsync(str);
            await Writer.FlushAsync();
        }

        public async Task<string> ConsumeLineAsync()
        {
            return Reader.EndOfStream ? null : await Reader.ReadLineAsync();
        }
#endif
    }
}
