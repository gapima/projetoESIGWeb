using System;
using System.Data;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;
using ESIGWeb.Models;

namespace ESIGWeb.Data
{
    public static class DatabaseHelper
    {
        // Lê a connection string do Web.config
        private static readonly string ConnectionString =
            ConfigurationManager
                .ConnectionStrings["OracleConnection"]
                .ConnectionString;

        /// <summary>
        /// Executa a procedure SP_CALCULAR_SALARIOS no Oracle,
        /// que preenche ou atualiza a tabela pessoa_salario.
        /// </summary>
        public static void ExecutarProcedureCalculo()
        {
            using (var conn = new OracleConnection(ConnectionString))
            {
                conn.Open();
                using (var cmd = new OracleCommand("SP_CALCULAR_SALARIOS", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Retorna todos os registros da tabela pessoa_salario,
        /// já ordenados por pessoa_id, prontos para exibir no GridView.
        /// </summary>
        public static DataTable ObterPessoasSalarios()
        {
            var dt = new DataTable();

            using (var conn = new OracleConnection(ConnectionString))
            {
                conn.Open();

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
                using (var adapter = new OracleDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }

            return dt;
        }

        public static Pessoa ObterPessoa(int Id)
        {
            const string sql = @"
                SELECT p.id AS Id,
                       p.nome AS Nome,
                       p.Cidade AS Cidade,
                       p.Email AS Email,
                       p.CEP AS CEP,
                       p.Endereco AS Endereco,
                       p.Pais AS Pais,
                       p.Usuario AS Usuario,
                       p.Telefone AS Telefone,
                       p.Data_Nascimento AS DataNascimento,
                       c.id AS CargoId,
                       c.nome AS CargoNome
                  FROM pessoa p
                  JOIN cargo c
                    ON c.id = p.cargo_id
                 WHERE p.id = :pId";

            using (var conn = new OracleConnection(ConnectionString))
            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("pId", OracleDbType.Int32).Value = Id;
                conn.Open();

                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        return new Pessoa
                        {
                            Id = rdr.GetInt32(0),
                            Nome = rdr.GetString(1),
                            Cidade = rdr.GetString(2),
                            Email = rdr.GetString(3),
                            CEP = rdr.GetString(4),
                            Endereco = rdr.GetString(5),
                            Pais = rdr.GetString(6),
                            Usuario = rdr.GetString(7),
                            Telefone = rdr.GetString(8),
                            DataNascimento = rdr.GetDateTime(9),
                            CargoId = rdr.GetInt32(10),
                            CargoNome = rdr.GetString(11)
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public static DataTable ObterTodosCargos()
        {
            var dt = new DataTable();

            using (var conn = new OracleConnection(ConnectionString))
            {
                conn.Open();

                const string sql = @"
                    SELECT
                        id,
                        nome
                    FROM cargo
                    ORDER BY id";

                using (var cmd = new OracleCommand(sql, conn))
                using (var adapter = new OracleDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }

            return dt;
        }
    }
}
