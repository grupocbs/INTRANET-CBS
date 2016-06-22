using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Subgurim.Controles;
using System.Data;

public partial class ControlRecorridas: System.Web.UI.Page
{
    

    protected void Page_Load(object sender, EventArgs e)
    {
       
       /* if (Session["usuario"] == null)
        {

            Response.Redirect("login.aspx");
        }*/
        if (!IsPostBack)
        {

            GMap1.Language = "es";
            GMap1.Add(GMapType.GTypes.Physical);
            GMap1.Add(GMapType.GTypes.Hybrid);
            GMap1.Add(GMapType.GTypes.Satellite);
            GMap1.Add(new GControl(GControl.preBuilt.MapTypeControl));
            GMap1.enableHookMouseWheelToZoom = true;
            GLatLng latlong = new GLatLng(-38.9517151, -68.0525031);
            GMapType.GTypes maptype = GMapType.GTypes.Normal;
            GMap1.setCenter(latlong, 10, maptype);



            txt_intervalo.Text = "01";
        }
    }
      

        
    


    private void cargar(string path)
    {
        try
        {
            GPXLoader L = new GPXLoader();
            List<Punto> s = L.LoadGPXTracks(path);
            GMap1.resetInfoWindows();
            ASPxGridView1.DataSource = null;
            ASPxGridView1.DataBind();

            GLatLng latlong_inicio = new GLatLng();

            string iconocolor = "imagenes/mm_20_red.png";

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("FECHA_HORA");
            dt.Columns.Add("LATITUD");
            dt.Columns.Add("LONGITUD");
            dt.Columns.Add("VELOCIDAD");

            bool insert = false;

            for (int i = 0; i < s.Count; i++)
            {
                Punto dr = s[i];
                if (i == 0)
                {
                    latlong_inicio.lat = Convert.ToDouble(dr.Latitude.Replace(".", ","));
                    latlong_inicio.lng = Convert.ToDouble(dr.Longitude.Replace(".", ","));
                }

                if (i + 1 < s.Count)
                {
                    

                    //Grilla
                    switch (cmb_unidades.SelectedItem.Value)
                    {
                        case "Segundos":
                            {
                                if (Convert.ToDateTime(dr.Time).AddSeconds(Convert.ToDouble(txt_intervalo.Text)) < Convert.ToDateTime(s[i + 1].Time))
                                {
                                    dt.Rows.Add(i.ToString(), Convert.ToDateTime(dr.Time).ToShortDateString() + " " + Convert.ToDateTime(dr.Time).ToLongTimeString(), dr.Latitude, dr.Longitude, dr.Speed);
                                    insert = true;
                                }
                                else
                                {
                                    insert = false;
                                }
                                break;
                            }
                        case "Minutos":
                            {
                                if (Convert.ToDateTime(dr.Time).AddMinutes(Convert.ToDouble(txt_intervalo.Text)) < Convert.ToDateTime(s[i + 1].Time))
                                {
                                    dt.Rows.Add(i.ToString(), Convert.ToDateTime(dr.Time).ToShortDateString() + " " + Convert.ToDateTime(dr.Time).ToLongTimeString(), dr.Latitude, dr.Longitude, dr.Speed);
                                    insert = true;
                                }
                                else
                                {
                                    insert = false;
                                }
                                break;
                            }
                        case "Horas":
                            {
                                if (Convert.ToDateTime(dr.Time).AddHours(Convert.ToDouble(txt_intervalo.Text)) < Convert.ToDateTime(s[i + 1].Time))
                                {
                                    dt.Rows.Add(i.ToString(), Convert.ToDateTime(dr.Time).ToShortDateString() + " " + Convert.ToDateTime(dr.Time).ToLongTimeString(), dr.Latitude, dr.Longitude, dr.Speed);
                                    insert = true;
                                }
                                else
                                {
                                    insert = false;
                                }

                                break;
                            }
                    }

                    //Mapa
                    if (insert)
                    {
                        GLatLng latlong = new GLatLng(Convert.ToDouble(dr.Latitude.Replace(".", ",")), Convert.ToDouble(dr.Longitude.Replace(".", ",")));

                        GMarker marker = new GMarker(latlong, new GIcon(iconocolor));
                        GInfoWindowOptions windowOptions = new GInfoWindowOptions();

                        string tabla = "<table>";
                        tabla += "<tr><td><b>" + dr.Track + "   " + "</b></td></tr>";
                        tabla += "<tr><td><b>Latitud:</b>" + dr.Latitude + "</td></tr>";
                        tabla += "<tr><td><b>Longitud:</b>" + dr.Longitude + "</td></tr>";
                        tabla += "<tr><td><b>Velocidad:</b>" + dr.Speed + "</td></tr>";
                        tabla += "<tr><td><b>Fecha y hora:</b>" + Convert.ToDateTime(dr.Time).ToShortDateString() + " " + Convert.ToDateTime(dr.Time).ToLongTimeString() + "</td></tr>";
                        tabla += "</table>";

                        GInfoWindow commonInfoWindow = new GInfoWindow(marker, tabla, false, GListener.Event.click);
                        GMap1.Add(commonInfoWindow);
                      
                    }

                }
            }

            if (s.Count > 0)
            {
                TimeSpan ts = Convert.ToDateTime(s[s.Count - 1].Time) - Convert.ToDateTime(s[0].Time);
                txt_totalrecorrido.Text = "Realizado en " + ts.Minutes.ToString() + " minutos.";
                txt_totalrecorrido.Text += "</br>Recorrido comienza el dia " + Convert.ToDateTime(s[0].Time).ToShortDateString() + " a las " + Convert.ToDateTime(s[0].Time).ToShortTimeString() + " y termina " + Convert.ToDateTime(s[s.Count - 1].Time).ToShortTimeString();
                
            }


            GMap1.setCenter(latlong_inicio, 14, GMapType.GTypes.Normal);


            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    protected void UploadButton_Click(object sender, EventArgs e)
    {
        if (txt_archivo.Text.Length == 0)
        {
            if (btn_abrir.HasFile)
            {
                try
                {
                    btn_abrir.SaveAs(Server.MapPath("Archivos/") + btn_abrir.FileName);
                    txt_archivo.Text = btn_abrir.FileName;
                    cargar(Server.MapPath("Archivos/") + txt_archivo.Text);
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
            else
            {
                Response.Write("Seleccione Archivo a Importar");
            }
        }
        else
        {
            cargar(Server.MapPath("Archivos/") + txt_archivo.Text);
        }
    }

    
}

