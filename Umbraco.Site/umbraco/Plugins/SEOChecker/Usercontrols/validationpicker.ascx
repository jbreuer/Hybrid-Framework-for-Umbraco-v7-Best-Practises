<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ValidationPicker.ascx.cs" Inherits="SEOChecker.Usercontrols.ValidationPicker" %>
<%@ Register TagPrefix="umbraco" Namespace="umbraco.uicontrols" Assembly="controls" %>
<%@ Register TagPrefix="SEOChecker" Namespace="SEOChecker.Core.Controls" Assembly="SEOChecker.Core" %>
<umbraco:Pane ID="StartLocationPane" runat="server">
            <umbraco:PropertyPanel ID="StartLocationPanel" runat="server">
                <asp:CustomValidator runat="server" ID="LocationValidator" CssClass="fieldError" Display="Dynamic" ControlToValidate="StartLocationPicker" Text="*" ValidateEmptyText="True" OnServerValidate="LocationValidator_OnServerValidate"></asp:CustomValidator>
                <SEOChecker:ContentPicker id="StartLocationPicker" runat="server" /> 
            </umbraco:PropertyPanel>
        </umbraco:Pane>
        <umbraco:Pane ID="IncludeChildrenPane" runat="server" >
            <umbraco:PropertyPanel ID="IncludeChildrenPanel" runat="server">
                <asp:CheckBox ID="IncludeChildrenCheckbox" runat="server" checked="true"/>
            </umbraco:PropertyPanel>
        </umbraco:Pane>