using ESIGWeb.Data;
using ESIGWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ESIGWeb
{
    public partial class Listagem : Page
    {
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
                    string msg = Session["MensagemGlobal"].ToString().Replace("'", "\\'");
                    string script = $@"
                    <script>
                      document.addEventListener('DOMContentLoaded', function() {{
                        showGlobalToast('{msg}');
                      }});
                    </script>";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msgGlobal", script, false);
                    Session["MensagemGlobal"] = null;
                }

                loading.Style["display"] = "block";
                await CarregarDadosAsync();
                loading.Style["display"] = "none";
            }
        }

        private async Task CarregarDadosAsync(string filtroNome = "", string filtroCargo = "")
        {
            var dt = await DatabaseHelper.ObterPessoasSalariosAsync();

            DataView dv = dt.DefaultView;

            List<string> filtros = new List<string>();
            if (!string.IsNullOrWhiteSpace(filtroNome))
                filtros.Add($"nome LIKE '%{filtroNome.Replace("'", "''")}%'");
            if (!string.IsNullOrWhiteSpace(filtroCargo))
                filtros.Add($"nome_cargo LIKE '%{filtroCargo.Replace("'", "''")}%'");

            if (filtros.Count > 0)
                dv.RowFilter = string.Join(" AND ", filtros);

            gridPessoas.DataSource = dv;
            gridPessoas.DataBind();
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
            await DatabaseHelper.ExecutarProcedureCalculoAsync();
            await CarregarDadosAsync();
            loading.Style["display"] = "none";
        }

        void LimparCampo(Control parent, string id)
        {
            var txt = parent.FindControl(id) as TextBox;
            if (txt != null) txt.Text = "";
        }

        protected async void btnAddPessoa_Click(object sender, EventArgs e)
        {
            var ddlCargo = RowModal1.FindControl("ddlCargo") as DropDownList;
            if (ddlCargo != null)
            {
                var dt = await DatabaseHelper.ObterTodosCargosAsync();
                ddlCargo.DataSource = dt;
                ddlCargo.DataValueField = "id";
                ddlCargo.DataTextField = "nome";
                ddlCargo.DataBind();
            }
            var gridCreditos = RowModal1.FindControl("gridCreditos") as GridView;
            var creditos = await DatabaseHelper.ObterDadosFinanceiroPessoaAsync(1, "C");
            gridCreditos.DataSource = creditos;
            gridCreditos.DataBind();

            var gridDebitos = RowModal1.FindControl("gridDebitos") as GridView;
            var debitos = await DatabaseHelper.ObterDadosFinanceiroPessoaAsync(1, "D");
            gridDebitos.DataSource = debitos;
            gridDebitos.DataBind();

            LimparCampo(RowModal1, "txtPessoaId");
            LimparCampo(RowModal1, "txtPessoaNome");
            LimparCampo(RowModal1, "txtDataNascimento");
            LimparCampo(RowModal1, "txtEmail");
            LimparCampo(RowModal1, "txtUsuario");
            LimparCampo(RowModal1, "txtCidade");
            LimparCampo(RowModal1, "txtCEP");
            LimparCampo(RowModal1, "txtEndereco");
            LimparCampo(RowModal1, "txtPais");
            LimparCampo(RowModal1, "txtTelefone");

            var txtPessoaId = RowModal1.FindControl("txtPessoaId") as TextBox;
            if (txtPessoaId != null) txtPessoaId.Text = "0";

            string script = "new bootstrap.Modal(document.getElementById('rowModal')).show();";
            ScriptManager.RegisterStartupScript(
                this,
                GetType(),
                "openNewPessoa",
                script,
                true
            );
        }

        protected void btnVincularVencimentos_Click(object sender, EventArgs e)
        {
            LimparCampo(VincularVencimentosModal2, "txtValor");
            VincularVencimentosModal2.CarregarDropdownsAsync(); // Deixe esse método async se o seu DataHelper for async!
            ScriptManager.RegisterStartupScript(
                this, GetType(),
                "showVincular",
                "new bootstrap.Modal(document.getElementById('vincularVencModal')).show();",
                true);
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
