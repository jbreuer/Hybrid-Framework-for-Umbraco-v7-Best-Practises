using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Core.Persistence;
using Umbraco.Extensions.Enums;
using Umbraco.Extensions.Models.Generated;
using Umbraco.Extensions.Models.Rdbms;
using Umbraco.Extensions.Utilities;
using Umbraco.Web;

namespace Umbraco.Extensions.BLL
{
    public class NewsLogic : BaseClass
    {
        /// <summary>
        /// Get the latest news item ordered by date.
        /// </summary>
        /// <returns></returns>
        public static Newsitem GetLatestNewsitem()
        {
            return
            (
                //Don't use CurrentPage.Website() because this method is also used in the backoffice for rendering widgets.
                from n in Umbraco.TypedContentAtRoot().DescendantsOrSelf<Newsitem>()
                where !n.HideOnNews
                orderby n.CurrentDate descending
                select n
            ).FirstOrDefault();
        }

        /// <summary>
        /// Get the news items ordered by date.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Newsitem> GetNewsitems(IPublishedContent content)
        {
            return
            (
                from n in content.Children<Newsitem>()
                where !n.HideOnNews
                orderby n.CurrentDate descending
                select n
            ).ToList();
        }
    }
}