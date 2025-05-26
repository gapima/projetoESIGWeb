using System.Data;
using ESIGWeb.Data;

namespace ESIGWeb.Repository
{
    public class RelatorioRepository
    {
        public DataTable ObterDadosRelatorio()
        {
            return DatabaseHelper.GetViewData();
        }
    }
}
