<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditRobots.aspx.cs" MasterPageFile="~/umbraco/masterpages/umbracoPage.Master" Inherits="SEOChecker.Pages.Settings.EditRobots" %>
<%@ Import Namespace="SEOChecker.Core.Helpers" %>
<%@ Register Assembly="controls" Namespace="umbraco.uicontrols" TagPrefix="umbraco" %>
<%@ Register TagPrefix="SEOChecker" Namespace="SEOChecker.Core.Controls" Assembly="SEOChecker.Core" %>
<asp:Content ID="header" ContentPlaceHolderID="head" runat="server">
     <SEOChecker:CssRenderControl ID="CssRenderControl1" runat="server"></SEOChecker:CssRenderControl>
     <script type="text/javascript">
         $(document).ready(function () {
             $(".CommandDropdown").change(function () {
                 var command = $('.CommandDropdown').val();
                 if (command.toLowerCase() == 'sitemap:') {
                     var sitemap = '<%=RobotsTxtHelper.GetDefaultSitemapEntry()%>';
                     $('.CommandTextBox').val(sitemap);
                 }
             });

             $(".CommandButton").click(function () {
                 var elementId = '<%= RobotsTextBox.ClientID %>';
                 var command = $('.CommandDropdown').val();
                 var commandText = $('.CommandTextBox').val();
                 if (TextSelected()) {
                     Insert(command, commandText, elementId);
                 } else {
                     Append(command, commandText, elementId);
                 }
                 return false;
             });
         });

         function Append(command, commandText, id) {
             var code = UmbEditor.GetCode();
             code += '\n' + command + ' ' + commandText;
             UmbEditor.SetCode(code);
         }

         function Insert(command, commandText,id) {
             var code = command + ' ' + commandText + '\n';
             UmbEditor.Insert(code, "", id);
         }

         function TextSelected() {
             UmbEditor._editor.focus();
             return UmbEditor._editor.getCursor().line > 0 || UmbEditor._editor.getCursor().ch > 0;
         }

    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <umbraco:UmbracoPanel ID="UmbracoPanel" runat="server">
         <asp:ValidationSummary runat="server" ID="valsum" CssClass="error"/>
        <asp:panel ID="PagePanel" runat="server">
        <SEOChecker:SEOCheckerPanel ID="SEOCheckerPanel" runat="server" />
        
       <umbraco:Pane runat="server" ID="RobotCommandPane">
            <umbraco:PropertyPanel runat="server" ID="RobotsCommandPanel" Text="Insert">
                <asp:DropDownList runat="server" ID="CommandDropDown" CssClass="CommandDropdown"/> 
            </umbraco:PropertyPanel>
            <umbraco:PropertyPanel runat="server" ID="RobotsValuePanel" Text="Value">
                <asp:TextBox runat="server" ID="CommandTextBox" CssClass="CommandTextBox"></asp:TextBox> <asp:LinkButton runat="server" ID="InsertButton" CssClass="CommandButton">Insert</asp:LinkButton>
            </umbraco:PropertyPanel>
        </umbraco:Pane>
        <umbraco:Pane runat="server" ID="RobotsPane">
         <umbraco:PropertyPanel ID="RobotsPanel" runat="server" Text=""><umbraco:CodeArea ID="RobotsTextBox" CssClass="RobotsTextBox"  AutoResize="true" OffSetX="47" OffSetY="55" CodeBase="Python" runat="server" /></umbraco:PropertyPanel>
            <asp:CustomValidator runat="server" ID="RobotsValidator" CssClass="hidden" OnServerValidate="RobotsValidator_OnServerValidate" ValidateEmptyText="True" ControlToValidate="CommandTextBox"></asp:CustomValidator>
        </umbraco:Pane>
        </asp:panel>
    </umbraco:UmbracoPanel>
</asp:Content>