using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Extensions.Models.Custom;
using Umbraco.Web;
using Umbraco.Web.Models;

namespace Umbraco.Extensions.Models
{
    public interface IMasterModel
    {
        IEnumerable<MenuItem> MenuItems { get; set; }
        string SeoTitle { get; set; }
        string SeoDescription { get; set; }
        string Twitter { get; set; }
        string Facebook { get; set; }
    }

    public class MasterModel<T> : RenderModel<T>, IMasterModel
        where T : class, IPublishedContent
    {
        public MasterModel(T content) : base(content) { }

        public IEnumerable<MenuItem> MenuItems { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string Twitter { get; set; }
        public string Facebook { get; set; }
    }
}