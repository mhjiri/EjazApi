using System;
using Application.Core;
using Microsoft.Extensions.Configuration;

namespace Application.Subscriptions.Core
{
    public class SubscriptionParams : PagingParams
    {
        public string Search { get; set; }
        public string Status { get; set; }
    }
}