using System;
using Application.Core;
using Microsoft.Extensions.Configuration;

namespace Application.BannerLocations.Core
{
    public class BannerLocationParams : PagingParams
    {
        public string Search { get; set; }
        public string Status { get; set; }
    }
}