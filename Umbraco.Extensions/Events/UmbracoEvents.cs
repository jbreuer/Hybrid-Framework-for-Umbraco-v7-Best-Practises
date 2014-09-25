using DevTrends.MvcDonutCaching;
using Examine;
using Our.Umbraco.PropertyConverters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using Umbraco.Core;
using Umbraco.Core.Events;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Umbraco.Core.PropertyEditors;
using Umbraco.Core.Publishing;
using Umbraco.Core.Services;
using Umbraco.Core.Strings;
using Umbraco.Extensions.BLL;
using Umbraco.Extensions.ContentFinder;
using Umbraco.Extensions.Controllers;
using Umbraco.Extensions.Models;
using Umbraco.Extensions.UrlProvider;
using Umbraco.Extensions.Utilities;
using Umbraco.Web;
using Umbraco.Web.Mvc;
using Umbraco.Web.Routing;

namespace Umbraco.Extensions.Events
{
    public class UmbracoEvents : ApplicationEventHandler
    {
        private static ServiceContext Services
        {
            get { return UmbracoContext.Current.Application.Services; }
        }

        protected static UmbracoHelper Umbraco
        {
            get { return new UmbracoHelper(UmbracoContext.Current); }
        }

        protected override void ApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            //Events
            ContentService.Created += Content_New;
            ContentService.Saving += ContentService_Saving;
            ContentService.Saved += ContentService_Saved;
            ContentService.Published += Content_Published;
            ContentService.UnPublished += Content_Unpublished;
            ContentService.Moved += Content_Moved;
            ContentService.Trashed += Content_Trashed;
            ContentService.Deleted += Content_Deleted;
            MediaService.Saved += Media_Saved;

            //By registering this here we can make sure that if route hijacking doesn't find a controller it will use this controller.
            //That way each page will always be routed through one of our controllers.
            DefaultRenderMvcControllerResolver.Current.SetDefaultControllerType(typeof(DefaultController));

            //Remove the media picker property converters from the Umbraco Core Property Value Converters package.
            //These will be replaced by custom converters.
            PropertyValueConvertersResolver.Current.RemoveType<MediaPickerPropertyConverter>();
            PropertyValueConvertersResolver.Current.RemoveType<MultipleMediaPickerPropertyConverter>();

            //Add a web api handler. Here we can change the values from each web api call.
            GlobalConfiguration.Configuration.MessageHandlers.Add(new WebApiHandler());

            //With the url providers we can change node urls.
            UrlProviderResolver.Current.InsertTypeBefore<DefaultUrlProvider, HomeUrlProvider>();
            UrlProviderResolver.Current.InsertTypeBefore<DefaultUrlProvider, NewsUrlProvider>();

            //With the url segment provider we can change a segment of the url. No content finder is required for this because the node still matches the tree structure.
            UrlSegmentProviderResolver.Current.InsertTypeBefore<DefaultUrlSegmentProvider, VacancyItemUrlSegmentProvider>();

