using ESIGWeb.Models;
using ESIGWeb.Repository;
using System.Threading.Tasks;

namespace ESIGWeb.Services
{
    public class UsuarioService
    {
        private readonly UsuarioRepository _repo = new UsuarioRepository();

        public async Task<Usuario> AutenticarAsync(string login, string senha)
        {
            return await _repo.ObterPorLoginSenhaAsync(login, senha);
        }

        public async Task<bool> UsuarioExisteAsync(string login, string email)
        {
            return await _repo.UsuarioExisteAsync(login, email);
        }

        public async Task<bool> InserirUsuarioAsync(Usuario usuario)
        {
            return await _repo.InserirUsuarioAsync(usuario);
        }
    }
}
