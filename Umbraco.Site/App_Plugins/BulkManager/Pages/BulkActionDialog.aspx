<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BulkActionDialog.aspx.cs" MasterPageFile="/umbraco/masterpages/umbracoDialog.Master" Inherits="BulkManager.UI.Pages.BulkActionDialog" %>

<%@ Register TagPrefix="umbraco" Namespace="umbraco.uicontrols" Assembly="controls" %>
<%@ Register TagPrefix="BulkManager" Namespace="BulkManager.Core.Controls" Assembly="BulkManager.Core" %>
<asp:Content ID="header" ContentPlaceHolderID="head" runat="server">
    <BulkManager:CssRenderControl ID="CssRenderControl" runat="server"></BulkManager:CssRenderControl>
    <script type="text/javascript" src="../js/bulkmanager.js"></script>
     <script type="text/javascript">
         $(document).ready(function () {
             setSelectedIds('<%=SelectedIdFields.ClientID%>');
         });
         

    </script>
   <asp:PlaceHolder runat="server" ID="ExecuteJSPlaceholder">
       <script type="text/javascript">
           $(document).ready(function () {
               executeBulk('<%=BulkActionIdField.Value%>');
           });
    </script>
   </asp:PlaceHolder>
    
    
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="body">
    <asp:HiddenField runat="server" ID="SelectedIdFields" />
    <asp:HiddenField runat="server" ID="BulkActionIdField" />
    <umbraco:Pane runat="server" ID="BulkActionPane">
        <umbraco:PropertyPanel runat="server" ID="BulkActionPanel">
            <asp:PlaceHolder runat="server" ID="FormPlaceHolder">
                <p>
                    <asp:PlaceHolder runat="server" ID="BulkActionPlaceHolder"></asp:PlaceHolder>
                </p>
                <p>
                    <asp:Button runat="server" ID="ExecuteButton" CssClass="btn btn-success BulkActionExecuteButton" Text="Execute" OnClick="ExecuteButton_OnClick" />&nbsp;<em>Or</em>&nbsp;
                <asp:LinkButton runat="server" ID="CancelButton" CssClass="btn bulkActionCancelButton" Text="Cancel" />
                </p>
            </asp:PlaceHolder>
            <asp:PlaceHolder runat="server" ID="ExecutingPlaceHolder" >
                 <div class="centerDiv">
                            <p><span id="dialogExecutingMessage"><BulkManager:ResourceControl runat="server" ID="ExecutingResource" ResourceKey="BulkManagerActionInitializing"></BulkManager:ResourceControl></span></p>
                            <p class="progressBar"><asp:Image runat="server" ID="ProgressIndicatorImage"/></p>
                                <p class="bulkActionFinished hidden">
                                    <asp:LinkButton runat="server" ID="CloseDialogButton"  CssClass="btn bulkActionCloseButton"></asp:LinkButton>
                                </p>
                             </div>
            </asp:PlaceHolder>
        </umbraco:PropertyPanel>
    </umbraco:Pane>

</asp:Content>
