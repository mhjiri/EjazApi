using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payments.Core
{
    public class GiftPaymentDto : PaymentDto
    {
        public string PM_Recipient { get; set; } // Gift to : Email ??
        public string PM_CouponCode { get; set; } // Guid/Alpha-numeric
        public DateTime PM_GiftedOn { get; set; } = DateTime.UtcNow;

    }
}
