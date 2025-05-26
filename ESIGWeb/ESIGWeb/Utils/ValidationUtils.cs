using ESIGWeb.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;

namespace ESIGWeb.Utils
{
    public static class ValidationUtils
    {
        // Exemplo: retorna lista de erros, se houver, ao tentar criar objeto Pessoa.
        public static Pessoa TryParsePessoa(
            string id, string nome, string dataNascimento, string email, string usuario,
            string cidade, string cep, string endereco, string pais, string telefone, string cargoId,
            out List<string> erros)
        {
            erros = new List<string>();

            int pessoaId = ConversionUtils.ToIntSafe(id);
            if (string.IsNullOrWhiteSpace(nome))
                erros.Add("Nome é obrigatório.");

            DateTime dtNascimento;
            if (!DateTime.TryParseExact(dataNascimento, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtNascimento))
                erros.Add("Data de nascimento inválida.");

            if (string.IsNullOrWhiteSpace(email))
                erros.Add("E-mail é obrigatório.");

            int cargo = ConversionUtils.ToIntSafe(cargoId);
            if (cargo == 0)
                erros.Add("Cargo deve ser selecionado.");

            // ... você pode adicionar outras validações conforme precisar

            if (erros.Count > 0)
                return null;

            return new Pessoa
            {
                Id = pessoaId,
                Nome = nome,
                DataNascimento = dtNascimento,
                Email = email,
                Usuario = usuario,
                Cidade = cidade,
                CEP = cep,
                Endereco = endereco,
                Pais = pais,
                Telefone = telefone,
                CargoId = cargo
            };
        }

        public static Vencimentos TryParseVencimento(
            string id, string descricao, string valor, string formaIncidencia, string tipo,
            out List<string> erros)
        {
            erros = new List<string>();

            int vencId = 0;
            if (!string.IsNullOrWhiteSpace(id))
                vencId = ConversionUtils.ToIntSafe(id);
            
            if (vencId <= 0 && string.IsNullOrWhiteSpace(descricao))
                erros.Add("Descrição é obrigatória.");

            decimal valorDec;
            if (!decimal.TryParse(valor, out valorDec) || valorDec < 0)
                erros.Add("Valor deve ser um número positivo válido.");

            if (string.IsNullOrWhiteSpace(formaIncidencia))
                erros.Add("Selecione a forma de incidência.");

            if (string.IsNullOrWhiteSpace(tipo))
                erros.Add("Selecione o tipo.");

            if (erros.Count > 0)
                return null;

            if (vencId > 0)
                descricao = "";
            return new Vencimentos
            {
                Id = vencId,
                Descricao = descricao.Trim(),
                Valor = valorDec,
                FormaIncidencia = formaIncidencia,
                Tipo = tipo
            };
        }

    }
}
