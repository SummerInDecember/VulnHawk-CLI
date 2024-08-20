using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using Terminal.Gui;

namespace VulnHawk_CLI
{
    public class MainWindow : Window
    {
        private Server _serv;
        private int _requestCount = 0;
        private List<NetworkStream> _regRequests = new List<NetworkStream>();
        List<string> _interceptedRequests = new List<string>();
        public MainWindow()
        {
            Title = "VulnHawk - CLI version";
            Task.Run(() => InitServerAsync());
            SetInitialLayout();
            Add(mainPage);
        }

        private async Task InitServerAsync()
        {
            _serv = new Server(this);
        }
        private void SetInitialLayout()
        {
            // TODO: Implement
        }

        private View mainPage = new View()
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill()
        };

        private void ShowRequest(string? reqFirstLine)
        {
            if (reqFirstLine == null)
                MessageBox.Query("Error on function ShowRequests", "Ok");
        }
        public async Task AddBtnRequests(NetworkStream stream)
        {
            StreamReader reader = new StreamReader(stream, Encoding.ASCII);
            string? reqFirstLine = await reader.ReadLineAsync();
            
            var requestBtn = new Button(reqFirstLine)
            {
                X = Pos.Center(),
                Y = Pos.Percent(20 + 10 * _requestCount)
            };
            
            _regRequests.Add(stream);
            
            requestBtn.Clicked += () => ShowRequest(reqFirstLine);
            Add(requestBtn);
        }
        public override bool ProcessKey(KeyEvent keyEvent)
        {
            if (keyEvent.Key == Key.i)
            {
                _serv.ToIntercept = "all";
                _serv.IsIntercepted = true;
                return true; 
            }

            return base.ProcessKey(keyEvent); 
        }
    }
}

