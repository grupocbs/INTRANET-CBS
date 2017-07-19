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
using System.Web.UI.HtmlControls;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text;

public partial class RRHH_CONTRATOS : System.Web.UI.Page
{

    private static List<String> Variables;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
           // Session.Add("usuario", "1");
           if (Session["usuario"] == null)
            {

                Response.Redirect("INTRANET_LOGIN.aspx");
            }
            if (!IsPostBack)
            {
                cmb_empresa.Items.Add(new ListEditItem("SELECCIONE", "SELECCIONE"));
                cmb_empresa.Items.Add(new ListEditItem("BARCELO", "BARCELO"));
                cmb_empresa.Items.Add(new ListEditItem("CASA DE LA COSTA", "CASA DE LA COSTA"));
                cmb_empresa.Items.Add(new ListEditItem("CBS SRL", "CBS SRL"));
                cmb_empresa.SelectedIndex = 0;
              
               Variables = new List<string>();
               
            }
            Tabla_Variables();


        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }
    }

    private void Tabla_Variables()
    {
        if (cmb_contratos.SelectedIndex > -1 && cmb_contratos.SelectedItem.Text != "SELECCIONE" && Variables.Count>0)
        {

            HtmlGenericControl tabla = new HtmlGenericControl("table");
            Panel_variables.Controls.Clear();

            for (int i = 0; i < Variables.Count; i++)
            {
                if (!Variables[i].Contains("_TEXTO") && !Variables[i].Contains("_+")) //genero todos los controles que no sean de copia
                {

                    HtmlGenericControl tr = new HtmlGenericControl("tr");
                    HtmlGenericControl td = new HtmlGenericControl("td");
                    td.Attributes.Add("style", "padding: 2px;");
                    HtmlGenericControl td1 = new HtmlGenericControl("td");
                    td1.Attributes.Add("style", "padding: 2px;");

                    ASPxLabel v = new ASPxLabel();
                    v.ID = "lbl_" + Variables[i];
                    v.Text = Variables[i];
                    td.Controls.Add(v);
                    tr.Controls.Add(td);



                    if (Variables[i].Contains("FECHA"))
                    {

                        ASPxDateEdit vf1 = new ASPxDateEdit();
                        vf1.UseMaskBehavior = true;
                        vf1.ID = "txt_" + Variables[i];
                        vf1.Width = Unit.Pixel(120);
                        vf1.MinDate = Convert.ToDateTime("01/01/1900");
                        vf1.EditFormatString = "dd/MM/yyyy";
                        vf1.DisplayFormatString = "dd/MM/yyyy";
                        vf1.ViewStateMode = System.Web.UI.ViewStateMode.Enabled;
                        vf1.ValidationSettings.EnableCustomValidation = true;
                        vf1.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.Text;
                        vf1.ValidationSettings.ErrorTextPosition = ErrorTextPosition.Bottom;
                        vf1.ValidationSettings.SetFocusOnError = true;
                        vf1.ValidationSettings.ErrorFrameStyle.Font.Size = FontUnit.Smaller;
                        vf1.ValidationSettings.RequiredField.IsRequired = true;
                        vf1.ValidationSettings.RequiredField.ErrorText = "Requerido";
                        td1.Controls.Add(vf1);
                        tr.Controls.Add(td1);
                        tabla.Controls.Add(tr);


                    }
                    else
                    {
                        if (Variables[i].Contains("SELECCIONE_"))
                        {

                            ASPxComboBox vd1 = new ASPxComboBox();
                            vd1.ID = "txt_" + Variables[i];
                            vd1.Width = Unit.Pixel(300);
                            vd1.ViewStateMode = System.Web.UI.ViewStateMode.Enabled;
                            vd1.EnableCallbackMode = true;
                            vd1.IncrementalFilteringMode = IncrementalFilteringMode.StartsWith;
                            vd1.ValidationSettings.EnableCustomValidation = true;
                            vd1.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.Text;
                            vd1.ValidationSettings.ErrorTextPosition = ErrorTextPosition.Bottom;
                            vd1.ValidationSettings.SetFocusOnError = true;
                            vd1.ValidationSettings.ErrorFrameStyle.Font.Size = FontUnit.Smaller;
                            vd1.ValidationSettings.RequiredField.IsRequired = true;
                            vd1.ValidationSettings.RequiredField.ErrorText = "Requerido";

                            switch (Variables[i].Replace("SELECCIONE_", ""))
                            {
                                case "OBJETIVO":
                                    {
                                        //string sql = "select USR_CLIOBJ_CODOBJ,USR_CLIOBJ_OBJDSC from USR_CLIOBJ with(nolock) order by USR_CLIOBJ_OBJDSC";
                                        //DataTable dt = Interfaz.EjecutarConsultaBD("CBS", sql);
                                        //if (dt.Rows.Count > 0)
                                        //{
                                        //    vd1.Items.Add(new ListEditItem("VARIOS", "VARIOS"));

                                        //    foreach (DataRow dr in dt.Rows)
                                        //    {
                                        //        vd1.Items.Add(new ListEditItem(dr["USR_CLIOBJ_OBJDSC"].ToString(), dr["USR_CLIOBJ_OBJDSC"].ToString()));
                                        //    }


                                        //}
                                        break;
                                    }
                                case "CLIENTE":
                                    {
                                        vd1.Items.Add(new ListEditItem("VARIOS", "VARIOS"));
                                        string sql = "select VTMCLH_NROCTA,VTMCLH_NOMBRE from VTMCLH with (nolock)  order by VTMCLH_NOMBRE";
                                        DataTable dt = Interfaz.EjecutarConsultaBD("CBS", sql);
                                        if (dt.Rows.Count > 0)
                                        {
                                         
                                            foreach (DataRow dr in dt.Rows)
                                            {
                                                vd1.Items.Add(new ListEditItem(dr["VTMCLH_NOMBRE"].ToString(), dr["VTMCLH_NROCTA"].ToString()));
                                            }
					 vd1.Items.Add(new ListEditItem("BASE", "BASE"));
                                            vd1.AutoPostBack = true;
                                            vd1.SelectedIndexChanged += new EventHandler(vd1_SelectedIndexChanged);
                                        }


                                        break;
                                    }
                                case "CATEGORIA":
                                    {
                                        string sql = "SELECT SJTCAT_DESCRP FROM SJTCAT WITH(NOLOCK) GROUP BY SJTCAT_DESCRP  order by SJTCAT_DESCRP";
                                        DataTable dt = Interfaz.EjecutarConsultaBD("CBS", sql);
                                        if (dt.Rows.Count > 0)
                                        {

                                            foreach (DataRow dr in dt.Rows)
                                            {
                                                vd1.Items.Add(new ListEditItem(dr["SJTCAT_DESCRP"].ToString(), dr["SJTCAT_DESCRP"].ToString()));
                                            }
                                        }

                                        break;
                                    }
                                case "CCT":
                                    {
                                        string sql = "select T=USR_SJTCON_DESCRP  from USR_SJTCON with (nolock)   order by USR_SJTCON_DESCRP";
                                        DataTable dt = Interfaz.EjecutarConsultaBD("CBS", sql);
                                        if (dt.Rows.Count > 0)
                                        {

                                            foreach (DataRow dr in dt.Rows)
                                            {
                                                vd1.Items.Add(new ListEditItem(dr["T"].ToString(), dr["T"].ToString()));
                                            }

                                        }
                                        break;
                                    }
                                case "SERVICIO":
                                    {
                                        string sql = "select USR_CTRCST_DESCRP from USR_CTRCST with (nolock) order by USR_CTRCST_DESCRP";
                                        DataTable dt = Interfaz.EjecutarConsultaBD("CBS", sql);
                                        if (dt.Rows.Count > 0)
                                        {

                                            foreach (DataRow dr in dt.Rows)
                                            {
                                                vd1.Items.Add(new ListEditItem(dr["USR_CTRCST_DESCRP"].ToString().ToUpper(), dr["USR_CTRCST_DESCRP"].ToString().ToUpper()));
                                            }

                                        }
                                        break;
                                    }


                            }

                            td1.Controls.Add(vd1);
                            tr.Controls.Add(td1);
                            tabla.Controls.Add(tr);
                        }
                        else
                        {

                            ASPxTextBox v1 = new ASPxTextBox();
                            v1.ID = "txt_" + Variables[i];
                            v1.Width = Unit.Pixel(300);
                            v1.ViewStateMode = System.Web.UI.ViewStateMode.Enabled;
                            v1.ValidationSettings.EnableCustomValidation = true;
                            v1.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.Text;
                            v1.ValidationSettings.ErrorTextPosition = ErrorTextPosition.Bottom;
                            v1.ValidationSettings.SetFocusOnError = true;
                            v1.ValidationSettings.ErrorFrameStyle.Font.Size = FontUnit.Smaller;
                            v1.ValidationSettings.RequiredField.IsRequired = true;
                            v1.ValidationSettings.RequiredField.ErrorText = "Requerido";
                            td1.Controls.Add(v1);
                            tr.Controls.Add(td1);
                            tabla.Controls.Add(tr);
                        }
                    }
                }

            }

            Panel_variables.Controls.Add(tabla);
        }
    }

    protected void vd1_SelectedIndexChanged(object sender, EventArgs e)
    {
         if(((ASPxComboBox)sender).ID=="txt_SELECCIONE_CLIENTE" && ((ASPxComboBox)sender).SelectedIndex>-1)
         {
            Control c = FindControl("ctl00$MainContent$formLayout$txt_SELECCIONE_OBJETIVO");
            if (c != null)
            {
                ((ASPxComboBox)c).Items.Clear();
                string sql = "select USR_CLIOBJ_CODOBJ,USR_CLIOBJ_OBJDSC from USR_CLIOBJ with(nolock) where USR_CLIOBJ_CODCLI='" + ((ASPxComboBox)sender).SelectedItem.Value.ToString() + "' order by USR_CLIOBJ_OBJDSC";
                DataTable dt = Interfaz.EjecutarConsultaBD("CBS", sql);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ((ASPxComboBox)c).Items.Add(new ListEditItem(dr["USR_CLIOBJ_OBJDSC"].ToString(), dr["USR_CLIOBJ_OBJDSC"].ToString()));
                    }
                }
            }
         }
    }
    protected void cmb_empresa_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            Variables.Clear();
            txt_descripcion.InnerHtml = "";
            FailureText.Text = "";
            ASPxButton1.Visible = false;
            Panel_variables.Controls.Clear();
            chk_adicionales.UnselectAll();
            if (File.Exists(Server.MapPath("tmp/Documento"+ Session["usuario"].ToString()+ ".pdf")))
            {
                File.Delete(Server.MapPath("tmp/Documento" + Session["usuario"].ToString() + ".pdf"));
            }
            if (File.Exists(Server.MapPath("tmp/DocumentoImprimir" + Session["usuario"].ToString() + ".pdf")))
            {
                File.Delete(Server.MapPath("tmp/DocumentoImprimir" + Session["usuario"].ToString() + ".pdf"));
            }


            if (cmb_empresa.SelectedItem.Text != "SELECCIONE")
            {
                cargar();
            }
            else
            {
                 
                

                cmb_contratos.Items.Clear();
                cmb_contratos.Text = "";
            }

        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }

    }
    private void cargar()
    {


        cmb_contratos.Items.Clear();
        DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", "SELECT ID, NOMBRE FROM RRHH_TIPOS_CONTRATOS with(nolock) WHERE EMPRESA='" + cmb_empresa .SelectedItem.Value.ToString()+ "' order by NOMBRE");
        if (dt.Rows.Count > 0)
        {
            cmb_contratos.Items.Add(new ListEditItem("SELECCIONE", "SELECCIONE"));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmb_contratos.Items.Add(new ListEditItem(dt.Rows[i]["NOMBRE"].ToString(), dt.Rows[i]["ID"].ToString()));

            }
            cmb_contratos.SelectedIndex = 0;
        }



    }
    protected void cmb_contratos_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            Variables.Clear();
            txt_descripcion.InnerHtml = "";
            FailureText.Text = "";
            ASPxButton1.Visible = false;
            chk_adicionales.UnselectAll();
            chk_adicionales.Items.Clear();

            if (File.Exists(Server.MapPath("tmp/Documento" + Session["usuario"].ToString() + ".pdf")))
            {
                File.Delete(Server.MapPath("tmp/Documento" + Session["usuario"].ToString() + ".pdf"));
            }
            if (File.Exists(Server.MapPath("tmp/DocumentoImprimir" + Session["usuario"].ToString() + ".pdf")))
            {
                File.Delete(Server.MapPath("tmp/DocumentoImprimir" + Session["usuario"].ToString() + ".pdf"));
            }

            if (cmb_contratos.SelectedIndex > -1 && cmb_contratos.SelectedItem.Text != "SELECCIONE")
            {
   
                string sql = "select * from RRHH_TIPOS_CONTRATOS with(nolock) where id=" + cmb_contratos.SelectedItem.Value.ToString() + " ORDER BY nombre";
                DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql);
                if (dt.Rows.Count > 0)
                {

                    Adicionales(dt.Rows[0]["id"].ToString());
                  

                    int inicio=0;
                    string t = dt.Rows[0]["DESCRIPCION"].ToString();
                    string variable = "";

                    for (int i = 0; i < t.Length; i++)
                    {
                        if (t.Substring(i, 1) == "[")
                        {
                            inicio = i;
                           
                        }

                        if (t.Substring(i, 1) == "]" && inicio>0)
                        {
                            variable = t.Substring(inicio, i - inicio + 1).Replace("[", "").Replace("]", "");
                            if (!Variables.Contains(variable))
                            {
                                Variables.Add(variable);
                            }
                           
                            inicio = 0;
                        }
                    }


                    Tabla_Variables();
                   


                }

            }

        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }

    }
    private void Adicionales(string id)
    {
        chk_adicionales.Items.Clear();
        DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", "SELECT ARCHIVO, NOMBRE FROM RRHH_ADIC_CONTRATOS with(nolock) WHERE ID_TIPO_CONTRATO=" + id + " order by NOMBRE");

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            chk_adicionales.Items.Add(new ListEditItem(dt.Rows[i]["NOMBRE"].ToString(), dt.Rows[i]["ARCHIVO"].ToString()));

        }
        chk_adicionales.UnselectAll();
    }
   
    protected void btn_actualizar_Click(object sender, EventArgs e)
    {
        try
        {

            if (cmb_contratos.SelectedIndex > -1 && cmb_contratos.SelectedItem.Text != "SELECCIONE" && Variables.Count>0)
            {
                string sql = "select * from RRHH_TIPOS_CONTRATOS with(nolock) where id=" + cmb_contratos.SelectedItem.Value.ToString();
                DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql);
                if (dt.Rows.Count > 0)
                {

                    txt_descripcion.InnerHtml = dt.Rows[0]["DESCRIPCION"].ToString();
                }

            }


            string v = "";
            for (int i = 0; i < Variables.Count; i++)
            {
                v = Variables[i];
                //if (Variables[i].Contains("_TEXTO") || Variables[i].Contains("_+90_DIAS"))
                if (Variables[i].Contains("_TEXTO") || Variables[i].Contains("_+"))
                {
                    if (Variables[i].Contains("_TEXTO"))
                    {
                        v = Variables[i].Replace("_TEXTO", "");
                    }
                    else
                    {
                        v = Variables[i].Substring(0, Variables[i].IndexOf("_+"));
                    }

                    //v = Variables[i].Replace("_TEXTO", "").Replace("_+90_DIAS", "");
                    
                }


                if (FindControl("ctl00$MainContent$formLayout$txt_" + v) != null)
                {
                    Control c = FindControl("ctl00$MainContent$formLayout$txt_" + v);
                    if (v.Contains("FECHA"))
                    {


                        if (Variables[i].Contains("_TEXTO"))
                        {
                            if (((ASPxDateEdit)c).Text.Length > 0)
                            {
                                string fecha_texto = String.Format("{0:dd' de 'MMMM' de 'yyyy}", Convert.ToDateTime(((ASPxDateEdit)c).Text));
                                txt_descripcion.InnerHtml = txt_descripcion.InnerHtml.Replace("[" + Variables[i] + "]", fecha_texto);
                            }
                        }
                        else
                        {
                            if (Variables[i].Contains("_+"))
                            {
                                if (((ASPxDateEdit)c).Text.Length > 0)
                                {
                                    string fecha_mas_dias = Convert.ToDateTime(((ASPxDateEdit)c).Text).AddDays(Convert.ToInt32(Variables[i].Replace("FECHA_+", "").Substring(0, 2))).ToString("dd/MM/yyyy");
                                    txt_descripcion.InnerHtml = txt_descripcion.InnerHtml.Replace("[" + Variables[i] + "]", fecha_mas_dias);
                                }
                            }
                            else
                            {
                                txt_descripcion.InnerHtml = txt_descripcion.InnerHtml.Replace("[" + v + "]", ((ASPxDateEdit)c).Text);
                            }
                        }


                    }
                    else
                    {
                        if (v.Contains("SELECCIONE"))
                        {

                            txt_descripcion.InnerHtml = txt_descripcion.InnerHtml.Replace("[" + v + "]", ((ASPxComboBox)c).Text);

                        }
                        else
                        {

                            txt_descripcion.InnerHtml = txt_descripcion.InnerHtml.Replace("[" + v + "]", ((ASPxTextBox)c).Text.ToUpper());

                        }
                    }
                }

            }

            try
            {

                List<string> Lista = new List<string>();
                bool b = false;
                for (int i = 0; i < chk_adicionales.Items.Count; i++)
                {
                    if (chk_adicionales.Items[i].Selected)
                    {
                        b = true;
                        Lista.Add(Server.MapPath(chk_adicionales.Items[i].Value.ToString()));
                    }

                }
                if (b)
                {
                    if (File.Exists(Server.MapPath("tmp/Documento" + Session["usuario"].ToString() + ".pdf")))
                    {
                        File.Delete(Server.MapPath("tmp/Documento" + Session["usuario"].ToString() + ".pdf"));
                    }
                    if (File.Exists(Server.MapPath("tmp/DocumentoImprimir" + Session["usuario"].ToString() + ".pdf")))
                    {
                        File.Delete(Server.MapPath("tmp/DocumentoImprimir" + Session["usuario"].ToString() + ".pdf"));
                    }

                    string sFileJoin = Server.MapPath("tmp/Documento" + Session["usuario"].ToString() + ".pdf");
                    Document Doc = new Document();
                    FileStream fs = new FileStream(sFileJoin, FileMode.Create, FileAccess.Write, FileShare.None);
                    PdfCopy copy = new PdfCopy(Doc, fs);
                    Doc.Open();
                    PdfReader Rd = default(PdfReader);
                    int n = 0;

                    foreach (string file in Lista)
                    {
                        Rd = new PdfReader(file);
                        n = Rd.NumberOfPages;
                        int page = 0;
                        while (page < n)
                        {
                            page += 1;
                            copy.AddPage(copy.GetImportedPage(Rd, page));
                        }
                        copy.FreeReader(Rd);
                        Rd.Close();
                    }
                    Doc.Close();
                    fs.Close();
                    copy.Close();

                    string Copia = Server.MapPath("tmp/DocumentoImprimir" + Session["usuario"].ToString() + ".pdf");
                    PdfReader reader = new PdfReader(sFileJoin);
                    PdfStamper stamper = new PdfStamper(reader, new FileStream(Copia, FileMode.Create));
                    AcroFields fields = stamper.AcroFields;
                    stamper.JavaScript = "this.print(true);\r";
                    stamper.FormFlattening = true;
                    stamper.Close();
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                FailureText.Text += ex.Message;
            }





            if (Session["IMP_CONTRATO"] != null)
            {

                Session["IMP_CONTRATO"] = txt_descripcion.InnerHtml;
            }
            else
            {
                Session.Add("IMP_CONTRATO", txt_descripcion.InnerHtml);
            }

            ASPxButton1.Visible = true;


        }
        catch (Exception ex)
        {
            FailureText.Text += ex.Message;
        }

    }

    private void limpiar()
    {
        txt_descripcion.InnerHtml = "";
        FailureText.Text = "";
        cmb_contratos.Items.Clear();
        cmb_contratos.Text = "";
        cmb_empresa.SelectedIndex = 0;
        Variables.Clear();
        ASPxButton1.Visible = false;
       
    }


   
}