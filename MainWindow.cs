using Terminal.Gui;

namespace VulnHawk_CLI
{
    public class MainWindow : Window
    {
        private Server serv;
        public MainWindow()
        {
            Title = "VulnHawk - CLI version";
            serv = new Server();
            SetInitialLayout();
            Add(mainPage);
        }

        private void SetInitialLayout()
        {
            
        }
        
        private View mainPage = new View()
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill()
        };
    }
}

