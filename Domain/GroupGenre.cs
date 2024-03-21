using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    [PrimaryKey(nameof(Gr_ID), nameof(Gn_ID))]
    public class GroupGenre
    {
        public Guid Gr_ID { get; set; }
        public Guid Gn_ID { get; set; }

        public int GrGn_Ordinal { get; set; }
        public bool GrGn_Active { get; set; } = false;
        public DateTime GrGn_CreatedOn { get; set; } = DateTime.UtcNow;
        public string GrGn_Creator { get; set; }
        public DateTime? GrGn_ModifyOn { get; set; }
        public string GrGn_Modifier { get; set; }

        [ForeignKey("Gr_ID")]
        public Group Group { get; set; }
        [ForeignKey("Gn_ID")]
        public Genre Genre { get; set; }
        [ForeignKey("GrGn_Creator")]
        public AppUser Creator { get; set; }
        [ForeignKey("GrGn_Modifier")]
        public AppUser Modifier { get; set; }

    }
}