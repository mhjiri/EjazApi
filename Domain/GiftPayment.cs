using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class GiftPayment
    {
        [Key]
        public Guid Pm_ID { get; set; }
        public Guid Py_ID { get; set; } // Payment Method : Cash, Card, In-App
        public Guid Sb_ID { get; set; } // Subscriber : Monthly, Yearly
        public string Pm_Payer { get; set; }
        public string Pm_PayedBy { get; set; }
        public string Pm_RefernceID { get; set; }
        public double Pm_DisplayPrice { get; set; }
        public int Pm_Days { get; set; }
        public double Pm_Price { get; set; }
        public int Pm_Result { get; set; }
        public int Pm_Ordinal { get; set; }
        public string PM_Recipient { get; set; }
        public string PM_CouponCode { get; set; }
        public DateTime PM_GiftedOn { get; set; } = DateTime.UtcNow;
        public bool Pm_Active { get; set; } = true;
        public bool PM_Used { get; set; } = false;

        [ForeignKey("Py_ID")]
        public PaymentMethod PaymentMethod { get; set; }

        [ForeignKey("Sb_ID")]
        public Subscription Subscription { get; set; }

        [ForeignKey("Pm_Subscriber")]
        public AppUser Subscriber { get; set; }

        [ForeignKey("Pm_Creator")]
        public AppUser Creator { get; set; }

    }
}
