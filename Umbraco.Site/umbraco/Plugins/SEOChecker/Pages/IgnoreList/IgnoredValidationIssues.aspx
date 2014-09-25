<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IgnoredValidationIssues.aspx.cs"  MasterPageFile="~/umbraco/masterpages/umbracoPage.Master" Inherits="SEOChecker.Pages.IgnoreList.IgnoredValidationIssues" %>
<%@ Register Assembly="controls" Namespace="umbraco.uicontrols" TagPrefix="umbraco" %>
<%@ Import Namespace="SEOChecker.Resources" %>
<%@ Register Assembly="controls" Namespace="umbraco.uicontrols" TagPrefix="umbraco" %>
<%@ Register TagPrefix="SEOChecker" Namespace="SEOChecker.Core.Controls" Assembly="SEOChecker.Core" %>
<asp:Content ID="header" ContentPlaceHolderID="head" runat="server">
    <SEOChecker:CssRenderControl ID="CssRenderControl1" runat="server"></SEOChecker:CssRenderControl>
    <script type="text/javascript" src="../../scripts/seochecker.js"></script>
    <script type="text/javascript">
        function DeleteItem(id) {
            $('.ActionPanel' + id).fadeOut(250);
        }

        function OpenDeleteDialog(id) {
            UmbClientMgr.openModalWindow('plugins/SEOChecker/pages/dialogs/confirmdeleteignorelist.aspx?type=validationerror&id=' + id, 'Delete', true, 400, 300);
        }

        function OpenDocumentDialog(id) {
            UmbClientMgr.openModalWindow('/umbraco/editContent.aspx?id=' + id, 'Edit document', true, 900, 800);
        }
        function OpenTemplateDialog(id) {
            UmbClientMgr.openModalWindow('/umbraco/settings/editTemplate.aspx?templateID=' + id, 'Edit template', true, 900, 800);
        }

    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <umbraco:UmbracoPanel ID="UmbracoPanel" runat="server">
        <asp:panel ID="PagePanel" runat="server">
        <SEOChecker:SEOCheckerPanel ID="SEOCheckerPanel" runat="server" />
          <asp:PlaceHolder ID="NoIssuesPlaceHolder" runat="server">
              <SEOChecker:ResourceMessageControl ID="ResourceHeaderControl1" runat="server" ResourceKey="SEOCheckerIgnoredValidationIssues_NoResult"></SEOChecker:ResourceMessageControl>
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
                                        <%#RenderTitle((SEOChecker.Core.Repository.ValidationIssues.ValidationErrorView)Container.DataItem)%>
                                    </div>
                                    <div class="propertyItemContent">
                                        <%#Eval("ErrorDescription") %>
                                    </div>
                                    <div class="SEOCheckerOverviewButtons">
                                    <SEOChecker:DeleteButton ID="DeleteButton" runat="server" IssueId='<%#Eval("IssueId") %>' />
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
