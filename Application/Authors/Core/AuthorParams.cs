using System;
using Application.Core;
using Microsoft.Extensions.Configuration;

namespace Application.Authors.Core
{
    public class AuthorParams : PagingParams
    {
        public string Search { get; set; }
        public string Status { get; set; }
    }
}