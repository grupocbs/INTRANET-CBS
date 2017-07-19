using System;
using System.Drawing;
using System.IO;
using DevExpress.Web;
using DevExpress.Web.Internal;
using System.Data;
using System.Collections.Generic;
using System.Net.Mail;
using System.Configuration;

public partial class RRHH_RESERVA_SALA : System.Web.UI.Page
{
  
    protected void Page_Load(object sender, EventArgs e)
    {
      
        //Session.Add("usuario", "1");
        if (Session["usuario"] == null)
        {
            Response.Redirect("INTRANET_LOGIN.aspx");
        }
        if (!IsPostBack)
        {

            cmb_lugar.Items.Add(new ListEditItem("SELECCIONE", "SELECCIONE"));
            cmb_lugar.Items.Add(new ListEditItem("SALA DE REUNIONES", "SALA DE REUNIONES"));
            cmb_lugar.Items.Add(new ListEditItem("SUM", "SUM"));
            cmb_lugar.SelectedIndex = 0;


            txt_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txt_fecha.MinDate = DateTime.Now.AddDays(-1);
            
 
            
        }

         
       
    }

    protected void chk_CheckedChanged(object sender, EventArgs e)
    {

        if (cmb_lugar.SelectedItem.Text != "SELECCIONE" && txt_fecha.Text.Length > 0)
        {
            if (((ASPxCheckBox)sender).Checked)
            {
                Interfaz.Alta_RESERVA_SALA(cmb_lugar.SelectedItem.Text, Convert.ToDateTime(txt_fecha.Text), ((ASPxCheckBox)sender).Text, ((System.Web.UI.WebControls.Label)(Master.FindControl("lbl_footer"))).Text);
                FailureText.Text = "Guardado";
            }
            else
            {
                Interfaz.Eliminar_RESERVA_SALA(cmb_lugar.SelectedItem.Text, Convert.ToDateTime(txt_fecha.Text), ((ASPxCheckBox)sender).Text, ((System.Web.UI.WebControls.Label)(Master.FindControl("lbl_footer"))).Text);
                FailureText.Text = "Eliminado";
            }
            switch (((ASPxCheckBox)sender).ID)
            {
                case "chk_08_09":
                    {
                        if (((ASPxCheckBox)sender).Checked)
                        {
                            txt_08_09.Text = " Asignado a: " + ((System.Web.UI.WebControls.Label)(Master.FindControl("lbl_footer"))).Text;
                            
                        }
                        else
                        {
                            txt_08_09.Text = "";
                        }
                        break;
                    }

                case "chk_09_10":
                    {
                        if (((ASPxCheckBox)sender).Checked)
                        {
                            txt_09_10.Text = " Asignado a: " + ((System.Web.UI.WebControls.Label)(Master.FindControl("lbl_footer"))).Text;

                        }
                        else
                        {
                            txt_09_10.Text = "";
                        }
                        break;
                    }
                case "chk_10_11":
                    {
                        if (((ASPxCheckBox)sender).Checked)
                        {
                            txt_10_11.Text = " Asignado a: " + ((System.Web.UI.WebControls.Label)(Master.FindControl("lbl_footer"))).Text;

                        }
                        else
                        {
                            txt_10_11.Text = "";
                        }
                        break;
                    }
                case "chk_11_12":
                    {
                        if (((ASPxCheckBox)sender).Checked)
                        {
                            txt_11_12.Text = " Asignado a: " + ((System.Web.UI.WebControls.Label)(Master.FindControl("lbl_footer"))).Text;

                        }
                        else
                        {
                            txt_11_12.Text = "";
                        }
                        break;
                    }
                case "chk_12_13":
                    {
                        if (((ASPxCheckBox)sender).Checked)
                        {
                            txt_12_13.Text = " Asignado a: " + ((System.Web.UI.WebControls.Label)(Master.FindControl("lbl_footer"))).Text;

                        }
                        else
                        {
                            txt_12_13.Text = "";
                        }
                        break;
                    }
                case "chk_13_14":
                    {
                        if (((ASPxCheckBox)sender).Checked)
                        {
                            txt_13_14.Text = " Asignado a: " + ((System.Web.UI.WebControls.Label)(Master.FindControl("lbl_footer"))).Text;

                        }
                        else
                        {
                            txt_13_14.Text = "";
                        }
                        break;
                    }
                case "chk_14_15":
                    {
                        if (((ASPxCheckBox)sender).Checked)
                        {
                            txt_14_15.Text = " Asignado a: " + ((System.Web.UI.WebControls.Label)(Master.FindControl("lbl_footer"))).Text;

                        }
                        else
                        {
                            txt_14_15.Text = "";
                        }
                        break;
                    }
                case "chk_15_16":
                    {
                        if (((ASPxCheckBox)sender).Checked)
                        {
                            txt_15_16.Text = " Asignado a: " + ((System.Web.UI.WebControls.Label)(Master.FindControl("lbl_footer"))).Text;

                        }
                        else
                        {
                            txt_15_16.Text = "";
                        }
                        break;
                    }
                case "chk_16_17":
                    {
                        if (((ASPxCheckBox)sender).Checked)
                        {
                            txt_16_17.Text = " Asignado a: " + ((System.Web.UI.WebControls.Label)(Master.FindControl("lbl_footer"))).Text;

                        }
                        else
                        {
                            txt_16_17.Text = "";
                        }
                        break;
                    }
                case "chk_17_18":
                    {
                        if (((ASPxCheckBox)sender).Checked)
                        {
                            txt_17_18.Text = " Asignado a: " + ((System.Web.UI.WebControls.Label)(Master.FindControl("lbl_footer"))).Text;

                        }
                        else
                        {
                            txt_17_18.Text = "";
                        }
                        break;
                    }

            }


        }
        else
        {
            cmb_lugar.Focus();
            FailureText.Text = "";
        }
        
    }

