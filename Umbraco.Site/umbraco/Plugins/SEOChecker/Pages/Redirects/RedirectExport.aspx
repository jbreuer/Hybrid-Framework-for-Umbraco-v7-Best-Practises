<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RedirectExport.aspx.cs" Inherits="SEOChecker.Pages.Redirects.RedirectExport" MasterPageFile="~/umbraco/masterpages/umbracoPage.Master" %>
<%@ Register Assembly="controls" Namespace="umbraco.uicontrols" TagPrefix="umbraco" %>
<%@ Register TagPrefix="SEOChecker" Namespace="SEOChecker.Core.Controls" Assembly="SEOChecker.Core" %>
<asp:Content ID="header" ContentPlaceHolderID="head" runat="server">
    <SEOChecker:CssRenderControl ID="CssRenderControl1" runat="server"></SEOChecker:CssRenderControl>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <umbraco:UmbracoPanel ID="UmbracoPanel" runat="server">
        <asp:CustomValidator runat="server" ID="ExportValidator" ControlToValidate="ExportProviderList" CssClass="error seoCheckerError" OnServerValidate="ExportValidator_OnServerValidate" Display="Dynamic"></asp:CustomValidator>
        <asp:panel ID="PagePanel" runat="server">
        <SEOChecker:SEOCheckerPanel ID="SEOCheckerPanel" runat="server"  />        
        <umbraco:Pane runat="server" ID="ExportType">
            <umbraco:PropertyPanel runat="server" ID="ExportTypePanel">
                <asp:DropDownList runat="server" ID="ExportProviderList" AutoPostBack="True"/>
            </umbraco:PropertyPanel>
        </umbraco:Pane>
        
        <asp:PlaceHolder runat="server" ID="ExportProviderPlaceHolder"></asp:PlaceHolder>
        
        <umbraco:Pane runat="server" ID="ExportButtonPane">
            <umbraco:PropertyPanel runat="server" ID="ExportButtonPanel">
                <asp:Button runat="server" ID="ExportButton" OnClick="ExportButton_OnClick" CssClass="btn btn-success"/>
            </umbraco:PropertyPanel>
        </umbraco:Pane>
        </asp:panel>

    </umbraco:UmbracoPanel>
</asp:Content>