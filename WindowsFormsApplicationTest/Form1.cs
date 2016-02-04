using System.Windows.Forms;
using OAuth;

namespace WindowsFormsApplicationTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            OAuthBroker broker;
            Authorization(out broker);
        }

        private static void Authorization(out OAuthBroker broker)
        {
            broker =
                new OAuthBroker().RegisterClient(
                    new OAuthBroker.ConsumerInfo(
                        "dYhyGEEA4OT6wR4LJn7NC5bhhUP2ISaBxw234CiW",
                        "N4Gi8a5x8yQ8TuHnKOqPYroeRN63Mmh9k1l5QVxA",
                        "http://www.bing.com"));
            OAuth.Authorization.RealiseToken(broker);
        }
    }
}