    protected void txt_fecha_ValueChanged(object sender, EventArgs e)
    {
        cargar();
    }




    protected void cmb_lugar_SelectedIndexChanged(object sender, EventArgs e)
    {

        cargar();

    }

    private void cargar()
    {
        try
        {
            FailureText.Text = "";
           
            if (cmb_lugar.SelectedItem.Text != "SELECCIONE" && txt_fecha.Text.Length > 0)
            {
                chk_08_09.Checked = false;
                chk_08_09.Enabled = true;
                txt_08_09.Text = "";
                chk_09_10.Checked = false;
                chk_09_10.Enabled = true;
                txt_09_10.Text = "";
                chk_10_11.Checked = false;
                chk_10_11.Enabled = true;
                txt_10_11.Text = "";
                chk_11_12.Checked = false;
                chk_11_12.Enabled = true;
                txt_11_12.Text = "";
                chk_12_13.Checked = false;
                chk_12_13.Enabled = true;
                txt_12_13.Text = "";
                chk_13_14.Checked = false;
                chk_13_14.Enabled = true;
                txt_13_14.Text = "";
                chk_14_15.Checked = false;
                chk_14_15.Enabled = true;
                txt_14_15.Text = "";
                chk_15_16.Checked = false;
                chk_15_16.Enabled = true;
                txt_15_16.Text = "";
                chk_16_17.Checked = false;
                chk_16_17.Enabled = true;
                txt_16_17.Text = "";
                chk_17_18.Checked = false;
                chk_17_18.Enabled = true;
                txt_17_18.Text = "";
                string sql = "select * from RESERVA_SALA with (nolock) WHERE lugar='" + cmb_lugar.SelectedItem.Text + "' and fecha='" + txt_fecha.Text + "'";
                DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql);
                foreach (DataRow dr in dt.Rows)
                {
                    switch (dr["HORA"].ToString())
                    {
                        case "De 08hs a 09hs":
                            {
                                if (dr["USUARIO"].ToString() == ((System.Web.UI.WebControls.Label)(Master.FindControl("lbl_footer"))).Text)
                                {
                                    chk_08_09.Enabled = true;
                                }
                                else
                                {
                                    chk_08_09.Enabled = false;
                                }
                                chk_08_09.Checked = true;
                                txt_08_09.Text = " Asignado a: " + dr["USUARIO"].ToString();
                                break;
                            }

                        case "De 09hs a 10hs":
                            {
                                if (dr["USUARIO"].ToString() == ((System.Web.UI.WebControls.Label)(Master.FindControl("lbl_footer"))).Text)
                                {
                                    chk_09_10.Enabled = true;
                                }
                                else
                                {
                                    chk_09_10.Enabled = false;
                                }
                                chk_09_10.Checked = true;
                                txt_09_10.Text = " Asignado a: " + dr["USUARIO"].ToString();
                                break;
                            }
                        case "De 10hs a 11hs":
                            {
                                if (dr["USUARIO"].ToString() == ((System.Web.UI.WebControls.Label)(Master.FindControl("lbl_footer"))).Text)
                                {
                                    chk_10_11.Enabled = true;
                                }
                                else
                                {
                                    chk_10_11.Enabled = false;
                                }
                                chk_10_11.Checked = true;
                                txt_10_11.Text = " Asignado a: " + dr["USUARIO"].ToString();
                                break;
                            }
                        case "De 11hs a 12hs":
                            {
                                if (dr["USUARIO"].ToString() == ((System.Web.UI.WebControls.Label)(Master.FindControl("lbl_footer"))).Text)
                                {
                                    chk_11_12.Enabled = true;
                                }
                                else
                                {
                                    chk_11_12.Enabled = false;
                                }
                                chk_11_12.Checked = true;
                                txt_11_12.Text = " Asignado a: " + dr["USUARIO"].ToString();
                                break;
                            }
                        case "De 12hs a 13hs":
                            {
                                if (dr["USUARIO"].ToString() == ((System.Web.UI.WebControls.Label)(Master.FindControl("lbl_footer"))).Text)
                                {
                                    chk_12_13.Enabled = true;
                                }
                                else
                                {
                                    chk_12_13.Enabled = false;
                                }
                                chk_12_13.Checked = true;
                                txt_12_13.Text = " Asignado a: " + dr["USUARIO"].ToString();
                                break;
                            }
                        case "De 13hs a 14hs":
                            {
                                if (dr["USUARIO"].ToString() == ((System.Web.UI.WebControls.Label)(Master.FindControl("lbl_footer"))).Text)
                                {
                                    chk_13_14.Enabled = true;
                                }
                                else
                                {
                                    chk_13_14.Enabled = false;
                                }
                                chk_13_14.Checked = true;
                                txt_13_14.Text = " Asignado a: " + dr["USUARIO"].ToString();
                                break;
                            }
                        case "De 14hs a 15hs":
                            {
                                if (dr["USUARIO"].ToString() == ((System.Web.UI.WebControls.Label)(Master.FindControl("lbl_footer"))).Text)
                                {
                                    chk_14_15.Enabled = true;
                                }
                                else
                                {
                                    chk_14_15.Enabled = false;
                                }
                                chk_14_15.Checked = true;
                                txt_14_15.Text = " Asignado a: " + dr["USUARIO"].ToString();
                                break;
                            }
                        case "De 15hs a 16hs":
                            {
                                if (dr["USUARIO"].ToString() == ((System.Web.UI.WebControls.Label)(Master.FindControl("lbl_footer"))).Text)
                                {
                                    chk_15_16.Enabled = true;
                                }
                                else
                                {
                                    chk_15_16.Enabled = false;
                                }
                                chk_15_16.Checked = true;
                                txt_15_16.Text = " Asignado a: " + dr["USUARIO"].ToString();
                                break;
                            }
                        case "De 16hs a 17hs":
                            {
                                if (dr["USUARIO"].ToString() == ((System.Web.UI.WebControls.Label)(Master.FindControl("lbl_footer"))).Text)
                                {
                                    chk_16_17.Enabled = true;
                                }
                                else
                                {
                                    chk_16_17.Enabled = false;
                                }
                                chk_16_17.Checked = true;
                                txt_16_17.Text = " Asignado a: " + dr["USUARIO"].ToString();
                                break;
                            }
                        case "De 17hs a 18hs":
                            {
                                if (dr["USUARIO"].ToString() == ((System.Web.UI.WebControls.Label)(Master.FindControl("lbl_footer"))).Text)
                                {
                                    chk_17_18.Enabled = true;
                                }
                                else
                                {
                                    chk_17_18.Enabled = false;
                                }
                                chk_17_18.Checked = true;
                                txt_17_18.Text = " Asignado a: " + dr["USUARIO"].ToString();
                                break;
                            }

                    }
                }

            }

        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }
    }

     

    
}