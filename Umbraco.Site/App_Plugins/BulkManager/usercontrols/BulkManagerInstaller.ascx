<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BulkManagerInstaller.ascx.cs" Inherits="BulkManager.UI.Usercontrols.BulkManagerInstaller" %>
<%@ Register Namespace="umbraco.uicontrols" Assembly="controls" TagPrefix="umb" %>
<%@ Register TagPrefix="bulkmanager" Namespace="BulkManager.Core.Controls" Assembly="BulkManager.Core" %>
<div class="dashboardWrapper">
        <bulkmanager:DashboardHeaderControl Id="DashboardHeader" runat="server" Text="Bulkmanager is installed" LegacyImage="~/app_plugins/bulkmanager/images/bulkmanagerdashboard.png" Image="~/app_plugins/bulkmanager/images/bulkmanagerdashboard-belle.png"/>
        <div class="dashboardColWrapper">
            <div class="dashboardCols">
                <p>The installer did the following:</p>
                    <ul>
                        <li>Added the package to the app_plugins folder</li>
                        <li>Created the Bulkmanager table in the database</li>
                        <li>Updated /umbraco/config/create/ui.xml</li>
                        <li>Added the bulkmanager tree to the developer section</li>
                    </ul>
                <p>&nbsp;</p>
                <p>By default Bulkmanager is added to the developer section. Using the dropdownlist below you can change the default location </p>
                <p><asp:DropDownList runat="server" ID="SectionList"/>&nbsp;<asp:Button runat="server" ID="ChangeSectionButton" Text="Change" OnClick="ChangeSectionButton_Click" CssClass="btn btn-success"/></p>
            </div>
        </div>
    </div>
                
 

     
    
