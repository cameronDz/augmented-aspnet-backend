using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AugmentedAspnetBackend.Models.ApiHelpers
{
    public class ApiLinksModel
    {
        public string Self { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
        public string Prev { get; set; }
        public string Next { get; set; }
    }
}