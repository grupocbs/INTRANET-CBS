using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;


public partial class FC_ENV_COMP: System.Web.UI.Page
{



    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
           // Session.Add("usuario", "1");

            if (!IsPostBack)
            {

                ASPxCalendarDesde.Text = DateTime.Now.AddMonths(-1).ToShortDateString();
                ASPxCalendarHasta.Text = DateTime.Now.ToShortDateString();


              

               
                cmb_empresa.Items.Add(new ListEditItem("SELECCIONE", "SELECCIONE"));
                cmb_empresa.Items.Add(new ListEditItem("CBS SRL", "CBS01"));
                cmb_empresa.Items.Add(new ListEditItem("BARCELO", "CBS02"));
                cmb_empresa.SelectedIndex = 0;
               
            }

            if (Session["usuario"] == null)
            {

                Response.Redirect("INTRANET_LOGIN.aspx");
            }
        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }


    }

    protected void cmb_empresa_SelectedIndexChanged(object sender, EventArgs e)
    {
        cmb_cliente.Items.Clear();
        ASPxGridView1.DataSource = null;
        ASPxGridView1.DataBind();
        if (cmb_empresa.SelectedItem.Text != "SELECCIONE")
        {
            DataTable dt = Interfaz.EjecutarConsultaBD("CBS", "SELECT VTMCLH_NROCTA,VTMCLH_NOMBRE FROM VTMCLH WITH(NOLOCK) WHERE USR_VTMCLH_CODEMP='" + cmb_empresa.SelectedItem.Value.ToString() + "' and len(VTMCLH_DIREML)>0 AND USR_VTMCLH_ENVMAI='S' order by VTMCLH_nombre");

            cmb_cliente.Items.Add(new ListEditItem("TODOS", "TODOS"));
            foreach (DataRow dr in dt.Rows)
            {
                cmb_cliente.Items.Add(new ListEditItem(dr["VTMCLH_NOMBRE"].ToString(), dr["VTMCLH_NROCTA"].ToString()));


            }
            cmb_cliente.SelectedIndex=0;
        }

        Recargar();
    }

    protected void cmb_cliente_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ASPxGridView1.DataSource = null;
            ASPxGridView1.DataBind();
            if (cmb_empresa.SelectedItem.Text != "SELECCIONE")
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
        string SQL = "select VTRMVH_CODFOR,VTRMVH_NROFOR, VTRMVH_FCHMOV,VTRMVH_NROCAE,VTRMVH_VENCAE, VTRMVH_USERID, accion=CASE WHEN USR_VTRMVH_ENVMAI='1' THEN 'ENVIADO' ELSE 'NO ENVIADO' END,VTMCLH_NROCTA,CLIENTE=VE.VTMCLH_NOMBRE from VTRMVH V WITH(NOLOCK) LEFT JOIN VTMCLH VE WITH(NOLOCK) ON V.VTRMVH_NROCTA=VE.VTMCLH_NROCTA where len(VE.VTMCLH_DIREML)>0 AND VE.USR_VTMCLH_ENVMAI='S' AND VTRMVH_VENCAE IS NOT NULL AND VTRMVH_CODCOM IN ('FC') and VTRMVH_MODFOR='VT' AND VTRMVH_CODEMP='" + cmb_empresa.SelectedItem.Value.ToString() + "'";
        if (cmb_cliente.SelectedItem.Value.ToString() != "TODOS")
        {
            SQL += " AND VTRMVH_NROCTA='" + cmb_cliente.SelectedItem.Value.ToString() + "'"; 
           
        }
        SQL += "  ORDER BY VTRMVH_FCHMOV DESC,VTRMVH_NROFOR DESC";
        ASPxGridView1.DataSource = Interfaz.EjecutarConsultaBD("CBS", SQL);
        ASPxGridView1.DataBind();
    }




    protected void ASPxGridView1_Init(object sender, EventArgs e)
    {
        try
        {
            GridViewDataComboBoxColumn combo = ASPxGridView1.Columns["accion"] as GridViewDataComboBoxColumn;
            combo.PropertiesComboBox.Items.Add("NO ENVIADO", "NO ENVIADO");
            combo.PropertiesComboBox.Items.Add("ENVIADO", "ENVIADO");
 


 



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
            Interfaz.Editar_envio_comp(cmb_empresa.SelectedItem.Value.ToString(), e.NewValues["VTMCLH_NROCTA"].ToString(), e.NewValues["VTRMVH_CODFOR"].ToString(), e.Keys[0].ToString(), e.NewValues["VTRMVH_NROCAE"].ToString(), e.NewValues["accion"].ToString());
            Recargar();
            e.Cancel = true;
        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }
    }


  
   

   
    



}