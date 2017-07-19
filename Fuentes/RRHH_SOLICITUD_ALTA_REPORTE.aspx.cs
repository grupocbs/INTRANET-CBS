using System;
using System.Drawing;
using System.IO;
using DevExpress.Web;
using DevExpress.Web.Internal;
using System.Data;
using System.Collections.Generic;
using System.Net.Mail;
using System.Configuration;

public partial class RRHH_SOLICITUD_ALTA_REPORTE : System.Web.UI.Page
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

            string sql = "select mail, usuario, nombre from USUARIOS with (nolock) where idusuario=" + Session["usuario"].ToString();
            DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql);


            sql = "select *,ESTADO=CASE WHEN PENDIENTE=1 THEN 'PENDIENTE' ELSE 'FINALIZADA' END  from RRHH_SOLICITUD_INGRESO with (nolock) WHERE USUARIO='" + dt.Rows[0]["usuario"].ToString() + "'  order by id desc";
            ASPxGridView1.DataSource = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql);
            ASPxGridView1.DataBind();

            
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

    protected void detalleGrid_Init(object sender, EventArgs e)
    {
        GridViewDataComboBoxColumn combo2 = ((ASPxGridView)sender).Columns["COD_CLIENTE"] as GridViewDataComboBoxColumn;
        string sql = " select VTMCLH_NROCTA,VTMCLH_NOMBRE from VTMCLH with (nolock)  order by VTMCLH_NOMBRE ";
        DataTable dt = Interfaz.EjecutarConsultaBD("CBS", sql);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            combo2.PropertiesComboBox.Items.Add(new ListEditItem(dt.Rows[i]["VTMCLH_NOMBRE"].ToString(), dt.Rows[i]["VTMCLH_NROCTA"].ToString()));
        }

        GridViewDataComboBoxColumn combo3 = ((ASPxGridView)sender).Columns["COD_OS"] as GridViewDataComboBoxColumn;
        sql = " select SJTOSO_CODOSO, SJTOSO_DESCRP from SJTOSO with (nolock) order by SJTOSO_DESCRP ";
        dt = Interfaz.EjecutarConsultaBD("CBS", sql);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            combo3.PropertiesComboBox.Items.Add(new ListEditItem(dt.Rows[i]["SJTOSO_DESCRP"].ToString(), dt.Rows[i]["SJTOSO_CODOSO"].ToString()));
        }

        GridViewDataComboBoxColumn combo4 = ((ASPxGridView)sender).Columns["COD_CONV"] as GridViewDataComboBoxColumn;
        sql = " select USR_SJTCON_CODIGO,USR_SJTCON_DESCRP  from USR_SJTCON with (nolock)   order by USR_SJTCON_DESCRP ";
        dt = Interfaz.EjecutarConsultaBD("CBS", sql);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            combo4.PropertiesComboBox.Items.Add(new ListEditItem(dt.Rows[i]["USR_SJTCON_DESCRP"].ToString(), dt.Rows[i]["USR_SJTCON_CODIGO"].ToString()));
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

        GridViewDataComboBoxColumn combo7 = ((ASPxGridView)sender).Columns["TIPO_CONTRATO"] as GridViewDataComboBoxColumn;
        sql = " SELECT ID, NOMBRE FROM RRHH_TIPOS_CONTRATOS ";
        dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            combo7.PropertiesComboBox.Items.Add(new ListEditItem(dt.Rows[i]["NOMBRE"].ToString(), dt.Rows[i]["ID"].ToString()));
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
  
  


  
}