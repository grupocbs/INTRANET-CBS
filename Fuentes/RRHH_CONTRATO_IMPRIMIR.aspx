<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RRHH_CONTRATO_IMPRIMIR.aspx.cs"
    Inherits="RRHH_CONTRATO_IMPRIMIR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script language="javascript">
        window.print();
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table width="567px" align="center">
        <tr>
            <td>
                <div id="txt_descripcion" runat="server" width="567px" height="800px">
                </div>
            </td>
        </tr>
        <tr>
            <td>
            <div runat="server" id="PanelPDF">
               <%-- <iframe src="tmp/DocumentoJoin2.pdf" width="0px" height="0px"></iframe>--%>
            </div>
            
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
