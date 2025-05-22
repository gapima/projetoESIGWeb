using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using ESIGWeb.Data;
using ESIGWeb.Models;

namespace ESIGWeb.Controls
{
    public partial class RowModal : UserControl, IPostBackEventHandler
    {
        public void RaisePostBackEvent(string eventArgument)
        {
            if (!int.TryParse(eventArgument, out var pessoaId))
                return;

            var p = DatabaseHelper.ObterPessoa(pessoaId);
            if (p == null) return;

            // Preenche os campos no server-side
            txtPessoaId.Text = p.Id.ToString();
            txtPessoaNome.Text = p.Nome;
            txtDataNascimento.Text = p.DataNascimento.ToString("yyyy-MM-dd");
            txtEmail.Text = p.Email;
            txtUsuario.Text = p.Usuario;
            txtCidade.Text = p.Cidade;
            txtCEP.Text = p.CEP;
            txtEndereco.Text = p.Endereco;
            txtPais.Text = p.Pais;
            txtTelefone.Text = p.Telefone;

            // Abre a modal
            Page.ClientScript.RegisterStartupScript(
                GetType(),
                "showRowModal",
                "new bootstrap.Modal(document.getElementById('rowModal')).show();",
                true
            );
        }
    }
}
