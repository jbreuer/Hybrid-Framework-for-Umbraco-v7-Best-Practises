using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Umbraco.Extensions.Enums;
using Umbraco.Extensions.Models;
using Umbraco.Extensions.Models.Custom;
using Umbraco.Extensions.Models.Generated;
using Umbraco.Extensions.Models.Rdbms;
using Umbraco.Web;
using Umbraco.Web.Models;
using UrlPicker.Umbraco.Models;

namespace Umbraco.Extensions.Utilities
{
    public static class ExtensionMethods
    {
        private static UmbracoHelper Umbraco
        {
            get { return new UmbracoHelper(UmbracoContext.Current); }
        }

        #region ImageCropDataSet - Methods

        public static string GetResponsiveCropUrl(
            this ImageCropDataSet cropDataSet,
            int width, 
            int height
            )
        {
            return cropDataSet.GetCropUrl(
                width: width, 
                height: height, 
                quality: 90, 
                ratioMode: ImageCropRatioMode.Height, 
                furtherOptions: "&slimmage=true"
            );
        }

        public static string GetCropUrl(
            this ImageCropDataSet cropDataSet,
            int? width = null,
            int? height = null,
            string cropAlias = null,
            int? quality = null,
            ImageCropMode? imageCropMode = null,
            ImageCropAnchor? imageCropAnchor = null,
            bool preferFocalPoint = false,
            bool useCropDimensions = false,
            string cacheBusterValue = null,
            string furtherOptions = null,
            ImageCropRatioMode? ratioMode = null,
            bool upScale = true
        )
        {
            var imageResizerUrl = new StringBuilder();

            imageResizerUrl.Append(cropDataSet.Src);

            if (imageCropMode == ImageCropMode.Crop || imageCropMode == null)
            {
                var crop = cropDataSet.GetCrop(cropAlias);

                var cropBaseUrl = cropDataSet.GetCropBaseUrl(cropAlias, preferFocalPoint);
                if (cropBaseUrl != null)
                {
                    imageResizerUrl.Append(cropBaseUrl);
                }
                else
                {
                    return null;
                }

                if (crop != null & useCropDimensions)
                {
                    width = crop.Width;
                    height = crop.Height;
                }
            }
            else
            {
                imageResizerUrl.Append("?mode=" + imageCropMode.ToString().ToLower());

                if (imageCropAnchor != null)
                {
                    imageResizerUrl.Append("&anchor=" + imageCropAnchor.ToString().ToLower());
                }
            }

            if (quality != null)
            {
                imageResizerUrl.Append("&quality=" + quality);
            }

            if (width != null && ratioMode != ImageCropRatioMode.Width)
            {
                imageResizerUrl.Append("&width=" + width);
            }

            if (height != null && ratioMode != ImageCropRatioMode.Height)
            {
                imageResizerUrl.Append("&height=" + height);
            }

            if (ratioMode == ImageCropRatioMode.Width && height != null)
            {
                //if only height specified then assume a sqaure
                if (width == null)
                {
                    width = height;
                }
                var widthRatio = (decimal)width / (decimal)height;
                imageResizerUrl.Append("&widthratio=" + widthRatio.ToString(CultureInfo.InvariantCulture));
            }

            if (ratioMode == ImageCropRatioMode.Height && width != null)
            {
                //if only width specified then assume a sqaure
                if (height == null)
                {
                    height = width;
                }
                var heightRatio = (decimal)height / (decimal)width;
                imageResizerUrl.Append("&heightratio=" + heightRatio.ToString(CultureInfo.InvariantCulture));
            }

            if (upScale == false)
            {
                imageResizerUrl.Append("&upscale=false");
            }

            if (furtherOptions != null)
            {
                imageResizerUrl.Append(furtherOptions);
            }

            if (cacheBusterValue != null)
            {
                imageResizerUrl.Append("&rnd=").Append(cacheBusterValue);
            }

            return imageResizerUrl.ToString();
        }

