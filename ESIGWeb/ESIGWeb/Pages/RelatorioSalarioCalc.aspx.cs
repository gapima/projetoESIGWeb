using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ESIGWeb.Data;
using System;

namespace ESIGWeb.Pages
{
    public partial class RelatorioSalarioCalc : System.Web.UI.Page
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
                // Preferencialmente, use a versão síncrona
                var dt = DatabaseHelper.GetViewData(); // <-- se existir

                // Se só existir a async, use .Result (apenas para testes locais)
                // var dt = DatabaseHelper.GetViewDataAsync().Result;

                ReportDocument rptDoc = new ReportDocument();
                rptDoc.Load(Server.MapPath("~/RelatorioSalarioCalc.rpt"));

                rptDoc.SetDataSource(dt);

                CrystalReportViewer1.ReportSource = rptDoc;
                CrystalReportViewer1.DataBind();

                Session["RptDoc"] = rptDoc;
            }
            else
            {
                CrystalReportViewer1.ReportSource = Session["RptDoc"];
            }
        }


    }
}