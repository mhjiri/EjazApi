using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    [PrimaryKey(nameof(Gr_ID), nameof(Ct_ID))]
    public class GroupCategory
    {
        public Guid Gr_ID { get; set; }
        public Guid Ct_ID { get; set; }

        public int GrCt_Ordinal { get; set; }
        public bool GrCt_Active { get; set; } = false;
        public DateTime GrCt_CreatedOn { get; set; } = DateTime.UtcNow;
        public string GrCt_Creator { get; set; }
        public DateTime? GrCt_ModifyOn { get; set; }
        public string GrCt_Modifier { get; set; }

        [ForeignKey("Gr_ID")]
        public Group Group { get; set; }
        [ForeignKey("Ct_ID")]
        public Category Category { get; set; }
        [ForeignKey("GrCt_Creator")]
        public AppUser Creator { get; set; }
        [ForeignKey("GrCt_Modifier")]
        public AppUser Modifier { get; set; }

    }
}