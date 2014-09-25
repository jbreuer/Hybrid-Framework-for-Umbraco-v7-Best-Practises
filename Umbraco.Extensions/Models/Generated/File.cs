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
    [RenamePropertyType("umbracoBytes", "Bytes")]
    [RenamePropertyType("UmbracoExtension", "Extension")]
    [RenamePropertyType("umbracoFile", "Url")]
    public partial class File
    {
    }
}