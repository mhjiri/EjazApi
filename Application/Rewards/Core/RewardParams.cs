using System;
using Application.Core;
using Microsoft.Extensions.Configuration;

namespace Application.Rewards.Core
{
    public class RewardParams : PagingParams
    {
        public string Search { get; set; }
        public string Status { get; set; }
    }
}