        public static ImageCropData GetCrop(this ImageCropDataSet dataset, string cropAlias)
        {
            if (dataset == null || dataset.Crops == null || !dataset.Crops.Any())
                return null;

            return dataset.Crops.GetCrop(cropAlias);
        }

        public static ImageCropData GetCrop(this IEnumerable<ImageCropData> dataset, string cropAlias)
        {
            if (dataset == null || !dataset.Any())
                return null;

            if (string.IsNullOrEmpty(cropAlias))
                return dataset.FirstOrDefault();

            return dataset.FirstOrDefault(x => x.Alias.ToLowerInvariant() == cropAlias.ToLowerInvariant());
        }

        public static string GetCropBaseUrl(this ImageCropDataSet cropDataSet, string cropAlias, bool preferFocalPoint)
        {
            var cropUrl = new StringBuilder();

            var crop = cropDataSet.GetCrop(cropAlias);

            // if crop alias has been specified but not found in the Json we should return null
            if (string.IsNullOrEmpty(cropAlias) == false && crop == null)
            {
                return null;
            }
            if ((preferFocalPoint && cropDataSet.HasFocalPoint()) || (crop != null && crop.Coordinates == null && cropDataSet.HasFocalPoint()) || (string.IsNullOrEmpty(cropAlias) && cropDataSet.HasFocalPoint()))
            {
                cropUrl.Append("?center=" + cropDataSet.FocalPoint.Top.ToString(CultureInfo.InvariantCulture) + "," + cropDataSet.FocalPoint.Left.ToString(CultureInfo.InvariantCulture));
                cropUrl.Append("&mode=crop");
            }
            else if (crop != null && crop.Coordinates != null)
            {
                cropUrl.Append("?crop=");
                cropUrl.Append(crop.Coordinates.X1.ToString(CultureInfo.InvariantCulture)).Append(",");
                cropUrl.Append(crop.Coordinates.Y1.ToString(CultureInfo.InvariantCulture)).Append(",");
                cropUrl.Append(crop.Coordinates.X2.ToString(CultureInfo.InvariantCulture)).Append(",");
                cropUrl.Append(crop.Coordinates.Y2.ToString(CultureInfo.InvariantCulture));
                cropUrl.Append("&cropmode=percentage");
            }
            else
            {
                cropUrl.Append("?anchor=center");
                cropUrl.Append("&mode=crop");
            }
            return cropUrl.ToString();
        }

        #endregion

        #region IPublishedContent - Methods

        #region Node

        /// <summary>
        /// Return the Website generated model (highest node) where default settings are stored.
        /// </summary>
        public static Website Website(this IPublishedContent content, bool noCache = false)
        {
            var website = (Website)System.Web.HttpContext.Current.Items["Website"];

            if (website == null || noCache)
            {
                website = content.AncestorOrSelf(1) as Website;

                if (!noCache)
                {
                    System.Web.HttpContext.Current.Items["Website"] = website;
                }
            }

            return website;
        }

        #endregion

        #endregion

        #region IContent - Methods

        #region Node

        public static IEnumerable<IContent> AncestorsOrSelf(this IContent content, bool orSelf = true)
        {
            var ancestors = new List<IContent>();

            if (orSelf)
            {
                ancestors.Add(content);
            }

            while (content.Parent() != null) // while we have a parent, consider the parent
            {
                content = content.Parent();
                ancestors.Add(content);
            }

            ancestors.Reverse();
            return ancestors;
        }

        #endregion

        #endregion

        #region UmbracoHelper - Methods

        #region Form

