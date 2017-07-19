using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
using System.Net.Mail;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using System.IO;

public partial class COT_SOLICITUD_REPORTE : System.Web.UI.Page
{



    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            Session.Add("usuario", "1");
            if (Session["usuario"] == null)
            {

                Response.Redirect("INTRANET_LOGIN.aspx");
            }
            if (!IsPostBack)
            {

                Recargar();
            }

          

        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }

    }


    private void Recargar()
    {
        try
        {
            ASPxGridView1.DataSource = Interfaz.EjecutarConsultaBD("LocalSqlServer", "SELECT * FROM COT_SOLICITUD_COTIZACION ORDER BY ID");
            ASPxGridView1.DataBind();
          
        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }
    }

  

    protected void ASPxGridView1_Init(object sender, EventArgs e)
    {
        try
        {
            GridViewDataComboBoxColumn combo1 = ASPxGridView1.Columns["IDCLIENTE"] as GridViewDataComboBoxColumn;
            foreach (DataRow dr in Interfaz.EjecutarConsultaBD("CBS", "select VTMCLH_NROCTA,VTMCLH_NOMBRE from VTMCLH with (nolock)  order by VTMCLH_NOMBRE ").Rows)
            {
                combo1.PropertiesComboBox.Items.Add(new ListEditItem(dr["VTMCLH_NOMBRE"].ToString(), dr["VTMCLH_NROCTA"].ToString()));

            }

            GridViewDataComboBoxColumn combo2 = ASPxGridView1.Columns["IDSECTOR"] as GridViewDataComboBoxColumn;
            foreach (DataRow dr in Interfaz.EjecutarConsultaBD("LocalSqlServer", "SELECT ID,DESCRIPCION FROM SECTORES WITH(NOLOCK) WHERE SERVICIO=1 ").Rows)
            {
                combo2.PropertiesComboBox.Items.Add(new ListEditItem(dr["DESCRIPCION"].ToString(), dr["ID"].ToString()));

            }


            GridViewDataComboBoxColumn combo = ASPxGridView1.Columns["EMPRESA"] as GridViewDataComboBoxColumn;
            combo.PropertiesComboBox.Items.Add("BARCELO", "BARCELO");
            combo.PropertiesComboBox.Items.Add("CBS SRL", "CBS SRL");
            

 

 

            
          
        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }
       
    }

   


    protected void ASPxGridView1_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        if (e.ButtonID == "btn_imprimir")
        {
            Imprimir(ASPxGridView1.GetRowValues(e.VisibleIndex, "ID").ToString());

        }
         

    }

    protected void Imprimir(string ID)
    {

        string sql = "select mail, usuario, nombre from USUARIOS with (nolock) where idusuario=" + Session["usuario"].ToString();
        DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql);

        if (dt.Rows.Count > 0)
        {
            

            
            Rep_COT_Form_Solicitud reporte = new Rep_COT_Form_Solicitud();
            DataTable dt2 = new DataTable();
            string sql2 = "select CLIENTE=VTMCLH_NOMBRE,IDSECTOR=s.ID,TIPO_SERVICIO=s.DESCRIPCION, NRO=c.id,CONTACTO=CONTACTO_NOMBRE,MAIL=CONTACTO_MAIL,TELEFONO=CONTACTO_TELEFONO,OBSERVACIONES,FECHA_SOLICITUD=CONVERT(VARCHAR(10),fecha,103) ";
            sql2 += " from COT_SOLICITUD_COTIZACION c WITH(NOLOCK)";
            sql2 += " left join CBS.dbo.VTMCLH v WITH(NOLOCK)";
            sql2 += " on c.idcliente=v.VTMCLH_NROCTA";
            sql2 += " left join SECTORES s WITH(NOLOCK)";
            sql2 += " on c.IDSECTOR=s.ID";
            sql2 += " WHERE c.ID=" + ID;






            dt2 = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql2);
            dt2.TableName = "DataTable1";
            DataSet set = new DataSet();
            set.Tables.Add(dt2);
            
            reporte.SetDataSource(set);
            CrystalReportViewer CrystalReportViewer1 = new CrystalReportViewer();
            CrystalReportViewer1.ReportSource = reporte;
            MemoryStream stream = new MemoryStream();
            stream = (MemoryStream)reporte.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);




            MailMessage mail = new MailMessage();
            string EMAIL_DE = dt.Rows[0]["mail"].ToString();
            mail.From = new MailAddress(EMAIL_DE);
            mail.Attachments.Add(new Attachment(stream, "Solicitud" + dt2.Rows[0]["NRO"].ToString(), "application/pdf"));


            
            sql = "select archivo from  COT_FORMULARIOS_SECTOR with (nolock) where IDSECTOR=" + dt2.Rows[0]["IDSECTOR"].ToString();
            DataTable dt3 = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql);
            if (dt3.Rows.Count > 0)
            {
                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    if (File.Exists(MapPath(dt3.Rows[i]["archivo"].ToString())))
                    {
                        mail.Attachments.Add(new Attachment(MapPath(dt3.Rows[i]["archivo"].ToString()), "application/pdf"));
                    }
                }
            }


            sql = "select mail from SECTORES with (nolock) where ID=" + dt2.Rows[0]["IDSECTOR"].ToString();
            DataTable dt1 = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql);
            if (dt1.Rows.Count > 0)
            {
                //mail.To.Add("dt1.Rows[0]["mail"].ToString()");

            }
            mail.To.Add("gabriel.alonso@grupo-cbs.com");


            mail.Subject = "Solicitud de Cotizacion (DE PRUEBA)";
            mail.IsBodyHtml = true;
            mail.Body = "Estimada/o:<BR>Se informa una nueva solicitud de Cotizacion, ingresada por:<b> " + dt.Rows[0]["nombre"].ToString() + "</b><BR><BR>";
            //mail.Body += "<BR>Para enviar Documentacion sobre esta solicitud, ingrese a Intranet Grupo CBS, Menu RRHH->Ingresos->Envio de Documentacion a Sectores: <a href='http://192.168.1.141/RRHH_ALTA_DOC.aspx' target='_blank'>Envio de Documentacion a Sectores</a><BR>";


            string EMAIL_SMTP = System.Configuration.ConfigurationSettings.AppSettings.Get("EMAIL_SMTP");
            SmtpClient smtp = new SmtpClient(EMAIL_SMTP, 587);
            smtp.Timeout = 10000;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential(System.Configuration.ConfigurationSettings.AppSettings.Get("USUARIO_SMTP"), System.Configuration.ConfigurationSettings.AppSettings.Get("PASS_SMTP"));
            if (mail.To.Count > 0)
            {
                smtp.Send(mail);
            }
        }





 


 

    }
    



}