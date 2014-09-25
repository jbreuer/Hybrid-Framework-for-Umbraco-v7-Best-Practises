using nuPickers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Extensions.Models.Custom;
using Umbraco.Web;
using Zbu.ModelsBuilder;

namespace Umbraco.Extensions.Models.Generated
{
    public partial class Newsoverview
    {
        [ImplementPropertyType("widgets")]
        public IEnumerable<IPublishedContent> Widgets
        {
            get
            {
                var picker = this.GetPropertyValue<Picker>("widgets");
                return picker.AsPublishedContent();
            }
        }

        public IEnumerable<Newsitem> NewsItems { get; set; }
        public Pager Pager { get; set; }
    }
}