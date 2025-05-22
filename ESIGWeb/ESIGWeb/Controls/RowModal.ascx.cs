using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESIGWeb.Data;
using ESIGWeb.Models;

namespace ESIGWeb.Controls
{
    public partial class RowModal : UserControl, IPostBackEventHandler
    {
        // 1) Certifique-se de que estes IDs batem com o seu RowModal.ascx:
        //    txtPessoaId, txtPessoaNome, txtDataNascimento, txtEmail, txtUsuario,
        //    txtCidade, txtCEP, txtEndereco, txtPais, txtTelefone, ddlCargo

        protected override void OnInit(EventArgs e)
        {
            // Importante chamar o base para manter o ciclo de vida
            base.OnInit(e);

            // Se houvesse controles criados dinamicamente, aqui seria o lugar
            // para recriá-los antes do LoadViewState.
        }

        /// <summary>
        /// Este método é disparado pelo __doPostBack no cliente,
        /// com o argumento contendo o pessoaId.
        /// </summary>
        public void RaisePostBackEvent(string eventArgument)
        {
            // 2) Extrai o ID da pessoa
            if (!int.TryParse(eventArgument, out var pessoaId))
                return;

            // 3) Carrega do banco
            var p = DatabaseHelper.ObterPessoa(pessoaId);
            if (p == null)
                return;

            // 4) Preenche campos de Pessoa
            txtPessoaId.Text = p.Id.ToString();
            txtPessoaNome.Text = p.Nome;
            txtDataNascimento.Text = p.DataNascimento.ToString("yyyy-MM-dd");
            txtEmail.Text = p.Email;
            txtUsuario.Text = p.Usuario;

            // 5) Preenche campos de Endereço & Contato
            txtCidade.Text = p.Cidade;
            txtCEP.Text = p.CEP;
            txtCEP.Attributes["data-initial-cep"] = p.CEP;  // marca valor original
            txtEndereco.Text = p.Endereco;
            txtPais.Text = p.Pais;
            txtTelefone.Text = p.Telefone;

            // 6) Popula o DropDownList de Cargos
            var dt = DatabaseHelper.ObterTodosCargos();
            ddlCargo.DataSource = dt;
            ddlCargo.DataValueField = "id";
            ddlCargo.DataTextField = "nome";
            ddlCargo.DataBind();
            // Seleciona o cargo atual da pessoa
            if (ddlCargo.Items.FindByValue(p.CargoId.ToString()) != null)
                ddlCargo.SelectedValue = p.CargoId.ToString();

            // 7) Enfileira o script para abrir a modal já populada
            Page.ClientScript.RegisterStartupScript(
                GetType(),
                "showRowModal",
                "new bootstrap.Modal(document.getElementById('rowModal')).show();",
                true
            );
        }
    }
}
