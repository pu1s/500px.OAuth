using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ags.OAuth;

namespace WindowsFormsApplicationTest
{
    public partial class UserAuthForm : Form
    {
        private UserAuthForm()
        {
            InitializeComponent();
            webBrowser1.DocumentCompleted += WebBrowser1_DocumentCompleted;
        }

        private readonly OAuthBroker _authBroker;
        private void WebBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            {
                if(sender is WebBrowser)
#if DEBUG
                    Debug.WriteLine("Doc coplete: {0}", e.Url);
#endif
                _authBroker.GetVerifier(e.Url);
            }
        }

        public UserAuthForm(string url, ref OAuthBroker broker):this()
        {
            webBrowser1.Navigate(url);
            _authBroker = broker;
        }

    }
}
