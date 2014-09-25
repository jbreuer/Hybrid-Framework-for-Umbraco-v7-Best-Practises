using DevTrends.MvcDonutCaching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core;
using Umbraco.Extensions.BLL;
using Umbraco.Extensions.Controllers.Base;
using Umbraco.Extensions.Enums;
using Umbraco.Extensions.Models;
using Umbraco.Extensions.Models.Custom;
using Umbraco.Extensions.Models.Generated;
using Umbraco.Extensions.Utilities;
using Umbraco.Web;

namespace Umbraco.Extensions.Controllers
{
    public class VacancyoverviewController : SurfaceRenderMvcController
    {
        [DonutOutputCache(CacheProfile = "OneDay")]
        public ActionResult Vacancyoverview()
        {
            var model = ModelLogic.CreateMasterModel() as MasterModel<Vacancyoverview>;
            
            model.Content.Container = GetContainer();

            return CurrentTemplate(model);
        }

        /// <summary>
        /// Used for ajax paging.
        /// </summary>
        /// <returns></returns>
        [DonutOutputCache(CacheProfile = "OneDay")]
        public ActionResult AjaxVacancyoverview()
        {
            //We don't use CreateMasterModel because we dont need the base model properties.
            //Only the vacancy overview part is reloaded.
            var model = GetContainer();

            //This page is loaded with jquery and sometimes it loads old (cached) data. This prevents that.
            Umbraco.DisableCache();

            return CurrentTemplate(model);
        }

        public VacancyoverviewContainer GetContainer()
        {
            var vacancyItems = GetVacancyitems();
            var pager = Umbraco.GetPager(2, vacancyItems.Count());

            return new VacancyoverviewContainer()
            {
                //Only put the paged items in the list.
                VacancyItems = vacancyItems.Skip((pager.CurrentPage - 1) * pager.ItemsPerPage).Take(pager.ItemsPerPage).ToList(),
                Pager = pager
            };
        }

        /// <summary>
        /// Get the vacancy items ordered by sort order and filtered by level.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Vacancyitem> GetVacancyitems()
        {
            //If the level is in the querystring try to convert it to the enum.
            VacancyLevel? level = null;
            if (!string.IsNullOrEmpty(Request.QueryString["level"]))
            {
                var attemptVacancyLevel = Request.QueryString["level"].TryConvertTo<VacancyLevel>();
                level = attemptVacancyLevel.Success ? attemptVacancyLevel.Result : (VacancyLevel?)null;
            }

            //Get the vacancy item children of the current page.
            var vacancyItems = CurrentPage.Children<Vacancyitem>();

            if(level.HasValue)
            {
                //Filter on the level if the enum is available.
                vacancyItems = vacancyItems.Where(x => x.VacancyLevel == level.Value);
            }

            //Return the vacancy items ordered by sort order.
            return vacancyItems.OrderBy(x => x.SortOrder);
        }
    }
}