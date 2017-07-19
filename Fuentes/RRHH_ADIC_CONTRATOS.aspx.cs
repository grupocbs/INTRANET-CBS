using System;
using System.Drawing;
using System.IO;
using DevExpress.Web;
using System.Data;
using System.Collections.Generic;
using System.Web;

public partial class RRHH_ADIC_CONTRATOS : System.Web.UI.Page
{

    const string UploadDirectory = "Archivos/CONTRATOS/ADICIONALES";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Session.Add("usuario", "1");
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

                cmb_nombre.Items.Add(new ListEditItem("SELECCIONE", "SELECCIONE"));
                //cmb_nombre.Items.Add(new ListEditItem("DECLARACION JURADA DE DOMICILIO LEGAL", "DECLARACION JURADA DE DOMICILIO LEGAL"));
                //cmb_nombre.Items.Add(new ListEditItem("MEMORANDUM 63/06 - 154/10 - 183/14", "MEMORANDUM 63/06 - 154/10 - 183/14"));
                //cmb_nombre.Items.Add(new ListEditItem("REGLAMENTO INTERNO MAESTRANZA", "REGLAMENTO INTERNO MAESTRANZA"));
               // cmb_nombre.Items.Add(new ListEditItem("REGLAMENTO INTERNO SEGURIDAD", "REGLAMENTO INTERNO SEGURIDAD"));
              //  cmb_nombre.Items.Add(new ListEditItem("SOLICITUD DE EMPLEO", "SOLICITUD DE EMPLEO"));
		//cmb_nombre.Items.Add(new ListEditItem("CONVENIO 389-04 CDLC", "CONVENIO 389-04 CDLC"));
		//cmb_nombre.Items.Add(new ListEditItem("DECLARACION JURADA DE DOMICILIO LEGAL CDLC", "DECLARACION JURADA DE DOMICILIO LEGAL CDLC"));
		//cmb_nombre.Items.Add(new ListEditItem("MEMORANDUM 63/06 - 15/10 - 183/14 CDLC", "MEMORANDUM 63/06 - 15/10 - 183/14 CDLC"));
		//cmb_nombre.Items.Add(new ListEditItem("NORMAS Y REGLAS INTERNAS CDLC", "NORMAS Y REGLAS INTERNAS CDLC"));
              //  cmb_nombre.Items.Add(new ListEditItem("SOLICITUD DE EMPLEO CDLC", "SOLICITUD DE EMPLEO CDLC"));
               // cmb_nombre.Items.Add(new ListEditItem("TRAMITACIÓN DE OBRA SOCIAL", "TRAMITACIÓN DE OBRA SOCIAL"));
//cmb_nombre.Items.Add(new ListEditItem("MEMORANDUM N°85/06", "MEMORANDUM N°85/06"));
//cmb_nombre.Items.Add(new ListEditItem("MEMORANDUM 93/06 LLAVES DE IDENTIFICACION VEHICULAR", "MEMORANDUM 93/06 LLAVES DE IDENTIFICACION VEHICULAR"));
//cmb_nombre.Items.Add(new ListEditItem("ACUERDO DE CONFIDENCIALIDAD", "ACUERDO DE CONFIDENCIALIDAD"));
//cmb_nombre.Items.Add(new ListEditItem("MEMORANDUM 01/17 CHOFERES - CONTROL DOCUMENTAL", "MEMORANDUM 01/17 CHOFERES - CONTROL DOCUMENTAL"));
//cmb_nombre.Items.Add(new ListEditItem("POLITICA DE MANEJO", "POLITICA DE MANEJO"));
//cmb_nombre.Items.Add(new ListEditItem("ANSES", "ANSES"));
                DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", "SELECT ID, DESCRIPCION FROM RRHH_ADIC_CONTRATOS_TIPOS with(nolock) order by DESCRIPCION");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmb_nombre.Items.Add(new ListEditItem(dt.Rows[i]["DESCRIPCION"].ToString(), dt.Rows[i]["ID"].ToString()));
                }
                cmb_nombre.SelectedIndex = 0;
            }

            

        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }
    }
 


    protected void cmb_empresa_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {

            chk_contratos.Items.Clear();
            FailureText.Text = "";
            cmb_nombre.SelectedIndex = 0;

            if (cmb_empresa.SelectedItem.Text != "SELECCIONE")
            {
                cargar();
            }
             

        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }

    }

    private void cargar()
    {
        chk_contratos.Items.Clear();
        DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", "SELECT ID, NOMBRE FROM RRHH_TIPOS_CONTRATOS with(nolock) WHERE EMPRESA='" + cmb_empresa .SelectedItem.Value.ToString()+ "' order by NOMBRE");
        if (dt.Rows.Count > 0)
        {
           
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                chk_contratos.Items.Add(new ListEditItem(dt.Rows[i]["NOMBRE"].ToString(), dt.Rows[i]["ID"].ToString()));

            }
             
        }
    }

    
    

    protected void btn_enviar_Click(object sender, EventArgs e)
    {
        try
        {

            if (cmb_empresa.SelectedItem.Text != "SELECCIONE" && cmb_nombre.SelectedItem.Text != "SELECCIONE" && chk_contratos.Items.Count > 0 && chk_contratos.SelectedItems.Count > 0)
            {

                if (!Directory.Exists(Server.MapPath(UploadDirectory)))
                {
                    Directory.CreateDirectory(Server.MapPath(UploadDirectory));
                }


                bool b = false;
                string fileName="";

                foreach (HttpPostedFile postedFile in FileUpload1.PostedFiles)
                {
                    fileName = UploadDirectory + "/" + Path.GetFileName(postedFile.FileName);
                    postedFile.SaveAs(Server.MapPath(fileName));
                    b = true;

                }

                if (b)
                {
                    for (int i = 0; i < chk_contratos.Items.Count; i++)
                    {
                        if (chk_contratos.Items[i].Selected)
                        {
                            Interfaz.Eliminar_ADIC_CONTRATOS(chk_contratos.Items[i].Value.ToString(), cmb_nombre.SelectedItem.Text);
                            Interfaz.Alta_ADIC_CONTRATOS(chk_contratos.Items[i].Value.ToString(), cmb_nombre.SelectedItem.Text, fileName);
                        }
                    }
                }


                FailureText.Text = "";
                cmb_nombre.SelectedIndex = 0;
                chk_contratos.Items.Clear();
                cmb_empresa.SelectedIndex = 0;
                FailureText.Text = "Guardado";
              
            }


        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }

    }

    private void limpiar()
    {
        chk_contratos.Items.Clear();
        FailureText.Text = "";
        cmb_empresa.SelectedIndex = 0;
        cmb_nombre.SelectedIndex = 0;

    }


    protected void btn_canacelar_Click(object sender, EventArgs e)
    {
        try
        {
            limpiar();
        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }

    }
}