<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SeoCheckerDashboard.ascx.cs" Inherits="SEOChecker.Usercontrols.SeoCheckerDashboard" %>
<%@ Register Namespace="umbraco.uicontrols" Assembly="controls" TagPrefix="umb" %>
<style type="text/css">
    .tvList .tvitem {
        display: block;
        float: left;
        font-size: 11px;
        height: 250px;
        margin: 0 20px 20px 0;
        overflow: hidden;
        text-align: center;
        width: 230px;
    }

    .tvList p {
        margin-top:10px;
    }
</style>

<umb:Pane ID="LicenseErrorPane" runat="server" Visible="False">  
    <div class="dashboardWrapper">
    <h2>License error!</h2>
    <img src="/umbraco/plugins/seochecker/images/content/seochecker.png" alt="SEO Checker" class="dashboardIcon" />
    </div>
    <umb:Feedback ID="expressNotice" runat="server" Visible="false" type="error" />   
</umb:Pane>

<umb:Pane ID="RegisterPane" runat="server" Visible="false">
    <div class="dashboardWrapper">
        <h2>Thank you for trying SEO Checker!</h2>
        <img src="/umbraco/plugins/seochecker/images/content/seochecker.png" alt="SEO Checker" class="dashboardIcon" />

        <h3>To purchase a license</h3>
        <p>
            To purchase this product, simply go to the <a target="_blank" href="http://soetemansoftware.nl/">our website</a> and you're up and running in minutes!
        </p>

        <p>If you've already purchased a license, you can install it by downloading it from your <a href="http://soetemansoftware.nl/myprofile" target="_blank">profile</a> and upload it below, or add it manually to the /bin folder of the Umbraco installation.</p>
        <asp:PlaceHolder ID="StatusPlaceholder" runat="server" Visible="false">
            <p>
                <strong>
                    <asp:Literal ID="StatusLiteral" runat="server" /></strong>
            </p>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="FormPlaceholder" runat="server">
            <p>
                <strong>Select license file </strong>
                <asp:FileUpload ID="LicenseUpload" runat="server" />
            </p>
            <p>
                <asp:Button ID="UploadLicenseButton" runat="server" Text="Upload license" OnClick="UploadLicenseButton_Click" CssClass="btn btn-success" />
            </p>
        </asp:PlaceHolder>

    </div>
</umb:Pane>

<umb:Pane ID="welcome" runat="server" Visible="true">

    <div class="dashboardWrapper">
        <h2>Welcome to SEO Checker</h2>
        <img src="/umbraco/plugins/seochecker/images/content/seochecker.png" alt="SEO Checker" class="dashboardIcon" />


        <div class="dashboardColWrapper">
            <div class="dashboardCols">
                <div class="dashboardCol third">
                    <h3>Resources:</h3>
                    <ul>
                        <li><a href="http://soetemansoftware.nl/seo-checker/documentation" target="_blank">Documentation</a></li>

                        <li><a href="http://soetemansoftware.nl/seo-checker/downloads" target="_blank">SEO checker, updates</a></li>

                        <li><a href="http://soetemansoftware.nl/seo-checker/documentation" target="_blank">List of recent changes</a></li>

                        <li><a href="mailto:support@soetemansoftware.nl" target="_blank">Report an issue</a></li>
                    </ul>
                </div>

                <div class="dashboardCol third">
                    <h3>Forum:</h3>
                    <ul>
                        <li><a href="http://our.umbraco.org/projects/website-utilities/seo-checker/using-seochecker" target="_blank">Using SEO checker</a></li>

                        <li><a href="http://our.umbraco.org/projects/website-utilities/seo-checker/configuration" target="_blank">Configuration</a></li>

                        <li><a href="http://our.umbraco.org/projects/website-utilities/seo-checker/feature-request" target="_blank">Feature requests</a></li>

                        <li><a href="http://our.umbraco.org/projects/website-utilities/seo-checker/bugs" target="_blank">Bugs</a></li>
                    </ul>
                </div>

                <div class="dashboardCol third last">
                    <h3>Licensing and product info</h3>
                    <ul>
                        <li><a href="http://soetemansoftware.nl/seochecker" target="_blank">SEO checker on Soeteman software site</a></li>
                        <li><a href="http://our.umbraco.org/projects/website-utilities/seo-checker/" target="_blank">SEO checker on our.umbraco.org</a></li>
                    </ul>
                </div>
            </div>
        </div>

    </div>
</umb:Pane>


<umb:Pane ID="Learn" runat="server" Visible="true">

    <div class="dashboardWrapper">
        <h2>SEO Checker Video's</h2>
        <img src="/umbraco/plugins/seochecker/images/content/seochecker.png" alt="SEO Checker" class="dashboardIcon" />
        <div id="latestformvids">
            <div class="tvList">
                <div class="tvitem">
                    <a href="https://vimeo.com/54138902" target="_blank"><img src="https://secure-b.vimeocdn.com/ts/374/232/374232781_295.jpg" width="200"  height="127" border="0" />
                    <p>Use SEO Checker when editing content</p>
                    </a>
                </div>
                 <div class="tvitem">
                    <a href="https://vimeo.com/54137188" target="_blank"><img src="https://secure-b.vimeocdn.com/ts/374/226/374226666_295.jpg" width="200" height="127"  border="0" />
                    <p>Validate pages</p>
                    </a>
                </div>
                <div class="tvitem">
                    <a href="https://vimeo.com/54139839" target="_blank"><img src="https://secure-b.vimeocdn.com/ts/374/254/374254428_295.jpg" width="200" height="127"  border="0" />
                    <p>Avoid and fix broken links</p>
                    </a>
                </div>
                 <div class="tvitem">
                    <a href="https://vimeo.com/54142970" target="_blank"><img src="https://secure-b.vimeocdn.com/ts/374/257/374257666_295.jpg" width="200" height="127"  border="0" />
                    <p>Configure SEO Checker</p>
                    </a>
                </div>
            </div>
        </div>
    </div>
</umb:Pane>
