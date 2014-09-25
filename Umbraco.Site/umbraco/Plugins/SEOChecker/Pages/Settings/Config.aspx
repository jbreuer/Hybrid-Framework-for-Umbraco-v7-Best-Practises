<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Config.aspx.cs" MasterPageFile="~/umbraco/masterpages/umbracoPage.Master" Inherits="SEOChecker.Pages.Settings.Config" %>

<%@ Register Assembly="controls" Namespace="umbraco.uicontrols" TagPrefix="umbraco" %>
<%@ Register TagPrefix="SEOChecker" Namespace="SEOChecker.Core.Controls" Assembly="SEOChecker.Core" %>
<%@ Import Namespace="SEOChecker.Resources" %>
<asp:Content ID="header" ContentPlaceHolderID="head" runat="server">
        <SEOChecker:CssRenderControl ID="CssRenderControl1" runat="server"></SEOChecker:CssRenderControl>
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <umbraco:UmbracoPanel ID="UmbracoPanel" runat="server">
        <asp:panel ID="PagePanel" runat="server">
        <SEOChecker:SEOCheckerPanel ID="SEOCheckerPanel" runat="server" />
            <SEOChecker:ResourceHeaderControl runat="server" ID="TriggerResourceHeader" ResourceKey="SEOCheckerConfiguration_Triggers"></SEOChecker:ResourceHeaderControl>
        <umbraco:Pane ID="TriggerPane" runat="server">
            <umbraco:PropertyPanel ID="TriggerPanel" runat="server">
                <asp:CheckBox ID="publishTriggerCheckbox" runat="server" /></umbraco:PropertyPanel>
        </umbraco:Pane>
             <SEOChecker:ResourceHeaderControl runat="server" ID="XmlSitemapResourceHeader" ResourceKey="SEOCheckerConfiguration_XmlSitemap"></SEOChecker:ResourceHeaderControl>
        <umbraco:Pane ID="SitemapPane" runat="server">
            <umbraco:PropertyPanel ID="EnableXmlSitemapPanel" runat="server">
                <asp:CheckBox ID="EnableXmlSitemapCheckbox" runat="server"  AutoPostBack="true"/></umbraco:PropertyPanel>
            <umbraco:PropertyPanel ID="ExcludeNavihide" runat="server" Text="">
                <asp:CheckBox ID="ExcludeXmlSitemapUmbracoNavihide" runat="server" /></umbraco:PropertyPanel>
            <umbraco:PropertyPanel ID="PreviewSiteMapXmlPanel" runat="server"><a href="/sitemap.xml" target="_blank" class="btn">Preview sitemap.xml file</a></umbraco:PropertyPanel>
        </umbraco:Pane>
             <SEOChecker:ResourceHeaderControl runat="server" ID="RobotsResourceHeaderControl" ResourceKey="SEOCheckerConfiguration_Robots"></SEOChecker:ResourceHeaderControl>
        <umbraco:Pane ID="RobotstxtPane" runat="server">
            <umbraco:PropertyPanel ID="RobotsTxtPanel" runat="server">
                <asp:CheckBox ID="EnableRobotsTxtCheckbox" runat="server" /></umbraco:PropertyPanel>
            <umbraco:PropertyPanel ID="PreviewRobotsTxtPanel" runat="server"><a href="/robots.txt" target="_blank" class="btn">Preview robots.txt file</a></umbraco:PropertyPanel>
        </umbraco:Pane>
             <SEOChecker:ResourceHeaderControl runat="server" ID="RewriteResourceHeaderControl" ResourceKey="SEOCheckerConfiguration_RewriteConfig"></SEOChecker:ResourceHeaderControl>
       <umbraco:Pane ID="EnableRewritingPane" runat="server">
            <umbraco:PropertyPanel ID="EnableRewritingPanel" runat="server">
                <asp:CheckBox ID="EnableRewritingCheckbox" runat="server" /></umbraco:PropertyPanel>
        </umbraco:Pane>
        <asp:PlaceHolder ID="RewriteOptionsVisible" runat="server">
            <umbraco:Pane ID="WWWPane" runat="server">
                <umbraco:PropertyPanel ID="WWWPanel" runat="server">
                    <asp:RadioButtonList ID="ForceWWWRadioList" RepeatDirection="Vertical" runat="server" /></umbraco:PropertyPanel>
            </umbraco:Pane>
            <umbraco:Pane ID="TrailingslashPane" runat="server">
                <umbraco:PropertyPanel ID="TrailingslashPanel" runat="server">
                    <asp:RadioButtonList ID="ForceTrailingslashRadioList" RepeatDirection="Vertical" runat="server" /></umbraco:PropertyPanel>
            </umbraco:Pane>
        </asp:PlaceHolder>
             <SEOChecker:ResourceHeaderControl runat="server" ID="RedirectResourceHeaderControl" ResourceKey="SEOCheckerConfiguration_Redirect"></SEOChecker:ResourceHeaderControl>
         <umbraco:Pane ID="StoreRedirectDomainPane" runat="server">
            <umbraco:PropertyPanel ID="StoreRedirectDomainPanel" runat="server">
                <asp:CheckBox ID="StoreRedirectDomainCheckBox" runat="server" /></umbraco:PropertyPanel>
        </umbraco:Pane>
        <umbraco:Pane ID="StoreRedirectQueryStringPane" runat="server">
            <umbraco:PropertyPanel ID="StoreRedirectQueryStringPanel" runat="server">
                <asp:CheckBox ID="StoreRedirectQueryCheckbox" runat="server" /></umbraco:PropertyPanel>
        </umbraco:Pane>
          <umbraco:Pane ID="ForwardRedirectQueryStringPane" runat="server">
            <umbraco:PropertyPanel ID="ForwardRedirectQueryStringPanel" runat="server">
                <asp:CheckBox ID="ForwardRedirectQueryCheckBox" runat="server" /></umbraco:PropertyPanel>
        </umbraco:Pane>
              <umbraco:Pane ID="RedirectWhenNodeExistsPane" runat="server">
            <umbraco:PropertyPanel ID="RedirectWhenNodeExistsPanel" runat="server">
                <asp:CheckBox ID="RedirectWhenNodeExistsCheckBox" runat="server" /></umbraco:PropertyPanel>
        </umbraco:Pane>

        <SEOChecker:ResourceHeaderControl runat="server" ID="NotFoundResourceHeaderControl" ResourceKey="SEOCheckerConfiguration_NotFound"></SEOChecker:ResourceHeaderControl>
                    <umbraco:Pane ID="NotFoundConfigPane" runat="server">
            <umbraco:PropertyPanel ID="NotFoundConfigPanel" runat="server">
                <asp:CheckBox ID="NotFoundConfigEnabledCheckBox" runat="server" /></umbraco:PropertyPanel>
            </umbraco:Pane>

        <SEOChecker:ResourceHeaderControl runat="server" ID="ValidationSResourceHeaderControl" ResourceKey="SEOCheckerConfiguration_Validation"></SEOChecker:ResourceHeaderControl>
        <umbraco:Pane ID="ValidationEmptyAltAttributePane" runat="server">
            <umbraco:PropertyPanel ID="ValidationEmptyAltAttributePanel" runat="server">
                <asp:CheckBox ID="ValidationEmptyAltCheckBox" runat="server" /></umbraco:PropertyPanel>
            </umbraco:Pane>
             <SEOChecker:ResourceHeaderControl runat="server" ID="GeneralResourceHeaderControl" ResourceKey="SEOCheckerConfiguration_General"></SEOChecker:ResourceHeaderControl>
        <umbraco:Pane ID="ToolPane" runat="server">
            <umbraco:PropertyPanel ID="KeywordSelectionToolPanel" runat="server">
                <asp:TextBox ID="KeywordSelectionToolTextBox" CssClass="umbEditorTextField" runat="server" /></umbraco:PropertyPanel>
        </umbraco:Pane>
        <umbraco:Pane ID="TemplateErrorsForNonAdminsPane" runat="server">
            <umbraco:PropertyPanel ID="TemplateErrorsForNonAdminsPanel" runat="server">
                <asp:CheckBox ID="TemplateErrorsForNonAdminsCheckBox" runat="server" /></umbraco:PropertyPanel>
        </umbraco:Pane>
            </asp:panel>
    </umbraco:UmbracoPanel>


</asp:Content>
