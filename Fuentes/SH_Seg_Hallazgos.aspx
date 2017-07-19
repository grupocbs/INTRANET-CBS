<%@ Page Title="Seguimiento de Hallazgos" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="SH_Seg_Hallazgos.aspx.cs" Inherits="SH_Seg_Hallazgos" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
        function fileuploaded(s, e) {
            alert("Archivo Cargado.");
        }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table width="100%">
        <tr>
            <td>
                <h1>
                    Seguimiento de Hallazgos</h1>
            </td>
        </tr>
        <tr>
            <td>
                <dx:ASPxLabel ID="FailureText" ClientInstanceName="FailureText" runat="server" ForeColor="Red">
                </dx:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td style="padding: 4px">
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
            <td>
                <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1">
                </dx:ASPxGridViewExporter>
            </td>
        </tr>
        <tr>
            <td style="padding-right: 4px">
                <dx:ASPxGridView ID="ASPxGridView1" runat="server" KeyFieldName="ID" OnRowUpdating="ASPxGridView1_RowUpdating"
                    OnRowInserting="ASPxGridView1_RowInserting" OnRowDeleting="ASPxGridView1_RowDeleting"
                    Width="100%" Font-Size="X-Small" OnHtmlRowPrepared="ASPxGridView1_HtmlRowPrepared"
                    ClientInstanceName="grid" OnHtmlDataCellPrepared="VencimientosGrid_HtmlDataCellPrepared"
                    OnInit="ASPxGridView1_Init" OnCustomButtonCallback="ASPxGridView1_CustomButtonCallback"
                    EnableCallBacks="false">
                    <SettingsPager Mode="ShowAllRecords" />
                    <SettingsBehavior ConfirmDelete="True" />
                    <SettingsText ConfirmDelete="Eliminar el registro?" />
                    <SettingsEditing Mode="EditForm" />
                    <SettingsBehavior ProcessSelectionChangedOnServer="true" />
                    <SettingsEditing EditFormColumnCount="1" />
                    <SettingsDetail ExportMode="Expanded" />
                    <SettingsBehavior EnableCustomizationWindow="true" />
                    <Settings ShowHeaderFilterButton="True" ShowHeaderFilterBlankItems="true" ShowHorizontalScrollBar="True" />
                    <SettingsCommandButton EditButton-Text="EDITAR" UpdateButton-Text="GUARDAR" DeleteButton-Text="QUITAR"
                        CancelButton-Text="CANCELAR" />
                    <Columns>
                        <dx:GridViewCommandColumn VisibleIndex="0" ShowEditButton="true" ShowDeleteButton="true">
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataColumn FieldName="ID" VisibleIndex="1" Caption="Nº" Visible="false">
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataComboBoxColumn FieldName="ID_SECTOR" Caption="SECTOR" VisibleIndex="2">
                            <PropertiesComboBox Width="300px" EnableCallbackMode="true" IncrementalFilteringMode="StartsWith">
                            </PropertiesComboBox>
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataColumn FieldName="NRO" VisibleIndex="2" Caption="NRO">
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataDateColumn FieldName="FECHA" VisibleIndex="2" Caption="FECHA">
                            <PropertiesDateEdit Width="80px">
                            </PropertiesDateEdit>
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataComboBoxColumn FieldName="ID_ORIGEN" Caption="ORIGEN" VisibleIndex="4">
                            <PropertiesComboBox Width="300px" EnableCallbackMode="true" IncrementalFilteringMode="StartsWith">
                            </PropertiesComboBox>
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataComboBoxColumn FieldName="ID_TIPO" Caption="TIPO" VisibleIndex="5">
                            <PropertiesComboBox Width="300px" EnableCallbackMode="true" IncrementalFilteringMode="StartsWith">
                            </PropertiesComboBox>
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataComboBoxColumn FieldName="ID_SECTOR_INT" Caption="SECTOR" VisibleIndex="5">
                            <PropertiesComboBox Width="300px" EnableCallbackMode="true" IncrementalFilteringMode="StartsWith">
                            </PropertiesComboBox>
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataMemoColumn FieldName="DESCRIPCION" Caption="DESC." VisibleIndex="6"
                            Width="400px">
                            <PropertiesMemoEdit Height="100px" Width="500px">
                            </PropertiesMemoEdit>
                            <EditFormSettings Visible="False" Caption="DESCRIPCION DE LA SITUACION DEL DESVIO" />
                        </dx:GridViewDataMemoColumn>
                        <dx:GridViewDataMemoColumn FieldName="ACCION_INMEDIATA" Caption="A.I." VisibleIndex="9"
                            Width="300px">
                            <PropertiesMemoEdit Height="100px" Width="500px">
                            </PropertiesMemoEdit>
                            <EditFormSettings Visible="False" Caption="ACCION CORRECTIVA/PLAZOS/RESPONSABLES" />
                        </dx:GridViewDataMemoColumn>
                        <dx:GridViewDataDateColumn FieldName="PLAZO" VisibleIndex="10" Caption="PLAZO">
                            <PropertiesDateEdit Width="150px">
                            </PropertiesDateEdit>
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataComboBoxColumn FieldName="RESPONSABLE" Caption="RESP." VisibleIndex="10">
                            <PropertiesComboBox Width="300px" />
                            <EditFormSettings Caption="RESPONSABLE" Visible="False" />
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataMemoColumn FieldName="OBSERVACIONES" Caption="OBS." VisibleIndex="11"
                            Width="200px">
                            <PropertiesMemoEdit Height="100px" Width="500px">
                            </PropertiesMemoEdit>
                            <EditFormSettings Caption="OBSERVACIONES" Visible="False" />
                        </dx:GridViewDataMemoColumn>
                        <dx:GridViewDataTextColumn FieldName="PUNTO_NORMA" Caption="P.N." VisibleIndex="12">
                            <EditFormSettings Visible="False" Caption="PUNTO DE LA NORMA" />
                            <PropertiesTextEdit Width="100px">
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="ARCHIVO" FieldName="ARCHIVO" VisibleIndex="12"
                            Width="50px">
                            <EditFormSettings Caption="Archivo" Visible="False" />
                            <DataItemTemplate>
                                <a href="<%# Eval("archivo") %>" target="_blank">
                                    <%# ((Eval("archivo").ToString().Length > 0) ? "<img src='imagenes/file.gif' border='0' />" : "")%>
                                </a>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataComboBoxColumn FieldName="VERIFICACION_SEG" Caption="VER." VisibleIndex="13">
                            <PropertiesComboBox Width="300px" />
                            <EditFormSettings Caption="VERIFICACION" />
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataCheckColumn FieldName="EFECTIVIDAD_SEG" Caption="EF." VisibleIndex="14">
                            <EditFormSettings Caption="EFECTIVIDAD" />
                        </dx:GridViewDataCheckColumn>
                        <dx:GridViewDataDateColumn FieldName="CIERRE_SEG" VisibleIndex="15" Caption="CIERRE">
                            <PropertiesDateEdit Width="150px">
                            </PropertiesDateEdit>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataMemoColumn FieldName="OBSERVACIONES_SEG" Caption="OBS.V" VisibleIndex="16"
                            Width="200px">
                            <PropertiesMemoEdit Height="100px" Width="500px">
                            </PropertiesMemoEdit>
                            <EditFormSettings Caption="OBSERVACIONES DE CUMPLIMIENTO" />
                        </dx:GridViewDataMemoColumn>
                        <dx:GridViewDataTextColumn Caption="ARCH.C" FieldName="ARCHIVO_SEG" VisibleIndex="17"
                            Width="50px">
                            <EditFormSettings Caption="ARCHIVO DE CUMPLIMIENTO" />
                            <DataItemTemplate>
                                <a href="<%# Eval("ARCHIVO_SEG") %>" target="_blank">
                                    <%# ((Eval("ARCHIVO_SEG").ToString().Length > 0) ? "<img src='imagenes/file.gif' border='0' />" : "")%>
                                </a>
                            </DataItemTemplate>
                            <EditItemTemplate>
                                <dx:ASPxUploadControl OnFileUploadComplete="fileuploaded_CS" ID="ASPxUploadControl2"
                                    runat="server" Width="300px" ShowUploadButton="True" UploadButton-Text="Cargar Informe"
                                    FileUploadMode="OnPageLoad" ShowProgressPanel="True" ClientSideEvents-FileUploadComplete="fileuploaded"
                                    AddUploadButtonsHorizontalPosition="InputRightSide">
                                </dx:ASPxUploadControl>
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewCommandColumn Caption="" VisibleIndex="18" Width="80px">
                            <CustomButtons>
                                <dx:GridViewCommandColumnCustomButton ID="btn_imprimir" Text="Imprimir">
                                </dx:GridViewCommandColumnCustomButton>
                            </CustomButtons>
                        </dx:GridViewCommandColumn>
                    </Columns>
                </dx:ASPxGridView>
            </td>
        </tr>
    </table>
</asp:Content>
