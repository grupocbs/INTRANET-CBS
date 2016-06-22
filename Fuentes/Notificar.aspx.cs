using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mail;
using System.Configuration;

public partial class Notificar : System.Web.UI.Page
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

                DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer","SELECT * FROM USUARIOS with(nolock) order by nombre");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    trv_destinatarios.Items.Add(new ListItem(dt.Rows[i]["Nombre"].ToString() + " (" + dt.Rows[i]["mail"].ToString() + ")", dt.Rows[i]["mail"].ToString()));
                    trv_destinatarios.Items[i].Selected = true;
                }
                
              
            }
            chk_destinatarios.CheckedChanged += new EventHandler(chk_destinatarios_CheckedChanged);


        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }
    }

    protected void chk_destinatarios_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_destinatarios.Checked)
        {
            for (int i = 0; i < trv_destinatarios.Items.Count; i++)
            {
               
                trv_destinatarios.Items[i].Selected = true;
            }
        }
        else
        {
            for (int i = 0; i < trv_destinatarios.Items.Count; i++)
            {

                trv_destinatarios.Items[i].Selected = false;
            }
        }

    }

    protected void btn_enviar_Click(object sender, EventArgs e)
    {
        try
        {

            for (int i = 0; i < trv_destinatarios.Items.Count; i++)
            {
                if (trv_destinatarios.Items[i].Selected)
                {
                    try
                    {
                        MailMessage mail = new MailMessage();

                        string EMAIL_DE = ConfigurationSettings.AppSettings.Get("EMAIL_DE");
                        mail.From = new MailAddress(EMAIL_DE, "Intranet GRUPO CBS");

                       // mail.To.Add("gabriel.alonso@grupocbs.com.ar");
                        mail.To.Add(trv_destinatarios.Items[i].Value.ToString());
                        mail.Subject = txt_titulo.Text;
                        mail.IsBodyHtml = true;
                        mail.Body = "Estimada/o " + trv_destinatarios.Items[i].Text + ":<BR>Esta es una notificación enviada a traves de Intranet GRUPO CBS. Al final del correo podrá visualizar sus datos de acceso. <BR>";
                        mail.Body += ASPxHtmlEditor1.Html;
                        mail.Body += "<BR><BR>Acceso a Intranet GRUPO CBS:<BR>";
                        mail.Body += "Direccion: http://192.168.1.141:8080<BR>";

                        DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer","SELECT * FROM USUARIOS with(nolock) WHERE mail='"+trv_destinatarios.Items[i].Value.ToString()+"' ");
                        mail.Body += "Usuario:" + dt.Rows[0]["USUARIO"].ToString() + "<BR>";
                        mail.Body += "Contraseña:" + dt.Rows[0]["CONTRASEÑA"].ToString() + "<BR>";

                        string EMAIL_SMTP = ConfigurationSettings.AppSettings.Get("EMAIL_SMTP");                     
                        SmtpClient smtp = new SmtpClient(EMAIL_SMTP, 587);
                        smtp.Timeout = 10000;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new System.Net.NetworkCredential(ConfigurationSettings.AppSettings.Get("USUARIO_SMTP"), ConfigurationSettings.AppSettings.Get("PASS_SMTP"));
                        Interfaz.AgregarNOTIFICACION(dt.Rows[0]["IDUSUARIO"].ToString(), txt_titulo.Text, mail.Body);
                        smtp.Send(mail);
                        
                        FailureText.Text += "<BR>Enviado a " + trv_destinatarios.Items[i].Value.ToString();
                    }
                    catch (Exception ex)
                    {
                        FailureText.Text = "<BR>" + ex.Message + "-" + trv_destinatarios.Items[i].Value.ToString();
                    }

                }

            }


        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }

    }


    protected void btn_canacelar_Click(object sender, EventArgs e)
    {
        try
        {
            txt_titulo.Text = "";
            chk_destinatarios.Checked = true;
            for (int i = 0; i < trv_destinatarios.Items.Count; i++)
            {
                trv_destinatarios.Items[i].Selected = true;
            }

            ASPxHtmlEditor1.Html = "";
            FailureText.Text = "";
        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }

    }
}