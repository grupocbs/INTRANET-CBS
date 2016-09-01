using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Threading;
using DevExpress.Web;

/// <summary>
/// Descripción breve de Class1
/// </summary>
public class Interfaz
{

    public static DataTable EjecutarConsultaBD(string NombreBase, string consulta)
    {
        try
        {


            DataTable dt = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings[NombreBase].ConnectionString;
            SqlDataAdapter daMenu = new SqlDataAdapter(consulta, strConnString);
            daMenu.SelectCommand.CommandTimeout = 0;
            daMenu.Fill(dt);
            daMenu.SelectCommand.Connection.Close();
            daMenu.Dispose();

            return dt;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - EjecutarConsultaBD(string NombreBase, string consulta)");
        }
    }
    public static DataTable EjecutarSP(string NombreBase, string nombresp, SortedList<string, string> parametros)
    {
        try
        {

            string strConnString = ConfigurationManager.ConnectionStrings[NombreBase].ConnectionString;

            SqlDataSource SqlDataSource1 = new SqlDataSource(strConnString, nombresp);
            SqlDataSource1.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
            SqlDataSource1.Selecting += new SqlDataSourceSelectingEventHandler(SqlDataSource1_Selecting);

            for (int i = 0; i < parametros.Count; i++)
            {
                SqlDataSource1.SelectParameters.Add(parametros.Keys[i], parametros.Values[i]);
            }


            DataView dt = new DataView();
            dt = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);

            SqlDataSource1.Dispose();
            return dt.ToTable();


        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - EjecutarSP(string NombreBase, string nombresp)");
        }
    }
    static void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {
        e.Command.CommandTimeout = 0;
    }

    #region menu
    public DevExpress.Web.MenuItemCollection Armar(string idusuario)
    {
        try
        {

            DevExpress.Web.MenuItemCollection m = new DevExpress.Web.MenuItemCollection();

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
                if (drMenuItem["MenuId"].Equals(drMenuItem["PadreId"]))
                {
                    DevExpress.Web.MenuItem mnuMenuItem = new DevExpress.Web.MenuItem();
                    mnuMenuItem.Name = drMenuItem["MenuId"].ToString();
                    mnuMenuItem.Text = drMenuItem["descripcion"].ToString();
                    //mnuMenuItem.ImageUrl = drMenuItem["Icono"].ToString();

                    mnuMenuItem.Target = drMenuItem["Url"].ToString();

                    AddMenuItem(ref mnuMenuItem, dtMenuItems);
                    // mnuPrincipal.Items.Add(mnuMenuItem);
                    m.Add(mnuMenuItem);
                    //hacemos un llamado al metodo recursivo encargado de generar el árbol del menú.


                }
            }
            return m;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - Armar(string usuario)");
        }
    }
    private void AddMenuItem(ref DevExpress.Web.MenuItem mnuMenuItem, DataTable dtMenuItems)
    {
        try
        {
            foreach (DataRow drMenuItem in dtMenuItems.Rows)
            {
                if (drMenuItem["PadreId"].ToString().Equals(mnuMenuItem.Name) && !drMenuItem["MenuId"].Equals(drMenuItem["PadreId"]))
                {
                    DevExpress.Web.MenuItem mnuNewMenuItem = new DevExpress.Web.MenuItem();
                    mnuNewMenuItem.Name = drMenuItem["MenuId"].ToString();
                    mnuNewMenuItem.Text = drMenuItem["descripcion"].ToString();
                    // mnuNewMenuItem.ImageUrl = drMenuItem["Icono"].ToString();

                    mnuNewMenuItem.Target = drMenuItem["Url"].ToString();


                    mnuMenuItem.Items.Add(mnuNewMenuItem);

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
    public static DataTable CargarMenu(string url)
    {
        try
        {
            DevExpress.Web.MenuItemCollection m = new DevExpress.Web.MenuItemCollection();

            string sql = "SELECT *";
            sql += " FROM Menu  with(nolock)";
            sql += " WHERE url='" + url + "'";


            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlDataAdapter daMenu = new SqlDataAdapter(sql, strConnString);
            daMenu.Fill(dtMenuItems);


            return dtMenuItems;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - CargarMenu(string url)");
        }
    }
    #endregion
    #region administradores
    #region Menu
    public static DataTable CargarMenu()
    {
        try
        {
            DevExpress.Web.MenuItemCollection m = new DevExpress.Web.MenuItemCollection();

            string sql = "SELECT [MenuId],[Descripcion],[PadreId],[Posicion],[Icono],[Url]";
            sql += " FROM Menu with(nolock)";

            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlDataAdapter daMenu = new SqlDataAdapter(sql, strConnString);
            daMenu.Fill(dtMenuItems);


            return dtMenuItems;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - CargarMenu()");
        }
    }
    public static DataTable CargarMenuPorID(string menuid)
    {
        try
        {
            

            string sql = "SELECT [Url]";
            sql += " FROM Menu with(nolock)";
            sql += " WHERE menuid=" + menuid;

            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlDataAdapter daMenu = new SqlDataAdapter(sql, strConnString);
            daMenu.Fill(dtMenuItems);


            return dtMenuItems;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - CargarMenu(string menuid)");
        }
    }
    public static void EliminarMenu(string menuid)
    {
        try
        {


            string sql = "DELETE FROM Menu ";
            sql += " WHERE menuid=" + menuid;


            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - EliminarMenu(string menuid)");
        }

    }
    public static void AgregarMenu(string descripcion, string padreid, string posicion, string icono, string url)
    {
        try
        {


            string sql = "INSERT INTO Menu (descripcion, padreid,posicion,icono,url) ";
            sql += " VALUES ('" + descripcion + "'," + padreid + "," + posicion + ",'" + icono + "','" + url + "')";


            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - AgregarMenu(string descripcion, string padreid, string posicion, string icono, string url)");
        }

    }
    public static void EditarMenu(string menuid,string descripcion, string padreid, string posicion, string icono, string url)
    {
        try
        {


            string sql = "UPDATE Menu SET  descripcion='" + descripcion + "',padreid=" + padreid + ",posicion=" + posicion + ",icono='" + icono + "',url='" + url + "'";
            sql += " WHERE menuid=" + menuid;


            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - EditarMenu(string menuid,string descripcion, string padreid, string posicion, string icono, string url)");
        }

    }
    #endregion
    #region MenuUsuario
    public static void EliminarMenuPorUsuario(string idusuario)
    {
        try
        {


            string sql = "DELETE FROM UsuarioMenu ";
            sql += " WHERE idusuario=" + idusuario;


            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - EliminarMenuPorUsuario(string usuario)");
        }

    }
    public static void AgregarMenuPorUsuario(string idusuario, int menuid)
    {
        try
        {

            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand();
            daMenu.Connection = new SqlConnection(strConnString);
            string sql = "";


            daMenu.Connection.Open();

            sql = " INSERT INTO UsuarioMenu (idusuario, MenuId) ";
            sql += " VALUES (" + idusuario + "," + menuid + ")";
            daMenu.CommandText = sql;
            daMenu.ExecuteNonQuery();



            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message + " - AgregarPerfil(string descripcion, List<int> menuid)");
        }

    }
    public static void CopiarMenuPorUsuario(string usuarioorigen, string usuariodestino)
    {
        try
        {

            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand();
            daMenu.Connection = new SqlConnection(strConnString);
            string sql = "";


            daMenu.Connection.Open();

            sql = " INSERT INTO UsuarioMenu (idusuario, MenuId) ";
            sql += " SELECT us=" + usuariodestino + ", menuid";
            sql += " FROM UsuarioMenu with(nolock)";
            sql += " WHERE idusuario=" + usuarioorigen;
            daMenu.CommandText = sql;
            daMenu.ExecuteNonQuery();



            daMenu.Connection.Close();
            daMenu.Dispose();


            //DataTable dt = new DataTable();
            //string strConnString = ConfigurationManager.ConnectionStrings["Intranet"].ConnectionString;
            //SqlDataAdapter daMenu = new SqlDataAdapter(sql, strConnString);
            //daMenu.SelectCommand.CommandTimeout = 0;
            //daMenu.Fill(dt);
            //daMenu.SelectCommand.Connection.Close();
            //daMenu.Dispose();

        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message + " - AgregarPerfil(string descripcion, List<int> menuid)");
        }

    }
    #endregion
    #region usuarios
    public static DataTable CargarCuentasUsuario(string usuario)
    {
        try
        {
            DevExpress.Web.MenuItemCollection m = new DevExpress.Web.MenuItemCollection();

            string sql = "SELECT Usuario, Contraseña, Nombre, Mail, Supervisor, Imei, Servicio";
            sql += " FROM Usuarios with(nolock)";

            if (usuario.Length > 0)
            {
                sql += " WHERE usuario='" + usuario + "'";
            }
            sql += " order by usuario ";

            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlDataAdapter daMenu = new SqlDataAdapter(sql, strConnString);
            daMenu.Fill(dtMenuItems);


            return dtMenuItems;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - CargarCuentasUsuario(string usuario)");
        }
    }
    public static void EliminarCuentaUsuario(string usuario)
    {
        try
        {


            string sql = "DELETE FROM Usuarios ";
            sql += " WHERE usuario='" + usuario + "'";


            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - EliminarCuentaUsuario(string usuario)");
        }

    }
    public static void EditarCuentaUsuario(string nuevo_usuario, string viejo_usuario, string contraseña, string nombre, string mail, string supervisor, string imei, string servicio)
    {
        try
        {


            string sql = "UPDATE Usuarios SET usuario='" + nuevo_usuario + "' , contraseña='" + contraseña + "' , nombre='" + nombre + "' , mail='" + mail + "'";
            sql += ", supervisor='" + supervisor + "'";
            sql += ", imei='" + imei + "'";
 sql += ", servicio='" + servicio + "'";

            sql += " WHERE usuario='" + viejo_usuario + "'";


            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - EliminarCuentaUsuario(string usuario)");
        }

    }
    public static void AgregarCuentaUsuario(string usuario, string contraseña, string nombre, string mail, string supervisor, string imei, string servicio)
    {
        try
        {


            string sql = "INSERT INTO Usuarios (usuario, contraseña, nombre, mail,supervisor, imei,servicio) ";
            sql += " VALUES ('" + usuario + "','" + contraseña + "','" + nombre + "','" + mail + "','" +supervisor + "','" + imei + "','"+servicio+"')";


            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - AgregarCuentaUsuario(string usuario, string perfilid)");
        }

    }
    public static string Traer_Id_usuario(string usuario)
    {
        try
        {


            DataTable dt = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlDataAdapter daMenu = new SqlDataAdapter("SELECT id FROM Usuarios with(nolock) where Usuario='venc'", strConnString);
            daMenu.SelectCommand.CommandTimeout = 0;
            daMenu.Fill(dt);
            daMenu.SelectCommand.Connection.Close();
            daMenu.Dispose();

            return dt.Rows[0]["id"].ToString();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - Traer_Id_usuario(string usuario)");
        }
    }
    #endregion

    #endregion

    #region Clientes
    public static void Cargarclientes(string CUIT, string Denominacion, string Domicilio, string Localidad, string Telefono, string Mail)
    {
        try
        {


            string sql = "INSERT INTO [Ventas_especiales_Clientes] ([CUIT],[Denominacion],[Domicilio],[Localidad],[Telefono],[Mail]) ";
            sql += " VALUES ('" + CUIT + "','" + Denominacion + "','" + Domicilio + "','" + Localidad + "','" + Telefono + "','" + Mail + "')";


            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - Cargarclientes(string CUIT, string Denominacion, string Domicilio, string Localidad, string Telefono, string Mail)");
        }

    }
    public static void Eliminarcliente(string id_cliente)
    {
        try
        {


            string sql = " DELETE FROM [Ventas_especiales_Clientes] WHERE [id_cliente]=" + id_cliente;



            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - Eliminarcliente(string id_cliente)");
        }

    }
    public static void Editarcliente(string id_cliente,string CUIT, string Denominacion, string Domicilio, string Localidad, string Telefono, string Mail)
    {
        try
        {


            string sql = "UPDATE [Ventas_especiales_Clientes] ";
            sql += " SET CUIT='" + CUIT + "'";
            sql += " ,Denominacion='" + Denominacion + "'";
            sql += " ,Domicilio='" + Domicilio + "'";
            sql += " ,Localidad='" + Localidad + "'";
            sql += " ,Telefono='" + Telefono + "'";
            sql += " ,Mail='" + Mail + "'";
            sql += " WHERE id_cliente=" + id_cliente;


            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - Editarcliente(string id_cliente,string CUIT, string Denominacion, string Domicilio, string Localidad, string Telefono, string Mail)");
        }

    }
    #endregion
    #region Modelos
    public static void CargarModelos(string Modelo_cod, string Modelo)
    {
        try
        {


            string sql = "INSERT INTO [Ventas_especiales_Modelos] ([Modelo_cod],[Modelo]) ";
            sql += " VALUES ('" + Modelo_cod + "','" + Modelo + "')";


            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - CargarModelos(string Modelo_cod, string Modelo)");
        }

    }
    public static void EliminarModelos(string id_modelo)
    {
        try
        {


            string sql = " DELETE FROM [Ventas_especiales_Modelos] WHERE [id_modelo]=" + id_modelo;



            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - EliminarModelos(string id_modelo)");
        }

    }
    public static void EditarModelos(string id_modelo, string Modelo_cod, string Modelo)
    {
        try
        {


            string sql = "UPDATE [Ventas_especiales_Modelos] ";
            sql += " SET Modelo_cod='" + Modelo_cod + "'";
            sql += " ,Modelo='" + Modelo + "'";
           
            sql += " WHERE id_modelo=" + id_modelo;


            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - EditarModelos(string id_modelo, string Modelo_cod, string Modelo)");
        }

    }
    #endregion
    #region Accesorios
    public static void CargarAccesorios(string Accesorio)
    {
        try
        {


            string sql = "INSERT INTO [Ventas_especiales_Accesorios] ([Accesorio]) ";
            sql += " VALUES ('" + Accesorio + "')";


            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - CargarAccesorios(string Accesorio)");
        }

    }
    public static void EliminarAccesorios(string id_accesorio)
    {
        try
        {


            string sql = " DELETE FROM [Ventas_especiales_Accesorios] WHERE [id_accesorio]=" + id_accesorio;



            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - EliminarAccesorios(string id_accesorio)");
        }

    }
    public static void EditarAccesorios(string id_accesorio, string Accesorio)
    {
        try
        {


            string sql = "UPDATE [Ventas_especiales_Accesorios] ";
            sql += " SET Accesorio='" + Accesorio + "'";


            sql += " WHERE id_accesorio=" + id_accesorio;


            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - EditarAccesorios(string id_accesorio, string nombre)");
        }

    }
    #endregion
    #region Operaciones
    public static void CargarCabecera(string NicOp, string Cliente, DateTime Fecha_NicOp, DateTime Fecha_entrega, string Orden_de_compra, string Orden_de_compra_pdf, string id_usuario)
    {
        try
        {


            string sql = "INSERT INTO [Ventas_especiales_Cabecera] ([NicOp],[Cliente],[Fecha_NicOp],[Fecha_entrega],[Orden_de_compra],[Orden_de_compra_pdf], [User_carga_NicOP], [Fecha_carga_NicOp]) ";
            sql += " VALUES ('" + NicOp + "'," + Cliente + ",'" + Fecha_NicOp.ToString("yyyyMMdd") + "','" + Fecha_entrega.ToString("yyyyMMdd") + "','" + Orden_de_compra + "','" + Orden_de_compra_pdf + "'," + id_usuario + ",'" + DateTime.Now.ToString("yyyyMMdd") + "')";


            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - (string NicOp, string Cliente, DateTime Fecha_NicOp, DateTime Fecha_entrega, string Orden_de_compra, string Orden_de_compra_pdf, string Asignacion)");
        }

    }
    public static void CargarCabeceraUnidad(string id_cabecera, string Modelo, string Cantidad, string Color, string Asignacion)
    {
        try
        {


            string sql = "INSERT INTO [Ventas_especiales_Detalle] ([id_cabecera],[Modelo],[Cantidad],[Color],[Asignacion],[Estado]) ";
            sql += " VALUES (" + id_cabecera + "," + Modelo + "," + Cantidad + ",'" + Color + "','" + Asignacion + "',"+ 0 +")";


            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - CargarCabeceraUnidad(string id_cabecera,string Modelo, string Cantidad,string Color)");
        }

    }
    public static void EliminarCabeceraUnidad(string id_cabecera)
    {
        try
        {


            string sql = " DELETE FROM [Ventas_especiales_Detalle] WHERE [id_cabecera]=" + id_cabecera;



            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - EliminarOperacion(string NICOP)");
        }

    }
    public static void EditarCabecera(string id, string NicOp, string Cliente, DateTime Fecha_entrega, string Orden_de_compra, string Orden_de_compra_pdf)
    {
        try
        {


            string sql = "UPDATE [Ventas_especiales_Cabecera] ";
            sql += " SET NicOp='" + NicOp + "'";
          
                sql += " ,Cliente=" + Cliente;
          

            sql += " ,Fecha_entrega='" + Fecha_entrega.ToString("yyyyMMdd") + "'";
            sql += " ,Orden_de_compra='" + Orden_de_compra + "'";
            sql += " ,Orden_de_compra_pdf='" + Orden_de_compra_pdf + "'";
          
    
            sql += " WHERE id=" + id;


            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - EditarCabecera(string id, string NicOp, string Cliente, DateTime Fecha_NicOp, DateTime Fecha_entrega, string Orden_de_compra, string Orden_de_compra_pdf, string Asignacion, string id_usuario)");
        }

    }
    public static void CerrarUnidad(string id)
    {
        try
        {


            string sql = "UPDATE [Ventas_especiales_Detalle] ";
            sql += " SET estado=1";
            sql += " WHERE id=" + id;


            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - CerrarNicop(string id)");
        }

    }
    public static void EliminarCabecera(string id)
    {
        try
        {


            string sql = " DELETE FROM [Ventas_especiales_Cabecera] WHERE [id]=" + id;



            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - EliminarCabecera(string NICOP)");
        }

    }
    #endregion
    #region facturas
    public static void CargarFacturaUnidad(string id_unidad, string Chasis, string Motor, DateTime Fecha_fac, string Prefijo_fac, string Numero_fac, string Op_fac_pdf, string User_factura, bool Pago_fac)
    {
        try
        {


            string sql = "UPDATE [Ventas_especiales_Detalle] ";
            sql += " SET Chasis='" + Chasis + "'";
            sql += " ,Motor='" + Motor + "'";
            sql += " ,Fecha_fac='" + Fecha_fac.ToString("yyyyMMdd") + "'";
            sql += " ,Prefijo_fac=" + Prefijo_fac;
            sql += " ,Numero_fac=" + Numero_fac;
            sql += " ,Op_fac_pdf='" + Op_fac_pdf + "'";
            sql += " ,User_factura=" + User_factura;
            sql += " ,Pago_fac=" + ((Pago_fac)?1:0);
            sql += " ,Fecha_carga_factura='" + DateTime.Now.ToString("yyyyMMdd") + "'";
            sql += " WHERE id=" + id_unidad;


            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - CargarFacturaUnidad(string id_unidad, string Chasis, string Motor, DateTime Fecha_fac, string Prefijo_fac, string Numero_fac,string Op_fac_pdf, string User_factura)");
        }

    }
    #endregion
    #region Documentacion
    public static void IngresoDocumentacion(string id_unidad, string F01, DateTime Fecha_F01, string F12, DateTime Fecha_F12, string User_Documentacion, string Nro_Certificado)
    {
        try
        {


            string sql = "UPDATE [Ventas_especiales_Detalle] ";
            sql += " SET Fecha_Documentacion='" + DateTime.Now.ToString("yyyyMMdd") + "'";
            sql += " ,User_Documentacion=" + User_Documentacion;
            sql += " ,F01='" + F01 + "'";
            sql += " ,Fecha_F01='" + Fecha_F01 + "'";
            sql += " ,F12='" + F12 + "'";
            sql += " ,Fecha_F12='" + Fecha_F12 + "'";
            sql += " ,Nro_Certificado='" + Nro_Certificado + "'";
            sql += " WHERE id=" + id_unidad;


            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - IngresoDocumentacion(string id_unidad, DateTime Fecha_Documentacion, bool Documentacion_recibido, string User_Documentacion)");
        }

    }
    public static void EgresoDocumentacion(string id_unidad, DateTime Fecha_Documentacion_entrega, string User_Documentacion_entrega)
    {
        try
        {


            string sql = "UPDATE [Ventas_especiales_Detalle] ";
            sql += " SET Fecha_Documentacion_entrega='" + Fecha_Documentacion_entrega.ToString("yyyyMMdd") + "'";
            sql += " ,User_Documentacion_entrega=" + User_Documentacion_entrega;
            
            sql += " WHERE id=" + id_unidad;


            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - EgresoDocumentacion(string id_unidad, DateTime Fecha_Documentacion_entrega, string User_Documentacion_entrega)");
        }

    }
    #endregion
    #region Stock
    public static void IngresoUnidad(string id_unidad, DateTime Fecha_unidades_ingreso, bool Unidades_ingreso_verificacion, string User_unidades_ingreso)
    {
        try
        {


            string sql = "UPDATE [Ventas_especiales_Detalle] ";
            sql += " SET Fecha_unidades_ingreso='" + Fecha_unidades_ingreso.ToString("yyyyMMdd") + "'";
           
            sql += " ,User_unidades_ingreso=" + User_unidades_ingreso;
            sql += " ,Unidades_ingreso_verificacion=" + ((Unidades_ingreso_verificacion) ? 1 : 0);
           
            sql += " WHERE id=" + id_unidad;


            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - IngresoUnidad(string id_unidad, DateTime Fecha_unidades_ingreso, bool Unidades_ingreso_verificacion, string User_unidades_ingreso, string Observaciones, string Carta_de_porte_pdf)");
        }

    }
    public static void CargarObservacion(string id_unidad, string Observaciones, string Carta_de_porte_pdf, string Nro_Carta_de_Porte)
    {
        try
        {


            string sql = "INSERT INTO [Ventas_especiales_unidades_averiadas] ([id_unidad],[Observaciones],[Carta_de_porte_pdf],[Nro_Carta_de_Porte]) ";
            sql += " VALUES (" + id_unidad + ",'" + Observaciones + "','" + Carta_de_porte_pdf + "','" + Nro_Carta_de_Porte + "')";


            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - CargarObservacion(string id_unidad, string Observaciones, string Carta_de_porte_pdf)");
        }

    }
    public static void EliminarObservacion(string id_unidad)
    {
        try
        {


            string sql = " DELETE FROM [Ventas_especiales_unidades_averiadas] WHERE [id_unidad]=" + id_unidad;



            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - EliminarObservacion(string id_unidad)");
        }

    }
    public static void EgresoUnidad(string id_unidad, DateTime Fecha_unidades_entrega, string User_unidades_entrega)
    {
        try
        {


            string sql = "UPDATE [Ventas_especiales_Detalle] ";
            sql += " SET Fecha_unidades_entrega='" + Fecha_unidades_entrega.ToString("yyyyMMdd") + "'";
            sql += " ,User_unidades_entrega=" + User_unidades_entrega;
          

            sql += " WHERE id=" + id_unidad;


            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - EgresoUnidad(string id_unidad, DateTime Fecha_unidades_entrega, string User_unidades_entrega)");
        }

    }
    #endregion
    #region UnidadesAccesorios
    public static void EliminarUnidadAccesorios(string id_unidad)
    {
        try
        {


            string sql = " DELETE FROM [Ventas_especiales_accesorio_asignacion] WHERE [id_unidad]=" + id_unidad;



            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - EliminarObservacion(string id_unidad)");
        }

    }
    public static void CargarUnidadAccesorios(string id_unidad, string id_accesorio)
    {
        try
        {


            string sql = "INSERT INTO [Ventas_especiales_accesorio_asignacion] ([id_unidad],[id_accesorio]) ";
            sql += " VALUES (" + id_unidad + "," + id_accesorio + ")";


            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - CargarUnidadAccesorios(string id_unidad, string id_accesorio)");
        }

    }
    public static void InstalacionAccesorio(string id_unidad, string id_accesorio, DateTime Fecha_Instalacion)
    {
        try
        {


            string sql = "UPDATE [Ventas_especiales_accesorio_asignacion] ";
            sql += " SET Fecha_Instalacion='" + Fecha_Instalacion.ToString("yyyyMMdd") + "'";
            sql += " WHERE id_unidad=" + id_unidad;
            sql += " and id_accesorio=" + id_accesorio;


            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - InstalacionAccesorio(string id_unidad, string id_accesorio, DateTime Fecha_Instalacion)");
        }

    }
    #endregion
    #region vehiculos
    public static void Cargarvehiculos(string interno, string descripcion, string dominio,string radicacion, string localizacion, string fabricante, string responsable, string año, string identidad, string email, string modelo, string motor, string chasis, string tacografo, string kilometraje, string area, bool activo, bool notificar)
    {
        try
        {


            string sql = "INSERT INTO [documentacion] ([Interno],[Descripción],[Dominio],[Radicacion],[Localización],[Fabricante], [Responsable], [Año],[ENTITY],[email],[Modelo],[Motor],[Chasis],[Tacografo],[Kilometraje],[Area], [Activo],[Notificar]) ";
            sql += " VALUES ('" + interno + "','" + descripcion + "','" + dominio + "','" + radicacion + "','" + localizacion + "','" + fabricante + "','" + responsable + "','" + año + "','" + identidad + "','" + email + "','" + modelo + "','" + motor + "','" + chasis + "','" + tacografo + "','" + kilometraje + "','" + area + "'," + ((activo) ? 1 : 0) + "," + ((notificar) ? 1 : 0) + ")";
            

            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - Cargarvehiculos(string interno,string descripcion ,string dominio ,int localizacion ,int fabricante , string responsable , string año,string identidad ,string email ,string modelo ,string motor ,string chasis ,string tacografo ,string kilometraje ,string area,bool activo, bool notificar)");
        }

    }

    public static void Eliminarvehiculos(string idregistro)
    {
        try
        {


            string sql = " DELETE FROM [documentacion] WHERE [idregistro]=" + idregistro;
          


            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - Eliminarvehiculos(string interno)");
        }

    }
    public static void Editarvehiculos(string idregistro,string interno, string descripcion, string dominio,string radicacion, string localizacion, string fabricante, string responsable, string año, string identidad, string email, string modelo, string motor, string chasis, string tacografo, string kilometraje, string area, bool activo, bool notificar)
    {
        try
        {


            string sql = "UPDATE [documentacion] ";
            sql += " SET Descripción='" + descripcion + "'";
            sql += " ,Dominio='" + dominio + "'";
            sql += " ,Radicacion='" + radicacion + "'";
            sql += " ,Localización='" + localizacion + "'";
            sql += " ,Fabricante='" + fabricante + "'";
            sql += " ,Responsable='" + responsable + "'";
            sql += " ,Año='" + año + "'";
            sql += " ,ENTITY='" + identidad + "'";
            sql += " ,email='" + email + "'";
            sql += " ,Modelo='" + modelo + "'";
            sql += " ,Motor='" + motor + "'";
            sql += " ,Chasis='" + chasis + "'";
            sql += " ,Tacografo='" + tacografo + "'";
            sql += " ,Kilometraje='" + kilometraje + "'";
            sql += " ,Area='" + area + "'";
            sql += " ,Notificar=" + ((notificar) ? 1 : 0);
            sql += " ,Activo=" + ((activo) ? 1 : 0);
            sql += " ,interno='" + interno + "'";

            sql += " WHERE idregistro=" + idregistro;


            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - Editarvehiculos(string interno, string descripcion, string dominio, int idtipo, int idlocalizacion, int idfabricante, string responsable, int año, string identidad, string email, string modelo, string motor, string chasis, string tacografo, string kilometraje, string area)");
        }

    }


    public static void CargarFechavehiculos(string idregistro,string interno, DateTime fecha_seguro, DateTime fecha_patente, DateTime fecha_ruta, DateTime fecha_inscripcion, DateTime fecha_vtv)
    {
        try
        {


            string sql = "UPDATE documentacion ";
            sql += " SET Seguro='" + fecha_seguro.ToString("yyyyMMdd") + "'";
            sql += " ,Patente='" + fecha_patente.ToString("yyyyMMdd") + "'";
            sql += " ,Ruta='" + fecha_ruta.ToString("yyyyMMdd") + "'";
            sql += " ,Inscripcion_Provincial='" + fecha_inscripcion.ToString("yyyyMMdd") + "'";
            sql += " ,VTV='" + fecha_vtv.ToString("yyyyMMdd") + "'";
            sql += " WHERE idregistro=" + idregistro;


            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - CargarFechavehiculos(string interno,DateTime fecha_seguro, DateTime fecha_patente, DateTime fecha_ruta, DateTime fecha_inscripcion, DateTime fecha_vtv)");
        }

    }
    public static void EditarVencimiento(DateTime fecha, string campo, List<string> menuid)
    {
        try
        {

            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand();
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            

            string sql = "";
            

            foreach (string mid in menuid)
            {
                sql = " UPDATE documentacion ";
                sql += " SET " + campo + "='" + fecha.ToString("yyyyMMdd") + "'";
                sql += " WHERE idregistro=" + mid;
                daMenu.CommandText = sql;
                daMenu.ExecuteNonQuery();
            }


            daMenu.Connection.Close();
            daMenu.Dispose();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - EditarVencimiento(DateTime fecha, string campo, List<string> menuid)");
        }

    }
    public static void EditarVencimientoBlanco(string campo, List<string> menuid)
    {
        try
        {

            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand();
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();


            string sql = "";


            foreach (string mid in menuid)
            {
                sql = " UPDATE documentacion ";
                sql += " SET " + campo + "=NULL";
                sql += " WHERE idregistro=" + mid;
                daMenu.CommandText = sql;
                daMenu.ExecuteNonQuery();
            }


            daMenu.Connection.Close();
            daMenu.Dispose();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - EditarVencimiento(DateTime fecha, string campo, List<string> menuid)");
        }

    }

    #endregion
    #region INTERNOS
    public static void EliminarInterno(string idinterno)
    {
        try
        {


            string sql = "DELETE FROM INTERNOS ";
            sql += " WHERE idinterno=" + idinterno;


            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - EliminarCuentaUsuario(string usuario)");
        }

    }
    public static void EditarInterno(string interno, string area, string integrantes, string idinterno)
    {
        try
        {


            string sql = "UPDATE INTERNOS SET interno='" + interno + "' , area='" + area + "', integrantes='" + integrantes + "'";
            sql += " WHERE idinterno=" + idinterno;


            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - EliminarCuentaUsuario(string usuario)");
        }

    }
    public static void AgregarInterno(string interno, string area, string integrantes)
    {
        try
        {


            string sql = "INSERT INTO INTERNOS (interno, area,integrantes) ";
            sql += " VALUES ('" + interno + "','" + area + "','" + integrantes + "')";


            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - AgregarCuentaUsuario(string usuario, string perfilid)");
        }

    }
    #endregion

    #region NOTIFICACIONES
    public static void EliminarNOTIFICACION(string IDNOTIFICACION)
    {
        try
        {


            string sql = "DELETE FROM NOTIFICACIONES ";
            sql += " WHERE IDNOTIFICACION=" + IDNOTIFICACION;


            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - EliminarCuentaUsuario(string usuario)");
        }

    }

    public static void AgregarNOTIFICACION(string IDUSUARIO, string TITULO, string NOTIFICACION)
    {
        try
        {


            string sql = "INSERT INTO NOTIFICACIONES (IDUSUARIO, TITULO,NOTIFICACION) ";
            sql += " VALUES (" + IDUSUARIO + ",'" + TITULO + "','" + NOTIFICACION + "')";


            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - AgregarCuentaUsuario(string usuario, string perfilid)");
        }

    }
    #endregion

    #region CBS_Observaciones
    public static void AltaObservacion(string codobs,DateTime fecha, string tipoob, string codcli, string observ, string coordn, string usernm)
    {
        try
        {


            string sql = "INSERT INTO [USR_OPOBOJ] ([USR_OPOBOJ_CODOBS],[USR_OPOBOJ_FCHOBS],[USR_OPOBOJ_TIPOOB],[USR_OPOBOJ_CODCLI],[USR_OPOBOJ_ESTADO],[USR_OPOBOJ_OBSERV],[USR_OPOBOJ_COORDN],[USR_OP_FECALT],[USR_OP_FECMOD],[USR_OP_ULTOPR],[USR_OP_DEBAJA],[USR_OP_OALIAS],[USR_OP_USERID],[USR_OPOBOJ_USERNM])";
            sql += " VALUES ('" + codobs + "','" + fecha.ToString("yyyyMMdd") + "','" + tipoob + "','" + codcli + "','P','" + observ + "','" + coordn + "',GETDATE(),GETDATE(),'A','N','USR_OPOBOJ','ADMIN','"+usernm+"')";


            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["CBS"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - AltaObservacion(string codobs,string fecha, string nroleg, string codemp, string tipoob, string codcli, string observ, string coordn, string usernm))");
        }

    }
    
    //public static void Eliminarcliente(string id_cliente)
    //{
    //    try
    //    {


    //        string sql = " DELETE FROM [Ventas_especiales_Clientes] WHERE [id_cliente]=" + id_cliente;



    //        DataTable dtMenuItems = new DataTable();
    //        string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
    //        SqlCommand daMenu = new SqlCommand(sql);
    //        daMenu.Connection = new SqlConnection(strConnString);
    //        daMenu.Connection.Open();
    //        daMenu.ExecuteNonQuery();
    //        daMenu.Connection.Close();
    //        daMenu.Dispose();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception(ex.Message + " - Eliminarcliente(string id_cliente)");
    //    }

    //}
    public static void EditarObservacion(string codobs,string tipoob, string codcli, string observ, string coordn)
    {
        try
        {


            string sql = "UPDATE [USR_OPOBOJ] ";
            sql += " SET USR_OPOBOJ_TIPOOB='" + tipoob + "'";
            sql += " ,USR_OPOBOJ_CODCLI='" + codcli + "'";
            sql += " ,USR_OPOBOJ_OBSERV='" + observ + "'";
            sql += " ,USR_OPOBOJ_COORDN='" + coordn + "'";
            sql += " WHERE USR_OPOBOJ_CODOBS='" + codobs + "'";


            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["CBS"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - EditarObservacion(string codobs,string tipoob, string codcli, string observ, string coordn)");
        }

    }
    public static void AltaCaptura_Observacion(string codobs, string nombre_imagen, string usuario)
    {
        try
        {
           //string url = "http://192.168.1.141:8080/Archivos/Capturas/" + codobs + "/" + nombre_imagen;
           string url = @"\\192.168.1.141\intranet_cbs\Archivos\Capturas\" +usuario + @"\" + codobs + @"\" + nombre_imagen;
		DateTime Fecha=DateTime.Now;

        string sql = "INSERT INTO [USR_OPOBCA] ([USR_OPOBCA_CODOBS],[USR_OPOBCA_URLOBS],[USR_OPOBCA_CPTITL],[USR_OPOBCA_FCHALT],[USR_OP_FECALT],[USR_OP_FECMOD],[USR_OP_ULTOPR],[USR_OP_DEBAJA],[USR_OP_OALIAS],[USR_OP_USERID],[USR_OPOBCA_USERNM])";
            sql += " VALUES ('" + codobs + "','" + url + "','" + nombre_imagen + "','" + Fecha.ToString("yyyyMMdd 00:00:00.000") + "',GETDATE(),GETDATE(),'A','N','USR_OPOBCA','ADMIN','" + usuario + "')";




            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["CBS"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - AltaCaptura_Observacion(string codobs, string url)");
        }

    }
    public static void EliminarClientePorUsuario(string usuario)
    {
        try
        {


            string sql = "DELETE FROM USUARIO_CLIENTES ";
            sql += " WHERE usuario='" + usuario + "'";


            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - EliminarClientePorUsuario(string usuario)");
        }

    }
public static void EliminarObjetivoPorUsuario(string usuario)
    {
        try
        {


            string sql = "DELETE FROM USUARIO_OBJETIVO ";
            sql += " WHERE usuario='" + usuario + "'";


            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - EliminarClientePorUsuario(string usuario)");
        }

    }
    public static void AgregarClientePorUsuario(string usuario, string cliente)
    {
        try
        {

            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand();
            daMenu.Connection = new SqlConnection(strConnString);
            string sql = "";


            daMenu.Connection.Open();

            sql = " INSERT INTO USUARIO_CLIENTES (usuario, cliente) ";
            sql += " VALUES ('" + usuario + "','" + cliente + "')";
            daMenu.CommandText = sql;
            daMenu.ExecuteNonQuery();



            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message + " - AgregarPerfil(string descripcion, List<int> menuid)");
        }

    }
 public static void AgregarObjetivoPorUsuario(string usuario, string objetivo)
    {
        try
        {

            string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand daMenu = new SqlCommand();
            daMenu.Connection = new SqlConnection(strConnString);
            string sql = "";


            daMenu.Connection.Open();

            sql = " INSERT INTO USUARIO_OBJETIVO (usuario, objetivo) ";
            sql += " VALUES ('" + usuario + "','" + objetivo + "')";
            daMenu.CommandText = sql;
            daMenu.ExecuteNonQuery();



            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message + " - AgregarPerfil(string descripcion, List<int> menuid)");
        }

    }
    #endregion

}