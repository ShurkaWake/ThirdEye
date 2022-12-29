// See https://aka.ms/new-console-template for more information

using System.Net.Http.Json;
using System.Text.Json.Serialization;


const string endpoint = "api/DeviceState";
const string baseUriString = "https://localhost:{0}";

if (args.Length < 3)
{
    Console.Error.WriteLine("You need to pass 3 parameters: SerialNumber, state(1 or 0) and port");
    return;
}

if (args[0].Length != 16)
{
    Console.Error.WriteLine("SerialNumber must contain 16 digits");
    return;
}

foreach (var ch in args[0])
{
    if (!char.IsDigit(ch))
    {
        Console.Error.WriteLine("SerialNumber must contain only digits");
        return;
    }
}

if (args[1].Length != 1)
{
    Console.Error.WriteLine("State must contain only 1 digit");
    return;
}

if (args[1] != "0" && args[1] != "1")
{
    Console.Error.WriteLine("Invalid state. Possible states are 0 or 1");
    return;
}

var serialNumber = args[0];
var state = int.Parse(args[1]) + 1;

var content = new
{
    serialNumber=serialNumber,
    state=state,
};

using var httpClient = new HttpClient();
httpClient.BaseAddress = new Uri(String.Format(baseUriString, args[2]));
while (true)
{
    try
    {
        var result = await httpClient.PostAsJsonAsync(endpoint, content);
        Console.WriteLine(result.StatusCode);
        Thread.Sleep(30_000);
    }
    catch
    {
        Console.Error.WriteLine("Unable to connect");
    }
}