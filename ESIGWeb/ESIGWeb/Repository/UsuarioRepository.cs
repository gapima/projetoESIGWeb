using ESIGWeb.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;


namespace ESIGWeb.Repository
{
    public class UsuarioRepository
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString;

        public Usuario ObterPorLoginSenha(string login, string senha)
        {
            using (var conn = new OracleConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new OracleCommand("SELECT * FROM Usuario WHERE Login = :login AND Senha = :senha", conn))
                {
                    cmd.Parameters.Add(new OracleParameter("login", login));
                    cmd.Parameters.Add(new OracleParameter("senha", senha)); // hash recomendado

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
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
        public bool InserirUsuario(Usuario usuario)
        {
            using (var conn = new OracleConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new OracleCommand(
                    "INSERT INTO Usuario (Login, Nome, Email, Senha) VALUES (:login, :nome, :email, :senha)", conn))
                {
                    cmd.Parameters.Add(new OracleParameter("login", usuario.Login));
                    cmd.Parameters.Add(new OracleParameter("nome", usuario.Nome));
                    cmd.Parameters.Add(new OracleParameter("email", usuario.Email));
                    cmd.Parameters.Add(new OracleParameter("senha", usuario.Senha)); // lembre do hash em produção

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool UsuarioExiste(string login, string email)
        {
            using (var conn = new OracleConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new OracleCommand(
                    "SELECT COUNT(*) FROM Usuario WHERE Login = :login OR Email = :email", conn))
                {
                    cmd.Parameters.Add(new OracleParameter("login", login));
                    cmd.Parameters.Add(new OracleParameter("email", email));

                    var count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }



    }
}