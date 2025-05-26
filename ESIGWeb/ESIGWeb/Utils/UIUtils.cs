using System.Web.UI.WebControls;

namespace ESIGWeb.Utils
{
    public static class UIUtils
    {
        public static void BindDropDownList(DropDownList ddl, object dataSource, string valueField, string textField)
        {
            ddl.DataSource = dataSource;
            ddl.DataValueField = valueField;
            ddl.DataTextField = textField;
            ddl.DataBind();
        }
    }
}
