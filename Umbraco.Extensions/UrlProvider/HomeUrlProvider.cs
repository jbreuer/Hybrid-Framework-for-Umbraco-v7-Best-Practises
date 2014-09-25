using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Extensions.Utilities;
using Umbraco.Web;
using Umbraco.Web.Routing;

namespace Umbraco.Extensions.UrlProvider
{
    public class HomeUrlProvider : BaseClass, IUrlProvider
    {
        public string GetUrl(UmbracoContext umbracoContext, int id, Uri current, UrlProviderMode mode)
        {
            var content = umbracoContext.ContentCache.GetById(id);
            if (content != null && content.DocumentTypeAlias == "Home" && content.Parent != null)
            {
                //The home node will have / instead of /home/.
                return content.Parent.Url;
            }

            return null;
        }

        public IEnumerable<string> GetOtherUrls(UmbracoContext umbracoContext, int id, Uri current)
        {
            return Enumerable.Empty<string>();
        }
    }
}