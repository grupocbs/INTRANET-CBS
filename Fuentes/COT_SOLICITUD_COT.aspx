<%@ Page Language="C#" AutoEventWireup="true" CodeFile="COT_SOLICITUD_COT.aspx.cs"
    MasterPageFile="~/Site.master" Inherits="COT_SOLICITUD_COT" MaintainScrollPositionOnPostback="true" %>

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
                        <dx:LayoutGroup Caption="Cotizaciones - Formulario de Solicitud de Cotizacion" GroupBoxDecoration="HeadingLine"
                            ColCount="1" UseDefaultPaddings="false" Paddings-PaddingTop="10">
                            <GroupBoxStyle>
                                <Caption Font-Bold="true" Font-Size="14" />
                            </GroupBoxStyle>
                        </dx:LayoutGroup>
                        <dx:LayoutGroup Caption="DATOS SOLICITUD" GroupBoxDecoration="box">
                            <Items>
                                <dx:LayoutItem Caption="">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxLabel ID="FailureText" runat="server" ForeColor="Red" Font-Size="14">
                                            </dx:ASPxLabel>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Nº Solicitud">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxTextBox ID="txt_nro_solicitud" runat="server" Width="120px" ReadOnly="true">
                                            </dx:ASPxTextBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Razon Social" CaptionStyle-Font-Bold="true">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxComboBox ID="cmb_razon_social" runat="server" Width="400px" HelpText="Seleccione la Empresa a la que se vinculara el servicio."
                                                HelpTextSettings-DisplayMode="Popup">
                                            </dx:ASPxComboBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Area Operativa" CaptionStyle-Font-Bold="true">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxComboBox ID="cmb_area" runat="server" Width="400px" EnableCallbackMode="true"
                                                HelpText="Seleccione el Area interna que estará vinculada al servicio." HelpTextSettings-DisplayMode="Popup"
                                                IncrementalFilteringMode="StartsWith">
                                            </dx:ASPxComboBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Cliente" CaptionStyle-Font-Bold="true">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxComboBox ID="cmb_cliente" runat="server" Width="400px" EnableCallbackMode="true"
                                                HelpText="Seleccione el que solicita el servicio" HelpTextSettings-DisplayMode="Popup"
                                                IncrementalFilteringMode="StartsWith">
                                            </dx:ASPxComboBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Nombre y Apellido" CaptionStyle-Font-Bold="true">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxTextBox ID="txt_nombres" runat="server" Width="400px" HelpText="Nombre y Apellido de un Contacto del cliente"
                                                HelpTextSettings-DisplayMode="Popup">
                                                <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom"
                                                    SetFocusOnError="true">
                                                    <ErrorFrameStyle Font-Size="Smaller" />
                                                    <RequiredField IsRequired="True" ErrorText="Requerido" />
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Domicilio" CaptionStyle-Font-Bold="true">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxTextBox ID="txt_domicilio_real" runat="server" Width="400px" HelpText="Domicilio donde se brindara el servicio."
                                                HelpTextSettings-DisplayMode="Popup">
                                                <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom"
                                                    SetFocusOnError="true">
                                                    <ErrorFrameStyle Font-Size="Smaller" />
                                                    <RequiredField IsRequired="True" ErrorText="Requerido" />
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Telefono" CaptionStyle-Font-Bold="true">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxTextBox ID="txt_telefono" runat="server" Width="400px" HelpText="Telefono de Contacto."
                                                HelpTextSettings-DisplayMode="Popup">
                                                <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom"
                                                    SetFocusOnError="true">
                                                    <ErrorFrameStyle Font-Size="Smaller" />
                                                    <RequiredField IsRequired="True" ErrorText="Requerido" />
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Email">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxTextBox ID="txt_mail" runat="server" Width="400px" HelpText="Email de Contacto."
                                                HelpTextSettings-DisplayMode="Popup">
                                            </dx:ASPxTextBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <%--     <dx:LayoutItem Caption="Fecha Visita Tecnico (estimada)">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxDateEdit ID="txt_fecha_alta" runat="server" UseMaskBehavior="true" MinDate="01/01/1900"
                                                Width="120px" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy"
                                                HelpText="Fecha estimada en la que deberia realizarse el relevamiento tecnico."
                                                HelpTextSettings-DisplayMode="Popup">
                                                <ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom"
                                                    SetFocusOnError="true">
                                                    <ErrorFrameStyle Font-Size="Smaller" />
                                                    <RequiredField IsRequired="True" ErrorText="Requerido" />
                                                </ValidationSettings>
                                            </dx:ASPxDateEdit>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>--%>
                                <dx:LayoutItem Caption="Observaciones">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxMemo ID="txt_observaciones" runat="server" Width="400px" Height="100px" DisplayFormatString="dd/MM/yyyy"
                                                HelpText="Indique todas las observaciones que considere relevantes" HelpTextSettings-DisplayMode="Popup">
                                            </dx:ASPxMemo>
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
