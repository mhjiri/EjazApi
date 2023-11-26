using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    [PrimaryKey(nameof(AnCn_ID), nameof(AnCh_ID))]
    public class AnnouncementConditionAnnouncementChannel
    {
        public Guid AnCn_ID { get; set; }
        public Guid AnCh_ID { get; set; }

        public int AnChGr_Ordinal { get; set; }
        public bool AnChGr_Active { get; set; } = false;
        public DateTime AnChGr_CreatedOn { get; set; } = DateTime.UtcNow;
        public string AnChGr_Creator { get; set; }
        public DateTime? AnChGr_ModifyOn { get; set; }
        public string AnChGr_Modifier { get; set; }

        [ForeignKey("AnCn_ID")]
        public AnnouncementCondition Condition { get; set; }
        [ForeignKey("AnCh_ID")]
        public AnnouncementChannel Channel { get; set; }
        [ForeignKey("AnCnGr_Creator")]
        public AppUser Creator { get; set; }
        [ForeignKey("AnCnGr_Modifier")]
        public AppUser Modifier { get; set; }

    }
}