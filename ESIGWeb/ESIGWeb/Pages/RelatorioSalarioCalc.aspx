<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelatorioSalarioCalc.aspx.cs" Inherits="ESIGWeb.Pages.RelatorioSalarioCalc" Async="true"%>
<%@ Register Assembly="CrystalDecisions.Web" Namespace="CrystalDecisions.Web" TagPrefix="cr" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Relatório de Salários</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background: #f8fafc;
            min-height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
        }
        .relatorio-container {
            background: #fff;
            border-radius: 1.4rem;
            box-shadow: 0 8px 24px rgba(0,0,0,.09);
            padding: 2.5rem 2.2rem 1.7rem 2.2rem;
            margin-top: 2.5rem;
            margin-bottom: 2.5rem;
            width: 100%;
            max-width: 1150px;
        }
        .relatorio-title {
            font-size: 2.2rem;
            font-weight: 700;
            margin-bottom: 1.7rem;
            text-align: center;
            letter-spacing: -1px;
            color: #2367b0;
        }
        @media (max-width: 1200px) {
            .relatorio-container {
                max-width: 98vw;
                padding: 1.5rem 0.2rem 0.5rem 0.2rem;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="relatorio-container mx-auto">
            <div class="relatorio-title">
                Relatório de Salários Calculados
            </div>
            <cr:CrystalReportViewer 
                ID="CrystalReportViewer1" 
                runat="server" 
                AutoDataBind="true"
                Width="100%" Height="800px" 
                ToolPanelView="None" 
                CssClass="mb-0"
            />
            <div class="text-center mt-4">
                <a href="Listagem.aspx" class="btn btn-outline-primary">
                    <svg xmlns="http://www.w3.org/2000/svg" width="18" height="18" fill="currentColor" class="bi bi-arrow-left" viewBox="0 0 16 16">
                      <path fill-rule="evenodd" d="M15 8a.5.5 0 0 1-.5.5H3.707l4.147 4.146a.5.5 0 0 1-.708.708l-5-5a.5.5 0 0 1 0-.708l5-5a.5.5 0 1 1 .708.708L3.707 7.5H14.5A.5.5 0 0 1 15 8z"/>
                    </svg>
                    Voltar à Listagem
                </a>
            </div>
        </div>
    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
