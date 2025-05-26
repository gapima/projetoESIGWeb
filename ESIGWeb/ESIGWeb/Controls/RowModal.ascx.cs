using System;
using System.Web.UI;
using ESIGWeb.Services;
using ESIGWeb.Models;
using ESIGWeb.Utils; // <== Import dos utilitários!
using System.Collections.Generic;

namespace ESIGWeb.Controls
{
    public partial class RowModal : UserControl, IPostBackEventHandler
    {
        private readonly PessoaService _pessoaService = new PessoaService();

        public async void RaisePostBackEvent(string eventArgument)
        {
            int pessoaId = ConversionUtils.ToIntSafe(eventArgument);
            if (pessoaId == 0)
                return;

            var p = await _pessoaService.ObterPessoaAsync(pessoaId);
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

            var dt = await _pessoaService.ObterTodosCargosAsync();
            UIUtils.BindDropDownList(ddlCargo, dt, "id", "nome");
            if (ddlCargo.Items.FindByValue(p.CargoId.ToString()) != null)
                ddlCargo.SelectedValue = p.CargoId.ToString();

            gridCreditos.DataSource = p.Creditos;
            gridCreditos.DataBind();

            gridDebitos.DataSource = p.Debitos;
            gridDebitos.DataBind();

            ScriptUtils.ShowModal(Page, "rowModal");
        }

        protected async void ddlCargo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int novoCargoId = ConversionUtils.ToIntSafe(ddlCargo.SelectedValue);
            if (novoCargoId > 0)
            {
                var creditos = await _pessoaService.ObterCreditosPorCargoAsync(novoCargoId);
                gridCreditos.DataSource = creditos;
                gridCreditos.DataBind();

                var debitos = await _pessoaService.ObterDebitosPorCargoAsync(novoCargoId);
                gridDebitos.DataSource = debitos;
                gridDebitos.DataBind();
            }

            updRowModalBody.Update();
            ScriptUtils.ShowModal(Page, "rowModal");
        }
protected async void btnSavePessoa_Click(object sender, EventArgs e)
{
    try
    {
        List<string> erros;
        var pessoa = ValidationUtils.TryParsePessoa(
            txtPessoaId.Text,
            txtPessoaNome.Text,
            txtDataNascimento.Text,
            txtEmail.Text,
            txtUsuario.Text,
            txtCidade.Text,
            txtCEP.Text,
            txtEndereco.Text,
            txtPais.Text,
            txtTelefone.Text,
            ddlCargo.SelectedValue,
            out erros
        );

        if (erros.Count > 0)
        {
            // Monta a mensagem de erro para o usuário (pode ser com <br/> para múltiplos erros)
            string mensagem = "Erros ao salvar:<br/>" + string.Join("<br/>", erros);
            WebUtils.SetMensagemGlobal(mensagem, "erro");
            ScriptUtils.ShowModal(Page, "rowModal"); // Mantém o modal aberto para correção
            return;
        }

        await _pessoaService.SalvarPessoaAsync(pessoa);
        WebUtils.SetMensagemGlobal("Dados salvo com sucesso!", "sucesso");
        Response.Redirect("Listagem.aspx", false);
    }
    catch (Exception ex)
    {
        WebUtils.SetMensagemGlobal("Erro ao salvar dados: " + ex.Message, "erro");
        Response.Redirect("Listagem.aspx", false);
    }
}


        protected async void btnDeletePessoa_Click(object sender, EventArgs e)
        {
            try
            {
                int pessoaId = ConversionUtils.ToIntSafe(txtPessoaId.Text);
                if (pessoaId == 0)
                    return;

                await _pessoaService.ExcluirPessoaAsync(pessoaId);

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
                WebUtils.SetMensagemGlobal("Pessoa excluida com sucesso!", "sucesso");
                Response.Redirect("Listagem.aspx", false);
            }
            catch (Exception ex)
            {
                WebUtils.SetMensagemGlobal("Erro ao excluir pessoa: " + ex.Message, "erro");
                Response.Redirect("Listagem.aspx", false);
            }
        }
    }
}
