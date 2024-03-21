using System;
using Application.Core;
using Microsoft.Extensions.Configuration;

namespace Application.Genres.Core
{
    public class GenreParams : PagingParams
    {
        public string Search { get; set; }
        public string Status { get; set; }
    }
}