using System;
using Application.Core;
using Microsoft.Extensions.Configuration;

namespace Application.Banners.Core
{
    public class BannerParams : PagingParams
    {
        public string Search { get; set; }
        public string Status { get; set; }
    }
}