using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;


public partial class SiteMaster : System.Web.UI.MasterPage
{
   
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
        

            if (Session["usuario"] != null)
            {
               
                if (NavigationMenu.Items.Count == 0)
                {
                    Interfaz i = new Interfaz();
                 
                    NavigationMenu.Items.AddRange(i.Armar(Session["usuario"].ToString()));

                }
                ASPxMenu1.Items[0].Visible = true;
                ASPxMenu1.Items[1].Visible = true;
                DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer","SELECT * FROM USUARIOS with(nolock) WHERE idusuario="+Session["usuario"].ToString()+" order by nombre");
                lbl_footer.Text = dt.Rows[0]["Nombre"].ToString();
            }
            else
            {
                NavigationMenu.Items.Clear();
                ASPxMenu1.Items[0].Visible = false;
                ASPxMenu1.Items[1].Visible = false;
                lbl_footer.Text = "";
            }
        }
        
    }

    

    protected void  ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
    {
        if (Session["usuario"] != null)
        {
            if (e.Item.Name == "nkb_cerrar")
            {
                if (Session["Op_fac_pdf"] != null)
                {
                    Session.Clear();
                    Session.Abandon();
                }
                if (Session["Carta_de_porte_pdf"] != null)
                {
                    Session.Clear();
                    Session.Abandon();
                }
                Session.Clear();
                Session.Abandon();


                ASPxMenu1.Items[0].Visible = false;
                ASPxMenu1.Items[1].Visible = false;
                lbl_footer.Text = "";
                Response.Redirect("Login.aspx");
            }

            if (e.Item.Name == "nkb_notificaciones")
            {
                Response.Redirect("Notificaciones.aspx");
            }

        }
    }



    protected void NavigationMenu_MenuItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {

        MainContent.Visible = true;
        if (e.Item.Target.Contains("http:") || e.Item.Target.Contains("file:") || e.Item.Target.Contains("https:"))
        { Response.Write("<script>window.open('" + e.Item.Target + "','_blank');</script>"); }
        else
        { Response.Redirect(e.Item.Target); }
    }
      
    
   
}
