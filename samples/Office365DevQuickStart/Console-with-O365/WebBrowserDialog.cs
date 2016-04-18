using System.Windows.Forms;

namespace Console_with_O365
{
    public class WebBrowserDialog
    {
        private const int DEFAULT_WIDTH = 400;

        private const int DEFAULT_HEIGHT = 500;

        private Form _displayLoginForm;

        private string _title;

        private WebBrowser _browser;

        private WebBrowserNavigatedEventHandler _webBrowserNavigatedEventHandler;

        public WebBrowserDialog()
        {
            _title = "Client Side Web Browser";

            _browser = new WebBrowser();

            _browser.Width = DEFAULT_WIDTH;

            _browser.Height = DEFAULT_HEIGHT;

            _browser.Navigated += WebBrowserNavigatedEventHandler;

            _browser.Navigating += _browser_Navigating;

            _displayLoginForm = new Form();

            _displayLoginForm.SuspendLayout();

            _displayLoginForm.Width = DEFAULT_WIDTH;

            _displayLoginForm.Height = DEFAULT_HEIGHT;

            _displayLoginForm.Text = _title;

            _displayLoginForm.Controls.Add(_browser);

            _displayLoginForm.ResumeLayout(false);
        }

        private void _browser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            System.Console.WriteLine(e.Url);
        }

        public void OnNavigated(WebBrowserNavigatedEventHandler handler)
        {
            _webBrowserNavigatedEventHandler = handler;
        }

        protected void WebBrowserNavigatedEventHandler(object sender, WebBrowserNavigatedEventArgs e)
        {
            System.Console.WriteLine(e.Url);

            if (_webBrowserNavigatedEventHandler != null)
            {
                _webBrowserNavigatedEventHandler.Invoke(sender, e);
            }
        }

        public void Open(string url)
        {
            _browser.Navigate(url);

            _displayLoginForm.ShowDialog();
        }

        public void Close()
        {
            _displayLoginForm.Close();
        }
    }
}
