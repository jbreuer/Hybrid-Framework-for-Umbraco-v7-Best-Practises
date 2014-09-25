<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SEOCheckerInstaller.ascx.cs" Inherits="SEOChecker.Usercontrols.SEOCheckerInstaller" %>
<%@ Register Namespace="umbraco.uicontrols" Assembly="controls" TagPrefix="umb" %>
<umb:Pane ID="welcome" runat="server" Visible="true">

    <div class="dashboardWrapper">
        <h2>SEO Checker has been installed</h2>
        <img src="/umbraco/plugins/seochecker/images/content/seochecker.png" alt="SEO Checker" class="dashboardIcon" />
        <div class="dashboardColWrapper">
            <div class="dashboardCols">
                    <ul>
                        <li>Added the package to the plugin folder</li>
                        <li>Created the SEO Checker tables in the database</li>
                        <li>Added the SEO Checker datatype</li>
                        <li>Updated /umbraco/config/create/ui.xml</li>
                        <li>Added dashboard to SEO Checker application</li>
                        <li>Updated web.config to add HTTPModules</li>
                    </ul>
                    <p>
                        <button onclick="window.parent.location.href = '/umbraco/umbraco.aspx?app=seochecker'; return false;">Open SEO Checker</button>
                    </p>
            </div>
        </div>
    </div>
</umb:Pane>

