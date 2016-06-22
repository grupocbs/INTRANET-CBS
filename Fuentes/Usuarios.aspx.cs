using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;


public partial class Usuarios : System.Web.UI.Page
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
            ASPxGridView1.DataSource = Interfaz.CargarCuentasUsuario("");
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
            Interfaz.EliminarCuentaUsuario(e.Keys[0].ToString());
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
            GridViewDataComboBoxColumn combo = ASPxGridView1.Columns["Supervisor"] as GridViewDataComboBoxColumn;
	combo.PropertiesComboBox.Items.Add(" ", " ");
            combo.PropertiesComboBox.Items.Add("1-Ver", "1-Ver");
            combo.PropertiesComboBox.Items.Add("2-Cargar", "2-Cargar");
            combo.PropertiesComboBox.Items.Add("3-Administrar", "3-Administrar");

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
            Interfaz.AgregarCuentaUsuario(e.NewValues["Usuario"].ToString(), e.NewValues["Contraseña"].ToString(), e.NewValues["Nombre"].ToString(), e.NewValues["Mail"].ToString(), e.NewValues["Supervisor"].ToString(), e.NewValues["Imei"].ToString());
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
            Interfaz.EditarCuentaUsuario(e.NewValues["Usuario"].ToString(), e.OldValues["Usuario"].ToString(), e.NewValues["Contraseña"].ToString(), e.NewValues["Nombre"].ToString(), e.NewValues["Mail"].ToString(), e.NewValues["Supervisor"].ToString(), e.NewValues["Imei"].ToString());
           
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
           

            if (e.NewValues["Usuario"] != null && e.NewValues["Usuario"].ToString().Length < 1)
            {
                AddError(e.Errors, ASPxGridView1.Columns["Usuario"], "Cargue Usuario");
            }

            if (e.NewValues["Contraseña"] != null && e.NewValues["Contraseña"].ToString().Length < 1)
            {
                AddError(e.Errors, ASPxGridView1.Columns["Contraseña"], "Cargue Contraseña");
            }
            if (e.NewValues["Nombre"] != null && e.NewValues["Nombre"].ToString().Length < 1)
            {
                AddError(e.Errors, ASPxGridView1.Columns["Nombre"], "Cargue Nombre");
            }
            if (e.NewValues["Mail"] != null && e.NewValues["Mail"].ToString().Length < 1)
            {
                AddError(e.Errors, ASPxGridView1.Columns["Mail"], "Cargue Mail");
            }

            if (e.NewValues["Supervisor"] != null && e.NewValues["Supervisor"].ToString().Length < 1)
            {
                AddError(e.Errors, ASPxGridView1.Columns["Supervisor"], "Cargue Permiso de Supervisor");
            }

            if (e.NewValues["Imei"] != null && e.NewValues["Imei"].ToString().Length < 1)
            {
                AddError(e.Errors, ASPxGridView1.Columns["Imei"], "Cargue Imei o complete con un espacio");
            }

            if (e.NewValues["Recorredor"] != null && e.NewValues["Recorredor"].ToString().Length < 1)
            {
                AddError(e.Errors, ASPxGridView1.Columns["Recorredor"], "Cargue Recorredor");
            }
            
            if (string.IsNullOrEmpty(e.RowError) && e.Errors.Count > 0) e.RowError = "Por favor verifique los errores";

            string sql = "SELECT cant=count(*) ";
            sql += " FROM Usuarios  with (nolock) ";
            sql += " WHERE usuario='" + e.NewValues["Usuario"].ToString() + "'";

            DataTable dt = Interfaz.EjecutarConsultaBD("intranet", sql);

            if (Convert.ToInt32(dt.Rows[0]["cant"].ToString()) > 1)
            {
                AddError(e.Errors, ASPxGridView1.Columns["Usuario"], "Cargue un Usuario Distinto");
                e.RowError = "Este Usuario ya existe";
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