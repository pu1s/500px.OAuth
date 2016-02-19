using System.Diagnostics;
using System.Windows.Forms;

namespace OAuth.UI
{
    /// <summary>
    ///     Форма авторизации пользователя
    /// </summary>
    public partial class UserAuthForm : Form
    {
        private readonly OAuthBroker _broker;
        private readonly string _url;

        /// <summary>
        ///     конструктор формы авторизации пользователя
        /// </summary>
        public UserAuthForm()
        {
            InitializeComponent();
            webBrowser.DocumentCompleted += WebBrowserDocumentCompleted;
            webBrowser.ScriptErrorsSuppressed = true;
        }

        /// <summary>
        ///     перегруженный конструктор формы авторизации пользователя
        /// </summary>
        /// <param name="broker">
        ///     объект авторизации
        /// </param>
        /// <param name="url">
        ///     адрес запроса авторизации пользователя
        /// </param>
        public UserAuthForm(OAuthBroker broker, string url) : this()
        {
            _broker = broker;
            _url = url;
        }

        /// <summary>
        ///     Переход к форме авторизации
        /// </summary>
        /// <returns>
        ///     объект формы авторизации
        /// </returns>
        public UserAuthForm Navigate()
        {
            webBrowser.Navigate(_url);
            // открываем модальное окно
            // TODO Решение вопроса о том, как закрыть форму автоматически
            Show();

            return this;
        }

        private void WebBrowserDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
#if DEBUG
            Debug.WriteLine(e.Url.OriginalString);
#endif

                // TODO Автоматически закрыть форму
                _broker.GetVerifier(e.Url);
        }
    }
}