﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.DAL.Helpers
{
    public class PagingParams
    {
        private const int MaxPageSize=50;
        public int PageNumber { get; set; }
        private int _pageSize=10;
        public int PageSize
        {
            get=> _pageSize;
            set=> _pageSize =(value>MaxPageSize)? MaxPageSize : value;
        }
    }
}
