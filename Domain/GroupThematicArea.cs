using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    [PrimaryKey(nameof(Gr_ID), nameof(Th_ID))]
    public class GroupThematicArea
    {
        public Guid Gr_ID { get; set; }
        public Guid Th_ID { get; set; }

        public int GrTh_Ordinal { get; set; }
        public bool GrTh_Active { get; set; } = false;
        public DateTime GrTh_CreatedOn { get; set; } = DateTime.UtcNow;
        public string GrTh_Creator { get; set; }
        public DateTime? GrTh_ModifyOn { get; set; }
        public string GrTh_Modifier { get; set; }

        [ForeignKey("Gr_ID")]
        public Group Group { get; set; }
        [ForeignKey("Th_ID")]
        public ThematicArea ThematicArea { get; set; }
        [ForeignKey("GrTh_Creator")]
        public AppUser Creator { get; set; }
        [ForeignKey("GrTh_Modifier")]
        public AppUser Modifier { get; set; }

    }
}