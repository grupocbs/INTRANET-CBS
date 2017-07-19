<%@ Page Title="Iniciar sesión" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    Culture="es-AR" UICulture="es-AR" CodeFile="INTRANET_USUARIOS.aspx.cs" Inherits="INTRANET_USUARIOS" %>

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
                <h2>
                    Administrar Usuarios de Intranet
                </h2>
                <span class="failureNotification">
                    <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                </span>
                <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification"
                    ValidationGroup="LoginUserValidationGroup" />
                <dx:ASPxGridView ID="ASPxGridView1" runat="server" KeyFieldName="Usuario" ClientInstanceName="grid"
                    OnRowDeleting="ASPxGridView1_RowDeleting" Width="100%" OnRowInserting="ASPxGridView1_RowInserting"
                    OnRowUpdating="ASPxGridView1_RowUpdating" OnInit="ASPxGridView1_Init" OnRowValidating="ASPxGridView1_RowValidating">
                    <ClientSideEvents BatchEditEndEditing="OnBatchEditEndEditing" CustomButtonClick="OnCustomButtonClick" />
                    <SettingsBehavior ConfirmDelete="True" AllowSort="false" />
                    <SettingsEditing Mode="Batch" EditFormColumnCount="2" BatchEditSettings-EditMode="Row" />
                    <SettingsDetail ExportMode="Expanded" />
                    <Settings ShowStatusBar="Hidden" />
                    <SettingsPager Mode="ShowAllRecords" />
                    <Columns>
                        <dx:GridViewCommandColumn VisibleIndex="0" ShowNewButtonInHeader="true">
                            <CustomButtons>
                                <dx:GridViewCommandColumnCustomButton ID="deleteButton" Text="Quitar">
                                </dx:GridViewCommandColumnCustomButton>
                            </CustomButtons>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataColumn VisibleIndex="1" FieldName="Usuario">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn VisibleIndex="2" FieldName="Contraseña">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn VisibleIndex="3" FieldName="Nombre">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn VisibleIndex="4" FieldName="Mail">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn VisibleIndex="5" FieldName="Imei">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn VisibleIndex="6" FieldName="Telefono" Caption="Telefono">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataComboBoxColumn VisibleIndex="7" FieldName="Servicio">
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataComboBoxColumn VisibleIndex="8" FieldName="Supervisor">
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataComboBoxColumn VisibleIndex="9" FieldName="id_sector" Caption="SECTOR">
                        </dx:GridViewDataComboBoxColumn>
                    </Columns>
                </dx:ASPxGridView>
            </td>
        </tr>
    </table>
</asp:Content>
