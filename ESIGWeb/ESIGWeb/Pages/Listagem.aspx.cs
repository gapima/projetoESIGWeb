using ESIGWeb.Data;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Configuration;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.Services;
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

        [WebMethod]
        public static object GetPessoaCargo(int pessoaId)
        {
            // Obter conexão e montar seu SELECT com INNER JOIN em pessoa e cargo
            string sql = @"
        SELECT p.id AS PessoaId,
               p.nome AS PessoaNome,
               c.id AS CargoId,
               c.nome AS CargoNome
          FROM pessoa p
          JOIN cargo c
            ON c.id = p.cargo_id
         WHERE p.id = :pId";

            using (var conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString))
            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("pId", OracleDbType.Int32).Value = pessoaId;
                conn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        return new
                        {
                            PessoaId = rdr.GetInt32(0),
                            PessoaNome = rdr.GetString(1),
                            CargoId = rdr.GetInt32(2),
                            CargoNome = rdr.GetString(3)
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }


    }
}
