using System;
using System.Drawing;
using System.IO;
using DevExpress.Web;
using System.Data;
using System.Collections.Generic;
using System.Web;

public partial class RRHH_ARCHIVO_CARGA : System.Web.UI.Page
{
    const string UploadDirectory = "~/Archivos/DIGITALIZACION";
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null)
        {

            Response.Redirect("INTRANET_LOGIN.aspx");
        }
        if (!IsPostBack)
        {

            /*DataTable dt = Interfaz.EjecutarConsultaBD("CBS", "select ID=Replace(SJMLGH_NROLEG,' ',''), NOMBRE=Replace(Replace(Replace(SJMLGH_NOMBRE,',',''),'_',''),' ','_') + '_' + Replace(SJMLGH_NROLEG,' ','') from SJMLGH with(nolock) ORDER BY SJMLGH_NOMBRE");
            cmb_legajos.Items.Add(new ListEditItem("SELECCIONE", "SELECCIONE"));
            foreach (DataRow dr in dt.Rows)
            {
                  cmb_legajos.Items.Add(new ListEditItem(dr["NOMBRE"].ToString(), dr["ID"].ToString()));
             }
          cmb_legajos.SelectedIndex=0;*/

            cmb_tipo.Items.Add(new ListEditItem("SELECCIONE", "SELECCIONE"));
            cmb_tipo.Items.Add(new ListEditItem("BAJA", "BAJA"));
            cmb_tipo.SelectedIndex = 0;

            cmb_empresa.Items.Add(new ListEditItem("SELECCIONE", "SELECCIONE"));
            cmb_empresa.Items.Add(new ListEditItem("CBS SRL", "CBS SRL"));
            cmb_empresa.Items.Add(new ListEditItem("BARCELO", "BARCELO"));
            cmb_empresa.Items.Add(new ListEditItem("CASA DE LA COSTA", "CASA DE LA COSTA"));
            cmb_empresa.SelectedIndex = 0;

            cmb_año.Items.Add(new ListEditItem("SELECCIONE", "SELECCIONE"));
            cmb_año.Items.Add(new ListEditItem("2016", "2016"));
            cmb_año.Items.Add(new ListEditItem("2015", "2015"));
            cmb_año.Items.Add(new ListEditItem("2014", "2014"));
            cmb_año.Items.Add(new ListEditItem("2013", "2013"));
            cmb_año.Items.Add(new ListEditItem("2012", "2012"));
            cmb_año.Items.Add(new ListEditItem("2011", "2011"));
            cmb_año.Items.Add(new ListEditItem("2010", "2010"));
            cmb_año.Items.Add(new ListEditItem("2009", "2009"));
            cmb_año.Items.Add(new ListEditItem("2008", "2008"));
            cmb_año.Items.Add(new ListEditItem("2007", "2007"));
            cmb_año.Items.Add(new ListEditItem("2006", "2006"));
            cmb_año.Items.Add(new ListEditItem("2005", "2005"));
            cmb_año.Items.Add(new ListEditItem("2004", "2004"));
            cmb_año.Items.Add(new ListEditItem("2003", "2003"));
            cmb_año.Items.Add(new ListEditItem("2002", "2002"));
            cmb_año.Items.Add(new ListEditItem("2001", "2001"));
            cmb_año.Items.Add(new ListEditItem("2000", "2000"));
            cmb_año.Items.Add(new ListEditItem("1999", "1999"));
            cmb_año.Items.Add(new ListEditItem("1998", "1998"));

            cmb_año.SelectedIndex = 0;

            fileManager.SettingsEditing.AllowMove = true;
            fileManager.SettingsEditing.AllowDelete = true;
            fileManager.SettingsEditing.AllowRename = true;
            fileManager.SettingsEditing.AllowCreate = false;
            fileManager.SettingsEditing.AllowDownload = true;
            fileManager.SettingsToolbar.ShowPath = false;
            fileManager.SettingsFolders.EnableCallBacks = true;
            fileManager.SettingsFolders.Visible = false;
            fileManager.SettingsUpload.Enabled = false;
            //fileManager.SettingsUpload.UseAdvancedUploadMode = true;
            //fileManager.SettingsUpload.AdvancedModeSettings.EnableMultiSelect = true;


        }
        



    }

    protected void cmb_legajos_SelectedIndexChanged(object sender, EventArgs e)
    {
	 fileManager.Settings.RootFolder = Server.MapPath(UploadDirectory);
            FailureText.Text = "";
        if (cmb_empresa.SelectedItem.Text != "SELECCIONE" && cmb_tipo.SelectedItem.Text != "SELECCIONE" && cmb_año.SelectedItem.Text != "SELECCIONE")
        {
            string directory = UploadDirectory + "/" + cmb_empresa.SelectedItem.Text + "/" + cmb_tipo.SelectedItem.Text + "/" + cmb_año.SelectedItem.Text;
            if (Directory.Exists(Server.MapPath(UploadDirectory + "/" + cmb_empresa.SelectedItem.Text + "/" + cmb_tipo.SelectedItem.Text + "/" + cmb_año.SelectedItem.Text)))
            {
                fileManager.Settings.RootFolder = Server.MapPath(UploadDirectory + "/" + cmb_empresa.SelectedItem.Text + "/" + cmb_tipo.SelectedItem.Text + "/" + cmb_año.SelectedItem.Text);
            }
        }
        
    }



    protected void fileManager_FileUploading(object sender, FileManagerFileUploadEventArgs e)
    {
        string filePath = MapPath(e.File.FullName);
        if (File.Exists(filePath))
            File.Delete(filePath);
        ValidateSiteEdit(e);
    }


    protected void fileManager_ItemRenaming(object sender, FileManagerItemRenameEventArgs e)
    {
        ValidateSiteEdit(e);
    }

    protected void fileManager_ItemMoving(object sender, FileManagerItemMoveEventArgs e)
    {
        ValidateSiteEdit(e);
    }

    protected void fileManager_ItemDeleting(object sender, FileManagerItemDeleteEventArgs e)
    {
        ValidateSiteEdit(e);
    }

    protected void fileManager_FolderCreating(object sender, FileManagerFolderCreateEventArgs e)
    {
        ValidateSiteEdit(e);
    }

    protected void fileManager_ItemCopying(object sender, FileManagerItemCopyEventArgs e)
    {
        ValidateSiteEdit(e);
    }





    protected void ValidateSiteEdit(FileManagerActionEventArgsBase e)
    {
        //  e.Cancel = Utils.IsSiteMode;
        // e.ErrorText = Utils.GetReadOnlyMessageText();
    }
        
  
    

    protected void btn_enviar_Click(object sender, EventArgs e)
    {
        try
        {
            if (cmb_empresa.SelectedItem.Text != "SELECCIONE" && cmb_tipo.SelectedItem.Text != "SELECCIONE" && cmb_año.SelectedItem.Text != "SELECCIONE" )
            {
                FailureText.Text = "";
               

                if (!Directory.Exists(Server.MapPath(UploadDirectory + "/" + cmb_empresa.SelectedItem.Text + "/" + cmb_tipo.SelectedItem.Text)))
                {
                    Directory.CreateDirectory(Server.MapPath(UploadDirectory + "/" + cmb_empresa.SelectedItem.Text + "/" + cmb_tipo.SelectedItem.Text));
                }

                if (!Directory.Exists(Server.MapPath(UploadDirectory + "/" + cmb_empresa.SelectedItem.Text + "/" + cmb_tipo.SelectedItem.Text + "/" + cmb_año.SelectedItem.Text)))
                {
                    Directory.CreateDirectory(Server.MapPath(UploadDirectory + "/" + cmb_empresa.SelectedItem.Text + "/" + cmb_tipo.SelectedItem.Text + "/" + cmb_año.SelectedItem.Text));
                }

              
               
     

                foreach (HttpPostedFile postedFile in FileUpload1.PostedFiles)
                {
                    string fileName = Path.GetFileName(postedFile.FileName);
                    postedFile.SaveAs(Server.MapPath(UploadDirectory + "/" + cmb_empresa.SelectedItem.Text + "/" + cmb_tipo.SelectedItem.Text + "/" + cmb_año.SelectedItem.Text + "/" + fileName));
                }
                

               FailureText.Text = "Informacion enviada con éxito.";
               
               


                LimpiarPantalla();
            }
        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
           
        }
    }

    private void LimpiarPantalla()
    {
        try
        {
            cmb_empresa.SelectedIndex = 0;
            //cmb_legajos.SelectedIndex = 0;
            cmb_año.SelectedIndex = 0;
            cmb_tipo.SelectedIndex = 0;
          

            fileManager.Settings.RootFolder = Server.MapPath(UploadDirectory);

        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
           
        }
    }
}