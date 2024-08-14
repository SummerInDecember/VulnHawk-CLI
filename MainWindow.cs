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

        public static void HttpNotSupportedDiag()
        {
            var diag = new Dialog()
            {
                Title = "Error",
                Width = Dim.Percent(50),
                Height = Dim.Percent(50)
            };
            
            var message = new Label("HttpListner is not supported by this device")
            {
                X = Pos.Center(),
                Y = 1
            };

            var okBtn = new Button("Ok")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(message) + 1
            };

            okBtn.Clicked += () =>
            {
                Application.RequestStop(); 
            };
            
            diag.Add(message, okBtn);
            
            Application.Run(diag);
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

