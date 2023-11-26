using System;
using Application.Core;
using Microsoft.Extensions.Configuration;

namespace Application.Publishers.Core
{
    public class PublisherParams : PagingParams
    {
        public string Search { get; set; }
        public string Status { get; set; }
    }
}