using System;
using Application.Core;
using Microsoft.Extensions.Configuration;

namespace Application.Books.Core
{
    public class BookParams : PagingParams
    {
        public string Search { get; set; }
        public string Language { get; set; }
        public string Status { get; set; }
    }
}