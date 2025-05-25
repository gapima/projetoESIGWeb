using ESIGWeb.Data;
using System;
using System.Web.UI;

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


    }
}
