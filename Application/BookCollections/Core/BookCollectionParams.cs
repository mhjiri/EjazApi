using System;
using Application.Core;
using Microsoft.Extensions.Configuration;

namespace Application.BookCollections.Core
{
    public class BookCollectionParams : PagingParams
    {
        public string Search { get; set; }
        public string Status { get; set; }
    }
}