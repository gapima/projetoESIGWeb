using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using ESIGWeb.Data;
using ESIGWeb.Models;

namespace ESIGWeb.Controls
{
    public partial class RowModal : UserControl, IPostBackEventHandler
    {
        protected HtmlGenericControl lblPessoaId;
        protected HtmlGenericControl lblPessoaNome;
        protected HtmlGenericControl lblCargoId;
        protected HtmlGenericControl lblCargoNome;

        // Recebe o pessoaId do __doPostBack
        public void RaisePostBackEvent(string eventArgument)
        {
            if (!int.TryParse(eventArgument, out var pessoaId))
                return;

            var p = DatabaseHelper.ObterPessoa(pessoaId);
            if (p == null)
                return;

            // Preenche os spans server-side
            lblPessoaId.InnerText = p.Id.ToString();
            lblPessoaNome.InnerText = p.Nome;
            lblCargoId.InnerText = p.CargoId.ToString();
            lblCargoNome.InnerText = p.CargoNome;

            // Injeta o script de abertura da modal após todo o HTML ter sido renderizado
            Page.ClientScript.RegisterStartupScript(
                this.GetType(),
                "showRowModal",
                "new bootstrap.Modal(document.getElementById('rowModal')).show();",
                true
            );
        }
    }
}
