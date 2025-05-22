<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Listagem.aspx.cs" Inherits="ESIGWeb.Listagem" %>
<%@ Register TagPrefix="uc" TagName="RowModal" Src="~/Controls/RowModal.ascx" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Listagem de Pessoas e Salários</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
      /* Hover nas linhas de dados: pinta TODAS as células da linha */
      .table tbody tr:hover td {
        background-color: #f0f8ff !important;
        cursor: pointer;
      }
    </style>

    <%--<style>
        /* Hover ao passar o mouse */
        .data-row:hover {
            background-color: #f0f8ff !important;
            cursor: pointer;
        }
        .data-row {
            transition: background-color 0.2s ease;
        }
        /* Linha selecionada */
        .data-row.selected {
            background-color: #ffe4b5 !important;
        }
    </style>--%>
</head>
<body>
    <form id="form1" runat="server" class="container mt-4">
        <h2>Listagem de Pessoas e Salários</h2>

        <asp:GridView
            ID="gridPessoas"
            runat="server"
            AutoGenerateColumns="false"
            AllowPaging="true"
            PageSize="10"
            OnPageIndexChanging="gridPessoas_PageIndexChanging"
            OnRowDataBound="gridPessoas_RowDataBound"
            CssClass="table table-bordered"
            DataKeyNames="pessoa_id"
            EmptyDataText="Nenhum registro encontrado.">
            <Columns>
                <asp:BoundField DataField="pessoa_id" HeaderText="ID" />
                <asp:BoundField DataField="nome" HeaderText="Nome" />
                <asp:BoundField DataField="salario_bruto" HeaderText="Salário Bruto" DataFormatString="{0:C}" />
                <asp:BoundField DataField="descontos" HeaderText="Descontos" DataFormatString="{0:C}" />
                <asp:BoundField DataField="salario_liquido" HeaderText="Salário Líquido" DataFormatString="{0:C}" />
            </Columns>
        </asp:GridView>

        <asp:Button
            ID="btnCalcular"
            runat="server"
            Text="Recalcular Salários"
            CssClass="btn btn-primary"
            OnClick="btnCalcular_Click" />

        <!-- Modal UserControl -->
        <uc:RowModal runat="server" ID="RowModal1" />
    </form>

    <!-- Scripts Bootstrap e lógica de click -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <script>
        document.addEventListener('DOMContentLoaded', function () {
            var grid = document.getElementById('<%= gridPessoas.ClientID %>');
            if (!grid) return;
            var tbody = grid.querySelector('tbody');
            if (!tbody) return;
            var rows = Array.from(tbody.rows);
            // dataRows são todas as linhas, já marcadas com data-row via RowDataBound

            tbody.addEventListener('click', function (e) {
                var tr = e.target;
                while (tr && tr.nodeName !== 'TR') tr = tr.parentNode;
                if (!tr || !tr.classList.contains('data-row')) return;

                // marca apenas a linha clicada
                rows.forEach(r => r.classList.remove('selected'));
                tr.classList.add('selected');

                // dispara evento para abrir modal
                var evt = new Event('showRowModal');
                document.dispatchEvent(evt);
            });
        });
    </script>
</body>
</html>
