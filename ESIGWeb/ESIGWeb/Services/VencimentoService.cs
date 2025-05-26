using ESIGWeb.Models;
using ESIGWeb.Repository;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ESIGWeb.Services
{
    public class VencimentoService
    {
        private readonly VencimentoRepository _repo = new VencimentoRepository();

        public async Task<DataTable> ObterTodosVencimentosAsync()
        {
            return await _repo.ObterTodosVencimentosAsync();
        }

        public async Task<Vencimentos> ObterVencimentoPorIdAsync(int id)
        {
            return await _repo.ObterVencimentoPorIdAsync(id);
        }

        public async Task AtualizarVencimentoAsync(Vencimentos v)
        {
            await _repo.AtualizarVencimentoAsync(v);
        }

        public async Task InserirVencimentoAsync(Vencimentos v)
        {
            await _repo.InserirVencimentoAsync(v);
        }

        public async Task ExcluirVencimentoAsync(int id)
        {
            await _repo.ExcluirVencimentoAsync(id);
        }

        public async Task<List<CargoVencimento>> ObterCargosVinculadosAsync(int vencimentoId)
        {
            return await _repo.ObterCargosVinculadosAsync(vencimentoId);
        }

        public async Task VincularCargoAsync(int vencimentoId, int cargoId)
        {
            await _repo.VincularCargoAsync(vencimentoId, cargoId);
        }

        public async Task DesvincularCargoAsync(int vencimentoId, int cargoId)
        {
            await _repo.DesvincularCargoAsync(vencimentoId, cargoId);
        }

        public async Task<DataTable> ObterTodosCargosAsync()
        {
            return await _repo.ObterTodosCargosAsync();
        }
    }
}
