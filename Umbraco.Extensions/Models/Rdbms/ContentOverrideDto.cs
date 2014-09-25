using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace Umbraco.Extensions.Models.Rdbms
{
    [TableName("hfContentOverride")]
    [PrimaryKey("id")]
    [ExplicitColumns]
    public class ContentOverrideDto
    {
        [Column("id")]
        [PrimaryKeyColumn()]
        public int Id { get; set; }

        [Column("contentTypeAlias")]
        public string ContentTypeAlias { get; set; }

        [Column("propertyTypeAlias")]
        public string PropertyTypeAlias { get; set; }

        [Column("archetypeAlias")]
        public string ArchetypeAlias { get; set; }

        [Column("nodeId")]
        public int? NodeId { get; set; }
        
        [Column("type")]
        public string Type { get; set; }

        [Column("configAlias")]
        public string ConfigAlias { get; set; }

        [Column("value")]
        public string Value { get; set; }
    }
}