using System;
using System.Windows.Forms;
using ags.OAuth;

namespace OAuth.UI
{
    public partial class TestForm : Form
    {
        private OAuthBroker _broker;

        public TestForm()
        {
            InitializeComponent();
            webBrowser1.DocumentCompleted += WebBrowser1_DocumentCompleted;
        }

        public TestForm(ref OAuthBroker broker) : this()
        {
            _broker = broker;
        }

        public TestForm Navigate(string url)
        {
            webBrowser1.Navigate(url);
        }
        private void WebBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}