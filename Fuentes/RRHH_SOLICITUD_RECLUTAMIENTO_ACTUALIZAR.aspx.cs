using System;
using System.Drawing;
using System.IO;
using DevExpress.Web;
using DevExpress.Web.Internal;
using System.Data;
using System.Collections.Generic;
using System.Net.Mail;
using System.Configuration;

public partial class RRHH_SOLICITUD_RECLUTAMIENTO_ACTUALIZAR : System.Web.UI.Page
{
  
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Add("usuario", "3085");
        if (Session["usuario"] == null)
        {
            Response.Redirect("INTRANET_LOGIN.aspx");
        }
        if (!IsPostBack)
        {

            cmb_razon_social.Items.Add(new ListEditItem("SELECCIONE", "SELECCIONE"));
            cmb_razon_social.Items.Add(new ListEditItem("BARCELO", "BARCELO"));
            cmb_razon_social.Items.Add(new ListEditItem("CASA DE LA COSTA", "CASA DE LA COSTA"));
            cmb_razon_social.Items.Add(new ListEditItem("CBS SRL", "CBS SRL"));
            cmb_razon_social.SelectedIndex = 0;


            RecargarSolicitudes();
            
        }

         
       
    }

    private void RecargarSolicitudes()
    {
        cmb_solicitudes.Items.Clear();
        string sql = "select ID,N=CAST(ID AS VARCHAR) + ' - ' + NOMBRE_PUESTO + ' '+AREA+ ' - ' + CONVERT(VARCHAR(10),FECHA_SOLICITUD,103) + ' - ' + cast(u.USUARIO as varchar) COLLATE Modern_Spanish_CI_AS + ' - ' + CASE WHEN PENDIENTE=1 THEN 'PENDIENTE' ELSE 'FINALIZADA' END from RRHH_SOLICITUD_RECLUTAMIENTO r with (nolock)  left join USUARIOS u with(nolock) on r.usuario=u.IDUSUARIO WHERE pendiente=1 order by id desc";
        DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql);
        if (dt.Rows.Count > 0)
        {
            cmb_solicitudes.Items.Add(new ListEditItem("SELECCIONE", "SELECCIONE"));
            foreach (DataRow dr in dt.Rows)
            {
                cmb_solicitudes.Items.Add(new ListEditItem(dr["N"].ToString(), dr["ID"].ToString()));
            }
            cmb_solicitudes.SelectedIndex = 0;
        }
    }

    protected void cmb_solicitudes_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmb_solicitudes.SelectedItem.Value.ToString() != "SELECCIONE")
        {
            string sql = "select FECHA_VAC=CONVERT(VARCHAR(10),FECHA_VACANTE,103),FECHA_SOL=CONVERT(VARCHAR(10),FECHA_SOLICITUD,103),* from RRHH_SOLICITUD_RECLUTAMIENTO with (nolock) where ID='" + cmb_solicitudes.SelectedItem.Value.ToString() + "' order by ID DESC";
            DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql);
            if (dt.Rows.Count > 0)
            {
           
                cmb_razon_social.SelectedIndex = cmb_razon_social.Items.IndexOf(cmb_razon_social.Items.FindByValue(dt.Rows[0]["RAZON_SOCIAL"].ToString()));
               
            }
        }
        else
        {
            Response.Redirect("RRHH_SOLICITUD_RECLUTAMIENTO_ACTUALIZAR.aspx");
        }
    }
  

    protected void txt_cuil_TextChanged(object sender, EventArgs e)
    {

        try
        {
            
            if (txt_cuil.Text.Length==11)
            {
                string sql = "select * from RRHH_SOLICITUD_INGRESO with(nolock) where cuil='" + txt_cuil.Text + "'";
                DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql);

                if (dt.Rows.Count > 0)
                {
                    FailureText.Text = "EL CUIL INGRESADO YA ESTA CARGADO EN LA BASE DE DATOS.";
                    txt_cuil.Focus();
                }
                else
                {
                    FailureText.Text = "";
                    txt_apellido.Focus();
                }




            }

        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }
    }


   

    protected void btn_enviar_Click(object sender, EventArgs e)
    {
        try
        {
            FailureText.Text = "";
            if (cmb_solicitudes.Items.Count > 0 && cmb_solicitudes.SelectedItem.Text != "SELECCIONE")
            {
                //email de
                 string sql = "select mail, usuario, nombre from USUARIOS with (nolock) where idusuario=" + Session["usuario"].ToString();
                    DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql);
                //email para
                    sql = "select mail,COD_CATEGORIA, COD_CONV, COD_CLIENTE, COD_OBJETIVO,INTERNO from RRHH_SOLICITUD_RECLUTAMIENTO r with(nolock) inner join usuarios u with(nolock) on r.USUARIO=u.IDUSUARIO where r.id=" + cmb_solicitudes.SelectedItem.Value.ToString();
                    DataTable dt2 = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql);


                    if (dt.Rows.Count > 0 && dt2.Rows.Count > 0)
                    {
                        Interfaz.Alta_SOLICITUD_INGRESO_RECLUTAMIENTO(txt_cuil.Text, txt_apellido.Text, txt_nombres.Text, txt_domicilio_real.Text, cmb_solicitudes.SelectedItem.Value.ToString(), cmb_razon_social.SelectedItem.Value.ToString(), txt_observaciones.Text, dt2.Rows[0]["COD_CATEGORIA"].ToString(), dt2.Rows[0]["COD_CONV"].ToString(), dt2.Rows[0]["COD_CLIENTE"].ToString(), dt2.Rows[0]["COD_OBJETIVO"].ToString(), Convert.ToBoolean(dt2.Rows[0]["INTERNO"]));
                        Interfaz.Editar_Estado_SOLICITUD_RECLUTAMIENTO(cmb_solicitudes.SelectedItem.Value.ToString(), false);


                        MailMessage mail = new MailMessage();
                        string EMAIL_DE = dt.Rows[0]["mail"].ToString();
                        mail.From = new MailAddress(EMAIL_DE);
                        mail.To.Add(dt2.Rows[0]["mail"].ToString());
                        
                          

                        mail.Subject = "Finalizacion de Proceso de Reclutamiento";
                        mail.IsBodyHtml = true;
                        mail.Body = "Estimada/o:<BR>Se informa que el proceso de Reclutamiento Nro " + cmb_solicitudes.SelectedItem.Value.ToString() + "  finalizo<BR>";
                        if(txt_observaciones.Text.Length>0)
                        {
                            mail.Body += "<BR>Observaciones:" + txt_observaciones.Text;
                        }
                        mail.Body += "<BR>Puede cargar El formulario de Solicitud de Ingreso, seleccionando el registro segun el Nro de Reclutamiento. Para ello Ingrese a Intranet Grupo CBS, Menu RRHH->Ingresos->Formulario de Solicitud: <a href='http://192.168.1.141/RRHH_SOLICITUD_ALTA.aspx' target='_blank'>Formulario de Solicitud</a><BR>";


                        string EMAIL_SMTP = System.Configuration.ConfigurationSettings.AppSettings.Get("EMAIL_SMTP");
                        SmtpClient smtp = new SmtpClient(EMAIL_SMTP, 587);
                        smtp.Timeout = 10000;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new System.Net.NetworkCredential(System.Configuration.ConfigurationSettings.AppSettings.Get("USUARIO_SMTP"), System.Configuration.ConfigurationSettings.AppSettings.Get("PASS_SMTP"));
                        smtp.Send(mail);

                       
                        Response.Redirect("RRHH_SOLICITUD_RECLUTAMIENTO_ACTUALIZAR.aspx");


                        RecargarSolicitudes();
                        LimpiarPantalla();
                    }
                    else
                    {
                        FailureText.Text = "No se puede guardar porque el usuario no tiene cuenta de mail registrada.";
                    }

            }
            else
            {


            }
        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;

        }
    }

    private void LimpiarPantalla()
    {
        try
        {
           
            txt_cuil.Text = "";
            txt_apellido.Text = "";
            txt_domicilio_real.Text = "";
            txt_nombres.Text = "";
            cmb_razon_social.SelectedIndex = 0;
            txt_observaciones.Text = "";
            FailureText.Text = "";

        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
           
        }
    }
}