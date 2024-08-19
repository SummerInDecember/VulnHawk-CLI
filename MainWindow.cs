using System.Runtime.CompilerServices;
using Terminal.Gui;

namespace VulnHawk_CLI
{
    public class MainWindow : Window
    {
        private Server serv;
        public MainWindow()
        {
            Title = "VulnHawk - CLI version";
            Task.Run(() => InitServerAsync());
            SetInitialLayout();
            Add(mainPage);
        }

        private async Task InitServerAsync()
        {
            serv = new Server();
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
        
        public override bool ProcessKey(KeyEvent keyEvent)
        {
            if (keyEvent.Key == Key.i)
            {
                serv.ToIntercept = "all";
                serv.IsIntercepted = true;
                return true; 
            }

            return base.ProcessKey(keyEvent); 
        }
    }
}

