<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreateRedirectControl.ascx.cs" Inherits="SEOChecker.Usercontrols.CreateRedirectControl" %>
<asp:RequiredFieldValidator id="UrlValidator" Text="*" ControlToValidate="UrlTextBox" runat="server" Display="Dynamic"></asp:RequiredFieldValidator>
<div style="MARGIN-TOP: 20px"><asp:Literal runat="server" ID="UrlLiteral"></asp:Literal>:<br/>

<asp:TextBox id="UrlTextBox" CssClass="bigInput" Runat="server" width="350px"></asp:TextBox>
    
<!-- added to support missing postback on enter in IE -->
<asp:TextBox runat="server" style="visibility:hidden;display:none;" ID="Textbox1"/>

<div style="padding-top: 25px;">
	<asp:Button id="CreateButton" Runat="server" style="Width:90px" onclick="CreateButton_Click"></asp:Button>
	&nbsp; <em><%= umbraco.ui.Text("or") %></em> &nbsp;
  <a href="#" style="color: blue"  onclick="UmbClientMgr.closeModalWindow()"><%=umbraco.ui.Text("cancel")%></a>
</div>