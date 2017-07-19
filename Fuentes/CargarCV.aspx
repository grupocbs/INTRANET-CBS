<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CargarCV.aspx.cs" Inherits="CargarCV" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="css/DragAndDrop.css" />
    <script type="text/javascript">
        function onUploadControlFileUploadComplete(s, e) {
            if (e.isValid)
            //document.getElementById("uploadedImage").src = "Archivos/CV/Fotos/" + e.callbackData;
                document.getElementById("uploadedImage").src = "tmp/" + e.callbackData;
            setElementVisible("uploadedImage", e.isValid);
        }
        function onImageLoad() {
            var externalDropZone = document.getElementById("externalDropZone");
            var uploadedImage = document.getElementById("uploadedImage");
            uploadedImage.style.left = (externalDropZone.clientWidth - uploadedImage.width) / 2 + "px";
            uploadedImage.style.top = (externalDropZone.clientHeight - uploadedImage.height) / 2 + "px";
            setElementVisible("dragZone", false);
        }
        function setElementVisible(elementId, visible) {
            document.getElementById(elementId).className = visible ? "" : "hidden";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dx:ASPxFormLayout runat="server" ID="formLayout" CssClass="formLayout" SettingsItems-HorizontalAlign="Center"
            SettingsItems-Width="1024px">
            <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
            <Items>
                <dx:LayoutItem Caption="">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer>
                            <table align="center">
                                <tr>
                                    <td>
                                        <dx:ASPxImage ID="img_logo" ImageUrl="imagenes/logo_header.png" Width="200px" runat="server">
                                        </dx:ASPxImage>
                                    </td>
                                </tr>
                            </table>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutGroup Caption="Ingrese la información de su CV" GroupBoxDecoration="HeadingLine"
                    ColCount="1" UseDefaultPaddings="false" Paddings-PaddingTop="10">
                    <GroupBoxStyle>
                        <Caption Font-Bold="true" Font-Size="14" />
                    </GroupBoxStyle>
                </dx:LayoutGroup>
                <dx:LayoutGroup Caption="DATOS PERSONALES" GroupBoxDecoration="box">
                    <Items>
                        <dx:LayoutItem Caption="">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxLabel ID="FailureText" runat="server" ForeColor="Red" Font-Size="14">
                                    </dx:ASPxLabel>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Nombre y Apellido">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxTextBox ID="txt_nombreyapellido" runat="server" Width="400px">
                                        <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom"
                                            SetFocusOnError="true">
                                            <ErrorFrameStyle Font-Size="Smaller" />
                                            <RequiredField IsRequired="True" ErrorText="Nombre y Apellido es requerido" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="DNI">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxTextBox ID="txt_dni" runat="server" Width="100px">
                                        <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom"
                                            SetFocusOnError="true">
                                            <ErrorFrameStyle Font-Size="Smaller" />
                                            <RequiredField IsRequired="True" ErrorText="DNI es requerido" />
                                        </ValidationSettings>
                                        <MaskSettings Mask="########" />
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Edad">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxTextBox ID="txt_edad" runat="server" Width="50px">
                                        <MaskSettings Mask="<0..99>" />
                                        <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom"
                                            SetFocusOnError="true">
                                            <ErrorFrameStyle Font-Size="Smaller" />
                                            <RequiredField IsRequired="True" ErrorText="Edad es requerida" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Sexo">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxRadioButtonList ID="rbtl_sexo" runat="server" Width="100px" RepeatDirection="Horizontal">
                                        <Items>
                                            <dx:ListEditItem Text="Masculino" Value="M" Selected="true" />
                                            <dx:ListEditItem Text="Femenino" Value="F" />
                                        </Items>
                                    </dx:ASPxRadioButtonList>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Fecha de Nacimiento">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxDateEdit ID="txt_fechanacimiento" runat="server" UseMaskBehavior="true" MinDate="01/01/1900"
                                        Width="120px" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                        <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom"
                                            SetFocusOnError="true">
                                            <ErrorFrameStyle Font-Size="Smaller" />
                                            <RequiredField IsRequired="True" ErrorText="Fecha de Nacimiento es requerida" />
                                        </ValidationSettings>
                                    </dx:ASPxDateEdit>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Direccion">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxTextBox ID="txt_direccion" runat="server" Width="400px">
                                        <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom"
                                            SetFocusOnError="true">
                                            <ErrorFrameStyle Font-Size="Smaller" />
                                            <RequiredField IsRequired="True" ErrorText="Dirección es requerida" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <%--<dx:LayoutItem Caption="CODIGO POSTAL">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxTextBox ID="txt_codigopostal" runat="server" Width="50px">
                                        <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom"
                                            SetFocusOnError="true">
                                            <ErrorFrameStyle Font-Size="Smaller" />
                                            <RequiredField IsRequired="True" ErrorText="CODIGO POSTAL es requerido" />
                                        </ValidationSettings>
                                        <MaskSettings Mask="####" />
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>--%>
                        <dx:LayoutItem Caption="Telefono">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxTextBox ID="txt_telefono" runat="server" Width="200px">
                                        <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom"
                                            SetFocusOnError="true">
                                            <ErrorFrameStyle Font-Size="Smaller" />
                                            <RequiredField IsRequired="True" ErrorText="Telefono es requerido" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="E-Mail">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxTextBox ID="txt_mail" runat="server" Width="300px">
                                        <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom"
                                            SetFocusOnError="true">
                                            <ErrorFrameStyle Font-Size="Smaller" />
                                            <RegularExpression ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"
                                                ErrorText="E-Mail inválido" />
                                            <RequiredField IsRequired="True" ErrorText="E-mail es requerido" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:LayoutGroup>
                <dx:LayoutGroup Caption="ME POSTULO PARA" GroupBoxDecoration="box">
                    <Items>
                        <dx:LayoutItem Caption="">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
				
                                    <dx:ASPxRadioButtonList ID="rbtl_postulacion" AutoPostBack="true" runat="server"
                                        OnSelectedIndexChanged="rbtl_postulacion_SelectedIndexChanged" Width="100px"
                                        RepeatDirection="Horizontal">
                                        <Items>
                                            <dx:ListEditItem Text="MAESTRANZA" Value="01" />
                                            <dx:ListEditItem Text="VIGILANCIA" Value="02" />
                                            <dx:ListEditItem Text="ADMINISTRACION" Value="03" />
                                            <dx:ListEditItem Text="LOGISTICA" Value="04" />
                                            <dx:ListEditItem Text="TECNICOS" Value="05" />
                                            <dx:ListEditItem Text="RRHH" Value="06" />
                                            <dx:ListEditItem Text="OPERARIOS" Value="07" />
                                            <dx:ListEditItem Text="PROFESIONAL" Value="08" />
                                        </Items>
                                    </dx:ASPxRadioButtonList>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:LayoutGroup>
                <dx:LayoutGroup Caption="REQUISITOS PARA INGRESO" GroupBoxDecoration="box">
                    <Items>
                        <dx:LayoutItem Caption="">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <p id="txt_requisitos" width="100%" height="200px" runat="server">
                                    </p>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:LayoutGroup>
                <dx:LayoutGroup Caption="EXPERIENCIA PROFESIONAL" GroupBoxDecoration="box">
                    <Items>
                        <dx:LayoutItem Caption="">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxGridView ID="ASPxGridView1" runat="server" KeyFieldName="NRO" AutoGenerateColumns="false"
                                        ClientInstanceName="ASPxGridView1" Width="100%" OnRowDeleting="ASPxGridView1_RowDeleting"
                                        OnInitNewRow="ASPxGridView1_InitNewRow" OnRowInserting="ASPxGridView1_RowInserting"
                                        OnRowUpdating="ASPxGridView1_RowUpdating" OnInit="ASPxGridView1_Init" OnRowValidating="ASPxGridView1_RowValidating">
                                        <SettingsBehavior ConfirmDelete="True" />
                                        <SettingsText ConfirmDelete="Eliminar el registro?" />
                                        <SettingsEditing EditFormColumnCount="1">
                                        </SettingsEditing>
                                        <EditFormLayoutProperties>
                                            <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
                                        </EditFormLayoutProperties>
                                        <SettingsCommandButton NewButton-Text="AGREGAR EXPERIENCIA" EditButton-Text="EDITAR"
                                            DeleteButton-Text="QUITAR" UpdateButton-Text="ACEPTAR" CancelButton-Text="CANCELAR" />
                                        <Styles>
                                            <StatusBar CssClass="statusBar">
                                            </StatusBar>
                                        </Styles>
                                        <Columns>
                                            <dx:GridViewCommandColumn VisibleIndex="0" ShowDeleteButton="true" ShowNewButtonInHeader="true"
                                                ShowEditButton="true">
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn FieldName="NRO" VisibleIndex="0" ReadOnly="true">
                                                <%-- <EditFormSettings Visible="False" />--%>
                                                <PropertiesTextEdit Width="100px">
                                                </PropertiesTextEdit>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataComboBoxColumn FieldName="TIPO" VisibleIndex="1">
                                                <PropertiesComboBox Width="100px">
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataDateColumn FieldName="DESDE" VisibleIndex="2">
                                                <PropertiesDateEdit Width="150px" UseMaskBehavior="true" EditFormatString="dd/MM/yyyy"
                                                    DisplayFormatString="dd/MM/yyyy">
                                                </PropertiesDateEdit>
                                            </dx:GridViewDataDateColumn>
                                            <dx:GridViewDataDateColumn FieldName="HASTA" VisibleIndex="2">
                                                <PropertiesDateEdit Width="150px" UseMaskBehavior="true" EditFormatString="dd/MM/yyyy"
                                                    DisplayFormatString="dd/MM/yyyy">
                                                </PropertiesDateEdit>
                                            </dx:GridViewDataDateColumn>
                                            <dx:GridViewDataTextColumn FieldName="EMPRESA" VisibleIndex="3">
                                                <PropertiesTextEdit Width="400px" MaxLength="60">
                                                </PropertiesTextEdit>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataMemoColumn FieldName="FUNCIONES" VisibleIndex="4">
                                                <PropertiesMemoEdit Width="500px" MaxLength="600" Height="50px">
                                                </PropertiesMemoEdit>
                                            </dx:GridViewDataMemoColumn>
                                            <dx:GridViewDataTextColumn FieldName="REFERENCIA" VisibleIndex="5">
                                                <PropertiesTextEdit Width="400px" MaxLength="100">
                                                </PropertiesTextEdit>
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                    </dx:ASPxGridView>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:LayoutGroup>
                <dx:LayoutGroup Caption="FORMACION ACADEMICA" GroupBoxDecoration="box">
                    <Items>
                        <dx:LayoutItem Caption="PRIMARIO">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxRadioButtonList ID="rbtl_primario" runat="server" Width="100px" RepeatDirection="Horizontal">
                                        <Items>
                                            <dx:ListEditItem Text="Completo" Value="Completo" Selected="true" />
                                            <dx:ListEditItem Text="Incompleto" Value="Incompleto" />
                                        </Items>
                                    </dx:ASPxRadioButtonList>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="SECUNDARIO">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxRadioButtonList ID="rbtl_secundario" runat="server" Width="100px" RepeatDirection="Horizontal">
                                        <Items>
                                            <dx:ListEditItem Text="Completo" Value="Completo" Selected="true" />
                                            <dx:ListEditItem Text="Incompleto" Value="Incompleto" />
                                        </Items>
                                    </dx:ASPxRadioButtonList>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="TERCIARIO">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxMemo ID="ASPxMemo1" runat="server" Width="500px" Height="100px">
                                    </dx:ASPxMemo>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="INGLES">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxRadioButtonList ID="rbtl_ingles" runat="server" Width="100px" RepeatDirection="Horizontal">
                                        <Items>
                                            <dx:ListEditItem Text="Basico" Value="Basico" Selected="true" />
                                            <dx:ListEditItem Text="Intermedio" Value="Intermedio" />
                                            <dx:ListEditItem Text="Avanzado" Value="Avanzado" />
                                        </Items>
                                    </dx:ASPxRadioButtonList>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="FRANCES">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxRadioButtonList ID="rbtl_frances" runat="server" Width="100px" RepeatDirection="Horizontal">
                                        <Items>
                                            <dx:ListEditItem Text="Basico" Value="Basico" Selected="true" />
                                            <dx:ListEditItem Text="Intermedio" Value="Intermedio" />
                                            <dx:ListEditItem Text="Avanzado" Value="Avanzado" />
                                        </Items>
                                    </dx:ASPxRadioButtonList>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="PORTUGUES">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxRadioButtonList ID="rbtl_portugues" runat="server" Width="100px" RepeatDirection="Horizontal">
                                        <Items>
                                            <dx:ListEditItem Text="Basico" Value="Basico" Selected="true" />
                                            <dx:ListEditItem Text="Intermedio" Value="Intermedio" />
                                            <dx:ListEditItem Text="Avanzado" Value="Avanzado" />
                                        </Items>
                                    </dx:ASPxRadioButtonList>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="WORD">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxRadioButtonList ID="rbtl_word" runat="server" Width="100px" RepeatDirection="Horizontal">
                                        <Items>
                                            <dx:ListEditItem Text="Basico" Value="Basico" Selected="true" />
                                            <dx:ListEditItem Text="Intermedio" Value="Intermedio" />
                                            <dx:ListEditItem Text="Avanzado" Value="Avanzado" />
                                        </Items>
                                    </dx:ASPxRadioButtonList>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="EXCEL">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxRadioButtonList ID="rbtl_excel" runat="server" Width="100px" RepeatDirection="Horizontal">
                                        <Items>
                                            <dx:ListEditItem Text="Basico" Value="Basico" Selected="true" />
                                            <dx:ListEditItem Text="Intermedio" Value="Intermedio" />
                                            <dx:ListEditItem Text="Avanzado" Value="Avanzado" />
                                        </Items>
                                    </dx:ASPxRadioButtonList>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="INTERNET">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxRadioButtonList ID="rbtl_internet" runat="server" Width="100px" RepeatDirection="Horizontal">
                                        <Items>
                                            <dx:ListEditItem Text="Basico" Value="Basico" Selected="true" />
                                            <dx:ListEditItem Text="Intermedio" Value="Intermedio" />
                                            <dx:ListEditItem Text="Avanzado" Value="Avanzado" />
                                        </Items>
                                    </dx:ASPxRadioButtonList>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="OTROS CONOCIMIENTOS">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxMemo ID="txt_otros_conocimientos" runat="server" Width="500px" Height="100px">
                                    </dx:ASPxMemo>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:LayoutGroup>
                <dx:LayoutGroup Caption="ARCHIVOS" GroupBoxDecoration="box">
                    <Items>
                        <dx:LayoutItem Caption="Foto">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <div id="externalDropZone" class="dropZoneExternal">
                                        <div id="dragZone">
                                            <span class="dragZoneText">Mi Foto</span>
                                        </div>
                                        <img id="uploadedImage" src="#" class="hidden" alt="" onload="onImageLoad()" />
                                        <div id="dropZone" class="hidden">
                                            <span class="dropZoneText">Arrastre una foto aqui</span>
                                        </div>
                                    </div>
                                    <dx:ASPxUploadControl ID="UploadControl" ClientInstanceName="UploadControl" runat="server"
                                        UploadMode="Auto" AutoStartUpload="True" Width="350" ShowProgressPanel="True"
                                        CssClass="uploadControl" DialogTriggerID="externalDropZone" OnFileUploadComplete="UploadControl_FileUploadComplete">
                                        <AdvancedModeSettings EnableDragAndDrop="True" EnableFileList="False" EnableMultiSelect="False"
                                            ExternalDropZoneID="externalDropZone" DropZoneText="" />
                                        <ValidationSettings MaxFileSize="4194304" AllowedFileExtensions=".jpg, .jpeg, .gif, .png"
                                            ErrorStyle-CssClass="validationMessage" />
                                        <BrowseButton Text="Seleccione una Imagen para subir..." />
                                        <DropZoneStyle CssClass="uploadControlDropZone" />
                                        <ProgressBarStyle CssClass="uploadControlProgressBar" />
                                        <ClientSideEvents DropZoneEnter="function(s, e) { if(e.dropZone.id == 'externalDropZone') setElementVisible('dropZone', true); }"
                                            DropZoneLeave="function(s, e) { if(e.dropZone.id == 'externalDropZone') setElementVisible('dropZone', false); }"
                                            FileUploadComplete="onUploadControlFileUploadComplete"></ClientSideEvents>
                                    </dx:ASPxUploadControl>
                                    <br />
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Puede subir fotos en formato JPG, GIF, o PNG.">
                                    </dx:ASPxLabel>
                                    <br />
                                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Tamaño máximo de Archivo 4MB.">
                                    </dx:ASPxLabel>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="CV en Archivo">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxUploadControl ID="ASPxUploadControl1" ClientInstanceName="UploadControl"
                                        runat="server" UploadMode="Auto" AutoStartUpload="True" Width="350" ShowProgressPanel="True"
                                        CssClass="uploadControl" DialogTriggerID="externalDropZone" OnFileUploadComplete="ASPxUploadControl1_FileUploadComplete">
                                        <AdvancedModeSettings EnableDragAndDrop="True" EnableFileList="False" EnableMultiSelect="False"
                                            ExternalDropZoneID="externalDropZone" DropZoneText="" />
                                        <ValidationSettings MaxFileSize="4194304" AllowedFileExtensions=".pdf, .doc" ErrorStyle-CssClass="validationMessage" />
                                        <BrowseButton Text="Seleccione un Archivo para subir..." />
                                        <DropZoneStyle CssClass="uploadControlDropZone" />
                                        <ProgressBarStyle CssClass="uploadControlProgressBar" />
                                    </dx:ASPxUploadControl>
                                    <br />
                                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Puede subir su CV en formato PDF o DOC.">
                                    </dx:ASPxLabel>
                                    <br />
                                    <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="Tamaño máximo de Archivo 4MB.">
                                    </dx:ASPxLabel>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:LayoutGroup>
                <dx:LayoutGroup Caption="ENVIAR INFORMACION A GRUPO CBS" GroupBoxDecoration="box">
                    <Items>
                        <dx:LayoutItem Caption="">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <table align="right">
                                        <tr>
                                            <td>
                                                <dx:ASPxButton ID="btn_enviar" UseSubmitBehavior="false" runat="server" Text="Enviar CV"
                                                    Width="100px" OnClick="btn_enviar_Click">
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxLabel ID="FailureText2" runat="server" ForeColor="Red" Font-Size="14">
                                    </dx:ASPxLabel>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:LayoutGroup>
            </Items>
        </dx:ASPxFormLayout>
    </div>
    </form>
</body>
</html>
