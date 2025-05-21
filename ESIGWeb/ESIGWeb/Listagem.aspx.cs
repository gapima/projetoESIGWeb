using System;
using System.Data;
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

        protected void btnCalcular_Click(object sender, EventArgs e)
        {
            DatabaseHelper.ExecutarProcedureCalculo();
            CarregarDados();
        }
    }
}
