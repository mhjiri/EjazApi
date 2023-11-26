using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    [PrimaryKey(nameof(Gr_ID), nameof(Tg_ID))]
    public class GroupTag
    {
        public Guid Gr_ID { get; set; }
        public Guid Tg_ID { get; set; }

        public int GrTg_Ordinal { get; set; }
        public bool GrTg_Active { get; set; } = false;
        public DateTime GrTg_CreatedOn { get; set; } = DateTime.UtcNow;
        public string GrTg_Creator { get; set; }
        public DateTime? GrTg_ModifyOn { get; set; }
        public string GrTg_Modifier { get; set; }

        [ForeignKey("Gr_ID")]
        public Group Group { get; set; }
        [ForeignKey("Tg_ID")]
        public Tag Tag { get; set; }
        [ForeignKey("GrTg_Creator")]
        public AppUser Creator { get; set; }
        [ForeignKey("GrTg_Modifier")]
        public AppUser Modifier { get; set; }

    }
}