using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ags.OAuth;

namespace OAuth.UI
{
    public static class ShowForm
    {
        public static void Show(ref OAuthBroker broker)
        {
            var form = new TestForm(ref broker).Navigate(broker.AuthorizeTokenAsync());
        }
    }
}
