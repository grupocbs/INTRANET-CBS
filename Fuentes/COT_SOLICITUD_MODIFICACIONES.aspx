<%@ Page Title="Iniciar sesión" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    Culture="es-AR" UICulture="es-AR" CodeFile="COT_SOLICITUD_MODIFICACIONES.aspx.cs"
    Inherits="COT_SOLICITUD_MODIFICACIONES" %>

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
    <table align="center" width="100%">
        <tr>
            <td>
                <h2>
                    Modificar Solicitudes de Cotizacion
                </h2>
                <span class="failureNotification">
                    <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                </span>
                <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification"
                    ValidationGroup="LoginUserValidationGroup" />
                <dx:ASPxGridView ID="ASPxGridView1" runat="server" KeyFieldName="ID" ClientInstanceName="grid"
                    EnableCallBacks="false" OnRowValidating="ASPxGridView1_RowValidating" OnRowDeleting="ASPxGridView1_RowDeleting"
                    Width="100%" OnRowInserting="ASPxGridView1_RowInserting" OnRowUpdating="ASPxGridView1_RowUpdating"
                    OnInit="ASPxGridView1_Init">
                    <ClientSideEvents BatchEditEndEditing="OnBatchEditEndEditing" CustomButtonClick="OnCustomButtonClick" />
                    <SettingsText ConfirmDelete="Confirma que desea eliminar el registro?" />
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
                        <dx:GridViewDataColumn FieldName="ID" VisibleIndex="1" ReadOnly="true">
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataComboBoxColumn VisibleIndex="2" FieldName="EMPRESA" Caption="EMPRESA">
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataComboBoxColumn VisibleIndex="3" FieldName="IDSECTOR" Caption="SECTOR">
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataComboBoxColumn VisibleIndex="4" FieldName="IDCLIENTE" Caption="CLIENTE">
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataColumn VisibleIndex="5" FieldName="CONTACTO_NOMBRE" Caption="CONTACTO">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn VisibleIndex="6" FieldName="CONTACTO_DOMICILO" Caption="DOMICILIO">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn VisibleIndex="7" FieldName="CONTACTO_MAIL" Caption="MAIL">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn VisibleIndex="8" FieldName="CONTACTO_TELEFONO" Caption="TELEFONO">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn VisibleIndex="9" FieldName="OBSERVACIONES">
                        </dx:GridViewDataColumn>
                    </Columns>
                </dx:ASPxGridView>
            </td>
        </tr>
    </table>
</asp:Content>
