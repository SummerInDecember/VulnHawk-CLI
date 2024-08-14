using System;
using Terminal.Gui;

// I absolutely hate the top level statements :(((((((

Application.Run<MainWindow>();

Application.Shutdown();
class MainWindow : Window
{
    public MainWindow()
    {
        Title = "VulnHawk - CLI version";
    }
}
