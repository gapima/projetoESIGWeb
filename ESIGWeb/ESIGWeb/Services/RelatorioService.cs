using System.Data;
using ESIGWeb.Data;

namespace ESIGWeb.Services
{
    public class RelatorioService
    {
        public DataTable ObterDadosRelatorio()
        {
            return DatabaseHelper.GetViewData();
        }
    }
}
