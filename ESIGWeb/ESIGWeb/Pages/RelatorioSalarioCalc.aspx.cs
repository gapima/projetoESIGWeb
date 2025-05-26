using CrystalDecisions.CrystalReports.Engine;
using ESIGWeb.Services;
using System;

namespace ESIGWeb.Pages
{
    public partial class RelatorioSalarioCalc : System.Web.UI.Page
    {
        private readonly RelatorioService _relatorioService = new RelatorioService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioLogado"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }
            if (!IsPostBack)
            {
                var dt = _relatorioService.ObterDadosRelatorio();

                ReportDocument rptDoc = new ReportDocument();
                rptDoc.Load(Server.MapPath("~/Reports/RelatorioSalarioCalc.rpt"));
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
