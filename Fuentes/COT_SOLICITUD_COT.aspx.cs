using System;
using System.Drawing;
using System.IO;
using DevExpress.Web;
using DevExpress.Web.Internal;
using System.Data;
using System.Collections.Generic;
using System.Net.Mail;
using System.Configuration;

public partial class COT_SOLICITUD_COT : System.Web.UI.Page
{
  
    protected void Page_Load(object sender, EventArgs e)
    {
      
        Session.Add("usuario", "1");
        if (Session["usuario"] == null)
        {
            Response.Redirect("INTRANET_LOGIN.aspx");
        }
        if (!IsPostBack)
        {

            cmb_razon_social.Items.Add(new ListEditItem("SELECCIONE", "SELECCIONE"));
            cmb_razon_social.Items.Add(new ListEditItem("BARCELO", "CBS02"));
            cmb_razon_social.Items.Add(new ListEditItem("CBS SRL", "CBS01"));
            cmb_razon_social.SelectedIndex = 0;


            string sql = "select VTMCLH_NROCTA,VTMCLH_NOMBRE from VTMCLH with (nolock)  order by VTMCLH_NOMBRE";
            DataTable dt = Interfaz.EjecutarConsultaBD("CBS", sql);
            cmb_cliente.Items.Add(new ListEditItem("SELECCIONE", "SELECCIONE"));
            foreach (DataRow dr in dt.Rows)
            {
                cmb_cliente.Items.Add(new ListEditItem(dr["VTMCLH_NOMBRE"].ToString(), dr["VTMCLH_NROCTA"].ToString()));
            }
            cmb_cliente.SelectedIndex = 0;


            UltimoNro();


            cmb_area.Items.Add(new ListEditItem("SELECCIONE", "SELECCIONE"));
            foreach (DataRow dr in Interfaz.EjecutarConsultaBD("LocalSqlServer", "SELECT ID,DESCRIPCION FROM SECTORES WITH(NOLOCK) WHERE SERVICIO=1").Rows)
            {
                cmb_area.Items.Add(new ListEditItem(dr["DESCRIPCION"].ToString(), dr["ID"].ToString()));

            }
            cmb_area.SelectedIndex = 0;
          

           


             
            
        }

         
       
    }

    


    
    protected void btn_enviar_Click(object sender, EventArgs e)
    {
        try
        {
            if (cmb_razon_social.SelectedItem.Text != "SELECCIONE" && cmb_area.SelectedItem.Text != "SELECCIONE")
            {
                
                   

                    string sql = "select mail, usuario, nombre from USUARIOS with (nolock) where idusuario=" + Session["usuario"].ToString();
                    DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql);

                    if (dt.Rows.Count > 0)
                    {
                        string EMPRESA = ((cmb_razon_social.SelectedIndex > 0) ? cmb_razon_social.SelectedItem.Value.ToString() : "");
                        string IDSECTOR = ((cmb_area.SelectedIndex > 0) ? cmb_area.SelectedItem.Value.ToString() : "");
                        string IDCLIENTE = ((cmb_cliente.SelectedIndex > 0) ? cmb_cliente.SelectedItem.Value.ToString() : "99999");


                        string CONTACTO_NOMBRE = txt_nombres.Text;
                        string CONTACTO_DOMICILO = txt_domicilio_real.Text;
                        string CONTACTO_TELEFONO = txt_telefono.Text;
                        string CONTACTO_MAIL = txt_mail.Text;
                        string OBSERVACIONES = txt_observaciones.Text;

                        Interfaz.Alta_SOLICITUD_COTIZACION(EMPRESA, IDSECTOR, IDCLIENTE, CONTACTO_NOMBRE, CONTACTO_DOMICILO, CONTACTO_TELEFONO, CONTACTO_MAIL, OBSERVACIONES, Session["usuario"].ToString());


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
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
          
        }
    }

  

    private void LimpiarPantalla()
    {
        try
        {
            UltimoNro();

            txt_telefono.Text = "";
            txt_domicilio_real.Text = "";
            txt_nombres.Text = "";
            txt_observaciones.Text = "";
            txt_mail.Text = "";
            cmb_cliente.SelectedIndex = 0;
            cmb_razon_social.SelectedIndex = 0;
            cmb_area.SelectedIndex = 0;
          
        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;

        }
    }

    private void UltimoNro()
    {
        string sql = "select ID=isnull(max(id),0) +1  from COT_SOLICITUD_COTIZACION WITH(NOLOCK)";
        DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql);
        txt_nro_solicitud.Text = dt.Rows[0]["ID"].ToString();
    }
}