using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Extensions.Utilities;
using Umbraco.Web;
using Umbraco.Web.Routing;

namespace Umbraco.Extensions.UrlProvider
{
    public class NewsUrlProvider : BaseClass, IUrlProvider
    {
        public string GetUrl(UmbracoContext umbracoContext, int id, Uri current, UrlProviderMode mode)
        {
            var content = umbracoContext.ContentCache.GetById(id);
            if (content != null && content.DocumentTypeAlias == "Newsitem" && content.Parent != null)
            {
                var date = content.GetPropertyValue<DateTime>("currentDate");
                if(date != null)
                {
                    //This will add the selected date before the node name.
                    //For example /news/item1/ becomes /news/28-07-2014/item1/.
                    var url = content.Parent.Url;
                    if (!(url.EndsWith("/")))
                    {
                        url += "/";
                    }
                    return url + date.ToString("dd-MM-yyyy") + "/" + content.UrlName + "/";
                }
            }

            return null;
        }

        public IEnumerable<string> GetOtherUrls(UmbracoContext umbracoContext, int id, Uri current)
        {
            return Enumerable.Empty<string>();
        }
    }
}