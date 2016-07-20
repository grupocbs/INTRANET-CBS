using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
using System.Data.SqlClient;
using System.Configuration;
using Subgurim.Controles;


public partial class MapaObservaciones: System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["usuario"] == null)
            {

               Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {

                ASPxCalendarDesde.Text = DateTime.Now.AddMonths(-1).ToShortDateString();
                ASPxCalendarHasta.Text =  DateTime.Now.ToShortDateString();


                DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", "SELECT id=mail +',' +contraseña,usuario FROM Usuarios with(nolock) WHERE supervisor <> ' '  GROUP BY mail +',' +contraseña ,usuario ORDER BY usuario");

                foreach (DataRow dr in dt.Rows)
                {
                    cmb_usuarios.Items.Add(new ListEditItem(dr["usuario"].ToString(), dr["id"].ToString()));
                     

                }

                 

                GMap1.Language = "es";
                GMap1.Add(GMapType.GTypes.Physical);
                GMap1.Add(GMapType.GTypes.Hybrid);
                GMap1.Add(GMapType.GTypes.Satellite);
                GMap1.Add(new GControl(GControl.preBuilt.MapTypeControl));
                GMap1.enableHookMouseWheelToZoom = true;
                GLatLng latlong = new GLatLng(-38.9517151, -68.0525031);
                GMapType.GTypes maptype = GMapType.GTypes.Normal;
                GMap1.setCenter(latlong,9, maptype);

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    protected void cmb_usuarios_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
               GMap1.resetInfoWindows();

            if (cmb_usuarios.SelectedIndex > -1 && Convert.ToDateTime(ASPxCalendarDesde.Text) <= Convert.ToDateTime(ASPxCalendarHasta.Text))
            {
                DataTable dt = Interfaz.EjecutarConsultaBD("CBS", "SELECT USR_CLIOBJ_OBJDSC as OBJETIVO, USR_OPOBOJ_CODOBS as CODIGO,USR_OPOBOJ_FCHOBS as FECHA,USR_OPOBOJ_COORDN as COORDENADAS,USR_OPOBOJ_OBSERV as OBSERVACION,USR_OPOBTP_DESCRP AS TIPO FROM USR_OPOBOJ o left join USR_OPOBTP t with(nolock) on o.USR_OPOBOJ_TIPOOB=t.USR_OPOBTP_CODIGO LEFT JOIN USR_CLIOBJ c with(nolock) on c.USR_CLIOBJ_CODOBJ=o.USR_OPOBOJ_CODCLI WHERE USR_OPOBOJ_USERNM='" + cmb_usuarios.SelectedItem.Text + "' and USR_OPOBOJ_FCHOBS between '" + Convert.ToDateTime(ASPxCalendarDesde.Text).ToString("yyyyMMdd 00:00:00") + "' and '" + Convert.ToDateTime(ASPxCalendarHasta.Text).ToString("yyyyMMdd 23:59:59") + "' ORDER BY USR_OPOBOJ_CODOBS DESC");


                lbl_mail.Text = cmb_usuarios.SelectedItem.Value.ToString().Substring(0,cmb_usuarios.SelectedItem.Value.ToString().IndexOf(","));
                lbl_contra.Text = cmb_usuarios.SelectedItem.Value.ToString().Remove(0, cmb_usuarios.SelectedItem.Value.ToString().IndexOf(",")+1);
                    
              

                
                ASPxGridView1.DataSource = dt;
                ASPxGridView1.DataBind();



            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    protected void fotosGrid_DataSelect(object sender, EventArgs e)
    {
        try
        {



            (sender as ASPxGridView).DataSource = Interfaz.EjecutarConsultaBD("CBS", "SELECT USR_OPOBCA_CPTITL AS TITULO, USR_OPOBCA_CODOBS AS CODIGO,USR_OPOBCA_USERNM AS SUP  FROM [USR_OPOBCA] WHERE USR_OPOBCA_USERNM='" + cmb_usuarios.SelectedItem.Text + "' AND USR_OPOBCA_CODOBS='" + (sender as ASPxGridView).GetMasterRowKeyValue() + "'");

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    protected void fotosGrid_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        if (e.ButtonID == "button")
        {
            //ASPxGridView1.GetRowValues(e.VisibleIndex, "CODIGO").ToString();
            DataTable dt = Interfaz.EjecutarConsultaBD("CBS", "SELECT USR_CLIOBJ_OBJDSC as OBJETIVO, USR_OPOBOJ_CODOBS as CODIGO,USR_OPOBOJ_FCHOBS as FECHA,USR_OPOBOJ_COORDN as COORDENADAS,USR_OPOBOJ_OBSERV as OBSERVACION,USR_OPOBTP_DESCRP AS TIPO FROM USR_OPOBOJ o left join USR_OPOBTP t with(nolock) on o.USR_OPOBOJ_TIPOOB=t.USR_OPOBTP_CODIGO LEFT JOIN USR_CLIOBJ c with(nolock) on c.USR_CLIOBJ_CODOBJ=o.USR_OPOBOJ_CODCLI WHERE USR_OPOBOJ_USERNM='" + cmb_usuarios.SelectedItem.Text + "' and USR_OPOBOJ_CODOBS='" + ASPxGridView1.GetRowValues(e.VisibleIndex, "CODIGO").ToString() + "'");
            GMap1.resetInfoWindows();
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["COORDENADAS"] != null && dr["COORDENADAS"].ToString() != "0,0")
                {
                    double lat = Convert.ToDouble(dr["COORDENADAS"].ToString().Substring(0, dr["COORDENADAS"].ToString().IndexOf(",")).Replace(".", ","));
                    double lng = Convert.ToDouble(dr["COORDENADAS"].ToString().Remove(0, dr["COORDENADAS"].ToString().IndexOf(",") + 1).Replace(".", ","));

                    GLatLng latlong = new GLatLng(lat, lng);
                    GMarker marker = new GMarker(latlong);
                    GInfoWindowOptions windowOptions = new GInfoWindowOptions();

                    string tabla = "<table width='50px'>";
                    tabla += "<tr><td><b>" + dr["CODIGO"].ToString() + "</td></tr>";
                    tabla += "</table>";

                    GInfoWindow commonInfoWindow = new GInfoWindow(marker, tabla, true, GListener.Event.mouseover);

                    GMap1.Add(commonInfoWindow);
                    





                }
           
            }
        }// ASPxGridView grid = sender as ASPxGridView;
        //grid.FilterExpression = "[Title] like '%Sales%'";
    }


    
}
   

