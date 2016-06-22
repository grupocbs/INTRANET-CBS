using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Data;

public partial class Account_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null)
        {
            ((DevExpress.Web.ASPxMenu)Master.FindControl("NavigationMenu")).Items.Clear();
            ((DevExpress.Web.ASPxMenu)Master.FindControl("ASPxMenu1")).Items[0].Visible = false;
            ((DevExpress.Web.ASPxMenu)Master.FindControl("ASPxMenu1")).Items[1].Visible = false;
            ((Label)Master.FindControl("lbl_footer")).Text = "";
           
          

        }
        else
        {
            Response.Redirect("Notificaciones.aspx");
        }
    }
    protected void LoginButton_Click(object sender, EventArgs e)
    {
        try
        {
            FailureText.Text = "";

            string sql = " SELECT * FROM usuarios WHERE usuario='" + UserName.Text + "' and contraseña='" + Password.Text + "'";





            DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql);
          
            if (dt.Rows.Count>0)
            {

                Session.Add("usuario", dt.Rows[0]["IDUSUARIO"].ToString());

                Response.Redirect("Notificaciones.aspx");

            }
            else
            {
                FailureText.Text = "Error de Usuario y Contraseña";
            }

            
            
        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }

    }
    
   
}
