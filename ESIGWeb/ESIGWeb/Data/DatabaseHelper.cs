using ESIGWeb.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;

namespace ESIGWeb.Data
{
    public static class DatabaseHelper
    {
        private static readonly string ConnectionString =
            ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString;


        //public static async Task ExecutarProcedureCalculoAsync()
        //{
        //    using (var conn = new OracleConnection(ConnectionString))
        //    {
        //        await conn.OpenAsync();
        //        using (var cmd = new OracleCommand("SP_CALCULAR_SALARIOS", conn))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            await cmd.ExecuteNonQueryAsync();
        //        }
        //    }
        //}
        //public static async Task<DataTable> ObterPessoasSalariosAsync()
        //{
        //    var dt = new DataTable();
        //    using (var conn = new OracleConnection(ConnectionString))
        //    {
        //        await conn.OpenAsync();

        //        const string sql = @"
        //            SELECT
        //                ps.pessoa_id,
        //                ps.nome,
        //                ps.salario_bruto,
        //                ps.descontos,
        //                ps.salario_liquido,
        //                c.nome     AS nome_cargo
        //            FROM pessoa_salario ps
        //            LEFT JOIN pessoa        p ON p.id = ps.pessoa_id
        //            LEFT JOIN cargo         c ON c.id = p.cargo_id
        //            ORDER BY ps.pessoa_id";

        //        using (var cmd = new OracleCommand(sql, conn))
        //        using (var reader = await cmd.ExecuteReaderAsync())
        //        {
        //            dt.Load(reader);
        //        }
        //    }
        //    return dt;
        //}
        //public static async Task<Pessoa> ObterPessoaAsync(int Id)
        //{
        //    const string sql = @"
        //        SELECT p.id             AS Id,
        //               p.nome           AS Nome,
        //               p.Cidade         AS Cidade,
        //               p.Email          AS Email,
        //               p.CEP            AS CEP,
        //               p.Endereco       AS Endereco,
        //               p.Pais           AS Pais,
        //               p.Usuario        AS Usuario,
        //               p.Telefone       AS Telefone,
        //               p.Data_Nascimento AS DataNascimento,
        //               c.id             AS CargoId,
        //               c.nome           AS CargoNome
        //          FROM pessoa p
        //          JOIN cargo c
        //            ON c.id = p.cargo_id
        //         WHERE p.id = :pId";

        //    using (var conn = new OracleConnection(ConnectionString))
        //    using (var cmd = new OracleCommand(sql, conn))
        //    {
        //        cmd.CommandType = CommandType.Text;
        //        cmd.Parameters.Add("pId", OracleDbType.Int32).Value = Id;
        //        await conn.OpenAsync();

        //        using (var rdr = await cmd.ExecuteReaderAsync())
        //        {
        //            if (!await rdr.ReadAsync())
        //                return null;

        //            var pessoa = new Pessoa
        //            {
        //                Id = rdr.GetInt32(rdr.GetOrdinal("Id")),
        //                Nome = rdr.GetString(rdr.GetOrdinal("Nome")),
        //                CEP = rdr.GetString(rdr.GetOrdinal("CEP")),
        //                Cidade = rdr.GetString(rdr.GetOrdinal("Cidade")),
        //                Email = rdr.GetString(rdr.GetOrdinal("Email")),
        //                Endereco = rdr.GetString(rdr.GetOrdinal("Endereco")),
        //                Pais = rdr.GetString(rdr.GetOrdinal("Pais")),
        //                Usuario = rdr.GetString(rdr.GetOrdinal("Usuario")),
        //                Telefone = rdr.GetString(rdr.GetOrdinal("Telefone")),
        //                DataNascimento = rdr.GetDateTime(rdr.GetOrdinal("DataNascimento")),
        //                CargoId = rdr.GetInt32(rdr.GetOrdinal("CargoId")),
        //                CargoNome = rdr.GetString(rdr.GetOrdinal("CargoNome"))
        //            };

        //            pessoa.Creditos = await ObterDadosFinanceiroPessoaAsync(pessoa.CargoId, "C");
        //            pessoa.Debitos = await ObterDadosFinanceiroPessoaAsync(pessoa.CargoId, "D");

        //            return pessoa;
        //        }
        //    }
        //} 
        //public static async Task<DataTable> ObterTodosCargosAsync()
        //{
        //    var dt = new DataTable();
        //    using (var conn = new OracleConnection(ConnectionString))
        //    {
        //        await conn.OpenAsync();

        //        const string sql = @"
        //            SELECT
        //                id,
        //                nome
        //            FROM cargo
        //            ORDER BY id";

