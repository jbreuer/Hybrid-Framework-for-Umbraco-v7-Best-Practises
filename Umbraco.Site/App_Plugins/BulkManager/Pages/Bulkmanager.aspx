<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BulkManager.aspx.cs" MasterPageFile="/umbraco/masterpages/umbracoPage.Master" Inherits="BulkManager.UI.Pages.BulkManager" %>

<%@ Register Assembly="controls" Namespace="umbraco.uicontrols" TagPrefix="umbraco" %>
<%@ Register TagPrefix="BulkManager" Namespace="BulkManager.Core.Controls" Assembly="BulkManager.Core" %>
<asp:Content ID="header" ContentPlaceHolderID="head" runat="server">
    <BulkManager:CssRenderControl ID="CssRenderControl" runat="server"></BulkManager:CssRenderControl>
    <script type="text/javascript" src="../js/bulkmanager.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            setSelectedProvider('<%=BulkManagerProvider.Id%>');
            setQueryParametersField('<%=QueryParametersField.ClientID%>');
            intializeBulkManagerForm();
        });


    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <umbraco:UmbracoPanel ID="UmbracoPanel" runat="server" DefaultButton="SearchButton">
        <asp:Panel runat="server" ID="ContentPanel" Style="width:100%" >
            <BulkManager:BulkManagerHeaderPanel ID="BulkManagerHeaderPanel" runat="server"></BulkManager:BulkManagerHeaderPanel>
                <asp:PlaceHolder runat="server" ID="LicenseErrorPlaceholder">
            <umbraco:Feedback runat="server" ID="LicenseErrorFeedbackControl" type="error"></umbraco:Feedback>
        </asp:PlaceHolder>
            <asp:Panel runat="server" ID="OverviewPanel">
            <asp:PlaceHolder runat="server" ID="QueryPlaceHolder"></asp:PlaceHolder>
            <asp:HiddenField runat="server" ID="QueryParametersField" />
            <umbraco:Pane runat="server" ID="ButtonsPane">
                <umbraco:PropertyPanel runat="server" ID="ButtonsPanel">
                    <asp:Button runat="server" ID="SearchButton" CssClass="btn btn-success btn-Search" OnClick="SearchButton_OnClick" />
                </umbraco:PropertyPanel>
            </umbraco:Pane>
            <asp:Panel runat="server" ID="ResultPlaceHolder">
                <BulkManager:BulkActionsGridview runat="server" ID="ResultGridView">
                    <PagerTemplate></PagerTemplate>
                </BulkManager:BulkActionsGridview>

            </asp:Panel>
            <asp:Panel runat="server" ID="NoResultPlaceHolder" CssClass="overviewHeader" Visible="False">

                <BulkManager:ResourceControl runat="server" ID="NoResultResource" ResourceKey="BulkManagerOverviewNoResult"></BulkManager:ResourceControl>
            </asp:Panel>
                </asp:Panel>
             <asp:Panel runat="server" ID="SavePanel">
                 <asp:ValidationSummary runat="server" ID="SaveSummary" ValidationGroup="SaveState" CssClass="error"/>
        <umbraco:Pane runat="server" ID="NamePane">
            <umbraco:PropertyPanel runat="server" ID="NamePropertyPanel">
                
                <asp:TextBox runat="server" ID="SaveNameTextBox" CssClass="umbEditorTextField"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator runat="server" ID="SaveNameRequired" ControlToValidate="SaveNameTextBox" Text="*" ValidationGroup="SaveState"></asp:RequiredFieldValidator>
            </umbraco:PropertyPanel>
            </umbraco:Pane>
        <umbraco:Pane runat="server" ID="BindWhenLoadPane">
            <umbraco:PropertyPanel runat="server" ID="BindWhenLoadPanel">
                <asp:CheckBox runat="server" ID="BindWhenLoadCheckBox"/>
            </umbraco:PropertyPanel>
            </umbraco:Pane>
        <umbraco:Pane runat="server" ID="PersonalPane">
            <umbraco:PropertyPanel runat="server" ID="PersonalPanel">
                <asp:CheckBox runat="server" ID="PersonalCheckBox"/>
            </umbraco:PropertyPanel>
            </umbraco:Pane>
        </asp:Panel>
        </asp:Panel>
   
    
    </umbraco:UmbracoPanel>
</asp:Content>
