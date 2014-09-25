<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE xsl:stylesheet [
  <!ENTITY nbsp "&#x00A0;">
]>
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
        Hi,
        <p>This is an automated mail to inform you that the scheduled validation task <strong><xsl:value-of select="seoCheckerScheduledValidationResult/name" /></strong> executed on <xsl:value-of select="seoCheckerScheduledValidationResult/date"/>.<br/>
        <xsl:choose>
          <xsl:when test="seoCheckerScheduledValidationResult/validateChildren = 'true'">Document <strong><xsl:value-of select="seoCheckerScheduledValidationResult/rootNodeName" /></strong> and children are added to the validation queue.</xsl:when>
          <xsl:otherwise>Document '<strong><xsl:value-of select="seoCheckerScheduledValidationResult/rootNodeName" /></strong>' is added to the validation queue.
          </xsl:otherwise>
        </xsl:choose>
        </p>
        <xsl:if test="seoCheckerScheduledValidationResult/validationQueueCount &gt; 0">
          <p>
            The validation queue contained  <strong><xsl:value-of select="seoCheckerScheduledValidationResult/validationQueueCount" /></strong> item(s) at the moment of sending this email.
          </p>
        </xsl:if>
        <p>Thanks,</p>
        <p>SEO Checker bot</p>
      </body>
    </html>

  </xsl:template>

</xsl:stylesheet>