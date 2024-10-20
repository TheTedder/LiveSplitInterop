
# LiveSplitInterop

This library is used to control LiveSplit via IPC. For a complete list of supported commands, refer to the [LiveSplit Github repo](https://github.com/LiveSplit/LiveSplit?tab=readme-ov-file#the-livesplit-server). Commands are passed as newline-terminated strings. Currently, two protocols are available for sending and receiving data: named pipe and TPC/IP. While both protocols will work over LAN, the named pipe should only be used for communicating with a LiveSplit instance running on the same computer. In order to control LiveSplit over TCP, you must right click LiveSplit, go to the **Control** menu, and select **Start TCP Server**. You may need to port forward in order to use the TCP server over the internet. The port number can be changed in the LiveSplit settings menu.

## How to Use

To use one of the available LiveSplit clients, first import the `LiveSplitInterop.Clients` namespace. Make sure to import the `LiveSplitInterop` and `LiveSplitInterop.Commands` namespaces too.

```csharp
using LiveSplitInterop;
using LiveSplitInterop.Clients;
using LiveSplitInterop.Commands;
```

Pick a suitable client based on your use case (see above) and connect to a running LiveSplit instance.

```csharp
using NamedPipeCommandClient client = new();
client.Connect();

// You can also connect asynchronously and await the operation.
await client.ConnectAsync();

// A timeout in milliseconds can be specified.
await client.ConnectAsync(5000);
``` 
or
```csharp
string host = "127.0.0.1";
int port = 16834;
using TcpCommandClient client = new(host, port);
client.Connect();
```

Using the `using` statement ensures that the client is properly disposed of. That way you don't have to call `Dispose()` manually.

The TCP client may raise an exception if it fails to connect to LiveSplit. You can catch the exception and retry the connection.

```csharp
using TcpCommandClient client = new("127.0.0.1", 16834);
bool errored;
 
do
{
    errored = false;
    
    try
    {
        client.Connect();
    }
    catch (SocketException)
    {
        errored = true;
    }
} while (errored);
```

Overloads of the `ConnectAsync()` method that take a timeout raise `TimeoutException` instead.

Once the client is connected, commands can be sent using the `SendCommand()` or `SendCommandAsync()` methods.

```csharp
client.SendCommand(new StartOrSplit());

await client.SendCommandAsync(new StartOrSplit());
```

Extension methods are provided for convenience.

```csharp
client.StartOrSplit();

await client.StartOrSplitAsync();
```

Attempting to send a command before the client has connected to LiveSplit will result in an error. Use the `IsConnected` property to check on the status of the connection to LiveSplit.

Attempting to send a command after losing connection to the server will result in an `IOException`.

```csharp
try
{
    client.StartOrSplit();
}
catch (IOException)
{
    // The connection dropped.
}
```

Once the connection is lost, the client should be disposed. In order to reconnect, construct a new client.

## Known Issues
Once a connection is established, the `IsConnected` property may continue to return `true` even after losing connection to LiveSplit.

## Best Practices
Make sure you give the user the option to disable LiveSplit integration in your application, especially when using the named pipe. It also doesn't hurt to have options to individually disable specific features such as splitting, starting the timer, resetting the timer, and setting game time.
