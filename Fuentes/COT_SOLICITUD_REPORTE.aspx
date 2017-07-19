<%@ Page Title="Iniciar sesión" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    Culture="es-AR" UICulture="es-AR" CodeFile="COT_SOLICITUD_REPORTE.aspx.cs" Inherits="COT_SOLICITUD_REPORTE" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table align="center" width="100%">
        <tr>
            <td>
                <h2>
                    Solicitudes de Cotizacion
                </h2>
                <span class="failureNotification">
                    <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                </span>
                <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification"
                    ValidationGroup="LoginUserValidationGroup" />
                <dx:ASPxGridView ID="ASPxGridView1" runat="server" KeyFieldName="ID" ClientInstanceName="grid"
                    OnCustomButtonCallback="ASPxGridView1_CustomButtonCallback" EnableCallBacks="false"
                    Width="100%" OnInit="ASPxGridView1_Init">
                    <SettingsText ConfirmDelete="Confirma que desea eliminar el registro?" />
                    <SettingsBehavior ConfirmDelete="True" AllowSort="false" />
                    <SettingsEditing Mode="Batch" EditFormColumnCount="2" BatchEditSettings-EditMode="Row" />
                    <SettingsDetail ExportMode="Expanded" />
                    <Settings ShowStatusBar="Hidden" />
                    <SettingsPager Mode="ShowAllRecords" />
                    <Columns>
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
                        <dx:GridViewCommandColumn Caption="" VisibleIndex="9">
                            <CustomButtons>
                                <dx:GridViewCommandColumnCustomButton ID="btn_imprimir" Text="Enviar Formularios">
                                </dx:GridViewCommandColumnCustomButton>
                            </CustomButtons>
                        </dx:GridViewCommandColumn>
                    </Columns>
                </dx:ASPxGridView>
            </td>
        </tr>
    </table>
</asp:Content>
