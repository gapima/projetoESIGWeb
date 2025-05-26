using ESIGWeb.Models;
using ESIGWeb.Services;
using System;

namespace ESIGWeb.Pages
{
    public partial class CadastroUsuario : System.Web.UI.Page
    {
        private readonly UsuarioService _usuarioService = new UsuarioService();

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
            lblMensagem.Text = "";
            string login = txtLogin.Text.Trim();
            string nome = txtNome.Text.Trim();
            string email = txtEmail.Text.Trim();
            string senha = txtSenha.Text.Trim();

            if (string.IsNullOrWhiteSpace(login) ||
                string.IsNullOrWhiteSpace(nome) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(senha))
            {
                lblMensagem.Text = "Todos os campos são obrigatórios.";
                return;
            }

            if (await _usuarioService.UsuarioExisteAsync(login, email))
            {
                lblMensagem.Text = "Login ou e-mail já cadastrado!";
                return;
            }

            var usuario = new Usuario
            {
                Login = login,
                Nome = nome,
                Email = email,
                Senha = senha
            };

            bool sucesso = await _usuarioService.InserirUsuarioAsync(usuario);

            if (sucesso)
            {
                ESIGWeb.Utils.WebUtils.SetMensagemGlobal("Usuário cadastrado com sucesso! Faça login para continuar.", "sucesso");
                Response.Redirect("Login.aspx");
            }
            else
            {
                lblMensagem.Text = "Erro ao cadastrar usuário.";
            }
        }
    }
}
