using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Persistence;
using Umbraco.Core.Services;
using Umbraco.Web;

namespace Umbraco.Extensions.Utilities
{
    public class BaseClass
    {
        protected static Database Database
        {
            get { return ApplicationContext.Current.DatabaseContext.Database; }
        }

        protected static ServiceContext Services
        {
            get { return ApplicationContext.Current.Services; }
        }

        protected static IPublishedContent CurrentPage
        {
            get { return UmbracoContext.Current.PublishedContentRequest.PublishedContent; }
        }

        protected static UmbracoHelper Umbraco
        {
            get { return new UmbracoHelper(UmbracoContext.Current); }
        }
    }
}