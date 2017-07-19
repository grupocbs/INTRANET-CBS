<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RRHH_RESERVA_SALA.aspx.cs"
    MasterPageFile="~/Site.master" Inherits="RRHH_RESERVA_SALA" MaintainScrollPositionOnPostback="true" %>

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
    <script type="text/javascript">
        function OnBatchEditEndEditing(s, e) {
            window.setTimeout(function () {
                if (s.batchEditApi.HasChanges())

                    grid.UpdateEdit();
            }, 0);

        }
        function OnAltaClick(s, e) {
            grid.AddNewRow();

        }
        function OnCustomButtonClick(s, e) {
            if (e.buttonID == "deleteButton") {
                s.DeleteRow(e.visibleIndex);
                if (s.batchEditApi.HasChanges()) {
                    grid.UpdateEdit();
                };

            }
        }    
    </script>
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
                        <dx:LayoutGroup Caption="Reserva de Sala" GroupBoxDecoration="HeadingLine" ColCount="1"
                            UseDefaultPaddings="false" Paddings-PaddingTop="10">
                            <GroupBoxStyle>
                                <Caption Font-Bold="true" Font-Size="14" />
                            </GroupBoxStyle>
                        </dx:LayoutGroup>
                        <dx:LayoutGroup Caption="Datos para la Reserva" GroupBoxDecoration="box">
                            <Items>
                                
                                <dx:LayoutItem Caption="Lugar" CaptionStyle-Font-Bold="true">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxComboBox ID="cmb_lugar" runat="server" Width="300px" EnableCallbackMode="true"
                                                Border-BorderColor="Blue" Font-Size="Medium" IncrementalFilteringMode="StartsWith"
                                                OnSelectedIndexChanged="cmb_lugar_SelectedIndexChanged" AutoPostBack="true">
                                            </dx:ASPxComboBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Fecha">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxDateEdit ID="txt_fecha" runat="server" UseMaskBehavior="true" MinDate="01/01/1900"  OnValueChanged="txt_fecha_ValueChanged" AutoPostBack="true"
                                                Width="120px" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy"
                                                HelpText="Es la Fecha de la reserva" HelpTextSettings-DisplayMode="Popup">
                                            </dx:ASPxDateEdit>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxLabel ID="FailureText" runat="server" ForeColor="Red" Font-Size="14">
                                            </dx:ASPxLabel>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutGroup Caption="Rango de Horarios" GroupBoxDecoration="box">
                                    <Items>
                                        <dx:LayoutItem Caption="">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <table>
                                                        <tr>
                                                            <td style="padding: 2px;">
                                                                <dx:ASPxCheckBox ID="chk_08_09" runat="server" Enabled="false" Text="De 08hs a 09hs" OnCheckedChanged="chk_CheckedChanged"
                                                                    AutoPostBack="true">
                                                                </dx:ASPxCheckBox>
                                                            </td>
                                                            <td style="padding: 2px;">
                                                                <dx:ASPxLabel ID="txt_08_09" runat="server" Font-Bold="true">
                                                                </dx:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <table>
                                                        <tr>
                                                            <td style="padding: 2px;">
                                                                <dx:ASPxCheckBox ID="chk_09_10" runat="server" Enabled="false" Text="De 09hs a 10hs" OnCheckedChanged="chk_CheckedChanged"
                                                                    AutoPostBack="true">
                                                                </dx:ASPxCheckBox>
                                                            </td>
                                                            <td style="padding: 2px;">
                                                                <dx:ASPxLabel ID="txt_09_10" runat="server" Font-Bold="true">
                                                                </dx:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <table>
                                                        <tr>
                                                            <td style="padding: 2px;">
                                                                <dx:ASPxCheckBox ID="chk_10_11" runat="server" Enabled="false" Text="De 10hs a 11hs" OnCheckedChanged="chk_CheckedChanged"
                                                                    AutoPostBack="true">
                                                                </dx:ASPxCheckBox>
                                                            </td>
                                                            <td style="padding: 2px;">
                                                                <dx:ASPxLabel ID="txt_10_11" runat="server" Font-Bold="true">
                                                                </dx:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <table>
                                                        <tr>
                                                            <td style="padding: 2px;">
                                                                <dx:ASPxCheckBox ID="chk_11_12" runat="server"  Enabled="false" Text="De 11hs a 12hs" OnCheckedChanged="chk_CheckedChanged"
                                                                    AutoPostBack="true">
                                                                </dx:ASPxCheckBox>
                                                            </td>
                                                            <td style="padding: 2px;">
                                                                <dx:ASPxLabel ID="txt_11_12" runat="server" Font-Bold="true">
                                                                </dx:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <table>
                                                        <tr>
                                                            <td style="padding: 2px;">
                                                                <dx:ASPxCheckBox ID="chk_12_13" runat="server" Enabled="false" Text="De 12hs a 13hs" OnCheckedChanged="chk_CheckedChanged"
                                                                    AutoPostBack="true">
                                                                </dx:ASPxCheckBox>
                                                            </td>
                                                            <td style="padding: 2px;">
                                                                <dx:ASPxLabel ID="txt_12_13" runat="server" Font-Bold="true">
                                                                </dx:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <table>
                                                        <tr>
                                                            <td style="padding: 2px;">
                                                                <dx:ASPxCheckBox ID="chk_13_14" runat="server" Enabled="false" Text="De 13hs a 13:30hs" OnCheckedChanged="chk_CheckedChanged"
                                                                    AutoPostBack="true">
                                                                </dx:ASPxCheckBox>
                                                            </td>
                                                            <td style="padding: 2px;">
                                                                <dx:ASPxLabel ID="txt_13_14" runat="server" Font-Bold="true">
                                                                </dx:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <table>
                                                        <tr>
                                                            <td style="padding: 2px;">
                                                                <dx:ASPxCheckBox ID="chk_14_15" runat="server" Enabled="false" Text="De 14:30hs a 15hs" OnCheckedChanged="chk_CheckedChanged"
                                                                    AutoPostBack="true">
                                                                </dx:ASPxCheckBox>
                                                            </td>
                                                            <td style="padding: 2px;">
                                                                <dx:ASPxLabel ID="txt_14_15" runat="server" Font-Bold="true">
                                                                </dx:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <table>
                                                        <tr>
                                                            <td style="padding: 2px;">
                                                                <dx:ASPxCheckBox ID="chk_15_16" runat="server" Enabled="false" Text="De 15hs a 16hs" OnCheckedChanged="chk_CheckedChanged"
                                                                    AutoPostBack="true">
                                                                </dx:ASPxCheckBox>
                                                            </td>
                                                            <td style="padding: 2px;">
                                                                <dx:ASPxLabel ID="txt_15_16" runat="server" Font-Bold="true">
                                                                </dx:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <table>
                                                        <tr>
                                                            <td style="padding: 2px;">
                                                                <dx:ASPxCheckBox ID="chk_16_17" runat="server" Enabled="false" Text="De 16hs a 17hs" OnCheckedChanged="chk_CheckedChanged"
                                                                    AutoPostBack="true">
                                                                </dx:ASPxCheckBox>
                                                            </td>
                                                            <td style="padding: 2px;">
                                                                <dx:ASPxLabel ID="txt_16_17" runat="server" Font-Bold="true">
                                                                </dx:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <table>
                                                        <tr>
                                                            <td style="padding: 2px;">
                                                                <dx:ASPxCheckBox ID="chk_17_18" runat="server" Enabled="false" Text="De 17hs a 18hs" OnCheckedChanged="chk_CheckedChanged"
                                                                    AutoPostBack="true">
                                                                </dx:ASPxCheckBox>
                                                            </td>
                                                            <td style="padding: 2px;">
                                                                <dx:ASPxLabel ID="txt_17_18" runat="server" Font-Bold="true">
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
