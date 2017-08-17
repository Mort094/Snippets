using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CRUD : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("SELECT * FROM genders", conn);

            conn.Open();

            dd_gender.DataSource = cmd.ExecuteReader();
            dd_gender.DataBind();
            dd_gender.Items.Insert(0, new ListItem("Select", ""));
            conn.Close();
        }
        if (!IsPostBack)
        {


            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

            SqlCommand cmd = new SqlCommand(@"SELECT * FROM users
                                                INNER JOIN genders
                                                ON fk_gender_id = gender_id", conn);


            conn.Open();
            repeater_data.DataSource = cmd.ExecuteReader();
            repeater_data.DataBind();
            conn.Close();
        }

    }

    protected void btn_create_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand();

        cmd.Connection = conn;

        cmd.CommandText = @"INSERT INTO users (user_name, user_email, user_password, fk_gender_id) 
                                        VALUES (@user_name, @user_email, @user_password, @fk_gender_id)";

        cmd.Parameters.AddWithValue("@user_name", tb_name.Text);
        cmd.Parameters.AddWithValue("@user_email", tb_email.Text);
        cmd.Parameters.AddWithValue("@user_password", tb_password.Text);
        cmd.Parameters.AddWithValue("@fk_gender_id", dd_gender.SelectedValue);

        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
        Response.Redirect("CRUD.aspx");

    }


    protected void repeater_data_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand();

        cmd.Connection = conn;

        if (e.CommandName == "edit")
        {

            cmd.CommandText = @"SELECT * FROM users WHERE user_id = @user_id";

            cmd.Parameters.AddWithValue("@user_id", e.CommandArgument);

            ViewState["user_id"] = e.CommandArgument;

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                tb_name.Text = reader["user_name"].ToString();
                tb_email.Text = reader["user_email"].ToString();
                tb_password.Text = reader["user_password"].ToString();
                dd_gender.Text = reader["fk_gender_id"].ToString();


            }
            conn.Close();
            btn_create.Visible = false;
            btn_save.Visible = true;

        }
        if (e.CommandName == "delete")
        {

            cmd.Connection = conn;
            cmd.CommandText = @"DELETE FROM users WHERE user_id = @user_id";
            cmd.Parameters.AddWithValue("@user_id", e.CommandArgument);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            Response.Redirect("CRUD.aspx");
        }
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand(@"UPDATE users SET user_name = @user_name, user_email = @user_email, fk_gender_id = @fk_gender_id WHERE user_id = @user_id", conn);


        cmd.Parameters.AddWithValue("@user_id", ViewState["user_id"]);
        cmd.Parameters.AddWithValue("@user_name", tb_name.Text);
        cmd.Parameters.AddWithValue("@user_email", tb_email.Text);
        cmd.Parameters.AddWithValue("@fk_gender_id", dd_gender.SelectedValue);


        if (tb_password.Text != "")
        {
            cmd.CommandText = @"UPDATE users SET user_name = @user_name, user_email = @user_email, fk_gender_id = @fk_gender_id WHERE user_id = @user_id";
            cmd.Parameters.AddWithValue("@user_password", tb_password.Text);
        }

        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();

        Response.Redirect("CRUD.aspx");
    }
}