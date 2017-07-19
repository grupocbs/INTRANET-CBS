<%@ Page Title="Carga de Hallazgos" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="SH_Hallazgos.aspx.cs" Inherits="SH_Hallazgos" %>

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
    <div>
        <dx:ASPxFormLayout runat="server" ID="formLayout" CssClass="formLayout">
            <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
            <Items>
                <dx:LayoutGroup Caption="Carga de Hallazgos" GroupBoxDecoration="HeadingLine" ColCount="1"
                    UseDefaultPaddings="false" Paddings-PaddingTop="10">
                    <GroupBoxStyle>
                        <Caption Font-Bold="true" Font-Size="14" />
                    </GroupBoxStyle>
                </dx:LayoutGroup>
                <dx:LayoutGroup Caption="Hallazgos" GroupBoxDecoration="box">
                    <Items>
                        <dx:LayoutItem Caption="">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxLabel ID="FailureText" ClientInstanceName="FailureText" runat="server" ForeColor="Red">
                                    </dx:ASPxLabel>
                                    <table width="100%">
                                        <tr>
                                            <td style="padding-right: 4px">
                                                <dx:ASPxGridView ID="ASPxGridView1" runat="server" KeyFieldName="ID" OnRowUpdating="ASPxGridView1_RowUpdating"
                                                   OnRowInserting="ASPxGridView1_RowInserting" Width="100%" OnRowDeleting="ASPxGridView1_RowDeleting"
                                                    Font-Size="X-Small" OnHtmlRowPrepared="ASPxGridView1_HtmlRowPrepared" ClientInstanceName="grid"
                                                    OnHtmlDataCellPrepared="VencimientosGrid_HtmlDataCellPrepared" OnInit="ASPxGridView1_Init">
                                                   
                                                    <SettingsPager Mode="ShowAllRecords" />
                                                    <SettingsBehavior ConfirmDelete="True" />
                                                    <SettingsText ConfirmDelete="Eliminar el registro?" />
                                                    <SettingsEditing Mode="EditForm" />
                                                    <SettingsBehavior ProcessSelectionChangedOnServer="true" />
                                                    <SettingsEditing EditFormColumnCount="1" />
                                                    <SettingsDetail ExportMode="Expanded" />
                                                    <SettingsBehavior EnableCustomizationWindow="true" />
                                                    <Settings   ShowHeaderFilterBlankItems="true" />
                                                    <SettingsCommandButton EditButton-Text="EDITAR" NewButton-Text="AGREGAR" UpdateButton-Text="GUARDAR"
                                                        CancelButton-Text="CANCELAR" />
                                                    <Columns>
                                                        <dx:GridViewCommandColumn VisibleIndex="0" ShowEditButton="true" ShowNewButtonInHeader="true"
                                                            Width="40px">
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataColumn FieldName="ID" VisibleIndex="1" Caption="Nº" Visible="false">
                                                            <EditFormSettings Visible="False" />
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="NRO" VisibleIndex="2" Caption="NRO">
                                                            <EditFormSettings Visible="False" />
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataComboBoxColumn FieldName="ID_SECTOR" Caption="SECTOR" VisibleIndex="2">
                                                            <PropertiesComboBox Width="300px" EnableCallbackMode="true" IncrementalFilteringMode="StartsWith">
                                                            </PropertiesComboBox>
                                                        </dx:GridViewDataComboBoxColumn>
                                                        <dx:GridViewDataDateColumn FieldName="FECHA" VisibleIndex="3" Caption="FECHA">
                                                            <PropertiesDateEdit Width="150px">
                                                            </PropertiesDateEdit>
                                                            <EditFormSettings Visible="False" />
                                                        </dx:GridViewDataDateColumn>
                                                        <dx:GridViewDataComboBoxColumn FieldName="ID_ORIGEN" Caption="ORIGEN" VisibleIndex="4">
                                                            <PropertiesComboBox Width="300px" EnableCallbackMode="true" IncrementalFilteringMode="StartsWith">
                                                            </PropertiesComboBox>
                                                        </dx:GridViewDataComboBoxColumn>
                                                        <dx:GridViewDataComboBoxColumn FieldName="ID_TIPO" Caption="TIPO" VisibleIndex="5">
                                                            <PropertiesComboBox Width="300px" EnableCallbackMode="true" IncrementalFilteringMode="StartsWith">
                                                            </PropertiesComboBox>
                                                        </dx:GridViewDataComboBoxColumn>
                                                        <dx:GridViewDataComboBoxColumn FieldName="ID_SECTOR_INT" Caption="SECTOR INT" VisibleIndex="5">
                                                            <PropertiesComboBox Width="300px" EnableCallbackMode="true" IncrementalFilteringMode="StartsWith">
                                                            </PropertiesComboBox>
                                                        </dx:GridViewDataComboBoxColumn>
                                                        <dx:GridViewDataMemoColumn FieldName="DESCRIPCION" Caption="DESC." VisibleIndex="6"
                                                            Width="300px">
                                                            <PropertiesMemoEdit Height="100px" Width="500px">
                                                            </PropertiesMemoEdit>
                                                            <EditFormSettings Caption="DESCRIPCION DE LA SITUACION DEL DESVIO" />
                                                        </dx:GridViewDataMemoColumn>
                                                        <dx:GridViewDataMemoColumn FieldName="MEDIDA_INMEDIATA" Caption="M.I." VisibleIndex="7"
                                                            Width="200px">
                                                            <PropertiesMemoEdit Height="100px" Width="500px">
                                                            </PropertiesMemoEdit>
                                                            <EditFormSettings Caption="MEDIDAS ADOPTADAS INMEDIATAMENTE" />
                                                        </dx:GridViewDataMemoColumn>
                                                        <dx:GridViewDataMemoColumn FieldName="INVESTIGACION" Caption="INV." VisibleIndex="8"
                                                            Width="200px">
                                                            <PropertiesMemoEdit Height="100px" Width="500px">
                                                            </PropertiesMemoEdit>
                                                            <EditFormSettings Caption="INVESTIGACION DE LAS CAUSAS" />
                                                        </dx:GridViewDataMemoColumn>
                                                        <dx:GridViewDataCheckColumn FieldName="CORRESPONDE_AC" Caption="AC" VisibleIndex="9">
                                                            <EditFormSettings Caption="CORRESPONDE ACCION CORRECTIVA" />
                                                        </dx:GridViewDataCheckColumn>
                                                        <dx:GridViewDataMemoColumn FieldName="ACCION_INMEDIATA" Caption="A.I." VisibleIndex="9"
                                                            Width="200px">
                                                            <PropertiesMemoEdit Height="100px" Width="500px">
                                                            </PropertiesMemoEdit>
                                                            <EditFormSettings Caption="ACCION CORRECTIVA/PLAZOS/RESPONSABLES" />
                                                        </dx:GridViewDataMemoColumn>
                                                        <dx:GridViewDataDateColumn FieldName="PLAZO" VisibleIndex="10" Caption="PLAZO">
                                                            <PropertiesDateEdit Width="150px">
                                                            </PropertiesDateEdit>
                                                        </dx:GridViewDataDateColumn>
                                                        <dx:GridViewDataComboBoxColumn FieldName="RESPONSABLE" Caption="RESP." VisibleIndex="10">
                                                            <PropertiesComboBox Width="300px" />
                                                            <EditFormSettings Caption="RESPONSABLE" />
                                                        </dx:GridViewDataComboBoxColumn>
                                                        <dx:GridViewDataMemoColumn FieldName="OBSERVACIONES" Caption="OBS." VisibleIndex="11"
                                                            Width="200px">
                                                            <PropertiesMemoEdit Height="100px" Width="500px">
                                                            </PropertiesMemoEdit>
                                                            <EditFormSettings Caption="OBSERVACIONES" />
                                                        </dx:GridViewDataMemoColumn>
                                                        <dx:GridViewDataTextColumn FieldName="PUNTO_NORMA" Caption="P.N." VisibleIndex="12">
                                                            <EditFormSettings Caption="PUNTO DE LA NORMA" />
                                                            <PropertiesTextEdit Width="100px">
                                                            </PropertiesTextEdit>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ARCHIVO" FieldName="ARCHIVO" VisibleIndex="12">
                                                            <EditFormSettings Caption="Archivo" />
                                                            <DataItemTemplate>
                                                                <a href="<%# Eval("archivo") %>" target="_blank">
                                                                    <%# ((Eval("archivo").ToString().Length > 0) ? "<img src='imagenes/file.gif' border='0' />" : "")%>
                                                                </a>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <dx:ASPxUploadControl OnFileUploadComplete="fileuploaded_C" ID="ASPxUploadControl2"
                                                                    runat="server" Width="300px" ShowUploadButton="True" UploadButton-Text="Cargar Informe"
                                                                    FileUploadMode="OnPageLoad" ShowProgressPanel="True" ClientSideEvents-FileUploadComplete="fileuploaded"
                                                                    AddUploadButtonsHorizontalPosition="InputRightSide">
                                                                </dx:ASPxUploadControl>
                                                            </EditItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <%-- <dx:GridViewDataCheckColumn FieldName="EFECTIVIDAD_SEG" Caption="EFECTIVIDAD_SEG" VisibleIndex="13">
                                                            <EditFormSettings Visible="False" />
                                                        </dx:GridViewDataCheckColumn>--%>
                                                    </Columns>
                                                </dx:ASPxGridView>
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
                                    </table>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:LayoutGroup>
            </Items>
        </dx:ASPxFormLayout>
    </div>
</asp:Content>
