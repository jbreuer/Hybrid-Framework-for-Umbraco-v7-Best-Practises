<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IgnoredConfigurationIssues.aspx.cs" Inherits="SEOChecker.Pages.IgnoreList.IgnoredConfigurationIssues" MasterPageFile="~/umbraco/masterpages/umbracoPage.Master" %>

<%@ Register Assembly="controls" Namespace="umbraco.uicontrols" TagPrefix="umbraco" %>
<%@ Register TagPrefix="SEOChecker" Namespace="SEOChecker.Core.Controls" Assembly="SEOChecker.Core" %>
<%@ Import Namespace="SEOChecker.Resources" %>
<asp:Content ID="header" ContentPlaceHolderID="head" runat="server">
    <SEOChecker:CssRenderControl runat="server"></SEOChecker:CssRenderControl>
    <script type="text/javascript" src="../../scripts/seochecker.js"></script>
    <script type="text/javascript">
        function DeleteItem(id) {
            $('.ActionPanel' + id).fadeOut(250);
        }

        function OpenDeleteDialog(id) {
            UmbClientMgr.openModalWindow('plugins/SEOChecker/pages/dialogs/confirmdeleteignorelist.aspx?type=configuration&id=' + id, 'Delete', true, 400, 300);
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <umbraco:UmbracoPanel ID="UmbracoPanel" runat="server">
        <asp:panel ID="PagePanel" runat="server">
        <SEOChecker:SEOCheckerPanel ID="SEOCheckerPanel" runat="server" />

        <asp:PlaceHolder ID="NoIssuesPlaceHolder" runat="server">
            <SEOChecker:ResourceMessageControl runat="server" ResourceKey="Overview_IgnoredConfigurationIssues_NoResult"></SEOChecker:ResourceMessageControl>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="OverviewPlaceHolder" runat="server">
            <asp:Repeater ID="OverviewRepeater" runat="server">
                <ItemTemplate>
                     <%# RenderCategory(Eval("Category").ToString())%>
                    <asp:Panel ID="NotFoundPanel" runat="server" CssClass='<%#string.Format("ActionPanel{0}",Eval("IssueId")) %>'>
                        <umbraco:Pane ID="NotFoundPane" runat="server">
                            <div class="propertyItem">
                                 <div class="propertyItemheader seoItemHeader">
                                     <div class="bulkItemSelectChecboxContainer"><input type="checkbox" value='<%#Eval("IssueId") %>' class="bulkItemSelectCheckbox"/></div>
                                    <%#Eval("Error") %>
                                </div>
                                <div class="SEOCheckerOverviewButtons">
                                    <SEOChecker:DeleteButton ID="DeleteButton1" runat="server" IssueId='<%#Eval("IssueId") %>' />
                                </div>
                            </div>
                        </umbraco:Pane>
                    </asp:Panel>
                </ItemTemplate>
            </asp:Repeater>
            <asp:PlaceHolder runat="server" id="PagingPlaceHolder"/>
        </asp:PlaceHolder>
            </asp:panel>
    </umbraco:UmbracoPanel>
</asp:Content>
