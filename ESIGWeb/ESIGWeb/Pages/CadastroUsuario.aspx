<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CadastroUsuario.aspx.cs" Inherits="ESIGWeb.Pages.CadastroUsuario" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Cadastro de Usuário</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lblLogin" runat="server" Text="Usuário:" />
            <asp:TextBox ID="txtLogin" runat="server" />
            <br />
            <asp:Label ID="lblNome" runat="server" Text="Nome completo:" />
            <asp:TextBox ID="txtNome" runat="server" />
            <br />
            <asp:Label ID="lblEmail" runat="server" Text="Email:" />
            <asp:TextBox ID="txtEmail" runat="server" />
            <br />
            <asp:Label ID="lblSenha" runat="server" Text="Senha:" />
            <asp:TextBox ID="txtSenha" runat="server" TextMode="Password" />
            <br />
            <asp:Button ID="btnCadastrar" runat="server" Text="Cadastrar" OnClick="btnCadastrar_Click" />
            <asp:Label ID="lblMensagem" runat="server" ForeColor="Red" />
        </div>
    </form>
</body>
</html>
