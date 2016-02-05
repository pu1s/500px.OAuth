using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OAuth;

namespace ConsoleApplicationTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Authorize();
        }

        public static void Authorize()
        {
            var broker = new OAuthBroker().RegisterClient(new OAuth.OAuthBroker.ConsumerInfo("dYhyGEEA4OT6wR4LJn7NC5bhhUP2ISaBxw234CiW",
                "N4Gi8a5x8yQ8TuHnKOqPYroeRN63Mmh9k1l5QVxA", "http://www.bing.com"));
            Authorization.RealiseToken(broker);
        }
    }

}
