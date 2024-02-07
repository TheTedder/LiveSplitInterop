using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LiveSplitInterop
{
    public interface ILiveSplitCommandClient
    {
        void SendCommand(Command command);
        T SendCommand<T>(Command<T> command);
    }

    public interface IAsyncLiveSplitCommandClient
    {
        Task SendCommandAsync(Command command);
        Task<T> SendCommandAsync<T>(Command<T> command);
    }
}
