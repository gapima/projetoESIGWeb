using ESIGWeb.Models;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;

namespace ESIGWeb.Repository
{
    public class VencimentoRepository
    {
        private readonly string ConnectionString =
            ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString;

        public async Task<DataTable> ObterTodosVencimentosAsync()
        {
            const string sql = @"SELECT id, descricao, valor, forma_incidencia, tipo
                                 FROM vencimentos
                                 ORDER BY descricao";

            var dt = new DataTable();
            using (var conn = new OracleConnection(ConnectionString))
            using (var cmd = new OracleCommand(sql, conn))
            {
                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    dt.Load(reader);
                }
            }
            return dt;
        }

        public async Task<Vencimentos> ObterVencimentoPorIdAsync(int id)
        {
            const string sql = @"SELECT id, descricao, valor, forma_incidencia, tipo
                                 FROM vencimentos
                                 WHERE id = :pId";

            using (var conn = new OracleConnection(ConnectionString))
            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.Parameters.Add("pId", OracleDbType.Int32).Value = id;
                await conn.OpenAsync();
                using (var rdr = await cmd.ExecuteReaderAsync())
                {
                    if (!await rdr.ReadAsync()) return null;

                    return new Vencimentos
                    {
                        Id = rdr.GetInt32(0),
                        Descricao = rdr.GetString(1),
                        Valor = rdr.GetDecimal(2),
                        FormaIncidencia = rdr.GetString(3),
                        Tipo = rdr.GetString(4)
                    };
                }
            }
        }

        public async Task AtualizarVencimentoAsync(Vencimentos v)
        {
            const string sql = @"UPDATE vencimentos
                                 SET valor = :valor,
                                     forma_incidencia = :forma,
                                     tipo = :tipo
                                 WHERE id = :pId";
            using (var conn = new OracleConnection(ConnectionString))
            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.Parameters.Add("valor", OracleDbType.Decimal).Value = v.Valor;
                cmd.Parameters.Add("forma", OracleDbType.Char).Value = v.FormaIncidencia;
                cmd.Parameters.Add("tipo", OracleDbType.Char).Value = v.Tipo;
                cmd.Parameters.Add("pId", OracleDbType.Int32).Value = v.Id;
                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task InserirVencimentoAsync(Vencimentos v)
        {
            const string sql = @"INSERT INTO vencimentos (descricao, valor, forma_incidencia, tipo)
                                 VALUES (:descricao, :valor, :forma, :tipo)";
            using (var conn = new OracleConnection(ConnectionString))
            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.Parameters.Add("descricao", OracleDbType.Varchar2).Value = v.Descricao;
                cmd.Parameters.Add("valor", OracleDbType.Decimal).Value = v.Valor;
                cmd.Parameters.Add("forma", OracleDbType.Char).Value = v.FormaIncidencia;
                cmd.Parameters.Add("tipo", OracleDbType.Char).Value = v.Tipo;
                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task ExcluirVencimentoAsync(int id)
        {
            const string sql = @"DELETE FROM vencimentos WHERE id = :vid";
            using (var conn = new OracleConnection(ConnectionString))
            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.Parameters.Add("vid", OracleDbType.Int32).Value = id;
                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<List<CargoVencimento>> ObterCargosVinculadosAsync(int vencimentoId)
        {
            const string sql = @"SELECT id, cargo_id, vencimento_id
                                 FROM cargo_vencimentos
                                 WHERE vencimento_id = :pVid";
            var lista = new List<CargoVencimento>();
            using (var conn = new OracleConnection(ConnectionString))
            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.Parameters.Add("pVid", OracleDbType.Int32).Value = vencimentoId;
                await conn.OpenAsync();
                using (var rdr = await cmd.ExecuteReaderAsync())
                {
                    while (await rdr.ReadAsync())
                    {
                        lista.Add(new CargoVencimento
                        {
                            Id = rdr.GetInt32(0),
                            CargoId = rdr.GetInt32(1),
                            VencimentoId = rdr.GetInt32(2)
                        });
                    }
                }
            }
            return lista;
        }

        public async Task VincularCargoAsync(int vencimentoId, int cargoId)
        {
            const string sqlChk = @"SELECT COUNT(*) FROM cargo_vencimentos
                                    WHERE vencimento_id = :vid AND cargo_id = :cid";
            using (var conn = new OracleConnection(ConnectionString))
            {
                await conn.OpenAsync();
                using (var cmd = new OracleCommand(sqlChk, conn))
                {
                    cmd.Parameters.Add("vid", OracleDbType.Int32).Value = vencimentoId;
                    cmd.Parameters.Add("cid", OracleDbType.Int32).Value = cargoId;
                    var count = System.Convert.ToInt32(await cmd.ExecuteScalarAsync());
                    if (count > 0) return;
                }
                const string sqlIns = @"INSERT INTO cargo_vencimentos (cargo_id, vencimento_id)
                                        VALUES (:cid, :vid)";
                using (var cmd2 = new OracleCommand(sqlIns, conn))
                {
                    cmd2.Parameters.Add("cid", OracleDbType.Int32).Value = cargoId;
                    cmd2.Parameters.Add("vid", OracleDbType.Int32).Value = vencimentoId;
                    await cmd2.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DesvincularCargoAsync(int vencimentoId, int cargoId)
        {
            const string sql = @"DELETE FROM cargo_vencimentos
                                 WHERE vencimento_id = :vid
                                 AND cargo_id      = :cid";
            using (var conn = new OracleConnection(ConnectionString))
            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.Parameters.Add("vid", OracleDbType.Int32).Value = vencimentoId;
                cmd.Parameters.Add("cid", OracleDbType.Int32).Value = cargoId;
                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<DataTable> ObterTodosCargosAsync()
        {
            var dt = new DataTable();
            using (var conn = new OracleConnection(ConnectionString))
            {
                await conn.OpenAsync();

                const string sql = @"SELECT id, nome FROM cargo ORDER BY id";

                using (var cmd = new OracleCommand(sql, conn))
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    dt.Load(reader);
                }
            }
            return dt;
        }
    }
}
