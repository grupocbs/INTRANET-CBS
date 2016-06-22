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


                DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", "SELECT idusuario as id,usuario FROM Usuarios with(nolock) WHERE supervisor <> ' '  GROUP BY idusuario ,usuario");

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
                GMap1.setCenter(latlong,10, maptype);

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
                DataTable dt = Interfaz.EjecutarConsultaBD("CBS", "SELECT VTMCLH_NOMBRE, USR_OPOBOJ_CODOBS,USR_OPOBOJ_FCHOBS as FECHA,USR_OPOBOJ_COORDN,USR_OPOBOJ_OBSERV,USR_OPOBTP_DESCRP FROM USR_OPOBOJ o left join USR_OPOBTP t with(nolock) on o.USR_OPOBOJ_TIPOOB=t.USR_OPOBTP_CODIGO LEFT JOIN VTMCLH c with(nolock) on c.VTMCLH_NROCTA=o.USR_OPOBOJ_CODCLI WHERE USR_OPOBOJ_USERNM='" + cmb_usuarios.SelectedItem.Text + "' and USR_OPOBOJ_FCHOBS between '" + Convert.ToDateTime(ASPxCalendarDesde.Text).ToString("yyyyMMdd 00:00:00") + "' and '" + Convert.ToDateTime(ASPxCalendarHasta.Text).ToString("yyyyMMdd 23:59:59") + "'");

                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["USR_OPOBOJ_COORDN"] != null && dr["USR_OPOBOJ_COORDN"].ToString() != "0,0")
                    {
                        double lat = Convert.ToDouble(dr["USR_OPOBOJ_COORDN"].ToString().Substring(0, dr["USR_OPOBOJ_COORDN"].ToString().IndexOf(",")).Replace(".", ","));
                        double lng = Convert.ToDouble(dr["USR_OPOBOJ_COORDN"].ToString().Remove(0, dr["USR_OPOBOJ_COORDN"].ToString().IndexOf(",") + 1).Replace(".", ","));

                        GLatLng latlong = new GLatLng(lat, lng);
                        GMarker marker = new GMarker(latlong);
                        GInfoWindowOptions windowOptions = new GInfoWindowOptions();

                        string tabla = "<table width='250px'>";
                        tabla += "<tr><td><b>" + dr["USR_OPOBOJ_CODOBS"].ToString() + "   " + "</b>"+ dr["FECHA"].ToString()+"</td></tr>";
                        tabla += "<tr><td><b>" + dr["VTMCLH_NOMBRE"].ToString() + "</b></td></tr>"; 
                        tabla +="<tr><td><b>" + dr["USR_OPOBTP_DESCRP"].ToString() + "</b></td></tr>"; 
                        tabla +="<tr><td><p>" + dr["USR_OPOBOJ_OBSERV"].ToString() + "</p></td></tr>";

                        dt = Interfaz.EjecutarConsultaBD("CBS", "SELECT USR_OPOBCA_CPTITL FROM [USR_OPOBCA] WHERE USR_OPOBCA_USERNM='" + cmb_usuarios.SelectedItem.Text + "' AND USR_OPOBCA_CODOBS='" + dr["USR_OPOBOJ_CODOBS"].ToString() + "'");

                        string archivo = "";
                        tabla += "<tr><td><table border='1px' align='center'><tr>";

                        foreach (DataRow dr1 in dt.Rows)
                        {

                            archivo = "<a target='_blank' href='http://192.168.1.141:3333/archivos/capturas/" + cmb_usuarios.SelectedItem.Text + "/" + dr["USR_OPOBOJ_CODOBS"].ToString() + "/" + dr1["USR_OPOBCA_CPTITL"].ToString() + "'><img src='http://192.168.1.141:3333/archivos/capturas/" + cmb_usuarios.SelectedItem.Text + "/" + dr["USR_OPOBOJ_CODOBS"].ToString() + "/" + dr1["USR_OPOBCA_CPTITL"].ToString() + "' width='50px' /></a>";
                             tabla += "<td>" + archivo + "</td>";
                        }

                        tabla += "</tr></table></td></tr></table>";

                        GInfoWindow commonInfoWindow = new GInfoWindow(marker, tabla, false, GListener.Event.click);

                        GMap1.Add(commonInfoWindow);




                    }


                }



            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }


    
}
   

