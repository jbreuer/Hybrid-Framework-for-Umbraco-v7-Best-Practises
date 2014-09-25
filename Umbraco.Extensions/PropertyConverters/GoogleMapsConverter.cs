using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;
using Umbraco.Extensions.Models.Custom;
using Umbraco.Extensions.Models.Generated;
using Umbraco.Extensions.Utilities;
using Umbraco.Web;
using Umbraco.Web.Models;

namespace Umbraco.Extensions.PropertyConverters
{
    public class GoogleMapsConverter : PropertyValueConverterBase, IPropertyValueConverterMeta
    {
        public override object ConvertSourceToObject(PublishedPropertyType propertyType, object source, bool preview)
        {
            if(source != null && !string.IsNullOrWhiteSpace(source.ToString()))
            {
                var coordinates = source.ToString().Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                if (coordinates.Length == 3)
                {
                    return new GoogleMaps
                    {
                        Lat = double.Parse(coordinates[0], CultureInfo.InvariantCulture),
                        Lng = double.Parse(coordinates[1], CultureInfo.InvariantCulture),
                        Zoom = int.Parse(coordinates[2], CultureInfo.InvariantCulture)
                    };
                }
            }

            return null;
        }

        public override bool IsConverter(PublishedPropertyType propertyType)
        {
            return "AngularGoogleMaps".InvariantEquals(propertyType.PropertyEditorAlias);
        }

        public PropertyCacheLevel GetPropertyCacheLevel(PublishedPropertyType propertyType, PropertyCacheValue cacheValue)
        {
            return PropertyCacheLevel.Content;
        }

        public Type GetPropertyValueType(PublishedPropertyType propertyType)
        {
            return typeof(GoogleMaps);
        }
    }
}