        //        using (var cmd = new OracleCommand(sql, conn))
        //        using (var reader = await cmd.ExecuteReaderAsync())
        //        {
        //            dt.Load(reader);
        //        }
        //    }
        //    return dt;
        //}
        //public static async Task<List<Vencimentos>> ObterDadosFinanceiroPessoaAsync(int cargoId, string tipo)
        //{
        //    const string sql = @"
        //        SELECT
        //            v.id,
        //            v.descricao,
        //            v.valor,
        //            v.forma_incidencia
        //        FROM Vencimentos v
        //        JOIN Cargo_Vencimentos cv 
        //          ON cv.vencimento_id = v.id
        //        WHERE cv.cargo_id = :pCargoId
        //          AND v.tipo      = :pTipo
        //        ORDER BY v.id";

        //    var lista = new List<Vencimentos>();
        //    using (var conn = new OracleConnection(ConnectionString))
        //    using (var cmd = new OracleCommand(sql, conn))
        //    {
        //        cmd.Parameters.Add("pCargoId", OracleDbType.Int32).Value = cargoId;
        //        cmd.Parameters.Add("pTipo", OracleDbType.Varchar2).Value = tipo;
        //        await conn.OpenAsync();

        //        using (var rdr = await cmd.ExecuteReaderAsync())
        //        {
        //            while (await rdr.ReadAsync())
        //            {
        //                lista.Add(new Vencimentos
        //                {
        //                    Id = rdr.GetInt32(rdr.GetOrdinal("id")),
        //                    Descricao = rdr.GetString(rdr.GetOrdinal("descricao")),
        //                    Valor = rdr.GetDecimal(rdr.GetOrdinal("valor")),
        //                    FormaIncidencia = rdr.GetString(rdr.GetOrdinal("forma_incidencia"))
        //                });
        //            }
        //        }
        //    }
        //    return lista;
        //}
        //public static async Task<DataTable> ObterTodosVencimentosAsync()
        //{
        //    const string sql = @"
        //      SELECT id, descricao, valor, forma_incidencia, tipo
        //        FROM vencimentos
        //       ORDER BY descricao";

        //    var dt = new DataTable();
        //    using (var conn = new OracleConnection(ConnectionString))
        //    using (var cmd = new OracleCommand(sql, conn))
        //    {
        //        await conn.OpenAsync();
        //        using (var reader = await cmd.ExecuteReaderAsync())
        //        {
        //            dt.Load(reader);
        //        }
        //    }
        //    return dt;
        //}
        //public static async Task<Vencimentos> ObterVencimentoPorIdAsync(int id)
        //{
        //    const string sql = @"
        //      SELECT id, descricao, valor, forma_incidencia, tipo
        //        FROM vencimentos
        //       WHERE id = :pId";

        //    using (var conn = new OracleConnection(ConnectionString))
        //    using (var cmd = new OracleCommand(sql, conn))
        //    {
        //        cmd.Parameters.Add("pId", OracleDbType.Int32).Value = id;
        //        await conn.OpenAsync();
        //        using (var rdr = await cmd.ExecuteReaderAsync())
        //        {
        //            if (!await rdr.ReadAsync()) return null;

        //            return new Vencimentos
        //            {
        //                Id = rdr.GetInt32(0),
        //                Descricao = rdr.GetString(1),
        //                Valor = rdr.GetDecimal(2),
        //                FormaIncidencia = rdr.GetString(3),
        //                Tipo = rdr.GetString(4)
        //            };
        //        }
        //    }
        //}
        //public static async Task SalvarPessoaAsync(Pessoa p)
        //{
        //    const string sqlUpdate = @"
        //        UPDATE pessoa
        //           SET nome            = :nome,
        //               data_nascimento = :dataNascimento,
        //               email           = :email,
        //               usuario         = :usuario,
        //               cidade          = :cidade,
        //               cep             = :cep,
        //               endereco        = :endereco,
        //               pais            = :pais,
        //               telefone        = :telefone,
        //               cargo_id        = :cargoId
        //         WHERE id = :id";

        //    using (var conn = new OracleConnection(ConnectionString))
        //    using (var cmd = new OracleCommand(sqlUpdate, conn))
        //    {
        //        cmd.CommandType = CommandType.Text;

        //        cmd.Parameters.Add("nome", OracleDbType.Varchar2).Value = p.Nome;
        //        cmd.Parameters.Add("dataNascimento", OracleDbType.Date).Value = p.DataNascimento;
        //        cmd.Parameters.Add("email", OracleDbType.Varchar2).Value = p.Email;
        //        cmd.Parameters.Add("usuario", OracleDbType.Varchar2).Value = p.Usuario;
        //        cmd.Parameters.Add("cidade", OracleDbType.Varchar2).Value = p.Cidade;
        //        cmd.Parameters.Add("cep", OracleDbType.Varchar2).Value = p.CEP;
        //        cmd.Parameters.Add("endereco", OracleDbType.Varchar2).Value = p.Endereco;
        //        cmd.Parameters.Add("pais", OracleDbType.Varchar2).Value = p.Pais;
        //        cmd.Parameters.Add("telefone", OracleDbType.Varchar2).Value = p.Telefone;
        //        cmd.Parameters.Add("cargoId", OracleDbType.Int32).Value = p.CargoId;
        //        cmd.Parameters.Add("id", OracleDbType.Int32).Value = p.Id;

