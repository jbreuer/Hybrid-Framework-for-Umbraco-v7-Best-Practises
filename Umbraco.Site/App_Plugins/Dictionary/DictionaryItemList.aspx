<%@ Page Language="C#" AutoEventWireup="true"
  Inherits="Umbraco.Extensions.Dictionary.DictionaryItemList" MasterPageFile="/umbraco/masterpages/umbracoPage.Master" %>

<%@ Register TagPrefix="cc1" Namespace="umbraco.uicontrols" Assembly="controls" %>


<asp:Content ContentPlaceHolderID="body" runat="server">

    <cc1:UmbracoPanel ID="Panel1" runat="server" Text="Dictionary overview" Width="408px" Height="264px">
    <cc1:Pane ID="pane1" runat="server">
      <table id="dictionaryItems" style="width: 100%;">
        <asp:Literal ID="lt_table" runat="server" />
      </table>
      </cc1:Pane>
    </cc1:UmbracoPanel>

</asp:Content>