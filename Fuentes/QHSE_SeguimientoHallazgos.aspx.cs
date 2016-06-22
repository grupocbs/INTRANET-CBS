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
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxCallbackPanel;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxClasses;
using DevExpress.Web.ASPxFileManager;
using DevExpress.Web.ASPxUploadControl;

public partial class QHSE_SeguimientoHallazgos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

         // Session.Add("usuario", "galonso");
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

            string sql1 = " select EVENTOS='ROJO',TOTAL=COUNT(*)";
            sql1 += " from QHSE_seguimientoHallazgo  with(nolock)";
            sql1 += " where LEN(RTRIM(LTRIM(ISNULL(archivo_ai,''))))=0 ";
            sql1 += " AND LEN(RTRIM(LTRIM(ISNULL(archivo_ac,''))))=0";
            sql1 += " UNION";
            sql1 += " select EVENTOS='AMARILLO',TOTAL=COUNT(*)";
            sql1 += " from QHSE_seguimientoHallazgo  with(nolock)";
            sql1 += " where LEN(RTRIM(LTRIM(ISNULL(archivo_evidencia,''))))=0 ";
            sql1 += " AND (LEN(RTRIM(LTRIM(ISNULL(archivo_ai,''))))>0 or LEN(RTRIM(LTRIM(ISNULL(archivo_ac,''))))>0)";
            sql1 += " UNION";
            sql1 += " select EVENTOS='VERDE',TOTAL=COUNT(*)";
            sql1 += " from QHSE_seguimientoHallazgo  with(nolock)";
            sql1 += " where LEN(RTRIM(LTRIM(ISNULL(archivo_evidencia,''))))>0 ";
            sql1 += " AND (LEN(RTRIM(LTRIM(ISNULL(archivo_ai,''))))>0 or LEN(RTRIM(LTRIM(ISNULL(archivo_ac,''))))>0)";



            DataTable dt1 = ConvertToCrossTab(Interfaz.EjecutarConsultaBD("Intranet", sql1));
            ASPxGridView2.DataSource = dt1;
            ASPxGridView2.DataBind();


            ASPxGridView2.SettingsText.Title = "Cuadro Resumen. Eventos:" + Interfaz.EjecutarConsultaBD("Intranet", "select total=COUNT(*) from QHSE_seguimientoHallazgo with(nolock)").Rows[0]["total"].ToString();




            string sql = "SELECT * ";
            sql += " FROM QHSE_seguimientoHallazgo with (nolock) ";
            sql += " ORDER BY idhallazgo desc";

            DataTable dt = Interfaz.EjecutarConsultaBD("Intranet", sql);
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }
    }

    public DataTable ConvertToCrossTab(DataTable table)
    {
        DataTable newTable = new DataTable();

        for (int r = 0; r <= table.Rows.Count; r++)
        {
            newTable.Columns.Add(r.ToString());
        }

        for (int c = 0; c < table.Columns.Count; c++)
        {
            DataRow row = newTable.NewRow();
            object[] rowValues = new object[table.Rows.Count + 1];

            rowValues[0] = table.Columns[c].Caption;

            for (int r = 1; r <= table.Rows.Count; r++)
            {
                rowValues[r] = table.Rows[r - 1][c].ToString();
            }

            row.ItemArray = rowValues;
            newTable.Rows.Add(row);
        }

        return newTable;
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

    protected void ASPxGridView1_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {
        try
        {

            string sql = "SELECT archivo_ai,archivo_ac, archivo_evidencia ";
            sql += " FROM QHSE_seguimientoHallazgo with (nolock) ";
            sql += " WHERE idhallazgo=" + e.KeyValue.ToString();
         

            DataTable dt = Interfaz.EjecutarConsultaBD("Intranet", sql);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["archivo_ai"].ToString().Length == 0 && dt.Rows[0]["archivo_ac"].ToString().Length == 0)
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "Red");
                    e.Row.Style.Add(HtmlTextWriterStyle.Color, "White");
                }

                if (dt.Rows[0]["archivo_evidencia"].ToString().Length == 0 && (dt.Rows[0]["archivo_ac"].ToString().Length > 0 || dt.Rows[0]["archivo_ai"].ToString().Length > 0))
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "Yellow");
                    e.Row.Style.Add(HtmlTextWriterStyle.Color, "Black");
                }

                if (dt.Rows[0]["archivo_evidencia"].ToString().Length > 0 && (dt.Rows[0]["archivo_ac"].ToString().Length > 0 || dt.Rows[0]["archivo_ai"].ToString().Length > 0))
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "Green");
                    e.Row.Style.Add(HtmlTextWriterStyle.Color, "White");
                }

             
            }

        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }
    }
    protected void ASPxGridView1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        try
        {


            DataTable dt = Interfaz.EjecutarConsultaBD("intranet", " select fecha,sector, lugar_equipo,descripcion from QHSE_accidentes with(nolock) where idaccidente=" + e.Parameters);
            ASPxGridView1.JSProperties["cpaccidente"] = "";
            ASPxGridView1.JSProperties["cpfecha"] = "";
            ASPxGridView1.JSProperties["cparea"] = "";
            ASPxGridView1.JSProperties["cpequipo_sector"] = "";
            ASPxGridView1.JSProperties["cpdescripcion"] = "";
            ASPxGridView1.JSProperties["cpcalificacion"] = "";
            if (dt.Rows.Count > 0)
            {
                ASPxGridView1.JSProperties["cpaccidente"] = e.Parameters;
                ASPxGridView1.JSProperties["cpfecha"] = dt.Rows[0]["fecha"];
                ASPxGridView1.JSProperties["cparea"] = dt.Rows[0]["sector"].ToString();
                ASPxGridView1.JSProperties["cpequipo_sector"] = dt.Rows[0]["lugar_equipo"].ToString();
                ASPxGridView1.JSProperties["cpdescripcion"] = dt.Rows[0]["descripcion"].ToString();
                ASPxGridView1.JSProperties["cpcalificacion"] = "Accidente/Incidente";
            }


 DataTable dt1 = Interfaz.EjecutarConsultaBD("Intranet", "SELECT  isnull(max(cast(numero as int)) +1,1) as numero FROM QHSE_seguimientoHallazgo with(nolock) ORDER BY numero DESC");
	ASPxGridView1.JSProperties["cpnumero"] = "";
            if (dt1.Rows.Count > 0)
            {

                ASPxGridView1.JSProperties["cpnumero"] = dt1.Rows[0]["numero"].ToString();
            }


        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }

    }
    protected void ASPxGridView1_Init(object sender, EventArgs e)
    {
        try
        {
            GridViewDataComboBoxColumn combo = ASPxGridView1.Columns["responsable"] as GridViewDataComboBoxColumn;
            GridViewDataComboBoxColumn combo5 = ASPxGridView1.Columns["responsable_ac"] as GridViewDataComboBoxColumn;
            GridViewDataComboBoxColumn combo6 = ASPxGridView1.Columns["responsable_ve"] as GridViewDataComboBoxColumn;
            foreach (DataRow dr in Interfaz.EjecutarConsultaBD("Intranet", "SELECT legajo,apellido + ' ' + nombre + ' ' + cast(legajo as varchar) AS n FROM nomina with(nolock) where legajo is not null and [fecha de baja] is null ORDER BY apellido").Rows)
            {
                combo.PropertiesComboBox.Items.Add(new ListEditItem(dr["n"].ToString(), dr["legajo"].ToString()));
                combo5.PropertiesComboBox.Items.Add(new ListEditItem(dr["n"].ToString(), dr["legajo"].ToString()));
                combo6.PropertiesComboBox.Items.Add(new ListEditItem(dr["n"].ToString(), dr["legajo"].ToString()));
            }

            GridViewDataComboBoxColumn combo1 = ASPxGridView1.Columns["equipo_sector"] as GridViewDataComboBoxColumn;
            foreach (DataRow dr in Interfaz.EjecutarConsultaBD("Intranet", " SELECT idequiposnov, Equipos FROM RRHH_NOV_EQUIPOS with(nolock)  order by Equipos desc").Rows)
            {
                combo1.PropertiesComboBox.Items.Add(new ListEditItem(dr["Equipos"].ToString(), dr["Equipos"].ToString()));
            }

            GridViewDataComboBoxColumn combo2 = ASPxGridView1.Columns["calificacion"] as GridViewDataComboBoxColumn;
            combo2.PropertiesComboBox.Items.Add(new ListEditItem("NC", "NC"));
            combo2.PropertiesComboBox.Items.Add(new ListEditItem("Inspeccion", "Inspeccion"));
            combo2.PropertiesComboBox.Items.Add(new ListEditItem("Accidente", "Accidente"));
		combo2.PropertiesComboBox.Items.Add(new ListEditItem("Observacion", "Observacion"));
		combo2.PropertiesComboBox.Items.Add(new ListEditItem("Oportunidad de mejora", "Oportunidad de mejora"));
            combo2.PropertiesComboBox.Items.Add(new ListEditItem("Reclamo del Cliente", "Reclamo del Cliente"));
            combo2.PropertiesComboBox.Items.Add(new ListEditItem("Reclamo de Partes interesadas", "Reclamo de Partes interesadas"));

            GridViewDataComboBoxColumn combo3 = ASPxGridView1.Columns["area"] as GridViewDataComboBoxColumn;
            combo3.PropertiesComboBox.Items.Add(new ListEditItem("PullyWO", "PullyWO"));
            combo3.PropertiesComboBox.Items.Add(new ListEditItem("Perforacion", "Perforacion"));
            combo3.PropertiesComboBox.Items.Add(new ListEditItem("Base", "Base"));
            combo3.PropertiesComboBox.Items.Add(new ListEditItem("Mantenimiento", "Mantenimiento"));
            combo3.PropertiesComboBox.Items.Add(new ListEditItem("Vehicular", "Vehicular"));
            combo3.PropertiesComboBox.Items.Add(new ListEditItem("In Itinere", "In Itinere"));

            GridViewDataComboBoxColumn combo4 = ASPxGridView1.Columns["accidente"] as GridViewDataComboBoxColumn;
            foreach (DataRow dr in Interfaz.EjecutarConsultaBD("Intranet", " select idaccidente,cast(idaccidente as varchar) + '-' + tipo as n from QHSE_accidentes with(nolock) order by idaccidente").Rows)
            {
                combo4.PropertiesComboBox.Items.Add(new ListEditItem(dr["n"].ToString(), dr["idaccidente"].ToString()));
            }
           
        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }

    }

    protected void btnPdfExport_Click(object sender, EventArgs e)
    {
        
        ASPxGridViewExporter1.WritePdfToResponse();
        
    }
    protected void btnXlsExport_Click(object sender, EventArgs e)
    {
        ASPxGridViewExporter1.WriteXlsToResponse();
    }
    protected void btnCsvExport_Click(object sender, EventArgs e)
    {
        
        ASPxGridViewExporter1.WriteCsvToResponse();
    }

    protected void fileuploaded_Evidencia(object sender, FileUploadCompleteEventArgs e)
    {
        ASPxUploadControl fileUC = (sender as ASPxUploadControl);
        if (fileUC.UploadedFiles != null)
            if (fileUC.UploadedFiles.Count() > 0)
            {
                string t = "Archivos/QHSE_Eventos/Hallazgos/EV/" + fileUC.UploadedFiles[0].FileName;
                fileUC.SaveAs(MapPath(t));
                Session["fileuploaded_Evidencia"] = t;
            }
    }
    protected void fileuploaded_AC(object sender, FileUploadCompleteEventArgs e)
    {
        ASPxUploadControl fileUC = (sender as ASPxUploadControl);
        if (fileUC.UploadedFiles != null)
            if (fileUC.UploadedFiles.Count() > 0)
            {
                string t = "Archivos/QHSE_Eventos/Hallazgos/AC/" + fileUC.UploadedFiles[0].FileName;
                fileUC.SaveAs(MapPath(t));
                Session["fileuploaded_AC"] = t;
            }
    }
    protected void fileuploaded_AI(object sender, FileUploadCompleteEventArgs e)
    {
        ASPxUploadControl fileUC = (sender as ASPxUploadControl);
        if (fileUC.UploadedFiles != null)
            if (fileUC.UploadedFiles.Count() > 0)
            {
                string t = "Archivos/QHSE_Eventos/Hallazgos/AI/" + fileUC.UploadedFiles[0].FileName;
                fileUC.SaveAs(MapPath(t));
                Session["fileuploaded_AI"] = t;
            }
    }
   
    protected void ASPxGridView1_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        try
        {
            DataTable dt = Interfaz.EjecutarConsultaBD("Intranet", "SELECT  isnull(max(cast(numero as int)) +1,1) as numero FROM QHSE_seguimientoHallazgo with(nolock) ORDER BY numero DESC");
            if (dt.Rows.Count > 0)
            {

                e.NewValues.Add("numero", dt.Rows[0]["numero"].ToString());
            }

            
            

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
            string accidente = ((e.NewValues["accidente"] == null) ? "" : e.NewValues["accidente"].ToString());
string numero= ((e.NewValues["numero"] == null) ? "" : e.NewValues["numero"].ToString());
string area= ((e.NewValues["area"] == null) ? "" : e.NewValues["area"].ToString());
string equipo_sector= ((e.NewValues["equipo_sector"] == null) ? "" : e.NewValues["equipo_sector"].ToString());
string calificacion= ((e.NewValues["calificacion"] == null) ? "" : e.NewValues["calificacion"].ToString());
string descripcion = ((e.NewValues["descripcion"] == null) ? "" : e.NewValues["descripcion"].ToString());
 string fecha = ((e.NewValues["fecha"] == null) ? "1900-01-01" : e.NewValues["fecha"].ToString());

            string comentario_ai = ((e.NewValues["comentario_ai"] == null) ? "" : e.NewValues["comentario_ai"].ToString());
            string comentario_ev = ((e.NewValues["comentario_ev"] == null) ? "" : e.NewValues["comentario_ev"].ToString());
            string comentario_ac = ((e.NewValues["comentario_ac"] == null) ? "" : e.NewValues["comentario_ac"].ToString());
            string resp_ai = ((e.NewValues["responsable"] == null) ? "" : e.NewValues["responsable"].ToString());
            string resp_ac = ((e.NewValues["responsable_ac"] == null) ? "" : e.NewValues["responsable_ac"].ToString());
            string resp_ve = ((e.NewValues["responsable_ve"] == null) ? "" : e.NewValues["responsable_ve"].ToString());
            string fecha_ai = ((e.NewValues["fecha_accion"] == null) ? "1900-01-01" : e.NewValues["fecha_accion"].ToString());
            string fecha_ac = ((e.NewValues["fecha_AC"] == null) ? "1900-01-01" : e.NewValues["fecha_AC"].ToString());
            string fecha_ve = ((e.NewValues["fecha_cierre_nc"] == null) ? "1900-01-01" : e.NewValues["fecha_cierre_nc"].ToString());
            string punto_norma = ((e.NewValues["punto_norma"] == null) ? "" : e.NewValues["punto_norma"].ToString());
            string causa_raiz = ((e.NewValues["causa_raiz"] == null) ? "" : e.NewValues["causa_raiz"].ToString());

            Interfaz.AltaHallazgo(area, numero, equipo_sector, calificacion, Convert.ToDateTime(fecha), comentario_ai, Convert.ToDateTime(fecha_ai), Convert.ToDateTime(fecha_ac), resp_ai, Convert.ToDateTime(fecha_ve), comentario_ac, comentario_ev,descripcion, punto_norma, accidente, resp_ac, resp_ve, causa_raiz);


            DataTable dt = Interfaz.EjecutarConsultaBD("Intranet", "SELECT top 1 idhallazgo FROm QHSE_seguimientoHallazgo with(nolock) order by idhallazgo desc ");
            if (dt.Rows[0]["idhallazgo"] != null)
            {
                if (Session["fileuploaded_Evidencia"] != null)
                {
                    Interfaz.EditarHallazgo_ArchivoEV(dt.Rows[0]["idhallazgo"].ToString(), Session["fileuploaded_Evidencia"].ToString());

                }
                if (Session["fileuploaded_AC"] != null)
                {
                    Interfaz.EditarHallazgo_ArchivoAC(dt.Rows[0]["idhallazgo"].ToString(), Session["fileuploaded_AC"].ToString());
                }
                if (Session["fileuploaded_AI"] != null)
                {
                    Interfaz.EditarHallazgo_ArchivoAI(dt.Rows[0]["idhallazgo"].ToString(), Session["fileuploaded_AI"].ToString());
                }
            }


            Session["fileuploaded_Evidencia"]=null;
            Session["fileuploaded_AC"] = null;
            Session["fileuploaded_AI"] = null;
            Session.Remove("fileuploaded_Evidencia");
            Session.Remove("fileuploaded_AC");
            Session.Remove("fileuploaded_AI");
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
            string accidente = ((e.NewValues["accidente"] == null) ? "" : e.NewValues["accidente"].ToString());
string numero= ((e.NewValues["numero"] == null) ? "" : e.NewValues["numero"].ToString());
string area= ((e.NewValues["area"] == null) ? "" : e.NewValues["area"].ToString());
string equipo_sector= ((e.NewValues["equipo_sector"] == null) ? "" : e.NewValues["equipo_sector"].ToString());
string calificacion= ((e.NewValues["calificacion"] == null) ? "" : e.NewValues["calificacion"].ToString());
string descripcion = ((e.NewValues["descripcion"] == null) ? "" : e.NewValues["descripcion"].ToString());
 string fecha = ((e.NewValues["fecha"] == null) ? "1900-01-01" : e.NewValues["fecha"].ToString());


            string comentario_ai = ((e.NewValues["comentario_ai"] == null) ? "" : e.NewValues["comentario_ai"].ToString());
            string comentario_ev = ((e.NewValues["comentario_ev"] == null) ? "" : e.NewValues["comentario_ev"].ToString());
            string comentario_ac = ((e.NewValues["comentario_ac"] == null) ? "" : e.NewValues["comentario_ac"].ToString());
            string resp_ai = ((e.NewValues["responsable"] == null) ? "" : e.NewValues["responsable"].ToString());
            string resp_ac = ((e.NewValues["responsable_ac"] == null) ? "" : e.NewValues["responsable_ac"].ToString());
            string resp_ve = ((e.NewValues["responsable_ve"] == null) ? "" : e.NewValues["responsable_ve"].ToString());
            string fecha_ai = ((e.NewValues["fecha_accion"] == null) ? "1900-01-01" : e.NewValues["fecha_accion"].ToString());
            string fecha_ac = ((e.NewValues["fecha_AC"] == null) ? "1900-01-01" : e.NewValues["fecha_AC"].ToString());
            string fecha_ve = ((e.NewValues["fecha_cierre_nc"] == null) ? "1900-01-01" : e.NewValues["fecha_cierre_nc"].ToString());
            string punto_norma = ((e.NewValues["punto_norma"] == null) ? "" : e.NewValues["punto_norma"].ToString());
            string causa_raiz = ((e.NewValues["causa_raiz"] == null) ? "" : e.NewValues["causa_raiz"].ToString());

            Interfaz.EditarHallazgo(e.Keys[0].ToString(), area, numero, equipo_sector, calificacion, Convert.ToDateTime(fecha), comentario_ai, Convert.ToDateTime(fecha_ai), Convert.ToDateTime(fecha_ac), resp_ai, Convert.ToDateTime(fecha_ve), comentario_ac, comentario_ev,descripcion, punto_norma,accidente, resp_ac, resp_ve, causa_raiz);

            if (Session["fileuploaded_Evidencia"] != null)
            {
                Interfaz.EditarHallazgo_ArchivoEV(e.Keys[0].ToString(), Session["fileuploaded_Evidencia"].ToString());

            }
            if (Session["fileuploaded_AC"] != null)
            {
                Interfaz.EditarHallazgo_ArchivoAC(e.Keys[0].ToString(), Session["fileuploaded_AC"].ToString());
            }
            if (Session["fileuploaded_AI"] != null)
            {
                Interfaz.EditarHallazgo_ArchivoAI(e.Keys[0].ToString(), Session["fileuploaded_AI"].ToString());
            }


            Session["fileuploaded_Evidencia"] = null;
            Session["fileuploaded_AC"] = null;
            Session["fileuploaded_AI"] = null;
            Session.Remove("fileuploaded_Evidencia");
            Session.Remove("fileuploaded_AC");
            Session.Remove("fileuploaded_AI");
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
            Interfaz.EliminarHallazgo(e.Keys[0].ToString());
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


    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        try
        {
            ASPxGridView1.AddNewRow();

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

            if (e.CellValue.ToString() == "AMARILLO")
            {
                e.Cell.Style.Add(HtmlTextWriterStyle.BackgroundColor, "Yellow");
                e.Cell.Style.Add(HtmlTextWriterStyle.Color, "Black");
            }

            if (e.CellValue.ToString() == "ROJO")
            {
                e.Cell.Style.Add(HtmlTextWriterStyle.BackgroundColor, "Red");
                e.Cell.Style.Add(HtmlTextWriterStyle.Color, "White");

            }

            if (e.CellValue.ToString() == "VERDE")
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
}