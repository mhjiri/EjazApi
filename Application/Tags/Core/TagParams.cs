using System;
using Application.Core;
using Microsoft.Extensions.Configuration;

namespace Application.Tags.Core
{
    public class TagParams : PagingParams
    {
        public string Search { get; set; }
        public string Status { get; set; }
    }
}