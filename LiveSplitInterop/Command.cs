using System;
using System.Text;

namespace LiveSplitInterop
{
    public abstract class Command
    {
        public abstract string Message { get; }
    }

    public abstract class Command<T> : Command
    {
        public abstract T ParseResponse(string response);
    }
}
