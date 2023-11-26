using System;
using Application.Core;
using Microsoft.Extensions.Configuration;

namespace Application.Countries.Core
{
    public class CountryParams : PagingParams
    {
        public string Search { get; set; }
        public string Status { get; set; }
    }
}