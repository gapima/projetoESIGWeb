using System.Data;
using ESIGWeb.Repository;

namespace ESIGWeb.Services
{
    public class RelatorioService
    {
        private readonly RelatorioRepository _repo = new RelatorioRepository();

        public DataTable ObterDadosRelatorio()
        {
            return _repo.ObterDadosRelatorio();
        }
    }
}
