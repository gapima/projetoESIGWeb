using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESIGWeb.Data;

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

        protected void btnCalcular_Click(object sender, EventArgs e)
        {
            DatabaseHelper.ExecutarProcedureCalculo();
            CarregarDados();
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

            // 2) Limpa todos os campos da modal
            var clearFields = @"
              // zera todos os inputs
              document.getElementById('" + RowModal1.FindControl("txtPessoaId").ClientID + @"').value = '0';
              document.getElementById('" + RowModal1.FindControl("txtPessoaNome").ClientID + @"').value = '';
              document.getElementById('" + RowModal1.FindControl("txtDataNascimento").ClientID + @"').value = '';
              document.getElementById('" + RowModal1.FindControl("txtEmail").ClientID + @"').value = '';
              document.getElementById('" + RowModal1.FindControl("txtUsuario").ClientID + @"').value = '';
              document.getElementById('" + RowModal1.FindControl("txtCidade").ClientID + @"').value = '';
              document.getElementById('" + RowModal1.FindControl("txtCEP").ClientID + @"').value = '';
              document.getElementById('" + RowModal1.FindControl("txtEndereco").ClientID + @"').value = '';
              document.getElementById('" + RowModal1.FindControl("txtPais").ClientID + @"').value = '';
              document.getElementById('" + RowModal1.FindControl("txtTelefone").ClientID + @"').value = '';
              // seleciona primeiro item do ddl
              var ddl = document.getElementById('" + ddlCargo.ClientID + @"');
              if (ddl) ddl.selectedIndex = 0;
            ";

            // 3) Registra script para limpar e abrir a modal
            string script = clearFields
                          + "new bootstrap.Modal(document.getElementById('rowModal')).show();";

            ScriptManager.RegisterStartupScript(
                this,
                GetType(),
                "openNewPessoa",
                script,
                true
            );
        }

    }
}
