<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE xsl:stylesheet [ <!ENTITY nbsp "&#x00A0;"> ]>
<xsl:stylesheet 
	version="1.0" 
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 
	xmlns:msxml="urn:schemas-microsoft-com:xslt"
	xmlns:umbraco.library="urn:umbraco.library" xmlns:Exslt.ExsltCommon="urn:Exslt.ExsltCommon" xmlns:Exslt.ExsltDatesAndTimes="urn:Exslt.ExsltDatesAndTimes" xmlns:Exslt.ExsltMath="urn:Exslt.ExsltMath" xmlns:Exslt.ExsltRegularExpressions="urn:Exslt.ExsltRegularExpressions" xmlns:Exslt.ExsltStrings="urn:Exslt.ExsltStrings" xmlns:Exslt.ExsltSets="urn:Exslt.ExsltSets" xmlns:seoChecker.MetaData="urn:seoChecker.MetaData" 
	exclude-result-prefixes="msxml umbraco.library Exslt.ExsltCommon Exslt.ExsltDatesAndTimes Exslt.ExsltMath Exslt.ExsltRegularExpressions Exslt.ExsltStrings Exslt.ExsltSets seoChecker.MetaData ">


<xsl:output method="xml" omit-xml-declaration="yes"/>

<xsl:template match="/">

  <html>
    <body style="font-family:Verdana;font-size:12px;">
      Hi <xsl:value-of select="seoCheckerNotificationResult/user"/>,
      <p>On <xsl:value-of select="seoCheckerNotificationResult/date"/> SEO Checker found some issues on your site. Please login to your Umbraco installation and fix the issues. Open issues:</p>
      <table>
        <tr>
          <td style="font-family:Verdana;font-size:12px;">Validation issues</td>
          <td style="font-family:Verdana;font-size:12px;">
            <xsl:value-of select="seoCheckerNotificationResult/validationIssuesCount" />
          </td>
        </tr>
        <tr>
          <td style="font-family:Verdana;font-size:12px;">Configuration issues</td>
          <td style="font-family:Verdana;font-size:12px;">
            <xsl:value-of select="seoCheckerNotificationResult/configurationIssuesCount" />
          </td>
        </tr>
        <tr>
          <td style="font-family:Verdana;font-size:12px;">Inbound link issues</td>
          <td style="font-family:Verdana;font-size:12px;">
            <xsl:value-of select="seoCheckerNotificationResult/inboundLinkIssuesCount" />
          </td>
        </tr>
      </table>
      <xsl:if test="seoCheckerNotificationResult/validationQueueCount &gt; 0">
        <p>
          The validation queue contained  <xsl:value-of select="seoCheckerNotificationResult/validationQueueCount" /> item(s) at the moment of sending this email.</p>
      </xsl:if>
      <p>Thanks,</p>
      <p>SEO Checker bot</p>
    </body>
  </html>


</xsl:template>

</xsl:stylesheet>