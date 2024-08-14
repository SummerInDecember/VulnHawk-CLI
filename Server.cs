using System;
using System.Net;
using Terminal.Gui;

namespace VulnHawk_CLI;

public class Server
{
    private int port = 5000;
    
    public Server()
    {
        if (!HttpListener.IsSupported)
        {
            MainWindow.HttpNotSupportedDiag();
        }
    }
    
    public int Port
    {
        get => port;
        set => port = value;
    }
}