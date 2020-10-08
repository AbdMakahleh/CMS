using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.ApiResponse
{
   public class PaginationResponseResult : ResponseResult
    {
        public PaginationResult Data { get; set; }
    }

    public class PaginationResult
    {
        public IList Items { get; set; }
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
    }
}
