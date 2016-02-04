using OAuth.UI;

namespace OAuth
{
    public partial class OAuthBroker
    {
        public class Authorization
        {
            private UserAuthForm form;

            private Authorization()
            {
                form = new UserAuthForm();
            }

            public static void UIAuth(ref OAuthBroker broker)
            {
               
            }
        }
    }
}
