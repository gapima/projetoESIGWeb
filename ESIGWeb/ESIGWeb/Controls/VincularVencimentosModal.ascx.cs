using ESIGWeb.Models;
using ESIGWeb.Services;
using ESIGWeb.Utils;
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
        private readonly VencimentoService _vencimentoService = new VencimentoService();
        public event EventHandler PessoaSalvaSucesso;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CarregarDropdownsAsync();
        }

        public async Task CarregarDropdownsAsync()
        {
            ddlVencimentos.DataSource = await _vencimentoService.ObterTodosVencimentosAsync();
            ddlVencimentos.DataTextField = "Descricao";
            ddlVencimentos.DataValueField = "Id";
            ddlVencimentos.DataBind();
            ddlVencimentos.Items.Insert(0, new ListItem("-- selecione --", ""));

            await PreencherCargosAsync();
        }

        protected async void ddlVencimentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int vid = ConversionUtils.ToIntSafe(ddlVencimentos.SelectedValue);
            if (vid > 0)
            {
                var v = await _vencimentoService.ObterVencimentoPorIdAsync(vid);
                txtValor.Text = v.Valor.ToString("F2");
                ddlForma.SelectedValue = v.FormaIncidencia;
                ddlTipo.SelectedValue = v.Tipo;
            }
            await PreencherCargosAsync();
            ScriptUtils.ShowModal(Page, "vincularVencModal");
        }

        private async Task PreencherCargosAsync()
        {
            var todosDt = await _vencimentoService.ObterTodosCargosAsync();
            var todasRows = todosDt.Rows.Cast<DataRow>();

            var vinculados = new HashSet<int>();
            int vid = ConversionUtils.ToIntSafe(ddlVencimentos.SelectedValue);
            if (vid > 0)
            {
                var cargosVinc = await _vencimentoService.ObterCargosVinculadosAsync(vid);
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
            if (!Page.IsValid)
            {
                ScriptUtils.ShowModal(Page, "vencimentoModal");
                return;
            }
            try
            {
                // Validação centralizada para vencimento (edição)
                List<string> erros;
                var venc = ValidationUtils.TryParseVencimento(
                    id: ddlVencimentos.SelectedValue,
                    descricao: null, // Não edita descrição aqui, então pode ser null
                    valor: txtValor.Text,
                    formaIncidencia: ddlForma.SelectedValue,
                    tipo: ddlTipo.SelectedValue,
                    out erros
                );

                if (erros.Count > 0)
                {
                    WebUtils.SetMensagemGlobal("Erros ao salvar:<br/>" + string.Join("<br/>", erros), "erro");
                    ScriptUtils.ShowModal(Page, "vincularVencModal");
                    return;
                }

                var valores = Request.Form.GetValues("chkCargo");
                var selecionados = new HashSet<int>();
                if (valores != null)
                {
                    foreach (var s in valores)
                        selecionados.Add(ConversionUtils.ToIntSafe(s));
                }

                await _vencimentoService.AtualizarVencimentoAsync(venc);

                var todosDt = await _vencimentoService.ObterTodosCargosAsync();
                var todosIds = todosDt.Rows.Cast<DataRow>().Select(r => Convert.ToInt32(r["id"]));

                foreach (var cid in todosIds)
                {
                    if (selecionados.Contains(cid))
                        await _vencimentoService.VincularCargoAsync(venc.Id, cid);
                    else
                        await _vencimentoService.DesvincularCargoAsync(venc.Id, cid);
                }
                await CarregarDropdownsAsync();

                if (this.Page is Listagem page)
                {
                    await page.RecarregarGridAsync();
                }

                ScriptUtils.HideModal(Page, "vincularVencModal");
                WebUtils.SetMensagemGlobal("Vencimento salvo com sucesso!", "sucesso");
                Response.Redirect("Listagem.aspx", false);
            }
            catch (Exception ex)
            {
                WebUtils.SetMensagemGlobal("Erro ao salvar vencimento: " + ex.Message, "erro");
                Response.Redirect("Listagem.aspx", false);
            }
        }

        protected async void btnExcluirVinc_Click(object sender, EventArgs e)
        {
            try
            {
                int vid = ConversionUtils.ToIntSafe(ddlVencimentos.SelectedValue);
                if (vid == 0)
                    return;

                var cargosVinc = await _vencimentoService.ObterCargosVinculadosAsync(vid);
                foreach (var cv in cargosVinc)
                    await _vencimentoService.DesvincularCargoAsync(vid, cv.CargoId);

                await _vencimentoService.ExcluirVencimentoAsync(vid);
                await CarregarDropdownsAsync();

                if (this.Page is Listagem page)
                {
                    await page.RecarregarGridAsync();
                }

                ScriptUtils.HideModal(Page, "vincularVencModal");
                WebUtils.SetMensagemGlobal("Vencimento excluído com sucesso!", "sucesso");
                Response.Redirect("Listagem.aspx", false);
            }
            catch (Exception ex)
            {
                WebUtils.SetMensagemGlobal("Erro ao excluir vencimento: " + ex.Message, "erro");
                Response.Redirect("Listagem.aspx", false);
            }
        }

        protected async void btnSalvarNovo_Click(object sender, EventArgs e)
        {
            try
            {
                // Validação centralizada para novo vencimento
                List<string> erros;
                var venc = ValidationUtils.TryParseVencimento(
                    id: null,
                    descricao: txtDescNovo.Text,
                    valor: txtValorNovo.Text,
                    formaIncidencia: ddlFormaNovo.SelectedValue,
                    tipo: ddlTipoNovo.SelectedValue,
                    out erros
                );

                if (erros.Count > 0)
                {
                    WebUtils.SetMensagemGlobal("Erros ao salvar:<br/>" + string.Join("<br/>", erros), "erro");
                    ScriptUtils.ShowModal(Page, "novoVencModal");
                    return;
                }

                await _vencimentoService.InserirVencimentoAsync(venc);
                await CarregarDropdownsAsync();

                // Troca de modal com JS custom
                string script = $@"
                    bootstrap.Modal.getInstance(document.getElementById('{novoVencModal.ClientID}')).hide();
                    setTimeout(function(){{
                        new bootstrap.Modal(document.getElementById('{vincularVencModal.ClientID}')).show();
                    }}, 200);
                ";
                ScriptManager.RegisterStartupScript(this, GetType(), "afterNovoVenc", script, true);

                WebUtils.SetMensagemGlobal("Novo vencimento salvo com sucesso!", "sucesso");
                Response.Redirect("Listagem.aspx", false);
            }
            catch (Exception ex)
            {
                WebUtils.SetMensagemGlobal("Erro ao salvar novo vencimento: " + ex.Message, "erro");
                Response.Redirect("Listagem.aspx", false);
            }
        }
    }
}
