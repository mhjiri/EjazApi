using System;
using Application.Core;
using Microsoft.Extensions.Configuration;

namespace Application.Groups.Core
{
    public class GroupParams : PagingParams
    {
        public string Search { get; set; }
        public string Status { get; set; }
    }
}