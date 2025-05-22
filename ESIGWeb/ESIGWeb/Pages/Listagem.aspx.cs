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
    }
}
