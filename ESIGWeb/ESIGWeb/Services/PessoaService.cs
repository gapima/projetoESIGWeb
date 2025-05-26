using ESIGWeb.Models;
using ESIGWeb.Repository;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ESIGWeb.Services
{
    public class PessoaService
    {
        private readonly PessoaRepository _repository = new PessoaRepository();

        public async Task<Pessoa> ObterPessoaAsync(int id)
        {
            return await _repository.ObterPessoaAsync(id);
        }

        public async Task SalvarPessoaAsync(Pessoa pessoa)
        {
            await _repository.SalvarPessoaAsync(pessoa);
        }

        public async Task ExcluirPessoaAsync(int id)
        {
            await _repository.ExcluirPessoaAsync(id);
        }

        public async Task<DataTable> ObterTodosCargosAsync()
        {
            return await _repository.ObterTodosCargosAsync();
        }

        public async Task<List<Vencimentos>> ObterCreditosPorCargoAsync(int cargoId)
        {
            return await _repository.ObterDadosFinanceiroPessoaAsync(cargoId, "C");
        }

        public async Task<List<Vencimentos>> ObterDebitosPorCargoAsync(int cargoId)
        {
            return await _repository.ObterDadosFinanceiroPessoaAsync(cargoId, "D");
        }
    }
}
