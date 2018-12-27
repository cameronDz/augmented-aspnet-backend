using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AugmentedAspnetBackend.Models
{
    public class PagingParameterModel
    {
        private const int MaxPageSize = 20;

        public string SortField { get; set; } = "";

        public int PageNumber { get; set; } = 0;

        public int PageSize { get; set; } = 0;
    }
}