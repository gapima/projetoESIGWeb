using System;
using System.Data;
using System.Web.UI.WebControls;
using ESIGWeb.Data; // namespace onde está DatabaseHelper

namespace ESIGWeb
{
    public partial class Listagem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CarregarDados();
        }

        private void CarregarDados()
        {
            DataTable dt = DatabaseHelper.ObterPessoasSalarios();
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
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Marca a linha como data-row para o CSS de hover
                e.Row.CssClass = (e.Row.CssClass + " data-row").Trim();
            }
        }


        protected void btnCalcular_Click(object sender, EventArgs e)
        {
            DatabaseHelper.ExecutarProcedureCalculo();
            CarregarDados();
        }
    }
}
