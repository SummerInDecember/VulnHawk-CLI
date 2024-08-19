using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Terminal.Gui;

namespace VulnHawk_CLI;

public class Server
{
    private TcpClient _client;
    private TcpListener _tcpListener;
    private IPAddress _ip = IPAddress.Parse("127.0.0.1");
    private int _port = 5000;
    private string? _toIntercept = null;
    private bool _isIntercepted = false;
    public enum RequestType
    {
        HTTP,
        TCP
    }
    
    private RequestType _requestType = RequestType.HTTP;
    public Server()
    {
         Task.Run(() => StartServer());
    }
    
    private async Task StartServer()
    {
        _tcpListener = new TcpListener(_ip, _port);
        _tcpListener.Start();
        
        Console.WriteLine($"Listening on {_ip}:{_port}");
        while (true)
        {
            if (_isIntercepted)
            {
                Console.WriteLine("Intercepting");
                _client = await _tcpListener.AcceptTcpClientAsync();
                _ = Task.Run(() => ClientHandler(_client));
            }
        }
    }

    private async Task ClientHandler(TcpClient client)
    {
        if (_requestType == RequestType.HTTP)
        {
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream, Encoding.ASCII);
            string request = await reader.ReadLineAsync();

            if (string.IsNullOrWhiteSpace(request))
                return;

            string[] tokens = request.Split(' ');
            string method = tokens[0];

            if (method == "CONNECT")
            {

            }
            else
            {
                await HandleHttp(tokens, reader, stream);
            }
        }
    }

    private async Task HandleHttp(string[] tokens, StreamReader reader, NetworkStream stream)
    {
        string method = tokens[0];
        string url = tokens[1];
        string version = tokens[2];

        List<string> headers = await GetHeaders(stream);
    }

    private async Task InterceptRequest(List<string> headers, string[] tokens)
    {
        
    }
    private async Task<List<string>> GetHeaders(NetworkStream stream)
    {
        byte idx = 0;
        List<string> headers = new List<string>();
        
        using (StreamReader reader = new StreamReader(stream, Encoding.ASCII, leaveOpen: true))
        {
            string line;
            while (!string.IsNullOrWhiteSpace(line = await reader.ReadLineAsync()))
            {
                if (idx > 0)
                {
                    headers.Add(line);    
                }
                
                if (0 == idx)
                    idx++; // makes sure that it's only reading the headers
            }
        }
        
        return headers;
    }

    public int Port
    {
        get => _port;
        set => _port = value;
    }

    public bool IsIntercepted
    {
        get => _isIntercepted;
        set => _isIntercepted = value;
    }
    public string? ToIntercept
    {
        get => _toIntercept;
        set => _toIntercept = value;
    }
    public RequestType TypeRequest
    {
        set => _requestType = value;
    }
}