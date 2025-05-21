<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Listagem.aspx.cs" Inherits="ESIGWeb.Listagem" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Listagem de Pessoas e Salários</title>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.6.0/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" class="container mt-4">
        <h2>Listagem de Pessoas e Salários</h2>
        <asp:GridView 
            ID="gridPessoas" 
            runat="server" 
            AutoGenerateColumns="false"
            CssClass="table table-striped table-bordered"
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
    </form>
</body>
</html>
