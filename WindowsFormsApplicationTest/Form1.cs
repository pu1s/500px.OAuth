using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OAuth;
using OAuth.UI;
using OAuthBroker = OAuth.OAuthBroker;


namespace WindowsFormsApplicationTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var broker = new OAuthBroker().RegisterClient(new OAuth.OAuthBroker.ConsumerInfo("dYhyGEEA4OT6wR4LJn7NC5bhhUP2ISaBxw234CiW",
                "N4Gi8a5x8yQ8TuHnKOqPYroeRN63Mmh9k1l5QVxA", "http://www.bing.com"));
            Authorization.RealiseToken(broker);
            
        }
     
    }
}
