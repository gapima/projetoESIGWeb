using ESIGWeb.Data;
using ESIGWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ESIGWeb.Controls
{

    public partial class VincularVencimentosModal : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CarregarDropdownsAsync();
        }
        public async Task CarregarDropdownsAsync()
        {
            ddlVencimentos.DataSource = await DatabaseHelper.ObterTodosVencimentosAsync();
            ddlVencimentos.DataTextField = "Descricao";
            ddlVencimentos.DataValueField = "Id";
            ddlVencimentos.DataBind();
            ddlVencimentos.Items.Insert(0, new ListItem("-- selecione --", ""));

            await PreencherCargosAsync();
        }
        protected async void ddlVencimentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(ddlVencimentos.SelectedValue, out var vid))
            {
                var v = await DatabaseHelper.ObterVencimentoPorIdAsync(vid);
                txtValor.Text = v.Valor.ToString("F2");
                ddlForma.SelectedValue = v.FormaIncidencia;
                ddlTipo.SelectedValue = v.Tipo;
            }
            await PreencherCargosAsync();
            ScriptManager.RegisterStartupScript(
                this, GetType(),
                "showVincular",
                "new bootstrap.Modal(document.getElementById('vincularVencModal')).show();",
                true);
        }
        private async Task PreencherCargosAsync()
        {
            var todosDt = await DatabaseHelper.ObterTodosCargosAsync();
            var todasRows = todosDt.Rows.Cast<DataRow>();

            var vinculados = new HashSet<int>();
            if (int.TryParse(ddlVencimentos.SelectedValue, out var vid))
            {
                var cargosVinc = await DatabaseHelper.ObterCargosVinculadosAsync(vid);
                foreach (var cv in cargosVinc)
                    vinculados.Add(cv.CargoId);
            }

            var lista = todasRows
                .Select(r => new
                {
                    Id = Convert.ToInt32(r["id"]),
                    Nome = r["nome"].ToString(),
                    Vinculado = vinculados.Contains(Convert.ToInt32(r["id"]))
                })
                .ToList();

            rptCargos.DataSource = lista;
            rptCargos.DataBind();
        }
        protected async void btnSalvarVinc_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(ddlVencimentos.SelectedValue, out var vid))
                    return;

                var valores = Request.Form.GetValues("chkCargo");
                var selecionados = new HashSet<int>();
                if (valores != null)
                {
                    foreach (var s in valores)
                        if (int.TryParse(s, out var i))
                            selecionados.Add(i);
                }

                var v = new Vencimentos
                {
                    Id = vid,
                    Valor = decimal.Parse(txtValor.Text),
                    FormaIncidencia = ddlForma.SelectedValue,
                    Tipo = ddlTipo.SelectedValue
                };
                await DatabaseHelper.AtualizarVencimentoAsync(v);

                var todosDt = await DatabaseHelper.ObterTodosCargosAsync();
                var todosIds = todosDt.Rows
                    .Cast<DataRow>()
                    .Select(r => Convert.ToInt32(r["id"]));

                foreach (var cid in todosIds)
                {
                    if (selecionados.Contains(cid))
                        await DatabaseHelper.VincularCargoAsync(vid, cid);
                    else
                        await DatabaseHelper.DesvincularCargoAsync(vid, cid);
                }
                await CarregarDropdownsAsync();

                ScriptManager.RegisterStartupScript(
                    this, GetType(), "closeVinc",
                    "new bootstrap.Modal(document.getElementById('vincularVencModal')).hide();",
                    true);
                Session["MensagemGlobal"] = "Vencimento salvo com sucesso!";
                Session["MensagemGlobalTipo"] = "sucesso";
                Response.Redirect("Listagem.aspx", false);
            }
            catch (Exception ex)
            {
                Session["MensagemGlobal"] = "Erro ao salvar vencimento: " + ex.Message;
                Session["MensagemGlobalTipo"] = "erro";
                Response.Redirect("Listagem.aspx", false);
            }
        }
        protected async void btnExcluirVinc_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(ddlVencimentos.SelectedValue, out var vid))
                    return;

                var cargosVinc = await DatabaseHelper.ObterCargosVinculadosAsync(vid);
                foreach (var cv in cargosVinc)
                    await DatabaseHelper.DesvincularCargoAsync(vid, cv.CargoId);

                await DatabaseHelper.ExcluirVencimentoAsync(vid);
                await CarregarDropdownsAsync();

                ScriptManager.RegisterStartupScript(
                    this, GetType(), "closeVinc",
                    "new bootstrap.Modal(document.getElementById('vincularVencModal')).hide();",
                    true);

                Session["MensagemGlobal"] = "Vencimento excluído com sucesso!";
                Session["MensagemGlobalTipo"] = "sucesso";
                Response.Redirect("Listagem.aspx", false);
            }
            catch (Exception ex)
            {
                Session["MensagemGlobal"] = "Erro ao excluir vencimento: " + ex.Message;
                Session["MensagemGlobalTipo"] = "erro";
                Response.Redirect("Listagem.aspx", false);
            }
        }
        protected async void btnSalvarNovo_Click(object sender, EventArgs e)
        {
            try
            {
                var v = new Vencimentos
                {
                    Descricao = txtDescNovo.Text.Trim(),
                    Valor = decimal.Parse(txtValorNovo.Text),
                    FormaIncidencia = ddlFormaNovo.SelectedValue,
                    Tipo = ddlTipoNovo.SelectedValue
                };
                await DatabaseHelper.InserirVencimentoAsync(v);
                await CarregarDropdownsAsync();

                var script = $@"
                  bootstrap.Modal.getInstance(
                    document.getElementById('{novoVencModal.ClientID}')
                  ).hide();

                  setTimeout(function(){{
                    new bootstrap.Modal(
                      document.getElementById('{vincularVencModal.ClientID}')
                    ).show();
                  }}, 200);
                ";
                ScriptManager.RegisterStartupScript(this, GetType(), "afterNovoVenc", script, true);

                Session["MensagemGlobal"] = "Novo vencimento salvo com sucesso!";
                Session["MensagemGlobalTipo"] = "sucesso";
                Response.Redirect("Listagem.aspx", false);
            }
            catch (Exception ex)
            {
                Session["MensagemGlobal"] = "Erro ao salvar novo vencimento: " + ex.Message;
                Session["MensagemGlobalTipo"] = "erro";
                Response.Redirect("Listagem.aspx", false);
            }
        }
    }
}
