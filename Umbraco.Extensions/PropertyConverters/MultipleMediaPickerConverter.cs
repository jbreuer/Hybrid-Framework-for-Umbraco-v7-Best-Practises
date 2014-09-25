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
    public class MultipleMediaPickerConverter : PropertyValueConverterBase, IPropertyValueConverterMeta
    {
        public override object ConvertSourceToObject(PublishedPropertyType propertyType, object source, bool preview)
        {
            if (source == null)
            {
                return null;
            }

            var nodeIds =
                source.ToString()
                    .Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x =>
                        {
                            int id = int.TryParse(x, out id) ? id : -1;
                            return id;
                        })
                    .ToList();

            if (IsMultipleDataType(propertyType.DataTypeId))
            {
                if (!IsConverterDefault(propertyType))
                {
                    return nodeIds.Select(x =>
                    {
                        var image = x < 0 ? null : UmbracoContext.Current.MediaCache.GetById(x) as Image;
                        if (image != null)
                        {
                            return image.AsValid();
                        }
                        return null;
                    }).Where(x => x != null).ToList();
                }

                return nodeIds.Select(x =>
                {
                    var media = x < 0 ? null : UmbracoContext.Current.MediaCache.GetById(x);
                    return media;
                }).Where(x => x != null).ToList();
            }

            if (nodeIds.Any())
            {
                var id = nodeIds.First();
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

            return null;
        }

        public override bool IsConverter(PublishedPropertyType propertyType)
        {
            return Constants.PropertyEditors.MultipleMediaPickerAlias.InvariantEquals(propertyType.PropertyEditorAlias);
        }

        public PropertyCacheLevel GetPropertyCacheLevel(PublishedPropertyType propertyType, PropertyCacheValue cacheValue)
        {
            return PropertyCacheLevel.Content;
        }

        public Type GetPropertyValueType(PublishedPropertyType propertyType)
        {
            if (IsMultipleDataType(propertyType.DataTypeId))
            {
                return !IsConverterDefault(propertyType) ? typeof(IEnumerable<Image>) : typeof(IEnumerable<IPublishedContent>);
            }
            return !IsConverterDefault(propertyType) ? typeof(Image) : typeof(IPublishedContent);
        }

        /// <summary>
        /// Almost all multiple media picker properties are images so here we exclude the properties that aren't.
        /// </summary>
        /// <param name="propertyType"></param>
        /// <returns></returns>
        public static bool IsConverterDefault(PublishedPropertyType propertyType)
        {
            return
                (propertyType.PropertyTypeAlias == "files1" && propertyType.ContentType.Alias == "Content")
                ||
                (propertyType.PropertyTypeAlias == "file1" && propertyType.ContentType.Alias == "Content")
                ||
                (propertyType.PropertyTypeAlias == "attachment" && propertyType.ContentType.Alias == "Newsitem");
        }

        /// <summary>
        /// Check if the multi picker checkbox is checked.
        /// </summary>
        /// <param name="dataTypeId"></param>
        /// <returns></returns>
        private static bool IsMultipleDataType(int dataTypeId)
        {
            var dts = ApplicationContext.Current.Services.DataTypeService;
            var multiPickerPreValue =
                dts.GetPreValuesCollectionByDataTypeId(dataTypeId)
                    .PreValuesAsDictionary.FirstOrDefault(
                        x => x.Key.InvariantEquals("multiPicker")).Value;

            return multiPickerPreValue != null && multiPickerPreValue.Value.TryConvertTo<bool>().Result;
        }
    }
}