        /// <summary>
        /// Given the set of replacement values and a list of email fields, construct and send the required emails.
        /// </summary>
        /// <param name="emailValues">The replacement values</param>
        /// <param name="formAliases">The node property aliases, relevant to the current node.</param>
        public static void ProcessForms(this UmbracoHelper umbraco, Dictionary<string, string> emailValues, IEnumerable<EmailFields> emailFieldsList, EmailType? emailType, bool addFiles = false)
        {
            var streams = new Dictionary<string, MemoryStream>();

            if (addFiles)
            {
                var files = HttpContext.Current.Request.Files;
                foreach (string fileKey in files)
                {
                    var file = files[fileKey];

                    //Only add the file if one has been selected.
                    if (file.ContentLength > 0)
                    {
                        file.InputStream.Position = 0;
                        var memoryStream = new MemoryStream();
                        file.InputStream.CopyTo(memoryStream);
                        streams.Add(file.FileName, memoryStream);
                    }
                }
            }

            foreach (var emailFields in emailFieldsList)
            {
                if (emailFields.Send 
                    && !string.IsNullOrWhiteSpace(emailFields.SenderName)
                    && !string.IsNullOrWhiteSpace(emailFields.SenderEmail)
                    && !string.IsNullOrWhiteSpace(emailFields.ReceiverEmail)
                    && !string.IsNullOrWhiteSpace(emailFields.Subject)
                    )
                {
                    var attachments = new Dictionary<string, MemoryStream>();
                    foreach (var stream in streams)
                    {
                        var memoryStream = new MemoryStream();
                        stream.Value.Position = 0;
                        stream.Value.CopyTo(memoryStream);
                        memoryStream.Position = 0;
                        attachments.Add(stream.Key, memoryStream);
                    }

                    ReplacePlaceholders(emailFields, emailValues);
                    emailFields.Body = AddImgAbsolutePath(emailFields.Body);
                    Umbraco.SendEmail(
                        emailFields.SenderEmail,
                        emailFields.SenderName,
                        emailFields.ReceiverEmail,
                        emailFields.Subject,
                        emailFields.Body,
                        emailFields.CcEmail,
                        emailFields.BccEmail,
                        emailType: emailType,
                        addFiles: addFiles,
                        attachments: attachments
                        );
                }
            }
        }

        /// <summary>
        /// Using a dictionary of replacement keys with their corresponding values,
        /// replace the placeholders in each of the email form fields. Dictionary
        /// keys have the placeholder brackets ("[]") added to them, so these
        /// don't need to be included.
        /// </summary>
        /// <param name="template">The email template to process.</param>
        /// <param name="phData">The placeholder data.</param>
        private static void ReplacePlaceholders(EmailFields emailFields, Dictionary<string, string> phData)
        {
            emailFields.Subject = ReplacePlaceholders(emailFields.Subject, phData);
            emailFields.Body = ReplacePlaceholders(emailFields.Body, phData);
            emailFields.ReceiverEmail = ReplacePlaceholders(emailFields.ReceiverEmail, phData);
            emailFields.CcEmail = ReplacePlaceholders(emailFields.CcEmail, phData);
            emailFields.BccEmail = ReplacePlaceholders(emailFields.BccEmail, phData);
            emailFields.SenderEmail = ReplacePlaceholders(emailFields.SenderEmail, phData);
            emailFields.SenderName = ReplacePlaceholders(emailFields.SenderName, phData);
        }

        private static string ReplacePlaceholders(string templateString, Dictionary<string, string> phData, bool escapeHtml = false)
        {
            StringBuilder templ = new StringBuilder(templateString);

            foreach (var kv in phData)
            {
                var val = kv.Value;
                if (escapeHtml)
                {
                    val = HttpContext.Current.Server.HtmlEncode(val);
                }
                templ.Replace("[" + kv.Key + "]", val);
            }

            return templ.ToString();
        }

        /// <summary>
        /// Add an absolute path to all the img tags in the html of an e-mail.
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        private static string AddImgAbsolutePath(string html)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            var uri = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);
            var domainUrl = string.Format("{0}://{1}", uri.Scheme, uri.Authority);

