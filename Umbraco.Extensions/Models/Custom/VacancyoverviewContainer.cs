using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Extensions.Models.Generated;

namespace Umbraco.Extensions.Models.Custom
{
    public class VacancyoverviewContainer
    {
        public IEnumerable<Vacancyitem> VacancyItems { get; set; }
        public Pager Pager { get; set; }
    }
}