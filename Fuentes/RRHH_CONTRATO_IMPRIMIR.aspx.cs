using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

 

public partial class RRHH_CONTRATO_IMPRIMIR : System.Web.UI.Page
{

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {

            if (Session["usuario"] != null && Session["IMP_CONTRATO"] != null)
            {

                txt_descripcion.InnerHtml = Session["IMP_CONTRATO"].ToString();
                if (File.Exists(Server.MapPath("tmp/DocumentoImprimir" + Session["usuario"].ToString() + ".pdf")))
                {

                    PanelPDF.InnerHtml = "<iframe src='" + "tmp/DocumentoImprimir" + Session["usuario"].ToString() + ".pdf" + "' width='0px' height='0px'></iframe>";
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }


     
 

    


   
}