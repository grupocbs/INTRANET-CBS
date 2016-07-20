using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mail;
using System.Configuration;
using AjaxControlToolkit;
using DevExpress.Web.ASPxHtmlEditor;
using System.Web.UI.HtmlControls;
using DevExpress.Web;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Collections;

public partial class Notificaciones : System.Web.UI.Page
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

                DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", "SELECT * FROM NOTIFICACIONES with(nolock) WHERE idusuario='" + Session["usuario"].ToString() + "' order by fecha desc");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    HyperLink lblTitle = new HyperLink();
                    lblTitle.NavigateUrl = "#";
                    lblTitle.Style.Add(HtmlTextWriterStyle.Color, "White");

                    lblTitle.Text = dt.Rows[i]["Titulo"].ToString() + '-' + dt.Rows[i]["Fecha"].ToString();

                    HtmlGenericControl p = new HtmlGenericControl("p");
                    p.ID = "P" + i;
                    p.InnerHtml = dt.Rows[i]["Notificacion"].ToString();


                    AccordionPane ap = new AccordionPane();
                    ap.ID = "Pane" + i;
                    ap.HeaderContainer.Controls.Add(lblTitle);
                    ap.ContentContainer.Controls.Add(p);
                    Accordion1.Panes.Add(ap);
                }




            }



/*
            fileManager.SettingsFolders.ShowFolderIcons = true;
            fileManager.SettingsEditing.AllowCreate = true;
            fileManager.SettingsEditing.AllowRename = true;
            fileManager.SettingsEditing.AllowMove = true;
            fileManager.SettingsEditing.AllowDelete = true;
            fileManager.SettingsUpload.Enabled = true;
            

            fileManager.SettingsFileList.View = FileListView.Details;*/

        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }
    }





    protected void fileManager_Init(object sender, EventArgs e)
    {

        try
        {
          /*  if (Directory.Exists(MapPath("~/Archivos/Compartida")))
            {
                fileManager.Settings.RootFolder = "~/Archivos/Compartida";
            }
            else
            {
                FailureText.Text = "Carpeta compartida no esta disponible";
            }*/
            
        }
        catch (Exception ex)
        {
            FailureText.Text = ex.Message;
        }
    }
          
    

    protected void nbMain_ItemClick(object source, DevExpress.Web.NavBarItemEventArgs e)
    {
        
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



    void ValidateSiteEdit(FileManagerActionEventArgsBase e)
    {
        //e.Cancel = Utils.IsSiteMode;
        //e.ErrorText = Utils.GetReadOnlyMessageText();
    }

     



     
   

}


