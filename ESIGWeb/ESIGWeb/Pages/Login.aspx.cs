using System;
using ESIGWeb.Models; // ajuste o namespace se for diferente
using ESIGWeb.Repository; // onde está seu UsuarioRepository

namespace ESIGWeb.Pages
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Se já está logado, redireciona para Listagem
            if (Session["UsuarioLogado"] != null)
                Response.Redirect("Listagem.aspx"); // ou "Pages/Listagem.aspx"
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string senha = txtSenha.Text.Trim();

            var repo = new UsuarioRepository();
            var usuario = repo.ObterPorLoginSenha(login, senha); // método novo

            if (usuario != null)
            {
                Session["UsuarioLogado"] = usuario;
                Response.Redirect("Listagem.aspx");
            }
            else
            {
                lblMensagem.Text = "Usuário ou senha inválidos!";
            }
        }

    }
}
