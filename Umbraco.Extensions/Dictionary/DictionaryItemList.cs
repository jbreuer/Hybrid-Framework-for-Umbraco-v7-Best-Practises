using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;

namespace Umbraco.Extensions.Dictionary
{
    public class DictionaryItemList : umbraco.presentation.settings.DictionaryItemList
    {
        public DictionaryItemList()
        {
            CurrentApp = Constants.Applications.Translation;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            lt_table.Text = lt_table.Text.Replace("../", "/umbraco/");
        }
    }
}