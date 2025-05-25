using ESIGWeb.Data;
using ESIGWeb.Models;
using System;
using System.Web.UI;

namespace ESIGWeb.Controls
{
    public partial class NovoVencimentoModal : UserControl
    {
        // Exponha o ClientID da DIV da modal
        public string ModalClientID => novoVencModal.ClientID;

        protected void btnSalvarNovo_Click(object sender, EventArgs e)
        {
            var v = new Vencimentos
            {
                Descricao = txtDescNovo.Text.Trim(),
                Valor = decimal.Parse(txtValorNovo.Text),
                FormaIncidencia = ddlFormaNovo.SelectedValue,
                Tipo = ddlTipoNovo.SelectedValue
            };
            var novoId = DatabaseHelper.InserirVencimento(v);

            // Opcional: notificar pai via evento ou recarregar combo
            // ...

            // Fecha esta modal e reabre a de vincular (via script)
            var script = $@"
              var m = bootstrap.Modal.getInstance(document.getElementById('{ModalClientID}'));
              if(m) m.hide();
              setTimeout(function(){{
                new bootstrap.Modal(document.getElementById('vincularVencModal')).show();
              }}, 200);
            ";
            ScriptManager.RegisterStartupScript(this, GetType(), "afterNovoVenc", script, true);
        }
    }
}
