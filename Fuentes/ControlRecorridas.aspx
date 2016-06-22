<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ControlRecorridas.aspx.cs"
    Inherits="ControlRecorridas" MasterPageFile="~/Site.master" %>

<%@ Register Assembly="GMaps" Namespace="Subgurim.Controles" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table align="center">
        <tr>
            <td>
                <table align="center" border="1px">
                    <tr>
                        <td>
                            Abrir Archivo
                        </td>
                        <td>
                            <asp:FileUpload ID="btn_abrir" runat="server" ValidationGroup="valores" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Recorrido
                        </td>
                        <td>
                            <asp:Label ID="txt_archivo" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Intervalos de:
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="txt_intervalo" runat="server" Width="30px">
                                <MaskSettings Mask="<00..99>" />
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Unidad:
                        </td>
                        <td>
                            <asp:RadioButtonList ID="cmb_unidades" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="Segundos" Text="Segundos" Selected="True" />
                                <asp:ListItem Value="Minutos" Text="Minutos" />
                                <asp:ListItem Value="Horas" Text="Horas" />
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="right">
                            <asp:Button ID="btn_procesar" ValidationGroup="valores" runat="server" Text="Procesar"
                                OnClick="UploadButton_Click" />
                        </td>
                    </tr>
                </table>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <b>
                    <asp:Label ID="txt_totalrecorrido" runat="server" Text=""></asp:Label></b>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td valign="top">
                            <dx:ASPxGridView ID="ASPxGridView1" runat="server" KeyFieldName="ID" ClientInstanceName="grid"
                                Width="500px">
                                <SettingsDetail ExportMode="Expanded" />
                                <Settings ShowStatusBar="Hidden" />
                                <SettingsPager Mode="ShowAllRecords" />
                                <Columns>
                                    <dx:GridViewDataColumn VisibleIndex="1" FieldName="ID">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn VisibleIndex="2" FieldName="FECHA_HORA">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn VisibleIndex="3" FieldName="LATITUD">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn VisibleIndex="4" FieldName="LONGITUD">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataCheckColumn VisibleIndex="5" FieldName="VELOCIDAD">
                                    </dx:GridViewDataCheckColumn>
                                </Columns>
                            </dx:ASPxGridView>
                        </td>
                        <td valign="top">
                            <cc1:GMap ID="GMap1" runat="server" Width="600px" Height="400px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
