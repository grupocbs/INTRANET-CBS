<%@ Page Title="Iniciar sesión" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    Culture="es-AR" UICulture="es-AR" CodeFile="EQUIPOS_INFORMATICOS.aspx.cs" Inherits="EQUIPOS_INFORMATICOS" %>

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
                    Administrar Equipos Informaticos
                </h2>
                <span class="failureNotification">
                    <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                </span>
                <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification"
                    ValidationGroup="LoginUserValidationGroup" />
                <dx:ASPxGridView ID="ASPxGridView1" runat="server" KeyFieldName="ID" ClientInstanceName="grid"
                    OnRowDeleting="ASPxGridView1_RowDeleting" Width="100%" OnRowInserting="ASPxGridView1_RowInserting"
                    OnRowUpdating="ASPxGridView1_RowUpdating" OnInit="ASPxGridView1_Init">
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
                        <dx:GridViewDataColumn FieldName="ID" VisibleIndex="1" Visible="false">
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataComboBoxColumn VisibleIndex="2" FieldName="IDUSUARIO" Caption="USUARIO">
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataColumn VisibleIndex="3" FieldName="IP">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataComboBoxColumn VisibleIndex="4" FieldName="TIPO">
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataColumn VisibleIndex="5" FieldName="NOMBRE_EQUIPO" Caption="EQUIPO">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn VisibleIndex="6" FieldName="DESCRIPCION">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn VisibleIndex="7" FieldName="USUARIO_SESION" Caption="USUARIO">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn VisibleIndex="8" FieldName="CONTRASEÑA_SESION" Caption="CONTRASEÑA">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn VisibleIndex="9" FieldName="RAM">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataComboBoxColumn VisibleIndex="10" FieldName="SISTEMA_OPERATIVO" Caption="SO">
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataColumn VisibleIndex="11" FieldName="LICENCIA_SO" Caption="LIC_SO">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataComboBoxColumn VisibleIndex="12" FieldName="ANTIVIRUS">
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataComboBoxColumn VisibleIndex="13" FieldName="OFFICE">
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataColumn VisibleIndex="14" FieldName="LICENCIA_OFFICE" Caption="LIC_OFFICE">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn VisibleIndex="15" FieldName="IDTEAMVIEWER" Caption="TEAM">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataComboBoxColumn VisibleIndex="16" FieldName="SWITCH">
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataColumn VisibleIndex="17" FieldName="BOCA">
                        </dx:GridViewDataColumn>
                    </Columns>
                </dx:ASPxGridView>
            </td>
        </tr>
    </table>
</asp:Content>
