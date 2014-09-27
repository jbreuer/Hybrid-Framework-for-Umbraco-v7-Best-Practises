using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Extensions.Models;
using Umbraco.Extensions.Models.Custom;
using Umbraco.Extensions.Models.Generated;
using Umbraco.Extensions.Utilities;
using Umbraco.Web;

namespace Umbraco.Extensions.BLL
{
    public class ModelLogic : BaseClass
    {
        #region MasterModel

        public static IMasterModel CreateMasterModel()
        {
            var content = UmbracoContext.Current.PublishedContentRequest.PublishedContent;
            return CreateMasterModel(content);
        }

        public static IMasterModel CreateMasterModel(IPublishedContent content)
        {
            var baseContent = content as Base;
            var model = CreateMagicModel(typeof(MasterModel<>), content) as IMasterModel;
            model.MenuItems = GetMenuItems();
            model.SeoTitle = baseContent.Seo.Title;
            model.SeoDescription = baseContent.Seo.Description;
            model.Twitter = content.Website().Twitter;
            model.Facebook = content.Website().Facebook;
            return model;
        }

        private static object CreateMagicModel(Type genericType, IPublishedContent content)
        {
            var contentType = content.GetType();
            var modelType = genericType.MakeGenericType(contentType);
            var model = Activator.CreateInstance(modelType, content);
            return model;
        }

        private static IEnumerable<MenuItem> GetMenuItems()
        {
            return
            (
                from n in CurrentPage.Website().Children<Menu>()
                where !n.HideInMenu
                select new MenuItem()
                {
                    Id = n.Id,
                    Title = n.MenuTitle,
                    Url = n.Url,
                    ActiveClass = CurrentPage.Path.Contains(n.Id.ToString()) ? "active" : null,
                    MenuIcon = n.MenuIcon
                }
            ).ToList();
        }

        #endregion
    }
}