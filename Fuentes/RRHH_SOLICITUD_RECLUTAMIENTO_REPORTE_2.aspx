<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RRHH_SOLICITUD_RECLUTAMIENTO_REPORTE_2.aspx.cs"
    MasterPageFile="~/Site.master" Inherits="RRHH_SOLICITUD_RECLUTAMIENTO_REPORTE_2"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxHtmlEditor.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxHtmlEditor" TagPrefix="dx" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
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
                        <dx:LayoutGroup Caption="Reclutamientos - Notificacion para Medios" GroupBoxDecoration="HeadingLine"
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
                                            <dx:ASPxComboBox ID="cmb_solicitudes" runat="server" Width="100%" EnableCallbackMode="true"
                                                Border-BorderColor="Blue" Font-Size="Medium" IncrementalFilteringMode="StartsWith"
                                                OnSelectedIndexChanged="cmb_solicitudes_SelectedIndexChanged" AutoPostBack="true">
                                            </dx:ASPxComboBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                        </dx:LayoutGroup>
                        <dx:LayoutGroup Caption="Reporte Aviso" GroupBoxDecoration="box">
                            <Items>
                                <dx:LayoutItem Caption="">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <table width="100%" align="center">
                                                <tr>
                                                    <td>
                                                        <dx:ASPxHtmlEditor ID="txt_descripcion" runat="server" Width="800px" Height="700px">
                                                        </dx:ASPxHtmlEditor>
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
</asp:Content>
