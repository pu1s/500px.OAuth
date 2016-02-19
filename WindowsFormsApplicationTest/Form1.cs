using System.Windows.Forms;
using OAuth;

namespace WindowsFormsApplicationTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           
        }

        private static void Authorization(OAuthBroker broker)
        {
            
            OAuth.Authorization.RealiseToken(ref broker);
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            var cK = string.Empty;
            var cS = string.Empty;
            var cU = string.Empty;
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Поле не заполнено...");
            }
            else
                cK = textBox1.Text;
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Поле не заполнено...");
            }
            else
                cS = textBox2.Text;
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Поле не заполнено...");
            }
            else
                cU = textBox3.Text;

            var consumer = new OAuthBroker.ConsumerInfo(cK, cS, cU);
            var broker = new OAuthBroker().RegisterClient(consumer); 
            Authorization(broker);
            textBox4.Text = broker.AccessToken.Token;
            textBox5.Text = broker.AccessToken.Secret;
        }
    }
}