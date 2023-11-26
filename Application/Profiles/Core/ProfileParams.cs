using System;
using Application.Core;
using Microsoft.Extensions.Configuration;

namespace Application.Profiles.Core
{
    public class ProfileParams : PagingParams
    {
        public string Search { get; set; }
        public string Status { get; set; }
    }
}