using System;
using System.Web.UI;
using ESIGWeb.Data;
using ESIGWeb.Models;
using Microsoft.Ajax.Utilities;

namespace ESIGWeb.Controls
{
    public partial class RowModal : UserControl, IPostBackEventHandler
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }
        public async void RaisePostBackEvent(string eventArgument)
        {
            if (!int.TryParse(eventArgument, out var pessoaId))
                return;

            var p = await DatabaseHelper.ObterPessoaAsync(pessoaId);
            if (p == null)
                return;

            txtPessoaId.Text = p.Id.ToString();
            txtPessoaNome.Text = p.Nome;
            txtDataNascimento.Text = p.DataNascimento.ToString("yyyy-MM-dd");
            txtEmail.Text = p.Email;
            txtUsuario.Text = p.Usuario;

            txtCidade.Text = p.Cidade;
            txtCEP.Text = p.CEP;
            txtCEP.Attributes["data-initial-cep"] = p.CEP;
            txtEndereco.Text = p.Endereco;
            txtPais.Text = p.Pais;
            txtTelefone.Text = p.Telefone;

            var dt = await DatabaseHelper.ObterTodosCargosAsync();
            ddlCargo.DataSource = dt;
            ddlCargo.DataValueField = "id";
            ddlCargo.DataTextField = "nome";
            ddlCargo.DataBind();
            if (ddlCargo.Items.FindByValue(p.CargoId.ToString()) != null)
                ddlCargo.SelectedValue = p.CargoId.ToString();

            gridCreditos.DataSource = p.Creditos;
            gridCreditos.DataBind();

            gridDebitos.DataSource = p.Debitos;
            gridDebitos.DataBind();

            Page.ClientScript.RegisterStartupScript(
                GetType(),
                "showRowModal",
                "new bootstrap.Modal(document.getElementById('rowModal')).show();",
                true
            );
        }
        protected async void ddlCargo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(ddlCargo.SelectedValue, out var novoCargoId))
            {
                var creditos = await DatabaseHelper.ObterDadosFinanceiroPessoaAsync(novoCargoId, "C");
                gridCreditos.DataSource = creditos;
                gridCreditos.DataBind();

                var debitos = await DatabaseHelper.ObterDadosFinanceiroPessoaAsync(novoCargoId, "D");
                gridDebitos.DataSource = debitos;
                gridDebitos.DataBind();
            }

            updRowModalBody.Update();
            ScriptManager.RegisterStartupScript(
                this,
                GetType(),
                "showRowModal",
                "var modalEl = document.getElementById('rowModal'); if(modalEl){var m=bootstrap.Modal.getOrCreateInstance(modalEl);m.show();}",
                true
            );
        }
        protected async void btnSavePessoa_Click(object sender, EventArgs e)
        {
            try
            {
                var p = new Pessoa
                {
                    Id = !txtPessoaId.Text.IsNullOrWhiteSpace() ? int.Parse(txtPessoaId.Text) : 0,
                    Nome = txtPessoaNome.Text,
                    DataNascimento = DateTime.Parse(txtDataNascimento.Text),
                    Email = txtEmail.Text,
                    Usuario = txtUsuario.Text,
                    Cidade = txtCidade.Text,
                    CEP = txtCEP.Text,
                    Endereco = txtEndereco.Text,
                    Pais = txtPais.Text,
                    Telefone = txtTelefone.Text,
                    CargoId = int.Parse(ddlCargo.SelectedValue)
                };

                if (p.Id == 0)
                {
                    await DatabaseHelper.InserirPessoaAsync(p);
                }
                else
                {
                    await DatabaseHelper.SalvarPessoaAsync(p);
                }
                Session["MensagemGlobal"] = "Dados salvo com sucesso!";
                Session["MensagemGlobalTipo"] = "sucesso";
                Response.Redirect("Listagem.aspx", false);
            }
            catch (Exception ex)
            {
                Session["MensagemGlobal"] = "Erro ao salvar dados: " + ex.Message;
                Session["MensagemGlobalTipo"] = "erro";
                Response.Redirect("Listagem.aspx", false);
            }
        }
        protected async void btnDeletePessoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtPessoaId.Text, out var pessoaId))
                    return;

                await DatabaseHelper.ExcluirPessoaAsync(pessoaId);

                var btnCalc = Page.FindControl("btnCalcular") as System.Web.UI.WebControls.Button;
                string postbackRef = btnCalc != null
                    ? Page.ClientScript.GetPostBackEventReference(btnCalc, "")
                    : "__doPostBack('','')";

                string script = $@"
                  var m = bootstrap.Modal.getInstance(document.getElementById('rowModal'));
                  if(m) m.hide();
                  {postbackRef};
                ";

                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "deletePessoa",
                    script,
                    true
                );
                Session["MensagemGlobal"] = "Pessoa excluida com sucesso!";
                Session["MensagemGlobalTipo"] = "sucesso";
                Response.Redirect("Listagem.aspx", false);
            }
            catch (Exception ex)
            {
                Session["MensagemGlobal"] = "Erro ao excluir pessoa: " + ex.Message;
                Session["MensagemGlobalTipo"] = "erro";
                Response.Redirect("Listagem.aspx", false);
            }
        }
    }
}
