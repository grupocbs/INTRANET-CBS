using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;


public partial class Menu : System.Web.UI.Page
{



    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                Recargar();
            }

            if (Session["usuario"] == null)
            {

                Response.Redirect("login.aspx");
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
            ASPxGridView1.DataSource = Interfaz.CargarMenu();
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
            Interfaz.EliminarMenu(e.Keys[0].ToString());
            Recargar();
            e.Cancel = true;
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
            Interfaz.AgregarMenu(e.NewValues["Descripcion"].ToString(), e.NewValues["PadreId"].ToString(), e.NewValues["Posicion"].ToString(), e.NewValues["Icono"].ToString(), e.NewValues["Url"].ToString());
            Recargar();
            e.Cancel = true;
            
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
            Interfaz.EditarMenu(e.Keys[0].ToString(), e.NewValues["Descripcion"].ToString(), e.NewValues["PadreId"].ToString(), e.NewValues["Posicion"].ToString(), e.NewValues["Icono"].ToString(), e.NewValues["Url"].ToString());
            Recargar();
            e.Cancel = true;
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
                if (e.NewValues[dataColumn.FieldName] == null && dataColumn.FieldName!="MenuId")
                {
                    e.Errors[dataColumn] = "Debe completar los casilleros";
                }
            }
            if (e.Errors.Count > 0) e.RowError = "Por favor, complete todos los datos";
            if (e.NewValues["descripcion"] != null && e.NewValues["descripcion"].ToString().Length < 1)
            {
                AddError(e.Errors, ASPxGridView1.Columns["descripcion"], "Carga descripcion");
            }

            if (e.NewValues["padreid"] != null && e.NewValues["padreid"].ToString().Length < 1)
            {
                AddError(e.Errors, ASPxGridView1.Columns["padreid"], "Elija id menu padre");
            }

            if (e.NewValues["posicion"] != null && e.NewValues["posicion"].ToString().Length < 1)
            {
                AddError(e.Errors, ASPxGridView1.Columns["posicion"], "Posicion");
            }

            if (e.NewValues["Icono"] != null && e.NewValues["Icono"].ToString().Length < 1)
            {
                AddError(e.Errors, ASPxGridView1.Columns["Icono"], "Coloque Icono");
            }

            if (e.NewValues["Url"] != null && e.NewValues["Url"].ToString().Length < 1)
            {
                AddError(e.Errors, ASPxGridView1.Columns["Url"], "Url de la pagin a abrir");
            }




            if (string.IsNullOrEmpty(e.RowError) && e.Errors.Count > 0) e.RowError = "Por favor verifique los errores";
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