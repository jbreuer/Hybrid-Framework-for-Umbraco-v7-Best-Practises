using DevTrends.MvcDonutCaching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Extensions.BLL;
using Umbraco.Extensions.Controllers.Base;
using Umbraco.Extensions.Models;
using Umbraco.Extensions.Models.Custom;
using Umbraco.Extensions.Models.Generated;
using Umbraco.Extensions.Utilities;
using Umbraco.Web;

namespace Umbraco.Extensions.Controllers
{
    public class NewsoverviewController : SurfaceRenderMvcController
    {
        [DonutOutputCache(CacheProfile = "OneDay")]
        public ActionResult Newsoverview()
        {
            var model = ModelLogic.CreateMasterModel() as MasterModel<Newsoverview>;

            var newsItems = NewsLogic.GetNewsitems(CurrentPage);
            var pager = Umbraco.GetPager(5, newsItems.Count());

            //Only put the paged items in the list.
            model.Content.NewsItems = newsItems.Skip((pager.CurrentPage - 1) * pager.ItemsPerPage).Take(pager.ItemsPerPage).ToList();
            model.Content.Pager = pager;

            return CurrentTemplate(model);
        }

        [ChildActionOnly]
        public ActionResult ShowDates()
        {
            var model = new Date()
            {
                Date1 = DateTime.Now,
                Date2 = DateTime.Now.AddDays(2)
            };

            return PartialView("ShowDates", model);
        }
    }
}