<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Notifications.aspx.cs" Inherits="SEOChecker.Pages.Notifications"   MasterPageFile="~/umbraco/masterpages/umbracoPage.Master"%>
<%@ Register Assembly="controls" Namespace="umbraco.uicontrols" TagPrefix="umbraco" %>
<%@ Register TagPrefix="SEOControls" TagName="ValidationPicker" Src="../Usercontrols/ValidationPicker.ascx" %>
<%@ Register TagPrefix="SEOControls" TagName="ScheduledTaskOptions" Src="../Usercontrols/ScheduledTaskOptions.ascx" %>
<%@ Register TagPrefix="SEOChecker" Namespace="SEOChecker.Core.Controls" Assembly="SEOChecker.Core" %>
<asp:Content ID="header" ContentPlaceHolderID="head" runat="server">
        <SEOChecker:CssRenderControl ID="CssRenderControl1" runat="server"></SEOChecker:CssRenderControl>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <umbraco:UmbracoPanel ID="UmbracoPanel" runat="server">
        <umbraco:Feedback runat="server" ID="ConfigureIssueFeedback" type="notice" Visible="False"></umbraco:Feedback>
        <asp:ValidationSummary runat="server" ID="valsum" CssClass="error"/>
        <asp:panel ID="PagePanel" runat="server">
        <SEOChecker:SEOCheckerPanel ID="SEOCheckerPanel" runat="server" />
        <umbraco:Pane runat="server" ID="EnableNotificationsPane"><umbraco:PropertyPanel runat="server" ID="EnableNotificationsPanel"><asp:CheckBox runat="server" ID="EnableNotificationsCheckBox" CausesValidation="False" AutoPostBack="True" OnCheckedChanged="EnableNotificationsCheckBox_OnCheckedChanged"/></umbraco:PropertyPanel> </umbraco:Pane>
        <SEOControls:ScheduledTaskOptions ID="ScheduledTaskOptionsControl" runat="server"/>
            </asp:panel>
    </umbraco:UmbracoPanel>
</asp:Content>