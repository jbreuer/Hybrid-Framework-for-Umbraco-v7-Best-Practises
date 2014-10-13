using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Persistence;
using Umbraco.Extensions.Enums;
using Umbraco.Extensions.Models.Rdbms;
using Umbraco.Extensions.Utilities;

namespace Umbraco.Extensions.BLL
{
    public class ContentOverrideLogic : BaseClass
    {
        #region Get

        /// <summary>
        /// Check if there is a content override for this content type and property type.
        /// </summary>
        /// <param name="contentTypeAlias"></param>
        /// <param name="propertyTypeAlias"></param>
        /// <param name="archetypeAlias"></param>
        /// <param name="nodeId"></param>
        /// <param name="type"></param>
        /// <param name="configAlias"></param>
        /// <returns></returns>
        public static bool HasContentOverride(
            string contentTypeAlias,
            string propertyTypeAlias,
            string archetypeAlias = null,
            int? nodeId = null,
            ContentOverrideType type = ContentOverrideType.Config,
            string configAlias = null
            )
        {
            var typeValue = type.ToString();

            var sql = new Sql();
            sql.Select("COUNT(*)")
                .From<ContentOverrideDto>()
                .Where<ContentOverrideDto>(x => x.ContentTypeAlias == contentTypeAlias)
                .Where<ContentOverrideDto>(x => x.PropertyTypeAlias == propertyTypeAlias)
                .Where<ContentOverrideDto>(x => x.Type == typeValue);

            if (!string.IsNullOrWhiteSpace(archetypeAlias))
            {
                sql.Where<ContentOverrideDto>(x => x.ArchetypeAlias == archetypeAlias);
            }

            if (nodeId.HasValue)
            {
                sql.Where<ContentOverrideDto>(x => x.NodeId == nodeId.Value);
            }

            if (type == ContentOverrideType.Config && !string.IsNullOrWhiteSpace(configAlias))
            {
                sql.Where<ContentOverrideDto>(x => x.ConfigAlias == configAlias);
            }

            return Database.ExecuteScalar<int>(sql) > 0;
        }

        /// <summary>
        /// Return the content override object for this content type and property type.
        /// </summary>
        /// <param name="contentTypeAlias"></param>
        /// <param name="propertyTypeAlias"></param>
        /// <param name="type"></param>
        /// <param name="configAlias"></param>
        /// <returns></returns>
        public static ContentOverrideDto GetContentOverride(
            string contentTypeAlias,
            string propertyTypeAlias,
            string archetypeAlias = null,
            int? nodeId = null,
            ContentOverrideType type = ContentOverrideType.Config,
            string configAlias = null
            )
        {
            var typeValue = type.ToString();

            var sql = new Sql();
            sql.Select("*")
                .From<ContentOverrideDto>()
                .Where<ContentOverrideDto>(x => x.ContentTypeAlias == contentTypeAlias)
                .Where<ContentOverrideDto>(x => x.PropertyTypeAlias == propertyTypeAlias)
                .Where<ContentOverrideDto>(x => x.Type == typeValue);

            if (!string.IsNullOrWhiteSpace(archetypeAlias))
            {
                sql.Where<ContentOverrideDto>(x => x.ArchetypeAlias == archetypeAlias);
            }

            if (nodeId.HasValue)
            {
                sql.Where<ContentOverrideDto>(x => x.NodeId == nodeId.Value);
            }
            
            if (type == ContentOverrideType.Config && !string.IsNullOrWhiteSpace(configAlias))
            {
                sql.Where<ContentOverrideDto>(x => x.ConfigAlias == configAlias);
            }

            return Database.SingleOrDefault<ContentOverrideDto>(sql); 
        }

        /// <summary>
        /// Return all the content override values for this content type.
        /// </summary>
        /// <param name="contentTypeAlias"></param>
        /// <param name="type"></param>
        /// <param name="configAlias"></param>
        /// <returns></returns>
        public static List<ContentOverrideDto> GetContentOverrides(
            string contentTypeAlias,
            ContentOverrideType type = ContentOverrideType.Config,
            string configAlias = null
            )
        {
            var typeValue = type.ToString();

            var sql = new Sql();
            sql.Select("*")
                .From<ContentOverrideDto>()
                .Where<ContentOverrideDto>(x => x.ContentTypeAlias == contentTypeAlias)
                .Where<ContentOverrideDto>(x => x.Type == typeValue);

            if (type == ContentOverrideType.Config && !string.IsNullOrWhiteSpace(configAlias))
            {
                sql.Where<ContentOverrideDto>(x => x.ConfigAlias == configAlias);
            }

            return Database.Fetch<ContentOverrideDto>(sql);
        }

        #endregion

        #region Insert

        /// <summary>
        /// Create the content override.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="contentTypeAlias"></param>
        /// <param name="propertyTypeAlias"></param>
        /// <param name="archetypeAlias"></param>
        /// <param name="nodeId"></param>
        /// <param name="type"></param>
        /// <param name="configAlias"></param>
        /// <returns></returns>
        public static ContentOverrideDto CreateContentOverride(
            string value,
            string contentTypeAlias,
            string propertyTypeAlias,
            string archetypeAlias = null,
            int? nodeId = null,
            ContentOverrideType type = ContentOverrideType.Config,
            string configAlias = null
            )
        {
            var contentOverride = new ContentOverrideDto()
            {
                Value = value,
                ContentTypeAlias = contentTypeAlias,
                PropertyTypeAlias = propertyTypeAlias,
                ArchetypeAlias = archetypeAlias,
                NodeId = nodeId,
                Type = type.ToString(),
                ConfigAlias = configAlias
            };

            //Insert the ContentOverride into the database.
            Database.Insert(contentOverride);

            return contentOverride;
        }

        #endregion
    }
}
