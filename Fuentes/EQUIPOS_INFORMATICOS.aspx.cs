using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;


public partial class EQUIPOS_INFORMATICOS: System.Web.UI.Page
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
            ASPxGridView1.DataSource = Interfaz.EjecutarConsultaBD("LocalSqlServer", "SELECT * FROM EQUIPOS_INFORMATICOS ORDER BY IDUSUARIO");
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
            Interfaz.Eliminar_EI(e.Keys[0].ToString());
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
            GridViewDataComboBoxColumn combo1 = ASPxGridView1.Columns["IDUSUARIO"] as GridViewDataComboBoxColumn;
            foreach (DataRow dr in Interfaz.EjecutarConsultaBD("LocalSqlServer", "SELECT ID=NOMBRE,DESCRP=NOMBRE FROM USUARIOS WITH(NOLOCK) ORDER By NOMBRE ").Rows)
            {
                combo1.PropertiesComboBox.Items.Add(new ListEditItem(dr["DESCRP"].ToString(), dr["ID"].ToString()));

            }


            GridViewDataComboBoxColumn combo = ASPxGridView1.Columns["TIPO"] as GridViewDataComboBoxColumn;
            combo.PropertiesComboBox.Items.Add("PC ESCRITORIO", "PC ESCRITORIO");
            combo.PropertiesComboBox.Items.Add("NOTEBOOK", "NOTEBOOK");
            combo.PropertiesComboBox.Items.Add("IMPRESORA", "IMPRESORA");


            GridViewDataComboBoxColumn combo2 = ASPxGridView1.Columns["SISTEMA_OPERATIVO"] as GridViewDataComboBoxColumn;
            combo2.PropertiesComboBox.Items.Add("WINDOWS 7  ULTIMATE", "WINDOWS 7  ULTIMATE");
            combo2.PropertiesComboBox.Items.Add("WINDOWS 7  PROFESIONAL", "WINDOWS 7  PROFESIONAL");
            combo2.PropertiesComboBox.Items.Add("WINDOWS 10  PROFESIONAL", "WINDOWS 10  PROFESIONAL");
            combo2.PropertiesComboBox.Items.Add("WINDOWS SERVER 2008 STANDARD", "WINDOWS SERVER 2008 STANDARD");
            combo2.PropertiesComboBox.Items.Add("WINDOWS SERVER 2008 ENTERPRISE", "WINDOWS SERVER 2008 ENTERPRISE");


            GridViewDataComboBoxColumn combo3 = ASPxGridView1.Columns["ANTIVIRUS"] as GridViewDataComboBoxColumn;
            combo3.PropertiesComboBox.Items.Add("ESET Endpoint Antivirus 6.4.2014.2", "ESET Endpoint Antivirus 6.4.2014.2");
            combo3.PropertiesComboBox.Items.Add("ESET File Security 6.3.12010.0", "ESET File Security 6.3.12010.0");
            combo3.PropertiesComboBox.Items.Add("Windows Defender", "Windows Defender");
          



            GridViewDataComboBoxColumn combo4 = ASPxGridView1.Columns["OFFICE"] as GridViewDataComboBoxColumn;
            combo4.PropertiesComboBox.Items.Add("NO UTILIZA", "NO UTILIZA");
            combo4.PropertiesComboBox.Items.Add("OPEN OFFICE", "OPEN OFFICE");
            combo4.PropertiesComboBox.Items.Add("LIBRE OFFICE", "LIBRE OFFICE");
            combo4.PropertiesComboBox.Items.Add("OFFICE 2007", "OFFICE 2007");
            combo4.PropertiesComboBox.Items.Add("OFFICE 2010", "OFFICE 2010");

            GridViewDataComboBoxColumn combo5 = ASPxGridView1.Columns["SWITCH"] as GridViewDataComboBoxColumn;
            combo5.PropertiesComboBox.Items.Add("HP 1920G Switch .154", "HP 1920G Switch .154");
            combo5.PropertiesComboBox.Items.Add("HP 1920G Switch .203", "HP 1920G Switch .203");
            
          
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
            string IDUSUARIO = ((e.NewValues["IDUSUARIO"] != null) ? e.NewValues["IDUSUARIO"].ToString() : "");
            string IP = ((e.NewValues["IP"] != null) ? e.NewValues["IP"].ToString() : "");
            string TIPO = ((e.NewValues["TIPO"] != null) ? e.NewValues["TIPO"].ToString() : "");
            string NOMBRE_EQUIPO = ((e.NewValues["NOMBRE_EQUIPO"] != null) ? e.NewValues["NOMBRE_EQUIPO"].ToString() : "");
            string DESCRIPCION = ((e.NewValues["DESCRIPCION"] != null) ? e.NewValues["DESCRIPCION"].ToString() : "");
            string USUARIO_SESION = ((e.NewValues["USUARIO_SESION"] != null) ? e.NewValues["USUARIO_SESION"].ToString() : "");
            string CONTRASEÑA_SESION = ((e.NewValues["CONTRASEÑA_SESION"] != null) ? e.NewValues["CONTRASEÑA_SESION"].ToString() : "");
            string RAM = ((e.NewValues["RAM"] != null) ? e.NewValues["RAM"].ToString() : "");
            string SISTEMA_OPERATIVO = ((e.NewValues["SISTEMA_OPERATIVO"] != null) ? e.NewValues["SISTEMA_OPERATIVO"].ToString() : "");
            string LICENCIA_SO = ((e.NewValues["LICENCIA_SO"] != null) ? e.NewValues["LICENCIA_SO"].ToString() : "");
            string ANTIVIRUS = ((e.NewValues["ANTIVIRUS"] != null) ? e.NewValues["ANTIVIRUS"].ToString() : "");
            string OFFICE = ((e.NewValues["OFFICE"] != null) ? e.NewValues["OFFICE"].ToString() : "");
            string LICENCIA_OFFICE = ((e.NewValues["LICENCIA_OFFICE"] != null) ? e.NewValues["LICENCIA_OFFICE"].ToString() : "");
            string IDTEAMVIEWER = ((e.NewValues["IDTEAMVIEWER"] != null) ? e.NewValues["IDTEAMVIEWER"].ToString() : "");
            string SWITCH = ((e.NewValues["SWITCH"] != null) ? e.NewValues["SWITCH"].ToString() : "");
            string BOCA = ((e.NewValues["BOCA"] != null) ? e.NewValues["BOCA"].ToString() : "");

            Interfaz.Agregar_EI(IDUSUARIO, IP, TIPO, NOMBRE_EQUIPO, DESCRIPCION, USUARIO_SESION, CONTRASEÑA_SESION, RAM, SISTEMA_OPERATIVO, LICENCIA_SO, ANTIVIRUS, OFFICE, LICENCIA_OFFICE, IDTEAMVIEWER, SWITCH, BOCA);
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

            string IDUSUARIO = ((e.NewValues["IDUSUARIO"] != null) ? e.NewValues["IDUSUARIO"].ToString() : "");
            string IP = ((e.NewValues["IP"] != null) ? e.NewValues["IP"].ToString() : "");
            string TIPO = ((e.NewValues["TIPO"] != null) ? e.NewValues["TIPO"].ToString() : "");
            string NOMBRE_EQUIPO = ((e.NewValues["NOMBRE_EQUIPO"] != null) ? e.NewValues["NOMBRE_EQUIPO"].ToString() : "");
            string DESCRIPCION = ((e.NewValues["DESCRIPCION"] != null) ? e.NewValues["DESCRIPCION"].ToString() : "");
            string USUARIO_SESION = ((e.NewValues["USUARIO_SESION"] != null) ? e.NewValues["USUARIO_SESION"].ToString() : "");
            string CONTRASEÑA_SESION = ((e.NewValues["CONTRASEÑA_SESION"] != null) ? e.NewValues["CONTRASEÑA_SESION"].ToString() : "");
            string RAM = ((e.NewValues["RAM"] != null) ? e.NewValues["RAM"].ToString() : "");
            string SISTEMA_OPERATIVO = ((e.NewValues["SISTEMA_OPERATIVO"] != null) ? e.NewValues["SISTEMA_OPERATIVO"].ToString() : "");
            string LICENCIA_SO = ((e.NewValues["LICENCIA_SO"] != null) ? e.NewValues["LICENCIA_SO"].ToString() : "");
            string ANTIVIRUS = ((e.NewValues["ANTIVIRUS"] != null) ? e.NewValues["ANTIVIRUS"].ToString() : "");
            string OFFICE = ((e.NewValues["OFFICE"] != null) ? e.NewValues["OFFICE"].ToString() : "");
            string LICENCIA_OFFICE = ((e.NewValues["LICENCIA_OFFICE"] != null) ? e.NewValues["LICENCIA_OFFICE"].ToString() : "");
            string IDTEAMVIEWER = ((e.NewValues["IDTEAMVIEWER"] != null) ? e.NewValues["IDTEAMVIEWER"].ToString() : "");
            string SWITCH = ((e.NewValues["SWITCH"] != null) ? e.NewValues["SWITCH"].ToString() : "");
            string BOCA = ((e.NewValues["BOCA"] != null) ? e.NewValues["BOCA"].ToString() : "");

            Interfaz.Editar_EI(e.Keys[0].ToString(),IDUSUARIO,IP,TIPO,NOMBRE_EQUIPO,DESCRIPCION,USUARIO_SESION,CONTRASEÑA_SESION,RAM,SISTEMA_OPERATIVO,LICENCIA_SO,ANTIVIRUS,OFFICE,LICENCIA_OFFICE,IDTEAMVIEWER,SWITCH,BOCA);     
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
            /*foreach (GridViewColumn column in ASPxGridView1.Columns)
            {
                GridViewDataColumn dataColumn = column as GridViewDataColumn;
                if (dataColumn == null) continue;
                if (e.NewValues[dataColumn.FieldName] == null && dataColumn.FieldName != "TELEFONO" && dataColumn.FieldName != "Servicio" && dataColumn.FieldName != "Imei" && dataColumn.FieldName != "Supervisor")
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

       
            
            if (string.IsNullOrEmpty(e.RowError) && e.Errors.Count > 0) e.RowError = "Por favor verifique los errores";

            string sql = "SELECT cant=count(*) ";
            sql += " FROM Usuarios  with (nolock) ";
            sql += " WHERE usuario='" + e.NewValues["Usuario"].ToString() + "'";

            DataTable dt = Interfaz.EjecutarConsultaBD("intranet", sql);

            if (Convert.ToInt32(dt.Rows[0]["cant"].ToString()) > 1)
            {
                AddError(e.Errors, ASPxGridView1.Columns["Usuario"], "Cargue un Usuario Distinto");
                e.RowError = "Este Usuario ya existe";
            }*/
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