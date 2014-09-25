using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web;

namespace Umbraco.Extensions.Actions
{
    public class BaseActionFilterAttribute : ActionFilterAttribute
    {
        public BaseActionFilterAttribute()
        {
            this.Order = 0;
        }
        public BaseActionFilterAttribute(int order)
        {
            this.Order = order;
        }

        protected static UmbracoHelper Umbraco
        {
            get
            {
                return new UmbracoHelper(UmbracoContext.Current);
            }
        }
    }
}