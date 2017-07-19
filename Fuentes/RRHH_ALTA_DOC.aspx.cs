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
using AjaxControlToolkit;

public partial class RRHH_ALTA_DOC : System.Web.UI.Page
{
    private static DataTable dt1;
    const string UploadDirectory = "~/Archivos/RRHH/ALTAS";
    protected void Page_Load(object sender, EventArgs e)
    {
      // Session.Add("usuario", "12");
        if (Session["usuario"] == null)
        {
            Response.Redirect("INTRANET_LOGIN.aspx");
        }
        if (!IsPostBack)
        {


            if (dt1 != null)
            {
                dt1.Rows.Clear();
            }
            



            
        }
       
        cargar();
         
       
    }
    private void cargar()
    {
     
        div.Controls.Clear();
        string sql = "select * from RRHH_SOLICITUD_INGRESO with (nolock) WHERE PENDIENTE=1 and fecha_alta is not null  order by id desc";
        ASPxGridView1.DataSource = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql);
        ASPxGridView1.DataBind();

        CargarVariables();
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
          
            if (ASPxGridView1.GetSelectedFieldValues("ID")[0].ToString().Length > 0)
            {
                div.Controls.Clear();
                if (dt1 != null)
                {
                    dt1.Rows.Clear();
                }
                CargarVariiablesSegunEnvios();
             
            }


        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message + " Por favor intente mas tarde.";
        }

    }
    private void CargarVariiablesSegunEnvios()
    {
        
        div.Controls.Clear();
        btn_enviar.Visible = false;

        string sql = "select ID_CLIENTE from RRHH_DOCUMENTACION_CCT with(nolock) WHERE id_cct='" + ASPxGridView1.GetSelectedFieldValues("COD_CONV")[0].ToString() + "' AND ID_CLIENTE='" + ASPxGridView1.GetSelectedFieldValues("COD_CLIENTE")[0].ToString() + "' GROUP BY ID_CLIENTE";
        DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql);

        string sql2 = "select id_cct from RRHH_DOCUMENTACION_CCT with(nolock) WHERE id_cct='" + ASPxGridView1.GetSelectedFieldValues("COD_CONV")[0].ToString() + "' GROUP BY id_cct";
        DataTable dt2 = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql2);


        sql = "select ID_DOC,OBLIGATORIO,DESCRIPCION=d.DESCRIPCION,SECTOR=sec.DESCRIPCION,ID_SECTOR ";
        sql += " from RRHH_DOCUMENTACION_CCT_SECTORES s with(nolock) ";
        sql  += " inner join RRHH_DOCUMENTACION_CCT c with(nolock) on s.id_doc=c.id ";
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
       // sql += " where id_sector='" + cmb_sectores.SelectedItem.Value.ToString() + "'";
        //  sql += " and ID_DOC not in (select ID_DOC_CCT";
        sql += " where ID_DOC not in (select ID_DOC_CCT";
        sql += "                   from RRHH_DOCUMENTACION_CCT_ENVIOS with(nolock)";
        sql += " 					where id_ingreso='" + ASPxGridView1.GetSelectedFieldValues("ID")[0].ToString() + "')";
        if (ASPxGridView1.GetSelectedFieldValues("RAZON_SOCIAL").Count > 0 && ASPxGridView1.GetSelectedFieldValues("RAZON_SOCIAL")[0] != null && ASPxGridView1.GetSelectedFieldValues("RAZON_SOCIAL")[0].ToString().Length > 0)
        {
            sql += " and c.ID_EMPRESA='" + ASPxGridView1.GetSelectedFieldValues("RAZON_SOCIAL")[0].ToString() + "'";
        }
        sql += " ORDER BY SECTOR,DESCRIPCION";

       
        dt1 = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql);
        CargarVariables();
    }
    private void CargarVariables()
    {
        if (dt1!=null && dt1.Rows.Count > 0)
        {
            btn_enviar.Visible = true;
            div.Controls.Clear();
            HtmlGenericControl tabla = new HtmlGenericControl("table");
            tabla.Attributes.Add("border", "1");
            tabla.Attributes.Add("width", "100%");
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                HtmlGenericControl tr = new HtmlGenericControl("tr");
                HtmlGenericControl td0 = new HtmlGenericControl("td");
                td0.Attributes.Add("style", "padding: 2px;background-color: yellow;"); td0.Attributes.Add("valign", "top");
                HtmlGenericControl td = new HtmlGenericControl("td");
                td.Attributes.Add("style", "padding: 2px;"); td.Attributes.Add("valign", "top");
                HtmlGenericControl td1 = new HtmlGenericControl("td");
                td1.Attributes.Add("style", "padding: 2px;"); td1.Attributes.Add("valign", "top");
                HtmlGenericControl td2 = new HtmlGenericControl("td");
                td2.Attributes.Add("style", "padding: 2px;color:red;font-size:10px;"); td2.Attributes.Add("valign", "top");
                // div.InnerHtml += dt.Rows[i]["ID_DOC"].ToString() + "/" + dt.Rows[i]["OBLIGATORIO"].ToString() + "/" + dt.Rows[i]["DESCRIPCION"].ToString() + "</br>";

                ASPxLabel l0 = new ASPxLabel();
                l0.Style.Add("font-size", "10px");
                l0.ID = "ars_" + dt1.Rows[i]["ID_DOC"].ToString() +"_" + dt1.Rows[i]["ID_SECTOR"].ToString();
                l0.Text = dt1.Rows[i]["SECTOR"].ToString();
                td0.Controls.Add(l0);

                ASPxLabel l = new ASPxLabel();
                l.Style.Add("font-size", "10px");
                l.ID = "ar_" + dt1.Rows[i]["ID_SECTOR"].ToString() + "_" + dt1.Rows[i]["ID_DOC"].ToString();
                l.Text = dt1.Rows[i]["DESCRIPCION"].ToString();
                td.Controls.Add(l);

              
                FileUpload up = new FileUpload();
                up.ID = "archivo_" + dt1.Rows[i]["ID_SECTOR"].ToString() + "_" + dt1.Rows[i]["ID_DOC"].ToString();
                up.EnableViewState = true;
                up.ViewStateMode = System.Web.UI.ViewStateMode.Enabled;
                up.Attributes.Add("onchange", "return CheckExtension(this);");

                string sql2 = "SELECT OBSERVACION,FECHA FROM RRHH_DOCUMENTACION_OBS WITH(NOLOCK) WHERE ID_DOC_CCT='" + dt1.Rows[i]["ID_DOC"].ToString() + "' AND ID_INGRESO='" + ASPxGridView1.GetSelectedFieldValues("ID")[0].ToString() + "' ORDER BY FECHA DESC"; 
                DataTable dt2 = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql2);

                for (int t = 0; t < dt2.Rows.Count; t++)
                {
                    td2.InnerHtml += dt2.Rows[t]["FECHA"].ToString() + ":" + dt2.Rows[t]["OBSERVACION"].ToString() + "</BR>";
                }
                
                td1.Controls.Add(up);
                tr.Controls.Add(td0);
                tr.Controls.Add(td);
                tr.Controls.Add(td1);
                tr.Controls.Add(td2);
                tabla.Controls.Add(tr);

            }
            div.Controls.Add(tabla);
        }
    }
    protected void btn_enviar_Click(object sender, EventArgs e)
    {
        try
        {
            if (dt1.Rows.Count > 0 && ASPxGridView1.GetSelectedFieldValues("RAZON_SOCIAL")[0] != null && ASPxGridView1.GetSelectedFieldValues("CUIL")[0] != null && ASPxGridView1.GetSelectedFieldValues("APELLIDO")[0].ToString() != null && ASPxGridView1.GetSelectedFieldValues("NOMBRES")[0].ToString() != null)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    if (FindControl("ctl00$MainContent$formLayout$archivo_" + dt1.Rows[i]["ID_SECTOR"].ToString() + "_" + dt1.Rows[i]["ID_DOC"].ToString()) != null)
                    {
                        FileUpload c = (FileUpload)FindControl("ctl00$MainContent$formLayout$archivo_" + dt1.Rows[i]["ID_SECTOR"].ToString() + "_" + dt1.Rows[i]["ID_DOC"].ToString());
                        foreach (HttpPostedFile postedFile in c.PostedFiles)
                        {
                            if (postedFile.ContentLength > 0)
                            {

                                ASPxLabel l = (ASPxLabel)FindControl("ctl00$MainContent$formLayout$ar_" + dt1.Rows[i]["ID_SECTOR"].ToString() + "_" + dt1.Rows[i]["ID_DOC"].ToString());

                                string fileName = dt1.Rows[i]["SECTOR"].ToString() + '-' + l.Text + Path.GetExtension(postedFile.FileName); //Path.GetFileName(postedFile.FileName);
                                if (!Directory.Exists(Server.MapPath(UploadDirectory + "/" + ASPxGridView1.GetSelectedFieldValues("RAZON_SOCIAL")[0].ToString() + "/" + ASPxGridView1.GetSelectedFieldValues("CUIL")[0].ToString() + "-" + ASPxGridView1.GetSelectedFieldValues("APELLIDO")[0].ToString() + " " + ASPxGridView1.GetSelectedFieldValues("NOMBRES")[0].ToString())))
                                {
                                    Directory.CreateDirectory(Server.MapPath(UploadDirectory + "/" + ASPxGridView1.GetSelectedFieldValues("RAZON_SOCIAL")[0].ToString() + "/" + ASPxGridView1.GetSelectedFieldValues("CUIL")[0].ToString() + "-" + ASPxGridView1.GetSelectedFieldValues("APELLIDO")[0].ToString() + " " + ASPxGridView1.GetSelectedFieldValues("NOMBRES")[0].ToString()));
                                }
                                Interfaz.Alta_DOCUMENTACION_CCT_ENVIOS(dt1.Rows[i]["ID_DOC"].ToString(), UploadDirectory + "/" + ASPxGridView1.GetSelectedFieldValues("RAZON_SOCIAL")[0].ToString() + "/" + ASPxGridView1.GetSelectedFieldValues("CUIL")[0].ToString() + "-" + ASPxGridView1.GetSelectedFieldValues("APELLIDO")[0].ToString() + " " + ASPxGridView1.GetSelectedFieldValues("NOMBRES")[0].ToString() + "/" + fileName, "", DateTime.Now, ASPxGridView1.GetSelectedFieldValues("ID")[0].ToString());
                                postedFile.SaveAs(Server.MapPath(UploadDirectory + "/" + ASPxGridView1.GetSelectedFieldValues("RAZON_SOCIAL")[0].ToString() + "/" + ASPxGridView1.GetSelectedFieldValues("CUIL")[0].ToString() + "-" + ASPxGridView1.GetSelectedFieldValues("APELLIDO")[0].ToString() + " " + ASPxGridView1.GetSelectedFieldValues("NOMBRES")[0].ToString() + "/" + fileName));

                            }
                        }
                    }
                }

                try
                {
                    MailMessage mail = new MailMessage();

                    //email de
                    string sql = "select mail, usuario, nombre from USUARIOS with (nolock) where idusuario=" + Session["usuario"].ToString();
                    DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql);

                    //email para
                    //MAIL DEL SECTOR QUE RECIBE LA DOCUMENTACION
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        sql = "select mail from SECTORES with(nolock) WHERE id=" + dt1.Rows[i]["ID_SECTOR"].ToString();
                        DataTable dt2 = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql);
                        if (dt2.Rows.Count == 1)
                        {
                            if (!mail.To.Contains(new MailAddress(dt2.Rows[0]["mail"].ToString())))
                            {
                                mail.To.Add(dt2.Rows[0]["mail"].ToString());
                            }
                        }
                    }



                    if (dt.Rows.Count > 0 && mail.To.Count > 0)
                    {


                        string EMAIL_DE = dt.Rows[0]["mail"].ToString();
                        mail.From = new MailAddress(EMAIL_DE);

                        mail.Subject = "Envio de Documentacion";
                        mail.IsBodyHtml = true;
                        mail.Body = "Estimada/o:<BR>Se informa que se envio documentacion de la solicitud Nro " + ASPxGridView1.GetSelectedFieldValues("ID")[0].ToString() + " para la persona:<b> " + ASPxGridView1.GetSelectedFieldValues("CUIL")[0].ToString() + "-" + ASPxGridView1.GetSelectedFieldValues("APELLIDO")[0].ToString() + " " + ASPxGridView1.GetSelectedFieldValues("NOMBRES")[0].ToString() + "</b><BR><BR>";
                        mail.Body += "<BR>Para ver esta Documentacion  ingrese a Intranet Grupo CBS, Menu RRHH->Ingresos->Ver Documentacion: <a href='http://192.168.1.141/DOC_INGRESOS.aspx' target='_blank'>Ver Documentacion</a><BR>";


                        string EMAIL_SMTP = System.Configuration.ConfigurationSettings.AppSettings.Get("EMAIL_SMTP");
                        SmtpClient smtp = new SmtpClient(EMAIL_SMTP, 587);

                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new System.Net.NetworkCredential(System.Configuration.ConfigurationSettings.AppSettings.Get("USUARIO_SMTP"), System.Configuration.ConfigurationSettings.AppSettings.Get("PASS_SMTP"));
                        smtp.Send(mail);

                    }

                }
                catch (Exception ex)
                {
                    FailureText.Text = "SE ENVIO LA DOCUMENTACION, PERO HUBO UN PROBLEMA CON EL ENVIO DEL AVISO:" + ex.Message;
                    
                }

                CargarVariiablesSegunEnvios();
            }
        }
        catch (Exception ex)
        {
            FailureText.Text += ex.Message;
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