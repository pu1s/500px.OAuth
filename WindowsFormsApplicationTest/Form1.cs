using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ags.Core;

namespace WindowsFormsApplicationTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            var broker =
                new ags.OAuth.OAuthBroker().RegisterClient(new ConsumerInfo("dYhyGEEA4OT6wR4LJn7NC5bhhUP2ISaBxw234CiW",
                    "N4Gi8a5x8yQ8TuHnKOqPYroeRN63Mmh9k1l5QVxA", "http://www.bing.com"));
            await broker.GetRequestTokenAsync();
            broker.GetAutorizeTokenAsync();
            var str = broker.GetAuthorizationUrl(broker.Token);
            var form = new UserAuthForm(str, ref broker);
            form.ShowDialog();
            await broker.GetAccessTokenAsync(broker.Token);
        }
    }
}
