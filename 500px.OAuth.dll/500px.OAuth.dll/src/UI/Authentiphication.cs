using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ags.OAuth;

namespace OAuth.UI
{
    public static class Authentiphication
    {
        public static void Show(ref OAuthBroker broker)
        {
            
            OAuthBroker authBroker = broker;
            broker.Token =Task.Run(async () => { await authBroker.GetRequestTokenAsync(); });
            broker = authBroker;
            var form = new UserAuthForm(ref broker).Navigate(broker.AuthorizeTokenAsync());
        }
    }
}
