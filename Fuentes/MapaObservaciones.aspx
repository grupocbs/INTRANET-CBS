<%@ Page Title="Página principal" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="MapaObservaciones.aspx.cs" Inherits="MapaObservaciones" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
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
                        <td>
                            Fecha Desde:
                        </td>
                        <td>
                            <asp:TextBox ID="ASPxCalendarDesde" runat="server" ValidationGroup="reporte" Width="90px"></asp:TextBox>
                            <cc2:MaskedEditExtender ID="ASPxCalendarDesde_MaskedEditExtender" runat="server"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="ASPxCalendarDesde">
                            </cc2:MaskedEditExtender>
                            <cc2:CalendarExtender ID="ASPxCalendarDesde_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="ASPxCalendarDesde">
                            </cc2:CalendarExtender>
                        </td>
                        <td>
                            Fecha Hasta:
                        </td>
                        <td>
                            <asp:TextBox ID="ASPxCalendarHasta" runat="server" ValidationGroup="reporte" Width="90px"></asp:TextBox>
                            <cc2:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="ASPxCalendarHasta">
                            </cc2:MaskedEditExtender>
                            <cc2:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="ASPxCalendarHasta">
                            </cc2:CalendarExtender>
                        </td>
                        <td>
                            Supervisores:
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="cmb_usuarios" runat="server" ValueType="System.String" AutoPostBack="true"
                                IncrementalFilteringMode="StartsWith" OnSelectedIndexChanged="cmb_usuarios_SelectedIndexChanged">
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <cc1:GMap ID="GMap1" runat="server" Width="100%" Height="600px" />
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
