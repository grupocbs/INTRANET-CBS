<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RRHH_SOLICITUD_RECLUTAMIENTO_REPORTE_TODOS.aspx.cs"
    MasterPageFile="~/Site.master" Inherits="RRHH_SOLICITUD_RECLUTAMIENTO_REPORTE_TODOS"
    MaintainScrollPositionOnPostback="true" %>

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
                        <dx:LayoutGroup Caption="Reclutamientos - Solicitudes" GroupBoxDecoration="HeadingLine"
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
                                <dx:LayoutItem Caption="Solicitudes de Reclutamiento" CaptionStyle-Font-Bold="true">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <table>
                                                <tr>
                                                    <td style="padding: 4px">
                                                        <dx:ASPxComboBox ID="cmb_solicitudes" runat="server" Width="500px" EnableCallbackMode="true"
                                                            Border-BorderColor="Blue" Font-Size="Medium" IncrementalFilteringMode="StartsWith"
                                                            OnSelectedIndexChanged="cmb_solicitudes_SelectedIndexChanged" AutoPostBack="true">
                                                        </dx:ASPxComboBox>
                                                    </td>
                                                    <td style="padding: 4px">
                                                        <dx:ASPxButton ID="btn_eliminar" UseSubmitBehavior="false" runat="server" Text="Eliminar Solicitud"
                                                            Width="100px" OnClick="btn_eliminar_Click">
                                                        </dx:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                        </dx:LayoutGroup>
                        <dx:LayoutGroup Caption="Descripcion del Puesto" GroupBoxDecoration="box">
                            <Items>
                                <dx:LayoutItem Caption="Fecha de Solicitud">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxDateEdit ID="txt_fecha_solicitud" runat="server" UseMaskBehavior="true" MinDate="01/01/1900"
                                                Enabled="false" Width="120px" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                                <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom"
                                                    SetFocusOnError="true">
                                                    <ErrorFrameStyle Font-Size="Smaller" />
                                                    <RequiredField IsRequired="True" ErrorText="Requerido" />
                                                </ValidationSettings>
                                            </dx:ASPxDateEdit>
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
                                <dx:LayoutGroup Caption="Datos generales" GroupBoxDecoration="box">
                                    <Items>
                                        <dx:LayoutItem Caption="Gerencia / Área a la que pertenece">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxComboBox ID="cmb_area" runat="server" Width="300px" EnableCallbackMode="true"
                                                        OnSelectedIndexChanged="cmb_area_SelectedIndexChanged" AutoPostBack="true" IncrementalFilteringMode="StartsWith"
                                                        HelpText="Seleccione el Area que solicita" HelpTextSettings-DisplayMode="Popup">
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Nombre del Puesto">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxComboBox ID="cmb_nombre_puesto" runat="server" Width="300px" EnableCallbackMode="true"
                                                        OnSelectedIndexChanged="cmb_nombre_puesto_SelectedIndexChanged" AutoPostBack="true"
                                                        IncrementalFilteringMode="StartsWith" HelpText="Seleccione el Puesto que ocupara"
                                                        HelpTextSettings-DisplayMode="Popup">
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
                                                        <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom"
                                                            SetFocusOnError="true">
                                                            <ErrorFrameStyle Font-Size="Smaller" />
                                                            <RequiredField IsRequired="True" ErrorText="Requerido" />
                                                        </ValidationSettings>
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Categorias">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxComboBox ID="cmb_categorias" runat="server" Width="400px" EnableCallbackMode="true"
                                                        IncrementalFilteringMode="StartsWith">
                                                        <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom"
                                                            SetFocusOnError="true">
                                                            <ErrorFrameStyle Font-Size="Smaller" />
                                                            <RequiredField IsRequired="True" ErrorText="Requerido" />
                                                        </ValidationSettings>
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
                                        <dx:LayoutItem Caption="Fecha en la que debe ser cubierta la vacante">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxDateEdit ID="txt_fecha_vacante" runat="server" UseMaskBehavior="true" MinDate="01/01/1900"
                                                        Width="120px" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy"
                                                        HelpText="Ingrese la Fecha limite en la que debe ser cubierta la Vacante" HelpTextSettings-DisplayMode="Popup">
                                                        <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom"
                                                            SetFocusOnError="true">
                                                            <ErrorFrameStyle Font-Size="Smaller" />
                                                            <RequiredField IsRequired="True" ErrorText="Requerido" />
                                                        </ValidationSettings>
                                                    </dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Información sobre la vacante">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxRadioButtonList runat="server" ID="rbtl_tipo" Width="300px">
                                                        <Items>
                                                            <dx:ListEditItem Selected="true" Text="Creación del cargo" Value="Creación del cargo" />
                                                            <dx:ListEditItem Value="Reemplazo definitivo" Text="Reemplazo definitivo" />
                                                            <dx:ListEditItem Value="Reestructuración" Text="Reestructuración" />
                                                            <dx:ListEditItem Value="Temporales" Text="Temporales" />
                                                        </Items>
                                                    </dx:ASPxRadioButtonList>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="La Vacante se produjo por">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxRadioButtonList runat="server" ID="rbtl_motivo" Width="300px">
                                                        <Items>
                                                            <dx:ListEditItem Selected="true" Text="Renuncia del titular" Value="Renuncia del titular" />
                                                            <dx:ListEditItem Value="Promoción  o traslado" Text="Promoción  o traslado" />
                                                            <dx:ListEditItem Value="Incapacidad" Text="Incapacidad" />
                                                            <dx:ListEditItem Value="Vacaciones" Text="Vacaciones" />
                                                            <dx:ListEditItem Value="Licencia" Text="Licencia" />
                                                            <dx:ListEditItem Value="Cancelación del Contrato" Text="Cancelación del Contrato" />
                                                            <dx:ListEditItem Value="Nuevo Cargo" Text="Nuevo Cargo" />
                                                            <dx:ListEditItem Value="Lic. Por maternidad" Text="Lic. Por maternidad" />
                                                            <dx:ListEditItem Value="Incremento de Trabajo" Text="Incremento de Trabajo" />
                                                            <dx:ListEditItem Value="Otros" Text="Otros" />
                                                        </Items>
                                                    </dx:ASPxRadioButtonList>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                                <dx:LayoutGroup Caption="Aspectos Organizativos" GroupBoxDecoration="box">
                                    <Items>
                                        <dx:LayoutItem Caption="Dedicación especial">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxRadioButtonList runat="server" ID="rbtl_ded_esp" Width="300px" AutoPostBack="true"
                                                        OnSelectedIndexChanged="rbtl_ded_esp_SelectedIndexChanged">
                                                        <Items>
                                                            <dx:ListEditItem Selected="true" Text="Si" Value="Si" />
                                                            <dx:ListEditItem Value="No" Text="No" />
                                                            <dx:ListEditItem Value="Full time" Text="Full time" />
                                                            <dx:ListEditItem Value="Part time" Text="Part time" />
                                                        </Items>
                                                    </dx:ASPxRadioButtonList>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Cantidad de Horas">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxTextBox ID="txt_cant_hs" runat="server" Width="80px" Enabled="false" HelpText="Si es Part Time, ingrese la Cantidad de Horas diarias"
                                                        HelpTextSettings-DisplayMode="Popup">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Horario">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <table>
                                                        <tr>
                                                            <td valign="middle">
                                                                <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="De">
                                                                </dx:ASPxLabel>
                                                            </td>
                                                            <td style="padding: 2px" valign="middle">
                                                                <dx:ASPxTextBox ID="txt_horario_de" runat="server" Width="120px" HelpText="Su horario sera Desde"
                                                                    HelpTextSettings-DisplayMode="Popup">
                                                                    <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom"
                                                                        SetFocusOnError="true">
                                                                        <ErrorFrameStyle Font-Size="Smaller" />
                                                                        <RequiredField IsRequired="True" ErrorText="Requerido" />
                                                                    </ValidationSettings>
                                                                    <MaskSettings Mask="<00..23>:<00..59>" />
                                                                </dx:ASPxTextBox>
                                                            </td>
                                                            <td style="padding: 2px" valign="middle">
                                                                <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="a">
                                                                </dx:ASPxLabel>
                                                            </td>
                                                            <td style="padding: 2px" valign="middle">
                                                                <dx:ASPxTextBox ID="txt_horario_hasta" runat="server" Width="120px" HelpText="Hasta"
                                                                    HelpTextSettings-DisplayMode="Popup">
                                                                    <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom"
                                                                        SetFocusOnError="true">
                                                                        <ErrorFrameStyle Font-Size="Smaller" />
                                                                        <RequiredField IsRequired="True" ErrorText="Requerido" />
                                                                    </ValidationSettings>
                                                                    <MaskSettings Mask="<00..23>:<00..59>" />
                                                                </dx:ASPxTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="middle">
                                                                <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="y/o De">
                                                                </dx:ASPxLabel>
                                                            </td>
                                                            <td style="padding: 2px" valign="middle">
                                                                <dx:ASPxTextBox ID="txt_horario_yde" runat="server" Width="120px" HelpText="y/o Desde"
                                                                    HelpTextSettings-DisplayMode="Popup">
                                                                    <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom"
                                                                        SetFocusOnError="true">
                                                                        <ErrorFrameStyle Font-Size="Smaller" />
                                                                        <RequiredField IsRequired="True" ErrorText="Requerido" />
                                                                    </ValidationSettings>
                                                                    <MaskSettings Mask="<00..23>:<00..59>" />
                                                                </dx:ASPxTextBox>
                                                            </td>
                                                            <td style="padding: 2px" valign="middle">
                                                                <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="a">
                                                                </dx:ASPxLabel>
                                                            </td>
                                                            <td style="padding: 2px" valign="middle">
                                                                <dx:ASPxTextBox ID="txt_horario_yhasta" runat="server" Width="120px" HelpText="Hasta"
                                                                    HelpTextSettings-DisplayMode="Popup">
                                                                    <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom"
                                                                        SetFocusOnError="true">
                                                                        <ErrorFrameStyle Font-Size="Smaller" />
                                                                        <RequiredField IsRequired="True" ErrorText="Requerido" />
                                                                    </ValidationSettings>
                                                                    <MaskSettings Mask="<00..23>:<00..59>" />
                                                                </dx:ASPxTextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Días de trabajo">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <table>
                                                        <tr>
                                                            <td valign="middle">
                                                                <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="De">
                                                                </dx:ASPxLabel>
                                                            </td>
                                                            <td style="padding: 2px" valign="middle">
                                                                <dx:ASPxComboBox ID="cmb_dia_de" runat="server" Width="200px" EnableCallbackMode="true"
                                                                    IncrementalFilteringMode="StartsWith" HelpText="Desde el Dia" HelpTextSettings-DisplayMode="Popup">
                                                                    <Items>
                                                                        <dx:ListEditItem Value="Lunes" Text="Lunes" />
                                                                        <dx:ListEditItem Value="Martes" Text="Martes" />
                                                                        <dx:ListEditItem Value="Miercoles" Text="Miercoles" />
                                                                        <dx:ListEditItem Value="Jueves" Text="Jueves" />
                                                                        <dx:ListEditItem Value="Viernes" Text="Viernes" />
                                                                        <dx:ListEditItem Value="Sabado" Text="Sabado" />
                                                                        <dx:ListEditItem Value="Domingo" Text="Domingo" />
                                                                    </Items>
                                                                </dx:ASPxComboBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 2px" valign="middle">
                                                                <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="a">
                                                                </dx:ASPxLabel>
                                                            </td>
                                                            <td style="padding: 2px" valign="middle">
                                                                <dx:ASPxComboBox ID="cmb_dia_a" runat="server" Width="200px" EnableCallbackMode="true"
                                                                    IncrementalFilteringMode="StartsWith" HelpText="Al dia" HelpTextSettings-DisplayMode="Popup">
                                                                    <Items>
                                                                        <dx:ListEditItem Value="Lunes" Text="Lunes" />
                                                                        <dx:ListEditItem Value="Martes" Text="Martes" />
                                                                        <dx:ListEditItem Value="Miercoles" Text="Miercoles" />
                                                                        <dx:ListEditItem Value="Jueves" Text="Jueves" />
                                                                        <dx:ListEditItem Value="Viernes" Text="Viernes" />
                                                                        <dx:ListEditItem Value="Sabado" Text="Sabado" />
                                                                        <dx:ListEditItem Value="Domingo" Text="Domingo" />
                                                                    </Items>
                                                                </dx:ASPxComboBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Turno de trabajo">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxRadioButtonList runat="server" ID="rbtl_turno_t" Width="300px">
                                                        <Items>
                                                            <dx:ListEditItem Selected="true" Text="Fijo" Value="Fijo" />
                                                            <dx:ListEditItem Value="Rotativo" Text="Rotativo" />
                                                        </Items>
                                                    </dx:ASPxRadioButtonList>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Debe Viajar">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxRadioButtonList runat="server" ID="rbtl_deb_v" Width="300px" AutoPostBack="true"
                                                        OnSelectedIndexChanged="rbtl_deb_v_SelectedIndexChanged">
                                                        <Items>
                                                            <dx:ListEditItem Selected="true" Value="No" Text="No" />
                                                            <dx:ListEditItem Text="Si" Value="Si" />
                                                        </Items>
                                                    </dx:ASPxRadioButtonList>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Zonas">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxMemo ID="txt_zonas" runat="server" Width="100%" Enabled="false" Height="80px"
                                                        HelpText="Ingrese las Zonas por las que debe Viajar" HelpTextSettings-DisplayMode="Popup">
                                                    </dx:ASPxMemo>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Debe conducir">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxRadioButtonList runat="server" ID="rbtl_deb_c" Width="300px" AutoPostBack="true"
                                                        OnSelectedIndexChanged="rbtl_deb_c_SelectedIndexChanged">
                                                        <Items>
                                                            <dx:ListEditItem Selected="true" Value="No" Text="No" />
                                                            <dx:ListEditItem Text="Si" Value="Si" />
                                                        </Items>
                                                    </dx:ASPxRadioButtonList>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Con movilidad propia">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxRadioButtonList runat="server" ID="rbtl_deb_cm" Width="300px" Enabled="false">
                                                        <Items>
                                                            <dx:ListEditItem Selected="true" Text="Si" Value="Si" />
                                                            <dx:ListEditItem Value="No" Text="No" />
                                                        </Items>
                                                    </dx:ASPxRadioButtonList>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                                <dx:LayoutGroup Caption="Contenido del puesto de trabajo" GroupBoxDecoration="box">
                                    <Items>
                                        <dx:LayoutItem Caption="">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <table width="100%">
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxMemo ID="txt_objetivo" runat="server" Width="100%" Height="50px" HelpText="Principal misión u objetivo del puesto. El resultado global que da sentido y razón de ser al puesto"
                                                                    HelpTextSettings-DisplayMode="Popup">
                                                                    <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom"
                                                                        SetFocusOnError="true">
                                                                        <ErrorFrameStyle Font-Size="Smaller" />
                                                                        <RequiredField IsRequired="True" ErrorText="Requerido" />
                                                                    </ValidationSettings>
                                                                </dx:ASPxMemo>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                                <dx:LayoutGroup Caption="Responsabilidades" GroupBoxDecoration="box">
                                    <Items>
                                        <dx:LayoutItem Caption="">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <table width="100%">
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxMemo ID="txt_responsabilildades" runat="server" Width="100%" Height="50px"
                                                                    HelpText="Directas derivadas del puesto de trabajo" HelpTextSettings-DisplayMode="Popup">
                                                                    <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom"
                                                                        SetFocusOnError="true">
                                                                        <ErrorFrameStyle Font-Size="Smaller" />
                                                                        <RequiredField IsRequired="True" ErrorText="Requerido" />
                                                                    </ValidationSettings>
                                                                </dx:ASPxMemo>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                                <dx:LayoutGroup Caption="Competencias" GroupBoxDecoration="box">
                                    <Items>
                                        <dx:LayoutItem Caption="">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <table width="100%">
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxMemo ID="txt_funciones" runat="server" Width="100%" Height="50px" HelpText="A continuación detallar con precisión las competencias requeridas para el puesto."
                                                                    HelpTextSettings-DisplayMode="Popup">
                                                                    <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom"
                                                                        SetFocusOnError="true">
                                                                        <ErrorFrameStyle Font-Size="Smaller" />
                                                                        <RequiredField IsRequired="True" ErrorText="Requerido" />
                                                                    </ValidationSettings>
                                                                </dx:ASPxMemo>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                                <dx:LayoutGroup Caption="Relación que mantiene con" GroupBoxDecoration="box">
                                    <Items>
                                        <dx:LayoutItem Caption="">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <table width="100%">
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxMemo ID="txt_relacion" runat="server" Width="100%" Height="50px" HelpText="Detallar con quién y cómo se relaciona (Jefe inmediato, contribuyentes, compañeros de trabajo, gente a su cargo, Otros)"
                                                                    HelpTextSettings-DisplayMode="Popup">
                                                                    <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom"
                                                                        SetFocusOnError="true">
                                                                        <ErrorFrameStyle Font-Size="Smaller" />
                                                                        <RequiredField IsRequired="True" ErrorText="Requerido" />
                                                                    </ValidationSettings>
                                                                </dx:ASPxMemo>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                            </Items>
                        </dx:LayoutGroup>
                        <dx:LayoutGroup Caption="Requerimientos del puesto" GroupBoxDecoration="box">
                            <Items>
                                <dx:LayoutGroup Caption="Generales" GroupBoxDecoration="box">
                                    <Items>
                                        <dx:LayoutItem Caption="Edad Minima">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxTextBox ID="txt_edad_minima" runat="server" Width="50px" HelpText="Ingrese la Edad Minima requerida"
                                                        HelpTextSettings-DisplayMode="Popup">
                                                        <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom"
                                                            SetFocusOnError="true">
                                                            <ErrorFrameStyle Font-Size="Smaller" />
                                                            <RequiredField IsRequired="True" ErrorText="Requerido" />
                                                        </ValidationSettings>
                                                        <MaskSettings Mask="<18..70>" />
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Edad MAxima">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxTextBox ID="txt_edad_maxima" runat="server" Width="50px" HelpText="Ingrese la Edad Maxima requerida"
                                                        HelpTextSettings-DisplayMode="Popup">
                                                        <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom"
                                                            SetFocusOnError="true">
                                                            <ErrorFrameStyle Font-Size="Smaller" />
                                                            <RequiredField IsRequired="True" ErrorText="Requerido" />
                                                        </ValidationSettings>
                                                        <MaskSettings Mask="<18..70>" />
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Genero preferido">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxRadioButtonList runat="server" ID="rbtl_genero" Width="300px">
                                                        <Items>
                                                            <dx:ListEditItem Selected="true" Text="Masculino" Value="Masculino" />
                                                            <dx:ListEditItem Value="Femenino" Text="Femenino" />
                                                            <dx:ListEditItem Value="No relevante" Text="No relevante" />
                                                        </Items>
                                                    </dx:ASPxRadioButtonList>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Estado civil">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxRadioButtonList runat="server" ID="rbtl_genero_estado_civil" Width="300px">
                                                        <Items>
                                                            <dx:ListEditItem Selected="true" Text="Indistinto" Value="Indistinto" />
                                                            <dx:ListEditItem Value="Casado" Text="Casado" />
                                                            <dx:ListEditItem Value="Soltero" Text="Soltero" />
                                                        </Items>
                                                    </dx:ASPxRadioButtonList>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                                <dx:LayoutGroup Caption="Formación" GroupBoxDecoration="box">
                                    <Items>
                                        <dx:LayoutItem Caption="Tipo">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxLabel ID="ASPxLabel13" runat="server" ForeColor="Red" Font-Size="10" Text="Seleccionar el tipo de formación general que se precisa como “base” para que el ocupante sea capaz de dar un rendimiento completo. No señale nada si resulta indistinto">
                                                                </dx:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxRadioButtonList runat="server" ID="rbtl_formacion" Width="300px">
                                                                    <Items>
                                                                        <dx:ListEditItem Selected="true" Text="Enseñanza secundaria" Value="Enseñanza secundaria" />
                                                                        <dx:ListEditItem Value="Terciario" Text="Terciario" />
                                                                        <dx:ListEditItem Value="Universitario" Text="Universitario" />
                                                                        <dx:ListEditItem Value="Pos grado" Text="Pos grado" />
                                                                        <dx:ListEditItem Value="Otro" Text="Otro" />
                                                                    </Items>
                                                                </dx:ASPxRadioButtonList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Estado">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxRadioButtonList runat="server" ID="rbtl_formacion_estado" Width="300px" AutoPostBack="true"
                                                        OnSelectedIndexChanged="rbtl_formacion_estado_SelectedIndexChanged">
                                                        <Items>
                                                            <dx:ListEditItem Selected="true" Text="Completo" Value="Completo" />
                                                            <dx:ListEditItem Value="Avanzado" Text="Avanzado" />
                                                            <dx:ListEditItem Value="Indistinto" Text="Indistinto" />
                                                        </Items>
                                                    </dx:ASPxRadioButtonList>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Titulo">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxTextBox ID="txt_titulo" runat="server" Width="100%" HelpText="Nombre del Titulo que debe poseer"
                                                        HelpTextSettings-DisplayMode="Popup">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                                <dx:LayoutGroup Caption="Experiencia" GroupBoxDecoration="box">
                                    <Items>
                                        <dx:LayoutItem Caption="Experiencia Laboral">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxMemo ID="txt_experiencia" runat="server" Width="100%" Height="50px" HelpText="Tanto específica como de otros puestos de trabajo. Se trata de aprendizajes adquiridos por la práctica profesional y no por el tiempo transcurrido, que garantizan la resolución de situaciones especiales que pueden presentarse en el ejercicio del puesto"
                                                                    HelpTextSettings-DisplayMode="Popup">
                                                                    <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom"
                                                                        SetFocusOnError="true">
                                                                        <ErrorFrameStyle Font-Size="Smaller" />
                                                                        <RequiredField IsRequired="True" ErrorText="Requerido" />
                                                                    </ValidationSettings>
                                                                </dx:ASPxMemo>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Tiempo">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxRadioButtonList runat="server" ID="rbtl_experiencia" Width="300px">
                                                        <Items>
                                                            <dx:ListEditItem Selected="true" Text="3 años o más" Value="3 años o más" />
                                                            <dx:ListEditItem Value="1 a 3 años" Text="1 a 3 años" />
                                                            <dx:ListEditItem Value="6 a 11 meses" Text="6 a 11 meses" />
                                                            <dx:ListEditItem Value="2 a 5 meses" Text="2 a 5 meses" />
                                                            <dx:ListEditItem Value="Sin experiencia" Text="Sin experiencia" />
                                                        </Items>
                                                    </dx:ASPxRadioButtonList>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                            </Items>
                        </dx:LayoutGroup>
                        <dx:LayoutGroup Caption="" GroupBoxDecoration="box">
                            <Items>
                                <dx:LayoutItem Caption="">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <table>
                                                <tr>
                                                    <td style="padding: 4px">
                                                        <dx:ASPxButton ID="btn_enviar" UseSubmitBehavior="false" runat="server" Text="Guardar Modificacion"
                                                            Width="100px" OnClick="btn_enviar_Click">
                                                        </dx:ASPxButton>
                                                    </td>
                                                    <td style="padding: 4px">
                                                        <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="F - 27 - SGI - Solicitud de Selección de Personal">
                                                        </dx:ASPxLabel>
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
