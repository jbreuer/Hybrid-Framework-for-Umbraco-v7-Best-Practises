<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BulkAction.aspx.cs" MasterPageFile="~/umbraco/masterpages/umbracoDialog.Master" Inherits="SEOChecker.Pages.Dialogs.BulkAction" %>
<%@ Register TagPrefix="SEOChecker" Namespace="SEOChecker.Core.Controls" Assembly="SEOChecker.Core" %>

<%@ Import Namespace="SEOChecker.Resources" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <SEOChecker:CssRenderControl ID="CssRenderControl" runat="server"></SEOChecker:CssRenderControl>
    <script type="text/javascript">
        var bulkActionId = '<%=GetActionId() %>';
        function bulkactionExecuted() {
            var speechBubbleTitle = '<%=ResourceHelper.Instance.GetStringResource("SpeechBubble_Header") %>';
            var speechBubbleBody = '<%=GetExecutedMessage() %>';

            UmbClientMgr.contentFrame().focus();
            UmbClientMgr.closeModalWindow();
            UmbClientMgr.contentFrame().ReloadPage();
            UmbClientMgr.mainWindow().UmbSpeechBubble.ShowMessage('save', speechBubbleTitle, speechBubbleBody);
        }
    </script>
        <asp:PlaceHolder runat="server" ID="ProcessedMessage" Visible="False">
        <script type="text/javascript">
            $(document).ready(function ($) {
                bulkactionExecuted();
            });
        
        </script>
    </asp:PlaceHolder>
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <SEOChecker:DialogTitle ID="DialogTitle" runat="server"/> 
    <asp:panel ID="PagePanel" runat="server">
    <asp:PlaceHolder runat="server" ID="ActionPlaceHolder"></asp:PlaceHolder>
        </asp:panel>
</asp:Content>
