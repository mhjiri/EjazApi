using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    [PrimaryKey(nameof(Rw_Id), nameof(Gr_ID))]
    public class RewardGroup
    { 
        public Guid Rw_Id { get; set; } 
        public Guid Gr_ID { get; set; }

        public int RwGr_Ordinal { get; set; }
        public bool RwGr_Active { get; set; } = false;
        public DateTime RwGr_CreatedOn { get; set; } = DateTime.UtcNow;
        public string RwGr_Creator { get; set; }
        public DateTime? RwGr_ModifyOn { get; set; }
        public string RwGr_Modifier { get; set; }

        [ForeignKey("Rw_Id")]
        public Reward Reward { get; set; }
        [ForeignKey("Gr_ID")]
        public Group Group { get; set; }
        [ForeignKey("RwGr_Creator")]
        public AppUser Creator { get; set; }
        [ForeignKey("RwGr_Modifier")]
        public AppUser Modifier { get; set; }

    }
}