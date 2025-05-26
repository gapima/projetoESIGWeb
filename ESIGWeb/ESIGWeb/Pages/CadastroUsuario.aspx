<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CadastroUsuario.aspx.cs" Inherits="ESIGWeb.Pages.CadastroUsuario" Async="true"%>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Cadastro de Usuário</title>
    <!-- Bootstrap CDN -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background: #f8fafc;
            min-height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
        }
        .cadastro-card {
            width: 100%;
            max-width: 410px;
            border-radius: 1.4rem;
            box-shadow: 0 8px 24px rgba(0,0,0,.08);
            background: #fff;
            padding: 2.5rem 2.2rem 2rem 2.2rem;
        }
        .cadastro-title {
            font-size: 2rem;
            font-weight: 700;
            margin-bottom: 1.3rem;
            text-align: center;
            color: #222;
            letter-spacing: -1px;
        }
        .form-label {
            font-weight: 500;
        }
        .btn-cadastrar {
            border-radius: 2rem;
            font-size: 1.13rem;
            padding: 0.6rem 0;
            margin-top: 1.1rem;
        }
        .login-link {
            text-align: center;
            margin-top: 1.3rem;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="cadastro-card mx-auto">
            <div class="cadastro-title">Criar nova conta</div>
            <div class="mb-3">
                <asp:Label ID="lblLogin" runat="server" AssociatedControlID="txtLogin" CssClass="form-label" Text="Usuário" />
                <asp:TextBox ID="txtLogin" runat="server" CssClass="form-control form-control-lg" placeholder="Escolha um usuário" />
            </div>
            <div class="mb-3">
                <asp:Label ID="lblNome" runat="server" AssociatedControlID="txtNome" CssClass="form-label" Text="Nome completo" />
                <asp:TextBox ID="txtNome" runat="server" CssClass="form-control form-control-lg" placeholder="Digite seu nome" />
            </div>
            <div class="mb-3">
                <asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtEmail" CssClass="form-label" Text="E-mail" />
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control form-control-lg" placeholder="Digite seu e-mail" />
            </div>
            <div class="mb-3">
                <asp:Label ID="lblSenha" runat="server" AssociatedControlID="txtSenha" CssClass="form-label" Text="Senha" />
                <div class="input-group">
                    <asp:TextBox ID="txtSenha" runat="server" TextMode="Password" CssClass="form-control form-control-lg" placeholder="Crie uma senha" />
                    <span class="input-group-text">
                        <svg xmlns="http://www.w3.org/2000/svg" width="18" height="18" fill="currentColor" class="bi bi-lock" viewBox="0 0 16 16">
                            <path d="M8 1a4 4 0 0 0-4 4v3a2 2 0 0 0-2 2v3a2 2 0 0 0 2 2h8a2 2 0 0 0 2-2v-3a2 2 0 0 0-2-2V5a4 4 0 0 0-4-4zm-3 4a3 3 0 1 1 6 0v3H5V5zm8 8a1 1 0 0 1-1 1H4a1 1 0 0 1-1-1v-3a1 1 0 0 1 1-1h8a1 1 0 0 1 1 1v3z"/>
                        </svg>
                    </span>
                </div>
            </div>
            <asp:Button ID="btnCadastrar" runat="server" CssClass="btn btn-success w-100 btn-cadastrar" Text="Cadastrar" OnClick="btnCadastrar_Click" />
            <asp:Label ID="lblMensagem" runat="server" CssClass="text-danger d-block mt-3 text-center" />
            <div class="login-link">
                <asp:HyperLink ID="lnkLogin" runat="server" NavigateUrl="Login.aspx">Já tem conta? <b>Entrar</b></asp:HyperLink>
            </div>
        </div>
    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
