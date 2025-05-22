using ESIGWeb.Data;
using ESIGWeb.Models;
using System;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

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


        [WebMethod]
        public static Pessoa GetPessoa(int pessoaId)
        {
            return DatabaseHelper.ObterPessoa(pessoaId);
        }

        protected void gridPessoas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Garantir que o DataField seja "pessoa_id"
                var id = DataBinder.Eval(e.Row.DataItem, "pessoa_id");
                e.Row.Attributes["data-pessoa-id"] = id?.ToString() ?? "";
                // opcional: classe para estilizar
                e.Row.CssClass += " data-row";
            }
        }


        protected void btnCalcular_Click(object sender, EventArgs e)
        {
            DatabaseHelper.ExecutarProcedureCalculo();
            CarregarDados();
        }
    }
}
