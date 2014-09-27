using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Umbraco.Extensions.Models.Custom
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string ActiveClass { get; set; }
        public string MenuIcon { get; set; }
    }
}