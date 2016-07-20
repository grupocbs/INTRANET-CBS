<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Notificaciones.aspx.cs" Inherits="Notificaciones"
    MasterPageFile="~/Site.master" %>

<%@ Register Assembly="DevExpress.Web.ASPxHtmlEditor.v15.1, Version=15.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxHtmlEditor" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxSpellChecker.v15.1, Version=15.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxSpellChecker" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table align="center" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <table align="center" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td valign="top">
                            <h2>
                                Mis Notificaciones Recibidas
                            </h2>
                            <ajaxToolkit:Accordion ID="Accordion1" runat="server" FadeTransitions="True" FramesPerSecond="50"
                                HeaderCssClass="headerAccordion" ContentCssClass="contentAccordion">
                                <Panes>
                                </Panes>
                            </ajaxToolkit:Accordion>
                        </td>
                    </tr>
                  <%--  <tr>
                        <td valign="top">
                            <h2>
                                Compartida
                            </h2>
                            <dx:ASPxFileManager ID="fileManager" runat="server" OnFolderCreating="fileManager_FolderCreating"
                                OnItemDeleting="fileManager_ItemDeleting" OnItemMoving="fileManager_ItemMoving"
                                OnInit="fileManager_Init" OnItemRenaming="fileManager_ItemRenaming" OnFileUploading="fileManager_FileUploading">
                                <Settings RootFolder="" ThumbnailFolder="~/Content/FileManager/Thumbnails" />
                                <SettingsEditing AllowDownload="true" />
                                <SettingsFolders EnableCallBacks="True" />
                                <SettingsPermissions>
                                    <AccessRules>
                                        <dx:FileManagerFolderAccessRule Path="System" Edit="Deny" />
                                    </AccessRules>
                                </SettingsPermissions>
                                <SettingsUpload UseAdvancedUploadMode="true">
                                    <AdvancedModeSettings EnableMultiSelect="true" />
                                </SettingsUpload>
                                <ClientSideEvents SelectedFileOpened="function(s, e) {
	e.file.Download();
	e.processOnServer = false;
}" />
                            </dx:ASPxFileManager>
                        </td>
                    </tr>--%>
                </table>
            </td>
        </tr>
    </table>
    <span class="failureNotification">
        <asp:Literal ID="FailureText" runat="server"></asp:Literal>
    </span>
</asp:Content>
