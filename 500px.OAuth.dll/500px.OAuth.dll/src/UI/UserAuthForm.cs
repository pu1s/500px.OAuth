using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace OAuth.UI
{
    public partial class UserAuthForm : Form
    {
        private OAuthBroker _broker;

        public UserAuthForm()
        {
            InitializeComponent();
            webBrowser1.DocumentCompleted += WebBrowser1_DocumentCompleted;
            Show();
        }

        public UserAuthForm(OAuthBroker broker) : this()
        {
            _broker = broker;
        }

        public UserAuthForm Navigate(string url)
        {
            webBrowser1.Navigate(url);
            
            return this;
        }
        private void WebBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
#if DEBUG
            Debug.WriteLine(e.Url.OriginalString);
#endif
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            
        }
    }
}