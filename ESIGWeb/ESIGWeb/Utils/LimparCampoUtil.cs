using System.Web.UI;
using System.Web.UI.WebControls;

namespace ESIGWeb.Utils
{
    public static class LimparCampoUtil
    {
        public static void LimparTextBox(Control parent, string id)
        {
            var txt = parent.FindControl(id) as TextBox;
            if (txt != null) txt.Text = "";
        }

        public static void LimparTodosTextBox(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is TextBox tb)
                    tb.Text = "";
                else if (c.HasControls())
                    LimparTodosTextBox(c);
            }
        }
    }
}
