using DevTrends.MvcDonutCaching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Extensions.BLL;
using Umbraco.Extensions.Controllers.Base;
using Umbraco.Extensions.Enums;
using Umbraco.Extensions.Models;
using Umbraco.Extensions.Models.Custom;
using Umbraco.Extensions.Models.Form;
using Umbraco.Extensions.Models.Generated;
using Umbraco.Extensions.Utilities;
using Umbraco.Web;

namespace Umbraco.Extensions.Controllers
{
    public class HomeController : SurfaceRenderMvcController
    {
        [DonutOutputCache(CacheProfile = "OneDay")]
        public ActionResult Home()
        {
            var model = ModelLogic.CreateMasterModel() as MasterModel<Home>;

            model.Content.LatestNewsItem = NewsLogic.GetLatestNewsitem();

            return CurrentTemplate(model);
        }
    }
}