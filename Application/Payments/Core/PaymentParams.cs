using System;
using Application.Core;
using Microsoft.Extensions.Configuration;

namespace Application.Payments.Core
{
    public class PaymentParams : PagingParams
    {
        public string Search { get; set; }
        public string Status { get; set; }
    }
}