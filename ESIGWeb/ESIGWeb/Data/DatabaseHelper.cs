using ESIGWeb.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

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
        SELECT p.id             AS Id,
               p.nome           AS Nome,
               p.Cidade         AS Cidade,
               p.Email          AS Email,
               p.CEP            AS CEP,
               p.Endereco       AS Endereco,
               p.Pais           AS Pais,
               p.Usuario        AS Usuario,
               p.Telefone       AS Telefone,
               p.Data_Nascimento AS DataNascimento,
               c.id             AS CargoId,
               c.nome           AS CargoNome
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
                    if (!rdr.Read())
                        return null;

                    // 1) Preenche o objeto básico
                    var pessoa = new Pessoa
                    {
                        Id = rdr.GetInt32(rdr.GetOrdinal("Id")),
                        Nome = rdr.GetString(rdr.GetOrdinal("Nome")),
                        Cidade = rdr.GetString(rdr.GetOrdinal("Cidade")),
                        Email = rdr.GetString(rdr.GetOrdinal("Email")),
                        CEP = rdr.GetString(rdr.GetOrdinal("CEP")),
                        Endereco = rdr.GetString(rdr.GetOrdinal("Endereco")),
                        Pais = rdr.GetString(rdr.GetOrdinal("Pais")),
                        Usuario = rdr.GetString(rdr.GetOrdinal("Usuario")),
                        Telefone = rdr.GetString(rdr.GetOrdinal("Telefone")),
                        DataNascimento = rdr.GetDateTime(rdr.GetOrdinal("DataNascimento")),
                        CargoId = rdr.GetInt32(rdr.GetOrdinal("CargoId")),
                        CargoNome = rdr.GetString(rdr.GetOrdinal("CargoNome"))
                    };

                    // 2) Busca créditos (tipo "C") e débitos (tipo "D") pelo cargo
                    pessoa.Creditos = ObterDadosFinanceiroPessoa(pessoa.CargoId, "C");
                    pessoa.Debitos = ObterDadosFinanceiroPessoa(pessoa.CargoId, "D");

                    return pessoa;
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

        public static List<VencimentoItem> ObterDadosFinanceiroPessoa(int cargoId, string tipo)
        {
            const string sql = @"
                SELECT
                    v.descricao,
                    v.valor,
                    v.forma_incidencia
                FROM Vencimentos v
                JOIN Cargo_Vencimentos cv 
                  ON cv.vencimento_id = v.id
                WHERE cv.cargo_id = :pCargoId
                  AND v.tipo      = :pTipo
                ORDER BY v.id";

            var lista = new List<VencimentoItem>();

            using (var conn = new OracleConnection(ConnectionString))
            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.Parameters.Add("pCargoId", OracleDbType.Int32).Value = cargoId;
                cmd.Parameters.Add("pTipo", OracleDbType.Varchar2).Value = tipo;  // "C" ou "D"

                conn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        lista.Add(new VencimentoItem
                        {
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
