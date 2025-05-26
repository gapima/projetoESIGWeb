<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ESIGWeb.Pages.Login" Async="true"%>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Login</title>
    <!-- Bootstrap 5 CDN -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background: #f8fafc;
            min-height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
        }
        .login-card {
            width: 100%;
            max-width: 370px;
            border-radius: 1.4rem;
            box-shadow: 0 8px 24px rgba(0,0,0,.07);
            background: #fff;
            padding: 2.5rem 2.2rem 1.7rem 2.2rem;
        }
        .login-title {
            font-size: 2rem;
            font-weight: 700;
            margin-bottom: 1.3rem;
            text-align: center;
            letter-spacing: -1px;
            color: #222;
        }
        .form-label {
            font-weight: 500;
        }
        .btn-login {
            border-radius: 2rem;
            font-size: 1.13rem;
            padding: 0.62rem 0;
            margin-top: 1.2rem;
        }
        .criar-link {
            text-align: center;
            margin-top: 1.3rem;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" />
        <div class="login-card mx-auto">
            <div class="login-title">Acesso ao Sistema</div>
            <div class="mb-3">
                <asp:Label ID="lblLogin" runat="server" AssociatedControlID="txtLogin" CssClass="form-label" Text="Usuário" />
                <asp:TextBox ID="txtLogin" runat="server" CssClass="form-control form-control-lg" placeholder="Digite seu usuário" />
            </div>
            <div class="mb-3">
                <asp:Label ID="lblSenha" runat="server" AssociatedControlID="txtSenha" CssClass="form-label" Text="Senha" />
                <div class="input-group">
                    <asp:TextBox ID="txtSenha" runat="server" TextMode="Password" CssClass="form-control form-control-lg" placeholder="Digite sua senha" />
                    <span class="input-group-text">
                        <svg xmlns="http://www.w3.org/2000/svg" width="18" height="18" fill="currentColor" class="bi bi-key" viewBox="0 0 16 16">
                          <path d="M3 8a5 5 0 1 1 9.584 1.485.5.5 0 0 0 .027.515l-4 6A.5.5 0 0 1 8 16a.5.5 0 0 1-.439-.244l-4-6A.5.5 0 0 0 3 8zm5 0a3 3 0 1 0-2.34 4.945l.376.565a1.5 1.5 0 1 1 1.928 0l.375-.565A3.001 3.001 0 0 0 8 8z"/>
                        </svg>
                    </span>
                </div>
            </div>
            <asp:Button 
                ID="btnLogin" 
                runat="server" 
                Text="Entrar"
                CssClass="btn btn-primary btn-login w-100"
                OnClick="btnLogin_Click" />
                <%-- 
                Se quiser usar OnClientClick para UX, use assim:
                OnClientClick="this.disabled=true; this.value='Entrando...'; return true;"
                --%>
            <asp:Label ID="lblMensagem" runat="server" CssClass="text-danger d-block mt-3 text-center" />
            <div class="criar-link">
                <asp:HyperLink ID="lnkCriarUsuario" runat="server" NavigateUrl="CadastroUsuario.aspx">Não tem conta? <b>Cadastre-se</b></asp:HyperLink>
            </div>
        </div>
    </form>
    <!-- Bootstrap JS para interações (opcional) -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
