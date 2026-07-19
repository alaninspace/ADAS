using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

class Program
{
    static async Task Main(string[] args)
    {
        var hubConnection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5035/chathub")
            .Build();

        hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
        {
            Console.WriteLine($"[Message] {user}: {message}");
        });

        hubConnection.On<string>("ReceiveToken", (token) =>
        {
            Console.Write(token);
        });

        hubConnection.On<object>("ReceiveError", (problem) =>
        {
            Console.WriteLine($"\n[ERROR] {problem}");
        });

        var tcs = new TaskCompletionSource();

        hubConnection.On("StreamComplete", () => 
        {
            Console.WriteLine("\n[StreamComplete]");
            tcs.SetResult();
        });

        await hubConnection.StartAsync();
        Console.WriteLine("Connected to Hub.");
        
        await hubConnection.SendAsync("StreamMessage", "Hello, who are you?", "openai/gpt-5.6-terra", "GeneralAgent");
        
        await Task.WhenAny(tcs.Task, Task.Delay(15000));
    }
}
