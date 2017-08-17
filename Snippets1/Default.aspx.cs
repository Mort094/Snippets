using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

            SqlCommand cmd = new SqlCommand(@"SELECT * FROM tests", conn);


            conn.Open();
            repeater_test.DataSource = cmd.ExecuteReader();
            repeater_test.DataBind();
            conn.Close();
        }

    }
   // cmd.Parameters.AddWithValue("@s_id", Request.QueryString["stilartId"]);

    protected void repeater_test_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        if (repeater_test.Items.Count < 1)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                Label lbl_fail = (Label)e.Item.FindControl("lbl_fail");
                lbl_fail.Visible = true;
            }
        }

    }
}