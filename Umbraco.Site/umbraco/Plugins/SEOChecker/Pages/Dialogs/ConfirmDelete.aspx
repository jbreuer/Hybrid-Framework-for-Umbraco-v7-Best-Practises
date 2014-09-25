<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/umbraco/masterpages/umbracoDialog.Master" CodeBehind="ConfirmDelete.aspx.cs" Inherits="SEOChecker.Pages.Dialogs.ConfirmDelete" %>
<%@ Register TagPrefix="SEOChecker" Namespace="SEOChecker.Core.Controls" Assembly="SEOChecker.Core" %>
<%@ Import Namespace="SEOChecker.Resources" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <SEOChecker:CssRenderControl ID="CssRenderControl" runat="server"></SEOChecker:CssRenderControl>
<script type="text/javascript">
    $(document).ready(function ($) {
        var processedField = '#<%=ProcessedIndicator.ClientID %>';
        var id = '<%=Id %>';
        var speechBubbleTitle = '<%=ResourceHelper.Instance.GetStringResource("SEOCheckerInboundLinkDeleteSpeechBubbleHeader") %>';
        var speechBubbleBody = '<%=ResourceHelper.Instance.GetStringResource("SEOCheckerInboundLinkDeleteSpeechBubbleBody") %>';
        if ($(processedField).val() == '1') {
            UmbClientMgr.contentFrame().focus();
            UmbClientMgr.closeModalWindow();
            UmbClientMgr.contentFrame().DeleteItem(id);
            UmbClientMgr.mainWindow().UmbSpeechBubble.ShowMessage('save', speechBubbleTitle, speechBubbleBody);
        }
    });
</script>
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
<asp:HiddenField ID="ProcessedIndicator" runat="server" Value="false" />
    <SEOChecker:DialogTitle ID="DialogTitle" runat="server"/> 
<div id="formDiv" style="visibility: visible;">
        <div class="propertyDiv">
        <p>
            <%:ResourceHelper.Instance.GetStringResource("Dialogs_ConfirmDeleteDialogConfirmation")%>
        </p>
        <p>
        <asp:CheckBox runat="server" ID="IgnoreNextTimeCheckbox" /> <%:ResourceHelper.Instance.GetStringResource("Dialogs_ConfirmDeleteDialogIgnoreNextTime")%>
        </p>
        </div>
        <div class="seoCheckerDialogButtons">
        <asp:Button ID="DeleteButton" runat="server" CssClass="guiInputButton btn btn-danger" OnClick="DeleteButton_Click" ></asp:Button> <em><%= umbraco.ui.Text("general","or") %></em> <a href="#" onclick="UmbClientMgr.closeModalWindow()" class="btn"><%=umbraco.ui.Text("general", "cancel", this.getUser())%></a>
         </div>       
      </div>
</asp:Content>