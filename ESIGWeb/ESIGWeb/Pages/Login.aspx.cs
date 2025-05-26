using System;
using ESIGWeb.Services;

namespace ESIGWeb.Pages
{
    public partial class Login : System.Web.UI.Page
    {
        private readonly UsuarioService _usuarioService = new UsuarioService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioLogado"] != null)
            {
                Response.Redirect("/Pages/Listagem.aspx");
                return;
            }
        }

        protected async void btnLogin_Click(object sender, EventArgs e)
        {
            lblMensagem.Text = "";
            btnLogin.Enabled = false;

            string login = txtLogin.Text.Trim();
            string senha = txtSenha.Text.Trim();

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(senha))
            {
                lblMensagem.Text = "Preencha todos os campos!";
                btnLogin.Enabled = true;
                return;
            }

            try
            {
                var usuario = await _usuarioService.AutenticarAsync(login, senha);

                if (usuario != null)
                {
                    Session["UsuarioLogado"] = usuario;
                    ESIGWeb.Utils.WebUtils.SetMensagemGlobal($"Bem-vindo, {usuario.Nome}!", "sucesso");
                    Response.Redirect("/Pages/Listagem.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    lblMensagem.Text = "Usuário ou senha inválidos!";
                    btnLogin.Enabled = true;
                }
            }
            catch (Exception)
            {
                lblMensagem.Text = "Erro ao tentar logar. Tente novamente.";
                btnLogin.Enabled = true;
            }
        }
    }
}
