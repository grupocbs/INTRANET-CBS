using System;
using System.Drawing;
using System.IO;
using DevExpress.Web;
using System.Data;
using System.Collections.Generic;
using System.Web;

public partial class RRHH_NORMATIVAS_CARGA: System.Web.UI.Page
{
    const string UploadDirectory = "~/Archivos/NORMATIVAS";
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null)
        {

            Response.Redirect("INTRANET_LOGIN.aspx");
        }
        if (!IsPostBack)
        {

           

            cmb_tipo.Items.Add(new ListEditItem("SELECCIONE", "SELECCIONE"));
            cmb_tipo.Items.Add(new ListEditItem("SEGURIDAD", "SEGURIDAD"));
            cmb_tipo.Items.Add(new ListEditItem("MAESTRANZA", "MAESTRANZA"));
            cmb_tipo.SelectedIndex = 0;

            

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

    protected void cmb_tipo_SelectedIndexChanged(object sender, EventArgs e)
    {
	 fileManager.Settings.RootFolder = Server.MapPath(UploadDirectory);
            FailureText.Text = "";
            if (cmb_tipo.SelectedItem.Text != "SELECCIONE")
        {
            string directory = UploadDirectory + "/" + cmb_tipo.SelectedItem.Text;
            if (Directory.Exists(Server.MapPath(UploadDirectory + "/" + cmb_tipo.SelectedItem.Text)))
            {
                fileManager.Settings.RootFolder = Server.MapPath(UploadDirectory + "/" + cmb_tipo.SelectedItem.Text);
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
            if (cmb_tipo.SelectedItem.Text != "SELECCIONE")
            {
                FailureText.Text = "";


                if (!Directory.Exists(Server.MapPath(UploadDirectory + "/" + cmb_tipo.SelectedItem.Text)))
                {
                    Directory.CreateDirectory(Server.MapPath(UploadDirectory + "/" + cmb_tipo.SelectedItem.Text));
                }

               
     

                foreach (HttpPostedFile postedFile in FileUpload1.PostedFiles)
                {
                    string fileName = Path.GetFileName(postedFile.FileName);
                    postedFile.SaveAs(Server.MapPath(UploadDirectory + "/" + cmb_tipo.SelectedItem.Text + "/" + fileName));
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
           
            cmb_tipo.SelectedIndex = 0;
          

            fileManager.Settings.RootFolder = Server.MapPath(UploadDirectory);

        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
           
        }
    }
}