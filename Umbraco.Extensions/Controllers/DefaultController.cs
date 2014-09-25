using DevTrends.MvcDonutCaching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Extensions.BLL;
using Umbraco.Extensions.Controllers.Base;
using Umbraco.Extensions.Models;
using Umbraco.Web.Models;

namespace Umbraco.Extensions.Controllers
{
    public class DefaultController : SurfaceRenderMvcController
    {
        /// <summary>
        /// If the route hijacking doesn't find a controller this default controller will be used.
        /// That way a each page will always go through a controller and we can always have a MasterModel for the masterpage.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [DonutOutputCache(CacheProfile = "OneDay")]
        public override ActionResult Index(RenderModel model)
        {
            var masterModel = ModelLogic.CreateMasterModel();
            return CurrentTemplate(masterModel);
        }
    }
}