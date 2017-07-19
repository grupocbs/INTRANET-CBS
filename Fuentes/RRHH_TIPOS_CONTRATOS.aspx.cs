using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mail;
using System.Configuration;
using DevExpress.Web;

public partial class RRHH_TIPOS_CONTRATOS : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Session.Add("usuario", "1");
           if (Session["usuario"] == null)
            {

                Response.Redirect("INTRANET_LOGIN.aspx");
            }
            if (!IsPostBack)
            {
                cmb_empresa.Items.Add(new ListEditItem("SELECCIONE", "SELECCIONE"));
                cmb_empresa.Items.Add(new ListEditItem("BARCELO", "BARCELO"));
                cmb_empresa.Items.Add(new ListEditItem("CASA DE LA COSTA", "CASA DE LA COSTA"));
                cmb_empresa.Items.Add(new ListEditItem("CBS SRL", "CBS SRL"));
                cmb_empresa.SelectedIndex = 0;
              
              
            }
           


        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }
    }


    protected void cmb_empresa_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
             
            txt_nombre.Text = "";
            txt_descripcion.Html = "";
            FailureText.Text = "";
            txt_nombre.ToolTip = "";
            cmb_contratos.Items.Clear();
            cmb_contratos.Text = "";

            if (cmb_empresa.SelectedItem.Text != "SELECCIONE")
            {
                cargar();
            }
            else
            {
                txt_nombre.ToolTip = "";
                txt_nombre.Text = "";
                txt_descripcion.Html = "";
               

                cmb_contratos.Items.Clear();
                cmb_contratos.Text = "";
            }

        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }

    }

    private void cargar()
    {
        cmb_contratos.Items.Clear();
        DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", "SELECT ID, NOMBRE FROM RRHH_TIPOS_CONTRATOS with(nolock) WHERE EMPRESA='" + cmb_empresa .SelectedItem.Value.ToString()+ "' order by NOMBRE");
        if (dt.Rows.Count > 0)
        {
            cmb_contratos.Items.Add(new ListEditItem("SELECCIONE", "SELECCIONE"));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmb_contratos.Items.Add(new ListEditItem(dt.Rows[i]["NOMBRE"].ToString(), dt.Rows[i]["ID"].ToString()));

            }
            cmb_contratos.SelectedIndex = 0;
        }
    }

    protected void cmb_contratos_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            FailureText.Text = "";
            if (cmb_contratos.SelectedItem.Text != "SELECCIONE")
            {
                string sql = "select * from RRHH_TIPOS_CONTRATOS with(nolock) where id=" + cmb_contratos.SelectedItem.Value.ToString();
                DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql);
                if (dt.Rows.Count > 0)
                {
                    txt_nombre.ToolTip = dt.Rows[0]["ID"].ToString();
                    txt_nombre.Text = dt.Rows[0]["NOMBRE"].ToString();
                    txt_descripcion.Html = dt.Rows[0]["DESCRIPCION"].ToString();
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

            if (cmb_empresa.SelectedItem.Text!="SELECCIONE" && txt_nombre.Text.Length > 0 && txt_descripcion.Html.Length > 0)
            {
                if (txt_nombre.ToolTip.Length == 0)
                {
                    Interfaz.Alta_TIPOS_CONTRATOS(txt_nombre.Text, txt_descripcion.Html,cmb_empresa.SelectedItem.Value.ToString());
                }
                else
                {
                    Interfaz.Editar_TIPOS_CONTRATOS(txt_nombre.ToolTip,txt_nombre.Text, txt_descripcion.Html);
                }
                FailureText.Text = "Guardado";
              
            }


        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }

    }

    private void limpiar()
    {
        txt_nombre.Text = "";
        txt_descripcion.Html = "";
        FailureText.Text = "";
        txt_nombre.ToolTip = "";
        cmb_contratos.Items.Clear();
        cmb_contratos.Text = "";
        cmb_empresa.SelectedIndex = 0;
    }


    protected void btn_canacelar_Click(object sender, EventArgs e)
    {
        try
        {
            limpiar();
        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }

    }
}