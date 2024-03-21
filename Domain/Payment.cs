using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    
    public class Payment
    {
        [Key]
        public Guid Pm_ID { get; set; }
        
        public Guid Py_ID { get; set; }
        public Guid Sb_ID { get; set; }
        public string Pm_Subscriber { get; set; }

        public string Pm_RefernceID { get; set; }

        public double Pm_DisplayPrice { get; set; }
        public int Pm_Days { get; set; }
        public double Pm_Price { get; set; }
        public int Pm_Result { get; set; }

        public int Pm_Ordinal { get; set; }
        public bool Pm_Active { get; set; } = false;

        public DateTime Pm_CreatedOn { get; set; } = DateTime.UtcNow;
        public string Pm_Creator { get; set; }


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