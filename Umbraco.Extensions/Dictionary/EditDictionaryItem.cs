using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;

namespace Umbraco.Extensions.Dictionary
{
    public class EditDictionaryItem : umbraco.settings.EditDictionaryItem
    {
        public EditDictionaryItem()
        {
            CurrentApp = Constants.Applications.Translation;
        }
    }
}