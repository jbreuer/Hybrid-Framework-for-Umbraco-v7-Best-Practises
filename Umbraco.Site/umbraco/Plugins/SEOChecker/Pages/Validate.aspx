<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Validate.aspx.cs" Inherits="SEOChecker.Pages.Validate"     MasterPageFile="~/umbraco/masterpages/umbracoPage.Master"%>
<%@ Register Assembly="controls" Namespace="umbraco.uicontrols" TagPrefix="umbraco" %>
<%@ Register TagPrefix="SEOControls" TagName="ValidationPicker" Src="../Usercontrols/ValidationPicker.ascx" %>
<%@ Register TagPrefix="SEOChecker" Namespace="SEOChecker.Core.Controls" Assembly="SEOChecker.Core" %>
<%@ Import Namespace="SEOChecker.Resources" %>
<%@ Import Namespace="SEOChecker.Core.Repository.Queue" %>
<asp:Content ID="header" ContentPlaceHolderID="head" runat="server">
        <SEOChecker:CssRenderControl ID="CssRenderControl1" runat="server"></SEOChecker:CssRenderControl>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <umbraco:UmbracoPanel ID="UmbracoPanel" runat="server">
        <asp:panel ID="PagePanel" runat="server">
        <SEOChecker:SEOCheckerPanel ID="SEOCheckerPanel" runat="server" />
        <SEOControls:ValidationPicker ID="ValidationPickerControl" runat="server"/>
        <umbraco:Pane ID="StartPane" runat="server">
            <umbraco:PropertyPanel ID="StartPanel" runat="server">
                <asp:button ID="StartButton" runat="server" OnClick="StartButton_Click" CssClass="btn btn-success" />
            </umbraco:PropertyPanel>
            </umbraco:Pane>
            </asp:panel>
    </umbraco:UmbracoPanel>
</asp:Content>