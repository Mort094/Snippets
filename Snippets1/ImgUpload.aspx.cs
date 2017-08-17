using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class ImgUpload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

            SqlCommand cmd = new SqlCommand(@"SELECT * FROM images", conn);


            conn.Open();
            repeater_data.DataSource = cmd.ExecuteReader();
            repeater_data.DataBind();
            conn.Close();
        }
    }

    protected void btn_upload_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand();

        cmd.Connection = conn;

        cmd.CommandText = @"INSERT INTO images (image_file) 
                                        VALUES (@image_file)";
        string img_path = "intetbillede.jpg";

        //Hvis der er en fil i FilUploaden

        if (fu_img.HasFile)
        {
            //NewGuid danner uniq navn for billeder
            img_path = Guid.NewGuid() + Path.GetExtension(fu_img.FileName);
            // Opret



            String UploadeMappe = Server.MapPath("~/Images/");
            
            //string Kategori = dd_type.SelectedItem.Text;
            String Lille = "Small";
            //String Stor = "Large";
            String Filnavn = DateTime.Now.ToFileTime() + fu_img.FileName;
            img_path = Filnavn;

            //Gem det orginale Billede
            fu_img.SaveAs(UploadeMappe + Filnavn);

            // Definer hvordan
            ImageResizer.ResizeSettings BilledeSkalering = new ImageResizer.ResizeSettings();
            BilledeSkalering.Width = 130;


            //Udfør skalleringen
            ImageResizer.ImageBuilder.Current.Build(UploadeMappe + Filnavn, UploadeMappe + "/" + Lille + "/" + Filnavn, BilledeSkalering);

            //Lav nogle nye skalerings instillinger
            BilledeSkalering = new ImageResizer.ResizeSettings();
            BilledeSkalering.Width = 65;


            //Udfør selve skaleringen
            //ImageResizer.ImageBuilder.Current.Build(UploadeMappe + Filnavn, CroppedMappe + Filnavn, BilledeSkalering);

        }
        // Tildel parameter-værdierne, fra input felterne.
        cmd.Parameters.AddWithValue("@image_file", img_path);



        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();

        Response.Redirect("ImgUpload.aspx");

    }

    protected void repeater_data_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand();

        cmd.Connection = conn;

        if (e.CommandName == "edit")
        {

            cmd.CommandText = @"SELECT * FROM images
                                WHERE image_id = @image_id";

            cmd.Parameters.AddWithValue("@image_id", e.CommandArgument);

            ViewState["image_id"] = e.CommandArgument;

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                hf_oldImg.Value = reader["image_file"].ToString();


            }
            conn.Close();
            btn_upload.Visible = false;
            btn_save.Visible = true;
        }
        if (e.CommandName == "delete")
        {


            cmd.Connection = conn;
            cmd.CommandText = @"SELECT * FROM images
                                WHERE image_id = @image_id";
            cmd.Parameters.AddWithValue("@image_id", e.CommandArgument);
            conn.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                //Skal med for at sletter billeder/filer i mapper når du sletter tingen på siden. Den ved allerede hvilken id det, så den skal også vide hvilken ting den skal slettet i mappen. Det finder du på sammen måde som når du SELECTER det til at kunne rette


                if (File.Exists(Server.MapPath("~/Images/") + reader["image_file"].ToString()))
                {
                    File.Delete(Server.MapPath("~/Images/") + reader["image_file"].ToString());
                }
                //if (File.Exists(Server.MapPath("~/Images/") + reader["image_file"].ToString()))
                //{
                //    File.Delete(Server.MapPath("~/Images/") + reader["image_file"].ToString());
                //}

            }
            cmd.CommandText = @"DELETE FROM images WHERE image_id = @image_id";

            conn.Close();

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            Response.Redirect("ImgUpload.aspx");
        }
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {

    }
}