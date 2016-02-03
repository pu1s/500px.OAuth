using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuth.UI
{
    public static class ShowForm
    {
        public static void Show()
        {
            var form = new TestForm();
            form.ShowDialog();
        }
    }
}
