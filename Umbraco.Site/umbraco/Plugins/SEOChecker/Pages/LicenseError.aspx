<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LicenseError.aspx.cs" Inherits="SEOChecker.Pages.LicenseError" MasterPageFile="~/umbraco/masterpages/umbracoPage.Master" %>
<%@ Register Namespace="umbraco.uicontrols" Assembly="controls" TagPrefix="umb" %>
<%@ Register Src="../Usercontrols/SeoCheckerDashboard.ascx" TagPrefix="uc1" TagName="SeoCheckerDashboard" %>
<%@ Register TagPrefix="SEOChecker" Namespace="SEOChecker.Core.Controls" Assembly="SEOChecker.Core" %>

<asp:Content ID="header" ContentPlaceHolderID="head" runat="server">
     <SEOChecker:CssRenderControl runat="server"></SEOChecker:CssRenderControl>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <umb:umbracopanel id="UmbracoPanel" runat="server">
<uc1:SeoCheckerDashboard runat="server" ID="SeoCheckerDashboard" />
        </umb:umbracopanel>
</asp:Content>

