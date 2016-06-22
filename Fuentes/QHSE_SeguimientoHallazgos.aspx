<%@ Page Title="QHSE Seguimiento Hallazgos" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="QHSE_SeguimientoHallazgos.aspx.cs" Inherits="QHSE_SeguimientoHallazgos" %>

<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxFileManager" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxUploadControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v12.1, Version=12.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v12.1.Export, Version=12.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
        function fileuploaded(s, e) {
            alert("Archivo Cargado.");
        }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <table align="center">
            <tr>
                <td>
                    <h2>
                        Seguimiento de Hallazgos
                    </h2>
                </td>
            </tr>
            <tr>
                <td>
                    <span class="failureNotification">
                        <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                    </span>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" style="margin-bottom: 16px">
                        <tr>
                            <td style="padding-right: 4px">
                                <dx:ASPxButton ID="btnPdfExport" runat="server" Text="Exportar a PDF" OnClick="btnPdfExport_Click">
                                </dx:ASPxButton>
                            </td>
                            <td style="padding-right: 4px">
                                <dx:ASPxButton ID="btnXlsExport" runat="server" Text="Exportar a XLS" OnClick="btnXlsExport_Click">
                                </dx:ASPxButton>
                            </td>
                            <td style="padding-right: 4px">
                                <dx:ASPxButton ID="btnCsvExport" runat="server" Text="Exportar a CSV" OnClick="btnCsvExport_Click">
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <dx:ASPxGridView ID="ASPxGridView2" runat="server" AutoGenerateColumns="true" OnHtmlDataCellPrepared="ASPxGridView2_HtmlDataCellPrepared">
                        <Settings ShowTitlePanel="true" />
                        <SettingsText Title="Cuadro Resumen" />
                        <Settings ShowColumnHeaders="false" />
                    </dx:ASPxGridView>
                </td>
            </tr>
            <tr>
                <td>
                    <dx:ASPxButton ID="btn_buscar" runat="server" Text="Nuevo" OnClick="btn_buscar_Click">
                    </dx:ASPxButton>
                </td>
            </tr>
            <tr>
                <td>
                    <dx:ASPxGridView ID="ASPxGridView1" runat="server" KeyFieldName="idhallazgo" OnRowUpdating="ASPxGridView1_RowUpdating"
                        OnRowInserting="ASPxGridView1_RowInserting" 
                        OnInitNewRow="ASPxGridView1_InitNewRow" OnRowDeleting="ASPxGridView1_RowDeleting"
                        OnHtmlRowPrepared="ASPxGridView1_HtmlRowPrepared" ClientInstanceName="grid" OnHtmlDataCellPrepared="VencimientosGrid_HtmlDataCellPrepared"
                        OnInit="ASPxGridView1_Init" OnCustomCallback="ASPxGridView1_CustomCallback" EnableCallBacks="true">
                        <ClientSideEvents EndCallback="function(s,e){
			 if (s.cpnumero) {
                            s.GetEditor('numero').SetValue(s.cpnumero);
                            delete s.cpnumero;
                        }
                          if (s.cpaccidente) {
                            s.GetEditor('accidente').SetValue(s.cpaccidente);
                            delete s.cpaccidente;
                        }
                         if (s.cpfecha) {
                            s.GetEditor('fecha').SetValue(s.cpfecha);
                            delete s.cpfecha;
                        }
                        if (s.cparea) {
                            s.GetEditor('area').SetValue(s.cparea);
                            delete s.cparea;
                        }
                        if (s.cpequipo_sector) {
                            s.GetEditor('equipo_sector').SetValue(s.cpequipo_sector);
                            delete s.cpequipo_sector;
                        }
                         if (s.cpdescripcion) {
                            s.GetEditor('descripcion').SetValue(s.cpdescripcion);
                            delete s.cpdescripcion;
                        }
                          if (s.cpcalificacion) {
                            s.GetEditor('calificacion').SetValue(s.cpcalificacion);
                            delete s.cpcalificacion;
                        }
                
                    }" />
                        <SettingsBehavior ConfirmDelete="True" />
                        <SettingsEditing Mode="EditForm" />
                        <SettingsBehavior ProcessSelectionChangedOnServer="true" />
                        <SettingsEditing EditFormColumnCount="1" />
                        <Columns>
                            <dx:GridViewCommandColumn VisibleIndex="0" ButtonType="Button">
                                <EditButton Visible="True" Text="Editar" />
                                <DeleteButton Visible="true" Text="Quitar" />
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataColumn FieldName="idhallazgo" VisibleIndex="1" Caption="Nº" Visible="false">
                                <EditFormSettings Visible="False" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataTextColumn FieldName="numero" Caption="Nº Hallazgo" VisibleIndex="1"
                                ReadOnly="true">
                                <PropertiesTextEdit Width="100px" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataComboBoxColumn FieldName="accidente" Caption="Accidente" VisibleIndex="2">
                                <PropertiesComboBox Width="200px" EnableCallbackMode="true" IncrementalFilteringMode="StartsWith">
                                    <ClientSideEvents TextChanged="function(s, e) {
                                                    grid.PerformCallback(s.GetValue());
                                                        }" />
                                </PropertiesComboBox>
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataDateColumn FieldName="fecha" VisibleIndex="2" Caption="Fecha">
                                <PropertiesDateEdit Width="100px" />
                            </dx:GridViewDataDateColumn>
                            <dx:GridViewDataComboBoxColumn FieldName="area" Caption="Sector" VisibleIndex="3">
                                <PropertiesComboBox Width="200px" EnableCallbackMode="true" IncrementalFilteringMode="StartsWith">
                                </PropertiesComboBox>
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataComboBoxColumn FieldName="equipo_sector" Caption="Lugar" VisibleIndex="4">
                                <PropertiesComboBox Width="200px" EnableCallbackMode="true" IncrementalFilteringMode="StartsWith">
                                </PropertiesComboBox>
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataComboBoxColumn FieldName="calificacion" Caption="Calificación" VisibleIndex="5">
                                <PropertiesComboBox Width="200px" />
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataMemoColumn FieldName="descripcion" Caption="Desc" VisibleIndex="7">
                                <PropertiesMemoEdit Height="100px" Width="200px">
                                </PropertiesMemoEdit>
                                <EditFormSettings Caption="Descripcion del Desvio" />
                            </dx:GridViewDataMemoColumn>
                            <dx:GridViewDataTextColumn FieldName="punto_norma" Caption="N.Norma" VisibleIndex="7">
                                <PropertiesTextEdit Width="100px" />
                                <EditFormSettings Caption="Numero de la Norma" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataComboBoxColumn FieldName="responsable" Caption="Resp.A.I." VisibleIndex="8">
                                <PropertiesComboBox Width="200px" EnableCallbackMode="true" IncrementalFilteringMode="StartsWith">
                                </PropertiesComboBox>
                                <EditFormSettings Caption="Responsable de Accion Inmediata" />
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataComboBoxColumn FieldName="responsable_ac" Caption="Resp.A.C." VisibleIndex="8">
                                <PropertiesComboBox Width="200px" EnableCallbackMode="true" IncrementalFilteringMode="StartsWith">
                                </PropertiesComboBox>
                                <EditFormSettings Caption="Responsable de Accion Correctiva" />
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataComboBoxColumn FieldName="responsable_ve" Caption="Resp.V.E." VisibleIndex="8">
                                <PropertiesComboBox Width="200px" EnableCallbackMode="true" IncrementalFilteringMode="StartsWith">
                                </PropertiesComboBox>
                                <EditFormSettings Caption="Responsable de Verificacion de Eficacia" />
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataDateColumn FieldName="fecha_accion" VisibleIndex="11" Caption="F.A.I.">
                                <PropertiesDateEdit Width="100px" />
                                <EditFormSettings Caption="Fecha Accion Inmediata" />
                            </dx:GridViewDataDateColumn>
                            <dx:GridViewDataMemoColumn FieldName="comentario_ai" Caption="Desc. A.I." VisibleIndex="9">
                                <PropertiesMemoEdit Height="100px" Width="200px">
                                </PropertiesMemoEdit>
                                <EditFormSettings Caption="Descripcion Accion Inmediata" />
                            </dx:GridViewDataMemoColumn>
                              <dx:GridViewDataMemoColumn FieldName="causa_raiz" Caption="Causa R." VisibleIndex="9">
                                <PropertiesMemoEdit Height="100px" Width="200px">
                                </PropertiesMemoEdit>
                                <EditFormSettings Caption="Descripcion Causa Raiz" />
                            </dx:GridViewDataMemoColumn>
                            <dx:GridViewDataTextColumn Caption="A.I." FieldName="archivo_ai" VisibleIndex="15">
                                <EditFormSettings Caption="Archivo Accion Inmediata" />
                                <DataItemTemplate>
                                    <a href="<%# Eval("archivo_ai") %>" target="_blank">
                                        <%# ((Eval("archivo_ai").ToString().Length > 0) ? "<img src='imagenes/file.gif' border='0' />" : "")%>
                                    </a>
                                </DataItemTemplate>
                                <EditItemTemplate>
                                    <dx:ASPxUploadControl OnFileUploadComplete="fileuploaded_AI" ID="ASPxUploadControl2"
                                        runat="server" Width="200px" ShowUploadButton="True" UploadButton-Text="Cargar"
                                        FileUploadMode="OnPageLoad" ShowProgressPanel="True" ClientSideEvents-FileUploadComplete="fileuploaded"
                                        AddUploadButtonsHorizontalPosition="InputRightSide">
                                    </dx:ASPxUploadControl>
                                </EditItemTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataDateColumn FieldName="fecha_AC" VisibleIndex="12" Caption="F.A.C.">
                                <PropertiesDateEdit Width="100px" />
                                <EditFormSettings Caption="Fecha Accion Correctiva" />
                            </dx:GridViewDataDateColumn>
                            <dx:GridViewDataMemoColumn FieldName="comentario_ac" Caption="Desc. A.C." VisibleIndex="9">
                                <PropertiesMemoEdit Height="100px" Width="200px">
                                </PropertiesMemoEdit>
                                <EditFormSettings Caption="Descripcion Accion Correctiva" />
                            </dx:GridViewDataMemoColumn>
                            <dx:GridViewDataTextColumn Caption="A.C." FieldName="archivo_ac" VisibleIndex="15">
                                <EditFormSettings Caption="Archivo Accion Correctiva" />
                                <DataItemTemplate>
                                    <a href="<%# Eval("archivo_ac") %>" target="_blank">
                                        <%# ((Eval("archivo_ac").ToString().Length > 0) ? "<img src='imagenes/file.gif' border='0' />" : "")%>
                                    </a>
                                </DataItemTemplate>
                                <EditItemTemplate>
                                    <dx:ASPxUploadControl OnFileUploadComplete="fileuploaded_AC" ID="ASPxUploadControl2"
                                        runat="server" Width="200px" ShowUploadButton="True" UploadButton-Text="Cargar"
                                        FileUploadMode="OnPageLoad" ShowProgressPanel="True" ClientSideEvents-FileUploadComplete="fileuploaded"
                                        AddUploadButtonsHorizontalPosition="InputRightSide">
                                    </dx:ASPxUploadControl>
                                </EditItemTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataDateColumn FieldName="fecha_cierre_nc" VisibleIndex="14" Caption="F.V.E.">
                                <PropertiesDateEdit Width="100px" />
                                <EditFormSettings Caption="Fecha de Verificacion de Eficacia" />
                            </dx:GridViewDataDateColumn>
                            <dx:GridViewDataMemoColumn FieldName="comentario_ev" Caption="Desc. V.E." VisibleIndex="9">
                                <PropertiesMemoEdit Height="100px" Width="200px">
                                </PropertiesMemoEdit>
                                <EditFormSettings Caption="Descripcion Verificacion de Eficacia" />
                            </dx:GridViewDataMemoColumn>
                            <dx:GridViewDataTextColumn Caption="E.V.E." FieldName="archivo_evidencia" VisibleIndex="15">
                                <EditFormSettings Caption="Archivo Verificacion de Eficacia" />
                                <DataItemTemplate>
                                    <a href="<%# Eval("archivo_evidencia") %>" target="_blank">
                                        <%# ((Eval("archivo_evidencia").ToString().Length > 0) ? "<img src='imagenes/file.gif' border='0' />" : "")%>
                                    </a>
                                </DataItemTemplate>
                                <EditItemTemplate>
                                    <dx:ASPxUploadControl OnFileUploadComplete="fileuploaded_Evidencia" ID="ASPxUploadControl1"
                                        runat="server" Width="200px" ShowUploadButton="True" UploadButton-Text="Cargar"
                                        FileUploadMode="OnPageLoad" ShowProgressPanel="True" ClientSideEvents-FileUploadComplete="fileuploaded"
                                        AddUploadButtonsHorizontalPosition="InputRightSide">
                                    </dx:ASPxUploadControl>
                                </EditItemTemplate>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <SettingsDetail ExportMode="Expanded" />
                        <SettingsPager Mode="ShowAllRecords" />
                        <SettingsBehavior EnableCustomizationWindow="true" />
                    </dx:ASPxGridView>
                </td>
            </tr>
            <tr>
                <td>
                    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1">
                    </dx:ASPxGridViewExporter>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
