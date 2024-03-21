using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    [PrimaryKey(nameof(Ct_ID), nameof(Tg_ID))]
    public class CategoryTag
    { 
        public Guid Ct_ID { get; set; } 
        public Guid Tg_ID { get; set; }

        public int CtTg_Ordinal { get; set; }
        public bool CtTg_Active { get; set; } = false;
        public DateTime CtTg_CreatedOn { get; set; } = DateTime.UtcNow;
        public string CtTg_Creator { get; set; }
        public DateTime? CtTg_ModifyOn { get; set; }
        public string CtTg_Modifier { get; set; }

        [ForeignKey("Ct_ID")]
        public Category Category { get; set; }
        [ForeignKey("Tg_ID")]
        public Tag Tag { get; set; }
        [ForeignKey("CtTg_Creator")]
        public AppUser Creator { get; set; }
        [ForeignKey("CtTg_Modifier")]
        public AppUser Modifier { get; set; }

    }
}