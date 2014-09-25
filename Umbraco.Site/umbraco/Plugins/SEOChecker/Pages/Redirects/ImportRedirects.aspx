<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportRedirects.aspx.cs" Inherits="SEOChecker.Pages.Redirects.ImportRedirects" MasterPageFile="~/umbraco/masterpages/umbracoPage.Master" %>

<%@ Register TagPrefix="umbraco" Namespace="umbraco.uicontrols" Assembly="controls" %>
<%@ Register TagPrefix="SEOChecker" Namespace="SEOChecker.Core.Controls" Assembly="SEOChecker.Core" %>
<asp:Content ID="header" ContentPlaceHolderID="head" runat="server">
        <SEOChecker:CssRenderControl ID="CssRenderControl1" runat="server"></SEOChecker:CssRenderControl>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <umbraco:UmbracoPanel ID="UmbracoPanel" runat="server">
          <umbraco:Feedback ID="SEOCheckerImportError" runat="server" type="error" Visible="false" />
        <asp:CustomValidator runat="server" ID="ImportValidator" ControlToValidate="ImportTypeDropdownList" CssClass="error seoCheckerError" ValidateEmptyText="True" OnServerValidate="ImportValidator_OnServerValidate" Display="Dynamic"></asp:CustomValidator>
        <asp:panel ID="PagePanel" runat="server">
        <SEOChecker:SEOCheckerPanel ID="SEOCheckerPanel" runat="server" />
        <asp:PlaceHolder runat="server" ID="ImportFormPlaceHolder">
            <SEOChecker:ResourceHeaderControl runat="server" ID="ResourceDatasourceHeaderControl" ResourceKey="ImportRedirect_DatasourceOptions"></SEOChecker:ResourceHeaderControl>
            <umbraco:Pane runat="server" ID="ImportTypePane">
                <umbraco:PropertyPanel runat="server" ID="ImportTypePanel">
                    <asp:DropDownList runat="server" ID="ImportTypeDropdownList" AutoPostBack="True" />
                </umbraco:PropertyPanel>
            </umbraco:Pane>

            <umbraco:Pane runat="server" ID="ImportFilePane">
                <umbraco:PropertyPanel runat="server" ID="ImportFilePanel">
                    <SEOChecker:FileUploadPicker runat="server" ID="FileUploadPicker" />
                </umbraco:PropertyPanel>
            </umbraco:Pane>
            <asp:PlaceHolder runat="server" ID="ImportProviderPlaceHolder"></asp:PlaceHolder>
            <asp:PlaceHolder runat="server" ID="AdditionalImportOptionsPlaceholder">
                <SEOChecker:ResourceHeaderControl runat="server" ID="MappingHeaderControl1" ResourceKey="ImportRedirect_Mapping"></SEOChecker:ResourceHeaderControl>
                <umbraco:Pane runat="server" ID="SourceColumnPane">
                    <umbraco:PropertyPanel runat="server" ID="SourceColumnPropertyPanel">
                        <asp:DropDownList runat="server" ID="SourceColumnDropdownList" />
                    </umbraco:PropertyPanel>
                </umbraco:Pane>

                <umbraco:Pane runat="server" ID="TargetColumnPane">
                    <umbraco:PropertyPanel runat="server" ID="TargetColumnPanel">
                        <asp:DropDownList runat="server" ID="TargetColumnDropdownList" />
                    </umbraco:PropertyPanel>
                </umbraco:Pane>
                  <SEOChecker:ResourceHeaderControl runat="server" ID="ResourceHeaderControl1" ResourceKey="ImportRedirect_MappingOptions"></SEOChecker:ResourceHeaderControl>
                <umbraco:Pane runat="server" ID="TryMapPane">
                    <umbraco:PropertyPanel runat="server" ID="TryMapPanel">
                        <asp:CheckBox runat="server" ID="TryMapCheckBox" AutoPostBack="True" />
                    </umbraco:PropertyPanel>
                </umbraco:Pane>

                <umbraco:Pane runat="server" ID="OnlyImportWhenMappedPane">
                    <umbraco:PropertyPanel runat="server" ID="OnlyImportWhenMappedPanel">
                        <asp:CheckBox runat="server" ID="OnlyImportWhenMappedCheckbox" />
                    </umbraco:PropertyPanel>
                </umbraco:Pane>

            </asp:PlaceHolder>
            <umbraco:Pane runat="server" ID="ImportButtonPane">
                <umbraco:PropertyPanel runat="server" ID="ImportButtonPanel">
                    <asp:Button runat="server" ID="ImportButton" OnClick="ImportButton_OnClick"  CssClass="btn btn-success"  />
                </umbraco:PropertyPanel>
            </umbraco:Pane>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="ImportFinishedPlaceHolder">
            <umbraco:Pane runat="server" ID="ImportCountPane">
                <umbraco:PropertyPanel runat="server" ID="ImportCountPanel">
                    <asp:Literal runat="server" ID="ImportCountLiteral"></asp:Literal>
                </umbraco:PropertyPanel>
            </umbraco:Pane>
            <asp:PlaceHolder runat="server" ID="ImportSkippedPlaceholder">
                <umbraco:Pane runat="server" ID="ImportSkippedPane">
                    <umbraco:PropertyPanel runat="server" ID="ImportSkippedPanel">
                        <asp:Literal runat="server" ID="ImportSkippedLiteral"></asp:Literal>
                    </umbraco:PropertyPanel>
                </umbraco:Pane>
                <umbraco:Pane runat="server" ID="ExportSkippedPane">
                    <umbraco:PropertyPanel runat="server" ID="ExportSkippedPanel">
                        <asp:Button runat="server" ID="ExportErrorsButton" CausesValidation="False" OnClick="ExportErrorsButton_OnClick" CssClass="btn btn-success" />
                    </umbraco:PropertyPanel>
                </umbraco:Pane>
            </asp:PlaceHolder>
        </asp:PlaceHolder>
            </asp:panel>
    </umbraco:UmbracoPanel>
</asp:Content>
