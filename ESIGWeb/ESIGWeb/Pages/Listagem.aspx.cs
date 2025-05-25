using ESIGWeb.Controls;
using ESIGWeb.Data;
using System;
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
            if (!IsPostBack)
                CarregarDados();
        }

        private void CarregarDados()
        {
            var dt = DatabaseHelper.ObterPessoasSalarios();
            gridPessoas.DataSource = dt;
            gridPessoas.DataBind();
        }

        protected void gridPessoas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridPessoas.PageIndex = e.NewPageIndex;
            CarregarDados();
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
    }
}
