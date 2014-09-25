<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocumentTypeSettings.aspx.cs"
    MasterPageFile="~/umbraco/masterpages/umbracoPage.Master" Inherits="SEOChecker.Pages.Settings.DocumentTypeSettings" %>

<%@ Register Assembly="controls" Namespace="umbraco.uicontrols" TagPrefix="umbraco" %>
<%@ Register TagPrefix="SEOChecker" Namespace="SEOChecker.Core.Controls" Assembly="SEOChecker.Core" %>
<asp:Content ID="header" ContentPlaceHolderID="head" runat="server">
        <SEOChecker:CssRenderControl ID="CssRenderControl1" runat="server"></SEOChecker:CssRenderControl>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <umbraco:UmbracoPanel ID="UmbracoPanel" runat="server">
        <umbraco:Feedback ID="SEOCheckerDatatypeFeedback" runat="server" type="notice" />
        <asp:panel ID="PagePanel" runat="server">
        <SEOChecker:SEOCheckerPanel ID="SEOCheckerPanel" runat="server" />
        <SEOChecker:ResourceHeaderControl runat="server" ID="TemplateResourceHeader" ResourceKey="SEOCheckerDoctypeSettingsTemplatePane"></SEOChecker:ResourceHeaderControl>
        <umbraco:Pane ID="TitlePane" runat="server">
            <umbraco:PropertyPanel ID="TitlePanel" runat="server">
                <SEOChecker:DualSelectBox ID="TitlePropertySelectBox" runat="server"></SEOChecker:DualSelectBox>
            </umbraco:PropertyPanel>
            </umbraco:Pane>
        <umbraco:Pane ID="DescriptionPane" runat="server">
            <umbraco:PropertyPanel ID="DescriptionPanel" runat="server">
                <SEOChecker:DualSelectBox ID="DescriptionPropertySelectBox" runat="server"></SEOChecker:DualSelectBox>
            </umbraco:PropertyPanel>
            </umbraco:Pane>
        <umbraco:Pane ID="TitleTemplatePane" runat="server">
            <umbraco:PropertyPanel ID="TitleTemplatePanel" runat="server">
                <asp:TextBox ID="TitleTemplateTextBox" CssClass="umbEditorTextField" runat="server"> </asp:TextBox>
            </umbraco:PropertyPanel>
        </umbraco:Pane>
            <SEOChecker:ResourceHeaderControl runat="server" ID="RobotsResourceHeaderControl" ResourceKey="SEOCheckerDoctypeSettingsRobotSettingsPane"></SEOChecker:ResourceHeaderControl>
        <umbraco:Pane ID="RobotSettingsPane" runat="server">
            <umbraco:PropertyPanel ID="RobotIndexProperty" runat="server">
                <asp:DropDownList ID="RobotIndexDropdown" runat="server" CssClass="SEOCheckerDropdown">
                </asp:DropDownList>
            </umbraco:PropertyPanel>
            <umbraco:PropertyPanel ID="RobotFollowProperty" runat="server">
                <asp:DropDownList ID="RobotFollowDropdown" CssClass="SEOCheckerDropdown" runat="server">
                </asp:DropDownList>
            </umbraco:PropertyPanel>
        </umbraco:Pane>
        <asp:PlaceHolder runat="server" ID="XmlSiteMapPlaceholder">
        <SEOChecker:ResourceHeaderControl runat="server" ID="XmlSitemapResourceHeaderControl" ResourceKey="SEOCheckerDoctypeSettingsXmlSitemapSettingsPane"></SEOChecker:ResourceHeaderControl>
        <umbraco:Pane ID="ExcludeXmlSitemapPane" runat="server">
            <umbraco:PropertyPanel ID="ExcludeXmlSitemapProperty" runat="server">
                <asp:CheckBox ID="ExcludeXmlSitemapCheckBox" runat="server" />
            </umbraco:PropertyPanel>
            <umbraco:PropertyPanel ID="SitemapPrioProperty" runat="server">
                <asp:DropDownList ID="SitemapPrioDropdown" CssClass="SEOCheckerDropdown" runat="server" />
            </umbraco:PropertyPanel>
             <umbraco:PropertyPanel ID="ChangeFreqPanel" runat="server">
                <asp:DropDownList ID="ChangeFreqDropdownlist" CssClass="SEOCheckerDropdown" runat="server" />
            </umbraco:PropertyPanel>
        </umbraco:Pane>
            </asp:PlaceHolder> 
            </asp:panel>
    </umbraco:UmbracoPanel>
</asp:Content>
