using System;
using Application.Core;
using Microsoft.Extensions.Configuration;

namespace Application.PaymentMethods.Core
{
    public class PaymentMethodParams : PagingParams
    {
        public string Search { get; set; }
        public string Status { get; set; }
    }
}