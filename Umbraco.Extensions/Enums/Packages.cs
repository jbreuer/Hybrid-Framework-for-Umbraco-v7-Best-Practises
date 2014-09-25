using nuPickers.Shared.EnumDataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Umbraco.Extensions.Enums
{
    public enum Packages
    {
        [EnumDataSource(Label = "Digibiz Advanced Media Picker")]
        Damp,

        [EnumDataSource(Label = "uComponents")]
        Ucomponents,

        [EnumDataSource(Label = "nuPickers")]
        NuPickers,

        [EnumDataSource(Label = "Archetype")]
        Archetype
    }
}