            //With the content finder we can match nodes to urls.
            ContentFinderResolver.Current.InsertTypeBefore<ContentFinderByNotFoundHandlers, NewsContentFinder>();
        }

        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            ExamineManager.Instance.IndexProviderCollection["ExternalIndexer"].GatheringNodeData += OnGatheringNodeData;
            PreRenderViewActionFilterAttribute.ActionExecuted += PreRenderViewActionFilterAttribute_ActionExecuted;
        }

        #region Events

        private void Content_New(IContentService sender, NewEventArgs<IContent> e)
        {
            //Set current date.
            if (e.Entity.HasProperty("currentDate"))
            {
                e.Entity.SetValue("currentDate", DateTime.Today);
            }
        }

        protected void ContentService_Saving(IContentService sender, SaveEventArgs<IContent> e)
        {
            foreach (var entity in e.SavedEntities)
            {
                //Set node name if the properties are empty. We can't do this in the Content_New event because we don't have the node name yet.
                if (entity.HasProperty("menuTitle") && string.IsNullOrWhiteSpace(entity.GetValue<string>("menuTitle")))
                {
                    entity.SetValue("menuTitle", entity.Name);
                }
                if (entity.HasProperty("title") && string.IsNullOrWhiteSpace(entity.GetValue<string>("title")))
                {
                    entity.SetValue("title", entity.Name);
                }
                if (entity.HasProperty("name") && string.IsNullOrWhiteSpace(entity.GetValue<string>("name")))
                {
                    entity.SetValue("name", entity.Name);
                }
            }
        }

        private void ContentService_Saved(IContentService sender, SaveEventArgs<IContent> e)
        {
            foreach (var entity in e.SavedEntities)
            {
                try
                {
                    if (entity.ContentType.Alias == "Gallery")
                    {
                        //Check if a media folder is already linked to the gallery.
                        bool hasContentOverride = ContentOverrideLogic.HasContentOverride(
                            "Gallery",
                            "images",
                            nodeId: entity.Id,
                            configAlias: "startNodeId"
                        );

                        if (!hasContentOverride)
                        {
                            //Create a media folder for the gallery.
                            var mediaFolder = Services.MediaService.CreateMedia(entity.Name, -1, "Folder");
                            Services.MediaService.Save(mediaFolder);

                            //Set the media folder as the start node for the Gallery images media picker.
                            ContentOverrideLogic.CreateContentOverride(
                                mediaFolder.Id.ToString(),
                                "Gallery",
                                "images",
                                nodeId: entity.Id,
                                configAlias: "startNodeId"
                            );
                        }
                    }
                }
                catch(Exception ex)
                {
                    Umbraco.LogException<UmbracoEvents>(ex);
                }
            }

            ClearCache();
        }

        private void Content_Published(IPublishingStrategy sender, PublishEventArgs<IContent> e)
        {
            ClearCache();
        }

        private void Content_Deleted(IContentService sender, DeleteEventArgs<IContent> e)
        {
            ClearCache();
        }

        private void Content_Trashed(IContentService sender, MoveEventArgs<IContent> e)
        {
            ClearCache();
        }

        private void Content_Moved(IContentService sender, MoveEventArgs<IContent> e)
        {
            ClearCache();
        }

        private void Content_Unpublished(IPublishingStrategy sender, PublishEventArgs<IContent> e)
        {
            ClearCache();
        }

        private void Media_Saved(IMediaService sender, SaveEventArgs<IMedia> e)
        {
            ClearCache();
        }

        private void OnGatheringNodeData(object sender, IndexingNodeDataEventArgs e)
        {
            // Create searchable path
            if (e.Fields.ContainsKey("path"))
            {
                e.Fields["searchPath"] = e.Fields["path"].Replace(',', ' ');
            }

            // Lowercase all the fields for case insensitive searching
            var keys = e.Fields.Keys.ToList();
            foreach (var key in keys)
            {
                e.Fields[key] = HttpUtility.HtmlDecode(e.Fields[key].ToLower(CultureInfo.InvariantCulture));
            }

            // Extract the filename from media items
            if (e.Fields.ContainsKey("umbracoFile"))
            {
                e.Fields["umbracoFileName"] = Path.GetFileName(e.Fields["umbracoFile"]);
            }

            // Stuff all the fields into a single field for easier searching
            var combinedFields = new StringBuilder();
            foreach (var keyValuePair in e.Fields)
            {
                combinedFields.AppendLine(keyValuePair.Value);
            }
            e.Fields.Add("contents", combinedFields.ToString());
        }

        protected void PreRenderViewActionFilterAttribute_ActionExecuted(object sender, ActionExecutedEventArgs e)
        {
            //In this event it's possible to modify the model that will go the view. 
            //If we use a package that has it's own route hijacking (for example Articulate) we can still give it our own master model here.
        }

        #endregion

        #region Other

        private void ClearCache()
        {
            try
            {
                //Clear all output cache so everything is refreshed.
                var cacheManager = new OutputCacheManager();
                cacheManager.RemoveItems();

                //Clear the content finder cache.
                HttpContext.Current.Cache.Remove("CachedNewsNodes");
            }
            catch (Exception ex)
            {
                LogHelper.Error<UmbracoEvents>(string.Format("Exception: {0} - StackTrace: {1}", ex.Message, ex.StackTrace), ex);
            }
        }

        #endregion
    }
}