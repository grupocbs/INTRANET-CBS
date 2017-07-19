using System;
using System.Drawing;
using System.IO;
using DevExpress.Web;
using DevExpress.Web.Internal;
using System.Data;
using System.Collections.Generic;
using System.Net.Mail;
using System.Configuration;

public partial class RRHH_SOLICITUD_RECLUTAMIENTO_REPORTE_TODOS : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
       // Session.Add("usuario", "1");
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
            string sql = "select ID,N=CAST(ID AS VARCHAR) + ' - ' + NOMBRE_PUESTO + ' '+AREA+ ' - ' + CONVERT(VARCHAR(10),FECHA_SOLICITUD,103) from RRHH_SOLICITUD_RECLUTAMIENTO with (nolock) order by ID DESC";
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



            dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", "SELECT ID, DESCRIPCION FROM RRHH_REC_SECTORES with(nolock) order by DESCRIPCION");
           
            if (dt.Rows.Count > 0)
            {
                cmb_area.Items.Add(new ListEditItem("SELECCIONE", "SELECCIONE"));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmb_area.Items.Add(new ListEditItem(dt.Rows[i]["DESCRIPCION"].ToString(), dt.Rows[i]["ID"].ToString()));

                }
                cmb_area.SelectedIndex = 0;
            }

            sql = "select VTMCLH_NROCTA,VTMCLH_NOMBRE from VTMCLH with (nolock)  order by VTMCLH_NOMBRE";
            dt = Interfaz.EjecutarConsultaBD("CBS", sql);
          
            cmb_cliente.Items.Add(new ListEditItem("SELECCIONE", "SELECCIONE"));
            foreach (DataRow dr in dt.Rows)
            {
                cmb_cliente.Items.Add(new ListEditItem(dr["VTMCLH_NOMBRE"].ToString(), dr["VTMCLH_NROCTA"].ToString()));
            }
            cmb_cliente.SelectedIndex = 0;

            sql = "select USR_SJTCON_CODIGO,USR_SJTCON_DESCRP  from USR_SJTCON with (nolock)   order by USR_SJTCON_DESCRP";
            dt = Interfaz.EjecutarConsultaBD("CBS", sql);
   
            cmb_convenio.Items.Add(new ListEditItem("SELECCIONE", "SELECCIONE"));
            foreach (DataRow dr in dt.Rows)
            {
                cmb_convenio.Items.Add(new ListEditItem(dr["USR_SJTCON_DESCRP"].ToString(), dr["USR_SJTCON_CODIGO"].ToString()));

            }
            cmb_convenio.SelectedIndex = 0;
        }
    }

    protected void cmb_solicitudes_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (cmb_solicitudes.SelectedItem.Value.ToString() != "SELECCIONE")
            {
                string sql = "select FECHA_VAC=CONVERT(VARCHAR(10),FECHA_VACANTE,103),FECHA_SOL=CONVERT(VARCHAR(10),FECHA_SOLICITUD,103),* from RRHH_SOLICITUD_RECLUTAMIENTO with (nolock) where ID='" + cmb_solicitudes.SelectedItem.Value.ToString() + "' order by ID DESC";
                DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql);
                if (dt.Rows.Count > 0)
                {
                    txt_fecha_solicitud.Text = dt.Rows[0]["FECHA_SOL"].ToString();
                    cmb_razon_social.SelectedIndex = cmb_razon_social.Items.IndexOf(cmb_razon_social.Items.FindByValue(dt.Rows[0]["RAZON_SOCIAL"].ToString()));
                    cmb_area.SelectedIndex = cmb_area.Items.IndexOf(cmb_area.Items.FindByText(dt.Rows[0]["AREA"].ToString()));
                    cmb_area_SelectedIndexChanged(sender, e);
                    cmb_nombre_puesto.SelectedIndex = cmb_nombre_puesto.Items.IndexOf(cmb_nombre_puesto.Items.FindByText(dt.Rows[0]["NOMBRE_PUESTO"].ToString()));
                    txt_fecha_vacante.Text = dt.Rows[0]["FECHA_VAC"].ToString();
                    rbtl_tipo.SelectedIndex = rbtl_tipo.Items.IndexOf(rbtl_tipo.Items.FindByValue(dt.Rows[0]["INFO_VACANTE"].ToString()));
                    rbtl_motivo.SelectedIndex = rbtl_motivo.Items.IndexOf(rbtl_motivo.Items.FindByValue(dt.Rows[0]["MOTIVO_VACANTE"].ToString()));
                    rbtl_ded_esp.SelectedIndex = rbtl_ded_esp.Items.IndexOf(rbtl_ded_esp.Items.FindByValue(dt.Rows[0]["DEDICACION_ESP"].ToString()));
                    txt_cant_hs.Text = dt.Rows[0]["CANT_HS"].ToString();
                    txt_horario_de.Text = dt.Rows[0]["HORA_DE"].ToString();
                    txt_horario_hasta.Text = dt.Rows[0]["HORA_HASTA"].ToString();
                    txt_horario_yde.Text = dt.Rows[0]["HORA_DE_Y"].ToString();
                    txt_horario_yhasta.Text = dt.Rows[0]["HORA_HASTA_Y"].ToString();
                    cmb_dia_de.SelectedIndex = cmb_dia_de.Items.IndexOf(cmb_dia_de.Items.FindByValue(dt.Rows[0]["DIA_DE"].ToString()));
                    cmb_dia_a.SelectedIndex = cmb_dia_a.Items.IndexOf(cmb_dia_a.Items.FindByValue(dt.Rows[0]["DIA_HASTA"].ToString()));
                    rbtl_turno_t.SelectedIndex = rbtl_turno_t.Items.IndexOf(rbtl_turno_t.Items.FindByValue(dt.Rows[0]["TURNO"].ToString()));
                    rbtl_deb_v.SelectedIndex = rbtl_deb_v.Items.IndexOf(rbtl_deb_v.Items.FindByValue(dt.Rows[0]["DEBE_VIAJAR"].ToString()));
                    txt_zonas.Text = dt.Rows[0]["ZONAS"].ToString();
                    rbtl_deb_c.SelectedIndex = rbtl_deb_c.Items.IndexOf(rbtl_deb_c.Items.FindByValue(dt.Rows[0]["DEBE_CONDUCIR"].ToString()));
                    rbtl_deb_cm.SelectedIndex = rbtl_deb_cm.Items.IndexOf(rbtl_deb_cm.Items.FindByValue(dt.Rows[0]["MOV_PROPIA"].ToString()));
                    txt_objetivo.Text = dt.Rows[0]["CONT_PUESTO_TRABAJO"].ToString();
                    txt_responsabilildades.Text = dt.Rows[0]["RESPONSABILIDADES"].ToString();
                    txt_funciones.Text = dt.Rows[0]["FUNCIONES"].ToString();
                    txt_relacion.Text = dt.Rows[0]["RELACION"].ToString();
                    txt_edad_minima.Text = dt.Rows[0]["EDAD_MINIMA"].ToString();
                    txt_edad_maxima.Text = dt.Rows[0]["EDAD_MAXIMA"].ToString();
                    rbtl_genero.SelectedIndex = rbtl_genero.Items.IndexOf(rbtl_genero.Items.FindByValue(dt.Rows[0]["GENERO"].ToString()));
                    rbtl_genero_estado_civil.SelectedIndex = rbtl_genero_estado_civil.Items.IndexOf(rbtl_genero_estado_civil.Items.FindByValue(dt.Rows[0]["ESTADO_CIVIL"].ToString()));
                    rbtl_formacion.SelectedIndex = rbtl_formacion.Items.IndexOf(rbtl_formacion.Items.FindByValue(dt.Rows[0]["FORMACION"].ToString()));
                    rbtl_formacion_estado.SelectedIndex = rbtl_formacion_estado.Items.IndexOf(rbtl_formacion_estado.Items.FindByValue(dt.Rows[0]["ESTADO_FORMACION"].ToString()));
                    txt_titulo.Text = dt.Rows[0]["TITULO"].ToString();
                    txt_experiencia.Text = dt.Rows[0]["EXPERIENCIA"].ToString();
                    rbtl_experiencia.SelectedIndex = rbtl_experiencia.Items.IndexOf(rbtl_experiencia.Items.FindByValue(dt.Rows[0]["TIEMPO"].ToString()));
                    cmb_convenio.SelectedIndex = cmb_convenio.Items.IndexOf(cmb_convenio.Items.FindByValue(dt.Rows[0]["COD_CONV"].ToString()));
                    cmb_convenio_SelectedIndexChanged(sender, e);
                    cmb_categorias.SelectedIndex = cmb_categorias.Items.IndexOf(cmb_categorias.Items.FindByValue(dt.Rows[0]["COD_CATEGORIA"].ToString()));
                    chk_interno.Checked = (bool)dt.Rows[0]["INTERNO"];
                    if (chk_interno.Checked)
                    {
                        cmb_cliente.Enabled = false;
                        cmb_cliente.SelectedIndex = cmb_cliente.Items.IndexOf(cmb_cliente.Items.FindByValue("99999"));
                        
                    }
                    else
                    {
                        cmb_cliente.Enabled = true;
                        cmb_cliente.SelectedIndex = cmb_cliente.Items.IndexOf(cmb_cliente.Items.FindByValue(dt.Rows[0]["COD_CLIENTE"].ToString()));
                       
                      
                    }
                    cmb_cliente_SelectedIndexChanged(sender, e);
                    cmb_objetivo.SelectedIndex = cmb_objetivo.Items.IndexOf(cmb_objetivo.Items.FindByValue(dt.Rows[0]["COD_OBJETIVO"].ToString()));
                }
            }
            else
            {
                Response.Redirect("RRHH_SOLICITUD_RECLUTAMIENTO_REPORTE_TODOS.aspx");
            }
        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }
    }

    protected void cmb_convenio_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            cmb_categorias.Items.Clear();
            if (cmb_convenio.SelectedItem.Value.ToString() != "SELECCIONE")
            {

                string sql = "select ID=SJTCAT_CODIGO, DESCRIPCION=SJTCAT_DESCRP from SJTCAT with(nolock) where USR_SJTCAT_CONVEN='" + cmb_convenio.SelectedItem.Value.ToString() + "' ORDER BY SJTCAT_DESCRP";
                DataTable dt = Interfaz.EjecutarConsultaBD("CBS", sql);
                if (dt.Rows.Count > 0)
                {
                    cmb_categorias.Items.Add(new ListEditItem("SELECCIONE", "SELECCIONE"));
                    foreach (DataRow dr in dt.Rows)
                    {
                        cmb_categorias.Items.Add(new ListEditItem(dr["DESCRIPCION"].ToString(), dr["ID"].ToString()));


                    }
                    cmb_categorias.SelectedIndex = 0;
                }
            }
        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }

    }
    protected void chk_interno_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            FailureText.Text = "";
            if (chk_interno.Checked)
            {
                cmb_cliente.Enabled = false;
                cmb_cliente.SelectedIndex = cmb_cliente.Items.IndexOf(cmb_cliente.Items.FindByValue("99999"));
                cmb_cliente_SelectedIndexChanged(sender, e);
                
                //cmb_objetivo.Enabled = false;
            }
            else
            {
                cmb_cliente.Enabled = true;
                cmb_cliente.SelectedIndex = cmb_cliente.Items.IndexOf(cmb_cliente.Items.FindByValue("SELECCIONE"));
                cmb_cliente_SelectedIndexChanged(sender, e);
                
                //cmb_objetivo.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }

    }

    protected void cmb_cliente_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            cmb_objetivo.Items.Clear();
            if (cmb_cliente.SelectedIndex>-1 && cmb_cliente.SelectedItem.Text != "SELECCIONE")
            {
                string sql = "select USR_CLIOBJ_CODOBJ,USR_CLIOBJ_OBJDSC from USR_CLIOBJ with(nolock) where USR_CLIOBJ_CODCLI='" + cmb_cliente.SelectedItem.Value.ToString() + "' order by USR_CLIOBJ_OBJDSC";
                DataTable dt = Interfaz.EjecutarConsultaBD("CBS", sql);
                if (dt.Rows.Count > 0)
                {
                    cmb_objetivo.Items.Add(new ListEditItem("SELECCIONE", "SELECCIONE"));
                    foreach (DataRow dr in dt.Rows)
                    {
                        cmb_objetivo.Items.Add(new ListEditItem(dr["USR_CLIOBJ_OBJDSC"].ToString(), dr["USR_CLIOBJ_CODOBJ"].ToString()));


                    }
                    cmb_objetivo.SelectedIndex = 0;
                }

            }

        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }

    }

    protected void cmb_area_SelectedIndexChanged(object sender, EventArgs e)
    {
        cmb_nombre_puesto.Items.Clear();

        if (cmb_area.SelectedItem.Value.ToString() != "SELECCIONE")
        {
            DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", "SELECT ID, DESCRIPCION FROM RRHH_REC_PUESTO_SECTOR ps with(nolock) left join RRHH_REC_PUESTOS p ON p.id=ps.ID_PUESTO WHERE ps.ID_SECTOR=" + cmb_area.SelectedItem.Value.ToString() + "  order by DESCRIPCION");
            if (dt.Rows.Count > 0)
            {
                cmb_nombre_puesto.Items.Add(new ListEditItem("SELECCIONE", "SELECCIONE"));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmb_nombre_puesto.Items.Add(new ListEditItem(dt.Rows[i]["DESCRIPCION"].ToString(), dt.Rows[i]["ID"].ToString()));

                }
                cmb_nombre_puesto.SelectedIndex = 0;
            }

        }
    }

    protected void cmb_nombre_puesto_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmb_nombre_puesto.SelectedItem.Value.ToString().Length > 0 && cmb_nombre_puesto.SelectedItem.Value.ToString() != "SELECCIONE")
        {
            DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", "SELECT * FROM RRHH_REC_PUESTOS with(nolock) WHERE ID=" + cmb_nombre_puesto.SelectedItem.Value.ToString() + "  order by DESCRIPCION");
            if (dt.Rows.Count > 0)
            {
                txt_funciones.Text = dt.Rows[0]["COMPETENCIAS"].ToString();
                txt_relacion.Text = dt.Rows[0]["RELACION_CON"].ToString();
                txt_responsabilildades.Text = dt.Rows[0]["RESPONSABILIDADES"].ToString();
                txt_objetivo.Text = dt.Rows[0]["CONTENIDO_PUESTO"].ToString();
            }
        }
        else
        {
            txt_funciones.Text = "";
            txt_relacion.Text = "";
            txt_responsabilildades.Text = "";
            txt_objetivo.Text = "";
        }
    }

    protected void rbtl_ded_esp_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtl_ded_esp.SelectedItem.Value.ToString() == "Part time")
        {
            txt_cant_hs.Enabled = true;
            txt_cant_hs.Focus();
        }
        else
        {
            txt_cant_hs.Enabled = false;
        }
    }


    protected void rbtl_deb_v_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtl_deb_v.SelectedItem.Value.ToString() == "Si")
        {
            txt_zonas.Enabled = true;
            txt_zonas.Focus();
        }
        else
        {
            txt_zonas.Enabled = false;
        }
    }

    protected void rbtl_deb_c_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtl_deb_c.SelectedItem.Value.ToString() == "Si")
        {
            rbtl_deb_cm.Enabled = true;
            rbtl_deb_cm.Focus();
        }
        else
        {
            rbtl_deb_cm.Enabled = false;
        }
    }

    protected void rbtl_formacion_estado_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtl_formacion_estado.SelectedItem.Value.ToString() == "Completo")
        {
            txt_titulo.Enabled = true;
            txt_titulo.Focus();
        }
        else
        {
            txt_titulo.Enabled = false;
        }
    }

    protected void txt_cuil_TextChanged(object sender, EventArgs e)
    {

        try
        {
            
            //if (txt_cuil.Text.Length==11)
            //{
            //    string sql = "select * from RRHH_SOLICITUD_INGRESO with(nolock) where cuil='" + txt_cuil.Text + "'";
            //    DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql);

            //    if (dt.Rows.Count > 0)
            //    {
            //        FailureText.Text = "EL CUIL INGRESADO YA ESTA CARGADO EN LA BASE DE DATOS.";
            //        txt_cuil.Focus();
            //    }
            //    else
            //    {
            //        FailureText.Text = "";
            //        txt_apellido.Focus();
            //    }




            //}

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
            if (cmb_razon_social.SelectedItem.Text != "SELECCIONE" && cmb_solicitudes.SelectedItem.Text != "SELECCIONE" && cmb_convenio.SelectedItem.Text != "SELECCIONE" && cmb_categorias.SelectedItem.Text != "SELECCIONE")
            {
               
                    string sql = "select mail, usuario, nombre from USUARIOS with (nolock) where idusuario=" + Session["usuario"].ToString();
                    DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql);

                    if (dt.Rows.Count > 0)
                    {
                        DateTime FECHA_SOLICITUD = Convert.ToDateTime(txt_fecha_solicitud.Text);
                        string RAZON_SOCIAL = ((cmb_razon_social.SelectedIndex > 0) ? cmb_razon_social.SelectedItem.Value.ToString() : "");
                        string NOMBRE_PUESTO = ((cmb_nombre_puesto.SelectedItem.Text.Length > 0 && cmb_nombre_puesto.SelectedItem.Text != "SELECCIONE") ? cmb_nombre_puesto.SelectedItem.Text : "");
                        string AREA = ((cmb_area.SelectedItem.Text.Length > 0 && cmb_area.SelectedItem.Text != "SELECCIONE") ? cmb_area.SelectedItem.Text : "");
                        DateTime FECHA_VACANTE = ((txt_fecha_vacante.Text.Length > 0) ? Convert.ToDateTime(txt_fecha_vacante.Text) : Convert.ToDateTime("1900-01-01"));
                        string INFO_VACANTE = rbtl_tipo.SelectedItem.Value.ToString();
                        string MOTIVO_VACANTE = rbtl_motivo.SelectedItem.Value.ToString();
                        string DEDICACION_ESP = rbtl_ded_esp.SelectedItem.Value.ToString();
                        string CANT_HS = ((txt_cant_hs.Enabled == false) ? "" : txt_cant_hs.Text);
                        string HORA_DE = ((txt_horario_de.Text.Length > 0) ? txt_horario_de.Text : "");
                        string HORA_HASTA = ((txt_horario_hasta.Text.Length > 0) ? txt_horario_hasta.Text : "");
                        string HORA_DE_Y = ((txt_horario_yde.Text.Length > 0) ? txt_horario_yde.Text : "");
                        string HORA_HASTA_Y = ((txt_horario_yhasta.Text.Length > 0) ? txt_horario_yhasta.Text : "");
                        string DIA_DE = ((cmb_dia_de.SelectedIndex > -1) ? cmb_dia_de.SelectedItem.Value.ToString() : "");
                        string DIA_HASTA = ((cmb_dia_a.SelectedIndex > -1) ? cmb_dia_a.SelectedItem.Value.ToString() : "");
                        string TURNO = rbtl_turno_t.SelectedItem.Value.ToString();
                        string DEBE_VIAJAR = rbtl_deb_v.SelectedItem.Value.ToString();
                        string ZONAS = ((txt_zonas.Enabled == false) ? "" : txt_zonas.Text);
                        string DEBE_CONDUCIR = rbtl_deb_c.SelectedItem.Value.ToString();
                        string MOV_PROPIA = ((rbtl_deb_cm.Enabled == false) ? "" : rbtl_deb_cm.SelectedItem.Value.ToString());
                        string CONT_PUESTO_TRABAJO = ((txt_objetivo.Text.Length > 0) ? txt_objetivo.Text : "");
                        string RESPONSABILIDADES = ((txt_responsabilildades.Text.Length > 0) ? txt_responsabilildades.Text : "");
                        string FUNCIONES = ((txt_funciones.Text.Length > 0) ? txt_funciones.Text : "");
                        string RELACION = ((txt_relacion.Text.Length > 0) ? txt_relacion.Text : "");
                        string EDAD_MINIMA = ((txt_edad_minima.Text.Length > 0) ? txt_edad_minima.Text : "");
                        string EDAD_MAXIMA = ((txt_edad_maxima.Text.Length > 0) ? txt_edad_maxima.Text : "");

                        string GENERO = rbtl_genero.SelectedItem.Value.ToString();
                        string ESTADO_CIVIL = rbtl_genero_estado_civil.SelectedItem.Value.ToString();
                        string FORMACION = rbtl_formacion.SelectedItem.Value.ToString();
                        string ESTADO_FORMACION = rbtl_formacion_estado.SelectedItem.Value.ToString();
                        string TITULO = ((txt_titulo.Enabled == false) ? "" : txt_titulo.Text);
                        string EXPERIENCIA = ((txt_experiencia.Text.Length > 0) ? txt_experiencia.Text : "");
                        string TIEMPO = rbtl_experiencia.SelectedItem.Value.ToString();

                        string Convenio = ((cmb_convenio.SelectedIndex > 0) ? cmb_convenio.SelectedItem.Value.ToString() : "");
                        string Categoria = ((cmb_categorias.SelectedIndex > -1) ? cmb_categorias.SelectedItem.Value.ToString() : "");
                        bool Interno = ((chk_interno.Checked) ? true : false);
                        string Cliente = ((chk_interno.Checked) ? "99999" : cmb_cliente.SelectedItem.Value.ToString());
                        string Objetivo = ((cmb_objetivo.SelectedIndex > -1) ? cmb_objetivo.SelectedItem.Value.ToString() : "");

                        Interfaz.Editar_SOLICITUD_RECLUTAMIENTO(cmb_solicitudes.SelectedItem.Value.ToString(), FECHA_SOLICITUD, RAZON_SOCIAL, NOMBRE_PUESTO, AREA, FECHA_VACANTE, INFO_VACANTE, MOTIVO_VACANTE, DEDICACION_ESP, CANT_HS, HORA_DE, HORA_HASTA, HORA_DE_Y, HORA_HASTA_Y, DIA_DE, DIA_HASTA, TURNO, DEBE_VIAJAR, ZONAS, DEBE_CONDUCIR, MOV_PROPIA, CONT_PUESTO_TRABAJO, RESPONSABILIDADES, FUNCIONES, RELACION, EDAD_MINIMA, EDAD_MAXIMA, GENERO, ESTADO_CIVIL, FORMACION, ESTADO_FORMACION, TITULO, EXPERIENCIA, TIEMPO, Session["usuario"].ToString(), Convenio, Categoria, Interno, Cliente, Objetivo);

                        

                       /* MailMessage mail = new MailMessage();
                        string EMAIL_DE = dt.Rows[0]["mail"].ToString();
                        mail.From = new MailAddress(EMAIL_DE);
                        mail.To.Add("seleccion@grupo-cbs.com");
                        //mail.To.Add("gabriel.alonso@grupo-cbs.com");




                        mail.Subject = "Solicitud de Reclutamiento";
                        mail.IsBodyHtml = true;
                        mail.Body = "Estimada/o:<BR>Se informa que se incio el proceso de Reclutamiento para " + txt_a_generar.Text + " reclutamiento/s, solicitado por:<b> " + dt.Rows[0]["nombre"].ToString() + "</b><BR>";
                        mail.Body += "El proceso debe cubrir el puesto de " + NOMBRE_PUESTO + " para la fecha:<b>" + FECHA_VACANTE.ToShortDateString() + "</b><BR>";
                        mail.Body += "<BR>Para ver o modificar este registro ingrese a Intranet Grupo CBS, Menu RRHH->Reclutamientos->Reporte de Solicitudes: <a href='http://192.168.1.141/RRHH_SOLICITUD_RECLUTAMIENTO_REPORTE_2.aspx' target='_blank'>Reporte de Solicitudes</a><BR>";


                        string EMAIL_SMTP = System.Configuration.ConfigurationSettings.AppSettings.Get("EMAIL_SMTP");
                        SmtpClient smtp = new SmtpClient(EMAIL_SMTP, 587);
                        smtp.Timeout = 10000;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new System.Net.NetworkCredential(System.Configuration.ConfigurationSettings.AppSettings.Get("USUARIO_SMTP"), System.Configuration.ConfigurationSettings.AppSettings.Get("PASS_SMTP"));
                        smtp.Send(mail);
                        */

                        Response.Redirect("RRHH_SOLICITUD_RECLUTAMIENTO_REPORTE_TODOS.aspx");
                    }
                    else
                    {
                        FailureText.Text = "No se puede guardar.El usuario logueado no tiene mail valido en Intranet";
                    }
              
            }
            else
            {
                FailureText.Text = "Es requerido seleccionar razon social";
                cmb_razon_social.Focus();
            }

        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;

        }
    }

    protected void btn_eliminar_Click(object sender, EventArgs e)
    {
        try
        {
            if (cmb_solicitudes.SelectedItem.Text != "SELECCIONE")
            {
                string sql = "select id from RRHH_SOLICITUD_INGRESO with (nolock) where ID_RECLUTAMIENTO=" + cmb_solicitudes.SelectedItem.Value.ToString();
                DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql);

                if (dt.Rows.Count == 0)
                {

                    sql = "select mail, usuario, nombre from USUARIOS with (nolock) where idusuario=" + Session["usuario"].ToString();
                    dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql);

                    if (dt.Rows.Count > 0)
                    {

                        Interfaz.Eliminar_SOLICITUD_RECLUTAMIENTO(cmb_solicitudes.SelectedItem.Value.ToString());

                        try
                        {
                            MailMessage mail = new MailMessage();
                            string EMAIL_DE = dt.Rows[0]["mail"].ToString();
                            mail.From = new MailAddress(EMAIL_DE);
                            mail.To.Add("seleccion@grupo-cbs.com");
                            //mail.To.Add("gabriel.alonso@grupo-cbs.com");


                            mail.Subject = "Eliminacion de Solicitud de Reclutamiento";
                            mail.IsBodyHtml = true;
                            mail.Body = "Estimada/o:<BR>Se informa que el usuario:" + dt.Rows[0]["nombre"].ToString() + ", elimino la solicitud de Reclutamiento Nro:<b>" + cmb_solicitudes.SelectedItem.Value.ToString() + "</b><BR>";

                            mail.Body += "<BR>Para ver o modificar este registro ingrese a Intranet Grupo CBS, Menu RRHH->Reclutamientos->Reporte de Solicitudes: <a href='http://192.168.1.141/RRHH_SOLICITUD_RECLUTAMIENTO_REPORTE_2.aspx' target='_blank'>Reporte de Solicitudes</a><BR>";


                            string EMAIL_SMTP = System.Configuration.ConfigurationSettings.AppSettings.Get("EMAIL_SMTP");
                            SmtpClient smtp = new SmtpClient(EMAIL_SMTP, 587);
                            smtp.Timeout = 10000;
                            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtp.EnableSsl = true;
                            smtp.UseDefaultCredentials = false;
                            smtp.Credentials = new System.Net.NetworkCredential(System.Configuration.ConfigurationSettings.AppSettings.Get("USUARIO_SMTP"), System.Configuration.ConfigurationSettings.AppSettings.Get("PASS_SMTP"));
                            smtp.Send(mail);
                        }
                        catch (Exception ex)
                        {
                            FailureText.Text = ex.Message;

                        }


                        Response.Redirect("RRHH_SOLICITUD_RECLUTAMIENTO_REPORTE_TODOS.aspx");
                    }
                    else
                    {
                        FailureText.Text = "No se puede guardar.El usuario logueado no tiene mail valido en Intranet";
                    }
                }
                else
                {
                    FailureText.Text = "No se puede eliminar el registro ya que esta relacionado a la Solicitud de Ingreso Nro:" + dt.Rows[0]["id"].ToString() + ". Elimine primero la solicitud de ingreso y luego continue.";
                }

            }
            else
            {
                FailureText.Text = "Es requerido seleccionar razon social";
                cmb_razon_social.Focus();
            }

        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;

        }
    }

    //private void LimpiarPantalla()
    //{
    //    try
    //    {
    //        txt_fecha_alta.Text = DateTime.Now.ToShortDateString();
    //        txt_fecha_baja.Text = "";
    //        txt_cuil.Text = "";
    //        txt_apellido.Text = "";
    //        txt_domicilio_real.Text = "";
    //        txt_nombres.Text = "";
    //        txt_remuneracion.Text = "";
    //        txt_tareas.Text = "";
    //        cmb_centro_costo.SelectedIndex = 0;
    //        cmb_cliente.SelectedIndex = 0;
    //        cmb_convenio.SelectedIndex = 0;
    //        cmb_objetivo.Items.Clear();
    //        cmb_obra_social.SelectedIndex = 0;
    //        chk_interno.Checked = false;
    //        cmb_objetivo.Enabled = true;
    //        cmb_cliente.Enabled = true;

    //    }
    //    catch (Exception ex)
    //    {
    //        FailureText.Text = ex.Message;
           
    //    }
    //}
}