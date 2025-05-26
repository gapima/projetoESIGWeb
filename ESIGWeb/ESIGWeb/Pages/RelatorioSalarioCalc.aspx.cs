using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ESIGWeb.Services; // (novo)
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
                // Consulta via Service
                var dt = _relatorioService.ObterDadosRelatorio();

                ReportDocument rptDoc = new ReportDocument();
                rptDoc.Load(Server.MapPath("~/Reports/RelatorioSalarioCalc.rpt"));
                rptDoc.SetDataSource(dt);

                CrystalReportViewer1.ReportSource = rptDoc;
                CrystalReportViewer1.DataBind();

                Session["RptDoc"] = rptDoc; // manter referência na sessão
            }
            else
            {
                CrystalReportViewer1.ReportSource = Session["RptDoc"];
            }
        }
    }
}
