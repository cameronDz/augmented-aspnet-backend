using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AugmentedAspnetBackend.Models.ApiHelpers
{
    public class ApiResponseModel
    {
        public IQueryable Data { get; set; }
        public ApiMetaDataModel Meta { get; set; }
        public ApiLinksModel Links { get; set; }
    }
}