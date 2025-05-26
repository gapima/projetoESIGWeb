using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ESIGWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ESIGWeb.Pages
{
    public partial class RelatorioSalarioCalc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Carrega os dados da view para um DataTable
                var dt = DatabaseHelper.GetViewData();

                // Instancia o ReportDocument e carrega o .rpt
                ReportDocument rptDoc = new ReportDocument();
                rptDoc.Load(Server.MapPath("~/RelatorioSalarioCalc.rpt"));

                // Alimenta o report com o DataTable (o passo ESSENCIAL)
                rptDoc.SetDataSource(dt);

                // Passa para o Viewer
                CrystalReportViewer1.ReportSource = rptDoc;
                CrystalReportViewer1.DataBind();

                // (Opcional) Salva na sessão para paginação
                Session["RptDoc"] = rptDoc;
            }
            else
            {
                // Em postback, usa o rptDoc da sessão (obrigatório para evitar o problema ao paginar)
                CrystalReportViewer1.ReportSource = Session["RptDoc"];
            }
        }

    }
}