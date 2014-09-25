using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Extensions.BLL;
using Umbraco.Extensions.Enums;
using Umbraco.Web;
using Umbraco.Web.Models.ContentEditing;

namespace Umbraco.Extensions.Utilities
{
    public class WebApiHandler : System.Net.Http.DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            switch (request.RequestUri.AbsolutePath.ToLower())
            {
                case "/umbraco/backoffice/umbracoapi/content/getempty":
                case "/umbraco/backoffice/umbracoapi/content/getbyid":
                    return SetContentMediaPickerStartNode(request, cancellationToken);
                case "/umbraco/backoffice/umbracoapi/member/getempty":
                case "/umbraco/backoffice/umbracoapi/member/getbykey":
                    return SetMemberMediaPickerStartNode(request, cancellationToken);
                case "/umbraco/backoffice/archetypeapi/archetypedatatype/getbyguid":
                    return SetArchetypeMediaPickerStartNode(request, cancellationToken);
                default:
                    return base.SendAsync(request, cancellationToken);
            }
        }

        private Task<HttpResponseMessage> SetContentMediaPickerStartNode(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return base.SendAsync(request, cancellationToken)
                .ContinueWith(task =>
                {
                    var response = task.Result;
                    try
                    {
                        //Get the content that we want to modify.
                        var data = response.Content;
                        var content = ((ObjectContent)(data)).Value as ContentItemDisplay;
                        
                        SetMediaPickerStartNode(request, Convert.ToInt32(content.Id), content.ContentTypeAlias, content.Properties);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Error<WebApiHandler>("Could not change the media picker start node.", ex);
                    }
                    return response;
                }
            );
        }

        private Task<HttpResponseMessage> SetMemberMediaPickerStartNode(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return base.SendAsync(request, cancellationToken)
                .ContinueWith(task =>
                {
                    var response = task.Result;
                    try
                    {
                        //Get the content that we want to modify.
                        var data = response.Content;
                        var member = ((ObjectContent)(data)).Value as MemberDisplay;

                        SetMediaPickerStartNode(request, Convert.ToInt32(member.Id), member.ContentTypeAlias, member.Properties);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Error<WebApiHandler>("Could not change the media picker start node.", ex);
                    }
                    return response;
                }
            );
        }

        private void SetMediaPickerStartNode(HttpRequestMessage request, int nodeId, string contentTypeAlias, IEnumerable<ContentPropertyDisplay> properties)
        {
            //Get the media pickers which we want to change the start node from.
            var mediaPickers = properties.Where(x =>
                Constants.PropertyEditors.MediaPickerAlias.InvariantEquals(x.Editor) ||
                Constants.PropertyEditors.MultipleMediaPickerAlias.InvariantEquals(x.Editor)
                ).ToList();

            if (mediaPickers.Any())
            {
                //Get the override values for this content type.
                var contentOverrides = ContentOverrideLogic.GetContentOverrides(contentTypeAlias, configAlias: "startNodeId");

                foreach (var mediaPicker in mediaPickers)
                {
                    //Get the content override for this media picker.
                    //First check if there is a content override for this specific node and property.
                    var contentOverride = contentOverrides.Where(x =>
                        x.NodeId == nodeId
                        && x.PropertyTypeAlias == mediaPicker.Alias
                    ).FirstOrDefault();

                    if (contentOverride == null)
                    {
                        //If no content override is found for a specific node check if there is an override for just the property.
                        contentOverride = contentOverrides.Where(x => x.PropertyTypeAlias == mediaPicker.Alias && x.NodeId == null).FirstOrDefault();
                    }

                    if (contentOverride != null)
                    {
                        //Set the start node id.
                        if (mediaPicker.Config.ContainsKey(contentOverride.ConfigAlias))
                        {
                            mediaPicker.Config[contentOverride.ConfigAlias] = contentOverride.Value;
                        }
                        else
                        {
                            mediaPicker.Config.Add(contentOverride.ConfigAlias, contentOverride.Value);
                        }
                    }
                }
            }
        }

        private Task<HttpResponseMessage> SetArchetypeMediaPickerStartNode(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return base.SendAsync(request, cancellationToken)
                .ContinueWith(task =>
                {
                    var response = task.Result;
                    try
                    {
                        //Get the content that we want to modify.
                        var data = response.Content;
                        var value = ((ObjectContent)(data)).Value;

                        //Use reflection to get the values of the anonymous type that the Archetype WebAPI returns.
                        Type type = value.GetType();
                        var selectedEditor = (string)type.GetProperty("selectedEditor").GetValue(value, null);

                        //Check if the property editor in Archetype is a media picker.
                        if (Constants.PropertyEditors.MediaPickerAlias.InvariantEquals(selectedEditor) ||
                            Constants.PropertyEditors.MultipleMediaPickerAlias.InvariantEquals(selectedEditor))
                        {
                            //Get the start node prevalue.
                            var prevalues = (IEnumerable<PreValueFieldDisplay>)type.GetProperty("preValues").GetValue(value, null);
                            var prevalue = prevalues.Where(x => x.Key == "startNodeId").FirstOrDefault();
                            if (prevalue != null)
                            {
                                //Get the properties we need to find the correct content override.
                                var contentTypeAlias = (string)type.GetProperty("contentTypeAlias").GetValue(value, null);
                                var propertyTypeAlias = (string)type.GetProperty("propertyTypeAlias").GetValue(value, null);
                                var archetypeAlias = (string)type.GetProperty("archetypeAlias").GetValue(value, null);
                                var nodeId = (int)type.GetProperty("nodeId").GetValue(value, null);

                                //Get the override values for this content type.
                                var contentOverrides = ContentOverrideLogic.GetContentOverrides(contentTypeAlias, configAlias: "startNodeId");

                                //Get the content override for this media picker.
                                //First check if there is a content override for this specific node, property and archetype.
                                var contentOverride = contentOverrides.Where(x =>
                                    x.NodeId == nodeId
                                    && HasArchetypePropertyTypeAlias(x.PropertyTypeAlias, propertyTypeAlias)
                                    && x.ArchetypeAlias == archetypeAlias
                                ).FirstOrDefault();

                                if (contentOverride == null)
                                {
                                    //If no content override is found for a specific node check if there is an override for just the property and archetype.
                                    contentOverride = contentOverrides.Where(x =>
                                        HasArchetypePropertyTypeAlias(x.PropertyTypeAlias, propertyTypeAlias)
                                        && x.ArchetypeAlias == archetypeAlias
                                        && x.NodeId == null
                                    ).FirstOrDefault();
                                }

                                if (contentOverride != null)
                                {
                                    //Set the start node.
                                    prevalue.Value = contentOverride.Value;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Error<WebApiHandler>("Could not change the media picker start node.", ex);
                    }
                    return response;
                }
            );
        }

        /// <summary>
        /// Check with some logic if the propertyTypeAlias and archetypePropertyTypeAlias match.
        /// </summary>
        /// <param name="propertyTypeAlias"></param>
        /// <param name="archetypePropertyTypeAlias"></param>
        /// <returns></returns>
        private bool HasArchetypePropertyTypeAlias(string propertyTypeAlias, string archetypePropertyTypeAlias)
        {
            //The value of the propertyTypeAlias in the database could contain multiple parts. 
            //Usually the first part is the real alias and the second part is the nested Archetype alias.
            var parts = propertyTypeAlias.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            //The archetypePropertyTypeAlias has a different format (for example archetype-property-blocks-f0-partners-p1), 
            //but should contain all the parts of the database propertyTypeAlias.
            return parts.Where(x => archetypePropertyTypeAlias.InvariantContains(x)).Count() == parts.Count();
        }
    }
}