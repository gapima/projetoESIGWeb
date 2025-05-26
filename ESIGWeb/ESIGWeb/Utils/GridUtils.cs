using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

namespace ESIGWeb.Utils
{
    public static class GridUtils
    {
        public static void CarregarGridComFiltro(
            GridView grid, DataTable dt,
            string filtroNome = "", string filtroCargo = "")
        {
            if (dt == null) return;

            DataView dv = dt.DefaultView;

            List<string> filtros = new List<string>();
            if (!string.IsNullOrWhiteSpace(filtroNome))
                filtros.Add($"nome LIKE '%{filtroNome.Replace("'", "''")}%'");
            if (!string.IsNullOrWhiteSpace(filtroCargo))
                filtros.Add($"nome_cargo LIKE '%{filtroCargo.Replace("'", "''")}%'");

            if (filtros.Count > 0)
                dv.RowFilter = string.Join(" AND ", filtros);

            grid.DataSource = dv;
            grid.DataBind();
        }
    }
}
