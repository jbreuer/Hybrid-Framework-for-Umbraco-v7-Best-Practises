using nuPickers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Extensions.Models.Custom;
using Umbraco.Web;
using Zbu.ModelsBuilder;

namespace Umbraco.Extensions.Models.Generated
{
    public partial class Vacancyoverview
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

        public VacancyoverviewContainer Container { get; set; }
    }
}