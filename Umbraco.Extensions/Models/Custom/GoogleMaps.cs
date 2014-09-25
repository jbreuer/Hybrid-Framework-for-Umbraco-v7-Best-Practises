using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Umbraco.Extensions.Models.Custom
{
    public class GoogleMaps
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
        public int Zoom { get; set; }
    }
}