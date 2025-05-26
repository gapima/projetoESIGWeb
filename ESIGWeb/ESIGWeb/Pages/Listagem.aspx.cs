using ESIGWeb.Controls;
using ESIGWeb.Models;
using ESIGWeb.Services;
using ESIGWeb.Utils;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ESIGWeb
{
    public partial class Listagem : Page
    {
        private readonly ListagemService _listagemService = new ListagemService();

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioLogado"] == null)
            {
                Response.Redirect("Login.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }
            if (!IsPostBack)
            {
                var usuario = (Usuario)Session["UsuarioLogado"];
                lblUsuarioLogado.Text = $"Olá, {usuario.Login}!";

                if (Session["MensagemGlobal"] != null)
                {
                    WebUtils.ShowMensagemGlobalScript(this, Session["MensagemGlobal"].ToString());
                    Session["MensagemGlobal"] = null;
                }

                loading.Style["display"] = "block";
                await CarregarDadosAsync();
                loading.Style["display"] = "none";
            }
            RowModal1.PessoaSalvaSucesso += RowModal1_PessoaSalvaSucesso;
        }

        public async Task RecarregarGridAsync()
        {
            await CarregarDadosAsync(ViewState["FiltroNome"]?.ToString() ?? "", ViewState["FiltroCargo"]?.ToString() ?? "");
        }

        private async void RowModal1_PessoaSalvaSucesso(object sender, EventArgs e)
        {
            await CarregarDadosAsync(
                ViewState["FiltroNome"]?.ToString() ?? "",
                ViewState["FiltroCargo"]?.ToString() ?? ""
            );
            // Se quiser, pode mostrar um toast aqui, mas a mensagem global já deve aparecer.
        }
        private async Task CarregarDadosAsync(string filtroNome = "", string filtroCargo = "")
        {
            var dt = await _listagemService.ObterPessoasSalariosAsync();
            GridUtils.CarregarGridComFiltro(gridPessoas, dt, filtroNome, filtroCargo);
        }

        protected async void gridPessoas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridPessoas.PageIndex = e.NewPageIndex;
            string filtroNome = ViewState["FiltroNome"]?.ToString() ?? "";
            string filtroCargo = ViewState["FiltroCargo"]?.ToString() ?? "";

            loading.Style["display"] = "block";
            await CarregarDadosAsync(filtroNome, filtroCargo);
            loading.Style["display"] = "none";
        }

        protected void gridPessoas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;

            var id = DataBinder.Eval(e.Row.DataItem, "pessoa_id")?.ToString();
            e.Row.Attributes["data-pessoa-id"] = id;
            e.Row.CssClass += " data-row";
            e.Row.Attributes["onclick"] =
                $"__doPostBack('{RowModal1.UniqueID}','{id}')";
            e.Row.Style["cursor"] = "pointer";
        }

        protected async void btnCalcular_Click(object sender, EventArgs e)
        {
            loading.Style["display"] = "block";
            await _listagemService.ExecutarProcedureCalculoAsync();
            await CarregarDadosAsync();
            loading.Style["display"] = "none";
        }

        protected async void btnAddPessoa_Click(object sender, EventArgs e)
        {
            // Preenche cargos no dropdown da modal
            var ddlCargo = RowModal1.FindControl("ddlCargo") as DropDownList;
            if (ddlCargo != null)
            {
                var dt = await _listagemService.ObterTodosCargosAsync();
                ddlCargo.DataSource = dt;
                ddlCargo.DataValueField = "id";
                ddlCargo.DataTextField = "nome";
                ddlCargo.DataBind();
            }

            // Preenche grids de créditos e débitos padrão (cargo 1)
            var gridCreditos = RowModal1.FindControl("gridCreditos") as GridView;
            var creditos = await _listagemService.ObterDadosFinanceiroPessoaAsync(1, "C");
            gridCreditos.DataSource = creditos;
            gridCreditos.DataBind();

            var gridDebitos = RowModal1.FindControl("gridDebitos") as GridView;
            var debitos = await _listagemService.ObterDadosFinanceiroPessoaAsync(1, "D");
            gridDebitos.DataSource = debitos;
            gridDebitos.DataBind();

            // Limpa todos os campos da modal (utilitário)
            LimparCampoUtil.LimparTodosTextBox(RowModal1);

            // Define PessoaId como 0 (novo)
            var txtPessoaId = RowModal1.FindControl("txtPessoaId") as TextBox;
            if (txtPessoaId != null) txtPessoaId.Text = "0";

            ScriptUtils.ShowModal(this, "rowModal");
        }

        protected async void btnVincularVencimentos_Click(object sender, EventArgs e)
        {
            LimparCampoUtil.LimparTextBox(VincularVencimentosModal2, "txtValor");
            await VincularVencimentosModal2.CarregarDropdownsAsync();
            ScriptUtils.ShowModal(this, "vincularVencModal");
        }

        protected void btnGerarRelatorio_Click(object sender, EventArgs e)
        {
            Response.Redirect("RelatorioSalarioCalc.aspx");
        }

        protected async void btnFiltrar_Click(object sender, EventArgs e)
        {
            ViewState["FiltroNome"] = txtFiltroNome.Text.Trim();
            ViewState["FiltroCargo"] = txtFiltroCargo.Text.Trim();

            loading.Style["display"] = "block";
            await CarregarDadosAsync(ViewState["FiltroNome"]?.ToString(), ViewState["FiltroCargo"]?.ToString());
            loading.Style["display"] = "none";
        }

        protected async void btnLimparFiltro_Click(object sender, EventArgs e)
        {
            txtFiltroNome.Text = "";
            txtFiltroCargo.Text = "";
            ViewState["FiltroNome"] = null;
            ViewState["FiltroCargo"] = null;

            loading.Style["display"] = "block";
            await CarregarDadosAsync();
            loading.Style["display"] = "none";
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }
    }
}
