using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;

public partial class RRHH_TEL_INTERNOS : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null)
        {

            Response.Redirect("login.aspx");
        }
        if (!IsPostBack)
        {

            Recargar();
        }
    }


    private void Recargar()
    {
        try
        {
            ASPxGridView1.DataSource = Interfaz.EjecutarConsultaBD("LocalSqlServer", "SELECT * FROM INTERNOS with(nolock) ORDER BY INTERNO  ");
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
            Interfaz.EliminarInterno(e.Keys[0].ToString());
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
            //GridViewDataComboBoxColumn combo = ASPxGridView1.Columns["Perfil"] as GridViewDataComboBoxColumn;


            //foreach (DataRow dr in Interfaz.CargarPerfiles("").Rows)
            //{
            //    combo.PropertiesComboBox.Items.Add(new ListEditItem(dr["descripcion"].ToString(), dr["perfilid"].ToString()));

            //}
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
            Interfaz.AgregarInterno(e.NewValues["INTERNO"].ToString(), e.NewValues["AREA"].ToString(), e.NewValues["INTEGRANTES"].ToString());
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
            Interfaz.EditarInterno(e.NewValues["INTERNO"].ToString(), e.NewValues["AREA"].ToString(), e.NewValues["INTEGRANTES"].ToString(), e.Keys[0].ToString());

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
                if (e.NewValues[dataColumn.FieldName] == null)
                {
                    e.Errors[dataColumn] = "Debe completar los casilleros";
                }
            }
            if (e.Errors.Count > 0) e.RowError = "Por favor, complete todos los datos";


            if (e.NewValues["INTERNO"] != null && e.NewValues["INTERNO"].ToString().Length < 1)
            {
                AddError(e.Errors, ASPxGridView1.Columns["INTERNO"], "Cargue INTERNO");
            }

            if (e.NewValues["AREA"] != null && e.NewValues["AREA"].ToString().Length < 1)
            {
                AddError(e.Errors, ASPxGridView1.Columns["AREA"], "Cargue AREA");
            }

            if (e.NewValues["INTEGRANTES"] != null && e.NewValues["INTEGRANTES"].ToString().Length < 1)
            {
                AddError(e.Errors, ASPxGridView1.Columns["INTEGRANTES"], "Cargue INTEGRANTES");
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
