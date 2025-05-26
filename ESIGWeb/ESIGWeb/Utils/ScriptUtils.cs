using System.Web.UI;

namespace ESIGWeb.Utils
{
    public static class ScriptUtils
    {
        public static void ShowModal(Page page, string modalId)
        {
            string script = $"new bootstrap.Modal(document.getElementById('{modalId}')).show();";
            page.ClientScript.RegisterStartupScript(page.GetType(), $"show{modalId}", script, true);
        }

        public static void HideModal(Page page, string modalId)
        {
            string script = $"var m = bootstrap.Modal.getInstance(document.getElementById('{modalId}')); if(m) m.hide();";
            page.ClientScript.RegisterStartupScript(page.GetType(), $"hide{modalId}", script, true);
        }

    }
}
