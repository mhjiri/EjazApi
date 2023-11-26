using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    [PrimaryKey(nameof(AnCn_Id), nameof(Gr_ID))]
    public class AnnouncementConditionGroup
    {
        [Key]
        public Guid AnCn_Id { get; set; }
        [Key]
        public Guid Gr_ID { get; set; }

        public int AnCnGr_Ordinal { get; set; }
        public bool AnCnGr_Active { get; set; } = false;
        public DateTime AnCnGr_CreatedOn { get; set; } = DateTime.UtcNow;
        public string AnCnGr_Creator { get; set; }
        public DateTime? AnCnGr_ModifyOn { get; set; }
        public string AnCnGr_Modifier { get; set; }

        [ForeignKey("AnCn_Id")]
        public AnnouncementCondition Condition { get; set; }
        [ForeignKey("Gr_ID")]
        public Group Group { get; set; }
        [ForeignKey("AnCnGr_Creator")]
        public AppUser Creator { get; set; }
        [ForeignKey("AnCnGr_Modifier")]
        public AppUser Modifier { get; set; }

    }
}