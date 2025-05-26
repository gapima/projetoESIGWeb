using ESIGWeb.Models;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;

namespace ESIGWeb.Repository
{
    public class ListagemRepository
    {
        private static readonly string ConnectionString =
            ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString;

        public async Task<DataTable> ObterPessoasSalariosAsync()
        {
            var dt = new DataTable();
            using (var conn = new OracleConnection(ConnectionString))
            {
                await conn.OpenAsync();

                const string sql = @"
                    SELECT
                        ps.pessoa_id,
                        ps.nome,
                        ps.salario_bruto,
                        ps.descontos,
                        ps.salario_liquido,
                        c.nome     AS nome_cargo
                    FROM pessoa_salario ps
                    LEFT JOIN pessoa        p ON p.id = ps.pessoa_id
                    LEFT JOIN cargo         c ON c.id = p.cargo_id
                    ORDER BY ps.pessoa_id";

                using (var cmd = new OracleCommand(sql, conn))
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    dt.Load(reader);
                }
            }
            return dt;
        }

        public async Task ExecutarProcedureCalculoAsync()
        {
            using (var conn = new OracleConnection(ConnectionString))
            {
                await conn.OpenAsync();
                using (var cmd = new OracleCommand("SP_CALCULAR_SALARIOS", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<DataTable> ObterTodosCargosAsync()
        {
            var dt = new DataTable();
            using (var conn = new OracleConnection(ConnectionString))
            {
                await conn.OpenAsync();

                const string sql = @"
                    SELECT
                        id,
                        nome
                    FROM cargo
                    ORDER BY id";

                using (var cmd = new OracleCommand(sql, conn))
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    dt.Load(reader);
                }
            }
            return dt;
        }

        public async Task<List<Vencimentos>> ObterDadosFinanceiroPessoaAsync(int cargoId, string tipo)
        {
            const string sql = @"
                SELECT
                    v.id,
                    v.descricao,
                    v.valor,
                    v.forma_incidencia
                FROM Vencimentos v
                JOIN Cargo_Vencimentos cv 
                  ON cv.vencimento_id = v.id
                WHERE cv.cargo_id = :pCargoId
                  AND v.tipo      = :pTipo
                ORDER BY v.id";

            var lista = new List<Vencimentos>();
            using (var conn = new OracleConnection(ConnectionString))
            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.Parameters.Add("pCargoId", OracleDbType.Int32).Value = cargoId;
                cmd.Parameters.Add("pTipo", OracleDbType.Varchar2).Value = tipo;
                await conn.OpenAsync();

                using (var rdr = await cmd.ExecuteReaderAsync())
                {
                    while (await rdr.ReadAsync())
                    {
                        lista.Add(new Vencimentos
                        {
                            Id = rdr.GetInt32(rdr.GetOrdinal("id")),
                            Descricao = rdr.GetString(rdr.GetOrdinal("descricao")),
                            Valor = rdr.GetDecimal(rdr.GetOrdinal("valor")),
                            FormaIncidencia = rdr.GetString(rdr.GetOrdinal("forma_incidencia"))
                        });
                    }
                }
            }
            return lista;
        }
    }
}
