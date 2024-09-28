using System;
using System.IO;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;


string server = "127.0.0.1"; // Server IP address
int port = 7; // Server port

try
{
    using TcpClient client = new TcpClient(server, port);
    using NetworkStream stream = client.GetStream();
    using StreamReader reader = new StreamReader(stream);
    using StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };

    Console.WriteLine("Connected to server");

    // Example: Sending a "Metoder" request
    var request = new { method = "Metoder" };
    string jsonRequest = JsonSerializer.Serialize(request);
    await writer.WriteLineAsync(jsonRequest);

    // Reading the response
    string jsonResponse = await reader.ReadLineAsync();
    Console.WriteLine("Response: " + jsonResponse);

    // Example: Sending an "Add" request
    request = new { method = "Add" };
    jsonRequest = JsonSerializer.Serialize(request);
    await writer.WriteLineAsync(jsonRequest);

    // Reading the prompt for numbers
    jsonResponse = await reader.ReadLineAsync();
    Console.WriteLine("Response: " + jsonResponse);

    // Sending the numbers
    string numbers = "<5> <10>";
    await writer.WriteLineAsync(numbers);

    // Reading the result
    jsonResponse = await reader.ReadLineAsync();
    Console.WriteLine("Response: " + jsonResponse);

    // Close the connection
    client.Close();
}
catch (Exception e)
{
    Console.WriteLine("Exception: " + e.Message);
}

