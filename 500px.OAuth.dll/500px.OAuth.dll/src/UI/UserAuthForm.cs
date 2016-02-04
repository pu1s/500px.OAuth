using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace OAuth.UI
{
    public partial class UserAuthForm : Form
    {
        private OAuthBroker _broker;
        private string _url;

        public UserAuthForm()
        {
            InitializeComponent();
            webBrowser1.DocumentCompleted += WebBrowser1_DocumentCompleted;
            webBrowser1.ScriptErrorsSuppressed = true;
        }

        public UserAuthForm(OAuthBroker broker, string url) : this()
        {
            _broker = broker;
            _url = url;
        }

        public UserAuthForm Navigate()
        {
            webBrowser1.Navigate(_url);
            ShowDialog();
            return this;
        }
        private void WebBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
#if DEBUG
            Debug.WriteLine(e.Url.OriginalString);
#endif
            _broker.GetVerifier(e.Url);
        } 
    }
}