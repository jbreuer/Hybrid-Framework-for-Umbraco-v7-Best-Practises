<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ScheduledTaskOptions.ascx.cs" Inherits="SEOChecker.Usercontrols.ScheduledTaskOptions" %>
<%@ Register TagPrefix="umbraco" Namespace="umbraco.uicontrols" Assembly="controls" %>
<%@ Register TagPrefix="SEOChecker" Namespace="SEOChecker.Core.Controls" Assembly="SEOChecker.Core" %>
<umbraco:Pane ID="ExecuteEveryPane" runat="server">
            <umbraco:PropertyPanel ID="ExecuteEveryPanel" runat="server" Text="Execute every">
                <asp:RadioButtonList ID="ExecuteEveryRadioList" runat="server" AutoPostBack="true" CausesValidation="false">
                </asp:RadioButtonList>
            </umbraco:PropertyPanel>
        </umbraco:Pane>
        <umbraco:Pane ID="DaysPane" runat="server">
            <umbraco:PropertyPanel ID="DaysProperties" runat="server" >
                <asp:CustomValidator ID="DaysValidator" runat="server"  Text="*" OnServerValidate="DaysValidator_Validate"  Display="Dynamic" CssClass="fieldError"/>
                <asp:checkboxList ID="DaysCheckboxList" runat="server">
                </asp:checkboxList>
            </umbraco:PropertyPanel>
            
        </umbraco:Pane>
        <umbraco:Pane ID="TimePane" runat="server">
            
            <umbraco:PropertyPanel ID="TimePanel" runat="server" Text="Time">
            <asp:DropDownList ID="HourDropdown" runat="server"  /> : <asp:DropDownList ID="MinuteDropdown" runat="server" />
            </umbraco:PropertyPanel>
            
        </umbraco:Pane>
<asp:PlaceHolder runat="server" ID="ScheduledInfoPlaceholder">
    <h2><SEOChecker:ResourceTextControl ID="ResourceTextControl1" runat="server" ResourceKey="Schedule_Info"/></h2>
    <umbraco:Pane runat="server" ID="NextRunPane">
        
        <umbraco:PropertyPanel runat="server" ID="NextRunPropertyPanel"><asp:Literal runat="server" ID="NextRunLiteral"/></umbraco:PropertyPanel>
    </umbraco:Pane>
    <umbraco:Pane runat="server" ID="LastRunPane">
        <umbraco:PropertyPanel runat="server" ID="LastRunPropertyPanel"><asp:Literal runat="server" ID="LastrunLiteral"/></umbraco:PropertyPanel>
    </umbraco:Pane>
</asp:PlaceHolder>