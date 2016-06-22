<%@ Page Title="Página principal" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="UsuariosPermisos.aspx.cs" Inherits="UsuariosPermisos" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table align="center" border="1">
                <tr valign="top">
                    <td valign="top">
                        <table>
                            <tr>
                                <td>
                                    <h2>
                                        Administrar Permisos de Usuarios</h2>
                                    <span class="failureNotification">
                                        <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" ValueType="System.String" EnableCallbackMode="true"
                                        AutoPostBack="true" IncrementalFilteringMode="StartsWith" OnSelectedIndexChanged="ASPxComboBox1_SelectedIndexChanged">
                                    </dx:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="Button1" runat="server" Text="Guardar Permisos Seleccionados" OnClick="Button1_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dx:ASPxTreeView ID="TreeView1" runat="server" ShowExpandButtons="true" ShowTreeLines="False"
                                        AllowCheckNodes="true" OnCheckedChanged="TreeView1_TreeNodeCheckChanged">
                                    </dx:ASPxTreeView>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td valign="top">
                        <table>
                            <tr>
                                <td>
                                    <h2>
                                        Copiar Permisos de Usuarios</h2>
                                    <span class="failureNotification">
                                        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Seleccione el usuario "origen" de la columna izquierda y
                                    <br />
                                    luego el usuario "destino" a quien se le copiaran los permisos.
                                    <dx:ASPxComboBox ID="ASPxComboBox2" runat="server" ValueType="System.String" EnableCallbackMode="true"
                                        IncrementalFilteringMode="StartsWith">
                                    </dx:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="Button2" runat="server" Text="Copiar Permisos Seleccionados" OnClick="Button2_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
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
    </asp:UpdateProgress>
</asp:Content>
