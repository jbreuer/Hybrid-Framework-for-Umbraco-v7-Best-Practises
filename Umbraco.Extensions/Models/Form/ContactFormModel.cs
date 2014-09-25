using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Umbraco.Extensions.Models.Form
{
    public class ContactFormModel
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }
}