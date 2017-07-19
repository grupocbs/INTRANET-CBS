<%@ Page Title="Página principal" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="OS_OBSERVACIONES.aspx.cs" Inherits="OS_OBSERVACIONES" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="GMaps" Namespace="Subgurim.Controles" TagPrefix="cc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <%--  <style type="text/css">
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
    </style>--%>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <table align="center" width="100%">
        <tr>
            <td>
                <table align="center">
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
                        <td style="padding: 2px;">
                            Estado:
                        </td>
                        <td style="padding: 2px;">
                            <dx:ASPxComboBox ID="cmb_estados" runat="server" ValueType="System.String" AutoPostBack="true"
                                IncrementalFilteringMode="StartsWith" OnSelectedIndexChanged="cmb_usuarios_SelectedIndexChanged">
                            </dx:ASPxComboBox>
                        </td>
                        <td style="padding: 2px;">
                            Supervisores:
                        </td>
                        <td style="padding: 2px;">
                            <dx:ASPxComboBox ID="cmb_usuarios" runat="server" ValueType="System.String" AutoPostBack="true"
                                IncrementalFilteringMode="StartsWith" OnSelectedIndexChanged="cmb_usuarios_SelectedIndexChanged">
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="padding: 2px;">
                <asp:Panel ID="panel_buscarequipo" GroupingText="Encontrar Equipo" runat="server">
                    <table>
                        <tr>
                            <td>
                                <u>Ingresar al siguiente link con el Mail y contraseña.</u>
                            </td>
                            <td>
                                <a target="_blank" href="https://findmymobile.samsung.com/">https://findmymobile.samsung.com/</a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <u>Mail (Usuario):</u>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lbl_mail"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <u>Contraseña:</u>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lbl_contra"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td style="padding: 2px;">
                <asp:Panel ID="panel1" GroupingText="Observaciones Cargadas" runat="server">
                    <table width="100%" align="center">
                        <tr>
                            <td valign="top">
                                <dx:ASPxGridView ID="ASPxGridView1" runat="server" Width="100%" KeyFieldName="CODIGO"
                                    OnHtmlDataCellPrepared="ASPxGridView1_HtmlDataCellPrepared" EnableCallBacks="false"
                                    ClientInstanceName="grid" OnCustomButtonCallback="fotosGrid_CustomButtonCallback">
                                    <ClientSideEvents CustomButtonClick="function(s, e) 
                     {
                        e.processOnServer = true;

                     }" />
                                    <Settings ShowStatusBar="Hidden" />
                                    <SettingsPager Mode="ShowAllRecords" />
                                    <Columns>
                                        <dx:GridViewDataColumn VisibleIndex="1" FieldName="CODIGO">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn VisibleIndex="2" FieldName="SUP">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn VisibleIndex="3" FieldName="OBJETIVO">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn VisibleIndex="4" FieldName="FECHA">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn VisibleIndex="5" FieldName="TIPO">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn VisibleIndex="6" FieldName="ESTADO">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn VisibleIndex="7" FieldName="FOTOS">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn VisibleIndex="8" FieldName="OBS">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn VisibleIndex="9" FieldName="RTA">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewCommandColumn VisibleIndex="10">
                                            <CustomButtons>
                                                <dx:GridViewCommandColumnCustomButton ID="button" Text="Mapa">
                                                </dx:GridViewCommandColumnCustomButton>
                                            </CustomButtons>
                                        </dx:GridViewCommandColumn>
                                    </Columns>
                                    <Templates>
                                        <DetailRow>
                                            <div style="padding: 3px 3px 2px 3px">
                                                <dx:ASPxPageControl runat="server" ID="pageControl" EnableCallBacks="true">
                                                    <TabPages>
                                                        <dx:TabPage Text="FOTOS">
                                                            <ContentCollection>
                                                                <dx:ContentControl ID="ContentControl2" runat="server">
                                                                    <dx:ASPxGridView ID="fotosGrid" runat="server" KeyFieldName="TITULO" OnBeforePerformDataSelect="fotosGrid_DataSelect"
                                                                        AutoGenerateColumns="false">
                                                                        <Columns>
                                                                            <dx:GridViewDataColumn FieldName="TITULO" VisibleIndex="1" />
                                                                            <dx:GridViewDataColumn FieldName="FOTO" Caption="FOTO" VisibleIndex="2" Width="50px">
                                                                                <DataItemTemplate>
                                                                                    <table align="center">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <%# "<a target='_blank' href='http://192.168.1.141:3333/archivos/capturas/" + Eval("SUP").ToString() + "/" + Eval("CODIGO").ToString() + "/" + Eval("TITULO").ToString()+ "'><img width='50px' src='http://192.168.1.141:3333/archivos/capturas/" + Eval("SUP").ToString() + "/" + Eval("CODIGO").ToString() + "/" + Eval("TITULO").ToString() +"' border='0' /></a>"%>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </DataItemTemplate>
                                                                            </dx:GridViewDataColumn>
                                                                        </Columns>
                                                                        <Settings ShowFooter="True" />
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
                                    <SettingsDetail ShowDetailRow="true" />
                                </dx:ASPxGridView>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="padding: 2px;">
                                <div id="seccion1">
                                    <cc1:GMap ID="GMap1" runat="server" Width="100%" Height="400px" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <%-- </ContentTemplate>
   </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="200">
        <ProgressTemplate>
            <div id="background">
            </div>
            <div id="process">
                <h6>
                    <p style="text-align: center">
                        <b>Espere por favor...</br></b></p>
                </h6>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
</asp:Content>
