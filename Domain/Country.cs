using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Country
    {
        [Key]
        public Guid Cn_ID { get; set; }

        public Guid Md_ID { get; set; }

        public int Cn_Code { get; set; }
        public string Cn_Title { get; set; }
        public string Cn_Title_Ar { get; set; }
        public string Cn_Desc { get; set; }
        public string Cn_Desc_Ar { get; set; }

        public bool Cn_Active { get; set; } = false;
        public DateTime Cn_CreatedOn { get; set; } = DateTime.UtcNow;
        public string Cn_Creator { get; set; }
        public DateTime? Cn_ModifyOn { get; set; }
        public string Cn_Modifier { get; set; }

        [ForeignKey("Md_ID")]
        public Medium Flag { get; set; }
        [ForeignKey("Cn_Creator")]
        public AppUser Creator { get; set; }
        [ForeignKey("Cn_Modifier")]
        public AppUser Modifier { get; set; }

        
        public virtual ICollection<Publisher> Publishers { get; set; } 
    }
}