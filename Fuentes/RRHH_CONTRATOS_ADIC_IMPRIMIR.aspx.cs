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

public partial class RRHH_CONTRATOS_ADIC_IMPRIMIR : System.Web.UI.Page
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
                cmb_empresa.Items.Add(new ListEditItem("SELECCIONE", "SELECCIONE"));
                cmb_empresa.Items.Add(new ListEditItem("BARCELO", "BARCELO"));
                cmb_empresa.Items.Add(new ListEditItem("CASA DE LA COSTA", "CASA DE LA COSTA"));
                cmb_empresa.Items.Add(new ListEditItem("CBS SRL", "CBS SRL"));
                cmb_empresa.SelectedIndex = 0;
              
             
               
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
           
          
            FailureText.Text = "";       
            chk_adicionales.Items.Clear();


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

          
            FailureText.Text = "";
           

            if (cmb_contratos.SelectedIndex > -1 && cmb_contratos.SelectedItem.Text != "SELECCIONE")
            {
   
                string sql = "select * from RRHH_TIPOS_CONTRATOS with(nolock) where id=" + cmb_contratos.SelectedItem.Value.ToString();
                DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql);
                if (dt.Rows.Count > 0)
                {
                    Adicionales(dt.Rows[0]["id"].ToString());
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

    }
    protected void btn_imprimir_Click(object sender, EventArgs e)
    {
        try
        {

            if (cmb_contratos.SelectedIndex > -1 && cmb_contratos.SelectedItem.Text != "SELECCIONE")
            {
                List<string> Lista = new List<string>();
                for (int i = 0; i < chk_adicionales.Items.Count; i++)
                {
                    //Lista.Add(Server.MapPath(chk_adicionales.Items[i].Value.ToString()));

                }

                Lista.Add(Server.MapPath("tmp/Constancia CBS SRL.pdf"));
                Lista.Add(Server.MapPath("tmp/NP163884.pdf"));


                string sFileJoin = Server.MapPath("tmp/DocumentoJoin1.pdf");
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

                string Copia = Server.MapPath("tmp/DocumentoJoin2.pdf");
                PdfReader reader = new PdfReader(sFileJoin);
                PdfStamper stamper = new PdfStamper(reader, new FileStream(Copia, FileMode.Create));
                AcroFields fields = stamper.AcroFields;
                stamper.JavaScript = "this.print(true);\r";
                stamper.FormFlattening = true;
                stamper.Close();
                reader.Close();

                Response.Redirect(Copia);

            }

        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }

    }

    private void limpiar()
    {
        
        FailureText.Text = "";
        cmb_contratos.Items.Clear();
        cmb_contratos.Text = "";
        cmb_empresa.SelectedIndex = 0;
        
        chk_adicionales.Items.Clear();
    }


   
}