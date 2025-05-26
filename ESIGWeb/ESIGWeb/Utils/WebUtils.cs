using System.Web;
using System.Web.UI;

namespace ESIGWeb.Utils
{
    public static class WebUtils
    {
        public static void SetMensagemGlobal(string mensagem, string tipo)
        {
            HttpContext.Current.Session["MensagemGlobal"] = mensagem;
            HttpContext.Current.Session["MensagemGlobalTipo"] = tipo;
        }

        public static void ShowMensagemGlobalScript(Page page, string mensagem)
        {
            string msg = mensagem.Replace("'", "\\'");
            string script = $@"
                <script>
                  document.addEventListener('DOMContentLoaded', function() {{
                    showGlobalToast('{msg}');
                  }});
                </script>";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "msgGlobal", script, false);
        }
    }
}
