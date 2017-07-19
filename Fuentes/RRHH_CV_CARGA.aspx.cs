using System;
using System.Drawing;
using System.IO;
using DevExpress.Web;
using DevExpress.Web.Internal;
using System.Data;
using System.Collections.Generic;

public partial class RRHH_CV_CARGA : System.Web.UI.Page
{
    const string UploadDirectory_Fotos = "~/Archivos/CV/Fotos/";
    const string UploadDirectory_Cvs = "~/Archivos/CV/CVs/";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("NRO",typeof(int));
            dt.PrimaryKey = new DataColumn[] { dt.Columns["NRO"] };
            dt.Columns.Add("TIPO");
            dt.Columns.Add("DESDE", typeof(DateTime));
            dt.Columns.Add("HASTA", typeof(DateTime));
            dt.Columns.Add("EMPRESA");
            dt.Columns.Add("FUNCIONES");
            dt.Columns.Add("REFERENCIA");

           

            Session.Add("Experiencia", dt);

        }
        Recargar();

       
    }

    protected void rbtl_postulacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            switch (rbtl_postulacion.SelectedItem.Value.ToString())
            {
                case "01":
                    {
                        txt_requisitos.InnerHtml = "* CURRICULUM VITAE<BR>";
                        txt_requisitos.InnerHtml += "* FOTOCOPIA DE DNI Y CUIL/CUIT<BR>";
                        txt_requisitos.InnerHtml += "* FOTOCOPIA CARNET DE VACUNACIÓN DOBLE VACTERIANA Y HEPATITIS B<BR>";
                        txt_requisitos.InnerHtml += "* FOTOCOPIA LIBRETA SANITARIA<BR>";
                        txt_requisitos.InnerHtml += "* CERTIFICACIÓN DE DOMICILIO (ORIGINAL TRAMITADO EN LA POLICIA)<BR>";
                        txt_requisitos.InnerHtml += "* ANTECEDENTES POLICIALES PROVINCIAL (TRAMITAR EN MINISTRO GONZALEZ ENTRE MEDOZA Y CORDOBA)<BR>";
                        txt_requisitos.InnerHtml += "* CONSTANCIA DE CBU DE CUENTA CAJA DE AHORRO HABILITADA DEL BANCO PROVICIA DEL NEUQUEN (SOLO SI TIENE)<BR>";
                        txt_requisitos.Focus();
                        break;
                    }
                case "02":
                    {
                        txt_requisitos.InnerHtml = "* CURRICULUM VITAE<BR>";
                        txt_requisitos.InnerHtml += "* FOTO CARNET (2)<BR>";
                        txt_requisitos.InnerHtml += "* FOTOCOPIA DE DNI Y CUIL/CUIT<BR>";
                        txt_requisitos.InnerHtml += "* FOTOCOPIA DNI (PERSONA MAYOR DE 21 AÑOS	PARA SEGURO DE VIDA)<BR>";
                        txt_requisitos.InnerHtml += "* FOTOCOPIA VACUNA ANTITETANICA<BR>";
                        txt_requisitos.InnerHtml += "* FOTOCOPIA DE TITULO SECUNDARIO Y ANALITICO<BR>";
                        txt_requisitos.InnerHtml += "* CERTIFICADO DE BUENA SALUD<BR>";
                        txt_requisitos.InnerHtml += "* CERTIFICADO DE DOMICILIO ACTUAL (ORIGINAL TRAMITADO EN LA POLICIA)<BR>";
                        txt_requisitos.InnerHtml += "* ANTECEDENTES POLICIALES PROVINCIAL (TRAMITAR EN MINISTRO GONZALEZ ENTRE MEDOZA Y CORDOBA)<BR>";
                        txt_requisitos.InnerHtml += "* CONSTANCIA DE CBU DE CUENTA CAJA DE AHORRO HABILITADA DEL BANCO PROVICIA DEL NEUQUEN (SOLO SI TIENE)<BR>";
                        txt_requisitos.Focus();
                        break;
                    }
                default:
                    {
                        txt_requisitos.InnerHtml = "";
                        break;
                    }
            }

           
        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
            FailureText2.Text = ex.Message;
        }
    }

    protected void Recargar()
    {
        try
        {


            ASPxGridView1.DataSource = (DataTable)Session["Experiencia"];
            ASPxGridView1.DataBind();
        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
            FailureText2.Text = ex.Message;
        }
    }
    protected void UploadControl_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        e.CallbackData = SavePostedFile(e.UploadedFile);
    }
    protected string SavePostedFile(UploadedFile uploadedFile)
    {
        if (!uploadedFile.IsValid)
            return string.Empty;
        string fileName = Path.ChangeExtension(Path.GetRandomFileName(), ".jpg");
        Session.Add("nombre_foto", fileName);
        string fullFileName = CombinePath(fileName);
        
        using (Image original = Image.FromStream(uploadedFile.FileContent))
        using (Image thumbnail = new ImageThumbnailCreator(original).CreateImageThumbnail(new Size(150, 150)))
            ImageUtils.SaveToJpeg((Bitmap)thumbnail, fullFileName);
        //UploadimgUtils.RemoveFileWithDelay(fileName, fullFileName, 5);
        return fileName;
    }
    protected string CombinePath(string fileName)
    {
        return Path.Combine(Server.MapPath("~/tmp/"), fileName);
    }
    protected void ASPxUploadControl1_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        if(e.UploadedFile.IsValid)
        {
            Session.Add("nombre_cv", e.UploadedFile.FileName);
            e.UploadedFile.SaveAs(Server.MapPath("~/tmp/") + e.UploadedFile.FileName);
        }
        
    }
    protected void ASPxGridView1_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        try
        {
            if (Session["Experiencia"] == null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("NRO", typeof(int));
                dt.PrimaryKey = new DataColumn[] { dt.Columns["NRO"] };
                dt.Columns.Add("TIPO");
                dt.Columns.Add("DESDE", typeof(DateTime));
                dt.Columns.Add("HASTA", typeof(DateTime));
                dt.Columns.Add("EMPRESA");
                dt.Columns.Add("FUNCIONES");
                dt.Columns.Add("REFERENCIA");

                Session.Add("Experiencia", dt);
                ASPxGridView1.DataSource = (DataTable)Session["Experiencia"];
                ASPxGridView1.DataBind();
            }
                e.NewValues.Add("NRO", (((DataTable)Session["Experiencia"]).Rows.Count + 1).ToString());
            


        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
            FailureText2.Text = ex.Message;
        }
    }
    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        try
        {
            ((DataTable)Session["Experiencia"]).Rows.RemoveAt(ASPxGridView1.FindVisibleIndexByKeyValue(e.Keys[ASPxGridView1.KeyFieldName]));

            Recargar();
            e.Cancel = true;
            ASPxGridView1.CancelEdit();
        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
            FailureText2.Text = ex.Message;
        }
    }
    protected void ASPxGridView1_Init(object sender, EventArgs e)
    {
        try
        {

            GridViewDataComboBoxColumn combo = ASPxGridView1.Columns["TIPO"] as GridViewDataComboBoxColumn;
            combo.PropertiesComboBox.Items.Add("ULTIMA", "01");
            combo.PropertiesComboBox.Items.Add("OTRA", "02");


        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
            FailureText2.Text = ex.Message;
        }
       
    }
    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        try
        {

            ((DataTable)Session["Experiencia"]).Rows.Add(e.NewValues["NRO"].ToString(), e.NewValues["TIPO"].ToString(), e.NewValues["DESDE"].ToString(), e.NewValues["HASTA"].ToString(), e.NewValues["EMPRESA"].ToString(), e.NewValues["FUNCIONES"].ToString(), e.NewValues["REFERENCIA"].ToString());

            Recargar();
            e.Cancel = true;
            ASPxGridView1.CancelEdit();
        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
            FailureText2.Text = ex.Message;
        }

    }
    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        try
        {
            DataRow row = ((DataTable)Session["Experiencia"]).Rows.Find(e.Keys["NRO"]);
            row["TIPO"] = e.NewValues["TIPO"];
            row["DESDE"] = e.NewValues["DESDE"];
            row["HASTA"] = e.NewValues["HASTA"];
            row["EMPRESA"] = e.NewValues["EMPRESA"];
            row["FUNCIONES"] = e.NewValues["FUNCIONES"];
            row["REFERENCIA"] = e.NewValues["REFERENCIA"];
           

            Recargar();
            e.Cancel = true;
            ASPxGridView1.CancelEdit();

        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
            FailureText2.Text = ex.Message;
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
                if (e.NewValues[dataColumn.FieldName] == null && dataColumn.FieldName != "NRO")
                {
                    e.Errors[dataColumn] = "Debe completar los casilleros";
                }
            }
            if (e.Errors.Count > 0) e.RowError = "Por favor, complete todos los datos";


            if (e.NewValues["TIPO"] != null && e.NewValues["TIPO"].ToString().Length < 1)
            {
                AddError(e.Errors, ASPxGridView1.Columns["TIPO"], "Cargue TIPO");
            }

            if (e.NewValues["DESDE"] != null && e.NewValues["DESDE"].ToString().Length < 1)
            {
                AddError(e.Errors, ASPxGridView1.Columns["DESDE"], "Cargue FECHA DESDE");
            }
            if (e.NewValues["HASTA"] != null && e.NewValues["HASTA"].ToString().Length < 1)
            {
                AddError(e.Errors, ASPxGridView1.Columns["HASTA"], "Cargue FECHA HASTA");
            }
            if (e.NewValues["EMPRESA"] != null && e.NewValues["EMPRESA"].ToString().Length < 1)
            {
                AddError(e.Errors, ASPxGridView1.Columns["EMPRESA"], "Cargue EMPRESA");
            }

            if (e.NewValues["FUNCIONES"] != null && e.NewValues["FUNCIONES"].ToString().Length < 1)
            {
                AddError(e.Errors, ASPxGridView1.Columns["FUNCIONES"], "Cargue FUNCIONESr");
            }

            if (e.NewValues["REFERENCIA"] != null && e.NewValues["REFERENCIA"].ToString().Length < 1)
            {
                AddError(e.Errors, ASPxGridView1.Columns["REFERENCIA"], "Cargue REFERENCIA");
            }

           
        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
            FailureText2.Text = ex.Message;
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
            FailureText2.Text = ex.Message;
        }
    }

    protected void btn_enviar_Click(object sender, EventArgs e)
    {
        try
        {
            if (rbtl_postulacion.SelectedIndex > -1)
            {
                FailureText.Text = "";
                FailureText2.Text = "";

                string b = Interfaz.EjecutarConsultaBD("CBS", "select b=COUNT(RHMCUH_NRODO2) from RHMCUH  WITH(nolock) WHERE RHMCUH_NRODO2='"+txt_dni.Text+"'").Rows[0]["b"].ToString();

                if (b == "0")
                {
                    string RHMCUH_NROCUR = Interfaz.EjecutarConsultaBD("CBS", "select ID=ISNULL(MAX(cast(RHMCUH_NROCUR as int)),0)+1 from RHMCUH WITH(nolock)").Rows[0]["ID"].ToString();

                    if (!Directory.Exists(Server.MapPath(UploadDirectory_Fotos + RHMCUH_NROCUR)))
                    {
                        Directory.CreateDirectory(Server.MapPath(UploadDirectory_Fotos + RHMCUH_NROCUR));
                    }

                    if (!Directory.Exists(Server.MapPath(UploadDirectory_Cvs + RHMCUH_NROCUR)))
                    {
                        Directory.CreateDirectory(Server.MapPath(UploadDirectory_Cvs + RHMCUH_NROCUR));
                    }

                    string servidor_foto = "";
                    if (Session["nombre_foto"] != null)
                    {
                        servidor_foto = @"\\192.168.1.141\Archivos\CV\Fotos\" + RHMCUH_NROCUR + @"\" + Session["nombre_foto"].ToString();
                    }
                    string servidor_cv = "";
                    if (Session["nombre_cv"] != null)
                    {
                        servidor_cv = @"\\192.168.1.141\Archivos\CV\CVs\" + RHMCUH_NROCUR + @"\" + Session["nombre_cv"].ToString();
                    }

                    Interfaz.Altacv(RHMCUH_NROCUR, txt_nombreyapellido.Text, txt_direccion.Text, "8300", txt_telefono.Text, txt_dni.Text, Convert.ToDateTime(txt_fechanacimiento.Text), rbtl_sexo.SelectedItem.Value.ToString(), servidor_foto, txt_edad.Text, txt_mail.Text, servidor_cv);
                    //POSTULACION
                    Interfaz.Altacv_Items(RHMCUH_NROCUR, "01", rbtl_postulacion.SelectedItem.Value.ToString(), "");

                    //EXPERIENCIA
                    if (Session["Experiencia"] != null && ((DataTable)Session["Experiencia"]).Rows.Count > 0)
                    {
                        string obs = "";
                        for (int i = 0; i < ((DataTable)Session["Experiencia"]).Rows.Count; i++)
                        {
                            obs = "EMPRESA:" + ((DataTable)Session["Experiencia"]).Rows[i]["EMPRESA"].ToString();
                            obs += " // FECHA DESDE:" + Convert.ToDateTime(((DataTable)Session["Experiencia"]).Rows[i]["DESDE"]).ToShortDateString();
                            obs += " // FECHA HASTA:" + Convert.ToDateTime(((DataTable)Session["Experiencia"]).Rows[i]["HASTA"]).ToShortDateString();
                            obs += " // FUNCIONES:" + ((DataTable)Session["Experiencia"]).Rows[i]["FUNCIONES"].ToString();
                            obs += " // REFERENCIA:" + ((DataTable)Session["Experiencia"]).Rows[i]["REFERENCIA"].ToString();
                            Interfaz.Altacv_Items(RHMCUH_NROCUR, "02", ((DataTable)Session["Experiencia"]).Rows[i]["TIPO"].ToString(), obs);
                        }
                    }

                    //FORMACION ACADEMICA
                    Interfaz.Altacv_Items(RHMCUH_NROCUR, "03", "01", rbtl_primario.SelectedItem.Value.ToString());
                    Interfaz.Altacv_Items(RHMCUH_NROCUR, "03", "02", rbtl_secundario.SelectedItem.Value.ToString());
                    Interfaz.Altacv_Items(RHMCUH_NROCUR, "03", "03", ASPxMemo1.Text);
                    Interfaz.Altacv_Items(RHMCUH_NROCUR, "03", "04", rbtl_ingles.SelectedItem.Value.ToString());
                    Interfaz.Altacv_Items(RHMCUH_NROCUR, "03", "05", rbtl_frances.SelectedItem.Value.ToString());
                    Interfaz.Altacv_Items(RHMCUH_NROCUR, "03", "06", rbtl_portugues.SelectedItem.Value.ToString());
                    Interfaz.Altacv_Items(RHMCUH_NROCUR, "03", "07", rbtl_word.SelectedItem.Value.ToString());
                    Interfaz.Altacv_Items(RHMCUH_NROCUR, "03", "08", rbtl_excel.SelectedItem.Value.ToString());
                    Interfaz.Altacv_Items(RHMCUH_NROCUR, "03", "09", rbtl_internet.SelectedItem.Value.ToString());
                    Interfaz.Altacv_Items(RHMCUH_NROCUR, "03", "10", txt_otros_conocimientos.Text);

                    string foto = "";
                    if (Session["nombre_foto"] != null)
                    {
                        foto = Server.MapPath(UploadDirectory_Fotos + RHMCUH_NROCUR + "/" + Session["nombre_foto"].ToString());
                        File.Move(Server.MapPath("~/tmp/" + Session["nombre_foto"].ToString()), foto);
                    }
                    string archivo_cv = "";
                    if (Session["nombre_cv"] != null)
                    {
                        archivo_cv = Server.MapPath(UploadDirectory_Cvs + RHMCUH_NROCUR + "/" + Session["nombre_cv"].ToString());
                        File.Move(Server.MapPath("~/tmp/" + Session["nombre_cv"].ToString()), archivo_cv);
                    }



                    FailureText.Text = "Informacion enviada con éxito.";
                    FailureText2.Text = "Informacion enviada con éxito.";


                    LimpiarPantalla();
                }
                else
                {
                    FailureText.Text = "Ya existe un CV cargado con ese DNI.";
                    FailureText2.Text = "Ya existe un CV cargado con ese DNI.";
                    txt_dni.Focus();
                }
            }
            else
            {
                FailureText.Text = "Seleccione para que sector se postula.";
                FailureText2.Text = "Seleccione para que sector se postula.";
                rbtl_postulacion.Focus();
            }
        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
            FailureText2.Text = ex.Message;
        }
    }

    private void LimpiarPantalla()
    {
        try
        {
            txt_nombreyapellido.Text = "";
            txt_direccion.Text = "";
            //txt_codigopostal.Text = "";
            txt_telefono.Text = "";
            txt_dni.Text = "";
            txt_fechanacimiento.Text = "";
            rbtl_sexo.Items[0].Selected = true;
            txt_edad.Text = "";
            txt_mail.Text = "";
            rbtl_postulacion.Items[0].Selected = true;
            rbtl_primario.Items[0].Selected = true;
            rbtl_secundario.Items[0].Selected = true;
            ASPxMemo1.Text = "";
            rbtl_ingles.Items[0].Selected = true;
            rbtl_frances.Items[0].Selected = true;
            rbtl_portugues.Items[0].Selected = true;
            rbtl_word.Items[0].Selected = true;
            rbtl_excel.Items[0].Selected = true;
            rbtl_internet.Items[0].Selected = true;
            txt_otros_conocimientos.Text = "";
		txt_requisitos.InnerHtml = "";

            Session.Clear();
            Session.Abandon();
            Recargar();

        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
            FailureText2.Text = ex.Message;
        }
    }
}