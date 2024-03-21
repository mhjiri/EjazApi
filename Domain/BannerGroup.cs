using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    [PrimaryKey(nameof(Bn_Id), nameof(Gr_ID))]
    public class BannerGroup
    {
        [Key]
        public Guid Bn_Id { get; set; }
        [Key]
        public Guid Gr_ID { get; set; }

        public int BnGr_Ordinal { get; set; }
        public bool BnGr_Active { get; set; } = false;
        public DateTime BnGr_CreatedOn { get; set; } = DateTime.UtcNow;
        public string BnGr_Creator { get; set; }
        public DateTime? BnGr_ModifyOn { get; set; }
        public string BnGr_Modifier { get; set; }

        [ForeignKey("Bn_Id")]
        public Banner Banner { get; set; }
        [ForeignKey("Gr_ID")]
        public Group Group { get; set; }
        [ForeignKey("BnGr_Creator")]
        public AppUser Creator { get; set; }
        [ForeignKey("BnGr_Modifier")]
        public AppUser Modifier { get; set; }

    }
}