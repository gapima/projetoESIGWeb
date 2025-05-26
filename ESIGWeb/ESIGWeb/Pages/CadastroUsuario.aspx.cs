using System;
using ESIGWeb.Repository;
using ESIGWeb.Models;

namespace ESIGWeb.Pages
{
    public partial class CadastroUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioLogado"] != null)
            {
                Response.Redirect("Listagem.aspx");
                return;
            }
        }
        protected async void btnCadastrar_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string nome = txtNome.Text.Trim();
            string email = txtEmail.Text.Trim();
            string senha = txtSenha.Text.Trim();

            var repo = new UsuarioRepository();

            if (await repo.UsuarioExisteAsync(login, email))
            {
                lblMensagem.Text = "Login ou e-mail já cadastrado!";
                return;
            }

            var usuario = new Usuario { Login = login, Nome = nome, Email = email, Senha = senha };

            bool sucesso = await repo.InserirUsuarioAsync(usuario);

            if (sucesso)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                lblMensagem.Text = "Erro ao cadastrar usuário.";
            }
        }
    }
}
