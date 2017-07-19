<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RRHH_ARCHIVO_CARGA.aspx.cs"
    MasterPageFile="~/Site.master" Inherits="RRHH_ARCHIVO_CARGA" Culture="es-AR"
    UICulture="es-AR" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <%--<style type="text/css">
        #background
        {
            position: fixed;
            top: 0px;
            bottom: 0px;
            left: 0px;
            right: 0px;
            overflow: hidden;
            padding: 0;
            margin: 0;
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }
        #process
        {
            position: fixed;
            top: 40%;
            left: 40%;
            height: 20%;
            width: 20%;
            z-index: 10001;
            background-color: White;
            border: 1px solid gray;
            background-image: url('imagenes/loading.gif');
            background-repeat: no-repeat;
            background-position: center;
        }
    </style>--%>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <%-- <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate> --%>
    <dx:ASPxFormLayout runat="server" ID="formLayout" CssClass="formLayout" SettingsItems-HorizontalAlign="Center"
        SettingsItems-Width="1024px">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
        <Items>
            <dx:LayoutGroup Caption="Carga Archivo Digitalizado" GroupBoxDecoration="HeadingLine"
                ColCount="1" UseDefaultPaddings="false" Paddings-PaddingTop="10">
                <GroupBoxStyle>
                    <Caption Font-Bold="true" Font-Size="14" />
                </GroupBoxStyle>
            </dx:LayoutGroup>
            <dx:LayoutGroup Caption="" GroupBoxDecoration="box">
                <Items>
                    <dx:LayoutItem Caption="">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxLabel ID="FailureText" runat="server" ForeColor="Red" Font-Size="14">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="EMPRESA">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxComboBox ID="cmb_empresa" runat="server" Width="300px" AutoPostBack="true"
                                    OnSelectedIndexChanged="cmb_legajos_SelectedIndexChanged">
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="TIPO DE ARCHIVO">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxComboBox ID="cmb_tipo" runat="server" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="cmb_legajos_SelectedIndexChanged">
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="AÑO">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxComboBox ID="cmb_año" runat="server" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="cmb_legajos_SelectedIndexChanged">
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Archivo">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <asp:FileUpload ID="FileUpload1" runat="server" AllowMultiple="true" />
                                <br />
                                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Puede subir archivos en formato PDF.">
                                </dx:ASPxLabel>
                                <br />
                                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="Tamaño máximo de Archivo 10MB.">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <table align="right">
                                    <tr>
                                        <td>
                                            <dx:ASPxButton ID="btn_enviar" UseSubmitBehavior="false" runat="server" Text="Enviar"
                                                Width="100px" OnClick="btn_enviar_Click">
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
            <dx:LayoutGroup Caption="Archivos Cargados" GroupBoxDecoration="box">
                <Items>
                    <dx:LayoutItem Caption="">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxFileManager ID="fileManager" runat="server" OnFolderCreating="fileManager_FolderCreating"
                                    Width="900px" OnItemDeleting="fileManager_ItemDeleting" OnItemMoving="fileManager_ItemMoving"
                                    SettingsFileList-View="Details" OnItemRenaming="fileManager_ItemRenaming" OnFileUploading="fileManager_FileUploading">
                                    <Settings RootFolder="~/Archivos/DIGITALIZACION" ThumbnailFolder="~/Content/FileManager/Thumbnails"
                                        AllowedFileExtensions=".pdf" InitialFolder="Images\Employees" />
                                    <SettingsPermissions>
                                        <AccessRules>
                                            <dx:FileManagerFolderAccessRule Path="System" Edit="Deny" />
                                        </AccessRules>
                                    </SettingsPermissions>
                                    <SettingsFileList ShowFolders="false" ShowParentFolder="false" />
                                    <SettingsBreadcrumbs Visible="false" ShowParentFolderButton="false" Position="Top" />
                                    <SettingsUpload UseAdvancedUploadMode="true">
                                        <AdvancedModeSettings EnableMultiSelect="true" />
                                    </SettingsUpload>
                                    <ClientSideEvents SelectedFileOpened="function(s, e) {
	e.file.Download();
	e.processOnServer = false;
}" />
                                </dx:ASPxFileManager>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
        </Items>
    </dx:ASPxFormLayout>
    <%-- </ContentTemplate></asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="200">
        <ProgressTemplate>
            <div id="background">
            </div>
            <div id="process">
                <h6>
                    <p style="text-align: center">
                        <b>Espere por favor...</br></b></p>
                </h6>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
</asp:Content>
