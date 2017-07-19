using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
using System.Data.SqlClient;
using System.Configuration;


public partial class OS_USUARIOS_OBJETIVOS : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["usuario"] == null)
            {

                Response.Redirect("INTRANET_LOGIN.aspx");
            }
            if (!IsPostBack)
            {

                DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", "SELECT usuario FROM Usuarios with(nolock) WHERE supervisor <> ' ' GROUP BY usuario order by usuario");

                foreach (DataRow dr in dt.Rows)
                {
                    ASPxComboBox1.Items.Add(new ListEditItem(dr["usuario"].ToString(), dr["usuario"].ToString()));


                }
                ASPxComboBox1.Items.Add(new ListEditItem("SELECCIONE", "SELECCIONE"));
                ASPxComboBox1.SelectedIndex = ASPxComboBox1.Items.Count - 1;

                armarmenu();

                TreeView1.ExpandAll();

            }
        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }
    }


    protected void ASPxComboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            FailureText.Text = "";
            LimpiaSelecciona();
        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }

    }

    private void LimpiaSelecciona()
    {
        foreach (TreeViewNode t in TreeView1.Nodes)
        {
            Limpia_menu(t);
        }
        if (ASPxComboBox1.SelectedItem.Value.ToString() != "SELECCIONE")
        {
            seleccionarmenu(ASPxComboBox1.SelectedItem.Value.ToString());
        }
    }
    protected void Limpia_menu(TreeViewNode t)
    {
        if (t != null)
        {

            t.Checked = false;

            foreach (TreeViewNode temp in t.Nodes)
            {
                Limpia_menu(temp);

            }



        }
        else
        {
            return;
        }
    }




    private void armarmenu()
    {
        try
        {


            string sql = "SELECT USR_CLIOBJ_CODOBJ, NOMBRE=USR_CLIOBJ_OBJDSC + ' - '+ VTMCLH_NOMBRE";
            sql += " FROM USR_CLIOBJ o with(nolock)";
	    sql += " left join VTMCLH c with(nolock)";
 	    sql += " on o.USR_CLIOBJ_CODCLI=c.VTMCLH_NROCTA";
            sql += " ORDER BY USR_CLIOBJ_OBJDSC + ' - ' + VTMCLH_NOMBRE";



            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["CBS"].ConnectionString;
            SqlDataAdapter daMenu = new SqlDataAdapter(sql, strConnString);
            daMenu.Fill(dtMenuItems);

            foreach (DataRow drMenuItem in dtMenuItems.Rows)
            {

                TreeViewNode mnuMenuItem = new TreeViewNode();
                mnuMenuItem.Name = drMenuItem["USR_CLIOBJ_CODOBJ"].ToString();
                mnuMenuItem.Text = drMenuItem["NOMBRE"].ToString();

                TreeView1.Nodes.Add(mnuMenuItem);
            }
        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }
    }
    




    private void seleccionarmenu(string usuario)
    {
        try
        {
            string sql = "SELECT OBJETIVO";
            sql += " FROM USUARIO_OBJETIVO with(nolock)";
            sql += " WHERE usuario='" + usuario + "'";

            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlDataAdapter daMenu = new SqlDataAdapter(sql, strConnString);
            daMenu.Fill(dtMenuItems);

            foreach (DataRow drMenuItem in dtMenuItems.Rows)
            {

                it(drMenuItem["OBJETIVO"].ToString());

            }
        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }

    }
    protected void it(string str)
    {
        foreach (TreeViewNode t in TreeView1.Nodes)
        {
            search_it(t, str);
        }
    }
    protected void search_it(TreeViewNode t, string str)
    {
        if (t != null)
        {
            if (t.Name == str)
            {
                t.Checked = true;
            }
            else
            {
                // if node contains child node,then process it
                foreach (TreeViewNode temp in t.Nodes)
                {
                    search_it(temp, str);

                }

            }

        }
        else
        {
            return;
        }

    }






    protected void Button1_Click(object sender, EventArgs e)
    {

        try
        {
            if (ASPxComboBox1.SelectedItem.Value.ToString() != "SELECCIONE")
            {
                FailureText.Text = "";
                Interfaz.EliminarObjetivoPorUsuario(ASPxComboBox1.SelectedItem.Value.ToString());

                foreach (TreeViewNode t in TreeView1.Nodes)
                {
                        search_guardar(t, ASPxComboBox1.SelectedItem.Value.ToString());
                }

                LimpiaSelecciona();
                FailureText.Text = "ALMACENADO CORRECTAMENTE";
            }
            else
            {
                FailureText.Text = "SELECCIONE UN USUARIO";
            }
        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }
    }
    protected void search_guardar(TreeViewNode t, string usuario)
    {
        if (t != null)
        {
            if (t.Checked)
            {
                Interfaz.AgregarObjetivoPorUsuario(usuario, t.Name);
                foreach (TreeViewNode temp in t.Nodes)
                {
                    search_guardar(temp, usuario);

                }
            }


        }
        else
        {
            return;
        }

    }
    protected void TreeView1_TreeNodeCheckChanged(object sender, TreeViewNodeEventArgs e)
    {

        selecciona_menu(e.Node, e.Node.Checked);

    }


    protected void selecciona_menu(TreeViewNode t, bool check)
    {
        if (t != null)
        {

            t.Checked = check;

            foreach (TreeViewNode temp in t.Nodes)
            {
                selecciona_menu(temp, check);

            }



        }
        else
        {
            return;
        }
    }



}
   

