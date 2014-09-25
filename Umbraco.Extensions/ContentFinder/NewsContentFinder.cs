using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Extensions.Models.Custom;
using Umbraco.Extensions.Utilities;
using Umbraco.Web;
using Umbraco.Web.Routing;

namespace Umbraco.Extensions.ContentFinder
{
    public class NewsContentFinder : BaseClass, IContentFinder
    {
        public bool TryFindContent(PublishedContentRequest contentRequest)
        {
            try
            {
                if (contentRequest != null)
                {
                    //Get the current url.
                    var url = contentRequest.Uri.AbsoluteUri;

                    //Get the news nodes that are already cached.
                    var cachedNewsNodes = (Dictionary<string, ContentFinderItem>)HttpContext.Current.Cache["CachedNewsNodes"];
                    if (cachedNewsNodes != null)
                    {
                        //Check if the current url already has a news item.
                        if (cachedNewsNodes.ContainsKey(url))
                        {
                            //If the current url already has a node use that so the rest of the code doesn't need to run again.
                            var contentFinderItem = cachedNewsNodes[url];
                            contentRequest.PublishedContent = contentFinderItem.Content;
                            contentRequest.TrySetTemplate(contentFinderItem.Template);
                            return true;
                        }
                    }

                    //Split the url segments.
                    var path = contentRequest.Uri.GetAbsolutePathDecoded();
                    var parts = path.Split(new[] { '/' }, System.StringSplitOptions.RemoveEmptyEntries);

                    //The news items should contain 3 segments.
                    if (parts.Length == 3)
                    {
                        //Get all the root nodes.
                        var rootNodes = contentRequest.RoutingContext.UmbracoContext.ContentCache.GetAtRoot();

                        //Find the news item that matches the last segment in the url.
                        var newsItem = rootNodes.DescendantsOrSelf("Newsitem").Where(x => x.UrlName == parts.Last()).FirstOrDefault();
                        if(newsItem != null)
                        {
                            //Get the news item template.
                            var template = Services.FileService.GetTemplate(newsItem.TemplateId);

                            if (template != null)
                            {
                                //Store the fields in the ContentFinderItem-object.
                                var contentFinderItem = new ContentFinderItem()
                                {
                                    Template = template.Alias,
                                    Content = newsItem
                                };

                                //If the correct node is found display that node.
                                contentRequest.PublishedContent = contentFinderItem.Content;
                                contentRequest.TrySetTemplate(contentFinderItem.Template);

                                if (cachedNewsNodes != null)
                                {
                                    //Add the new ContentFinderItem-object to the cache.
                                    cachedNewsNodes.Add(url, contentFinderItem);
                                }
                                else
                                {
                                    //Create a new dictionary and store it in the cache.
                                    cachedNewsNodes = new Dictionary<string, ContentFinderItem>()
                                    {
                                        {
                                            url, contentFinderItem
                                        }
                                    };
                                    HttpContext.Current.Cache.Add("CachedNewsNodes",
                                          cachedNewsNodes,
                                          null,
                                          DateTime.Now.AddDays(1),
                                          System.Web.Caching.Cache.NoSlidingExpiration,
                                          System.Web.Caching.CacheItemPriority.High,
                                          null);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Umbraco.LogException<NewsContentFinder>(ex);
            }

            return contentRequest.PublishedContent != null;
        }
    }
}