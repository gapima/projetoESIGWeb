using ESIGWeb.Controls;
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
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["UsuarioLogado"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }
            if (!IsPostBack)
            {
                var usuario = (Usuario)Session["UsuarioLogado"];
                lblUsuarioLogado.Text = $"Olá, {usuario.Login}!"; // Ou usuario.Nome, se preferir

                if (Session["MensagemGlobal"] != null)
                {
                    // Cria o JS para mostrar o toast com a mensagem
                    string msg = Session["MensagemGlobal"].ToString().Replace("'", "\\'");
                    string script = $@"
                <script>
                  document.addEventListener('DOMContentLoaded', function() {{
                    showGlobalToast('{msg}');
                  }});
                </script>";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msgGlobal", script, false);
                    // Limpa a sessão para não repetir a mensagem
                    Session["MensagemGlobal"] = null;
                }
                CarregarDados();
            }
        }

        private void CarregarDados(string filtroNome = "", string filtroCargo = "")
        {
            var dt = DatabaseHelper.ObterPessoasSalarios();

            // Cria DataView para filtrar sem alterar o DataTable original
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


        protected void gridPessoas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridPessoas.PageIndex = e.NewPageIndex;
            // Pegue filtros do ViewState, senão fica vazio
            string filtroNome = ViewState["FiltroNome"]?.ToString() ?? "";
            string filtroCargo = ViewState["FiltroCargo"]?.ToString() ?? "";
            CarregarDados(filtroNome, filtroCargo);
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
            await Task.Run(() =>DatabaseHelper.ExecutarProcedureCalculoAsync());
            CarregarDados();
        }

        void LimparCampo(Control parent, string id)
        {
            var txt = parent.FindControl(id) as TextBox;
            if (txt != null) txt.Text = "";
        }
        protected void btnAddPessoa_Click(object sender, EventArgs e)
        {
            // 1) Popula o dropdown de cargos dentro do RowModal
            var ddlCargo = RowModal1.FindControl("ddlCargo") as DropDownList;
            if (ddlCargo != null)
            {
                var dt = DatabaseHelper.ObterTodosCargos();
                ddlCargo.DataSource = dt;
                ddlCargo.DataValueField = "id";
                ddlCargo.DataTextField = "nome";
                ddlCargo.DataBind();
            }
            var gridCreditos = RowModal1.FindControl("gridCreditos") as GridView;
            var creditos = DatabaseHelper.ObterDadosFinanceiroPessoa(1, "C");
            gridCreditos.DataSource = creditos;
            gridCreditos.DataBind();

            var gridDebitos = RowModal1.FindControl("gridDebitos") as GridView;
            var debitos = DatabaseHelper.ObterDadosFinanceiroPessoa(1, "D");
            gridDebitos.DataSource = debitos;
            gridDebitos.DataBind();

            var txtPessoaId = RowModal1.FindControl("txtPessoaId") as TextBox;
            if (txtPessoaId != null) txtPessoaId.Text = "0";

            var txtPessoaNome = RowModal1.FindControl("txtPessoaNome") as TextBox;
            if (txtPessoaNome != null) txtPessoaNome.Text = "";

            var txtDataNascimento = RowModal1.FindControl("txtDataNascimento") as TextBox;
            if (txtDataNascimento != null) txtDataNascimento.Text = "";

            var txtEmail = RowModal1.FindControl("txtEmail") as TextBox;
            if (txtEmail != null) txtEmail.Text = "";

            var txtUsuario = RowModal1.FindControl("txtUsuario") as TextBox;
            if (txtUsuario != null) txtUsuario.Text = "";

            var txtCidade = RowModal1.FindControl("txtCidade") as TextBox;
            if (txtCidade != null) txtCidade.Text = "";

            var txtCEP = RowModal1.FindControl("txtCEP") as TextBox;
            if (txtCEP != null) txtCEP.Text = "";

            var txtEndereco = RowModal1.FindControl("txtEndereco") as TextBox;
            if (txtEndereco != null) txtEndereco.Text = "";

            var txtPais = RowModal1.FindControl("txtPais") as TextBox;
            if (txtPais != null) txtPais.Text = "";

            var txtTelefone = RowModal1.FindControl("txtTelefone") as TextBox;
            if (txtTelefone != null) txtTelefone.Text = "";

            LimparCampo(RowModal1, "txtPessoaId");
            LimparCampo(RowModal1, "txtPessoaNome");

            // 3) Registra script para limpar e abrir a modal
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
            // abre a modal principal de vinculação
            LimparCampo(VincularVencimentosModal2, "txtValor");
            VincularVencimentosModal2.CarregarDropdowns();
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
        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            ViewState["FiltroNome"] = txtFiltroNome.Text.Trim();
            ViewState["FiltroCargo"] = txtFiltroCargo.Text.Trim();
            CarregarDados(ViewState["FiltroNome"]?.ToString(), ViewState["FiltroCargo"]?.ToString());
        }
        protected void btnLimparFiltro_Click(object sender, EventArgs e)
        {
            txtFiltroNome.Text = "";
            txtFiltroCargo.Text = "";
            ViewState["FiltroNome"] = null;
            ViewState["FiltroCargo"] = null;
            CarregarDados();
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }


    }
}
