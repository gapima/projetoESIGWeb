using System.Web;

namespace ESIGWeb.Utils
{
    public static class WebUtils
    {
        public static void SetMensagemGlobal(string mensagem, string tipo)
        {
            HttpContext.Current.Session["MensagemGlobal"] = mensagem;
            HttpContext.Current.Session["MensagemGlobalTipo"] = tipo;
        }
    }
}
