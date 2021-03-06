﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="INTRANET_NOTIFICAR.aspx.cs"
    Inherits="INTRANET_NOTIFICAR" MasterPageFile="~/Site.master" %>

<%@ Register Assembly="DevExpress.Web.ASPxHtmlEditor.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxHtmlEditor" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxSpellChecker.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxSpellChecker" TagPrefix="dx" %>
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
    <table align="center">
        <tr>
            <td>
                <h2>
                    Notificacion por Mail a Usuarios
                </h2>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td valign="top" style="padding: 2px;">
                            Destinatarios
                            <br />
                            <asp:CheckBox ID="chk_destinatarios" runat="server" AutoPostBack="true" Text="Todos"
                                Checked="true" />
                        </td>
                        <td align="left" style="padding: 2px;">
                            <div id="Layer1" style="border: 1px solid #DCDCDC; position: relative; width: 350px;
                                height: 200px; overflow-x: hidden; overflow-y: scroll;">
                                <asp:CheckBoxList ID="trv_destinatarios" runat="server">
                                </asp:CheckBoxList>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 2px;">
                            Titulo
                        </td>
                        <td style="padding: 2px;">
                            <asp:TextBox ID="txt_titulo" runat="server" Width="350px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 2px;">
                            Notificacion
                        </td>
                        <td style="padding: 2px;">
                            <dx:ASPxHtmlEditor ID="ASPxHtmlEditor1" runat="server">
                            </dx:ASPxHtmlEditor>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="padding: 2px;">
                            <dx:ASPxButton ID="btn_enviar" runat="server" Text="Enviar" OnClick="btn_enviar_Click">
                            </dx:ASPxButton>
                            <dx:ASPxButton ID="btn_canacelar" runat="server" Text="Cancelar" OnClick="btn_canacelar_Click">
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <span class="failureNotification">
        <asp:Literal ID="FailureText" runat="server"></asp:Literal>
    </span>
</asp:Content>
