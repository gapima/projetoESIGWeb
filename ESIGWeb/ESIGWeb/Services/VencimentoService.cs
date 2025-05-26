using ESIGWeb.Data;
using ESIGWeb.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ESIGWeb.Services
{
    public class VencimentoService
    {
        public async Task<DataTable> ObterTodosVencimentosAsync()
        {
            return await DatabaseHelper.ObterTodosVencimentosAsync();
        }

        public async Task<Vencimentos> ObterVencimentoPorIdAsync(int id)
        {
            return await DatabaseHelper.ObterVencimentoPorIdAsync(id);
        }

        public async Task AtualizarVencimentoAsync(Vencimentos v)
        {
            await DatabaseHelper.AtualizarVencimentoAsync(v);
        }

        public async Task InserirVencimentoAsync(Vencimentos v)
        {
            await DatabaseHelper.InserirVencimentoAsync(v);
        }

        public async Task ExcluirVencimentoAsync(int id)
        {
            await DatabaseHelper.ExcluirVencimentoAsync(id);
        }

        public async Task<List<CargoVencimento>> ObterCargosVinculadosAsync(int vencimentoId)
        {
            return await DatabaseHelper.ObterCargosVinculadosAsync(vencimentoId);
        }

        public async Task VincularCargoAsync(int vencimentoId, int cargoId)
        {
            await DatabaseHelper.VincularCargoAsync(vencimentoId, cargoId);
        }

        public async Task DesvincularCargoAsync(int vencimentoId, int cargoId)
        {
            await DatabaseHelper.DesvincularCargoAsync(vencimentoId, cargoId);
        }

        public async Task<DataTable> ObterTodosCargosAsync()
        {
            return await DatabaseHelper.ObterTodosCargosAsync();
        }
    }
}
