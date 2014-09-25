<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RootNodeSettings.aspx.cs" MasterPageFile="~/umbraco/masterpages/umbracoPage.Master" Inherits="SEOChecker.Pages.Settings.RootNodeSettings" %>

<%@ Register Assembly="controls" Namespace="umbraco.uicontrols" TagPrefix="umbraco" %>
<%@ Register TagPrefix="SEOChecker" Namespace="SEOChecker.Core.Controls" Assembly="SEOChecker.Core" %>
<asp:Content ID="header" ContentPlaceHolderID="head" runat="server">
        <SEOChecker:CssRenderControl ID="CssRenderControl1" runat="server"></SEOChecker:CssRenderControl>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <umbraco:UmbracoPanel ID="UmbracoPanel" runat="server">
        <umbraco:Feedback runat="server" ID="NotFoundFeedback" type="notice" Visible="False"></umbraco:Feedback>
        <umbraco:Feedback runat="server" ID="DomainFeedback" type="error" Visible="False"  />
        <asp:panel ID="PagePanel" runat="server">
        <SEOChecker:SEOCheckerPanel ID="SEOCheckerPanel" runat="server" />
        
        <umbraco:Pane runat="server" ID="WildcardCanonicalPane">
            <umbraco:PropertyPanel runat="server" ID="WildcardCanonicalPanel">
                 <asp:TextBox runat="server" ID="CanonicalDomainTextBox" CssClass="umbEditorTextField"></asp:TextBox>
            </umbraco:PropertyPanel>
        </umbraco:Pane>
        <asp:PlaceHolder runat="server" ID="NotFoundPlaceHolder">
       <SEOChecker:ResourceHeaderControl ID="TriggerResourceHeader" ResourceKey="DomainSettings_NotFoundTitle" runat="server" />
        <umbraco:PropertyPanel runat="server" ID="NotfoundPanel" />
        <asp:PlaceHolder runat="server" ID="NotFoundPickerPanel"></asp:PlaceHolder>
            </asp:PlaceHolder>
            </asp:panel>
    </umbraco:UmbracoPanel>
</asp:Content>
