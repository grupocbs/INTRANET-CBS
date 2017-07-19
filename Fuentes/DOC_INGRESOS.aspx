<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DOC_INGRESOS.aspx.cs" MasterPageFile="~/Site.master"
    Inherits="DOC_INGRESOS" MaintainScrollPositionOnPostback="true" %>

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
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <%-- <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>--%>
    <dx:ASPxFormLayout runat="server" ID="formLayout" CssClass="formLayout" SettingsItems-Width="100%">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
        <Items>
            <dx:LayoutGroup Caption="Documentacion de Ingresos" GroupBoxDecoration="HeadingLine"
                ColCount="1" UseDefaultPaddings="false" Paddings-PaddingTop="10">
                <GroupBoxStyle>
                    <Caption Font-Bold="true" Font-Size="14" />
                </GroupBoxStyle>
            </dx:LayoutGroup>
            <dx:LayoutGroup Caption="Ingresos" GroupBoxDecoration="box">
                <Items>
                    <dx:LayoutItem Caption="">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxLabel ID="FailureText" runat="server" ForeColor="Red" Font-Size="14">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Sector">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxComboBox ID="cmb_sectores" runat="server" Width="300px" Enabled="false">
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Altas">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxGridView ID="ASPxGridView1" runat="server" KeyFieldName="ID" ClientInstanceName="ASPxGridView1"
                                    OnHtmlRowPrepared="ASPxGridView1_HtmlRowPrepared" OnInit="ASPxGridView1_Init"
                                    Width="100%" OnSelectionChanged="ASPxGridView1_SelectionChanged" Font-Size="X-Small"
                                    EnableCallBacks="false">
                                    <SettingsBehavior AllowSort="false" AllowGroup="false" AllowSelectByRowClick="True"
                                        AllowSelectSingleRowOnly="True" ProcessSelectionChangedOnServer="True" />
                                    <SettingsDetail ExportMode="Expanded" ShowDetailRow="true" />
                                    <Settings ShowStatusBar="Hidden" ShowHeaderFilterBlankItems="true" ShowHeaderFilterButton="True" />
                                    <SettingsText CommandSelect="Ver Doc. (Cuadro Inferior)" />
                                    <Columns>
                                        <dx:GridViewCommandColumn VisibleIndex="0" ShowSelectButton="true">
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataColumn VisibleIndex="1" FieldName="ID" Caption="Nº">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn VisibleIndex="2" FieldName="ID_RECLUTAMIENTO" Caption="RECLUT.">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn VisibleIndex="3" FieldName="FECHA_ALTA" Caption="ALTA">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn VisibleIndex="4" FieldName="CUIL">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn VisibleIndex="5" FieldName="APELLIDO">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn VisibleIndex="6" FieldName="NOMBRES">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataComboBoxColumn VisibleIndex="7" FieldName="COD_CONV" Caption="CCT">
                                        </dx:GridViewDataComboBoxColumn>
                                        <dx:GridViewDataComboBoxColumn VisibleIndex="8" FieldName="COD_CATEGORIA" Caption="CAT">
                                        </dx:GridViewDataComboBoxColumn>
                                        <dx:GridViewDataComboBoxColumn VisibleIndex="9" FieldName="COD_CLIENTE" Caption="CLIENTE">
                                        </dx:GridViewDataComboBoxColumn>
                                        <dx:GridViewDataComboBoxColumn VisibleIndex="9" FieldName="COD_ZONA" Caption="LOC.">
                                        </dx:GridViewDataComboBoxColumn>
                                        <dx:GridViewDataComboBoxColumn VisibleIndex="10" FieldName="RAZON_SOCIAL" Caption="RAZON SOCIAL">
                                        </dx:GridViewDataComboBoxColumn>
                                        <dx:GridViewDataComboBoxColumn VisibleIndex="11" FieldName="TIPO_CONTRATO" Caption="CONTRATO">
                                        </dx:GridViewDataComboBoxColumn>
                                        <dx:GridViewDataMemoColumn VisibleIndex="12" FieldName="OBSERVACIONES">
                                        </dx:GridViewDataMemoColumn>
                                    </Columns>
                                    <Templates>
                                        <DetailRow>
                                            <div style="padding: 3px 3px 2px 3px">
                                                <dx:ASPxPageControl runat="server" ID="pageControl" Width="100%" EnableCallBacks="true">
                                                    <TabPages>
                                                        <dx:TabPage Text="Detalles" Visible="true">
                                                            <ContentCollection>
                                                                <dx:ContentControl ID="ContentControl1" runat="server">
                                                                    <dx:ASPxGridView ID="detalleGrid" runat="server" KeyFieldName="ID" Width="100%" OnBeforePerformDataSelect="detalleGrid_DataSelect"
                                                                        AutoGenerateColumns="false" OnInit="detalleGrid_Init">
                                                                        <SettingsBehavior AllowSort="false" AllowGroup="false" />
                                                                        <Columns>
                                                                            <dx:GridViewDataColumn VisibleIndex="1" FieldName="ID" Caption="Nº">
                                                                            </dx:GridViewDataColumn>
                                                                            <dx:GridViewDataColumn VisibleIndex="3" FieldName="FECHA_BAJA" Caption="BAJA">
                                                                            </dx:GridViewDataColumn>
                                                                            <dx:GridViewDataColumn VisibleIndex="7" FieldName="DOMICILIO_REAL" Caption="DOM.">
                                                                            </dx:GridViewDataColumn>
                                                                            <dx:GridViewDataComboBoxColumn VisibleIndex="8" FieldName="COD_OS" Caption="O.S.">
                                                                            </dx:GridViewDataComboBoxColumn>
                                                                            <dx:GridViewDataComboBoxColumn VisibleIndex="11" FieldName="COD_CENTRO_COSTO" Caption="C.COSTO">
                                                                            </dx:GridViewDataComboBoxColumn>
                                                                            <dx:GridViewDataCheckColumn VisibleIndex="12" FieldName="INTERNO" Caption="INT.">
                                                                            </dx:GridViewDataCheckColumn>
                                                                            <dx:GridViewDataComboBoxColumn VisibleIndex="14" FieldName="COD_OBJETIVO" Caption="OBJ.">
                                                                            </dx:GridViewDataComboBoxColumn>
                                                                        </Columns>
                                                                        <SettingsDetail IsDetailGrid="True" />
                                                                    </dx:ASPxGridView>
                                                                </dx:ContentControl>
                                                            </ContentCollection>
                                                        </dx:TabPage>
                                                        <dx:TabPage Text="Herramientas Informaticas" Visible="true">
                                                            <ContentCollection>
                                                                <dx:ContentControl ID="ContentControl2" runat="server">
                                                                    <dx:ASPxGridView ID="herinfGrid" runat="server" KeyFieldName="ID" Width="100%" OnBeforePerformDataSelect="herinfGrid_DataSelect"
                                                                        AutoGenerateColumns="false">
                                                                        <SettingsBehavior AllowSort="false" AllowGroup="false" />
                                                                        <Columns>
                                                                            <dx:GridViewDataColumn VisibleIndex="1" FieldName="ID" Caption="Nº">
                                                                            </dx:GridViewDataColumn>
                                                                            <dx:GridViewDataColumn VisibleIndex="3" FieldName="ITEM" Caption="ITEM">
                                                                            </dx:GridViewDataColumn>
                                                                            <dx:GridViewDataColumn VisibleIndex="7" FieldName="OBSERVACION">
                                                                            </dx:GridViewDataColumn>
                                                                        </Columns>
                                                                        <SettingsDetail IsDetailGrid="True" />
                                                                    </dx:ASPxGridView>
                                                                </dx:ContentControl>
                                                            </ContentCollection>
                                                        </dx:TabPage>
                                                    </TabPages>
                                                </dx:ASPxPageControl>
                                            </div>
                                        </DetailRow>
                                    </Templates>
                                </dx:ASPxGridView>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
            <dx:LayoutGroup Caption="Documentacion" GroupBoxDecoration="box">
                <Items>
                    <dx:LayoutItem Caption="Archivos Recibidos">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxGridView ID="ASPxGridView2" Visible="false" runat="server" KeyFieldName="ID_DOC_CCT"
                                    OnRowDeleting="ASPxGridView2_RowDeleting" Width="100%" EnableCallBacks="true">
                                    <SettingsBehavior ConfirmDelete="True" AllowSort="false" />
                                    <SettingsDetail ExportMode="Expanded" ShowDetailRow="true" />
                                    <Settings ShowStatusBar="Hidden" />
                                    <SettingsPager Mode="ShowAllRecords" />
                                    <SettingsText ConfirmDelete="Desea eliminar el Archivo Recibido? (primero debe hacer una Observacion)" />
                                    <SettingsCommandButton DeleteButton-Text="QUITAR" />
                                    <ClientSideEvents EndCallback="function OnEndCallback(s,e)
                                    {          
                                        if (s.cpConfirmationMessage!= null && s.cpConfirmationMessage != '')
                                        {
                                            
                                               alert(s.cpConfirmationMessage);
                                               txt_observaciones.Focus();
                                        }
                                       
                                    }" />
                                    <Columns>
                                        <dx:GridViewCommandColumn VisibleIndex="0" ShowDeleteButton="true">
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataColumn VisibleIndex="1" FieldName="ID" Visible="false">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn VisibleIndex="1" FieldName="ID_DOC_CCT" Visible="false">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn VisibleIndex="2" FieldName="DESCRIPCION">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn VisibleIndex="3" FieldName="ARCHIVO">
                                            <DataItemTemplate>
                                                <a href="<%# Eval("ARCHIVO").ToString().Replace("~/","") %>" target="_blank">
                                                    <%# ((Eval("ARCHIVO").ToString().Length > 0) ? "<img src='imagenes/file.gif' border='0' />" : "")%>
                                                </a>
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn VisibleIndex="4" FieldName="FECHA" Caption="FECHA ENVIO">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                    <Templates>
                                        <DetailRow>
                                            <div style="padding: 3px 3px 2px 3px">
                                                <dx:ASPxPageControl runat="server" ID="pageControl" Width="100%" EnableCallBacks="true">
                                                    <TabPages>
                                                        <dx:TabPage Text="Observaciones" Visible="true">
                                                            <ContentCollection>
                                                                <dx:ContentControl ID="ContentControl1" runat="server">
                                                                    <dx:ASPxGridView ID="detalleGrid1" runat="server" KeyFieldName="ID" Width="100%"
                                                                        OnBeforePerformDataSelect="detalleGrid1_DataSelect" AutoGenerateColumns="false">
                                                                        <SettingsBehavior AllowSort="false" AllowGroup="false" />
                                                                        <Columns>
                                                                            <dx:GridViewDataColumn VisibleIndex="1" FieldName="ID" Visible="false">
                                                                            </dx:GridViewDataColumn>
                                                                            <dx:GridViewDataColumn VisibleIndex="3" FieldName="FECHA">
                                                                            </dx:GridViewDataColumn>
                                                                            <dx:GridViewDataColumn VisibleIndex="15" FieldName="OBSERVACION">
                                                                            </dx:GridViewDataColumn>
                                                                        </Columns>
                                                                        <SettingsDetail IsDetailGrid="True" />
                                                                    </dx:ASPxGridView>
                                                                </dx:ContentControl>
                                                            </ContentCollection>
                                                        </dx:TabPage>
                                                    </TabPages>
                                                </dx:ASPxPageControl>
                                            </div>
                                        </DetailRow>
                                    </Templates>
                                </dx:ASPxGridView>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Observacion">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxMemo ID="txt_observaciones" Visible="false" runat="server" Width="500px"
                                    HelpText="Si el archivo recibido no corresponde, escriba aqui su observación y luego clic en QUITAR en la lista de Archivos Recibidos"
                                    HelpTextSettings-DisplayMode="Popup" ClientInstanceName="txt_observaciones" Height="80px">
                                </dx:ASPxMemo>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <%--<dx:LayoutItem Caption="">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <table align="left">
                                    <tr>
                                      
                                        <td style="padding: 2px;">
                                            <dx:ASPxButton ID="btn_enviar_obs" Visible="false" runat="server" Text="Enviar observacion a RRHH"
                                                OnClick="btn_enviar_Click" Width="100px">
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>--%>
                </Items>
            </dx:LayoutGroup>
        </Items>
    </dx:ASPxFormLayout>
    <%--</div>
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
    </asp:UpdateProgress>--%>
</asp:Content>
