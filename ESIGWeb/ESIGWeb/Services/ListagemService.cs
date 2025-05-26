using ESIGWeb.Models;
using ESIGWeb.Repository;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ESIGWeb.Services
{
    public class ListagemService
    {
        private readonly ListagemRepository _repository;

        public ListagemService()
        {
            _repository = new ListagemRepository();
        }

        public async Task<DataTable> ObterPessoasSalariosAsync()
        {
            return await _repository.ObterPessoasSalariosAsync();
        }

        public async Task ExecutarProcedureCalculoAsync()
        {
            await _repository.ExecutarProcedureCalculoAsync();
        }

        public async Task<DataTable> ObterTodosCargosAsync()
        {
            return await _repository.ObterTodosCargosAsync();
        }

        public async Task<List<Vencimentos>> ObterDadosFinanceiroPessoaAsync(int cargoId, string tipo)
        {
            return await _repository.ObterDadosFinanceiroPessoaAsync(cargoId, tipo);
        }
    }
}
