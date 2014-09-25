using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Extensions.Utilities;
using Umbraco.Core.Models;
using Umbraco.Web;
using Zbu.ModelsBuilder;

namespace Umbraco.Extensions.Models.Generated
{
    public partial class Newsitem
    {
        [ImplementPropertyType("attachment")]
        public File Attachment
        {
            get
            {
                var file = this.GetPropertyValue<IPublishedContent>("attachment") as File;
                return file;
            }
        }
    }
}