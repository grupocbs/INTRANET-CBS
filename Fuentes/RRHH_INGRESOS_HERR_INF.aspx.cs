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

public partial class RRHH_INGRESOS_HERR_INF : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
          //  Session.Add("usuario", "1");
           if (Session["usuario"] == null)
            {

                Response.Redirect("INTRANET_LOGIN.aspx");
            }
            if (!IsPostBack)
            {

                DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", "SELECT ID,APELLIDO,NOMBRES FROM RRHH_SOLICITUD_INGRESO with(nolock) WHERE ID IN (SELECT ID_INGRESO FROM RRHH_SOLICITUD_INGRESO_HERINF where COMPLETADO=0) order by ID DESC");
                if (dt.Rows.Count > 0)
                {
                    cmb_ingresos.Items.Add(new ListEditItem("SELECCIONE", "SELECCIONE"));
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        cmb_ingresos.Items.Add(new ListEditItem(dt.Rows[i]["ID"].ToString()+"-"+dt.Rows[i]["APELLIDO"].ToString() + " " + dt.Rows[i]["NOMBRES"].ToString(), dt.Rows[i]["ID"].ToString()));

                    }
                    cmb_ingresos.SelectedIndex = 0;
                }
              
            }
           


        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }
    }
    protected void cmb_ingresos_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            Recargar();

        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }

    }

    private void Recargar()
    {
        FailureText.Text = "";
        if (cmb_ingresos.SelectedItem.Text != "SELECCIONE")
        {
            string sql = "select * from RRHH_SOLICITUD_INGRESO_HERINF with(nolock) where id_INGRESO=" + cmb_ingresos.SelectedItem.Value.ToString();
            DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql);
            if (dt.Rows.Count > 0)
            {
                ASPxGridView1.DataSource = dt;
                ASPxGridView1.DataBind();
            }
            else
            {
                ASPxGridView1.DataSource = null;
                ASPxGridView1.DataBind();
            }


        }
        else
        {
            ASPxGridView1.DataSource = null;
            ASPxGridView1.DataBind();
        }
    }
    protected void ASPxGridView1_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        try
        {
            if (ASPxGridView1.GetDataRow(e.VisibleIndex) != null)
            {
                string sql = "select COMPLETADO from RRHH_SOLICITUD_INGRESO_HERINF with(nolock) WHERE ID=" + ASPxGridView1.GetDataRow(e.VisibleIndex)["ID"].ToString();
                DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql);


                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["COMPLETADO"].ToString() == "False")
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
                 
            }
        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }
    }
    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        try
        {

            bool COMPLETADO = ((e.NewValues["COMPLETADO"] != null) ? Convert.ToBoolean(e.NewValues["COMPLETADO"].ToString()) : false);



            Interfaz.Editar_ESTADO_SOLICITUD_INGRESO_HERINFO(e.Keys[0].ToString(), COMPLETADO);



            Recargar();
            e.Cancel = true;

            ASPxGridView1.CancelEdit();

        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }
    }
}