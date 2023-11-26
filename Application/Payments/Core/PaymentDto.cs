using Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Payments.Core
{
    public class PaymentDto
    {

        public Guid Pm_ID { get; set; }

        public Guid Py_ID { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentMethod_Ar { get; set; }

        public Guid Sb_ID { get; set; }
        public string Subscription { get; set; }
        public string Subscription_Ar { get; set; }

        public string Pm_Subscriber { get; set; }
        public string SubscriberName { get; set; }
        public string SubscriberPhoneNumber { get; set; }
        public string SubscriberEmail { get; set; }

        public string Pm_RefernceID { get; set; }

        public double Pm_DisplayPrice { get; set; }
        public int Pm_Days { get; set; }
        public double Pm_Price { get; set; }
        public int Pm_Result { get; set; }

        public int Pm_Ordinal { get; set; }
        public bool Pm_Active { get; set; } = false;

        public string Pm_Creator { get; set; }
        public DateTime Pm_CreatedOn { get; set; } = DateTime.UtcNow;
    }
}