<%@ Page Title="Envio de comprobantes" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="FC_ENV_COMP.aspx.cs" Inherits="FC_ENV_COMP"
    Culture="es-AR" UICulture="es-AR" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
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
    <table align="center">
        <tr>
            <td>
                <h2>
                    Envio de comprobantes por Mail
                </h2>
                <span class="failureNotification">
                    <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                </span>
                <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification"
                    ValidationGroup="LoginUserValidationGroup" />
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td style="padding: 2px;">
                            Fecha Desde:
                        </td>
                        <td style="padding: 2px;">
                            <asp:TextBox ID="ASPxCalendarDesde" runat="server" ValidationGroup="reporte" Width="90px"></asp:TextBox>
                            <cc2:MaskedEditExtender ID="ASPxCalendarDesde_MaskedEditExtender" runat="server"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="ASPxCalendarDesde">
                            </cc2:MaskedEditExtender>
                            <cc2:CalendarExtender ID="ASPxCalendarDesde_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="ASPxCalendarDesde">
                            </cc2:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 2px;">
                            Fecha Hasta:
                        </td>
                        <td style="padding: 2px;">
                            <asp:TextBox ID="ASPxCalendarHasta" runat="server" ValidationGroup="reporte" Width="90px"></asp:TextBox>
                            <cc2:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="ASPxCalendarHasta">
                            </cc2:MaskedEditExtender>
                            <cc2:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="ASPxCalendarHasta">
                            </cc2:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 2px;">
                            Empresa:
                        </td>
                        <td style="padding: 2px;">
                            <dx:ASPxComboBox ID="cmb_empresa" runat="server" ValueType="System.String" AutoPostBack="true"
                                IncrementalFilteringMode="StartsWith" OnSelectedIndexChanged="cmb_empresa_SelectedIndexChanged">
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 2px;">
                            Clientes :
                        </td>
                        <td style="padding: 2px;">
                            <dx:ASPxComboBox ID="cmb_cliente" runat="server" ValueType="System.String" AutoPostBack="true"
                                Width="400px" IncrementalFilteringMode="StartsWith" OnSelectedIndexChanged="cmb_cliente_SelectedIndexChanged">
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <dx:ASPxGridView ID="ASPxGridView1" runat="server" KeyFieldName="VTRMVH_NROFOR" ClientInstanceName="grid"
                    OnRowUpdating="ASPxGridView1_RowUpdating" Width="100%" OnInit="ASPxGridView1_Init">
                    <ClientSideEvents BatchEditEndEditing="OnBatchEditEndEditing" CustomButtonClick="OnCustomButtonClick" />
                    <SettingsBehavior ConfirmDelete="True" AllowSort="false" />
                    <SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Cell" />
                    <SettingsDetail ExportMode="Expanded" />
                    <Settings ShowStatusBar="Hidden" />
                    <SettingsPager Mode="ShowAllRecords" />
                    <Columns>
                        <dx:GridViewDataColumn VisibleIndex="1" FieldName="VTRMVH_CODFOR" Caption="COMPROBANTE"
                            ReadOnly="true">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn VisibleIndex="2" FieldName="VTRMVH_NROFOR" Caption="NRO" ReadOnly="true">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn VisibleIndex="3" FieldName="VTRMVH_FCHMOV" Caption="FECHA"
                            ReadOnly="true">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn VisibleIndex="4" FieldName="VTRMVH_NROCAE" Caption="CAE" ReadOnly="true">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn VisibleIndex="5" FieldName="VTRMVH_VENCAE" Caption="VTO CAE"
                            ReadOnly="true">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn VisibleIndex="6" FieldName="VTMCLH_NROCTA" Caption="COD CLIENTE"
                            ReadOnly="true">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn VisibleIndex="7" FieldName="CLIENTE" Caption="CLIENTE" ReadOnly="true">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn VisibleIndex="8" FieldName="VTRMVH_USERID" Caption="USUARIO CARGO"
                            ReadOnly="true">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataComboBoxColumn VisibleIndex="9" FieldName="accion" Caption="ACCION"
                            Width="100px">
                        </dx:GridViewDataComboBoxColumn>
                    </Columns>
                </dx:ASPxGridView>
            </td>
        </tr>
    </table>
</asp:Content>
