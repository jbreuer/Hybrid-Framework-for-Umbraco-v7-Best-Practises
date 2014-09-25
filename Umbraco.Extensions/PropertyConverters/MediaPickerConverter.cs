using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;
using Umbraco.Extensions.Models.Generated;
using Umbraco.Extensions.Utilities;
using Umbraco.Web;

namespace Umbraco.Extensions.PropertyConverters
{
    public class MediaPickerConverter : PropertyValueConverterBase, IPropertyValueConverterMeta
    {
        public override object ConvertSourceToObject(PublishedPropertyType propertyType, object source, bool preview)
        {
            if (source == null)
            {
                return null;
            }

            int id = int.TryParse(source.ToString(), out id) ? id : -1;

            if (!IsConverterDefault(propertyType))
            {
                var image = id < 0 ? null : UmbracoContext.Current.MediaCache.GetById(id) as Image;
                if (image != null)
                {
                    return image.AsValid();
                }
            }

            var media = id < 0 ? null : UmbracoContext.Current.MediaCache.GetById(id);
            return media;
        }

        public override bool IsConverter(PublishedPropertyType propertyType)
        {
            return Constants.PropertyEditors.MediaPickerAlias.InvariantEquals(propertyType.PropertyEditorAlias);
        }

        public PropertyCacheLevel GetPropertyCacheLevel(PublishedPropertyType propertyType, PropertyCacheValue cacheValue)
        {
            return PropertyCacheLevel.Content;
        }

        public Type GetPropertyValueType(PublishedPropertyType propertyType)
        {
            return !IsConverterDefault(propertyType) ? typeof(Image) : typeof(IPublishedContent);
        }

        /// <summary>
        /// Almost all media picker properties are images so here we exclude the properties that aren't.
        /// </summary>
        /// <param name="propertyType"></param>
        /// <returns></returns>
        public static bool IsConverterDefault(PublishedPropertyType propertyType)
        {
            return false;
        }
    }
}