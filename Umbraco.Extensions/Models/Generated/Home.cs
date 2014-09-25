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
    public partial class Home
    {
        public Newsitem LatestNewsItem { get; set; }

        [ImplementPropertyType("slider")]
        public IEnumerable<Slider> Slider
        {
            get
            {
                var archetypeModel = this.GetPropertyValue<ArchetypeModel>("slider");
                return archetypeModel.Select(x =>
                    {
                        return new Slider()
                        {
                            Title = x.GetValue<string>("title"),
                            Text = x.GetValue<IHtmlString>("text"),
                            Image = x.GetValue<Image>("image")
                        };
                    }
                ).ToList();
            }
        }
    }
}