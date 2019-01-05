using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AugmentedAspnetBackend.Models
{
    public class PagingParameterModel
    {
        private const int MaxPageSize = 20;

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = MaxPageSize;
    }
}