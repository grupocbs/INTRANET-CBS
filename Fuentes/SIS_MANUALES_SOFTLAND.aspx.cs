using System;
using System.Drawing;
using System.IO;
using DevExpress.Web;
using System.Data;
using System.Collections.Generic;
using System.Web;

public partial class SIS_MANUALES_SOFTLAND: System.Web.UI.Page
{

    const string UploadDirectory = "~/Archivos/SIS/SOFTLAND/MANUALES";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null)
        {

            Response.Redirect("INTRANET_LOGIN.aspx");
        }
        if (!IsPostBack)
        {
             

            

            fileManager.SettingsEditing.AllowMove = false;
            fileManager.SettingsEditing.AllowDelete = false;
            fileManager.SettingsEditing.AllowRename = false;
            fileManager.SettingsEditing.AllowCreate = false;
            fileManager.SettingsEditing.AllowDownload = true;
            fileManager.SettingsToolbar.ShowPath = false;
            fileManager.SettingsFolders.EnableCallBacks = true;
            fileManager.SettingsFolders.Visible = false;
            fileManager.SettingsUpload.Enabled = false;

            
                if (Directory.Exists(Server.MapPath(UploadDirectory)))
                {
                    fileManager.Settings.RootFolder = Server.MapPath(UploadDirectory);
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
        
  
    

}