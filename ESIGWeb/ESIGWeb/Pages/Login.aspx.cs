using System;
using ESIGWeb.Repository;

namespace ESIGWeb.Pages
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Se já está logado, vai pra listagem (NÃO precisa async aqui)
            if (Session["UsuarioLogado"] != null)
            {
                Response.Redirect("/Pages/Listagem.aspx");
                // NÃO precisa do CompleteRequest aqui
                return;
            }
        }

        protected async void btnLogin_Click(object sender, EventArgs e)
        {
            lblMensagem.Text = "";
            btnLogin.Enabled = false; // desativa o botão ao entrar

            string login = txtLogin.Text.Trim();
            string senha = txtSenha.Text.Trim();

            var repo = new UsuarioRepository();
            var usuario = await repo.ObterPorLoginSenhaAsync(login, senha);

            if (usuario != null)
            {
                Session["UsuarioLogado"] = usuario;
                Response.Redirect("/Pages/Listagem.aspx", false); // false evita ThreadAbortException, mas não obrigatório no Click
                Context.ApplicationInstance.CompleteRequest(); // opcional, mas pode deixar se quiser
            }
            else
            {
                lblMensagem.Text = "Usuário ou senha inválidos!";
                btnLogin.Enabled = true;
            }
        }
    }
}
