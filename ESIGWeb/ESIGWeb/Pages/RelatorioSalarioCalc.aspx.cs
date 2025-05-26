using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;

namespace ESIGWeb.Pages
{
    public partial class RelatorioSalarioCalc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ReportDocument rpt = new ReportDocument();
                string reportPath = Server.MapPath("~/RelatorioSalarioCalc.rpt"); // coloque o nome do seu .rpt aqui!
                rpt.Load(reportPath);

                // Se for necessário, defina novamente a conexão do banco aqui (pode ser automático pelo próprio .rpt)
                // rpt.SetDatabaseLogon(usuario, senha, servidor, banco);

                CrystalReportViewer1.ReportSource = rpt;
                CrystalReportViewer1.DataBind();
            }
        }
    }
}