using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Strings;
using Umbraco.Extensions.Enums;
using Umbraco.Extensions.Utilities;
using Umbraco.Web;
using Umbraco.Web.Routing;

namespace Umbraco.Extensions.UrlProvider
{
    public class VacancyItemUrlSegmentProvider : BaseClass, IUrlSegmentProvider
    {
        public string GetUrlSegment(IContentBase content, System.Globalization.CultureInfo culture)
        {
            return GetUrlSegment(content);
        }

        public string GetUrlSegment(IContentBase content)
        {
            if (content != null)
            {
                var contentType = Services.ContentTypeService.GetContentType(content.ContentTypeId);
                if (contentType != null && contentType.Alias == "Vacancyitem")
                {
                    //This will add the level before the node name.
                    //For example /developer/ becomes /junior-developer/
                    var level = content.GetValue<VacancyLevel>("level");
                    return level.ToString().ToUrlSegment() + "-" + content.Name.ToUrlSegment();
                }
            }

            return null;
        }
    }
}