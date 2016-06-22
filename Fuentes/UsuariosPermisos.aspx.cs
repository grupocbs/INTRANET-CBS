using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
using System.Data.SqlClient;
using System.Configuration;


public partial class UsuariosPermisos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["usuario"] == null)
            {

               Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {

                DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", "SELECT idusuario as id,usuario FROM Usuarios with(nolock) GROUP BY idusuario ,usuario");

                foreach (DataRow dr in dt.Rows)
                {
                    ASPxComboBox1.Items.Add(new ListEditItem(dr["usuario"].ToString(), dr["id"].ToString()));
                    ASPxComboBox2.Items.Add(new ListEditItem(dr["usuario"].ToString(), dr["id"].ToString()));

                }
                ASPxComboBox1.Items.Add(new ListEditItem("SELECCIONE", "SELECCIONE"));
                ASPxComboBox1.SelectedIndex = ASPxComboBox1.Items.Count - 1;
                ASPxComboBox2.Items.Add(new ListEditItem("SELECCIONE", "SELECCIONE"));
                ASPxComboBox2.SelectedIndex = ASPxComboBox1.Items.Count - 1;
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
          

            string sql = "SELECT MenuId, Descripcion, Posicion, PadreId, Icono, Url";
            sql += " FROM Menu with(nolock)";
          


            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlDataAdapter daMenu = new SqlDataAdapter(sql, strConnString);
            daMenu.Fill(dtMenuItems);

            foreach (DataRow drMenuItem in dtMenuItems.Rows)
            {
                if (drMenuItem["MenuId"].Equals(drMenuItem["PadreId"]))
                {
                    TreeViewNode mnuMenuItem = new TreeViewNode();
                    mnuMenuItem.Name = drMenuItem["MenuId"].ToString();
                    mnuMenuItem.Text = drMenuItem["descripcion"].ToString();
                    //mnuMenuItem.SelectAction = TreeNodeSelectAction.None;
                    
                   

                    AddMenuItem(ref mnuMenuItem, dtMenuItems);
                   
                    TreeView1.Nodes.Add(mnuMenuItem);
              


                }
            }
        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }
    }
    private void AddMenuItem(ref TreeViewNode mnuMenuItem, DataTable dtMenuItems)
    {
        try
        {
            foreach (DataRow drMenuItem in dtMenuItems.Rows)
            {
                if (drMenuItem["PadreId"].ToString().Equals(mnuMenuItem.Name) && !drMenuItem["MenuId"].Equals(drMenuItem["PadreId"]))
                {
                    TreeViewNode mnuNewMenuItem = new TreeViewNode();
                    mnuNewMenuItem.Name = drMenuItem["MenuId"].ToString();
                    mnuNewMenuItem.Text = drMenuItem["descripcion"].ToString();
                   // mnuNewMenuItem.SelectAction = TreeNodeSelectAction.None;


                    mnuMenuItem.Nodes.Add(mnuNewMenuItem);

                    //hacemos un llamado al metodo recursivo encargado de generar el árbol del menú.
                    AddMenuItem(ref mnuNewMenuItem, dtMenuItems);
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - AddMenuItem(ref MenuItem mnuMenuItem, DataTable dtMenuItems)");
        }
    }




    private void seleccionarmenu(string idusuario)
    {
        try
        {
            string sql = "SELECT m.MenuId, Descripcion, Posicion, PadreId, Icono, Url";
            sql += " FROM Menu m with(nolock)";
            sql += " LEFT JOIN UsuarioMenu mu with(nolock)";
            sql += " ON m.menuid=mu.menuid";
            sql += " WHERE mu.idusuario=" + idusuario;

            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlDataAdapter daMenu = new SqlDataAdapter(sql, strConnString);
            daMenu.Fill(dtMenuItems);

            foreach (DataRow drMenuItem in dtMenuItems.Rows)
            {

                it(drMenuItem["MenuId"].ToString());

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
                Interfaz.EliminarMenuPorUsuario(ASPxComboBox1.SelectedItem.Value.ToString());

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
                //almacenarpermiso(usuario, t.value);
                Interfaz.AgregarMenuPorUsuario(usuario, Convert.ToInt32(t.Name));
               // Label1.InnerHtml += usuario + "-" + t.Value.ToString() + "<br>";
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
      
            selecciona_menu(e.Node,e.Node.Checked);
        
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



    protected void Button2_Click(object sender, EventArgs e)
    {

        try
        {
            if (ASPxComboBox1.SelectedItem.Value.ToString() != "SELECCIONE" && ASPxComboBox2.SelectedItem.Value.ToString() != "SELECCIONE")
            {
                Literal1.Text = "";
                Interfaz.EliminarMenuPorUsuario(ASPxComboBox2.SelectedItem.Value.ToString());
                Interfaz.CopiarMenuPorUsuario(ASPxComboBox1.SelectedItem.Value.ToString(), ASPxComboBox2.SelectedItem.Value.ToString());

                Literal1.Text = "COPIADO CORRECTAMENTE";
            }
            else
            {
                Literal1.Text = "SELECCIONE UN USUARIO";
            }
        }
        catch (Exception ex)
        {
            Literal1.Text = ex.Message;
        }
    }
}
   

