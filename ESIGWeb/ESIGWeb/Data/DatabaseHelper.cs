using System;
using System.Data;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;

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
                using (var cmd = new OracleCommand("calcular_salarios", conn))
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
                        pessoa_id,
                        nome,
                        salario_bruto,
                        descontos,
                        salario_liquido
                    FROM pessoa_salario
                    ORDER BY pessoa_id";

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
