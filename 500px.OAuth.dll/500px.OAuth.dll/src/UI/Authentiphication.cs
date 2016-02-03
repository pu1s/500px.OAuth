using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ags.OAuth;

namespace OAuth.UI
{
    public class Authentiphication
    {
        private UserAuthForm form;

        private Authentiphication()
        {
            form = new UserAuthForm();
        }
        public static void UIAuth(ref OAuthBroker broker)
        {
            var authBroker = broker;
            authBroker.Token = Task.Run(async () => await authBroker.GetRequestTokenAsync()).GetAwaiter().GetResult();
            var form = new UserAuthForm(authBroker).Navigate(authBroker.AuthorizeTokenAsync());
            broker = authBroker;
        }
    }
}
