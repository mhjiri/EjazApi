using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class Address
    {

        [Key]
        public Guid Ad_ID { get; set; }

        public string Us_ID { get; set; }
        public Guid Cn_ID { get; set; }
        
        public string Ad_Title { get; set; }
        public string Ad_Title_Ar { get; set; }
        public string Ad_Desc { get; set; }
        public string Ad_Desc_Ar { get; set; }
        public bool Ad_IsBilling { get; set; }
        public bool Ad_IsDefault { get; set; }

        public string Ad_Building { get; set; }
        public string Ad_Unit{ get; set; }
        public string Ad_Street { get; set; }
        public string Ad_Zone { get; set; }
        public string Ad_State { get; set; }
        public string Ad_Note { get; set; }

        public bool Ad_Active { get; set; } = false;
        public DateTime Ad_CreatedOn { get; set; } = DateTime.UtcNow;
        public string Ad_Creator { get; set; }
        public DateTime? Ad_ModifyOn { get; set; }
        public string Ad_Modifier { get; set; }

        [ForeignKey("Cn_ID")]
        public Country Country { get; set; }
        [ForeignKey("Us_ID")]
        public AppUser User { get; set; }


    }
}

