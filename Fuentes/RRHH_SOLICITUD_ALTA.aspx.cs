using System;
using System.Drawing;
using System.IO;
using DevExpress.Web;
using DevExpress.Web.Internal;
using System.Data;
using System.Collections.Generic;
using System.Net.Mail;
using System.Configuration;

public partial class RRHH_SOLICITUD_ALTA : System.Web.UI.Page
{
  
    protected void Page_Load(object sender, EventArgs e)
    {
      
        //Session.Add("usuario", "1");
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


            string sql = "select VTMCLH_NROCTA,VTMCLH_NOMBRE from VTMCLH with (nolock)  order by VTMCLH_NOMBRE";
            DataTable dt = Interfaz.EjecutarConsultaBD("CBS", sql);
            cmb_cliente.Items.Add(new ListEditItem("SELECCIONE", "SELECCIONE"));
            foreach (DataRow dr in dt.Rows)
            {
                cmb_cliente.Items.Add(new ListEditItem(dr["VTMCLH_NOMBRE"].ToString(), dr["VTMCLH_NROCTA"].ToString()));
            }
            cmb_cliente.SelectedIndex = 0;

            sql = "select SJTOSO_CODOSO, SJTOSO_DESCRP from SJTOSO with (nolock) order by SJTOSO_DESCRP";
            dt = Interfaz.EjecutarConsultaBD("CBS", sql);
            cmb_obra_social.Items.Add(new ListEditItem("SELECCIONE", "SELECCIONE"));
            foreach (DataRow dr in dt.Rows)
            {
                cmb_obra_social.Items.Add(new ListEditItem(dr["SJTOSO_DESCRP"].ToString(), dr["SJTOSO_CODOSO"].ToString()));
            }
            cmb_obra_social.SelectedIndex = 0;


            sql = "select USR_SJTCON_CODIGO,USR_SJTCON_DESCRP  from USR_SJTCON with (nolock)   order by USR_SJTCON_DESCRP";
            dt = Interfaz.EjecutarConsultaBD("CBS", sql);
            cmb_convenio.Items.Add(new ListEditItem("SELECCIONE", "SELECCIONE"));
            foreach (DataRow dr in dt.Rows)
            {
                cmb_convenio.Items.Add(new ListEditItem(dr["USR_SJTCON_DESCRP"].ToString(), dr["USR_SJTCON_CODIGO"].ToString()));

            }
            cmb_convenio.SelectedIndex = 0;

            sql = "select USR_CTRCST_CODIGO,USR_CTRCST_DESCRP from USR_CTRCST with (nolock) order by USR_CTRCST_DESCRP";
            dt = Interfaz.EjecutarConsultaBD("CBS", sql);
            cmb_centro_costo.Items.Add(new ListEditItem("SELECCIONE", "SELECCIONE"));
            foreach (DataRow dr in dt.Rows)
            {
                cmb_centro_costo.Items.Add(new ListEditItem(dr["USR_CTRCST_DESCRP"].ToString(), dr["USR_CTRCST_CODIGO"].ToString()));
            }
            cmb_centro_costo.SelectedIndex = 0;

            sql = "select ID=GRTPAC_CODPOS, DES=GRTJUR_DESCRP +'-'+ GRTPAC_DESCRP from GRTPAC c with(nolock) left join GRTJUR j with(nolock) on c.GRTPAC_CODPRO=j.GRTJUR_JURISD where j.GRTJUR_JURISD in ('916','915','918') order by GRTJUR_DESCRP +'-'+ GRTPAC_DESCRP";
            dt = Interfaz.EjecutarConsultaBD("CBS", sql);
            cmb_localidad.Items.Add(new ListEditItem("SELECCIONE", "SELECCIONE"));
            foreach (DataRow dr in dt.Rows)
            {
                cmb_localidad.Items.Add(new ListEditItem(dr["DES"].ToString(), dr["ID"].ToString()));
            }
            cmb_localidad.SelectedIndex = 0;

          

            txt_fecha_alta.Text = DateTime.Now.ToShortDateString();


            CargarSolicitudes();
            
        }

         
       
    }

    protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        switch (((ASPxCheckBox)sender).ID)
        {
            case "chk_utiliza_pc":
                {
                    if (((ASPxCheckBox)sender).Checked)
                    {
                        txt_obs_utiliza_pc.Focus();
                        txt_obs_utiliza_pc.Enabled = true;
                    }
                    else
                    {
                        txt_obs_utiliza_pc.Enabled = false;
                    }
                    break;
                }
            case "chk_utiliza_cuenta_google":
                {
                    if (((ASPxCheckBox)sender).Checked)
                    {
                        txt_obs_cuenta_google.Focus();
                        txt_obs_cuenta_google.Enabled = true;
                    }
                    else
                    {
                        txt_obs_cuenta_google.Enabled = false;
                    }
                    break;
                }

            case "chk_utiliza_cuenta_local":
                {
                    if (((ASPxCheckBox)sender).Checked)
                    {
                        txt_obs_cuenta_local.Focus();
                        txt_obs_cuenta_local.Enabled = true;
                    }
                    else
                    {
                        txt_obs_cuenta_local.Enabled = false;
                    }
                    break;
                }
            case "chk_utiliza_intranet":
                {
                    if (((ASPxCheckBox)sender).Checked)
                    {
                        txt_intranet.Focus();
                        txt_intranet.Enabled = true;
                    }
                    else
                    {
                        txt_intranet.Enabled = false;
                    }
                    break;
                }
            case "chk_tango":
                {
                    if (((ASPxCheckBox)sender).Checked)
                    {
                        txt_tango.Focus();
                        txt_tango.Enabled = true;
                    }
                    else
                    {
                        txt_tango.Enabled = false;
                    }
                    break;
                }
            case "chk_softland":
                {
                    if (((ASPxCheckBox)sender).Checked)
                    {
                        txt_softland.Focus();
                        txt_softland.Enabled = true;
                    }
                    else
                    {
                        txt_softland.Enabled = false;
                    }
                    break;
                }
            case "chk_sisitema_rrhh":
                {
                    if (((ASPxCheckBox)sender).Checked)
                    {
                        txt_sistema_rrhh.Focus();
                        txt_sistema_rrhh.Enabled = true;
                    }
                    else
                    {
                        txt_sistema_rrhh.Enabled = false;
                    }
                    break;
                }
            case "chk_sistema_stock":
                {
                    if (((ASPxCheckBox)sender).Checked)
                    {
                        txt_sistema_stock.Focus();
                        txt_sistema_stock.Enabled = true;
                    }
                    else
                    {
                        txt_sistema_stock.Enabled = false;
                    }
                    break;
                }
            case "chk_compartida":
                {
                    if (((ASPxCheckBox)sender).Checked)
                    {
                        txt_compartida.Focus();
                        txt_compartida.Enabled = true;
                    }
                    else
                    {
                        txt_compartida.Enabled = false;
                    }
                    break;
                }
            case "chk_office":
                {
                    if (((ASPxCheckBox)sender).Checked)
                    {
                        txt_office.Focus();
                        txt_office.Enabled = true;
                    }
                    else
                    {
                        txt_office.Enabled = false;
                    }
                    break;
                }
            case "chk_impresora":
                {
                    if (((ASPxCheckBox)sender).Checked)
                    {
                        txt_impresora.Focus();
                        txt_impresora.Enabled = true;
                    }
                    else
                    {
                        txt_impresora.Enabled = false;
                    }
                    break;
                }
            case "chk_scanner":
                {
                    if (((ASPxCheckBox)sender).Checked)
                    {
                        txt_scanner.Focus();
                        txt_scanner.Enabled = true;
                    }
                    else
                    {
                        txt_scanner.Enabled = false;
                    }
                    break;
                }
            case "chk_smartphone":
                {
                    if (((ASPxCheckBox)sender).Checked)
                    {
                        txt_smartphone.Focus();
                        txt_smartphone.Enabled = true;
                    }
                    else
                    {
                        txt_smartphone.Enabled = false;
                    }
                    break;
                }
            case "chk_app":
                {
                    if (((ASPxCheckBox)sender).Checked)
                    {
                        txt_app.Focus();
                        txt_app.Enabled = true;
                    }
                    else
                    {
                        txt_app.Enabled = false;
                    }
                    break;
                }
        }
    }

    private void CargarSolicitudes()
    {
        cmb_solicitudes.Items.Clear();
        string sql = "select ID,N=CAST(ID AS VARCHAR) + ' - ' + NOMBRE_PUESTO + ' '+ AREA + ' - ' + CONVERT(VARCHAR(10),FECHA_SOLICITUD,103) + ' - ' + cast(u.USUARIO as varchar) COLLATE Modern_Spanish_CI_AS + ' - ' + CASE WHEN PENDIENTE=1 THEN 'PENDIENTE' ELSE 'FINALIZADA' END from RRHH_SOLICITUD_RECLUTAMIENTO r with (nolock)  left join USUARIOS u with(nolock) on r.usuario=u.IDUSUARIO WHERE pendiente=0 and r.usuario='" + Session["usuario"].ToString() + "' and id not in (select id_reclutamiento from [dbo].[RRHH_SOLICITUD_INGRESO] with(nolock) where FECHA_ALTA is not null)   order by id desc";
        //string sql = "select ID,N=CAST(ID AS VARCHAR) + ' - ' + NOMBRE_PUESTO + ' '+ AREA + ' - ' + CONVERT(VARCHAR(10),FECHA_SOLICITUD,103) + ' - ' + cast(u.USUARIO as varchar) COLLATE Modern_Spanish_CI_AS + ' - ' + CASE WHEN PENDIENTE=1 THEN 'PENDIENTE' ELSE 'FINALIZADA' END from RRHH_SOLICITUD_RECLUTAMIENTO r with (nolock)  left join USUARIOS u with(nolock) on r.usuario=u.IDUSUARIO  order by id desc";
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
            string sql = "select i.* from RRHH_SOLICITUD_INGRESO i with(nolock) inner join RRHH_SOLICITUD_RECLUTAMIENTO r with(nolock) on i.ID_RECLUTAMIENTO=r.ID where r.id='" + cmb_solicitudes.SelectedItem.Value.ToString() + "'";
            DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql);
            if (dt.Rows.Count > 0)
            {
                txt_nro_alta.Text = dt.Rows[0]["ID"].ToString();
                cmb_razon_social.SelectedIndex = cmb_razon_social.Items.IndexOf(cmb_razon_social.Items.FindByValue(dt.Rows[0]["RAZON_SOCIAL"].ToString()));
                txt_cuil.Text = dt.Rows[0]["CUIL"].ToString();
                txt_apellido.Text = dt.Rows[0]["APELLIDO"].ToString();
                txt_nombres.Text = dt.Rows[0]["NOMBRES"].ToString();
                txt_domicilio_real.Text = dt.Rows[0]["DOMICILIO_REAL"].ToString();
                txt_observaciones.Text = dt.Rows[0]["OBSERVACIONES"].ToString();
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

                cmb_tipo_contrato.Items.Clear();
                string sql1 = "select ID, NOMBRE FROM RRHH_TIPOS_CONTRATOS with (nolock) WHERE EMPRESA='" + dt.Rows[0]["RAZON_SOCIAL"].ToString() + "' order by NOMBRE";
                DataTable dt1 = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql1);
                cmb_tipo_contrato.Items.Add(new ListEditItem("SELECCIONE", "SELECCIONE"));
                foreach (DataRow dr in dt1.Rows)
                {
                    cmb_tipo_contrato.Items.Add(new ListEditItem(dr["NOMBRE"].ToString(), dr["ID"].ToString()));
                }
                cmb_tipo_contrato.SelectedIndex = 0;



            }
        }
        else
        {
            Response.Redirect("RRHH_SOLICITUD_ALTA.aspx");
        }
    }

    protected void cmb_convenio_SelectedIndexChanged(object sender, EventArgs e)
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

    

    protected void chk_interno_CheckedChanged(object sender, EventArgs e)
    {
        FailureText.Text = "";
        if (chk_interno.Checked)
        {
            cmb_cliente.Enabled = false;
            cmb_cliente.SelectedIndex = cmb_cliente.Items.IndexOf(cmb_cliente.Items.FindByValue("99999"));
            cmb_cliente_SelectedIndexChanged(sender, e);
            cmb_objetivo.Focus();
            //cmb_objetivo.Enabled = false;
        }
        else
        {
            cmb_cliente.Enabled = true;
            cmb_cliente.SelectedIndex = cmb_cliente.Items.IndexOf(cmb_cliente.Items.FindByValue("SELECCIONE"));
            cmb_cliente_SelectedIndexChanged(sender, e);
            cmb_cliente.Focus();
            //cmb_objetivo.Enabled = true;
        }

    }

  

    protected void txt_cuil_TextChanged(object sender, EventArgs e)
    {

        try
        {
            
            if (txt_cuil.Text.Length==11)
            {
                string sql = "select NOMBRE=emp_apellido + ' ' + emp_nombre,ESTADO=CASE WHEN year(emp_fch_baja)=1000 THEN 'ACTIVO' ELSE 'INACTIVO' END from openquery([TANGOSRV], 'SELECT * FROM cbs_empleado') where emp_cuil='" + txt_cuil.Text + "'";
                DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql);

                if (dt.Rows.Count > 0)
                {
                    FailureText.Text = "IMPORTANTE!!!. EL CUIL INGRESADO YA ESTA CARGADO EN LA BASE DE DATOS DEL SISTEMA DE GESTION COMO:" + dt.Rows[0]["NOMBRE"].ToString() + ",ESTADO=" + dt.Rows[0]["ESTADO"].ToString();
                    txt_apellido.Focus();
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


    protected void cmb_cliente_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            cmb_objetivo.Items.Clear();
            if (cmb_cliente.SelectedItem.Text != "SELECCIONE")
            {
                string sql = "select USR_CLIOBJ_CODOBJ,USR_CLIOBJ_OBJDSC from USR_CLIOBJ with(nolock) where USR_CLIOBJ_CODCLI='"+cmb_cliente.SelectedItem.Value.ToString() +"' order by USR_CLIOBJ_OBJDSC";
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

    protected void btn_enviar_Click(object sender, EventArgs e)
    {
        try
        {
            if (cmb_solicitudes.Items.Count > 0 && cmb_solicitudes.SelectedItem.Text != "SELECCIONE" && cmb_convenio.SelectedItem.Text != "SELECCIONE" && cmb_centro_costo.SelectedItem.Text != "SELECCIONE" && txt_nro_alta.Text.Length > 0 && cmb_categorias.SelectedItem.Text != "SELECCIONE" && cmb_tipo_contrato.SelectedItem.Text != "SELECCIONE" && cmb_localidad.SelectedItem.Text != "SELECCIONE")
            {
                if ((chk_interno.Checked) || (!chk_interno.Checked && cmb_cliente.SelectedItem.Text != "SELECCIONE" && cmb_objetivo.Items.Count > 0 && cmb_objetivo.SelectedItem.Text != "SELECCIONE" && cmb_objetivo.SelectedItem.Text != ""))
                {
                   

                    string sql = "select mail, usuario, nombre from USUARIOS with (nolock) where idusuario=" + Session["usuario"].ToString();
                    DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql);

                    if (dt.Rows.Count > 0)
                    {
                        string RAZON_SOCIAL = ((cmb_razon_social.SelectedIndex > 0) ? cmb_razon_social.SelectedItem.Value.ToString() : "");
                        bool Interno = ((chk_interno.Checked) ? true : false);
                        string Cliente = ((chk_interno.Checked) ? "99999" : cmb_cliente.SelectedItem.Value.ToString());
                        string Objetivo = ((cmb_objetivo.SelectedIndex > -1) ? cmb_objetivo.SelectedItem.Value.ToString() : "");
                        string Localidad = ((cmb_localidad.SelectedIndex > 0) ? cmb_localidad.SelectedItem.Value.ToString() : "");

                        Interfaz.Editar_SOLICITUD_INGRESO(txt_nro_alta.Text, Convert.ToDateTime(txt_fecha_alta.Text), ((txt_fecha_baja.Text.Length > 0) ? Convert.ToDateTime(txt_fecha_baja.Text) : Convert.ToDateTime("01/01/1900")), txt_cuil.Text, txt_apellido.Text, txt_nombres.Text, txt_domicilio_real.Text, ((cmb_obra_social.SelectedItem.Text != "SELECCIONE") ? cmb_obra_social.SelectedItem.Value.ToString() : ""), cmb_convenio.SelectedItem.Value.ToString(), cmb_centro_costo.SelectedItem.Value.ToString(), Objetivo, dt.Rows[0]["usuario"].ToString(), Cliente, Interno, cmb_solicitudes.SelectedItem.Value.ToString(), RAZON_SOCIAL, cmb_categorias.SelectedItem.Value.ToString(), cmb_tipo_contrato.SelectedItem.Value.ToString(), txt_observaciones.Text, Localidad);

                        Guarda_HERINF(txt_nro_alta.Text);
                      

                        MailMessage mail = new MailMessage();

                        string EMAIL_DE = dt.Rows[0]["mail"].ToString();
                        mail.From = new MailAddress(EMAIL_DE);
                        mail.To.Add("gabriel.alonso@grupo-cbs.com");
                        mail.To.Add("altas@grupo-cbs.com");
                        mail.To.Add("inforrhh@grupo-cbs.com");
                        
                      

                        mail.Subject = "Solicitud de Ingreso";
                        mail.IsBodyHtml = true;
                        mail.Body = "Estimada/o:<BR>Se informa una nueva solicitud de Ingreso de Personal solicitado por:<b> " + dt.Rows[0]["nombre"].ToString() + "</b><BR><BR>";
                        mail.Body += "<b>Nro DE SOLICITUD DE RECLUTAMIENTO:</b> " + cmb_solicitudes.SelectedItem.Value.ToString() + "<BR>";
                        mail.Body += "<b>Nro DE SOLICITUD DE INGRESO:</b> " + txt_nro_alta.Text + "<BR>";

                        mail.Body += "<BR>Para enviar Documentacion sobre esta solicitud, ingrese a Intranet Grupo CBS, Menu RRHH->Ingresos->Envio de Documentacion a Sectores: <a href='http://192.168.1.141/RRHH_ALTA_DOC.aspx' target='_blank'>Envio de Documentacion a Sectores</a><BR>";


                        string EMAIL_SMTP = System.Configuration.ConfigurationSettings.AppSettings.Get("EMAIL_SMTP");
                        SmtpClient smtp = new SmtpClient(EMAIL_SMTP, 587);
                        smtp.Timeout = 10000;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new System.Net.NetworkCredential(System.Configuration.ConfigurationSettings.AppSettings.Get("USUARIO_SMTP"), System.Configuration.ConfigurationSettings.AppSettings.Get("PASS_SMTP"));
                        smtp.Send(mail);

                        FailureText.Text = "Enviado con exito";
                        LimpiarPantalla();
                    }
                    else
                    {
                        FailureText.Text = "No se puede guardar.El usuario logueado no tiene mail valido en Intranet";
                    }
                }
                else
                {
                    FailureText.Text = "Si el ingreso no es Interno, debe seleccionar Cliente y Objetivo";
                    cmb_cliente.Focus();
                }

            }
            else
            {
                FailureText.Text = "Es requerido seleccionar Convenio, Categoria,Centro de Costo, Tipo de Contrato y Localidad";
                cmb_convenio.Focus();
            }


             
        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
          
        }
    }

    private void Guarda_HERINF(string ID_INGRESO)
    {
        if (chk_utiliza_pc.Checked)
        {
            Interfaz.Alta_SOLICITUD_INGRESO_HERINFO(ID_INGRESO, chk_utiliza_pc.Text, txt_obs_utiliza_pc.Text);
        }

        if (chk_utiliza_cuenta_google.Checked)
        {
            Interfaz.Alta_SOLICITUD_INGRESO_HERINFO(ID_INGRESO, chk_utiliza_cuenta_google.Text, txt_obs_cuenta_google.Text);
        }

        if (chk_utiliza_cuenta_local.Checked)
        {
            Interfaz.Alta_SOLICITUD_INGRESO_HERINFO(ID_INGRESO, chk_utiliza_cuenta_local.Text, txt_obs_cuenta_local.Text);
        }

        if (chk_utiliza_intranet.Checked)
        {
            Interfaz.Alta_SOLICITUD_INGRESO_HERINFO(ID_INGRESO, chk_utiliza_intranet.Text, txt_intranet.Text);
        }

        if (chk_tango.Checked)
        {
            Interfaz.Alta_SOLICITUD_INGRESO_HERINFO(ID_INGRESO, chk_tango.Text, txt_tango.Text);
        }
        if (chk_softland.Checked)
        {
            Interfaz.Alta_SOLICITUD_INGRESO_HERINFO(ID_INGRESO, chk_softland.Text, txt_softland.Text);
        }
        if (chk_sisitema_rrhh.Checked)
        {
            Interfaz.Alta_SOLICITUD_INGRESO_HERINFO(ID_INGRESO, chk_sisitema_rrhh.Text, txt_sistema_rrhh.Text);
        }
        if (chk_sistema_stock.Checked)
        {
            Interfaz.Alta_SOLICITUD_INGRESO_HERINFO(ID_INGRESO, chk_sistema_stock.Text, txt_sistema_stock.Text);
        }
        if (chk_compartida.Checked)
        {
            Interfaz.Alta_SOLICITUD_INGRESO_HERINFO(ID_INGRESO, chk_compartida.Text, txt_compartida.Text);
        }
        if (chk_office.Checked)
        {
            Interfaz.Alta_SOLICITUD_INGRESO_HERINFO(ID_INGRESO, chk_office.Text, txt_office.Text);
        }
        if (chk_impresora.Checked)
        {
            Interfaz.Alta_SOLICITUD_INGRESO_HERINFO(ID_INGRESO, chk_impresora.Text, txt_impresora.Text);
        }
        if (chk_scanner.Checked)
        {
            Interfaz.Alta_SOLICITUD_INGRESO_HERINFO(ID_INGRESO, chk_scanner.Text, txt_scanner.Text);
        }
        if (chk_smartphone.Checked)
        {
            Interfaz.Alta_SOLICITUD_INGRESO_HERINFO(ID_INGRESO, chk_smartphone.Text, txt_smartphone.Text);
        }
        if (chk_app.Checked)
        {
            Interfaz.Alta_SOLICITUD_INGRESO_HERINFO(ID_INGRESO, chk_app.Text, txt_app.Text);
        }
    }

    private void LimpiarPantalla()
    {
        try
        {
            txt_fecha_alta.Text = DateTime.Now.ToShortDateString();
            txt_fecha_baja.Text = "";
            txt_cuil.Text = "";
            txt_apellido.Text = "";
            txt_domicilio_real.Text = "";
            txt_nombres.Text = "";
           
            cmb_centro_costo.SelectedIndex = 0;
            cmb_cliente.SelectedIndex = 0;
            cmb_convenio.SelectedIndex = 0;
            cmb_localidad.SelectedIndex = 0;
            cmb_tipo_contrato.Items.Clear();
            cmb_objetivo.Items.Clear();
            cmb_obra_social.SelectedIndex = 0;
            cmb_razon_social.SelectedIndex = 0;
            chk_interno.Checked = false;
            cmb_objetivo.Enabled = true;
            cmb_cliente.Enabled = true;
            CargarSolicitudes();
            chk_utiliza_pc.Checked = false;
            txt_obs_utiliza_pc.Text = "";
            chk_utiliza_cuenta_google.Checked = false;
            txt_obs_cuenta_google.Text = "";
            chk_utiliza_cuenta_local.Checked = false;
            txt_obs_cuenta_local.Text = "";
            chk_utiliza_intranet.Checked = false;
            txt_intranet.Text = "";
            chk_tango.Checked = false;
            txt_tango.Text = "";
            chk_softland.Checked = false;
            txt_softland.Text = "";
            chk_sisitema_rrhh.Checked = false;
            txt_sistema_rrhh.Text = "";
            chk_sistema_stock.Checked = false;
            txt_sistema_stock.Text = "";
            chk_compartida.Checked = false;
            txt_compartida.Text = "";
            chk_office.Checked = false;
            txt_office.Text = "";
            chk_impresora.Checked = false;
            txt_impresora.Text = "";
            chk_scanner.Checked = false;
            txt_scanner.Text = "";
            chk_smartphone.Checked = false;
            txt_smartphone.Text = "";
            chk_app.Checked = false;
            txt_app.Text = "";
        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;

        }
    }
}