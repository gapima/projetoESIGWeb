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
    }
}
