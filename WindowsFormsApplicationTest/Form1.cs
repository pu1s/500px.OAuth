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
using ags.OAuth;
using OAuth.UI;


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
            GetForm();
            await GetValue(broker);
        }

        private static async Task GetValue(OAuthBroker broker)
        {
            await broker.GetRequestTokenAsync();
            var str =broker.AuthorizeTokenAsync();
            var form = new UserAuthForm(str, ref broker);
            form.ShowDialog();
            await broker.GetAccessTokenAsync(broker.Token);
        }

        public static void GetForm()
        {
            ShowForm.Show();
        }
    }
}
