using System;
using Application.Core;
using Microsoft.Extensions.Configuration;

namespace Application.Categories.Core
{
    public class CategoryParams : PagingParams
    {
        public string Search { get; set; }
        public string Status { get; set; }
    }
}