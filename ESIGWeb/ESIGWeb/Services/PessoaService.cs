using ESIGWeb.Models;
using ESIGWeb.Data;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ESIGWeb.Services
{
    public class PessoaService
    {
        public async Task<Pessoa> ObterPessoaAsync(int id)
        {
            return await DatabaseHelper.ObterPessoaAsync(id);
        }

        public async Task SalvarPessoaAsync(Pessoa pessoa)
        {
            if (pessoa.Id == 0)
                await DatabaseHelper.InserirPessoaAsync(pessoa);
            else
                await DatabaseHelper.SalvarPessoaAsync(pessoa);
        }

        public async Task ExcluirPessoaAsync(int id)
        {
            await DatabaseHelper.ExcluirPessoaAsync(id);
        }

        public async Task<DataTable> ObterTodosCargosAsync()
        {
            return await DatabaseHelper.ObterTodosCargosAsync();
        }

        public async Task<List<Vencimentos>> ObterCreditosPorCargoAsync(int cargoId)
        {
            return await DatabaseHelper.ObterDadosFinanceiroPessoaAsync(cargoId, "C");
        }

        public async Task<List<Vencimentos>> ObterDebitosPorCargoAsync(int cargoId)
        {
            return await DatabaseHelper.ObterDadosFinanceiroPessoaAsync(cargoId, "D");
        }
    }
}
