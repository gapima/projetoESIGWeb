<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Listagem.aspx.cs" Inherits="ESIGWeb.Listagem" %>
<%@ Register TagPrefix="uc" TagName="RowModal" Src="~/Controls/RowModal.ascx" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Listagem de Pessoas e Salários</title>

  <!-- CSS do Bootstrap no head -->
  <link
    href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css"
    rel="stylesheet" />

  <!-- JS do Bootstrap no head, para garantir que 'bootstrap' existe antes de qualquer script -->
  <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>

  <style>
    .table tbody tr:hover td {
      background-color: #f0f8ff !important;
      cursor: pointer;
    }
    .table tbody tr.selected td {
      background-color: #ffe4b5 !important;
    }
  </style>
</head>
<body>
  <form id="form1" runat="server" class="container mt-4">
    <asp:ScriptManager runat="server" />

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
        <asp:BoundField DataField="nome_cargo" HeaderText="Cargo" />
      </Columns>
    </asp:GridView>

    <asp:Button
      ID="btnCalcular"
      runat="server"
      Text="Recalcular Salários"
      CssClass="btn btn-primary mt-3"
      OnClick="btnCalcular_Click" />

    <!-- Injeção do UserControl de modal -->
    <uc:RowModal ID="RowModal1" runat="server" />
  </form>
</body>
</html>
