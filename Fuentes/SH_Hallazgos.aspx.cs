using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Collections;
using DevExpress.Export;
using DevExpress.Web;
using DevExpress.XtraPrinting;

public partial class SH_Hallazgos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

         //Session.Add("usuario", "1");
         if (Session["usuario"] == null)
         {

             Response.Redirect("login.aspx");
         }

            if (!IsPostBack)
            {
               

                 
            }


            Cargar();
           
        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }

    }

    


    
    
    private void Cargar()
    {
        try
        {
            string sql = "SELECT NRO=RIGHT('00'+CAST(ID AS VARCHAR),2) + '/' + CAST(YEAR(FECHA) AS VARCHAR),* ";
            sql += " FROM NO_CONFORMIDADES with (nolock) ";
           //  sql += " where ID_SECTOR='" + rbtl_sectores.SelectedItem.Value.ToString() + "' ";
            sql += " WHERE USUARIO='" + Session["usuario"].ToString() + "'";
            sql += " ORDER BY id";

            DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql);
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }
    }


    protected void fileuploaded_C(object sender, FileUploadCompleteEventArgs e)
    {
        ASPxUploadControl file_C = (sender as ASPxUploadControl);
        if (file_C.UploadedFiles != null)
            if (file_C.UploadedFiles.Count() > 0)
            {
                string t = "Archivos/SH/Hallazgos/" + file_C.UploadedFiles[0].FileName;
                file_C.SaveAs(MapPath(t));
                Session["fileuploaded_C"] = t;
            }
    }
   
    protected void VencimientosGrid_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        try
        {
            e.Cell.Style.Add(HtmlTextWriterStyle.TextAlign, "center");


             
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

            GridViewDataComboBoxColumn combo1 = ASPxGridView1.Columns["ID_ORIGEN"] as GridViewDataComboBoxColumn;
            GridViewDataComboBoxColumn combo3 = ASPxGridView1.Columns["ID_TIPO"] as GridViewDataComboBoxColumn;

            foreach (DataRow dr in Interfaz.EjecutarConsultaBD("LocalSqlServer", "select ID, DESCRIPCION, TIPO from NO_CONFORMIDADES_TIPOS with(nolock)").Rows)
            {
                switch (dr["TIPO"].ToString())
                {

                    case "ORIGEN":
                        {
                            combo1.PropertiesComboBox.Items.Add(new ListEditItem(dr["DESCRIPCION"].ToString(), dr["ID"].ToString()));
                            break;
                        }
                    case "TIPO":
                        {
                            combo3.PropertiesComboBox.Items.Add(new ListEditItem(dr["DESCRIPCION"].ToString(), dr["ID"].ToString()));
                            break;
                        }
                }


            }




            GridViewDataComboBoxColumn combo2 = ASPxGridView1.Columns["RESPONSABLE"] as GridViewDataComboBoxColumn;

            foreach (DataRow dr in Interfaz.EjecutarConsultaBD("CBS", " SELECT SJMLGH_NOMBRE, SJMLGH_NROLEG  FROM SJMLGH WITH(NOLOCK)  WHERE SJMLGH_DEBAJA='N'  ORDER BY SJMLGH_NOMBRE").Rows)
            {
                combo2.PropertiesComboBox.Items.Add(new ListEditItem(dr["SJMLGH_NOMBRE"].ToString(), dr["SJMLGH_NROLEG"].ToString()));

            }

            GridViewDataComboBoxColumn combo4 = ASPxGridView1.Columns["ID_SECTOR_INT"] as GridViewDataComboBoxColumn;

            foreach (DataRow dr in Interfaz.EjecutarConsultaBD("CBS", "  SELECT GRTSEM_CODSEM, GRTSEM_DESCRP  FROM GRTSEM WITH(NOLOCK)  WHERE GRTSEM_DEBAJA='N'  ORDER BY GRTSEM_DESCRP").Rows)
            {
                combo4.PropertiesComboBox.Items.Add(new ListEditItem(dr["GRTSEM_DESCRP"].ToString(), dr["GRTSEM_CODSEM"].ToString()));

            }

            GridViewDataComboBoxColumn combo5 = ASPxGridView1.Columns["ID_SECTOR"] as GridViewDataComboBoxColumn;
            foreach (DataRow dr in Interfaz.EjecutarConsultaBD("LocalSqlServer", "select ID, DESCRIPCION from NO_CONFORMIDADES_TIPOS  with(nolock) where tipo='SECTOR'").Rows)
            {
                combo5.PropertiesComboBox.Items.Add(new ListEditItem(dr["DESCRIPCION"].ToString(), dr["ID"].ToString()));

            }

             

        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }

    }

    protected void btnPdfExport_Click(object sender, EventArgs e)
    {
        ASPxGridViewExporter1.ReportHeader = ASPxGridView1.FilterExpression;
        ASPxGridViewExporter1.WritePdfToResponse();
    }
    protected void btnXlsExport_Click(object sender, EventArgs e)
    {
        ASPxGridViewExporter1.ReportHeader = ASPxGridView1.FilterExpression;
        ASPxGridViewExporter1.WriteXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.WYSIWYG });
    }

    protected void btnCsvExport_Click(object sender, EventArgs e)
    {
        ASPxGridViewExporter1.ReportHeader = ASPxGridView1.FilterExpression;
        ASPxGridViewExporter1.WriteCsvToResponse(new CsvExportOptionsEx() { ExportType = ExportType.WYSIWYG });
    }

     
   
    
    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        try
        {
            string ID_SECTOR = ((e.NewValues["ID_SECTOR"] == null) ? "" : e.NewValues["ID_SECTOR"].ToString());
            string DESCRIPCION = ((e.NewValues["DESCRIPCION"] == null) ? "" : e.NewValues["DESCRIPCION"].ToString());
            string MEDIDA_INMEDIATA = ((e.NewValues["MEDIDA_INMEDIATA"] == null) ? "" : e.NewValues["MEDIDA_INMEDIATA"].ToString());
            string INVESTIGACION = ((e.NewValues["INVESTIGACION"] == null) ? "" : e.NewValues["INVESTIGACION"].ToString());
            bool CORRESPONDE_AC = ((e.NewValues["CORRESPONDE_AC"] == null) ? false : Convert.ToBoolean(e.NewValues["CORRESPONDE_AC"]));
            string ACCION_INMEDIATA = ((e.NewValues["ACCION_INMEDIATA"] == null) ? "" : e.NewValues["ACCION_INMEDIATA"].ToString());
            DateTime PLAZO = ((e.NewValues["PLAZO"] == null) ? Convert.ToDateTime("19000101") : Convert.ToDateTime(e.NewValues["PLAZO"]));
             string RESPONSABLE = ((e.NewValues["RESPONSABLE"] == null) ? "" : e.NewValues["RESPONSABLE"].ToString());
             string OBSERVACIONES = ((e.NewValues["OBSERVACIONES"] == null) ? "" : e.NewValues["OBSERVACIONES"].ToString());
           
             string PUNTO_NORMA = ((e.NewValues["PUNTO_NORMA"] == null) ? "" : e.NewValues["PUNTO_NORMA"].ToString());
             string ID_ORIGEN = ((e.NewValues["ID_ORIGEN"] == null) ? "" : e.NewValues["ID_ORIGEN"].ToString());
             string ID_TIPO = ((e.NewValues["ID_TIPO"] == null) ? "" : e.NewValues["ID_TIPO"].ToString());
             string ID_SECTOR_INT = ((e.NewValues["ID_SECTOR_INT"] == null) ? "" : e.NewValues["ID_SECTOR_INT"].ToString());

             Interfaz.Agregar_NC(ID_SECTOR, DESCRIPCION, MEDIDA_INMEDIATA, INVESTIGACION, CORRESPONDE_AC, ACCION_INMEDIATA, PLAZO, RESPONSABLE, OBSERVACIONES, Session["usuario"].ToString(), PUNTO_NORMA, ID_ORIGEN, ID_TIPO, ID_SECTOR_INT);
            DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", "SELECT top 1 id FROm NO_CONFORMIDADES with(nolock) order by id desc ");
            if (dt.Rows[0]["id"] != null)
            {

                if (((ASPxGridView)sender).ID == "ASPxGridView1")
                {
                    if (Session["fileuploaded_C"] != null)
                    {
                        Interfaz.EditarHallazgo_Archivo(dt.Rows[0]["id"].ToString(), Session["fileuploaded_C"].ToString());
                    }
                }


            }



            Session["fileuploaded_C"] = null;
            Session.Remove("fileuploaded_C");
 
            Cargar();

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
            string ID_SECTOR = ((e.NewValues["ID_SECTOR"] == null) ? "" : e.NewValues["ID_SECTOR"].ToString());
            string DESCRIPCION = ((e.NewValues["DESCRIPCION"] == null) ? "" : e.NewValues["DESCRIPCION"].ToString());
            string MEDIDA_INMEDIATA = ((e.NewValues["MEDIDA_INMEDIATA"] == null) ? "" : e.NewValues["MEDIDA_INMEDIATA"].ToString());
            string INVESTIGACION = ((e.NewValues["INVESTIGACION"] == null) ? "" : e.NewValues["INVESTIGACION"].ToString());
            bool CORRESPONDE_AC = ((e.NewValues["CORRESPONDE_AC"] == null) ? false : Convert.ToBoolean(e.NewValues["CORRESPONDE_AC"]));
            string ACCION_INMEDIATA = ((e.NewValues["ACCION_INMEDIATA"] == null) ? "" : e.NewValues["ACCION_INMEDIATA"].ToString());
            DateTime PLAZO = ((e.NewValues["PLAZO"] == null) ? Convert.ToDateTime("19000101") : Convert.ToDateTime(e.NewValues["PLAZO"]));
            string RESPONSABLE = ((e.NewValues["RESPONSABLE"] == null) ? "" : e.NewValues["RESPONSABLE"].ToString());
            string OBSERVACIONES = ((e.NewValues["OBSERVACIONES"] == null) ? "" : e.NewValues["OBSERVACIONES"].ToString());
          
            string PUNTO_NORMA = ((e.NewValues["PUNTO_NORMA"] == null) ? "" : e.NewValues["PUNTO_NORMA"].ToString());
            string ID_ORIGEN = ((e.NewValues["ID_ORIGEN"] == null) ? "" : e.NewValues["ID_ORIGEN"].ToString());
            string ID_TIPO = ((e.NewValues["ID_TIPO"] == null) ? "" : e.NewValues["ID_TIPO"].ToString());
            string ID_SECTOR_INT = ((e.NewValues["ID_SECTOR_INT"] == null) ? "" : e.NewValues["ID_SECTOR_INT"].ToString());

            Interfaz.Editar_NC(e.Keys[0].ToString(), DESCRIPCION, MEDIDA_INMEDIATA, INVESTIGACION, CORRESPONDE_AC, ACCION_INMEDIATA, PLAZO, RESPONSABLE, OBSERVACIONES, Session["usuario"].ToString(), PUNTO_NORMA, ID_ORIGEN, ID_TIPO, ID_SECTOR_INT);

            if (((ASPxGridView)sender).ID == "ASPxGridView1")
            {
                if (Session["fileuploaded_C"] != null)
                {
                    Interfaz.EditarHallazgo_Archivo(e.Keys[0].ToString(), Session["fileuploaded_C"].ToString());

                }
            }


            Session["fileuploaded_C"] = null;
            Session.Remove("fileuploaded_C");
            
            Cargar();


            e.Cancel = true;
            ASPxGridView1.CancelEdit();
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
            Interfaz.Eliminar_NC(e.Keys[0].ToString());
            Cargar();
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
         

        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }
    }
    protected void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
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



    protected void ASPxGridView2_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        try
        {
            e.Cell.Style.Add(HtmlTextWriterStyle.TextAlign, "center");

            if (e.CellValue.ToString() == "0")
            {
                e.Cell.Style.Add(HtmlTextWriterStyle.BackgroundColor, "Yellow");
                e.Cell.Style.Add(HtmlTextWriterStyle.Color, "Black");
            }

          

            if (e.CellValue.ToString() == "1")
            {
                e.Cell.Style.Add(HtmlTextWriterStyle.BackgroundColor, "Green");
                e.Cell.Style.Add(HtmlTextWriterStyle.Color, "White");
            }
        }

        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }
    }
    protected void ASPxGridView1_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        try
        {

            string sql = "SELECT EFECTIVIDAD_SEG ";
            sql += " FROM NO_CONFORMIDADES with (nolock) ";
            sql += " WHERE id=" + e.KeyValue.ToString();


            DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql);
            if (dt.Rows.Count > 0)
            {


                if (Convert.ToBoolean(dt.Rows[0]["EFECTIVIDAD_SEG"]) == false)
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#ffffbf");
                    // e.Row.Style.Add(HtmlTextWriterStyle.Color, "Black");
                }

                if (Convert.ToBoolean(dt.Rows[0]["EFECTIVIDAD_SEG"]) == true)
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#dfefdf");
                    // e.Row.Style.Add(HtmlTextWriterStyle.Color, "White");
                }


            }

        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }
    }
}