<%@ Page Title="Página principal" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="RRHH_TEL_INTERNOS.aspx.cs" Inherits="RRHH_TEL_INTERNOS" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
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
    <table align="center">
        <tr>
            <td>
                <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification"
                    ValidationGroup="LoginUserValidationGroup" />
                <dx:ASPxGridView ID="ASPxGridView1" runat="server" KeyFieldName="IDINTERNO" ClientInstanceName="grid"
                    OnRowDeleting="ASPxGridView1_RowDeleting" OnRowInserting="ASPxGridView1_RowInserting"
                    OnRowUpdating="ASPxGridView1_RowUpdating" OnInit="ASPxGridView1_Init" OnRowValidating="ASPxGridView1_RowValidating">
                    <ClientSideEvents BatchEditEndEditing="OnBatchEditEndEditing" CustomButtonClick="OnCustomButtonClick" />
                    <SettingsBehavior ConfirmDelete="True" AllowSort="false" />
                    <SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Row" />
                    <SettingsDetail ExportMode="Expanded" />
                    <Settings ShowStatusBar="Hidden" ShowTitlePanel="true" />
                    <SettingsText Title="TELEFONOS INTERNOS – BASE CBS" />
                    <SettingsPager Mode="ShowAllRecords" />
                    <Columns>
                        <dx:GridViewCommandColumn VisibleIndex="0" ShowNewButtonInHeader="true">
                            <CustomButtons>
                                <dx:GridViewCommandColumnCustomButton ID="deleteButton" Text="Quitar">
                                </dx:GridViewCommandColumnCustomButton>
                            </CustomButtons>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataColumn VisibleIndex="1" FieldName="INTERNO">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataComboBoxColumn VisibleIndex="2" FieldName="INTEGRANTES" Caption="NOMBRE Y APELLIDO">
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataColumn VisibleIndex="3" FieldName="CORPORATIVO">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn VisibleIndex="4" FieldName="POSICION">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataComboBoxColumn VisibleIndex="5" FieldName="AREA">
                        </dx:GridViewDataComboBoxColumn>
                    </Columns>
                </dx:ASPxGridView>
            </td>
        </tr>
    </table>
</asp:Content>