            if (doc.DocumentNode.SelectNodes("//img[@src]") != null)
            {
                foreach (HtmlNode img in doc.DocumentNode.SelectNodes("//img[@src]"))
                {
                    HtmlAttribute att = img.Attributes["src"];
                    if (att.Value.StartsWith("/"))
                    {
                        att.Value = domainUrl + att.Value;
                    }
                }
            }

            return doc.DocumentNode.InnerHtml;
        }

        #endregion

        #region Email

        /// <summary>
        /// Send the e-mail.
        /// </summary>
        /// <param name="umbraco"></param>
        /// <param name="emailFrom"></param>
        /// <param name="emailFromName"></param>
        /// <param name="emailTo"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="emailCc"></param>
        /// <param name="emailBcc"></param>
        /// <param name="emailType"></param>
        /// <param name="addFiles"></param>
        /// <param name="attachments"></param>
        public static void SendEmail(
            this UmbracoHelper umbraco, 
            string emailFrom, 
            string emailFromName, 
            string emailTo, 
            string subject, 
            string body, 
            string emailCc = "", 
            string emailBcc = "",
            EmailType? emailType = null,
            bool addFiles = false,
            Dictionary<string, MemoryStream> attachments = null
            )
        {
            //Make the MailMessage and set the e-mail address.
            MailMessage message = new MailMessage();
            message.From = new MailAddress(emailFrom, emailFromName);

            //Split all the e-mail addresses on , or ;.
            char[] splitChar = { ',', ';' };

            //Remove all spaces.
            emailTo = emailTo.Trim();
            emailCc = emailCc.Trim();
            emailBcc = emailBcc.Trim();

            //Split emailTo.
            string[] toArray = emailTo.Split(splitChar, StringSplitOptions.RemoveEmptyEntries);
            foreach (string to in toArray)
            {
                //Check if the e-mail is valid.
                if (umbraco.IsValidEmail(to))
                {
                    //Loop through all e-mail addressen in toArray and add them in the to.
                    message.To.Add(new MailAddress(to));
                }
            }

            //Split emailCc.
            string[] ccArray = emailCc.Split(splitChar, StringSplitOptions.RemoveEmptyEntries);
            foreach (string cc in ccArray)
            {
                //Check if the e-mail is valid.
                if (umbraco.IsValidEmail(cc))
                {
                    //Loop through all e-mail addressen in ccArray and add them in the cc.
                    message.CC.Add(new MailAddress(cc));
                }
            }

            //Split emailBcc.
            string[] bccArray = emailBcc.Split(splitChar, StringSplitOptions.RemoveEmptyEntries);
            foreach (string bcc in bccArray)
            {
                //Check if the e-mail is valid.
                if (umbraco.IsValidEmail(bcc))
                {
                    //Loop through all e-mail addressen in bccArray and add them in the bcc.
                    message.Bcc.Add(new MailAddress(bcc));
                }
            }

            if (addFiles && attachments != null)
            {
                foreach (var attachment in attachments)
                {
                    var att = new Attachment(attachment.Value, attachment.Key);
                    message.Attachments.Add(att);
                }
            }

            //Set the rest of the e-mail data.
            message.Subject = subject;
            message.SubjectEncoding = Encoding.UTF8;
            message.Body = body;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;

            //Only send the email if there is a to address.
            if (message.To.Any())
            {
                if (emailType.HasValue)
                {
                    try
                    {
                        //Get the database.
                        var database = ApplicationContext.Current.DatabaseContext.Database;

                        //Create the email object and set all properties.
                        var email = new EmailDto()
                        {
                            Type = emailType.Value.ToString(),
                            FromName = emailFromName,
                            FromEmail = emailFrom,
                            ToEmail = emailTo,
                            CCEmail = emailCc,
                            BCCEmail = emailBcc,
                            Date = DateTime.Now,
                            Subject = subject,
                            Message = body
                        };

                        //Insert the email into the database.
                        database.Insert(email);
                    }
                    catch (Exception ex)
                    {
                        Umbraco.LogException<UmbracoHelper>(ex);
                    }
                }

                //Make the SmtpClient.
                SmtpClient smtpClient = new SmtpClient();

                //Send the e-mail.
                smtpClient.Send(message);
            }

            //Clear the resources.
            message.Dispose();
        }

        #endregion

        #region Validation

        /// <summary>
        /// Checks if the e-mail is valid.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsValidEmail(this UmbracoHelper umbraco, string email)
        {
            Regex r = new Regex(@"([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})");
            return r.IsMatch(email);
        }

        #endregion

        #region Error

        /// <summary>
        /// Log an exception and send an email.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="nodeId"></param>
        /// <param name="type"></param>
        public static void LogException<T>(this UmbracoHelper umbraco, Exception ex)
        {
            try
            {
                int nodeId = -1;
                if (System.Web.HttpContext.Current.Items["pageID"] != null)
                {
                    int.TryParse(System.Web.HttpContext.Current.Items["pageID"].ToString(), out nodeId);
                }

                StringBuilder comment = new StringBuilder();
                StringBuilder commentHtml = new StringBuilder();

                commentHtml.AppendFormat("<p><strong>Url:</strong><br/>{0}</p>", System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
                commentHtml.AppendFormat("<p><strong>Node id:</strong><br/>{0}</p>", nodeId);

                //Add the exception.
                comment.AppendFormat("Exception: {0} - StackTrace: {1}", ex.Message, ex.StackTrace);
                commentHtml.AppendFormat("<p><strong>Exception:</strong><br/>{0}</p>", ex.Message);
                commentHtml.AppendFormat("<p><strong>StackTrace:</strong><br/>{0}</p>", ex.StackTrace);

                if (ex.InnerException != null)
                {
                    //Add the inner exception.
                    comment.AppendFormat("- InnerException: {0} - InnerStackTrace: {1}", ex.InnerException.Message, ex.InnerException.StackTrace);
                    commentHtml.AppendFormat("<p><strong>InnerException:</strong><br/>{0}</p>", ex.InnerException.Message);
                    commentHtml.AppendFormat("<p><strong>InnerStackTrace:</strong><br/>{1}</p>", ex.InnerException.StackTrace);
                }

                //Log the Exception into Umbraco.
                LogHelper.Error<T>(comment.ToString(), ex);

                var errorFrom = ConfigurationManager.AppSettings["errorFrom"];
                var errorFromName = ConfigurationManager.AppSettings["errorFromName"];
                var errorTo = ConfigurationManager.AppSettings["errorTo"];

                if (!string.IsNullOrWhiteSpace(errorFrom) && !string.IsNullOrWhiteSpace(errorFromName) && !string.IsNullOrWhiteSpace(errorTo))
                {
                    //Send an email with the exception.
                    umbraco.SendEmail(
                        errorFrom,
                        errorFromName,
                        errorTo,
                        "Error log",
                        commentHtml.ToString());
                }
            }
            catch
            {
                //Do nothing because nothing can be done with this exception.
            }
        }

        #endregion

        #region Pager

        /// <summary>
        /// Return all fields required for paging.
        /// </summary>
        /// <param name="itemsPerPage"></param>
        /// <param name="numberOfItems"></param>
        /// <returns></returns>
        public static Pager GetPager(this UmbracoHelper umbraco, int itemsPerPage, int numberOfItems)
        {
            // paging calculations
            int currentPage = int.TryParse(HttpContext.Current.Request.QueryString["Page"], out currentPage) ? currentPage : 1;
            var numberOfPages = numberOfItems % itemsPerPage == 0 ? Math.Ceiling((decimal)(numberOfItems / itemsPerPage)) : Math.Ceiling((decimal)(numberOfItems / itemsPerPage)) + 1;
            var pages = Enumerable.Range(1, (int)numberOfPages).ToList();

            return new Pager()
            {
                NumberOfItems = numberOfItems,
                ItemsPerPage = itemsPerPage,
                CurrentPage = currentPage,
                Pages = pages
            };
        }

        #endregion

        #region Other

        /// <summary>
        /// Removes the starting and ending paragraph tags in a string.
        /// </summary>
        /// <param name="u"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static HtmlString RemoveFirstParagraphTag(this UmbracoHelper u, IHtmlString text)
        {
            return new HtmlString(umbraco.library.RemoveFirstParagraphTag(text.ToString()));
        }

        /// <summary>
        /// Appends or updates a query string value to the current Url
        /// </summary>
        /// <param name="key">The query string key</param>
        /// <param name="value">The query string value</param>
        /// <returns>The updated Url</returns>
        public static string AppendOrUpdateQueryString(this UmbracoHelper umbraco, string key, string value)
        {
            return umbraco.AppendOrUpdateQueryString(HttpContext.Current.Request.RawUrl, key, value);
        }

        /// <summary>
        /// Appends or updates a query string value to supplied Url
        /// </summary>
        /// <param name="url">The Url to update</param>
        /// <param name="key">The query string key</param>
        /// <param name="value">The query string value</param>
        /// <returns>The updated Url</returns>
        public static string AppendOrUpdateQueryString(this UmbracoHelper umbraco, string url, string key, string value)
        {
            var q = '?';

            if (url.IndexOf(q) == -1)
            {
                return string.Concat(url, q, key, '=', HttpUtility.UrlEncode(value));
            }

            var baseUrl = url.Substring(0, url.IndexOf(q));
            var queryString = url.Substring(url.IndexOf(q) + 1);
            var match = false;
            var kvps = HttpUtility.ParseQueryString(queryString);

            foreach (var queryStringKey in kvps.AllKeys)
            {
                if (queryStringKey == key)
                {
                    kvps[queryStringKey] = value;
                    match = true;
                    break;
                }
            }

            if (!match)
            {
                kvps.Add(key, value);
            }

            return string.Concat(baseUrl, q, ConstructQueryString(kvps, null, false));
        }

        /// <summary>
        /// Constructs a NameValueCollection into a query string.
        /// </summary>
        /// <remarks>Consider this method to be the opposite of "System.Web.HttpUtility.ParseQueryString"</remarks>
        /// <param name="parameters">The NameValueCollection</param>
        /// <param name="delimiter">The String to delimit the key/value pairs</param>
        /// <param name="omitEmpty">Boolean to chose whether to omit empty values</param>
        /// <returns>A key/value structured query string, delimited by the specified String</returns>
        /// <example>
        /// http://blog.leekelleher.com/2009/09/19/how-to-convert-namevaluecollection-to-a-query-string-revised/
        /// </example>
        private static string ConstructQueryString(NameValueCollection parameters, string delimiter, bool omitEmpty)
        {
            if (string.IsNullOrEmpty(delimiter))
                delimiter = "&";

            var equals = '=';
            var items = new List<string>();

            for (var i = 0; i < parameters.Count; i++)
            {
                foreach (var value in parameters.GetValues(i))
                {
                    var addValue = omitEmpty ? !string.IsNullOrEmpty(value) : true;
                    if (addValue)
                    {
                        items.Add(string.Concat(parameters.GetKey(i), equals, HttpUtility.UrlEncode(value)));
                    }
                }
            }

            return string.Join(delimiter, items.ToArray());
        }

        /// <summary>
        /// Disable the cache of a page.
        /// </summary>
        public static void DisableCache(this UmbracoHelper umbraco)
        {
            HttpContext.Current.Response.AddHeader("pragma", "no-cache");
            HttpContext.Current.Response.AddHeader("cache-control", "private");
            HttpContext.Current.Response.CacheControl = "no-cache";
            HttpContext.Current.Response.Cache.SetExpires(DateTime.Now.AddMinutes(-1));
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetNoStore();
        }

        #endregion

        #endregion

        #region Other object - Methods

        public static string Target(this Meta meta)
        {
            return meta.NewWindow ? "_blank" : "_self";
        }

        #endregion
    }
}