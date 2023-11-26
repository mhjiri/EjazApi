using System;
using Application.Core;
using Microsoft.Extensions.Configuration;

namespace Application.ThematicAreas.Core
{
    public class ThematicAreaParams : PagingParams
    {
        public string Search { get; set; }
        public string Status { get; set; }
    }
}