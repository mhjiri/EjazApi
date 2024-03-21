using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    [PrimaryKey(nameof(Or_ID), nameof(Sb_ID))]
    public class OrderSubscription
    {
        [Key]
        public Guid Or_ID { get; set; }
        [Key]
        public Guid Sb_ID { get; set; }

        public string OrSb_Code { get; set; }
        public string OrSb_Name { get; set; }
        public string OrSb_Name_Ar { get; set; }
        public string OrSb_Title { get; set; }
        public string OrSb_Title_Ar { get; set; }
        public string OrSb_Desc { get; set; }
        public string OrSb_Desc_Ar { get; set; }

        public double OrSb_Price { get; set; }
        public int OrSb_Quantity { get; set; }
        public double OrSb_Discount { get; set; }
        public double OrSb_SubTotal { get; set; }
        public int OrSb_Ordinal { get; set; }
        public bool OrSb_Active { get; set; } = false;

        public DateTime OrSb_CreatedOn { get; set; } = DateTime.UtcNow;
        public string OrSb_Creator { get; set; }
        public DateTime? OrSb_Modified { get; set; } = DateTime.UtcNow;
        public string OrSb_Modifier { get; set; }


        [ForeignKey("Or_ID")]
        public Order Order { get; set; }
        [ForeignKey("Sb_ID")]
        public Subscription Subscription { get; set; }
        [ForeignKey("OrSb_Creator")]
        public AppUser Creator { get; set; }
        [ForeignKey("OrSb_Modifier")]
        public AppUser Modifier { get; set; }

    }
}