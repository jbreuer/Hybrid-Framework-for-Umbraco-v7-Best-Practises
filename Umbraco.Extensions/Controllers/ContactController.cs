using DevTrends.MvcDonutCaching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Extensions.BLL;
using Umbraco.Extensions.Controllers.Base;
using Umbraco.Extensions.Enums;
using Umbraco.Extensions.Models;
using Umbraco.Extensions.Models.Custom;
using Umbraco.Extensions.Models.Form;
using Umbraco.Extensions.Models.Generated;
using Umbraco.Extensions.Utilities;
using Umbraco.Web;

namespace Umbraco.Extensions.Controllers
{
    public class ContactController : SurfaceRenderMvcController
    {
        [DonutOutputCache(CacheProfile = "OneDay")]
        public ActionResult Contact()
        {
            var masterModel = ModelLogic.CreateMasterModel();
            return CurrentTemplate(masterModel);
        }

        [HttpPost]
        public ActionResult SendContact(ContactFormModel model)
        {
            var contact = CurrentPage as Contact;

            //Set the fields that need to be replaced.
            var formFields = new Dictionary<string, string> 
            {
                {"Name", model.Name},
                {"Email", model.Email},
                {"Phone", model.Phone},
                {"Message", model.Message}
            };

            //Send the e-mail with the filled in form data.
            Umbraco.ProcessForms(formFields, contact.Email, EmailType.Contact);

            //Redirect to the succes page.
            var child = CurrentPage.Children.FirstOrDefault();
            if (child != null)
            {
                return RedirectToUmbracoPage(child);
            }

            return RedirectToCurrentUmbracoPage();

        }
    }
}