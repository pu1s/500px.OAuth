using System;
using System.Windows.Forms;
using ags.OAuth;

namespace OAuth.UI
{
    public partial class UserAuthForm : Form
    {
        private OAuthBroker _broker;

        private UserAuthForm()
        {
            InitializeComponent();
            webBrowser1.DocumentCompleted += WebBrowser1_DocumentCompleted;
        }

        public UserAuthForm(ref OAuthBroker broker) : this()
        {
            _broker = broker;
        }

        public UserAuthForm Navigate(string url)
        {
            webBrowser1.Navigate(url);
            this.ShowDialog();
            return this;
        }
        private void WebBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            
        }
    }
}