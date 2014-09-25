using Archetype.PropertyConverters;
using Archetype.Models;
using Archetype.Extensions;
using nuPickers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Extensions.Enums;
using Umbraco.Extensions.Utilities;
using Umbraco.Web;
using Zbu.ModelsBuilder;
using Umbraco.Extensions.Models.Custom;

namespace Umbraco.Extensions.Models.Generated
{
    public partial class Vacancyitem
    {
        [ImplementPropertyType("level")]
        public VacancyLevel VacancyLevel
        {
            get
            {
                var picker = this.GetPropertyValue<Picker>("level");
                return picker.AsEnums<VacancyLevel>().FirstOrDefault();
            }
        }
    }
}