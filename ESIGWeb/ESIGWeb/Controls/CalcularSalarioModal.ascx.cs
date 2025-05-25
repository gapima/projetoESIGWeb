using ESIGWeb.Data;
using ESIGWeb.Models;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ESIGWeb.Controls
{
    public partial class CalcularSalarioModal : UserControl
    {
        public void PopularCargos()
        {
            var dt = DatabaseHelper.ObterTodosCargos();
            ddlCargo.DataSource = dt;
            ddlCargo.DataValueField = "id";
            ddlCargo.DataTextField = "nome";
            ddlCargo.DataBind();
        }
        protected void ddlCargo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(ddlCargo.SelectedValue, out var cargoId))
            {
                var creditos = DatabaseHelper.ObterDadosFinanceiroPessoa(cargoId, "C");
                gridCreditos.DataSource = creditos;
                gridCreditos.DataBind();

                var debitos = DatabaseHelper.ObterDadosFinanceiroPessoa(cargoId, "D");
                gridDebitos.DataSource = debitos;
                gridDebitos.DataBind();
            }

            updCalcularSalarioBody.Update(); // Atualiza só o conteúdo!
            ScriptManager.RegisterStartupScript(
                this,
                GetType(),
                "showCalculaSalarioModal",
                "var modalEl = document.getElementById('calcularSalarioModal'); if(modalEl){var m=bootstrap.Modal.getOrCreateInstance(modalEl);m.show();}",
                true
            );
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            // 1) id do cargo selecionado
            var cargoId = int.Parse(ddlCargo.SelectedValue);

            // 2) lê arrays de descrição, valor e incidência
            var descs = Request.Form.GetValues("cred_desc") ?? Array.Empty<string>();
            var vals = Request.Form.GetValues("cred_valor") ?? Array.Empty<string>();
            var incs = Request.Form.GetValues("cred_incid") ?? Array.Empty<string>();

            // 3) para cada índice, insere um novo vencimento e o vincula ao cargo
            for (int i = 0; i < descs.Length; i++)
            {
                var v = new Vencimentos
                {
                    Descricao = descs[i],
                    Valor = decimal.Parse(vals[i]),
                    Tipo = "C",
                    FormaIncidencia = incs[i]
                };
                // insere e captura novo id
                var newVencId = DatabaseHelper.InserirVencimento(v);

                // vincula com o cargo
                DatabaseHelper.InserirCargoVencimento(cargoId, newVencId);
            }

             descs = Request.Form.GetValues("deb_desc") ?? Array.Empty<string>();
             vals = Request.Form.GetValues("deb_valor") ?? Array.Empty<string>();
             incs = Request.Form.GetValues("deb_incid") ?? Array.Empty<string>();
            // 2) Agora, percorre débitos (mesma lógica, só muda Tipo="D")
            for (int i = 0; i < descs.Length; i++)
            {
                var v = new Vencimentos
                {
                    Descricao = descs[i],
                    Valor = decimal.Parse(vals[i]),
                    Tipo = "D",
                    FormaIncidencia = incs[i]
                };
                // insere e captura novo id
                var newVencId = DatabaseHelper.InserirVencimento(v);

                // vincula com o cargo
                DatabaseHelper.InserirCargoVencimento(cargoId, newVencId);
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "done",
              "bootstrap.Modal.getInstance(document.getElementById('calcularSalarioModal')).hide();" +
              "__doPostBack('btnCalcular','');", true);
        }

    }
}
