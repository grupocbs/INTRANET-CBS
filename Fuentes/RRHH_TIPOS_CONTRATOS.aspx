<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RRHH_TIPOS_CONTRATOS.aspx.cs"
    MaintainScrollPositionOnPostback="true" Inherits="RRHH_TIPOS_CONTRATOS" MasterPageFile="~/Site.master" %>

<%@ Register Assembly="DevExpress.Web.ASPxHtmlEditor.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxHtmlEditor" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxSpellChecker.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxSpellChecker" TagPrefix="dx" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <dx:ASPxFormLayout runat="server" ID="formLayout" CssClass="formLayout" SettingsItems-HorizontalAlign="Center"
        SettingsItems-Width="1024px">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
        <Items>
            <dx:LayoutGroup Caption="Tipos de Contratos" GroupBoxDecoration="HeadingLine" ColCount="1"
                UseDefaultPaddings="false" Paddings-PaddingTop="10">
                <GroupBoxStyle>
                    <Caption Font-Bold="true" Font-Size="14" />
                </GroupBoxStyle>
            </dx:LayoutGroup>
            <dx:LayoutGroup Caption="Contratos Cargados" GroupBoxDecoration="box">
                <Items>
                    <dx:LayoutItem Caption="">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxLabel ID="FailureText" runat="server" ForeColor="Red" Font-Size="14">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Empresa">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxComboBox ID="cmb_empresa" runat="server" Width="300px" AutoPostBack="true"
                                    OnSelectedIndexChanged="cmb_empresa_SelectedIndexChanged">
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Cargados">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxComboBox ID="cmb_contratos" runat="server" Width="100%" AutoPostBack="true"
                                    OnSelectedIndexChanged="cmb_contratos_SelectedIndexChanged">
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
            <dx:LayoutGroup Caption="Contrato" GroupBoxDecoration="box">
                <Items>
                    <dx:LayoutItem Caption="NOMBRE">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox ID="txt_nombre" runat="server" Width="100%">
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="DESCRIPCION">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <table width="100%" align="center">
                                    <tr>
                                        <td>
                                            <dx:ASPxHtmlEditor ID="txt_descripcion" runat="server" Width="567px" Height="800px">
                                            </dx:ASPxHtmlEditor>
                                        </td>
                                    </tr>
                                </table>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <table align="right">
                                    <tr>
                                        <td>
                                            <dx:ASPxButton ID="btn_enviar" runat="server" Text="Enviar" OnClick="btn_enviar_Click"
                                                Width="100px">
                                            </dx:ASPxButton>
                                            <dx:ASPxButton ID="btn_canacelar" runat="server" Text="Cancelar" OnClick="btn_canacelar_Click"
                                                Width="100px">
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
</asp:Content>
