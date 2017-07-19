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

public partial class COT_SOLICITUD_MODIFICACIONES : System.Web.UI.Page
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

    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        try
        {
            Interfaz.Eliminar_SOLICITUD_COTIZACION(e.Keys[0].ToString());
            Recargar();
            e.Cancel = true;
            ASPxGridView1.CancelEdit();
	
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

   

    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        try
        {
            string EMPRESA = ((e.NewValues["EMPRESA"] != null) ? e.NewValues["EMPRESA"].ToString() : "");
            string IDSECTOR = ((e.NewValues["IDSECTOR"] != null) ? e.NewValues["IDSECTOR"].ToString() : "");
            string IDCLIENTE = ((e.NewValues["IDCLIENTE"] != null) ? e.NewValues["IDCLIENTE"].ToString() : "");
            string CONTACTO_NOMBRE = ((e.NewValues["CONTACTO_NOMBRE"] != null) ? e.NewValues["CONTACTO_NOMBRE"].ToString() : "");
            string CONTACTO_DOMICILO = ((e.NewValues["CONTACTO_DOMICILO"] != null) ? e.NewValues["CONTACTO_DOMICILO"].ToString() : "");
            string CONTACTO_TELEFONO = ((e.NewValues["CONTACTO_TELEFONO"] != null) ? e.NewValues["CONTACTO_TELEFONO"].ToString() : "");
            string CONTACTO_MAIL = ((e.NewValues["CONTACTO_MAIL"] != null) ? e.NewValues["CONTACTO_MAIL"].ToString() : "");
            string OBSERVACIONES = ((e.NewValues["OBSERVACIONES"] != null) ? e.NewValues["OBSERVACIONES"].ToString() : "");

            Interfaz.Alta_SOLICITUD_COTIZACION(EMPRESA, IDSECTOR, IDCLIENTE, CONTACTO_NOMBRE, CONTACTO_DOMICILO, CONTACTO_TELEFONO, CONTACTO_MAIL, OBSERVACIONES, Session["usuario"].ToString());
            Recargar();

            

            e.Cancel = true;
            ASPxGridView1.CancelEdit();


           


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

            string EMPRESA = ((e.NewValues["EMPRESA"] != null) ? e.NewValues["EMPRESA"].ToString() : "");
            string IDSECTOR = ((e.NewValues["IDSECTOR"] != null) ? e.NewValues["IDSECTOR"].ToString() : "");
            string IDCLIENTE = ((e.NewValues["IDCLIENTE"] != null) ? e.NewValues["IDCLIENTE"].ToString() : "");
            string CONTACTO_NOMBRE = ((e.NewValues["CONTACTO_NOMBRE"] != null) ? e.NewValues["CONTACTO_NOMBRE"].ToString() : "");
            string CONTACTO_DOMICILO = ((e.NewValues["CONTACTO_DOMICILO"] != null) ? e.NewValues["CONTACTO_DOMICILO"].ToString() : "");
            string CONTACTO_TELEFONO = ((e.NewValues["CONTACTO_TELEFONO"] != null) ? e.NewValues["CONTACTO_TELEFONO"].ToString() : "");
            string CONTACTO_MAIL = ((e.NewValues["CONTACTO_MAIL"] != null) ? e.NewValues["CONTACTO_MAIL"].ToString() : "");
            string OBSERVACIONES = ((e.NewValues["OBSERVACIONES"] != null) ? e.NewValues["OBSERVACIONES"].ToString() : "");

            Interfaz.Editar_SOLICITUD_COTIZACION(e.Keys[0].ToString(),EMPRESA, IDSECTOR, IDCLIENTE, CONTACTO_NOMBRE, CONTACTO_DOMICILO, CONTACTO_TELEFONO, CONTACTO_MAIL, OBSERVACIONES, Session["usuario"].ToString());
            Recargar();

            e.Cancel = true;
            ASPxGridView1.CancelEdit();

        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }
    }


    protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        try
        {
            foreach (GridViewColumn column in ASPxGridView1.Columns)
            {
                GridViewDataColumn dataColumn = column as GridViewDataColumn;
                if (dataColumn == null) continue;
                if (e.NewValues[dataColumn.FieldName] == null && dataColumn.FieldName != "ID" && dataColumn.FieldName != "OBSERVACIONES" && dataColumn.FieldName != "CONTACTO_MAIL" && dataColumn.FieldName != "CONTACTO_DOMICILO")
                {
                    e.Errors[dataColumn] = "Debe completar los casilleros";
                }
            }
            if (e.Errors.Count > 0) e.RowError = "Por favor, complete todos los datos";


            if (e.NewValues["EMPRESA"] != null && e.NewValues["EMPRESA"].ToString().Length < 1)
            {
                AddError(e.Errors, ASPxGridView1.Columns["EMPRESA"], "Cargue EMPRESA");
            }

            if (e.NewValues["IDSECTOR"] != null && e.NewValues["IDSECTOR"].ToString().Length < 1)
            {
                AddError(e.Errors, ASPxGridView1.Columns["IDSECTOR"], "Cargue SECTOR");
            }
            if (e.NewValues["IDCLIENTE"] != null && e.NewValues["IDCLIENTE"].ToString().Length < 1)
            {
                AddError(e.Errors, ASPxGridView1.Columns["IDCLIENTE"], "Cargue CLIENTE");
            }
            if (e.NewValues["CONTACTO_NOMBRE"] != null && e.NewValues["CONTACTO_NOMBRE"].ToString().Length < 1)
            {
                AddError(e.Errors, ASPxGridView1.Columns["CONTACTO_NOMBRE"], "Cargue CONTACTO");
            }
            
            
            if (e.NewValues["CONTACTO_TELEFONO"] != null && e.NewValues["CONTACTO_TELEFONO"].ToString().Length < 1)
            {
                AddError(e.Errors, ASPxGridView1.Columns["CONTACTO_TELEFONO"], "Cargue MAIL");
            }

       
            
            
        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }
    }

    void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
    {
        try
        {
            if (errors.ContainsKey(column)) return;
            errors[column] = errorText;
        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }
    }


   


}