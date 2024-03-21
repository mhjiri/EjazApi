using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Subscription
    {
        [Key]
        public Guid Sb_ID { get; set; }

        public Guid? Md_ID { get; set; }


        public string Sb_Code { get; set; }
        public string Sb_Name { get; set; }
        public string Sb_Name_Ar { get; set; }
        public string Sb_Title { get; set; }
        public string Sb_Title_Ar { get; set; }
        public string Sb_Desc { get; set; }
        public string Sb_Desc_Ar { get; set; }
        public double Sb_Price { get; set; }
        public double Sb_DisplayPrice { get; set; }
        public string Sb_DiscountDesc { get; set; }
        public string Sb_DiscountDesc_Ar { get; set; }
        public int Sb_Days { get; set; }

        public bool Sb_Active { get; set; } = false;
        public DateTime Sb_CreatedOn { get; set; } = DateTime.UtcNow;
        public string Sb_Creator { get; set; }
        public DateTime? Sb_ModifyOn { get; set; }
        public string Sb_Modifier { get; set; }

        [ForeignKey("Md_ID")]
        public Medium Image { get; set; }
        [ForeignKey("Sb_Creator")]
        public AppUser Creator { get; set; }
        [ForeignKey("Sb_Modifier")]
        public AppUser Modifier { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }
    }
}

