<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelatorioSalarioCalc.aspx.cs" Inherits="ESIGWeb.Pages.RelatorioSalarioCalc" %>
<%@ Register Assembly="CrystalDecisions.Web" Namespace="CrystalDecisions.Web" TagPrefix="cr" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Relatório de Salários</title>
</head>
<body>
    <form id="form1" runat="server">
        <cr:CrystalReportViewer 
            ID="CrystalReportViewer1" 
            runat="server" 
            AutoDataBind="true"
            Width="100%" Height="800px" 
            ToolPanelView="None" />
    </form>
</body>
</html>