        //        await conn.OpenAsync();
        //        await cmd.ExecuteNonQueryAsync();
        //    }
        //}

        //public static async Task<int> InserirPessoaAsync(Pessoa p)
        //{
        //    const string sql = @"
        //      INSERT INTO pessoa (
        //          nome, data_nascimento, email, usuario,
        //          cidade, cep, endereco, pais, telefone,
        //          cargo_id
        //      ) VALUES (
        //          :nome, :dataNascimento, :email, :usuario,
        //          :cidade, :cep, :endereco, :pais, :telefone,
        //          :cargoId
        //      )";
        //    using (var conn = new OracleConnection(ConnectionString))
        //    using (var cmd = new OracleCommand(sql, conn))
        //    {
        //        cmd.Parameters.Add("nome", OracleDbType.Varchar2).Value = p.Nome;
        //        cmd.Parameters.Add("dataNascimento", OracleDbType.Date).Value = p.DataNascimento;
        //        cmd.Parameters.Add("email", OracleDbType.Varchar2).Value = p.Email;
        //        cmd.Parameters.Add("usuario", OracleDbType.Varchar2).Value = p.Usuario;
        //        cmd.Parameters.Add("cidade", OracleDbType.Varchar2).Value = p.Cidade;
        //        cmd.Parameters.Add("cep", OracleDbType.Varchar2).Value = p.CEP;
        //        cmd.Parameters.Add("endereco", OracleDbType.Varchar2).Value = p.Endereco;
        //        cmd.Parameters.Add("pais", OracleDbType.Varchar2).Value = p.Pais;
        //        cmd.Parameters.Add("telefone", OracleDbType.Varchar2).Value = p.Telefone;
        //        cmd.Parameters.Add("cargoId", OracleDbType.Int32).Value = p.CargoId;
        //        await conn.OpenAsync();
        //        await cmd.ExecuteNonQueryAsync();
        //    }
        //    return 0;
        //}
        //public static async Task InserirCargoVencimentoAsync(int cargoId, int vencimentoId)
        //{
        //    const string sql = @"
        //        INSERT INTO CARGO_VENCIMENTOS (cargo_id, vencimento_id)
        //        VALUES (:cargoId, :vencimentoId)";
        //    using (var conn = new OracleConnection(ConnectionString))
        //    using (var cmd = new OracleCommand(sql, conn))
        //    {
        //        cmd.Parameters.Add("cargoId", OracleDbType.Int32).Value = cargoId;
        //        cmd.Parameters.Add("vencimentoId", OracleDbType.Int32).Value = vencimentoId;
        //        await conn.OpenAsync();
        //        await cmd.ExecuteNonQueryAsync();
        //    }
        //}
        //public static async Task InserirVencimentoAsync(Vencimentos v)
        //{
        //    const string sql = @"
        //        INSERT INTO vencimentos (descricao, valor, forma_incidencia, tipo)
        //        VALUES (:descricao, :valor, :forma, :tipo)";

        //    using (var conn = new OracleConnection(ConnectionString))
        //    using (var cmd = new OracleCommand(sql, conn))
        //    {
        //        cmd.Parameters.Add("descricao", OracleDbType.Varchar2).Value = v.Descricao;
        //        cmd.Parameters.Add("valor", OracleDbType.Decimal).Value = v.Valor;
        //        cmd.Parameters.Add("forma", OracleDbType.Char).Value = v.FormaIncidencia;
        //        cmd.Parameters.Add("tipo", OracleDbType.Char).Value = v.Tipo;

        //        await conn.OpenAsync();
        //        await cmd.ExecuteNonQueryAsync();
        //    }
        //}
        //public static async Task AtualizarVencimentoAsync(Vencimentos v)
        //{
        //    const string sql = @"
        //      UPDATE vencimentos
        //      SET 
        //      valor             = :valor
        //      , forma_incidencia  = :forma
        //      , tipo              = :tipo
        //      WHERE id = :pId";

