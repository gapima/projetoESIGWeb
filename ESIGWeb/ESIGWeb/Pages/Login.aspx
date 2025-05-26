<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ESIGWeb.Pages.Login" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Login</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lblLogin" runat="server" Text="Usuário:" />
            <asp:TextBox ID="txtLogin" runat="server" />
            <br />
            <asp:Label ID="lblSenha" runat="server" Text="Senha:" />
            <asp:TextBox ID="txtSenha" runat="server" TextMode="Password" />
            <br />
            <asp:Button ID="btnLogin" runat="server" Text="Entrar" OnClick="btnLogin_Click" />
            <asp:Label ID="lblMensagem" runat="server" ForeColor="Red" />
            <br />
            <asp:HyperLink ID="lnkCriarUsuario" runat="server" NavigateUrl="CadastroUsuario.aspx">Criar usuário</asp:HyperLink>

        </div>
    </form>
</body>
</html>
