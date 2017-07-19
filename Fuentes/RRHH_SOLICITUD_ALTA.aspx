<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RRHH_SOLICITUD_ALTA.aspx.cs"
    MasterPageFile="~/Site.master" Inherits="RRHH_SOLICITUD_ALTA" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
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
            font-family: Calibri;
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
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <dx:ASPxFormLayout runat="server" ID="formLayout" CssClass="formLayout" SettingsItems-HorizontalAlign="Center"
                    SettingsItems-Width="1024px">
                    <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
                    <Items>
                        <dx:LayoutGroup Caption="Ingresos - Formulario de Solicitud de Ingreso" GroupBoxDecoration="HeadingLine"
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
                                <dx:LayoutItem Caption="Reclutamientos Finalizados" CaptionStyle-Font-Bold="true">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxComboBox ID="cmb_solicitudes" runat="server" Width="100%" EnableCallbackMode="true"
                                                Border-BorderColor="Blue" Font-Size="Medium" IncrementalFilteringMode="StartsWith"
                                                OnSelectedIndexChanged="cmb_solicitudes_SelectedIndexChanged" AutoPostBack="true">
                                            </dx:ASPxComboBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Nº Alta">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxTextBox ID="txt_nro_alta" runat="server" Width="120px" ReadOnly="true">
                                            </dx:ASPxTextBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Razon Social">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxComboBox ID="cmb_razon_social" runat="server" Width="300px">
                                            </dx:ASPxComboBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Fecha Ingreso">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxDateEdit ID="txt_fecha_alta" runat="server" UseMaskBehavior="true" MinDate="01/01/1900"
                                                Width="120px" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy"
                                                HelpText="Es la Fecha efectiva de Ingreso con la cual se dara de alta en Sistemas y se realizará el contrato."
                                                HelpTextSettings-DisplayMode="Popup">
                                                <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom"
                                                    SetFocusOnError="true">
                                                    <ErrorFrameStyle Font-Size="Smaller" />
                                                    <RequiredField IsRequired="True" ErrorText="Requerido" />
                                                </ValidationSettings>
                                            </dx:ASPxDateEdit>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Fecha de Baja">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxDateEdit ID="txt_fecha_baja" runat="server" UseMaskBehavior="true" MinDate="01/01/1900"
                                                Width="120px" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                                <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom"
                                                    SetFocusOnError="true">
                                                    <ErrorFrameStyle Font-Size="Smaller" />
                                                </ValidationSettings>
                                            </dx:ASPxDateEdit>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="CUIL">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxTextBox ID="txt_cuil" runat="server" Width="120px" AutoPostBack="true" OnTextChanged="txt_cuil_TextChanged">
                                                <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom"
                                                    SetFocusOnError="true">
                                                    <ErrorFrameStyle Font-Size="Smaller" />
                                                    <RequiredField IsRequired="True" ErrorText="Requerido" />
                                                </ValidationSettings>
                                                <MaskSettings Mask="###########" />
                                            </dx:ASPxTextBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Apellido">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxTextBox ID="txt_apellido" runat="server" Width="400px">
                                                <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom"
                                                    SetFocusOnError="true">
                                                    <ErrorFrameStyle Font-Size="Smaller" />
                                                    <RequiredField IsRequired="True" ErrorText="Requerido" />
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Nombres">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxTextBox ID="txt_nombres" runat="server" Width="400px">
                                                <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom"
                                                    SetFocusOnError="true">
                                                    <ErrorFrameStyle Font-Size="Smaller" />
                                                    <RequiredField IsRequired="True" ErrorText="Requerido" />
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Domicilio Real">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxTextBox ID="txt_domicilio_real" runat="server" Width="400px">
                                                <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom"
                                                    SetFocusOnError="true">
                                                    <ErrorFrameStyle Font-Size="Smaller" />
                                                    <RequiredField IsRequired="True" ErrorText="Requerido" />
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Obra Social">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxComboBox ID="cmb_obra_social" runat="server" Width="400px" EnableCallbackMode="true"
                                                IncrementalFilteringMode="StartsWith">
                                            </dx:ASPxComboBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Convenio">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxComboBox ID="cmb_convenio" runat="server" Width="400px" EnableCallbackMode="true"
                                                IncrementalFilteringMode="StartsWith" OnSelectedIndexChanged="cmb_convenio_SelectedIndexChanged"
                                                AutoPostBack="true">
                                            </dx:ASPxComboBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Categorias">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxComboBox ID="cmb_categorias" runat="server" Width="400px" EnableCallbackMode="true"
                                                IncrementalFilteringMode="StartsWith">
                                            </dx:ASPxComboBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Centro de Costo/Sector">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxComboBox ID="cmb_centro_costo" runat="server" Width="400px" EnableCallbackMode="true"
                                                IncrementalFilteringMode="StartsWith">
                                            </dx:ASPxComboBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Tipo de Contrato">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxComboBox ID="cmb_tipo_contrato" runat="server" Width="400px" EnableCallbackMode="true"
                                                IncrementalFilteringMode="StartsWith">
                                            </dx:ASPxComboBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Ingreso Interno">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxCheckBox ID="chk_interno" runat="server" Text="" AutoPostBack="true" OnCheckedChanged="chk_interno_CheckedChanged">
                                            </dx:ASPxCheckBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Cliente">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxComboBox ID="cmb_cliente" runat="server" Width="400px" EnableCallbackMode="true"
                                                AutoPostBack="true" IncrementalFilteringMode="StartsWith" OnSelectedIndexChanged="cmb_cliente_SelectedIndexChanged">
                                            </dx:ASPxComboBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Objetivo">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxComboBox ID="cmb_objetivo" runat="server" Width="400px" EnableCallbackMode="true"
                                                IncrementalFilteringMode="StartsWith">
                                            </dx:ASPxComboBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Localidad">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxComboBox ID="cmb_localidad" runat="server" Width="400px" EnableCallbackMode="true"
                                                HelpText="Localidad del Objetivo" HelpTextSettings-DisplayMode="Popup" IncrementalFilteringMode="StartsWith">
                                            </dx:ASPxComboBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Observaciones">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxMemo ID="txt_observaciones" runat="server" Width="400px" Height="100px" DisplayFormatString="dd/MM/yyyy"
                                                HelpText="Indique todas las observaciones que considere reelevantes como: La persona cubre vacaciones? Licencias? periodo? tiempo? nuevo puesto, etc. "
                                                HelpTextSettings-DisplayMode="Popup">
                                            </dx:ASPxMemo>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                        </dx:LayoutGroup>
                        <dx:LayoutGroup Caption="HERRAMIENTAS INFORMATICAS" GroupBoxDecoration="box">
                            <Items>
                                <dx:LayoutItem Caption="">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxCheckBox ID="chk_utiliza_pc" runat="server" Text="Utiliza PC?" AutoPostBack="true"
                                                            OnCheckedChanged="chk_CheckedChanged">
                                                        </dx:ASPxCheckBox>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxTextBox ID="txt_obs_utiliza_pc" runat="server" Width="400px" Enabled="false"
                                                            HelpText="Puede describir el tipo de equipo que debera utilizar." HelpTextSettings-DisplayMode="Popup">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxCheckBox ID="chk_utiliza_cuenta_google" runat="server" Text="Comunicación por Mail con Clientes/Proveedores?"
                                                            AutoPostBack="true" OnCheckedChanged="chk_CheckedChanged">
                                                        </dx:ASPxCheckBox>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxTextBox ID="txt_obs_cuenta_google" runat="server" Width="400px" Enabled="false"
                                                            HelpText="Puede sugerir algun nombre para la cuenta de correo (@grupo-cbs.com)"
                                                            HelpTextSettings-DisplayMode="Popup">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxCheckBox ID="chk_utiliza_cuenta_local" runat="server" Text="Comunicación por Mail Interna?"
                                                            AutoPostBack="true" OnCheckedChanged="chk_CheckedChanged">
                                                        </dx:ASPxCheckBox>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxTextBox ID="txt_obs_cuenta_local" runat="server" Width="400px" Enabled="false"
                                                            HelpText="Puede sugerir algun nombre para la cuenta de correo (@grupocbs.com.ar)"
                                                            HelpTextSettings-DisplayMode="Popup">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxCheckBox ID="chk_utiliza_intranet" runat="server" Text="Utiliza Intranet?"
                                                            AutoPostBack="true" OnCheckedChanged="chk_CheckedChanged">
                                                        </dx:ASPxCheckBox>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxTextBox ID="txt_intranet" runat="server" Width="400px" Enabled="false" HelpText="Puede especificar que permisos debe tener"
                                                            HelpTextSettings-DisplayMode="Popup">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxCheckBox ID="chk_tango" runat="server" Text="Utiliza Tango?" AutoPostBack="true"
                                                            OnCheckedChanged="chk_CheckedChanged">
                                                        </dx:ASPxCheckBox>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxTextBox ID="txt_tango" runat="server" Width="400px" Enabled="false" HelpText="Puede especificar que permisos debe tener"
                                                            HelpTextSettings-DisplayMode="Popup">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxCheckBox ID="chk_softland" runat="server" Text="Utiliza Softland?" AutoPostBack="true"
                                                            OnCheckedChanged="chk_CheckedChanged">
                                                        </dx:ASPxCheckBox>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxTextBox ID="txt_softland" runat="server" Width="400px" Enabled="false" HelpText="Puede especificar que permisos debe tener"
                                                            HelpTextSettings-DisplayMode="Popup">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxCheckBox ID="chk_sisitema_rrhh" runat="server" Text="Utiliza Sistema de RRHH?"
                                                            AutoPostBack="true" OnCheckedChanged="chk_CheckedChanged">
                                                        </dx:ASPxCheckBox>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxTextBox ID="txt_sistema_rrhh" runat="server" Width="400px" Enabled="false"
                                                            HelpText="Puede especificar que permisos debe tener" HelpTextSettings-DisplayMode="Popup">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxCheckBox ID="chk_sistema_stock" runat="server" Text="Utiliza Sistema de Stock?"
                                                            AutoPostBack="true" OnCheckedChanged="chk_CheckedChanged">
                                                        </dx:ASPxCheckBox>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxTextBox ID="txt_sistema_stock" runat="server" Width="400px" Enabled="false"
                                                            HelpText="Puede especificar que permisos debe tener" HelpTextSettings-DisplayMode="Popup">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxCheckBox ID="chk_compartida" runat="server" Text="Utiliza Carpeta Compartida?"
                                                            AutoPostBack="true" OnCheckedChanged="chk_CheckedChanged">
                                                        </dx:ASPxCheckBox>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxTextBox ID="txt_compartida" runat="server" Width="400px" Enabled="false"
                                                            HelpText="Puede especificar a que sector puede ingresar" HelpTextSettings-DisplayMode="Popup">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxCheckBox ID="chk_office" runat="server" Text="Utiliza Ms Office?" AutoPostBack="true"
                                                            OnCheckedChanged="chk_CheckedChanged">
                                                        </dx:ASPxCheckBox>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxTextBox ID="txt_office" runat="server" Width="400px" Enabled="false">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxCheckBox ID="chk_impresora" runat="server" Text="Utiliza Impresora?" AutoPostBack="true"
                                                            OnCheckedChanged="chk_CheckedChanged">
                                                        </dx:ASPxCheckBox>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxTextBox ID="txt_impresora" runat="server" Width="400px" Enabled="false">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxCheckBox ID="chk_scanner" runat="server" Text="Utiliza Scanner?" AutoPostBack="true"
                                                            OnCheckedChanged="chk_CheckedChanged">
                                                        </dx:ASPxCheckBox>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxTextBox ID="txt_scanner" runat="server" Width="400px" Enabled="false">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxCheckBox ID="chk_smartphone" runat="server" Text="Utiliza Smartphone Corporativa?"
                                                            AutoPostBack="true" OnCheckedChanged="chk_CheckedChanged">
                                                        </dx:ASPxCheckBox>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxTextBox ID="txt_smartphone" runat="server" Width="400px" Enabled="false">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxCheckBox ID="chk_app" runat="server" Text="Utiliza App Seguridad?" AutoPostBack="true"
                                                            OnCheckedChanged="chk_CheckedChanged">
                                                        </dx:ASPxCheckBox>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxTextBox ID="txt_app" runat="server" Width="400px" Enabled="false">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                        </dx:LayoutGroup>
                        <dx:LayoutGroup Caption="" GroupBoxDecoration="box">
                            <Items>
                                <dx:LayoutItem Caption="">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <table align="left">
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
                    </Items>
                </dx:ASPxFormLayout>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="200">
        <ProgressTemplate>
            <div id="background">
            </div>
            <div id="process">
                <h3>
                    <p style="text-align: center">
                        <b>Espere por favor...</br></b></p>
                </h3>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
