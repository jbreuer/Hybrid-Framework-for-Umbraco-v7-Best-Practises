<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Redirects.aspx.cs" Inherits="SEOChecker.Pages.Redirects.Redirects" MasterPageFile="~/umbraco/masterpages/umbracoPage.Master" %>

<%@ Register Assembly="controls" Namespace="umbraco.uicontrols" TagPrefix="umbraco" %>
<%@ Register TagPrefix="SEOChecker" Namespace="SEOChecker.Core.Controls" Assembly="SEOChecker.Core" %>
<%@ Import Namespace="SEOChecker.Resources" %>
<asp:Content ID="header" ContentPlaceHolderID="head" runat="server">
    <SEOChecker:CssRenderControl ID="CssRenderControl1" runat="server"></SEOChecker:CssRenderControl>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <umbraco:UmbracoPanel ID="UmbracoPanel" runat="server">
        <asp:Panel ID="PagePanel" runat="server">
            <asp:PlaceHolder ID="OverviewPlaceHolder" runat="server"></asp:PlaceHolder>
        </asp:Panel>
    </umbraco:UmbracoPanel>
</asp:Content>
