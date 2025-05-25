using ESIGWeb.Data;
using ESIGWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ESIGWeb.Controls
{

    public partial class VincularVencimentosModal : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CarregarDropdowns();
        }

        public void CarregarDropdowns()
        {
            // popula vencimentos
            ddlVencimentos.DataSource = DatabaseHelper.ObterTodosVencimentos();
            ddlVencimentos.DataTextField = "Descricao";
            ddlVencimentos.DataValueField = "Id";
            ddlVencimentos.DataBind();
            ddlVencimentos.Items.Insert(0, new ListItem("-- selecione --", ""));

            // popula lista de cargos (marcados ou não)
            PreencherCargos();
        }

        protected void ddlVencimentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(ddlVencimentos.SelectedValue, out var vid))
            {
                var v = DatabaseHelper.ObterVencimento(vid);
                txtValor.Text = v.Valor.ToString("F2");
                ddlForma.SelectedValue = v.FormaIncidencia;
                ddlTipo.SelectedValue = v.Tipo;
            }
            PreencherCargos();
            // reabre a modal após postback
            ScriptManager.RegisterStartupScript(
                this, GetType(),
                "showVincular",
                "new bootstrap.Modal(document.getElementById('vincularVencModal')).show();",
                true);
        }

        private void PreencherCargos()
        {
            // obtém todos os cargos do banco
            var todosDt = DatabaseHelper.ObterTodosCargos();
            var todasRows = todosDt.Rows.Cast<DataRow>();

            // prepara o conjunto de IDs já vinculados (se houver um vencimento selecionado)
            var vinculados = new HashSet<int>();
            if (int.TryParse(ddlVencimentos.SelectedValue, out var vid))
            {
                foreach (var cv in DatabaseHelper.ObterCargosVinculados(vid))
                    vinculados.Add(cv.CargoId);
            }

            // transforma cada DataRow num objeto anônimo com Id, Nome e Vinculado
            var lista = todasRows
                .Select(r => new {
                    Id = Convert.ToInt32(r["id"]),
                    Nome = r["nome"].ToString(),
                    Vinculado = vinculados.Contains(Convert.ToInt32(r["id"]))
                })
                .ToList();

            rptCargos.DataSource = lista;
            rptCargos.DataBind();
        }

        protected void btnSalvarVinc_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(ddlVencimentos.SelectedValue, out var vid))
                return;

            // lê quais checkboxes foram marcados (name="chkCargo" no repeater)
            var valores = Request.Form.GetValues("chkCargo");
            var selecionados = new HashSet<int>();
            if (valores != null)
            {
                foreach (var s in valores)
                    if (int.TryParse(s, out var i))
                        selecionados.Add(i);
            }

            // pega todos os cargos de novo
            var todosDt = DatabaseHelper.ObterTodosCargos();
            var todosIds = todosDt.Rows
                .Cast<DataRow>()
                .Select(r => Convert.ToInt32(r["id"]));

            // para cada cargo, vincula ou desvincula
            foreach (var cid in todosIds)
            {
                if (selecionados.Contains(cid))
                    DatabaseHelper.VincularCargo(vid, cid);
                else
                    DatabaseHelper.DesvincularCargo(vid, cid);
            }

            // fecha modal
            ScriptManager.RegisterStartupScript(
                this, GetType(), "closeVinc",
                "new bootstrap.Modal(document.getElementById('vincularVencModal')).hide();",
                true);
        }

        protected void btnExcluirVinc_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(ddlVencimentos.SelectedValue, out var vid))
                return;

            // desfaz todos os vínculos
            foreach (var cv in DatabaseHelper.ObterCargosVinculados(vid))
                DatabaseHelper.DesvincularCargo(vid, cv.CargoId);

            // fecha modal
            ScriptManager.RegisterStartupScript(
                this, GetType(), "closeVinc",
                "new bootstrap.Modal(document.getElementById('vincularVencModal')).hide();",
                true);
        }

        protected void btnNovoVencimento_Click(object sender, EventArgs e)
        {
            // Pega o ClientID da modal filha
            var child = NovoVencimentoModal1;
            var modalId = child.ModalClientID;

            var script = $@"
              var m = new bootstrap.Modal(document.getElementById('{modalId}'));
              m.show();
            ";
            ScriptManager.RegisterStartupScript(this, GetType(), "showNovoVenc", script, true);
        }
    }
}
