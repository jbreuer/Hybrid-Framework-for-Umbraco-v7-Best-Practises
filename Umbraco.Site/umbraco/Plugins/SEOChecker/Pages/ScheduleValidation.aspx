<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScheduleValidation.aspx.cs" Inherits="SEOChecker.Pages.ScheduleValidation"  MasterPageFile="~/umbraco/masterpages/umbracoPage.Master" %>
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
             <SEOChecker:ResourceHeaderControl runat="server" ResourceKey="ScheduledTask_ValidationOptions"></SEOChecker:ResourceHeaderControl>
        <SEOControls:ValidationPicker ID="ValidationPickerControl" runat="server"/>
             <SEOChecker:ResourceHeaderControl ID="ResourceHeaderControl1" runat="server" ResourceKey="ScheduledTask_ScheduleOptions "></SEOChecker:ResourceHeaderControl>
        <umbraco:Pane ID="Pane1" runat="server">
            <umbraco:PropertyPanel runat="server" ID="ScheduledTaskNamePanel"><asp:TextBox runat="server" ID="ScheduledTaskNameTextBox" CssClass="umbEditorTextField"></asp:TextBox><asp:RequiredFieldValidator runat="server" ID="ScheduledTaskNameValidator" ControlToValidate="ScheduledTaskNameTextBox" Text="*" CssClass="fieldError"></asp:RequiredFieldValidator></umbraco:PropertyPanel>
        </umbraco:Pane>
        <umbraco:Pane ID="Pane2" runat="server">
            <umbraco:PropertyPanel runat="server" ID="ScheduledTaskNotifyEmailPanel"><asp:TextBox runat="server" ID="ScheduledTaskNotifyEmailTextBox" CssClass="umbEditorTextField"></asp:TextBox><asp:RequiredFieldValidator runat="server" ID="ScheduledTaskNotifyEmailValidator" ControlToValidate="ScheduledTaskNotifyEmailTextBox" Text="*" CssClass="fieldError"></asp:RequiredFieldValidator><asp:CustomValidator runat="server" ID="EmailValidator" ControlToValidate="ScheduledTaskNotifyEmailTextBox" Text="*" OnServerValidate="EmailValidator_OnServerValidate" CssClass="fieldError"></asp:CustomValidator></umbraco:PropertyPanel>
        </umbraco:Pane>
        <SEOControls:ScheduledTaskOptions ID="ScheduledTaskOptionsControl" runat="server"/>
            </asp:panel>
    </umbraco:UmbracoPanel>
</asp:Content>
