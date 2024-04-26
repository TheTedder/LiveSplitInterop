using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplitInterop.Clients
{
    public class TcpCommandClient : BaseClient
    {
        protected readonly TcpClient Client;
        protected readonly string Host;
        protected readonly int Port;

        public TcpCommandClient(string host, int port)
        {
            Client = new TcpClient();
            Host = host;
            Port = port;
        }

        public bool Connected => Client.Connected;

        public void Connect()
        {
            Client.Connect(Host, Port);
            Setup(Client.GetStream());
        }

        public async Task ConnectAsync()
        {
            await Client.ConnectAsync(Host, Port);
            Setup(Client.GetStream());
        }

        public void Close() => Client.Close();

        public override void Dispose()
        {
            base.Dispose();
            Client.Dispose();
        }
    }
}
