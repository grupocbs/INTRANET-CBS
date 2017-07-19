<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RRHH_INGRESOS_HERR_INF.aspx.cs"
    MaintainScrollPositionOnPostback="true" Inherits="RRHH_INGRESOS_HERR_INF" MasterPageFile="~/Site.master" %>

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
    <dx:ASPxFormLayout runat="server" ID="formLayout" CssClass="formLayout" SettingsItems-HorizontalAlign="Center"
        SettingsItems-Width="1024px">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
        <Items>
            <dx:LayoutGroup Caption="Herramientas Informaticas" GroupBoxDecoration="HeadingLine" ColCount="1"
                UseDefaultPaddings="false" Paddings-PaddingTop="10">
                <GroupBoxStyle>
                    <Caption Font-Bold="true" Font-Size="14" />
                </GroupBoxStyle>
            </dx:LayoutGroup>
            <dx:LayoutGroup Caption="Ingresos que requieren Herramientas Informaticas" GroupBoxDecoration="box">
                <Items>
                    <dx:LayoutItem Caption="">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxLabel ID="FailureText" runat="server" ForeColor="Red" Font-Size="14">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Ingresos">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxComboBox ID="cmb_ingresos" runat="server" Width="400px" AutoPostBack="true"
                                    OnSelectedIndexChanged="cmb_ingresos_SelectedIndexChanged">
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Herramientas Informaticas">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxGridView ID="ASPxGridView1" runat="server" KeyFieldName="ID" OnHtmlRowPrepared="ASPxGridView1_HtmlRowPrepared"
                                    ClientInstanceName="grid" Width="100%" OnRowUpdating="ASPxGridView1_RowUpdating">
                                    <ClientSideEvents BatchEditEndEditing="OnBatchEditEndEditing" CustomButtonClick="OnCustomButtonClick" />
                                    <SettingsBehavior ConfirmDelete="True" AllowSort="false" />
                                    <SettingsEditing Mode="Batch" EditFormColumnCount="2" BatchEditSettings-EditMode="Cell" />
                                    <SettingsDetail ExportMode="Expanded" />
                                    <Settings ShowStatusBar="Hidden" />
                                    <SettingsPager Mode="ShowAllRecords" />
                                    <SettingsBehavior AllowSort="false" AllowGroup="false" />
                                    <Columns>
                                        <dx:GridViewDataColumn VisibleIndex="1" FieldName="ID" Caption="Nº" ReadOnly="true">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn VisibleIndex="2" FieldName="ITEM" Caption="ITEM" ReadOnly="true">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn VisibleIndex="3" FieldName="OBSERVACION" ReadOnly="true">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataCheckColumn VisibleIndex="4" FieldName="COMPLETADO">
                                        </dx:GridViewDataCheckColumn>
                                    </Columns>
                                    <SettingsDetail IsDetailGrid="True" />
                                </dx:ASPxGridView>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
        </Items>
    </dx:ASPxFormLayout>
</asp:Content>
