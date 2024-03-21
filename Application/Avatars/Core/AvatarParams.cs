using System;
using Application.Core;
using Microsoft.Extensions.Configuration;

namespace Application.Avatars.Core
{
    public class AvatarParams : PagingParams
    {
        public string Search { get; set; }
        public string Status { get; set; }
    }
}