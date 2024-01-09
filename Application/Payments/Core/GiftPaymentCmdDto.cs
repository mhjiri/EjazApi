using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payments.Core
{
    public class GiftPaymentCmdDto : PaymentCmdDto
    {
        public string PM_Recipient { get; set; } // Gift to : Email ??

        #nullable enable
        public string? PM_CouponCode { get; set; } // Guid/Alpha-numeric
        public DateTime PM_GiftedOn { get; set; } = DateTime.Now;

    }
}
