using ESIGWeb.Data;
using ESIGWeb.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ESIGWeb.Services
{
    public class ListagemService
    {
        public async Task<DataTable> ObterPessoasSalariosAsync()
        {
            return await DatabaseHelper.ObterPessoasSalariosAsync();
        }

        public async Task ExecutarProcedureCalculoAsync()
        {
            await DatabaseHelper.ExecutarProcedureCalculoAsync();
        }

        public async Task<DataTable> ObterTodosCargosAsync()
        {
            return await DatabaseHelper.ObterTodosCargosAsync();
        }

        public async Task<List<Vencimentos>> ObterDadosFinanceiroPessoaAsync(int cargoId, string tipo)
        {
            return await DatabaseHelper.ObterDadosFinanceiroPessoaAsync(cargoId, tipo);
        }
    }
}
