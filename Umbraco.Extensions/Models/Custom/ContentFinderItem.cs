using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;

namespace Umbraco.Extensions.Models.Custom
{
    public class ContentFinderItem
    {
        public string Template { get; set; }
        public IPublishedContent Content { get; set; }
    }
}