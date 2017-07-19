using System;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using DevExpress.Web.Internal;
using System.Data;
using System.Collections.Generic;
using System.Net.Mail;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.IO;
public partial class DOC_INGRESOS : System.Web.UI.Page
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



            ASPxGridView2.JSProperties["cpConfirmationMessage"] = "";

            DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", "SELECT ID, DESCRIPCION FROM SECTORES with(nolock) order by DESCRIPCION");
            if (dt.Rows.Count > 0)
            {
                
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmb_sectores.Items.Add(new ListEditItem(dt.Rows[i]["DESCRIPCION"].ToString(), dt.Rows[i]["ID"].ToString()));

                }
              
            }


            dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", "SELECT ID_SECTOR FROM USUARIOS with(nolock) WHERE IDUSUARIO=" + Session["usuario"].ToString());
            if (dt.Rows.Count > 0)
            {
                cmb_sectores.SelectedIndex = cmb_sectores.Items.IndexOf(cmb_sectores.Items.FindByValue(dt.Rows[0]["ID_SECTOR"].ToString()));
            }

            
        }
       
       cargar();
         
       
    }

    private void cargar()
    {
        string sql = "select ESTADO= CASE WHEN pendiente=1 then 'PENDIENTE' else 'COMPLETADO' end,* from RRHH_SOLICITUD_INGRESO with (nolock) where id in (select e.ID_INGRESO from RRHH_DOCUMENTACION_CCT_ENVIOS e with(nolock) left join RRHH_DOCUMENTACION_CCT c with(nolock) on e.ID_DOC_CCT=c.ID left join RRHH_DOCUMENTACION_CCT_SECTORES s with(nolock) on c.ID=s.ID_DOC where ID_SECTOR="+cmb_sectores.SelectedItem.Value.ToString()+") and fecha_alta is not null  order by id desc";
        ASPxGridView1.DataSource = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql);
        ASPxGridView1.DataBind();
        CargarVariiablesSegunEnvios();
    }

    protected void ASPxGridView1_Init(object sender, EventArgs e)
    {
        GridViewDataComboBoxColumn combo2 = ((ASPxGridView)sender).Columns["COD_CLIENTE"] as GridViewDataComboBoxColumn;
        string sql = " select VTMCLH_NROCTA,VTMCLH_NOMBRE from VTMCLH with (nolock)  order by VTMCLH_NOMBRE ";
        DataTable dt = Interfaz.EjecutarConsultaBD("CBS", sql);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            combo2.PropertiesComboBox.Items.Add(new ListEditItem(dt.Rows[i]["VTMCLH_NOMBRE"].ToString(), dt.Rows[i]["VTMCLH_NROCTA"].ToString()));
        }

        GridViewDataComboBoxColumn combo4 = ((ASPxGridView)sender).Columns["COD_CONV"] as GridViewDataComboBoxColumn;
        sql = " select USR_SJTCON_CODIGO,USR_SJTCON_DESCRP  from USR_SJTCON with (nolock)   order by USR_SJTCON_DESCRP ";
        dt = Interfaz.EjecutarConsultaBD("CBS", sql);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            combo4.PropertiesComboBox.Items.Add(new ListEditItem(dt.Rows[i]["USR_SJTCON_DESCRP"].ToString(), dt.Rows[i]["USR_SJTCON_CODIGO"].ToString()));
        }

        GridViewDataComboBoxColumn combo5 = ((ASPxGridView)sender).Columns["COD_CATEGORIA"] as GridViewDataComboBoxColumn;
        sql = " select COD_CATEGORIA=SJTCAT_CODIGO, DESCRIPCION=SJTCAT_DESCRP from SJTCAT with(nolock) ORDER BY SJTCAT_DESCRP ";
        dt = Interfaz.EjecutarConsultaBD("CBS", sql);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            combo5.PropertiesComboBox.Items.Add(new ListEditItem(dt.Rows[i]["DESCRIPCION"].ToString(), dt.Rows[i]["COD_CATEGORIA"].ToString()));
        }

        GridViewDataComboBoxColumn combo7 = ((ASPxGridView)sender).Columns["TIPO_CONTRATO"] as GridViewDataComboBoxColumn;
        sql = " SELECT ID, NOMBRE FROM RRHH_TIPOS_CONTRATOS ";
        dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            combo7.PropertiesComboBox.Items.Add(new ListEditItem(dt.Rows[i]["NOMBRE"].ToString(), dt.Rows[i]["ID"].ToString()));
        }

        GridViewDataComboBoxColumn combo8 = ((ASPxGridView)sender).Columns["COD_ZONA"] as GridViewDataComboBoxColumn;
        sql = "select ID=GRTPAC_CODPOS, DES=GRTJUR_DESCRP +'-'+ GRTPAC_DESCRP from GRTPAC c with(nolock) left join GRTJUR j with(nolock) on c.GRTPAC_CODPRO=j.GRTJUR_JURISD where j.GRTJUR_JURISD in ('916','915','918') order by GRTJUR_DESCRP +'-'+ GRTPAC_DESCRP";
        dt = Interfaz.EjecutarConsultaBD("CBS", sql);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            combo8.PropertiesComboBox.Items.Add(new ListEditItem(dt.Rows[i]["DES"].ToString(), dt.Rows[i]["ID"].ToString()));
        }

    }
    protected void detalleGrid_DataSelect(object sender, EventArgs e)
    {
        try
        {
            (sender as ASPxGridView).DataSource = Interfaz.EjecutarConsultaBD("LocalSqlServer", "select * from RRHH_SOLICITUD_INGRESO with (nolock) WHERE ID='" + (sender as ASPxGridView).GetMasterRowKeyValue() + "'");
            
        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }
    }

    protected void herinfGrid_DataSelect(object sender, EventArgs e)
    {
        try
        {
            (sender as ASPxGridView).DataSource = Interfaz.EjecutarConsultaBD("LocalSqlServer", "select * from RRHH_SOLICITUD_INGRESO_HERINF with (nolock) WHERE ID_INGRESO=" + (sender as ASPxGridView).GetMasterRowKeyValue());

        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }
    }

    protected void detalleGrid_Init(object sender, EventArgs e)
    {
       

        GridViewDataComboBoxColumn combo3 = ((ASPxGridView)sender).Columns["COD_OS"] as GridViewDataComboBoxColumn;
        string sql = " select SJTOSO_CODOSO, SJTOSO_DESCRP from SJTOSO with (nolock) order by SJTOSO_DESCRP ";
        DataTable dt = Interfaz.EjecutarConsultaBD("CBS", sql);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            combo3.PropertiesComboBox.Items.Add(new ListEditItem(dt.Rows[i]["SJTOSO_DESCRP"].ToString(), dt.Rows[i]["SJTOSO_CODOSO"].ToString()));
        }

      



        GridViewDataComboBoxColumn combo5 = ((ASPxGridView)sender).Columns["COD_CENTRO_COSTO"] as GridViewDataComboBoxColumn;
        sql = " select USR_CTRCST_CODIGO,USR_CTRCST_DESCRP from USR_CTRCST with (nolock) order by USR_CTRCST_DESCRP ";
        dt = Interfaz.EjecutarConsultaBD("CBS", sql);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            combo5.PropertiesComboBox.Items.Add(new ListEditItem(dt.Rows[i]["USR_CTRCST_DESCRP"].ToString(), dt.Rows[i]["USR_CTRCST_CODIGO"].ToString()));
        }

        GridViewDataComboBoxColumn combo6 = ((ASPxGridView)sender).Columns["COD_OBJETIVO"] as GridViewDataComboBoxColumn;
        sql = " select USR_CLIOBJ_CODOBJ,USR_CLIOBJ_OBJDSC from USR_CLIOBJ with(nolock) order by USR_CLIOBJ_OBJDSC ";
        dt = Interfaz.EjecutarConsultaBD("CBS", sql);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            combo6.PropertiesComboBox.Items.Add(new ListEditItem(dt.Rows[i]["USR_CLIOBJ_OBJDSC"].ToString(), dt.Rows[i]["USR_CLIOBJ_CODOBJ"].ToString()));
        }


        


    }
    protected void ASPxGridView1_SelectionChanged(object sender, EventArgs e)
    {
        try
        {
           CargarVariiablesSegunEnvios();

        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message + " Por favor intente mas tarde.";
        }




    }
    protected void detalleGrid1_DataSelect(object sender, EventArgs e)
    {
        try
        {

            (sender as ASPxGridView).DataSource = Interfaz.EjecutarConsultaBD("LocalSqlServer", "SELECT ID,FECHA,OBSERVACION FROM RRHH_DOCUMENTACION_OBS WITH(NOLOCK) WHERE ID_DOC_CCT='" + (sender as ASPxGridView).GetMasterRowKeyValue() + "' AND ID_INGRESO='" + ASPxGridView1.GetSelectedFieldValues("ID")[0].ToString() + "' ORDER BY FECHA DESC");

        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }
    }

    private void CargarVariiablesSegunEnvios()
    {

        if (cmb_sectores.SelectedIndex > -1 && ASPxGridView1.GetSelectedFieldValues("ID").Count>0 && ASPxGridView1.GetSelectedFieldValues("ID")[0] != null)
        {

            string sql = "SELECT ID=e.ID,ID_DOC_CCT,DESCRIPCION=d.DESCRIPCION,ARCHIVO, FECHA";
            sql += " FROM RRHH_DOCUMENTACION_CCT_ENVIOS e with(nolock)";
            sql += " inner join RRHH_DOCUMENTACION_CCT c with(nolock)";
            sql += " on e.ID_DOC_CCT=c.ID";
            sql += " inner join RRHH_DOCUMENTACION d with(nolock)";
            sql += " on d.ID=c.ID_DOCUMENTO";
            sql += " inner join RRHH_DOCUMENTACION_CCT_SECTORES s with(nolock)";
            sql += " on s.ID_DOC=c.ID";
            sql += " where id_ingreso='" + ASPxGridView1.GetSelectedFieldValues("ID")[0].ToString() + "'";
            sql += " and  id_sector='" + cmb_sectores.SelectedItem.Value.ToString() + "'";

            DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql);
            ASPxGridView2.DataSource = dt;
            ASPxGridView2.DataBind();

            //btn_cerrar.Visible = true;
            //btn_enviar_obs.Visible = true;
          
            ASPxGridView2.Visible = true;
            txt_observaciones.Visible = true;
            FailureText.Text = "";
            txt_observaciones.Text = "";
        }
            
    }

    
    protected void ASPxGridView2_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        try
        {
            if (txt_observaciones.Text.Length > 0)
            {
                ObservacionyMail(e.Values["ID_DOC_CCT"].ToString());
                if (File.Exists(Server.MapPath(e.Values["ARCHIVO"].ToString())))
                {

                    File.Delete(Server.MapPath(e.Values["ARCHIVO"].ToString()));
                }

                Interfaz.Eliminar_DOCUMENTACION_CCT_ENVIOS(e.Values["ID"].ToString());
                ((ASPxGridView)sender).JSProperties["cpConfirmationMessage"] = "";
            }
            else
            {
                
                ((ASPxGridView)sender).JSProperties["cpConfirmationMessage"] = "Debe Ingresar una observacion antes de Quitar el documento";
               
            }
            CargarVariiablesSegunEnvios();
            e.Cancel = true;
            ((ASPxGridView)sender).CancelEdit();
        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }
    }
  



    //protected void btn_enviar_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (ASPxGridView2.GetSelectedFieldValues("ID")[0] != null)
    //        {
    //            if (txt_observaciones.Text.Length > 0)
    //            {

    //                ObservacionyMail();



    //                cargar();
    //            }
    //            else
    //            {
    //                FailureText.Text = "Ingrese Observacion del cambio de estado.";
    //                txt_observaciones.Focus();
    //            }
    //        }
           

    //    }
    //    catch (Exception ex)
    //    {
    //        FailureText.Text += ex.Message;
    //    }

    //}

    private void ObservacionyMail(string ID_DOC_CCT)
    {
        Interfaz.Alta_obs_DOC_ENVIO(ID_DOC_CCT, ASPxGridView1.GetSelectedFieldValues("ID")[0].ToString(), txt_observaciones.Text);

        //email de
        string sql = "select mail, usuario, nombre from USUARIOS with (nolock) where idusuario=" + Session["usuario"].ToString();
        DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql);


        if (dt.Rows.Count > 0)
        {
            MailMessage mail = new MailMessage();

            string EMAIL_DE = dt.Rows[0]["mail"].ToString();
            mail.From = new MailAddress(EMAIL_DE);
            //mail.To.Add("gabriel.alonso@grupo-cbs.com");
            mail.To.Add("altas@grupo-cbs.com");
            mail.To.Add("pilar.masman@grupo-cbs.com");



            mail.Subject = "Observacion de Documentacion";
            mail.IsBodyHtml = true;
            mail.Body = "Estimada/o:<BR>Se informa observacion de documentacion de la solicitud Nro <b>" + ASPxGridView1.GetSelectedFieldValues("ID")[0].ToString() + "</b>.<BR><BR>";
            mail.Body += "Observaciones:<BR>" + txt_observaciones.Text;
            mail.Body += "<BR>Para ver esta Documentacion  ingrese a Intranet Grupo CBS, Menu RRHH->Ingresos->Envio Documentacion: <a href='http://192.168.1.141/RRHH_ALTA_DOC.aspx' target='_blank'>Envio Documentacion</a><BR>";

            txt_observaciones.Text = "";
            string EMAIL_SMTP = System.Configuration.ConfigurationSettings.AppSettings.Get("EMAIL_SMTP");
            SmtpClient smtp = new SmtpClient(EMAIL_SMTP, 587);
            //smtp.Timeout = 10000;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential(System.Configuration.ConfigurationSettings.AppSettings.Get("USUARIO_SMTP"), System.Configuration.ConfigurationSettings.AppSettings.Get("PASS_SMTP"));
            smtp.Send(mail);


        }


    }

    protected void ASPxGridView1_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        try
        {

            string sql = "select ID_CLIENTE from RRHH_DOCUMENTACION_CCT with(nolock) WHERE id_cct='" + ASPxGridView1.GetDataRow(e.VisibleIndex)["COD_CONV"].ToString() + "' AND ID_CLIENTE='" + ASPxGridView1.GetDataRow(e.VisibleIndex)["COD_CLIENTE"].ToString() + "' GROUP BY ID_CLIENTE";
            DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql);

            string sql2 = "select id_cct from RRHH_DOCUMENTACION_CCT with(nolock) WHERE id_cct='" + ASPxGridView1.GetDataRow(e.VisibleIndex)["COD_CONV"].ToString() + "' GROUP BY id_cct";
            DataTable dt2 = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql2);



            //sql = "select ID_DOC";
            //sql += " from (select ID_DOC";
            //sql += " from RRHH_DOCUMENTACION_CCT_SECTORES s with(nolock) ";
            //sql += " inner join RRHH_DOCUMENTACION_CCT c with(nolock) on s.id_doc=c.id";
            //sql += " inner join RRHH_DOCUMENTACION  d with(nolock) on d.id=c.ID_DOCUMENTO ";
            //sql += " where OBLIGATORIO=1 AND c.ID_EMPRESA='" + ASPxGridView1.GetDataRow(e.VisibleIndex)["RAZON_SOCIAL"].ToString() + "'";
            //if (dt2.Rows.Count > 0)
            //{
            //    sql += " and id_cct='" + dt2.Rows[0]["id_cct"].ToString() + "'";
            //}
            //if (dt.Rows.Count > 0)
            //{
            //    sql += " and id_cliente='" + dt.Rows[0]["ID_CLIENTE"].ToString() + "'";
            //}
            //sql += " union";
            //sql += " select ID_DOC";
            //sql += " from RRHH_DOCUMENTACION_CCT_SECTORES s with(nolock) ";
            //sql += " inner join RRHH_DOCUMENTACION_CCT c with(nolock) on s.id_doc=c.id";
            //sql += " inner join RRHH_DOCUMENTACION  d with(nolock) on d.id=c.ID_DOCUMENTO ";
            //sql += " where OBLIGATORIO=1 and c.ID_EMPRESA='" + ASPxGridView1.GetDataRow(e.VisibleIndex)["RAZON_SOCIAL"].ToString() + "'";
            //sql += " and (id_cct is null)";
            //sql += " and (id_cliente  is null)) as T";
            //sql += " where ID_DOC not in (select ID_DOC_CCT";
            //sql += " from RRHH_DOCUMENTACION_CCT_ENVIOS with(nolock)";
            //sql += " where id_ingreso='" + ASPxGridView1.GetDataRow(e.VisibleIndex)["ID"].ToString() + "')";
            sql = "select ID_DOC ";
            sql += " from RRHH_DOCUMENTACION_CCT_SECTORES s with(nolock) ";
            sql += " inner join RRHH_DOCUMENTACION_CCT c with(nolock) on s.id_doc=c.id ";
            if (dt2.Rows.Count > 0)
            {
                sql += " and id_cct='" + dt2.Rows[0]["id_cct"].ToString() + "'";
            }
            else
            {
                sql += " and id_cct IS NULL";
            }

            if (dt.Rows.Count > 0)
            {
                sql += " and id_cliente='" + dt.Rows[0]["ID_CLIENTE"].ToString() + "'";
            }
  else
        {
            sql += " and id_cliente IS NULL";
        }
            sql += " inner join RRHH_DOCUMENTACION  d with(nolock) on d.id=c.ID_DOCUMENTO ";
            sql += " inner join SECTORES sec with(nolock) on sec.id=s.ID_SECTOR ";
            sql += " where ID_DOC not in (select ID_DOC_CCT";
            sql += "                   from RRHH_DOCUMENTACION_CCT_ENVIOS with(nolock)";
            sql += " 					where id_ingreso='" + ASPxGridView1.GetDataRow(e.VisibleIndex)["ID"].ToString() + "')";
            sql += " and c.ID_EMPRESA='" + ASPxGridView1.GetDataRow(e.VisibleIndex)["RAZON_SOCIAL"].ToString() + "'";
            sql += " and  id_sector='" + cmb_sectores.SelectedItem.Value.ToString() + "'";

            dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql);
            if (dt.Rows.Count > 0)
            {
                e.Row.Cells[0].Style.Add(HtmlTextWriterStyle.BackgroundColor, "Yellow");
                e.Row.Cells[0].Style.Add(HtmlTextWriterStyle.Color, "Black");
            }
            else
            {
                e.Row.Cells[0].Style.Add(HtmlTextWriterStyle.BackgroundColor, "Green");
                e.Row.Cells[0].Style.Add(HtmlTextWriterStyle.Color, "White");
            }

        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }
    }

  
}