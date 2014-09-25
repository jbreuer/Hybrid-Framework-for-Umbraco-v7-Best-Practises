using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Extensions.Models.Generated;

namespace Umbraco.Extensions.Models.Custom
{
    public class Slider
    {
        public string Title { get; set; }
        public IHtmlString Text { get; set; }
        public Image Image { get; set; }
    }
}