        //    using (var conn = new OracleConnection(ConnectionString))
        //    using (var cmd = new OracleCommand(sql, conn))
        //    {
        //        cmd.Parameters.Add("valor", OracleDbType.Decimal).Value = v.Valor;
        //        cmd.Parameters.Add("forma", OracleDbType.Char).Value = v.FormaIncidencia;
        //        cmd.Parameters.Add("tipo", OracleDbType.Char).Value = v.Tipo;
        //        cmd.Parameters.Add("pId", OracleDbType.Int32).Value = v.Id;
        //        await conn.OpenAsync();
        //        await cmd.ExecuteNonQueryAsync();
        //    }
        //}
        //public static async Task<List<CargoVencimento>> ObterCargosVinculadosAsync(int vencimentoId)
        //{
        //    const string sql = @"
        //      SELECT id, cargo_id, vencimento_id
        //      FROM cargo_vencimentos
        //      WHERE vencimento_id = :pVid";

        //    var lista = new List<CargoVencimento>();
        //    using (var conn = new OracleConnection(ConnectionString))
        //    using (var cmd = new OracleCommand(sql, conn))
        //    {
        //        cmd.Parameters.Add("pVid", OracleDbType.Int32).Value = vencimentoId;
        //        await conn.OpenAsync();

        //        using (var rdr = await cmd.ExecuteReaderAsync())
        //        {
        //            while (await rdr.ReadAsync())
        //            {
        //                lista.Add(new CargoVencimento
        //                {
        //                    Id = rdr.GetInt32(0),
        //                    CargoId = rdr.GetInt32(1),
        //                    VencimentoId = rdr.GetInt32(2)
        //                });
        //            }
        //        }
        //    }
        //    return lista;
        //}
        //public static async Task VincularCargoAsync(int vencimentoId, int cargoId)
        //{
        //    const string sqlChk = @"
        //        SELECT COUNT(*) FROM cargo_vencimentos
        //        WHERE vencimento_id = :vid AND cargo_id = :cid";
        //    using (var conn = new OracleConnection(ConnectionString))
        //    {
        //        await conn.OpenAsync();
        //        using (var cmd = new OracleCommand(sqlChk, conn))
        //        {
        //            cmd.Parameters.Add("vid", OracleDbType.Int32).Value = vencimentoId;
        //            cmd.Parameters.Add("cid", OracleDbType.Int32).Value = cargoId;
        //            var count = Convert.ToInt32(await cmd.ExecuteScalarAsync());
        //            if (count > 0) return;
        //        }
        //        const string sqlIns = @"
        //          INSERT INTO cargo_vencimentos (cargo_id, vencimento_id)
        //          VALUES (:cid, :vid)";
        //        using (var cmd2 = new OracleCommand(sqlIns, conn))
        //        {
        //            cmd2.Parameters.Add("cid", OracleDbType.Int32).Value = cargoId;
        //            cmd2.Parameters.Add("vid", OracleDbType.Int32).Value = vencimentoId;
        //            await cmd2.ExecuteNonQueryAsync();
        //        }
        //    }
        //}
        //public static async Task DesvincularCargoAsync(int vencimentoId, int cargoId)
        //{
        //    const string sql = @"
        //      DELETE FROM cargo_vencimentos
        //      WHERE vencimento_id = :vid
        //      AND cargo_id      = :cid";
        //    using (var conn = new OracleConnection(ConnectionString))
        //    using (var cmd = new OracleCommand(sql, conn))
        //    {
        //        cmd.Parameters.Add("vid", OracleDbType.Int32).Value = vencimentoId;
        //        cmd.Parameters.Add("cid", OracleDbType.Int32).Value = cargoId;
        //        await conn.OpenAsync();
        //        await cmd.ExecuteNonQueryAsync();
        //    }
        //}
        //public static async Task ExcluirVencimentoAsync(int vencimentoId)
        //{
        //    const string sql = @"
        //        DELETE FROM vencimentos
        //        WHERE id = :vid";
        //    using (var conn = new OracleConnection(ConnectionString))
        //    using (var cmd = new OracleCommand(sql, conn))
        //    {
        //        cmd.Parameters.Add("vid", OracleDbType.Int32).Value = vencimentoId;
        //        await conn.OpenAsync();
        //        await cmd.ExecuteNonQueryAsync();
        //    }
        //}
        public static DataTable GetViewData()
        {
            using (var conn = new OracleConnection(ConnectionString))
            {
                string query = "SELECT * FROM VW_RELATORIO_SALARIOS";
                using (var cmd = new OracleCommand(query, conn))
                {
                    conn.Open();
                    DataTable dt = new DataTable();
                    using (var da = new OracleDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    return dt;
                }
            }
        }

        //public static async Task ExcluirPessoaAsync(int id)
        //{
        //    const string sql = "DELETE FROM pessoa WHERE id = :pId";

        //    using (var conn = new OracleConnection(ConnectionString))
        //    using (var cmd = new OracleCommand(sql, conn))
        //    {
        //        cmd.CommandType = CommandType.Text;
        //        cmd.Parameters.Add("pId", OracleDbType.Int32).Value = id;
        //        await conn.OpenAsync();
        //        await cmd.ExecuteNonQueryAsync();
        //    }
        //}
    }
}
