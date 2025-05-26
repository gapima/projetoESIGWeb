using ESIGWeb.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace ESIGWeb.Repository
{
    public class UsuarioRepository
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString;

        public async Task<Usuario> ObterPorLoginSenhaAsync(string login, string senha)
        {
            using (var conn = new OracleConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var cmd = new OracleCommand("SELECT * FROM Usuario WHERE Login = :login AND Senha = :senha", conn))
                {
                    cmd.Parameters.Add(new OracleParameter("login", login));
                    cmd.Parameters.Add(new OracleParameter("senha", senha)); 

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Usuario
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Login = reader["Login"].ToString(),
                                Nome = reader["Nome"].ToString(),
                                Email = reader["Email"].ToString(),
                                Senha = reader["Senha"].ToString()
                            };
                        }
                    }
                }
            }
            return null;
        }

        public async Task<bool> InserirUsuarioAsync(Usuario usuario)
        {
            using (var conn = new OracleConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var cmd = new OracleCommand(
                    "INSERT INTO Usuario (Login, Nome, Email, Senha) VALUES (:login, :nome, :email, :senha)", conn))
                {
                    cmd.Parameters.Add(new OracleParameter("login", usuario.Login));
                    cmd.Parameters.Add(new OracleParameter("nome", usuario.Nome));
                    cmd.Parameters.Add(new OracleParameter("email", usuario.Email));
                    cmd.Parameters.Add(new OracleParameter("senha", usuario.Senha)); 

                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
        }

        public async Task<bool> UsuarioExisteAsync(string login, string email)
        {
            using (var conn = new OracleConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var cmd = new OracleCommand(
                    "SELECT COUNT(*) FROM Usuario WHERE Login = :login OR Email = :email", conn))
                {
                    cmd.Parameters.Add(new OracleParameter("login", login));
                    cmd.Parameters.Add(new OracleParameter("email", email));

                    var count = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                    return count > 0;
                }
            }
        }
    }
}
