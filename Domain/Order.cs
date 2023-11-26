using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Order
    {
        [Key]
        public Guid Or_ID { get; set; }

        public Guid? Py_ID { get; set; }
        public string Cs_ID { get; set; }
        public Guid Or_AddressID { get; set; }
        public Guid Or_BillingAddressID { get; set; }


        public string Or_CustomerNotes { get; set; }
        public string Or_StaffNotes { get; set; }
        public string Or_PromotionCode { get; set; }
        public double Or_SubTotal { get; set; }
        public double Or_TotalDiscount { get; set; }
        public double Or_Vat { get; set; }
        public double Or_Total { get; set; }

        public string Or_PaymentStatus { get; set; }
        public string Or_PaymentNotes { get; set; }
        public string Or_PaymentDesc { get; set; }
        public string Or_PaymentReferenceNo { get; set; }

        public bool Or_Active { get; set; } = false;
        public bool Or_IsPaid { get; set; } = false;
        public string Or_Status { get; set; }
        public DateTime Or_CreatedOn { get; set; } = DateTime.UtcNow;
        public string Or_Creator { get; set; }
        public DateTime? Or_ModifyOn { get; set; }
        public string Or_Modifier { get; set; }

        [ForeignKey("Cs_ID")]
        public AppUser Customer { get; set; }
        [ForeignKey("Or_AddressID")]
        public Address Address { get; set; }
        [ForeignKey("Or_BillingAddressID")]
        public Address BillingAddress { get; set; }
        [ForeignKey("Py_ID")]
        public PaymentMethod PaymentMethod { get; set; }
        [ForeignKey("Or_Creator")]
        public AppUser Creator { get; set; }
        [ForeignKey("Or_Modifier")]
        public AppUser Modifier { get; set; }

         public virtual ICollection<OrderSubscription> Subscriptions { get; set; }
    }
}

