using ESIGWeb.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;

namespace ESIGWeb.Repository
{
    public class PessoaRepository
    {
        private static readonly string ConnectionString =
            ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString;

        public async Task<Pessoa> ObterPessoaAsync(int id)
        {
            const string sql = @"
                SELECT p.id AS Id, p.nome AS Nome, p.Cidade, p.Email, p.CEP, p.Endereco, p.Pais, 
                       p.Usuario, p.Telefone, p.Data_Nascimento AS DataNascimento, 
                       c.id AS CargoId, c.nome AS CargoNome
                FROM pessoa p
                JOIN cargo c ON c.id = p.cargo_id
                WHERE p.id = :pId";

            using (var conn = new OracleConnection(ConnectionString))
            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("pId", OracleDbType.Int32).Value = id;
                await conn.OpenAsync();

                using (var rdr = await cmd.ExecuteReaderAsync())
                {
                    if (!await rdr.ReadAsync())
                        return null;

                    var pessoa = new Pessoa
                    {
                        Id = rdr.GetInt32(rdr.GetOrdinal("Id")),
                        Nome = rdr.GetString(rdr.GetOrdinal("Nome")),
                        CEP = rdr.GetString(rdr.GetOrdinal("CEP")),
                        Cidade = rdr.GetString(rdr.GetOrdinal("Cidade")),
                        Email = rdr.GetString(rdr.GetOrdinal("Email")),
                        Endereco = rdr.GetString(rdr.GetOrdinal("Endereco")),
                        Pais = rdr.GetString(rdr.GetOrdinal("Pais")),
                        Usuario = rdr.GetString(rdr.GetOrdinal("Usuario")),
                        Telefone = rdr.GetString(rdr.GetOrdinal("Telefone")),
                        DataNascimento = rdr.GetDateTime(rdr.GetOrdinal("DataNascimento")),
                        CargoId = rdr.GetInt32(rdr.GetOrdinal("CargoId")),
                        CargoNome = rdr.GetString(rdr.GetOrdinal("CargoNome"))
                    };

                    // Busca créditos e débitos
                    pessoa.Creditos = await ObterDadosFinanceiroPessoaAsync(pessoa.CargoId, "C");
                    pessoa.Debitos = await ObterDadosFinanceiroPessoaAsync(pessoa.CargoId, "D");

                    return pessoa;
                }
            }
        }

        public async Task SalvarPessoaAsync(Pessoa p)
        {
            if (p.Id == 0)
                await InserirPessoaAsync(p);
            else {
                int affected = await AtualizarPessoaAsync(p);
                if (affected == 0)
                    throw new Exception("Nenhuma linha foi alterada. Verifique o ID!");
            }
        }

    public static async Task<int> AtualizarPessoaAsync(Pessoa p)
    {
        const string sqlUpdate = @"
            UPDATE pessoa
               SET nome            = :nome,
                   data_nascimento = :dataNascimento,
                   email           = :email,
                   usuario         = :usuario,
                   cidade          = :cidade,
                   cep             = :cep,
                   endereco        = :endereco,
                   pais            = :pais,
                   telefone        = :telefone,
                   cargo_id        = :cargoId
             WHERE id = :id";

        using (var conn = new OracleConnection(ConnectionString))
        using (var cmd = new OracleCommand(sqlUpdate, conn))
        {
            cmd.CommandType = CommandType.Text;
            // ... (parâmetros igual ao seu código)
            cmd.Parameters.Add("nome", OracleDbType.Varchar2).Value = p.Nome;
            cmd.Parameters.Add("dataNascimento", OracleDbType.Date).Value = p.DataNascimento;
            cmd.Parameters.Add("email", OracleDbType.Varchar2).Value = p.Email;
            cmd.Parameters.Add("usuario", OracleDbType.Varchar2).Value = p.Usuario;
            cmd.Parameters.Add("cidade", OracleDbType.Varchar2).Value = p.Cidade;
            cmd.Parameters.Add("cep", OracleDbType.Varchar2).Value = p.CEP;
            cmd.Parameters.Add("endereco", OracleDbType.Varchar2).Value = p.Endereco;
            cmd.Parameters.Add("pais", OracleDbType.Varchar2).Value = p.Pais;
            cmd.Parameters.Add("telefone", OracleDbType.Varchar2).Value = p.Telefone;
            cmd.Parameters.Add("cargoId", OracleDbType.Int32).Value = p.CargoId;
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = p.Id;

            await conn.OpenAsync();
            int affected = await cmd.ExecuteNonQueryAsync();
            return affected;
        }
}
        private async Task InserirPessoaAsync(Pessoa p)
        {
            const string sql = @"
                INSERT INTO pessoa (
                    nome, data_nascimento, email, usuario, cidade, cep, endereco, pais, telefone, cargo_id
                ) VALUES (
                    :nome, :dataNascimento, :email, :usuario, :cidade, :cep, :endereco, :pais, :telefone, :cargoId
                )";
            using (var conn = new OracleConnection(ConnectionString))
            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.Parameters.Add("nome", OracleDbType.Varchar2).Value = p.Nome;
                cmd.Parameters.Add("dataNascimento", OracleDbType.Date).Value = p.DataNascimento;
                cmd.Parameters.Add("email", OracleDbType.Varchar2).Value = p.Email;
                cmd.Parameters.Add("usuario", OracleDbType.Varchar2).Value = p.Usuario;
                cmd.Parameters.Add("cidade", OracleDbType.Varchar2).Value = p.Cidade;
                cmd.Parameters.Add("cep", OracleDbType.Varchar2).Value = p.CEP;
                cmd.Parameters.Add("endereco", OracleDbType.Varchar2).Value = p.Endereco;
                cmd.Parameters.Add("pais", OracleDbType.Varchar2).Value = p.Pais;
                cmd.Parameters.Add("telefone", OracleDbType.Varchar2).Value = p.Telefone;
                cmd.Parameters.Add("cargoId", OracleDbType.Int32).Value = p.CargoId;
                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task ExcluirPessoaAsync(int id)
        {
            const string sql = "DELETE FROM pessoa WHERE id = :pId";

            using (var conn = new OracleConnection(ConnectionString))
            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("pId", OracleDbType.Int32).Value = id;
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

                const string sql = @"
                    SELECT id, nome FROM cargo ORDER BY id";

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
                SELECT v.id, v.descricao, v.valor, v.forma_incidencia
                FROM Vencimentos v
                JOIN Cargo_Vencimentos cv ON cv.vencimento_id = v.id
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
